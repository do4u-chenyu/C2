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
    public partial class RingChart : UserControl
    {
        public System.Windows.Forms.DataVisualization.Charting.Chart GetChart { get => this.chart1; }
        public bool EmptyInput { get; set; }
        public RingChart(List<List<string>> dataList, List<string> title)
        {
            EmptyInput = false;
            InitializeComponent();
            InitChart(title[0], title[1],title[2]);

            DataBind(dataList[0], dataList[1]);
        }
        void InitChart(string title,string xTitle,string yTitle)
        {
            chart1.Titles.Add(title);
            chart1.Titles[0].ForeColor = Color.White;
            chart1.Titles[0].Font = new Font("微软雅黑", 12f, FontStyle.Regular);
            chart1.Titles[0].Alignment = ContentAlignment.TopCenter;

            //控件背景
            chart1.BackColor = Color.Transparent;
            //图表区背景
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
            chart1.ChartAreas[0].AxisX.Title = "xTitle";
            chart1.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            chart1.ChartAreas[0].AxisX.TitleForeColor = Color.White;
            chart1.ChartAreas[0].AxisX.TextOrientation = TextOrientation.Horizontal;
            chart1.ChartAreas[0].AxisX.ToolTip = "xTitle";
            //X轴网络线条
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

            //Y坐标轴颜色
            chart1.ChartAreas[0].AxisY.LineColor = ColorTranslator.FromHtml("#38587a");
            chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;
            chart1.ChartAreas[0].AxisY.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);
            //Y坐标轴标题
            chart1.ChartAreas[0].AxisY.Title = "yTitle";
            chart1.ChartAreas[0].AxisY.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            chart1.ChartAreas[0].AxisY.TitleForeColor = Color.White;
            chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            chart1.ChartAreas[0].AxisY.ToolTip = "yTitle";
            //Y轴网格线条
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

            chart1.ChartAreas[0].AxisY2.LineColor = Color.Transparent;

            //背景渐变
            chart1.ChartAreas[0].BackGradientStyle = GradientStyle.None;

            //图例样式
            Legend legend2 = new Legend("#VALX");
            legend2.Title = "图例";
            legend2.TitleBackColor = Color.Transparent;
            legend2.BackColor = Color.Transparent;
            legend2.TitleForeColor = Color.White;
            legend2.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            legend2.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            legend2.ForeColor = Color.White;

            chart1.Series[0].XValueType = ChartValueType.String;  //设置X轴上的值类型
            chart1.Series[0].Label = "#VAL";                //设置显示X Y的值    
            chart1.Series[0].LabelForeColor = Color.White;
            chart1.Series[0].ToolTip = "#VALX:#VAL(宗)";     //鼠标移动到对应点显示数值
            chart1.Series[0].ChartType = SeriesChartType.Doughnut;    //图类型(折线)

            chart1.Series[0].Color = Color.Lime;
            chart1.Series[0].LegendText = legend2.Name;
            chart1.Series[0].IsValueShownAsLabel = true;
            chart1.Series[0].LabelForeColor = Color.White;
            chart1.Series[0].CustomProperties = "DrawingStyle = Cylinder";
            chart1.Series[0].CustomProperties = "PieLabelStyle = Outside";
            chart1.Legends.Add(legend2);
            chart1.Legends[0].Position.Auto = true;
            chart1.Series[0].IsValueShownAsLabel = true;
            //是否显示图例
            chart1.Series[0].IsVisibleInLegend = true;
            chart1.Series[0].ShadowOffset = 0;
            chart1.Series[0]["PieLabelStyle"] = "Outside";
            chart1.Series[0]["PieLineColor"] = "White";
            string[] x = new string[] { "南山大队", "福田大队", "罗湖大队", "宝安大队", "指挥处" };
            double[] y = new double[] { 541, 574, 345, 854, 684 };
            //chart1.ChartAreas.Clear(); //图表区
            //chart1.Titles.Clear(); //图表标题
            //chart1.Series.Clear(); //图表序列
            //chart1.Legends.Clear(); //图表图例

            ////新建chart1图表要素
            //chart1.ChartAreas.Add(new ChartArea("chart1Area"));
            //chart1.ChartAreas["chart1Area"].AxisX.IsMarginVisible = false;
            //chart1.ChartAreas["chart1Area"].Area3DStyle.Enable3D = false;
            //chart1.ChartAreas[0].BackColor = Color.Transparent;
            //chart1.ChartAreas[0].BorderColor = Color.Transparent;
            //chart1.Titles.Add("某行业各公司市场占有率调查报告");
            //chart1.Titles[0].Font = new Font("宋体", 20);
            
            //chart1.Series.Add("data");
            //chart1.Series["data"].ChartType = SeriesChartType.Doughnut; //这一行与上个例子不同
            //chart1.Series["data"]["PieLabelStyle"] = "Outside";
            //chart1.Series["data"]["PieLineColor"] = "Black";

            //Legend legend = new Legend("legend");
            //legend.TitleBackColor = Color.Transparent;
            //legend.BackColor = Color.Transparent;

            //chart1.Legends.Add(legend);
            //chart1.Palette = ChartColorPalette.BrightPastel;

            //////为chart1图表赋值
            //////点1
            ////int idxA = chart1.Series["data"].Points.AddY(20);
            ////DataPoint pointA = chart1.Series["data"].Points[idxA];
            ////pointA.Label = "甲公司";
            ////pointA.LegendText = "#LABEL(#VAL) #PERCENT{P2}";
            //////点2
            ////int idxB = chart1.Series["data"].Points.AddY(15);
            ////DataPoint pointB = chart1.Series["data"].Points[idxB];
            ////pointB.Label = "乙公司";
            ////pointB.LegendText = "#LABEL(#VAL) #PERCENT{P2}";
            //////点3
            ////int idxC = chart1.Series["data"].Points.AddY(30);
            ////DataPoint pointC = chart1.Series["data"].Points[idxC];
            ////pointC.Label = "丙公司";
            ////pointC.LegendText = "#LABEL(#VAL) #PERCENT{P2}";
            //////点4
            ////int idxD = chart1.Series["data"].Points.AddY(30);
            ////DataPoint pointD = chart1.Series["data"].Points[idxD];
            ////pointD.Label = "丁公司";
            ////pointD.LegendText = "#LABEL(#VAL) #PERCENT{P2}";
            //////点5
            ////int idxE = chart1.Series["data"].Points.AddY(85);
            ////DataPoint pointE = chart1.Series["data"].Points[idxE];
            ////pointE.Label = "戊公司";
            ////pointE.LegendText = "#LABEL(#VAL) #PERCENT{P2}";
            //chart1.Series[0].Points.DataBindXY(x,y);
        }
        void DataBind(List<string> x, List<string> y)
        {
            int count = 0;
            try
            {
                // chart1.Series[0]["PieLineColor"] = "Gray";
                List<double> y_double = new List<double>();
                foreach (string tmp in y)
                {
                    try
                    {
                        y_double.Add(Convert.ToDouble(tmp));
                    }
                    catch
                    {
                        y_double.Add(0);
                        count++;
                    }
                }
                if (y.Count == count)
                {
                    EmptyInput = true;
                }
                chart1.Series[0].Points.DataBindXY(x, y_double);
            }
            catch
            {
                return;
            }
        }
    }
}
