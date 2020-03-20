using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Business.Model
{
    class ModelRelation
    {
        private ElementType type;
        string start;
        string end;
        string startLocation;
        string endLocation;
        string endPin;
        public ElementType Type { get => type;}
        public string EndPin { get => this.endPin; set => this.endPin = value; }
        public string Start { get => this.start; set => this.start = value; }
        public string End { get => this.end; set => this.end = value; }
        public string StartLocation { get => this.startLocation; set => this.startLocation = value; }
        public string EndLocation { get => endLocation; set => endLocation = value; }
        public ModelRelation(string startID, string endID, string startLocation, string endLocation, string endPin)
        {
          
            this.start = startID;
            this.end = endID;
            this.startLocation = startLocation;
            this.endLocation = endLocation;
            this.endPin = endPin;
            this.type = ElementType.Relation;

        }

    }
}
