using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Business.Model
{
    class ModelRelation
    {
        private ElementType type;
        private int startID;
        private int endID;
        private PointF startP;
        private PointF endP;
        int endPin;


        private PointF a;
        private PointF b;

        public ElementType Type { get => type;}
        public int EndPin { get => this.endPin; set => this.endPin = value; }
        public int StartID { get => this.startID; set => this.startID = value; }
        public int EndID { get => this.endID; set => this.endID = value; }
        public PointF StartP { get => this.startP; set => this.startP = value; }
        public PointF EndP { get => endP; set => endP = value; }
        public PointF A { get => a;}
        public PointF B { get => b; }

        public ModelRelation(int startID, int endID, PointF startLocation, PointF endLocation, int endPin)
        {
          
            this.startID = startID;
            this.endID = endID;
            this.startP = startLocation;
            this.endP = endLocation;
            this.endPin = endPin;
            this.type = ElementType.Relation;
            UpdatePoints();
        }

        public void ChangLoc(float x, float y)
        {
            this.startP = new PointF(this.startP.X + x, this.startP.Y + y);
            this.endP = new PointF(this.endP.X + x, this.endP.Y + y);
            UpdatePoints();
        }
        // MoveOpControl.ChangeSize
        public void ZoomOut(float zoomFactor = 1.3F)
        {
            this.startP = new PointF(this.startP.X / zoomFactor, this.startP.Y / zoomFactor);
            this.endP = new PointF(this.endP.X / zoomFactor, this.endP.Y / zoomFactor);
            UpdatePoints();
        }

        public void ZoomIn(float zoomFactor = 1.3F)
        {
            this.startP = new PointF(this.startP.X * zoomFactor, this.startP.Y * zoomFactor);
            this.endP = new PointF(this.endP.X * zoomFactor, this.endP.Y * zoomFactor);
            UpdatePoints();
        }
        public void UpdatePoints()
        {
            this.a = new PointF((startP.X + endP.X) / 2, startP.Y);
            this.b = new PointF((startP.X + endP.X) / 2, endP.Y);
        }

        public void OnMouseMoveEndP(PointF p)
        {
            endP = p;
            UpdatePoints();
        }

        public void OnMouseMoveStartP(PointF p)
        {
            startP = p;
            UpdatePoints();
        }

        public RectangleF GetBoundingRectF()
        {
            float x = Math.Min(startP.X, endP.X);
            float y = Math.Min(startP.Y, endP.Y);
            float w = Math.Abs(startP.X - endP.X);
            float h = Math.Abs(startP.Y - endP.Y);
            return new RectangleF(x, y, w, h);
        }

        public Rectangle GetBoundingRect()
        {
            float x = Math.Min(startP.X, endP.X);
            float y = Math.Min(startP.Y, endP.Y);
            float w = Math.Abs(startP.X - endP.X);
            float h = Math.Abs(startP.Y - endP.Y);
            return new Rectangle(Convert.ToInt32(x), Convert.ToInt32(y), Convert.ToInt32(w), Convert.ToInt32(h));
        }

        //public Bezier GetLine()
        //{
        //    return new B
        //}
    }
}
