using Citta_T1.Business.Model;
using Citta_T1.Controls.Interface;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Citta_T1.Controls
{
    class DragWrapper
    {
        private int width;
        private int height;
        private Point start, now;
        private bool startDrag;

        private int worldWidth;
        private int worldHeight;
        private Bitmap staticImage;

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public float Factor { get; set; }
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
            Factor = canvasFactor;
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
            Bitmap staticImage = new Bitmap(Convert.ToInt32(worldWidth * Factor), Convert.ToInt32(worldHeight * Factor));
            Graphics g = Graphics.FromImage(staticImage);
            g.Clear(Color.White);
            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
            
            Point mapOrigin = Global.GetCurrentDocument().MapOrigin;
            mapOrigin.X = Convert.ToInt32(mapOrigin.X * Factor);
            mapOrigin.Y = Convert.ToInt32(mapOrigin.Y * Factor);
            for(int i = 0; i < modelElements.Count; i++)
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
            return staticImage;
        }

        public void MoveWorldImage(Graphics n)
        {
            Point mapOrigin = Global.GetCurrentDocument().MapOrigin;

            int dx = Convert.ToInt32((now.X - start.X) / Factor);
            int dy = Convert.ToInt32((now.Y - start.Y) / Factor);
            mapOrigin = new Point(mapOrigin.X + dx, mapOrigin.Y + dy);
            Point moveOffset = Utils.OpUtil.WorldBoundControl(mapOrigin, Factor, Width, Height);

            mapOrigin = Global.GetCurrentDocument().MapOrigin;
            mapOrigin.X = Convert.ToInt32(mapOrigin.X * Factor) + now.X - start.X;
            mapOrigin.Y = Convert.ToInt32(mapOrigin.Y * Factor) + now.Y - start.Y;

            
            moveOffset.X = Convert.ToInt32(moveOffset.X * Factor);
            moveOffset.Y = Convert.ToInt32(moveOffset.Y * Factor);
            n.DrawImageUnscaled(this.staticImage, mapOrigin.X - moveOffset.X, mapOrigin.Y - moveOffset.Y);
            
        }

        private void ControlChange(Point start, Point now)
        {

            Point mapOrigin = Global.GetCurrentDocument().MapOrigin;
            int dx = Convert.ToInt32((now.X - start.X) / this.Factor);
            int dy = Convert.ToInt32((now.Y - start.Y) / this.Factor);
            mapOrigin = new Point(mapOrigin.X + dx, mapOrigin.Y + dy);
            Point moveOffset = Utils.OpUtil.WorldBoundControl(mapOrigin, Factor, Width, Height);

            OpUtil.ChangLoc(now.X - start.X - moveOffset.X * Factor, now.Y - start.Y - moveOffset.Y * Factor);

            Global.GetCurrentDocument().MapOrigin = new Point(mapOrigin.X - moveOffset.X, mapOrigin.Y - moveOffset.Y);
           
            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
                     
            foreach (ModelElement me in modelElements)
            {
                me.Show();
            }

            Global.GetNaviViewControl().UpdateNaviView();
        }
    }
}