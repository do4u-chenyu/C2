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
    public partial class BarChart : UserControl
    {
        private List<string> x;
        private double[] y;
        private string title;
        private List<List<string>> dataList;
        public BarChart(List<string> x, double[] y, string title = "柱状图")
        {
            InitializeComponent();
            this.x = x;
            this.y = y;
            this.title = title;
            InitChart();
        }

        public BarChart(List<List<string>> dataList, List<string> title)
        {
            InitializeComponent();
            this.x = dataList[0];
            this.title = title[0];
            InitChart();
        }
        public void InitChart()
        {
            chart1.Titles.Add(title);
            chart1.Titles[0].ForeColor = Color.White;
            chart1.Titles[0].Font = new Font("微软雅黑", 12f, FontStyle.Regular);
            chart1.Titles[0].Alignment = ContentAlignment.TopCenter;

            //
            //图标背景透明化
            //
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
            //chart1.ChartAreas[0].AxisX.Title = "数量(宗)";
            //chart1.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            //chart1.ChartAreas[0].AxisX.TitleForeColor = Color.White;
            //chart1.ChartAreas[0].AxisX.TextOrientation = TextOrientation.Horizontal;
            //chart1.ChartAreas[0].AxisX.ToolTip = "数量(宗)";
            //X轴网络线条
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

            //Y坐标轴颜色
            chart1.ChartAreas[0].AxisY.LineColor = ColorTranslator.FromHtml("#38587a");
            chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;
            chart1.ChartAreas[0].AxisY.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);
            //Y坐标轴标题
            chart1.ChartAreas[0].AxisY.Title = "数量(宗)";
            chart1.ChartAreas[0].AxisY.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            chart1.ChartAreas[0].AxisY.TitleForeColor = Color.White;
            chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            chart1.ChartAreas[0].AxisY.ToolTip = "数量(宗)";
            //Y轴网格线条
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

            chart1.ChartAreas[0].AxisY2.LineColor = Color.Transparent;
            chart1.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;
            Legend legend = new Legend("legend");
            legend.Title = "legendTitle";

            chart1.Series[0].XValueType = ChartValueType.String;  //设置X轴上的值类型
            chart1.Series[0].Label = "#VAL";                //设置显示X Y的值    
            chart1.Series[0].LabelForeColor = Color.White;
            chart1.Series[0].ToolTip = "#VALX:#VAL";     //鼠标移动到对应点显示数值
            chart1.Series[0].ChartType = SeriesChartType.Column;    //图类型(折线)


            chart1.Series[0].Color = Color.Lime;
            chart1.Series[0].LegendText = legend.Name;
            chart1.Series[0].IsValueShownAsLabel = true;
            chart1.Series[0].LabelForeColor = Color.White;
            chart1.Series[0].CustomProperties = "DrawingStyle = Cylinder";
            chart1.Legends.Add(legend);
            chart1.Legends[0].Position.Auto = false;


            //绑定数据
            chart1.Series[0].Points.DataBindXY(x, y);
            chart1.Series[0].Points[0].Color = Color.White;
            chart1.Series[0].Palette = ChartColorPalette.Bright;
        }
        public void save()
        {
            chart1.SaveImage("test.png",System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);
        }
    }
}
