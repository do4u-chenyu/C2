using Citta_T1.Business.Model;
using Citta_T1.Controls.Interface;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Controls
{
    class ControlMoveWrapper : DragWrapper
    {
        private Control control;
        private LogUtil log = LogUtil.GetInstance("MoveDtContorl");

        public ControlMoveWrapper(Control ctr)
        {
            this.control = ctr;
        }
        public override Bitmap CreateWorldImage()
        {
            Bitmap staticImage = new Bitmap(Convert.ToInt32(this.WorldWidth * Factor), Convert.ToInt32(this.WorldHeight * Factor));
            Graphics g = Graphics.FromImage(staticImage);
            g.Clear(Color.White);
            List<ModelRelation> modelRelations = Global.GetCurrentDocument().ModelRelations;

            Point mapOrigin = Global.GetCurrentDocument().MapOrigin;
            mapOrigin.X = Convert.ToInt32(mapOrigin.X * Factor);
            mapOrigin.Y = Convert.ToInt32(mapOrigin.Y * Factor);
            Control ct = this.control;
            Point Pw = Global.GetCurrentDocument().ScreenToWorld(ct.Location, mapOrigin);
            g.Dispose();
            return staticImage;
        }

        public override void MoveWorldImage(Graphics n)
        {
            // 每次Move都需要画一张新图
            if (this.StaticImage != null)
            {
                this.StaticImage.Dispose();
                this.StaticImage = null;
            }
            this.StaticImage = this.CreateWorldImage();
            ModelDocument currentDoc = Global.GetCurrentDocument();
            Point mapOrigin = currentDoc.MapOrigin;

            int dx = Convert.ToInt32((Now.X - Start.X) / Factor);
            int dy = Convert.ToInt32((Now.Y - Start.Y) / Factor);
            mapOrigin = new Point(mapOrigin.X + dx, mapOrigin.Y + dy);
            Point moveOffset = Utils.OpUtil.WorldBoundControl(mapOrigin, Factor, Width, Height);

            mapOrigin = currentDoc.MapOrigin;
            mapOrigin.X = Convert.ToInt32(mapOrigin.X * Factor) + Now.X - Start.X;
            mapOrigin.Y = Convert.ToInt32(mapOrigin.Y * Factor) + Now.Y - Start.Y;


            moveOffset.X = Convert.ToInt32(moveOffset.X * Factor);
            moveOffset.Y = Convert.ToInt32(moveOffset.Y * Factor);

            Graphics g = Graphics.FromImage(StaticImage);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            foreach (ModelRelation mr in currentDoc.ModelRelations)
            {
                PointF s = currentDoc.ScreenToWorldF(mr.StartP, mapOrigin);
                PointF a = currentDoc.ScreenToWorldF(mr.A, mapOrigin);
                PointF b = currentDoc.ScreenToWorldF(mr.B, mapOrigin);
                PointF e = currentDoc.ScreenToWorldF(mr.EndP, mapOrigin);
                LineUtil.DrawBezier(g, s, a, b, e, mr.Selected);
            }
            g.Dispose();

            n.DrawImageUnscaled(StaticImage, mapOrigin.X, mapOrigin.Y);
            this.StaticImage.Dispose();
            this.StaticImage = null;
            this.RepaintCtrs();
        }


        public void DragUp(Size canvasSize, float canvasFactor, MouseEventArgs e)
        {
            Graphics n = Global.GetCanvasPanel().CreateGraphics();
            this.Now = e.Location;
            this.InitDragWrapper(canvasSize, canvasFactor);
            this.MoveWorldImage(n);
            Global.GetCanvasPanel().Invalidate();
            n.Dispose();
            this.StartDrag = false;
            this.Start = e.Location;
        }
        
        /// <summary>
        ///  重绘碰到的控件
        /// </summary>
        private void RepaintCtrs()
        {
            CanvasPanel cp = Global.GetCanvasPanel();
            List<ModelElement> md = Global.GetCurrentDocument().ModelElements;

            Rectangle thisRect = new Rectangle(this.control.Location, new Size(this.control.Width, this.control.Height));
            foreach (ModelElement me in md)
            {
                Control ctr = me.GetControl;
                //if (ctr == this.control)
                //    continue;
                Rectangle ctrRect = new Rectangle(ctr.Location, new Size(ctr.Width, ctr.Height));
                cp.Invalidate(ctrRect);
                cp.Update();
            }
        }
    }
}
