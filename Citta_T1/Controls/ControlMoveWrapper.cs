using Citta_T1.Business.Model;
using Citta_T1.Controls.Interface;
using Citta_T1.Core;
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
        private static LogUtil log = LogUtil.GetInstance("ControlMoveWrapper");

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


            Control ct = this.control;
            Point Pw = Global.GetCurrentDocument().WorldMap.ScreenToWorld(ct.Location,false);
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
            Point mapOrigin = currentDoc.WorldMap.MapOrigin;
            float factor = currentDoc.WorldMap.ScreenFactor;
            mapOrigin.X = Convert.ToInt32(mapOrigin.X * factor);
            mapOrigin.Y = Convert.ToInt32(mapOrigin.Y * factor);
            Graphics g = Graphics.FromImage(StaticImage);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            foreach (ModelRelation mr in currentDoc.ModelRelations)
            {
                PointF s = currentDoc.WorldMap.ScreenToWorldF(mr.StartP,false);
                PointF a = currentDoc.WorldMap.ScreenToWorldF(mr.A, false);
                PointF b = currentDoc.WorldMap.ScreenToWorldF(mr.B, false);
                PointF e = currentDoc.WorldMap.ScreenToWorldF(mr.EndP, false);
                LineUtil.DrawBezier(g, s, a, b, e, mr.Selected);
            }
            g.Dispose();
            n.DrawImageUnscaled(StaticImage, mapOrigin);
            this.StaticImage.Dispose();
            this.StaticImage = null;
            this.RepaintCtrs();
        }


        public override void DragUp(Size canvasSize, float canvasFactor, MouseEventArgs e)
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
                Control ctr = me.InnerControl;
                //if (ctr == this.control)
                //    continue;
                Rectangle ctrRect = new Rectangle(ctr.Location, new Size(ctr.Width, ctr.Height));
                cp.Invalidate(ctrRect);
                cp.Update();
            }
        }
    }
}
