using Citta_T1.Business.Model;
using Citta_T1.Controls.Interface;
using Citta_T1.Core;
using Citta_T1.Utils;
using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.Formula.Functions;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Controls
{
    class FrameWrapper
    {
        private const bool noSelect = false;
        private Bitmap staticImage, staticImageFrame, moveImage;
        private Point startP, endP;
        private bool startSelect = false;
        private bool startDrag = false;
        private Pen p = new Pen(Color.Gray, 1f);
        private Rectangle frameRec = new Rectangle(0, 0, 0, 0);
        private Rectangle minBoding = new Rectangle(0, 0, 0, 0);
        private static LogUtil log = LogUtil.GetInstance("FrameWrapper");
        private int worldWidth, worldHeight;
        private Point mapOrigin;
        private float screenFactor;
        private List<Control> controls = new List<Control>();
        public Rectangle MinBoding { get => minBoding; set => minBoding = value; }

        public FrameWrapper()
        {
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            worldWidth = 2000;
            worldHeight = 1000;
        }
        #region 对应画布鼠标事件
        public void FrameDown(MouseEventArgs e)
        {
            mapOrigin = Global.GetCurrentDocument().WorldMap1.GetWmInfo().MapOrigin;
            screenFactor = Global.GetCurrentDocument().WorldMap1.GetWmInfo().ScreenFactor;
            startP = Global.GetCurrentDocument().WorldMap1.ScreenToWorld(e.Location,false);
            if (e.Button == MouseButtons.Right)
                return;
            else if (minBoding.IsEmpty)
            {
                startSelect = true;
                startDrag = false;
                CreateWorldImage();
            }
            else if (!minBoding.Contains(startP))
            {
                InitFrame();
                startSelect = true;
                startDrag = false;
                CreateWorldImage();
            }
            else if (minBoding.Contains(startP))
            {
                startSelect = false;
                startDrag = true;
            }
        }

        public void FrameMove(MouseEventArgs e)
        {
            endP = Global.GetCurrentDocument().WorldMap1.ScreenToWorld(e.Location,false);
            FrameEnter(endP);
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            if (startSelect)
            {
                DrawFrame_move();
            }

            if (startDrag)
            {
                DragFrame_Move();
            }
        }
        public void FrameUp(MouseEventArgs e)
        {
            endP = Global.GetCurrentDocument().WorldMap1.ScreenToWorld(e.Location,false);
            if (e.Button != MouseButtons.Left)
                return;
            if (startDrag)
            {
                DragFrame_Up();
            }

            if (startSelect)
            {
                DrawFrame_Up();
            }
        }
        public void FrameEnter(Point pw)
        {           
            if (minBoding.Contains(pw))
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
            minBoding = new Rectangle(0, 0, 0, 0);
            CreateWorldImage();
            
        }
        public bool FramePaint(PaintEventArgs e)
        {
            if (Global.GetFlowControl().SelectFrame & staticImage != null)
            {
                Bitmap i = new Bitmap(staticImage);
                e.Graphics.DrawImageUnscaled(i, Convert.ToInt32(mapOrigin.X * screenFactor), Convert.ToInt32(mapOrigin.X * screenFactor));
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
        #region 绘制虚线框

        private void DrawFrame_move()
        {
            CreateRect();

            Bitmap i = new Bitmap(staticImage);
            Graphics g = Graphics.FromImage(i);
            g.DrawRectangle(p, frameRec);
            Graphics n = Global.GetCanvasPanel().CreateGraphics();
            n.DrawImageUnscaled(i, Convert.ToInt32(mapOrigin.X * screenFactor), Convert.ToInt32(mapOrigin.X * screenFactor));

            n.Dispose();
            g.Dispose();
            i.Dispose();
            i = null;
        }
        private void DrawFrame_Up()
        {
            CreateRect();
            FindControl();
            DrawRoundedRect(staticImage, 2);
            Graphics n = Global.GetCanvasPanel().CreateGraphics();
            n.DrawImageUnscaled(staticImage, Convert.ToInt32(mapOrigin.X * screenFactor), Convert.ToInt32(mapOrigin.X * screenFactor));
            n.Dispose();
            CreateMoveImg();
            startSelect = noSelect;
        }

        //生成当前模型控件快照
        public void CreateWorldImage()
        {
            float screenFactor = Global.GetCanvasPanel().ScreenFactor;
            staticImage = new Bitmap(Convert.ToInt32(worldWidth * screenFactor), Convert.ToInt32(worldHeight * screenFactor));
            Graphics g = Graphics.FromImage(staticImage);

            g.SmoothingMode = SmoothingMode.HighQuality;//去掉锯齿
            g.CompositingQuality = CompositingQuality.HighQuality;//合成图像的质量
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;//去掉文字的锯齿

            g.Clear(Color.White);
            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
            List<ModelRelation> modelRelations = Global.GetCurrentDocument().ModelRelations;
          
            // 先画线，避免线盖住控件
            foreach (ModelRelation mr in modelRelations)
            {
                //Point Pw = Global.GetCurrentDocument().ScreenToWorld(mr.GetBoundingRect().Location, mapOrigin);
                Point Pw = Global.GetCurrentDocument().WorldMap1.ScreenToWorld(mr.GetBoundingRect().Location,false);
                if (Pw.X < 0 || Pw.Y < 0)
                    continue;

                PointF s = Global.GetCurrentDocument().WorldMap1.ScreenToWorldF(mr.StartP);
                PointF a = Global.GetCurrentDocument().WorldMap1.ScreenToWorldF(mr.A);
                PointF b = Global.GetCurrentDocument().WorldMap1.ScreenToWorldF(mr.B);
                PointF e = Global.GetCurrentDocument().WorldMap1.ScreenToWorldF(mr.EndP);
                LineUtil.DrawBezier(g, s, a, b, e, mr.Selected);
            }
            // 反向遍历,解决Move时旧控件压在新控件上
            for (int i = 0; i < modelElements.Count; i++)
            {
                ModelElement me = modelElements[modelElements.Count - i - 1];
                Control ct = me.GetControl;
                Point Pw = Global.GetCurrentDocument().WorldMap1.ScreenToWorld(ct.Location,false);
                if (Pw.X < 0 || Pw.Y < 0)
                    continue;
                ct.DrawToBitmap(staticImage, new Rectangle(Pw.X, Pw.Y, ct.Width, ct.Height));
                me.Hide();
            }
            g.Dispose();            
        }


        private void CreateRect()
        {
            if (endP.X < startP.X && endP.Y < startP.Y)
                frameRec = new Rectangle(endP.X, endP.Y , System.Math.Abs(endP.X - startP.X), System.Math.Abs(endP.Y - startP.Y));
            else if (endP.X > startP.X && endP.Y < startP.Y)
                frameRec = new Rectangle(startP.X, endP.Y, System.Math.Abs(endP.X - startP.X), System.Math.Abs(endP.Y - startP.Y));
            else if (endP.X < startP.X && endP.Y > startP.Y)
                frameRec = new Rectangle(endP.X, startP.Y, System.Math.Abs(endP.X - startP.X), System.Math.Abs(endP.Y - startP.Y));
            else
                frameRec = new Rectangle(startP.X, startP.Y, System.Math.Abs(endP.X - startP.X), System.Math.Abs(endP.Y - startP.Y));
        }
        private void FindControl()
        {
            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
            List<int> minX = new List<int>();
            List<int> minY = new List<int>();
            List<int> maxX = new List<int>();
            List<int> maxY = new List<int>();
            for (int i = 0; i < modelElements.Count; i++)
            {
                ModelElement me = modelElements[modelElements.Count - i - 1];
                Control ct = me.GetControl;
                Point ctW = Global.GetCurrentDocument().WorldMap1.ScreenToWorld(ct.Location,false);
                if (frameRec.Contains(ctW) && frameRec.Contains(new Point(ctW.X + ct.Width,ctW.Y + ct.Height)))
                {
                    minX.Add(ctW.X - (int)(ct.Height * 0.4));
                    minY.Add(ctW.Y - (int)(ct.Height * 0.4));
                    maxX.Add(ctW.X + ct.Width + (int)(ct.Height * 0.4));
                    maxY.Add(ctW.Y + (int)(ct.Height * 1.4));
                    controls.Add(ct);
                }
            }
            if (minX.Count == 0)
            {
                minBoding = new Rectangle(0, 0, 0, 0);
                return;
            }
            UpDateMinBoding(minX, minY, maxX, maxY);
        }
        private void UpDateMinBoding(List<int> minX, List<int> minY, List<int> maxX, List<int> maxY)
        {
            int x = minX.Min() ;
            int y = minY.Min() ;
            int width = maxX.Max() - x;
            int height = maxY.Max() - y;
            minBoding = new Rectangle(x, y, width, height);
        }

        #endregion
        #region 框选特效
        public void DrawRoundedRect(Bitmap bitmap, int radius)
        {

            Graphics g = Graphics.FromImage(bitmap);
            int x = this.minBoding.X;
            int y = this.minBoding.Y;
            int width = this.minBoding.Width;
            int height = this.minBoding.Height;
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

           
            DrawShadow(g, x, y, width, height, radius);
            g.Dispose();

        }
        private void DrawShadow(Graphics g, int x, int y, int width, int height, int radius)
        {
            Rectangle shadowDown = new Rectangle(this.minBoding.X + 2,
                                     this.minBoding.Y + this.minBoding.Height,
                                     this.minBoding.Width, 3
                                     );

            Rectangle shadowRight = new Rectangle(this.minBoding.X + this.minBoding.Width,
                                                  this.minBoding.Y + 2,
                                                  3, this.minBoding.Height
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
        private void FillShadow(LinearGradientBrush brush,Graphics g,Rectangle rec, Rectangle arc,float angle)
        {
            g.FillRectangle(brush, rec);
            g.DrawArc(p, arc, angle, 90);
            g.FillPie(brush, arc, angle, 90);
        }
        #endregion
        
        #region 实现框选控件批量移动
        private void CreateMoveImg()
        {
            if (minBoding.Width == 0 || minBoding.Y == 0)
                return;
            if (moveImage != null)
                moveImage = null;
            moveImage = new Bitmap(minBoding.Width + 2, minBoding.Height + 3);
            Rectangle realRect = new Rectangle(minBoding.Location,new Size(minBoding.Width + 2, minBoding.Height + 3));
            //创建作图区域
            float screenFactor = Global.GetCanvasPanel().ScreenFactor;
            staticImageFrame = new Bitmap(Convert.ToInt32(worldWidth * screenFactor), Convert.ToInt32(worldHeight * screenFactor));
            Graphics g = Graphics.FromImage(staticImageFrame);

            g.SmoothingMode = SmoothingMode.HighQuality;//去掉锯齿
            g.CompositingQuality = CompositingQuality.HighQuality;//合成图像的质量
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;//去掉文字的锯齿

            g.Clear(Color.White);
            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
            List<ModelRelation> modelRelations = Global.GetCurrentDocument().ModelRelations;

            mapOrigin.X = Convert.ToInt32(mapOrigin.X * screenFactor);
            mapOrigin.Y = Convert.ToInt32(mapOrigin.Y * screenFactor);
            // 先画线，避免线盖住控件
            foreach (ModelRelation mr in modelRelations)
            {
                Point Pw = Global.GetCurrentDocument().WorldMap1.ScreenToWorld(mr.GetBoundingRect().Location,false);
                if (Pw.X < 0 || Pw.Y < 0)
                    continue;

                PointF s = Global.GetCurrentDocument().WorldMap1.ScreenToWorldF(mr.StartP);
                PointF a = Global.GetCurrentDocument().WorldMap1.ScreenToWorldF(mr.A);
                PointF b = Global.GetCurrentDocument().WorldMap1.ScreenToWorldF(mr.B);
                PointF e = Global.GetCurrentDocument().WorldMap1.ScreenToWorldF(mr.EndP);
                
                LineUtil.DrawBezier(g, s, a, b, e, mr.Selected);
            }
            // 反向遍历,解决Move时旧控件压在新控件上
            for (int i = 0; i < modelElements.Count; i++)
            {
                ModelElement me = modelElements[modelElements.Count - i - 1];
                Control ct = me.GetControl;
                Point Pw = Global.GetCurrentDocument().WorldMap1.ScreenToWorld(ct.Location,false);
                if (Pw.X < 0 || Pw.Y < 0 || !minBoding.Contains(Pw) || !minBoding.Contains(new Point(Pw.X + ct.Width,Pw.Y + ct.Height)))
                    continue;
                ct.DrawToBitmap(staticImageFrame, new Rectangle(Pw.X, Pw.Y, ct.Width, ct.Height));
                me.Hide();
            }
            g.Dispose();


            DrawRoundedRect(staticImageFrame, 2);
            Graphics gM = Graphics.FromImage(moveImage);
            gM.DrawImage(staticImageFrame, 0, 0, realRect, GraphicsUnit.Pixel);
            for (int i = 0; i < moveImage.Height; i++)
            {
                for (int j = 0; j < moveImage.Width; j++)
                {
                    Color c = moveImage.GetPixel(j, i);
                    moveImage.SetPixel(j, i, Color.FromArgb(200, c.R, c.G, c.B));      
                }
            }
            g.Dispose();
            gM.Dispose();
            staticImageFrame = null;
        }
        private void DragFrame_Move()
        {
            if (this.moveImage == null )
                return;
            int dx = endP.X - startP.X;
            int dy = endP.Y - startP.Y;
            MoveImage_Display(dx, dy);
        }
        private void MoveImage_Display(int dx,int dy)
        {
            if (staticImage == null || minBoding.IsEmpty)
            {
                return;
            }
            Graphics n = Global.GetCanvasPanel().CreateGraphics();
            Bitmap i = new Bitmap(staticImage);
            Graphics g = Graphics.FromImage(i);
            g.DrawImage(this.moveImage, minBoding.X + dx, minBoding.Y + dy);
            n.DrawImageUnscaled(i, Convert.ToInt32(mapOrigin.X * screenFactor), Convert.ToInt32(mapOrigin.X * screenFactor));
            n.Dispose();
            g.Dispose();
            i = null;
        }

        private void DragFrame_Up()
        {
            foreach(Control ct in controls)
            {
                ct.Left = ct.Left + endP.X - startP.X;
                ct.Top = ct.Top + endP.Y - startP.Y;
            }
            Global.GetCurrentDocument().Show();
            Global.GetCurrentDocument().UpdateAllLines();
            Global.GetNaviViewControl().UpdateNaviView();
            Global.GetMainForm().SetDocumentDirty();
            CreateWorldImage();
            minBoding.X = minBoding.X + endP.X - startP.X;
            minBoding.Y = minBoding.Y + endP.Y - startP.Y;
            DrawRoundedRect(this.staticImage, 2);
            startDrag = false;
        }
        #endregion

        public void InitFrame()
        {
            frameRec = new Rectangle(0, 0, 0, 0);
            minBoding = new Rectangle(0, 0, 0, 0);
            startSelect = true;
            startDrag = false;
            this.staticImage = null;
            this.staticImageFrame = null;
            this.moveImage = null;
            controls = new List<Control>();
        }
        
    }
}
