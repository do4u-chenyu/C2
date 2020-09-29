using System;
using Citta_T1.Model.Documents;

namespace Citta_T1.Controls.Charts
{
    class FlowDiagramView : ChartControl
    {
        public FlowDiagram Diagram { get; private set; }

        public override ChartPage ChartPage
        {
            get { return Diagram; }
        }

        public override bool CanCopy
        {
            get { return false; }
        }

        public override bool CanCut
        {
            get { return false; }
        }

        public override bool CanPaste
        {
            get { return false; }
        }

        public override bool CanDelete
        {
            get { return false; }
        }

        public override bool CanEdit
        {
            get { return false; }
        }

        public override bool CanPasteAsRemark
        {
            get { return false; }
        }
    }
}
