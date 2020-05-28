using Citta_T1.Core;
using Citta_T1.Utils;
using log4net.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Business.Model
{
    class ModelRelation
    {
        private PointF startP;
        private PointF endP;
        private PointF a;  // 贝塞尔曲线的控制点A
        private PointF b;  // 贝塞尔曲线的控制点B

        private bool selected = false;

        public ElementType Type { get; }
        public int EndPin { get; set; }
        public int StartID { get; set; }
        public int EndID { get; set; }

        public PointF EndP { get => endP; set => endP = value; }
        public PointF A { get => a;}
        public PointF B { get => b; }
        public bool Selected { get => selected; set => selected = value; }
        public PointF StartP { get => startP; set => startP = value; }

        public static ModelRelation Empty = new ModelRelation();
        private ModelRelation() { }

        public ModelRelation(int startID, int endID, PointF startLocation, PointF endLocation, int endPin)
        {
          
            this.StartID = startID;
            this.EndID = endID;
            this.StartP = startLocation;
            this.EndP = endLocation;
            this.EndPin = endPin;
            this.Type = ElementType.Relation;
            UpdatePoints();
        }

        public void ChangeLoc(float x, float y)
        {
            this.StartP = new PointF(this.StartP.X + x, this.StartP.Y + y);
            this.endP = new PointF(this.endP.X + x, this.endP.Y + y);
            UpdatePoints();
        }
        // MoveOpControl.ChangeSize
        public void ZoomOut(float zoomFactor = Global.Factor)
        {
            this.StartP = new PointF(this.StartP.X / zoomFactor, this.StartP.Y / zoomFactor);
            this.endP = new PointF(this.endP.X / zoomFactor, this.endP.Y / zoomFactor);
            UpdatePoints();
        }

        public void ZoomIn(float zoomFactor = Global.Factor)
        {
            this.StartP = new PointF(this.StartP.X * zoomFactor, this.StartP.Y * zoomFactor);
            this.endP = new PointF(this.endP.X * zoomFactor, this.endP.Y * zoomFactor);
            UpdatePoints();
        }
        public void UpdatePoints()
        {
            this.a = new PointF((StartP.X + endP.X) / 2, StartP.Y);
            this.b = new PointF((StartP.X + endP.X) / 2, endP.Y);
        }

        public void OnMouseMoveEndP(PointF p)
        {
            endP = p;
            UpdatePoints();
        }

        public void OnMouseMoveStartP(PointF p)
        {
            StartP = p;
            UpdatePoints();
        }

        public RectangleF GetBoundingRectF()
        {
            float x = Math.Min(StartP.X, endP.X);
            float y = Math.Min(StartP.Y, endP.Y);
            float w = Math.Abs(StartP.X - endP.X);
            float h = Math.Abs(StartP.Y - endP.Y);
            return new RectangleF(x, y, w, h);
        }

        public Rectangle GetBoundingRect()
        {
            float x = Math.Min(StartP.X, endP.X);
            float y = Math.Min(StartP.Y, endP.Y);
            float w = Math.Abs(StartP.X - endP.X);
            float h = Math.Abs(StartP.Y - endP.Y);
            return new Rectangle(Convert.ToInt32(x), Convert.ToInt32(y), Convert.ToInt32(w), Convert.ToInt32(h));
        }
    }
}
