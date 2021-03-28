using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace C2.Controls.DataCharts
{
    class BarChart : BaseChart
    {

        public System.Windows.Forms.DataVisualization.Charting.Chart GetChart { get => Chart1; }


        public BarChart(List<List<string>> dataList, List<string> title) : base(dataList, title)
        {
            setStyle();
        }
        void setStyle()
        {
            Chart1.Series[0].Points[0].Color = Color.White;
            Chart1.Series[0].Palette = ChartColorPalette.Bright;
        }
    }
}
