using Citta_T1.Business.Model;
using Citta_T1.Controls.Interface;
using Citta_T1.Utils;
using NPOI.SS.Formula.Functions;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Controls
{
    class FrameWrapper
    {
        private Bitmap staticImage,moveImage;
        private CanvasPanel canvas;
        private Point startP, endP;
        private bool  mouseIsDown = false;
        private Pen p2 = new Pen(Color.Gray, 0.0001f);
        private Pen p1 = new Pen(Color.Gray, 0.0001f);
        private Pen p  = new Pen(Color.Gray, 1f);
        private Rectangle frameRec = new Rectangle(0,0,0,0);
        private Rectangle minBoding = new Rectangle(0,0,0,0);
        private LogUtil log = LogUtil.GetInstance("CanvasPanel");
        public FrameWrapper()
        {
            p1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
        }

        public void FrameDown(MouseEventArgs e)
        {
            startP = e.Location;
            CreateImg();
            if (frameRec.IsEmpty)
            {
                mouseIsDown = true;
            }
            else if(!frameRec.Contains(e.Location))
            {
                frameRec = new Rectangle(0,0,0,0);
                List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
                for (int i = 0; i < modelElements.Count; i++)
                {
                    ModelElement me = modelElements[modelElements.Count - i - 1];
                    Control ct = me.GetControl;
                    (ct as IMoveControl).ControlNoSelect();
                }

            }
            else if (frameRec.Contains(e.Location))
            {
                log.Info("选中....");
                mouseIsDown = false;
            }
        }


        public void FrameMove(MouseEventArgs e)
        {
            if (!mouseIsDown)
                return;
            DrawFrame_move(e);
        }
        public void FrameUp(MouseEventArgs e)
        {
            if (!mouseIsDown)
                return;
            DrawFrame_Up(e);

        }
        #region 绘制虚线框

        private void DrawFrame_move(MouseEventArgs e)
        {
            Bitmap i = new Bitmap(staticImage);
            Graphics g = Graphics.FromImage(i);
            Rectangle changeRec;
            if (e.X < startP.X && e.Y < startP.Y)
                changeRec = new Rectangle(e.X, e.Y, System.Math.Abs(e.X - startP.X), System.Math.Abs(e.Y - startP.Y));
            else if (e.X > startP.X && e.Y < startP.Y)
                changeRec = new Rectangle(startP.X, e.Y, System.Math.Abs(e.X - startP.X), System.Math.Abs(e.Y - startP.Y));
            else if (e.X < startP.X && e.Y > startP.Y)
                changeRec = new Rectangle(e.X, startP.Y, System.Math.Abs(e.X - startP.X), System.Math.Abs(e.Y - startP.Y));
            else
                changeRec = new Rectangle(startP.X, startP.Y, System.Math.Abs(e.X - startP.X), System.Math.Abs(e.Y - startP.Y));

            g.DrawRectangle(p1, changeRec);
            Graphics n = canvas.CreateGraphics();

            n.DrawImageUnscaled(i, 0, 0);
            g.Dispose();
            i.Dispose();
            i = null;
        }
        private void DrawFrame_Up(MouseEventArgs e)
        {
            endP = e.Location;
            CreateRect();
            FindControl();
            DrawRoundedRect(2);
            mouseIsDown = false;
        }
        private void CreateImg()
        {
            canvas = Global.GetCanvasPanel();
            mouseIsDown = true;
            if (staticImage != null)
            {
                staticImage.Dispose();
                staticImage = null;
            }
            staticImage = new Bitmap(canvas.Width, canvas.Height);
            canvas.DrawToBitmap(staticImage, new Rectangle(0, 0, canvas.Width, canvas.Height));         
        }



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
            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
            List<int> minX = new List<int>();
            List<int> minY = new List<int>();
            List<int> maxX = new List<int>();
            List<int> maxY = new List<int>();
            for (int i = 0; i < modelElements.Count; i++)
            {
                ModelElement me = modelElements[modelElements.Count - i - 1];
                Control ct = me.GetControl;
                if (frameRec.Contains(ct.Location))
                {
                    (ct as IMoveControl).ControlSelect();
                    minX.Add(ct.Left - (int)(ct.Height * 0.4));
                    minY.Add(ct.Top  - (int)(ct.Height * 0.4));
                    maxX.Add(ct.Left + ct.Width + (int)(ct.Height * 0.4));
                    maxY.Add(ct.Top  + (int)(ct.Height * 1.4));
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
            int x = minX.Min() - 1;
            int y = minY.Min() - 4;
            int width  = maxX.Max() - x + 1;
            int height = maxY.Max() - y + 4;
            minBoding = new Rectangle(x, y, width, height);
        }

        public void DrawRoundedRect(int radius)
        {

            Bitmap i = new Bitmap(this.staticImage);
            Graphics g = Graphics.FromImage(i);
            Graphics n = Global.GetCanvasPanel().CreateGraphics();
            Rectangle shadowDown = new Rectangle(this.minBoding.X + 2,
                                                 this.minBoding.Y + this.minBoding.Height,
                                                 this.minBoding.Width,3
                                                 ) ;

            Rectangle shadowRight = new Rectangle(this.minBoding.X + this.minBoding.Width,
                                                  this.minBoding.Y + 2,
                                                  3, this.minBoding.Height
                                                 );

            int x = this.minBoding.X;
            int y = this.minBoding.Y;
            int width = this.minBoding.Width;
            int height = this.minBoding.Height;
            if (width == 0 || height == 0)
            {
                
                n.DrawImageUnscaled(i, 0, 0);
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

            //算子边角上下左右
            g.DrawArc(p, new Rectangle(x, y, radius * 2, radius * 2), 180, 90);
            g.DrawArc(p, new Rectangle(x + width - radius * 2, y, radius * 2, radius * 2), 270, 90);
            g.DrawArc(p, new Rectangle(x, y + height - radius * 2, radius * 2, radius * 2), 90, 90);
            g.DrawArc(p, new Rectangle(x + width - radius * 2, y + height - radius * 2, radius * 2, radius * 2), 0, 90);


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
            Rectangle arcDown = new Rectangle(x , y + height + 2 - radius * 2, radius * 2, radius * 2);
            Rectangle arcRight = new Rectangle(x + width + 2 - radius * 2, y , radius * 2,radius* 2);
            
            g.DrawArc(p, arcDown, 90, 90);
            g.DrawArc(p, arcRight, 270, 90);

            g.FillPie(brush, arcDown, 90, 90);
            g.FillPie(brush, arcRight, 270, 90);


            n.DrawImageUnscaled(i, 0, 0);
            n.Dispose();
            g.Dispose();
            
        }
        #endregion


    }

}
