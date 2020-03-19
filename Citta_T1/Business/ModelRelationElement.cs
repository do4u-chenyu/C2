using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Business
{
    class ModelRelation
    {
        private ElementType type;
        string startControl;
        string endControl;
        string startLocation;
        string endLocation;
        string endPinLabel;
        public ElementType Type { get => type;}
        public string EndPinLabel { get => this.endPinLabel; set => this.endPinLabel = value; }
        public string StartControl { get => this.startControl; set => this.startControl = value; }
        public string EndControl { get => this.endControl; set => this.endControl = value; }
        public string StartLocation { get => this.startLocation; set => this.startLocation = value; }
        public string EndLocation { get => endLocation; set => endLocation = value; }
        public ModelRelation( string startControl, string endControl, string startLocation, string endLocation, string endPinLabel)
        {
          
            this.startControl = startControl;
            this.endControl = endControl;
            this.startLocation = startLocation;
            this.endLocation = endLocation;
            this.endPinLabel = endPinLabel;
            this.type = ElementType.Relation;

        }

    }
}
