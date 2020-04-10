using Citta_T1.Business.Model;
using Citta_T1.Controls;
using Citta_T1.Controls.Move;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Utils    
{
    public static class LineUtil
    {
        public static void ChangeLoc(float dx, float dy)
        {
            List<ModelRelation> modelRelations = Global.GetCurrentDocument().ModelRelations;
            foreach (ModelRelation mr in modelRelations)
            {
                mr.ChangLoc(dx, dy);
            }
        }

        public static Rectangle ConvertRect(RectangleF r)
        {
            return new Rectangle((int)r.Left, (int)r.Top, (int)r.Width, (int)r.Height);
        }


    }

    // 划线类
    public class Bezier
    {
        private PointF startP;
        private PointF endP;
        private PointF a;
        private PointF b;
 

        public PointF StartP { get => startP; set => startP = value; }
        public PointF EndP { get => endP; set => endP = value; }

        //Pen pen;
        public Bezier(PointF p1, PointF p2)
        {
            startP = p1;
            endP = p2;
            UpdatePoints();
        }

        public void Draw(CanvasWrapper canvas, RectangleF rect)
        {
            DrawBezier(canvas.Graphics);
        }

        public void DrawBezier(Graphics g, RectangleF rect)
        {
            DrawBezier(g);
        }

        public void DrawBezier(Graphics g)
        {
            g.DrawBezier(Pens.Green, this.startP, this.a, this.b, this.endP);
        }
        public RectangleF GetBoundingRect()
        {
            // TODO [DK] 没有考虑到坐标系放大系数
            return GetRect(startP, endP);
        }
        private  RectangleF GetRect(PointF p1, PointF p2, float width = 0)
        {
            float x = Math.Min(p1.X, p2.X);
            float y = Math.Min(p1.Y, p2.Y);
            float w = Math.Abs(p1.X - p2.X);
            float h = Math.Abs(p1.Y - p2.Y);
            RectangleF rect = new RectangleF(x, y, w, h); ;
            rect.Inflate(width, width);
            return rect;
        }
        public static RectangleF GetRect(float x, float y, float w, float h)
        {
            return new RectangleF(x, y, w, h);
        }
        public void OnMouseMove(PointF p)
        {
            endP = p;
        }

        public void UpdatePoints()
        {
            this.a = new PointF((startP.X + endP.X) / 2, startP.Y);
            this.b = new PointF((startP.X + endP.X) / 2, endP.Y);
        }
    }
}
