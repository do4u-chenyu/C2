using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using C2.Model.Styles;

namespace C2.Model.Documents
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
