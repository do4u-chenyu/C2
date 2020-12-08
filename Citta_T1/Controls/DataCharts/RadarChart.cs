using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace C2.Controls.DataCharts
{
    class RadarChart:BaseChart
    {
        private int series = 0;
        public System.Windows.Forms.DataVisualization.Charting.Chart GetChart { get => Chart1; }
        public RadarChart(List<List<string>> dataList, List<string> title) : base(dataList, title)
        {
            SetStyle(title[1]);
        }
        void SetStyle(string legendText)
        {
            Chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Auto;
            Chart1.Series[0].ChartType = SeriesChartType.Radar;

            Chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
            Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
            Chart1.ChartAreas[0].AxisX.IsInterlaced = false;
            Chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
            //刻度线
            Chart1.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;
            Chart1.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
            Legend legend4 = new Legend();
            legend4.Title = "";
            legend4.TitleBackColor = Color.Transparent;
            legend4.BackColor = Color.Transparent;
            legend4.TitleForeColor = Color.White;
            legend4.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            legend4.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            legend4.ForeColor = Color.White;
            Chart1.Legends[0] = legend4;
            Chart1.Legends[0].Position.Auto = true;
            //背景渐变
            Chart1.ChartAreas[0].BackGradientStyle = GradientStyle.None;
            //设置XY轴标题的名称所在位置位远  
            Chart1.Series[series].XValueType = ChartValueType.String;
            Chart1.Series[series].Label = "#VAL";
            Chart1.Series[series].LabelForeColor = Color.White;
            Chart1.Series[series].ToolTip = "#LEGENDTEXT:#VAL";
            Chart1.Series[series].ChartType = SeriesChartType.Radar;
            Chart1.Series[series]["RadarDrawingStyle"] = "Line";
            Chart1.Series[series].LegendText = legendText;
            Chart1.Series[series].IsValueShownAsLabel = true;
            Chart1.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
            Chart1.ChartAreas[0].AxisX.TitleAlignment = StringAlignment.Near;
            for (int i = 0; i < Chart1.Series[series].Points.Count; i++)
            {
                Chart1.Series[series].Points[i].MarkerStyle = MarkerStyle.Circle;//设置折点的风格                                                                      //    chart1.Series[2].Points[i].MarkerColor = Color.Green;//设置seires中折点的颜色  
            }
            Chart1.AntiAliasing = AntiAliasingStyles.All;
            //调色板 磨沙:SemiTransparent  
            Chart1.Palette = ChartColorPalette.BrightPastel;
            Chart1.Series[series].ChartType = SeriesChartType.Radar;
            Chart1.Width = 500;
            Chart1.Height = 350;
        }
    }
}
