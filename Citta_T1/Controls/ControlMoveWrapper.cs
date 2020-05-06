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
            // TODO [DK] 这里是不是少了点什么东西
            if (Pw.X < 0 || Pw.Y < 0)
                return staticImage;
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
            Pen p1 = new Pen(Color.Green, 3);
            Pen p2 = new Pen(Color.Green, 1);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            foreach (ModelRelation mr in currentDoc.ModelRelations)
            {
                if (mr.Selected)
                    g.DrawBezier(p1,
                        currentDoc.ScreenToWorldF(mr.StartP, mapOrigin),
                        currentDoc.ScreenToWorldF(mr.A, mapOrigin),
                        currentDoc.ScreenToWorldF(mr.B, mapOrigin),
                        currentDoc.ScreenToWorldF(mr.EndP, mapOrigin)
                );
                else
                    g.DrawBezier(p2,
                        currentDoc.ScreenToWorldF(mr.StartP, mapOrigin),
                        currentDoc.ScreenToWorldF(mr.A, mapOrigin),
                        currentDoc.ScreenToWorldF(mr.B, mapOrigin),
                        currentDoc.ScreenToWorldF(mr.EndP, mapOrigin)
);
            }
            g.Dispose();
            p1.Dispose();
            p2.Dispose();
            //n.DrawImageUnscaled(StaticImage, 0, 0);

            n.DrawImageUnscaled(StaticImage, mapOrigin.X, mapOrigin.Y);
            this.StaticImage.Dispose();
            this.StaticImage = null;
        }


        public void DragUp(Size canvasSize, float canvasFactor, MouseEventArgs e)
        {
            Graphics n = Global.GetCanvasPanel().CreateGraphics();
            this.Now = e.Location;
            this.InitDragWrapper(canvasSize, canvasFactor);
            this.MoveWorldImage(n);
            n.Dispose();
            this.StartDrag = false;
            this.Start = e.Location;
        }
    }
}
