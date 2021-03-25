using C2.Business.Model;
using C2.Business.Model.World;
using C2.Controls.Move;
using C2.Core;
using C2.Core.UndoRedo;
using C2.Core.UndoRedo.Command;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

namespace C2.Controls
{

    class FrameWrapperVFX
    {
        // 拖拽过程中的高级视觉特效类
        private readonly bool backImgMode = false;
        private Pen p = new Pen(Color.Gray, 1f);
        public FrameWrapperVFX()
        {
            p.DashStyle = DashStyle.Dash;
        }

        #region 静态图生成相关特效
        public Bitmap CreateWorldImage(int worldWidth, int worldHeight, List<Control> controls, bool mode)
        {
            float screenFactor = Global.GetCurrentModelDocument().WorldMap.ScreenFactor;
            
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
                DrawSelectedControls(staticImage, controls);
            }
            g.Dispose();
            return staticImage;
        }
        public Bitmap UpdateMoveImg(Rectangle minBodingRec, int worldWidth, int worldHeight, List<Control> controls)
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
            List<ModelRelation> modelRelations = Global.GetCurrentModelDocument().ModelRelations;

            // 先画线，避免线盖住控件
            foreach (ModelRelation mr in modelRelations)
            {

                Point Pw = Global.GetCurrentModelDocument().WorldMap.ScreenToWorld(mr.GetBoundingRect().Location, false);
                if (Pw.X < 0 || Pw.Y < 0)
                    continue;

                PointF s = Global.GetCurrentModelDocument().WorldMap.ScreenToWorldF(mr.StartP, false);
                PointF a = Global.GetCurrentModelDocument().WorldMap.ScreenToWorldF(mr.A, false);
                PointF b = Global.GetCurrentModelDocument().WorldMap.ScreenToWorldF(mr.B, false);
                PointF e = Global.GetCurrentModelDocument().WorldMap.ScreenToWorldF(mr.EndP, false);
                LineUtil.DrawBezier(g, s, a, b, e, mr.Selected);
            }
        }
        private void DrawAllControls(Bitmap staticImage)
        {
            List<ModelElement> modelElements = Global.GetCurrentModelDocument().ModelElements;
            // 反向遍历,解决Move时旧控件压在新控件上
            for (int i = 0; i < modelElements.Count; i++)
            {
                ModelElement me = modelElements[modelElements.Count - i - 1];
                Control ct = me.InnerControl;
                Point Pw = Global.GetCurrentModelDocument().WorldMap.ScreenToWorld(ct.Location, false);
                if (Pw.X < 0 || Pw.Y < 0)
                    continue;
                ct.DrawToBitmap(staticImage, new Rectangle(Pw.X, Pw.Y, ct.Width, ct.Height));
                me.Hide();
            }
        }
        private void DrawSelectedControls(Bitmap staticImage, List<Control> controls)
        {
            foreach (Control ct in controls)
            {
                Point Pw = Global.GetCurrentModelDocument().WorldMap.ScreenToWorld(ct.Location, false);
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
        public void DrawRoundRect(Rectangle minBoundingRect, Bitmap bitmap, int radius)
        {

            Graphics g = Graphics.FromImage(bitmap);
            int x = minBoundingRect.X;
            int y = minBoundingRect.Y;
            int width = minBoundingRect.Width;
            int height = minBoundingRect.Height;
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


            DrawShadow(g, x, y, width, height, radius, minBoundingRect);
            g.Dispose();

        }
        private void DrawShadow(Graphics g, int x, int y, int width, int height, int radius, Rectangle minBodingRec)
        {
            Rectangle shadowDown = new Rectangle(minBodingRec.X + 2,
                                     minBodingRec.Y + minBodingRec.Height,
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
        private CanvasPanel canvasPanel;
        private const bool endSelect = false;
        private const bool startSelect = true;
        private const bool dragStatus = true;
        private const bool noDragStatus = false;
        private const int arcRadius = 2;
        private const double minBoundingRectOffset = 0.4;
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
        private List<int> minBoundingBuffMinX;
        private List<int> minBoundingBuffMinY;
        private List<int> minBoundingBuffMaxX;
        private List<int> minBoundingBuffMaxY;
        private Point moveOffset;
        public Rectangle MinBoundingBox { get => minBoundingBox; set => minBoundingBox = value; }
        public List<Control> Controls { get => controls; set => controls = value; }

        FrameWrapperVFX frameWrapperVFX = new FrameWrapperVFX();
        public FrameWrapper(CanvasPanel canvas)
        {
            canvasPanel = canvas;
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
            mapOrigin = Global.GetCurrentModelDocument().WorldMap.MapOrigin;
            screenFactor = Global.GetCurrentModelDocument().WorldMap.ScreenFactor;
        }
        #endregion
        #region 框选后画布鼠标基本操作事件
        public void FrameWrapper_MouseDown(MouseEventArgs e)
        {
            FramePropertySet();
          
            startP = Global.GetCurrentModelDocument().WorldMap.ScreenToWorld(e.Location, false);
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
            endP = Global.GetCurrentModelDocument().WorldMap.ScreenToWorld(e.Location, false);
            
            if (e.Button != MouseButtons.Left)
            {
                FrameWrapper_MouseEnter(endP,noDragStatus);
            }
            if (!Global.GetCanvasPanel().LeftButtonDown)
                return;
            if (selectStatus.Equals(startSelect))
            {
                SelectFrame_MouseMove();
                return;
            }
            DragFrame_MouseMove();

        }
        public void FrameWrapper_MouseUp(MouseEventArgs e)
        {
            endP = Global.GetCurrentModelDocument().WorldMap.ScreenToWorld(e.Location, false);
            if (e.Button != MouseButtons.Left)
                return;
            if (selectStatus.Equals(startSelect))
            {
                SelectFrame_MouseUp();
                
                return;
            }
            DragFrame_MouseUp();
        }
        public void FrameWrapper_MouseEnter(Point pw,bool status)
        {
            if (minBoundingBox.Contains(pw) || status)
            {
                Global.GetCanvasPanel().Cursor = Cursors.SizeAll;
                return;
            }
            Global.GetCanvasPanel().Cursor = Cursors.Default;
        }
        public void FrameDel(object sender, EventArgs e)
        {
            //foreach (Control ct in controls)
            //(ct as IMoveControl).DeleteMenuItem_Click(sender, e); // TODO [DK] 批量删除
            Tuple<List<ModelElement>, List<Tuple<int, int, int>>> mesAndMrs = Global.GetCanvasPanel().DeleteSelectedElesByCtrID(this.controls.Select(t => (t as MoveBaseControl).ID));
            Global.GetCurrentModelDocument().Show();
            Global.GetCurrentModelDocument().UpdateAllLines();
            Global.GetNaviViewControl().UpdateNaviView();
            minBoundingBox = new Rectangle(0, 0, 0, 0);
            if (!staticImage.Equals(null))
            {
                staticImage.Dispose();
                staticImage = null;
            }
            staticImage = frameWrapperVFX.CreateWorldImage(worldWidth, worldHeight, controls, false);

            if (mesAndMrs != null && mesAndMrs.Item1.Count != 0)
                //if (mesAndMrs != null && mesAndMrs.Item1.Count != 0 && mesAndMrs.Item2.Count != 0)
            {
                BaseCommand cmd = new BatchDeleteCommand(mesAndMrs.Item1, mesAndMrs.Item2);
                UndoRedoManager.GetInstance().PushCommand(Global.GetCurrentModelDocument(), cmd);
            }
        }
        public bool FramePaint(PaintEventArgs e)
        {
            if (selectStatus.Equals(startSelect) & staticImage != null)
            {
                Bitmap i = new Bitmap(staticImage);
                Graphics g = Graphics.FromImage(i);
                g.DrawRectangle(p, frameRec);
                e.Graphics.DrawImageUnscaled(i, Convert.ToInt32(mapOrigin.X * screenFactor), Convert.ToInt32(mapOrigin.Y * screenFactor));
                g.Dispose();
                i.Dispose();
                i = null;
                return true;
            }
            if (Global.GetTopToolBarControl().SelectFrame & staticImage != null)
            {
                Bitmap i = new Bitmap(staticImage);
                e.Graphics.DrawImageUnscaled(i, Convert.ToInt32(mapOrigin.X * screenFactor), Convert.ToInt32(mapOrigin.Y * screenFactor));
                i.Dispose();
                i = null;
                return true;
            }
            return false;
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
            Global.GetCanvasPanel().Invalidate();
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
            FrameWrapper_MouseEnter(endP, dragStatus);
            if (this.moveImage == null)
                return;
            int dx = endP.X - startP.X;
            int dy = endP.Y - startP.Y;
            MoveImage_Display(dx, dy);
        }
        private void DragFrame_MouseUp()
        {
            WorldMap wm = Global.GetCurrentModelDocument().WorldMap;
            Dictionary<int, Point> idPtsDict = new Dictionary<int, Point>();
            //DragFrame_MouseMove();
            foreach (Control ct in controls)
            {
                idPtsDict.Add((ct as MoveBaseControl).ID, wm.ScreenToWorld(ct.Location, true));
                ct.Left = ct.Left + endP.X - startP.X + moveOffset.X;
                ct.Top = ct.Top + endP.Y - startP.Y + moveOffset.Y;
            }

            BaseCommand cmd = new BatchMoveCommand(idPtsDict);
            UndoRedoManager.GetInstance().PushCommand(Global.GetCurrentModelDocument(), cmd);


            Global.GetCurrentModelDocument().Show();
            Global.GetCurrentModelDocument().UpdateAllLines();
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
            foreach (ModelElement me in Global.GetCurrentModelDocument().ModelElements)
            {
                Control ct = me.InnerControl;
                UpdateMinBoundingBuff(ct);
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

        private void UpdateMinBoundingBuff(Control ct)
        {

            Point ctW = Global.GetCurrentModelDocument().WorldMap.ScreenToWorld(ct.Location, false);
            if (!frameRec.Contains(ctW) || !frameRec.Contains(new Point(ctW.X + ct.Width, ctW.Y + ct.Height)))
            {
                return;
            }
            minBoundingBuffMinX.Add(ctW.X - (int)(ct.Height * minBoundingRectOffset));
            minBoundingBuffMinY.Add(ctW.Y - (int)(ct.Height * minBoundingRectOffset));
            minBoundingBuffMaxX.Add(ctW.X + ct.Width + (int)(ct.Height * minBoundingRectOffset));
            minBoundingBuffMaxY.Add(ctW.Y + ct.Height + (int)(ct.Height * minBoundingRectOffset));
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
            moveOffset = Global.GetCurrentModelDocument().WorldMap
                               .WorldBoundControl(new Point(minBoundingBox.X + dx, minBoundingBox.Y + dy),
                                                  minBoundingBox);
            g.DrawImage(moveImage, minBoundingBox.X + dx + moveOffset.X, minBoundingBox.Y + dy + moveOffset.Y);
            n.DrawImageUnscaled(i,
                                Convert.ToInt32(mapOrigin.X * screenFactor),
                                Convert.ToInt32(mapOrigin.Y * screenFactor));
            n.Dispose();
            g.Dispose();
            i.Dispose();
            i = null;
        }
    }
}
