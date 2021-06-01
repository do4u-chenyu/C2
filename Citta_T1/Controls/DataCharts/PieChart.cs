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

            Legend legend2 = new Legend("#VALX")
            {
                Title = String.Empty,
                TitleBackColor = Color.Transparent,
                BackColor = Color.Transparent,
                TitleForeColor = Color.White,
                TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular),
                Font = new Font("微软雅黑", 8f, FontStyle.Regular),
                ForeColor = Color.White
            };
            Chart1.Legends[0] = legend2;
            Chart1.Legends[0].Position.Auto = true;

            Chart1.ChartAreas[0].BackGradientStyle = GradientStyle.None;
            Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
            //某些情况下,饼图位置不对导致部分内容会被遮盖住, 这里手动调整饼图位置
            //但是不解决根本问题
            //Chart1.ChartAreas[0].InnerPlotPosition = new ElementPosition(30, 0, 60, 80);  

            Chart1.Series[0].ChartType = SeriesChartType.Pie;
            Chart1.Series[0].Points[0].Color = Color.Gray;
            Chart1.Series[0].Palette = ChartColorPalette.BrightPastel;
            Chart1.Series[0].CustomProperties = "PieLabelStyle = Outside";
            Chart1.Series[0].LegendText = legend2.Name;
            Chart1.Series[0].IsValueShownAsLabel = true;
            //是否显示图例
            Chart1.Series[0].IsVisibleInLegend = true;
            Chart1.Series[0].ShadowOffset = 0;
        }
    }
}

