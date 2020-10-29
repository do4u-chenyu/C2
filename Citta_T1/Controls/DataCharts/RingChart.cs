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
        public RingChart()
        {
            InitializeComponent();
            InitChart();
        }
        void InitChart()
        {
            chart1.ChartAreas.Clear(); //图表区
            chart1.Titles.Clear(); //图表标题
            chart1.Series.Clear(); //图表序列
            chart1.Legends.Clear(); //图表图例

            //新建chart1图表要素
            chart1.ChartAreas.Add(new ChartArea("chart1Area"));
            chart1.ChartAreas["chart1Area"].AxisX.IsMarginVisible = false;
            chart1.ChartAreas["chart1Area"].Area3DStyle.Enable3D = false;
            chart1.ChartAreas[0].BackColor = Color.Transparent;
            chart1.ChartAreas[0].BorderColor = Color.Transparent;
            chart1.Titles.Add("某行业各公司市场占有率调查报告");
            chart1.Titles[0].Font = new Font("宋体", 20);
            
            chart1.Series.Add("data");
            chart1.Series["data"].ChartType = SeriesChartType.Doughnut; //这一行与上个例子不同
            chart1.Series["data"]["PieLabelStyle"] = "Outside";
            chart1.Series["data"]["PieLineColor"] = "Black";

            Legend legend = new Legend("legend");
            legend.TitleBackColor = Color.Transparent;
            legend.BackColor = Color.Transparent;

            chart1.Legends.Add(legend);
            chart1.Palette = ChartColorPalette.BrightPastel;

            //为chart1图表赋值
            //点1
            int idxA = chart1.Series["data"].Points.AddY(20);
            DataPoint pointA = chart1.Series["data"].Points[idxA];
            pointA.Label = "甲公司";
            pointA.LegendText = "#LABEL(#VAL) #PERCENT{P2}";
            //点2
            int idxB = chart1.Series["data"].Points.AddY(15);
            DataPoint pointB = chart1.Series["data"].Points[idxB];
            pointB.Label = "乙公司";
            pointB.LegendText = "#LABEL(#VAL) #PERCENT{P2}";
            //点3
            int idxC = chart1.Series["data"].Points.AddY(30);
            DataPoint pointC = chart1.Series["data"].Points[idxC];
            pointC.Label = "丙公司";
            pointC.LegendText = "#LABEL(#VAL) #PERCENT{P2}";
            //点4
            int idxD = chart1.Series["data"].Points.AddY(30);
            DataPoint pointD = chart1.Series["data"].Points[idxD];
            pointD.Label = "丁公司";
            pointD.LegendText = "#LABEL(#VAL) #PERCENT{P2}";
            //点5
            int idxE = chart1.Series["data"].Points.AddY(85);
            DataPoint pointE = chart1.Series["data"].Points[idxE];
            pointE.Label = "戊公司";
            pointE.LegendText = "#LABEL(#VAL) #PERCENT{P2}";
        }
    }
}
