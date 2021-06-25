using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace C2.Controls.DataCharts
{
    class RingChart : BaseChart
    {
        public System.Windows.Forms.DataVisualization.Charting.Chart GetChart { get => Chart1; }
        public RingChart(List<List<string>> dataList, List<string> title) : base(dataList, title)
        {
            SetStyle();
        }
        void SetStyle()
        {
            Chart1.Series[0].ChartType = SeriesChartType.Doughnut;
            Chart1.ChartAreas[0].BackGradientStyle = GradientStyle.None;
            //图例样式
            Legend legend2 = new Legend("#VALX");
            legend2.Title = "";
            legend2.TitleBackColor = Color.Transparent;
            legend2.BackColor = Color.Transparent;
            legend2.TitleForeColor = Color.White;
            legend2.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            legend2.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            legend2.ForeColor = Color.White;
            Chart1.Series[0].CustomProperties = "PieLabelStyle = Outside";
            Chart1.Legends[0] = legend2;
            Chart1.Series[0].LegendText = legend2.Name;
            Chart1.Legends[0].Position.Auto = true;
            Chart1.Series[0].IsVisibleInLegend = true;
            Chart1.Series[0].ShadowOffset = 0;
            Chart1.Series[0]["PieLabelStyle"] = "Outside";
            Chart1.Series[0]["PieLineColor"] = "White";
        }
    }

}
