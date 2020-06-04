using Citta_T1.Core;
using System;
using System.Drawing;

namespace Citta_T1.Business.Model
{
    class ModelRelation
    {
        public ElementType Type { get { return ElementType.Relation; } }
        public int EndPin { get; set; }
        public int StartID { get; set; }
        public int EndID { get; set; }

        public PointF EndP { get; set; }
        public PointF A { get; set; }    // 贝塞尔曲线的控制点A
        public PointF B { get; set; }    // 贝塞尔曲线的控制点B
        public PointF StartP { get; set; }
        public bool Selected { get; set; }

        public static ModelRelation Empty = new ModelRelation();
        // 默认不能自己构造,仅给Empty使用
        private ModelRelation() 
        {
            EndPin = StartID = EndID = -1;
            EndP = StartP = A = B = new PointF(0, 0);
            Selected = false;
        }

        public ModelRelation(int startID, int endID, PointF startLocation, PointF endLocation, int endPin)
        {      
            this.StartID = startID;
            this.EndID = endID;
            this.StartP = startLocation;
            this.EndP = endLocation;
            this.EndPin = endPin;
            this.Selected = false;
            UpdatePoints();
        }

        public void ChangeLoc(float x, float y)
        {
            this.StartP = new PointF(this.StartP.X + x, this.StartP.Y + y);
            this.EndP = new PointF(this.EndP.X + x, this.EndP.Y + y);
            UpdatePoints();
        }

        public void ZoomOut(float zoomFactor = Global.Factor)
        {
            this.StartP = new PointF(this.StartP.X / zoomFactor, this.StartP.Y / zoomFactor);
            this.EndP = new PointF(this.EndP.X / zoomFactor, this.EndP.Y / zoomFactor);
            UpdatePoints();
        }

        public void ZoomIn(float zoomFactor = Global.Factor)
        {
            this.StartP = new PointF(this.StartP.X * zoomFactor, this.StartP.Y * zoomFactor);
            this.EndP = new PointF(this.EndP.X * zoomFactor, this.EndP.Y * zoomFactor);
            UpdatePoints();
        }
        public void UpdatePoints()
        {
            this.A = new PointF((StartP.X + EndP.X) / 2, StartP.Y);
            this.B = new PointF((StartP.X + EndP.X) / 2, EndP.Y);
        }

        public Rectangle GetBoundingRect()
        {
            float x = Math.Min(StartP.X, EndP.X);
            float y = Math.Min(StartP.Y, EndP.Y);
            float w = Math.Abs(StartP.X - EndP.X);
            float h = Math.Abs(StartP.Y - EndP.Y);
            return new Rectangle(Convert.ToInt32(x), Convert.ToInt32(y), Convert.ToInt32(w), Convert.ToInt32(h));
        }
    }
}
