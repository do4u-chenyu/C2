using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Citta_T1.Model.Styles;

namespace Citta_T1.Model.Documents
{
    class FlowDiagram : ChartPage
    {
        public FlowDiagram()
        {
        }

        [Browsable(false)]
        public override ChartType Type
        {
            get { return ChartType.FlowDiagram; }
        }

        public override void ApplyTheme(ChartTheme chartTheme)
        {
        }
    }
}
