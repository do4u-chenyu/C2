using System.Collections.Generic;

namespace C2.Model.Widgets
{
    class ChartWidget : C2BaseWidget
    {
        public const string TypeID = "CHART";
        public ChartWidget()
        {
            DisplayIndex = 3;
            DataItems = new List<DataItem>();
            widgetIcon = Properties.Resources.chart_w_icon; 
        }
        public override string GetTypeID()
        {
            return TypeID;
        }
    }
}
