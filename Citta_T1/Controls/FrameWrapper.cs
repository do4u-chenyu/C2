using Citta_T1.Business.Model;
using Citta_T1.Controls.Interface;
using Citta_T1.Core;
using Citta_T1.Utils;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

namespace Citta_T1.Controls
{
    class FrameWrapperVFX
    {
        private bool backImgMode = false;
        private Pen p = new Pen(Color.Gray, 1f);
        public FrameWrapperVFX()
        {
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
        }
       
        #region 静态图生成相关特效
        public Bitmap CreateWorldImage(int worldWidth,int worldHeight, List<Control> controls, bool mode)
        {
            float screenFactor = Global.GetCanvasPanel().ScreenFactor;
            Bitmap staticImage = new Bitmap(Convert.ToInt32(worldWidth * screenFactor), Convert.ToInt32(worldHeight * screenFactor));
            Graphics g = Graphics.FromImage(staticImage);

            g.SmoothingMode = SmoothingMode.HighQuality;//去掉锯齿
            g.CompositingQuality = CompositingQuality.HighQuality;//合成图像的质量
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;//去掉文字的锯齿

            g.Clear(Color.White);
            DrawAllLines(g);
            if (mode.Equals(backImgMode))
            {
                DrawAllControls(staticImage);
            }
            else
            {
                DrawSelectControls(staticImage, controls);
            } 
            g.Dispose();
            return staticImage;
        }
        public Bitmap UpdateMoveImg(Rectangle minBodingRec,int worldWidth,int worldHeight, List<Control> controls)
        {
            if (minBodingRec.Width == 0 || minBodingRec.Y == 0)
                return null;
            Bitmap moveImage = new Bitmap(minBodingRec.Width + 2, minBodingRec.Height + 3);
            Rectangle realRect = new Rectangle(minBodingRec.Location, new Size(minBodingRec.Width + 2, minBodingRec.Height + 3));
            Bitmap staticImageFrame = CreateWorldImage(worldWidth, worldHeight, controls, true);
            DrawRoundRect(minBodingRec, staticImageFrame, 2);
            Graphics gM = Graphics.FromImage(moveImage);
            gM.DrawImage(staticImageFrame, 0, 0, realRect, GraphicsUnit.Pixel);
            SetImgTransparency(moveImage);
            gM.Dispose();
            staticImageFrame.Dispose();
            staticImageFrame = null;

            return moveImage;

        }
        private void DrawAllLines(Graphics g)
        {
            List<ModelRelation> modelRelations = Global.GetCurrentDocument().ModelRelations;

            // 先画线，避免线盖住控件
            foreach (ModelRelation mr in modelRelations)
            {

                Point Pw = Global.GetCurrentDocument().WorldMap.ScreenToWorld(mr.GetBoundingRect().Location, false);
                if (Pw.X < 0 || Pw.Y < 0)
                    continue;

                PointF s = Global.GetCurrentDocument().WorldMap.ScreenToWorldF(mr.StartP, false);
                PointF a = Global.GetCurrentDocument().WorldMap.ScreenToWorldF(mr.A, false);
                PointF b = Global.GetCurrentDocument().WorldMap.ScreenToWorldF(mr.B, false);
                PointF e = Global.GetCurrentDocument().WorldMap.ScreenToWorldF(mr.EndP, false);
                LineUtil.DrawBezier(g, s, a, b, e, mr.Selected);
            }
        }
        private void DrawAllControls(Bitmap staticImage)
        {
            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
            // 反向遍历,解决Move时旧控件压在新控件上
            for (int i = 0; i < modelElements.Count; i++)
            {
                ModelElement me = modelElements[modelElements.Count - i - 1];
                Control ct = me.InnerControl;
                Point Pw = Global.GetCurrentDocument().WorldMap.ScreenToWorld(ct.Location, false);
                if (Pw.X < 0 || Pw.Y < 0)
                    continue;
                ct.DrawToBitmap(staticImage, new Rectangle(Pw.X, Pw.Y, ct.Width, ct.Height));
                me.Hide();
            }
        }
        private void DrawSelectControls(Bitmap staticImage, List<Control> controls)
        {
            foreach (Control ct in controls)
            {                               
                Point Pw = Global.GetCurrentDocument().WorldMap.ScreenToWorld(ct.Location, false);
                if (Pw.X < 0 || Pw.Y < 0)
                    continue;
                ct.DrawToBitmap(staticImage, new Rectangle(Pw.X, Pw.Y, ct.Width, ct.Height));                
            }
        }
        private void SetImgTransparency(Bitmap img)
        {
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    Color c = img.GetPixel(j, i);
                    img.SetPixel(j, i, Color.FromArgb(200, c.R, c.G, c.B));
                }
            }
        }
        #endregion
        #region 阴影相关特效
        public void DrawRoundRect(Rectangle minBodingRec, Bitmap bitmap, int radius)
        {

            Graphics g = Graphics.FromImage(bitmap);
            int x = minBodingRec.X;
            int y = minBodingRec.Y;
            int width = minBodingRec.Width;
            int height = minBodingRec.Height;
            if (width == 0 || height == 0)
            {
                return;
            }
            g.SmoothingMode = SmoothingMode.HighQuality;//去掉锯齿
            g.CompositingQuality = CompositingQuality.HighQuality;//合成图像的质量
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;//去掉文字的锯齿


            //绘制虚线框
            g.DrawLine(p, new PointF(x + radius, y), new PointF(x + width - radius, y));
            g.DrawLine(p, new PointF(x + radius, y + height), new PointF(x + width - radius, y + height));
            g.DrawLine(p, new PointF(x, y + radius), new PointF(x, y + height - radius));
            g.DrawLine(p, new PointF(x + width, y + radius), new PointF(x + width, y + height - radius));

            //圆角绘制
            g.DrawArc(p, new Rectangle(x, y, radius * 2, radius * 2), 180, 90);
            g.DrawArc(p, new Rectangle(x + width - radius * 2, y, radius * 2, radius * 2), 270, 90);
            g.DrawArc(p, new Rectangle(x, y + height - radius * 2, radius * 2, radius * 2), 90, 90);
            g.DrawArc(p, new Rectangle(x + width - radius * 2, y + height - radius * 2, radius * 2, radius * 2), 0, 90);


            DrawShadow(g, x, y, width, height, radius, minBodingRec);
            g.Dispose();

        }
        private void DrawShadow(Graphics g, int x, int y, int width, int height, int radius,Rectangle minBodingRec)
        {
            Rectangle shadowDown = new Rectangle(minBodingRec.X + 2,
                                     minBodingRec.Y+ minBodingRec.Height,
                                     minBodingRec.Width, 3
                                     );

            Rectangle shadowRight = new Rectangle(minBodingRec.X + minBodingRec.Width,
                                                  minBodingRec.Y + 2,
                                                  3, minBodingRec.Height
                                                 );
            LinearGradientBrush brush = new LinearGradientBrush(shadowDown,
                                                    Color.DarkGray,
                                                    Color.LightGray,
                                                    LinearGradientMode.Horizontal
                                                    );
            LinearGradientBrush brush1 = new LinearGradientBrush(shadowRight,
                                                    Color.DarkGray,
                                                    Color.LightGray,
                                                    LinearGradientMode.Vertical
                                                    );

            Rectangle arcDown = new Rectangle(x, y + height + 2 - radius * 2, radius * 2, radius * 2);
            Rectangle arcRight = new Rectangle(x + width + 2 - radius * 2, y, radius * 2, radius * 2);
            FillShadow(brush, g, shadowDown, arcDown, 90);
            FillShadow(brush1, g, shadowRight, arcRight, 270);
        }
        private void FillShadow(LinearGradientBrush brush, Graphics g, Rectangle rec, Rectangle arc, float angle)
        {
            g.FillRectangle(brush, rec);
            g.DrawArc(p, arc, angle, 90);
            g.FillPie(brush, arc, angle, 90);
        }
        #endregion
    }
    class FrameWrapper
    {
        private static LogUtil log = LogUtil.GetInstance("CanvasPanel");
        private const bool endSelect = false;
        private const bool startSelect = true;
        
        private const int arcRadius = 2;
        private const double minBodingRec_Off = 0.4;
        private Bitmap staticImage, moveImage;
        private Point startP, endP;
        private bool selectStatus;
        private Pen p = new Pen(Color.Gray, 1f);
        private Rectangle initRec = new Rectangle(0, 0, 0, 0);
        private Rectangle frameRec, minBoundingBox;
        private readonly int worldWidth;
        private readonly int worldHeight;
        private Point mapOrigin;
        private float screenFactor;
        private List<Control> controls = new List<Control>();
        private List<int> minBoundingBuffMinX ;
        private List<int> minBoundingBuffMinY ;
        private List<int> minBoundingBuffMaxX ;
        private List<int> minBoundingBuffMaxY ;
        private Point moveOffset;
        public Rectangle MinBoundingBox { get => minBoundingBox; set => minBoundingBox = value; }
        FrameWrapperVFX frameWrapperVFX = new FrameWrapperVFX();
        public FrameWrapper()
        {
            p.DashStyle = DashStyle.Dash;
            worldWidth = 2000;
            worldHeight = 1000;
            InitFrame();
        }
        #region 属性初始化
        public void InitFrame()
        {
            frameRec = initRec;
            minBoundingBox = initRec;
            selectStatus = endSelect;
            staticImage = null;
            moveImage = null;
            controls = new List<Control>();
            minBoundingBuffMinX = new List<int>();
            minBoundingBuffMinY = new List<int>();
            minBoundingBuffMaxX = new List<int>();
            minBoundingBuffMaxY = new List<int>();
            moveOffset = new Point(0, 0);
    }
        private void FramePropertySet()
        {
            mapOrigin = Global.GetCurrentDocument().WorldMap.MapOrigin;
            screenFactor = Global.GetCurrentDocument().WorldMap.ScreenFactor;
        }
        #endregion
        #region 框选后画布鼠标基本操作事件
        public void FrameWrapper_MouseDown(MouseEventArgs e)
        {
            FramePropertySet();
            startP = Global.GetCurrentDocument().WorldMap.ScreenToWorld(e.Location, false);
            if (e.Button == MouseButtons.Right)
            {
                return;
            }
            if (minBoundingBox.IsEmpty || !minBoundingBox.Contains(startP))
            {
                SelectFrame_MouseDown();
                return;
            }
            DragFrame_MouseDown();
        }

        public void FrameWrapper_MouseMove(MouseEventArgs e)
        {
            endP = Global.GetCurrentDocument().WorldMap.ScreenToWorld(e.Location,false);
            FrameWrapper_MouseEnter(endP);
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            if (selectStatus.Equals(startSelect))
            {
                SelectFrame_MouseMove();
                return;
            }
            DragFrame_MouseMove();
            
        }
        public void FrameWrapper_MouseUp(MouseEventArgs e)
        {
            endP = Global.GetCurrentDocument().WorldMap.ScreenToWorld(e.Location,false);
            if (e.Button != MouseButtons.Left)
                return;
            if (selectStatus.Equals(startSelect))
            {
                SelectFrame_MouseUp();
                return;
            }
            DragFrame_MouseUp();
        }
        public void FrameWrapper_MouseEnter(Point pw)
        {           
            if (minBoundingBox.Contains(pw))
            {
                Global.GetCanvasPanel().Cursor = Cursors.SizeAll;
                return;
            }   
            Global.GetCanvasPanel().Cursor = Cursors.Default;
        }
        public void FrameDel(object sender, EventArgs e)
        {
            foreach (Control ct in controls)
                (ct as IMoveControl).DeleteMenuItem_Click(sender, e);
            Global.GetCurrentDocument().Show();
            Global.GetCurrentDocument().UpdateAllLines();
            Global.GetNaviViewControl().UpdateNaviView();
            minBoundingBox = new Rectangle(0, 0, 0, 0);
            staticImage = frameWrapperVFX.CreateWorldImage(worldWidth, worldHeight, controls, false);

        }
        public bool FramePaint(PaintEventArgs e)
        {
            if (Global.GetFlowControl().SelectFrame & staticImage != null)
            {
                Bitmap i = new Bitmap(staticImage);
                e.Graphics.DrawImageUnscaled(i, Convert.ToInt32(mapOrigin.X * screenFactor), Convert.ToInt32(mapOrigin.Y * screenFactor));
                i.Dispose();
                i = null;
                return true;
            }
            return false;
        }
        public void FramePaste()
        {
            if (controls.Count != 1)
                return;
            foreach(Control ct in controls)
            {
                //框选后粘贴 暂无实现
            }
        }
        #endregion
        #region 控件框选实现
        private void SelectFrame_MouseDown()
        {
            InitFrame();
            selectStatus = startSelect;
            staticImage = frameWrapperVFX.CreateWorldImage(worldWidth, worldHeight, controls, false);
        }
        private void SelectFrame_MouseMove()
        {
            CreateRect();
            Bitmap i = new Bitmap(staticImage);
            Graphics g = Graphics.FromImage(i);
            g.DrawRectangle(p, frameRec);
            Graphics n = Global.GetCanvasPanel().CreateGraphics();
            n.DrawImageUnscaled(i, Convert.ToInt32(mapOrigin.X * screenFactor), Convert.ToInt32(mapOrigin.Y * screenFactor));
            n.Dispose();
            g.Dispose();
            i.Dispose();
            i = null;
        }
        private void SelectFrame_MouseUp()
        {
            CreateRect();
            FindControl();
            frameWrapperVFX.DrawRoundRect(minBoundingBox, staticImage, arcRadius);
            Graphics n = Global.GetCanvasPanel().CreateGraphics();
            n.DrawImageUnscaled(staticImage, Convert.ToInt32(mapOrigin.X * screenFactor), Convert.ToInt32(mapOrigin.Y * screenFactor));
            n.Dispose();
            moveImage = frameWrapperVFX.UpdateMoveImg(minBoundingBox, worldWidth, worldHeight, controls);
            selectStatus = endSelect;
        }
        #endregion
        #region 框选控件移动
        private void DragFrame_MouseDown()
        {
            selectStatus = endSelect;
        }
        private void DragFrame_MouseMove()
        {
            if (this.moveImage == null)
                return;
            int dx = endP.X - startP.X;
            int dy = endP.Y - startP.Y;
            MoveImage_Display(dx, dy);
        }
        private void DragFrame_MouseUp()
        {
            foreach (Control ct in controls)
            {
                ct.Left = ct.Left + endP.X - startP.X + moveOffset.X;
                ct.Top = ct.Top + endP.Y - startP.Y + moveOffset.Y;
            }
            Global.GetCurrentDocument().Show();
            Global.GetCurrentDocument().UpdateAllLines();
            Global.GetNaviViewControl().UpdateNaviView();
            Global.GetMainForm().SetDocumentDirty();
            staticImage = frameWrapperVFX.CreateWorldImage(worldWidth, worldHeight, controls, false);
            minBoundingBox.X = minBoundingBox.X + endP.X - startP.X + moveOffset.X;
            minBoundingBox.Y = minBoundingBox.Y + endP.Y - startP.Y + moveOffset.Y;
            
            frameWrapperVFX.DrawRoundRect(minBoundingBox, staticImage, arcRadius);
        }
        #endregion
        #region 最小外包矩形计算
        private void CreateRect()
        {
            if (endP.X < startP.X && endP.Y < startP.Y)
                frameRec = new Rectangle(endP.X, endP.Y, System.Math.Abs(endP.X - startP.X), System.Math.Abs(endP.Y - startP.Y));
            else if (endP.X > startP.X && endP.Y < startP.Y)
                frameRec = new Rectangle(startP.X, endP.Y, System.Math.Abs(endP.X - startP.X), System.Math.Abs(endP.Y - startP.Y));
            else if (endP.X < startP.X && endP.Y > startP.Y)
                frameRec = new Rectangle(endP.X, startP.Y, System.Math.Abs(endP.X - startP.X), System.Math.Abs(endP.Y - startP.Y));
            else
                frameRec = new Rectangle(startP.X, startP.Y, System.Math.Abs(endP.X - startP.X), System.Math.Abs(endP.Y - startP.Y));
        }
        private void FindControl()
        {
            foreach(ModelElement me in Global.GetCurrentDocument().ModelElements)
            {
                Control ct = me.InnerControl;
                UpDateMinBodingBuff(ct);
            }               
            if (minBoundingBuffMinX.Count == 0)
            {
                minBoundingBox = initRec;
                return;
            }
            minBoundingBox = new Rectangle(minBoundingBuffMinX.Min(),
                                         minBoundingBuffMinY.Min(),
                                         minBoundingBuffMaxX.Max() - minBoundingBuffMinX.Min(),
                                         minBoundingBuffMaxY.Max() - minBoundingBuffMinY.Min());            
        }

        private void UpDateMinBodingBuff(Control ct)
        {

            Point ctW = Global.GetCurrentDocument().WorldMap.ScreenToWorld(ct.Location, false);
            if (!frameRec.Contains(ctW) || !frameRec.Contains(new Point(ctW.X + ct.Width, ctW.Y + ct.Height)))
            {
                return;
            }            
            minBoundingBuffMinX.Add(ctW.X - (int)(ct.Height * minBodingRec_Off));
            minBoundingBuffMinY.Add(ctW.Y - (int)(ct.Height * minBodingRec_Off));
            minBoundingBuffMaxX.Add(ctW.X + ct.Width + (int)(ct.Height * minBodingRec_Off));
            minBoundingBuffMaxY.Add(ctW.Y + ct.Height + (int)(ct.Height * minBodingRec_Off));
            controls.Add(ct);
            
        }
        #endregion
        private void MoveImage_Display(int dx, int dy)
        {
            if (staticImage == null || minBoundingBox.IsEmpty)
            {
                return;
            }
            Graphics n = Global.GetCanvasPanel().CreateGraphics();
            Bitmap i = new Bitmap(staticImage);
            Graphics g = Graphics.FromImage(i);
            moveOffset = WorldBoundControl(new Point(minBoundingBox.X + dx, minBoundingBox.Y + dy));
            g.DrawImage(moveImage, minBoundingBox.X + dx + moveOffset.X, minBoundingBox.Y + dy + moveOffset.Y);
            n.DrawImageUnscaled(i,
                                Convert.ToInt32(mapOrigin.X * screenFactor),
                                Convert.ToInt32(mapOrigin.Y * screenFactor));
            n.Dispose();
            g.Dispose();
            i.Dispose();
            i = null;
        }
        public Point WorldBoundControl(Point Pm)
        {
            Point Pw = Global.GetCurrentDocument().WorldMap.ScreenToWorld(Pm, true);
            Point off = new Point(0, 0);
            if (Pw.X < 20)
            {
                off.X = 20 - Pm.X;
            }
            if (Pw.Y < 70)
            {
                off.Y =  70 - Pm.Y;
            }
            if (Pw.X > 2000 - minBoundingBox.Width)
            {
                off.X = Global.GetCanvasPanel().Width - minBoundingBox.Width;
            }
            if (Pw.Y > 980 - minBoundingBox.Height)
            {
                off.Y = Global.GetCanvasPanel().Height - minBoundingBox.Height;
            }
            return off;
        }
    }
}
