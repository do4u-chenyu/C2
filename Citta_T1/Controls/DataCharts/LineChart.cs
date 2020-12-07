using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace C2.Controls.DataCharts
{
    class LineChart:BaseChart
    {
        public System.Windows.Forms.DataVisualization.Charting.Chart GetChart { get => Chart1;}
        public LineChart(List<List<string>> dataList, List<string> title) : base(dataList, title)
        {
            Chart1.Series[0].ChartType = SeriesChartType.Spline;
        }
    }
}
