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
        private int start;
        private int end;
        private Point startLocation;
        private Point endLocation;
        int endPin;
        public ElementType Type { get => type;}
        public int EndPin { get => this.endPin; set => this.endPin = value; }
        public int Start { get => this.start; set => this.start = value; }
        public int End { get => this.end; set => this.end = value; }
        public Point StartLocation { get => this.startLocation; set => this.startLocation = value; }
        public Point EndLocation { get => endLocation; set => endLocation = value; }
        public ModelRelation(int startID, int endID, Point startLocation, Point endLocation, int endPin)
        {
          
            this.start = startID;
            this.end = endID;
            this.startLocation = startLocation;
            this.endLocation = endLocation;
            this.endPin = endPin;
            this.type = ElementType.Relation;

        }
        public ModelRelation(Line line, int startID, int endID, int endPin)
        {
            // TODO [DK] Canvas存的是Control
            this.start = startID;
            this.end = endID;
            this.startLocation = new Point((int)line.StartP.X, (int)line.StartP.Y);
            this.endLocation = new Point((int)line.EndP.X, (int)line.EndP.Y);
            this.endPin = endPin;
            this.type = ElementType.Relation;
        }

    }
}
