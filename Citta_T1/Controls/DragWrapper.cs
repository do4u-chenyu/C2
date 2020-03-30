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
    class DragWrapper
    {
        private int width;
        private int height;
        private float factor;
        private Point start, now;
        private bool startDrag;

        private int worldWidth;
        private int worldHeight;
        private Bitmap staticImage;

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public float Factor { get => factor; set => factor = value; }
        public bool StartDrag { get => startDrag; set => startDrag = value; }

        public DragWrapper()
        {
            worldWidth = 2000;
            worldHeight = 1000;
            startDrag = false;
        }
        public void InitDragWrapper(Size canvasSize, float canvasFactor)
        {
            width = canvasSize.Width;
            height = canvasSize.Height;
            factor = canvasFactor;
        }
        public void DragDown(Size canvasSize, float canvasFactor, MouseEventArgs e)
        {
            this.startDrag = true;
            this.start = e.Location;
            if (this.staticImage != null)
            {   // bitmap是重型资源,需要强制释放
                this.staticImage.Dispose();
                this.staticImage = null;
            }
               


            this.InitDragWrapper(canvasSize, canvasFactor);
            this.staticImage = this.CreateWorldImage();
        }
        public void DragMove(Size canvasSize, float canvasFactor, MouseEventArgs e)
        {
            this.now = e.Location;
            this.InitDragWrapper(canvasSize, canvasFactor);
            Graphics n = Global.GetCanvasPanel().CreateGraphics();

            this.MoveWorldImage(n);
            n.Dispose();
        }
        public void DragUp(Size canvasSize, float canvasFactor, MouseEventArgs e)
        {
            Graphics n = Global.GetCanvasPanel().CreateGraphics();
            this.now = e.Location;
            this.InitDragWrapper(canvasSize, canvasFactor);
            this.MoveWorldImage(n);
            this.ControlChange(start, now);
            n.Dispose();
            this.startDrag = false;
            this.start = e.Location;
        }
        public bool DragPaint(Size canvasSize, float canvasFactor, PaintEventArgs e)
        {
            if (this.startDrag && this.staticImage != null)
            {
                this.InitDragWrapper(canvasSize, canvasFactor);
                this.MoveWorldImage(e.Graphics);
                return true;
            }
            return false;
        }
        //生成当前模型控件快照
        public Bitmap CreateWorldImage()
        {
            Bitmap staticImage = new Bitmap(Convert.ToInt32(worldWidth * factor), Convert.ToInt32(worldHeight * factor));
            Graphics g = Graphics.FromImage(staticImage);
            g.Clear(Color.White);
            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
            
            Point mapOrigin = Global.GetCurrentDocument().MapOrigin;
            mapOrigin.X = Convert.ToInt32(mapOrigin.X * factor);
            mapOrigin.Y = Convert.ToInt32(mapOrigin.Y * factor);
            modelElements.Reverse();
            foreach (ModelElement me in modelElements)
            {
                if (me.Type != ElementType.DataSource & me.Type != ElementType.Operator & me.Type != ElementType.Result)
                    continue;
                Control ct = me.GetControl;
                Point Pw = Global.GetCurrentDocument().ScreenToWorld(ct.Location, mapOrigin);
                if (Pw.X < 0 || Pw.Y < 0) 
                    continue;
                ct.DrawToBitmap(staticImage, new Rectangle(Pw.X, Pw.Y, ct.Width, ct.Height));
                me.Hide();
            }

            g.Dispose();
            return staticImage;
        }

        public void MoveWorldImage(Graphics n)
        {
            Point mapOrigin = Global.GetCurrentDocument().MapOrigin;

            int dx = Convert.ToInt32((now.X - start.X) / factor);
            int dy = Convert.ToInt32((now.Y - start.Y) / factor);
            mapOrigin = new Point(mapOrigin.X + dx, mapOrigin.Y + dy);
            Point moveOffset = WorldBoundControl(mapOrigin);

            mapOrigin = Global.GetCurrentDocument().MapOrigin;
            mapOrigin.X = Convert.ToInt32(mapOrigin.X * factor) + now.X - start.X;
            mapOrigin.Y = Convert.ToInt32(mapOrigin.Y * factor) + now.Y - start.Y;

            Bitmap i = new Bitmap(this.staticImage);
            moveOffset.X = Convert.ToInt32(moveOffset.X * factor);
            moveOffset.Y = Convert.ToInt32(moveOffset.Y * factor);
            n.DrawImageUnscaled(i, mapOrigin.X - moveOffset.X, mapOrigin.Y - moveOffset.Y);
            
            i.Dispose();
            i = null;
        }

        private void ControlChange(Point start, Point now)
        {

            Point mapOrigin = Global.GetCurrentDocument().MapOrigin;
            int dx = Convert.ToInt32((now.X - start.X) / this.factor);
            int dy = Convert.ToInt32((now.Y - start.Y) / this.factor);
            mapOrigin = new Point(mapOrigin.X + dx, mapOrigin.Y + dy);
            Point moveOffset = WorldBoundControl(mapOrigin);

            ChangLoc(now.X - start.X - moveOffset.X * factor, now.Y - start.Y - moveOffset.Y * factor);

            Global.GetCurrentDocument().MapOrigin = new Point(mapOrigin.X - moveOffset.X, mapOrigin.Y - moveOffset.Y);
            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
            modelElements.Reverse();
            foreach (ModelElement me in modelElements)
            {
                me.Show();
            }

            Global.GetNaviViewControl().UpdateNaviView();
        }

        private void ChangLoc(float dx, float dy)
        {

            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
            foreach (ModelElement me in modelElements)
            {
                Control ct = me.GetControl;
                if (ct is IDragable)
                    (ct as IDragable).ChangeLoc(dx, dy);
            }
        }

        public Point WorldBoundControl(Point Pm)
        {

            Point dragOffset = new Point(0, 0);
            Point Pw = Global.GetCurrentDocument().ScreenToWorld(new Point(50, 30), Pm);
            if (Pw.X < 50)
            {
                dragOffset.X = 50 - Pw.X;
            }
            if (Pw.Y < 30)
            {
                dragOffset.Y = 30 - Pw.Y;
            }
            if (Pw.X > 2000 - Convert.ToInt32(this.width / factor))
            {
                dragOffset.X = 2000 - Convert.ToInt32(this.width / factor) - Pw.X;
            }
            if (Pw.Y > 1000 - Convert.ToInt32((this.height) / factor))
            {
                dragOffset.Y = 1000 - Convert.ToInt32((this.height) / factor) - Pw.Y;
            }
            return dragOffset;
        }

    }
}