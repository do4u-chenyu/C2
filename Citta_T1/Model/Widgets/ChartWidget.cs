namespace C2.Model.Widgets
{
    class ChartWidget : C2BaseWidget
    {
        public const string TypeID = "CHART";
        public ChartWidget()
        {
            DisplayIndex = 3;
            widgetIcon = Properties.Resources.chart_w_icon; 
        }
        public override string GetTypeID()
        {
            return TypeID;
        }
    }
}
