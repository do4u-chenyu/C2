using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace C2.Controls.DataCharts
{
    class PieChart:BaseChart
    {
        public System.Windows.Forms.DataVisualization.Charting.Chart GetChart { get => Chart1; }

        public PieChart(List<List<string>> dataList, List<string> title) : base(dataList, title)
        {
            SetStyle();
        }
        void SetStyle()
        {
            Chart1.ChartAreas[0].BackGradientStyle = GradientStyle.None;
            Chart1.Series[0].ChartType = SeriesChartType.Pie;
            Chart1.Series[0].Points[0].Color = Color.Gray;
            Chart1.Series[0].Palette = ChartColorPalette.BrightPastel;
            Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
            Chart1.Series[0].CustomProperties = "PieLabelStyle = Outside";
            Legend legend2 = new Legend("#VALX");
            legend2.Title = "图例";
            legend2.TitleBackColor = Color.Transparent;
            legend2.BackColor = Color.Transparent;
            legend2.TitleForeColor = Color.White;
            legend2.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            legend2.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            legend2.ForeColor = Color.White;
            Chart1.Legends[0] = legend2;
            Chart1.Legends[0].Position.Auto = true;
            Chart1.Series[0].IsValueShownAsLabel = true;
            //是否显示图例
            Chart1.Series[0].IsVisibleInLegend = true;
            Chart1.Series[0].ShadowOffset = 0;
        }
    }
}

