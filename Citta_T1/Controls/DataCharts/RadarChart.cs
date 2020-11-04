using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace C2.Controls.DataCharts
{
    public partial class RadarChart : UserControl
    {
        private int series = 0;
        private string title;
        public System.Windows.Forms.DataVisualization.Charting.Chart GetChart { get => this.chart1; }
        public RadarChart(List<List<string>> dataList, List<string> title)
        {
            this.title = title[0];
            InitializeComponent();
            InitChart();
            DataBind(dataList[0], dataList[1], title[1]);
        }

        void InitChart()
        {
            chart1.Titles.Add(this.title);
            chart1.Titles[0].ForeColor = Color.White;
            chart1.Titles[0].Font = new Font("微软雅黑", 12f, FontStyle.Regular);
            chart1.Titles[0].Alignment = ContentAlignment.TopCenter;

            //控件背景
            chart1.BackColor = Color.Transparent;
            chart1.ChartAreas[0].BackColor = Color.Transparent;
            chart1.ChartAreas[0].BorderColor = Color.Transparent;
            //X轴标签间距
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;
            chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chart1.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 14f, FontStyle.Regular);
            chart1.ChartAreas[0].AxisX.TitleForeColor = Color.White;

            //X坐标轴颜色
            chart1.ChartAreas[0].AxisX.LineColor = ColorTranslator.FromHtml("#38587a"); ;
            chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
            chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);
            //X坐标轴标题
            //chart1.ChartAreas[0].AxisX.Title = "数量(宗)";
            //chart1.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            //chart1.ChartAreas[0].AxisX.TitleForeColor = Color.White;
            //chart1.ChartAreas[0].AxisX.TextOrientation = TextOrientation.Auto;
            //chart1.ChartAreas[0].AxisX.ToolTip = "数量(宗)";
            //X轴网络线条
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

            //Y坐标轴颜色
            chart1.ChartAreas[0].AxisY.LineColor = ColorTranslator.FromHtml("#38587a");
            chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;
            chart1.ChartAreas[0].AxisY.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);
            //Y坐标轴标题
            //chart1.ChartAreas[0].AxisY.Title = "数量(宗)";
            //chart1.ChartAreas[0].AxisY.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            //chart1.ChartAreas[0].AxisY.TitleForeColor = Color.White;
            //chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Auto;
            //chart1.ChartAreas[0].AxisY.ToolTip = "数量(宗)";
            //Y轴网格线条
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

            chart1.ChartAreas[0].AxisY2.LineColor = Color.Transparent;
            chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
            chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
            chart1.ChartAreas[0].AxisX.IsInterlaced = false;
            chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
            //刻度线
            chart1.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;
            //chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            //chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            //chart1.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
            chart1.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
            //背景渐变
            chart1.ChartAreas[0].BackGradientStyle = GradientStyle.None;
            //chart1.ChartAreas[0].AxisX2.InterlacedColor = Color.Red;
            //chart1.ChartAreas[0].AxisY2.InterlacedColor = Color.Red;
            //chart1.ChartAreas[0].BorderWidth = 0;
            //chart1.ChartAreas[0].BackSecondaryColor = Color.Red;
            //chart1.ChartAreas[0].BackImageTransparentColor = Color.Red;
            //chart1.ChartAreas[0].AxisX.InterlacedColor = Color.Red;
            //chart1.ChartAreas[0].AxisX.LineColor = Color.Red;
            //chart1.ChartAreas[0].AxisX2.LineColor = Color.Red;
            //chart1.ChartAreas[0].AxisX2.MajorGrid.LineColor = Color.Red;
            //chart1.ChartAreas[0].AxisX2.MajorTickMark.LineColor = Color.Red;
            //chart1.ChartAreas[0].AxisX2.MinorTickMark.LineColor = Color.Red;
            //chart1.ChartAreas[0].AxisY.InterlacedColor = Color.Red;
            //chart1.ChartAreas[0].AxisY.LineColor = Color.Red;
            //chart1.ChartAreas[0].AxisY2.InterlacedColor = Color.Red;
            //chart1.ChartAreas[0].AxisY2.LineColor = Color.Red;
            //chart1.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Red;
            //chart1.ChartAreas[0].AxisY2.MajorTickMark.LineColor = Color.Red;
            //chart1.ChartAreas[0].AxisY2.MinorTickMark.LineColor = Color.Red;


            //图例样式
            Legend legend4 = new Legend();
            legend4.Title = "图例";
            legend4.TitleBackColor = Color.Transparent;
            legend4.BackColor = Color.Transparent;
            legend4.TitleForeColor = Color.White;
            legend4.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            legend4.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            legend4.ForeColor = Color.White;
            chart1.Legends.Add(legend4);
            chart1.Legends[0].Position.Auto = true;

            //Series1
            chart1.Series[0].XValueType = ChartValueType.String;
            chart1.Series[0].Label = "#VAL";
            chart1.Series[0].LabelForeColor = Color.White;
            chart1.Series[0].ToolTip = "#LEGENDTEXT:#VAL(宗)";
            chart1.Series[0].ChartType = SeriesChartType.Radar;
            chart1.Series[0]["RadarDrawingStyle"] = "Line";
            chart1.Series[0].LegendText = "2015年";
            chart1.Series[0].IsValueShownAsLabel = true;

            ////Series2
            //chart1.Series.Add(new Series("Series2"));
            //chart1.Series[1].Label = "#VAL";
            //chart1.Series[1].LabelForeColor = Color.White;
            //chart1.Series[1].ToolTip = "#LEGENDTEXT:#VAL(宗)";
            //chart1.Series[1].ChartType = SeriesChartType.Radar;
            //chart1.Series[1]["RadarDrawingStyle"] = "Line";
            //chart1.Series[1].LegendText = "2016年";
            //chart1.Series[1].IsValueShownAsLabel = true;

            ////Series3
            //chart1.Series.Add(new Series("Series3"));
            //chart1.Series[2].Label = "#VAL";
            //chart1.Series[2].LabelForeColor = Color.White;
            //chart1.Series[2].ToolTip = "#LEGENDTEXT:#VAL(宗)";
            //chart1.Series[2].ChartType = SeriesChartType.Radar;
            //chart1.Series[2]["RadarDrawingStyle"] = "Line";
            //chart1.Series[2].LegendText = "2017年";
            //chart1.Series[2].IsValueShownAsLabel = true;


            //double[] yValues = { 65.62, 75.54, 60.45, 34.73, 85.42, 55.9, 63.6, 55.2, 77.1 };
            //string[] xValues = { "France", "Canada", "Germany", "USA", "Italy", "Spain", "Russia", "Sweden", "Japan" };


            ////Seris2  
            //double[] y2 = { 45.62, 65.54, 70.45, 84.73, 35.42, 55.9, 63.6 };
            //double[] y3 = { 88.62, 35.54, 52.45, 45.73, 88.42, 14.9, 33.6 };
            //this.chart1.Series[0].Points.DataBindXY(xValues, yValues);
            //this.chart1.Series[1].Points.DataBindY(y2);
            //this.chart1.Series[2].Points.DataBindY(y3);


            //设置X轴显示间隔为1,X轴数据比较多的时候比较有用  
            //chart1.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
            ////设置XY轴标题的名称所在位置位远  
            //chart1.ChartAreas[0].AxisX.TitleAlignment = StringAlignment.Near;

            //for (int i = 0; i < chart1.Series[2].Points.Count; i++)
            //{
            //    chart1.Series[2].Points[i].MarkerStyle = MarkerStyle.Circle;//设置折点的风格     
            //    chart1.Series[2].Points[i].MarkerColor = Color.Red;//设置seires中折点的颜色   
            //                                                       //    chart1.Series[1].Points[i].MarkerStyle = MarkerStyle.Square;//设置折点的风格     
            //                                                       //    chart1.Series[1].Points[i].MarkerColor = Color.Blue;//设置seires中折点的颜色  
            //                                                       //    chart1.Series[2].Points[i].MarkerStyle = MarkerStyle.Square;//设置折点的风格     
            //                                                       //    chart1.Series[2].Points[i].MarkerColor = Color.Green;//设置seires中折点的颜色  
            //}
            //for (int i = 0; i < chart1.Series.Count; i++)
            //{
            //    for (int j = 0; j < chart1.Series[i].Points.Count; j++)
            //    {
            //        chart1.Series[i].Points[j].Label = " ";
            //        //chart1.Series[i].Points[j].LabelToolTip = "string.Empty";
            //    }
            //}
            //chart1.ImageType = ChartImageType.Jpeg;
            //反锯齿  
            //chart1.AntiAliasing = AntiAliasingStyles.All;
            ////调色板 磨沙:SemiTransparent  
            //chart1.Palette = ChartColorPalette.BrightPastel;

            //chart1.Series[0].ChartType = SeriesChartType.Radar;
            ////chart1.Series[1].ChartType = SeriesChartType.Radar;
            ////chart1.Series[2].ChartType = SeriesChartType.Radar;
            //chart1.Width = 500;
            //chart1.Height = 350;
        }
        void DataBind(List<string> x, List<string> y,string legendText)
        {
            try
            {
                if (!series.Equals(0))
                    chart1.Series.Add(new Series(string.Format("Series{0}", series)));
                chart1.Series[series].XValueType = ChartValueType.String;
                chart1.Series[series].Label = "#VAL";
                chart1.Series[series].LabelForeColor = Color.White;
                chart1.Series[series].ToolTip = "#LEGENDTEXT:#VAL";
                chart1.Series[series].ChartType = SeriesChartType.Radar;
                chart1.Series[series]["RadarDrawingStyle"] = "Line";
                chart1.Series[series].LegendText = legendText;
                chart1.Series[series].IsValueShownAsLabel = true;
                List<double> y_double = y.ConvertAll(d => Convert.ToDouble(d));
                this.chart1.Series[series].Points.DataBindXY(x, y_double);
                chart1.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
                //设置XY轴标题的名称所在位置位远  
                chart1.ChartAreas[0].AxisX.TitleAlignment = StringAlignment.Near;
                for (int i = 0; i < chart1.Series[series].Points.Count; i++)
                {
                    chart1.Series[series].Points[i].MarkerStyle = MarkerStyle.Circle;//设置折点的风格                                                                      //    chart1.Series[2].Points[i].MarkerColor = Color.Green;//设置seires中折点的颜色  
                }
                chart1.AntiAliasing = AntiAliasingStyles.All;
                //调色板 磨沙:SemiTransparent  
                chart1.Palette = ChartColorPalette.BrightPastel;
                chart1.Series[series].ChartType = SeriesChartType.Radar;
                chart1.Width = 500;
                chart1.Height = 350;
            }
            catch
            {
                return;
            }

        }
    }
}
