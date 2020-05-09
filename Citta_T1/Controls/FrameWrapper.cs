using Citta_T1.Business.Model;
using Citta_T1.Controls.Interface;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Controls
{
    class FrameWrapper
    {
        private Bitmap staticImage;
        private CanvasPanel canvas;
        private Point startP, endP;
        private bool  mouseIsDown = false;
        private Pen p1 = new Pen(Color.Gray, 0.0001f);
        private Rectangle frameRec = new Rectangle();
        public FrameWrapper()
        {
            p1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
        }

        public void FrameDown(MouseEventArgs e)
        {
            startP = e.Location;
            mouseIsDown = true;
            CreateImg();
        }


        public void FrameMove(MouseEventArgs e)
        {
            if (!mouseIsDown)
                return;
            Bitmap i = new Bitmap(staticImage);
            Graphics g = Graphics.FromImage(i);
            if (e.X < startP.X && e.Y < startP.Y)
                g.DrawRectangle(p1, e.X, e.Y, System.Math.Abs(e.X - startP.X), System.Math.Abs(e.Y - startP.Y));
            else if (e.X > startP.X && e.Y < startP.Y)
                g.DrawRectangle(p1, startP.X, e.Y, System.Math.Abs(e.X - startP.X), System.Math.Abs(e.Y - startP.Y));
            else if (e.X < startP.X && e.Y > startP.Y)
                g.DrawRectangle(p1, e.X, startP.Y, System.Math.Abs(e.X - startP.X), System.Math.Abs(e.Y - startP.Y));
            else
                g.DrawRectangle(p1, startP.X, startP.Y, System.Math.Abs(e.X - startP.X), System.Math.Abs(e.Y - startP.Y));

            Graphics n = canvas.CreateGraphics();
            n.DrawImageUnscaled(i, 0, 0);   
            g.Dispose();
            i.Dispose();
            i = null;
        }
        public void FrameUp(MouseEventArgs e)
        {
            endP = e.Location;
            Bitmap i = new Bitmap(this.staticImage);
            Graphics n = Global.GetCanvasPanel().CreateGraphics();
            n.DrawImageUnscaled(i, 0, 0);
            n.Dispose();
            mouseIsDown = false;
            CreateRect();
            FindControl();
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
            for (int i = 0; i < modelElements.Count; i++)
            {
                ModelElement me = modelElements[modelElements.Count - i - 1];
                Control ct = me.GetControl;
                if (frameRec.Contains(ct.Location))
                    (ct as IMoveControl).ControlSelect();
            }
        }
    }
}
