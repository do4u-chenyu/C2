using C2.Controls.MapViews;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Model.Widgets
{
    class ChartWidget: Widget
    {
        public const string TypeID = "CHART";
        public override bool ResponseMouse
        {
            get
            {
                return true;
            }
        }
        public override Size CalculateSize(MindMapLayoutArgs e)
        {
            return new Size(20, 20);
        }
        public override string GetTypeID()
        {
            return TypeID;
        }
    }
}
