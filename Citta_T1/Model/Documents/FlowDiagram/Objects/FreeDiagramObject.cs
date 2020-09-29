using System;
using System.Collections.Generic;
using System.Text;

namespace Citta_T1.Model.Documents.FlowDiagramObjects
{
    class FreeDiagramObject : ChartObject
    {
        public override string XmlElementName
        {
            get { return "object"; }
        }
    }
}
