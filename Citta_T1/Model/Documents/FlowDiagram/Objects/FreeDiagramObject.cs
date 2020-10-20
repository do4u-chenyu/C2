using System;
using System.Collections.Generic;
using System.Text;

namespace C2.Model.Documents.FlowDiagramObjects
{
    class FreeDiagramObject : ChartObject
    {
        public override string XmlElementName
        {
            get { return "object"; }
        }
    }
}
