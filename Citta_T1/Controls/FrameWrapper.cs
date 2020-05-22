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
        private Bitmap staticImage, staticImage2, moveImage;
        private CanvasPanel canvas;
        private Point startP, endP, nowP;
        private bool startSelect = false;
        private bool startDrag = false;
        private Pen p2 = new Pen(Color.Gray, 0.0001f);
        private Pen p1 = new Pen(Color.Gray, 0.0001f);
        private Pen p = new Pen(Color.Gray, 1f);
        private Rectangle frameRec = new Rectangle(0, 0, 0, 0);
        private Rectangle minBoding = new Rectangle(0, 0, 0, 0);
        private static LogUtil log = LogUtil.GetInstance("FrameWrapper");
        private int worldWidth, worldHeight;
        private Point mapOrigin;
        private List<Control> controls = new List<Control>();
        public Rectangle MinBoding { get => minBoding; set => minBoding = value; }

        public FrameWrapper()
        {
            p1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            worldWidth = 2000;
            worldHeight = 1000;
        }
        #region 对应画布鼠标事件
        public void FrameDown(MouseEventArgs e)
        {
            mapOrigin = Global.GetCurrentDocument().MapOrigin;
            startP = Global.GetCurrentDocument().ScreenToWorld(e.Location, mapOrigin);

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
            
            endP = Global.GetCurrentDocument().ScreenToWorld(e.Location, mapOrigin);
            FrameEnter(endP);
            if (e.Button != MouseButtons.Left)
                return;
            if (startSelect)
                DrawFrame_move();

            if (startDrag)
                dragFrame_Move();

        }
        public void FrameUp(MouseEventArgs e)
        {
            endP = Global.GetCurrentDocument().ScreenToWorld(e.Location, mapOrigin);
            if (e.Button == MouseButtons.Right)
                return;
            if (startDrag)
                dragFrame_Up(e);
            if (startSelect)
                DrawFrame_Up(e);
        }
        public void FrameEnter(Point pw)
        {
            if (minBoding.Contains(pw))
                Global.GetCanvasPanel().Cursor = Cursors.SizeAll;
            if (!minBoding.Contains(pw))
                Global.GetCanvasPanel().Cursor = Cursors.Default;

        }
        public void FrameDel(object sender, EventArgs e)
        {
            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements.ToList();
            foreach(ModelElement me in modelElements)
            {
                Control ct = me.GetControl;
                Point ctW = Global.GetCurrentDocument().ScreenToWorld(ct.Location, mapOrigin);
                if (minBoding.Contains(ctW))
                {
                    (ct as IMoveControl).DeleteMenuItem_Click(sender, e);
                }       
            }            
            Global.GetCurrentDocument().Show();            
            Global.GetCurrentDocument().UpdateAllLines();
            Global.GetNaviViewControl().UpdateNaviView();
            this.minBoding = new Rectangle(0,0,0,0);
            CreateWorldImage();
            
        }
        public bool FramePaint(PaintEventArgs e)
        {
            if (Global.GetFlowControl().SelectFrame & this.staticImage != null)
            {
                Bitmap i = new Bitmap(staticImage);
                e.Graphics.DrawImageUnscaled(i, mapOrigin.X, mapOrigin.Y);
                return true;
            }
            return false;
        }
        #endregion 
        #region 绘制虚线框

        private void DrawFrame_move()
        {
            Bitmap i = new Bitmap(staticImage);
            Graphics g = Graphics.FromImage(i);
            CreateRect();
            g.DrawRectangle(p1, frameRec);

            Graphics n = Global.GetCanvasPanel().CreateGraphics();
            n.DrawImageUnscaled(i, mapOrigin.X, mapOrigin.Y);
            g.Dispose();
            i.Dispose();
            i = null;
        }
        private void DrawFrame_Up(MouseEventArgs e)
        {
            CreateRect();
            FindControl();
            DrawRoundedRect(2);
            CreateMoveImg();
            startSelect = false;
        }

        //生成当前模型控件快照
        public void  CreateWorldImage()
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

            mapOrigin.X = Convert.ToInt32(mapOrigin.X * screenFactor);
            mapOrigin.Y = Convert.ToInt32(mapOrigin.Y * screenFactor);
            // 先画线，避免线盖住控件
            foreach (ModelRelation mr in modelRelations)
            {
                Point Pw = Global.GetCurrentDocument().ScreenToWorld(mr.GetBoundingRect().Location, mapOrigin);
                if (Pw.X < 0 || Pw.Y < 0)
                    continue;

                PointF s = Global.GetCurrentDocument().ScreenToWorldF(mr.StartP, mapOrigin);
                PointF a = Global.GetCurrentDocument().ScreenToWorldF(mr.A, mapOrigin);
                PointF b = Global.GetCurrentDocument().ScreenToWorldF(mr.B, mapOrigin);
                PointF e = Global.GetCurrentDocument().ScreenToWorldF(mr.EndP, mapOrigin);
                LineUtil.DrawBezier(g, s, a, b, e, mr.Selected);
            }
            // 反向遍历,解决Move时旧控件压在新控件上
            for (int i = 0; i < modelElements.Count; i++)
            {
                ModelElement me = modelElements[modelElements.Count - i - 1];
                Control ct = me.GetControl;
                Point Pw = Global.GetCurrentDocument().ScreenToWorld(ct.Location, mapOrigin);
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
                Point ctW = Global.GetCurrentDocument().ScreenToWorld(ct.Location, mapOrigin);
                if (frameRec.Contains(ctW))
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

        public void DrawRoundedRect(int radius)
        {
            
            Graphics g = Graphics.FromImage(this.staticImage);
            Graphics n = Global.GetCanvasPanel().CreateGraphics();
            Rectangle shadowDown = new Rectangle(this.minBoding.X + 2 ,
                                                 this.minBoding.Y + this.minBoding.Height ,
                                                 this.minBoding.Width, 3
                                                 );

            Rectangle shadowRight = new Rectangle(this.minBoding.X + this.minBoding.Width,
                                                  this.minBoding.Y + 2 ,
                                                  3, this.minBoding.Height
                                                 );

            int x = this.minBoding.X ;
            int y = this.minBoding.Y ;
            int width = this.minBoding.Width;
            int height = this.minBoding.Height;
            if (width == 0 || height == 0)
            {

                n.DrawImageUnscaled(this.staticImage, mapOrigin.X, mapOrigin.Y);
                n.Dispose();
                g.Dispose();
                return;
            }
            g.SmoothingMode = SmoothingMode.HighQuality;//去掉锯齿
            g.CompositingQuality = CompositingQuality.HighQuality;//合成图像的质量
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;//去掉文字的锯齿


            //边框上下左右
            g.DrawLine(p, new PointF(x + radius, y), new PointF(x + width - radius, y));
            g.DrawLine(p, new PointF(x + radius, y + height), new PointF(x + width - radius, y + height));
            g.DrawLine(p, new PointF(x, y + radius), new PointF(x, y + height - radius));
            g.DrawLine(p, new PointF(x + width, y + radius), new PointF(x + width, y + height - radius));

            //圆角
            g.DrawArc(p, new Rectangle(x, y, radius * 2, radius * 2), 180, 90);
            g.DrawArc(p, new Rectangle(x + width - radius * 2, y, radius * 2, radius * 2), 270, 90);
            g.DrawArc(p, new Rectangle(x, y + height - radius * 2, radius * 2, radius * 2), 90, 90);
            g.DrawArc(p, new Rectangle(x + width - radius * 2, y + height - radius * 2, radius * 2, radius * 2), 0, 90);

            // 阴影
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
            g.FillRectangle(brush, shadowDown);
            g.FillRectangle(brush1, shadowRight);
            Rectangle arcDown = new Rectangle(x, y + height + 2 - radius * 2, radius * 2, radius * 2);
            Rectangle arcRight = new Rectangle(x + width + 2 - radius * 2, y, radius * 2, radius * 2);

            g.DrawArc(p, arcDown, 90, 90);
            g.DrawArc(p, arcRight, 270, 90);

            g.FillPie(brush, arcDown, 90, 90);
            g.FillPie(brush1, arcRight, 270, 90);


            n.DrawImageUnscaled(this.staticImage, mapOrigin.X, mapOrigin.Y);
            n.Dispose();
            g.Dispose();
            
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
            Graphics g = Graphics.FromImage(moveImage);
            g.DrawImage(staticImage, 0, 0, realRect, GraphicsUnit.Pixel);
            for (int i = 0; i < moveImage.Height; i++)
            {
                for (int j = 0; j < moveImage.Width; j++)
                {
                    Color c = moveImage.GetPixel(j, i);
                    moveImage.SetPixel(j, i, Color.FromArgb(200, c.R, c.G, c.B));      
                }
            }
            g.Dispose();
        }
        private void dragFrame_Move()
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
            n.DrawImageUnscaled(i, mapOrigin.X, mapOrigin.Y);
            n.Dispose();
            g.Dispose();
            i = null;
        }

        private void dragFrame_Up(MouseEventArgs e)
        {

            foreach(Control ct in controls)
            {
                Point pw = Global.GetCurrentDocument().ScreenToWorld(ct.Location, mapOrigin);
                if (minBoding.Contains(pw))
                {
                    ct.Left = ct.Left + endP.X - startP.X;
                    ct.Top = ct.Top + endP.Y - startP.Y;
                }
            }
            Global.GetCurrentDocument().Show();
            Global.GetCurrentDocument().UpdateAllLines();
            Global.GetNaviViewControl().UpdateNaviView();
            Global.GetMainForm().SetDocumentDirty();
            CreateWorldImage();
            minBoding.X = minBoding.X + endP.X - startP.X;
            minBoding.Y = minBoding.Y + endP.Y - startP.Y;
            DrawRoundedRect(2);
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
            this.moveImage = null;           
        }
        
    }
}
