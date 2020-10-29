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
    public partial class LineChart : UserControl
    {
        public LineChart()
        {
            InitializeComponent();
            InitChart();
        }
        void InitChart()
        {
            int[] arr = new int[] { 94, 52, 25, 67, 91, 77, 69, 56 };
            chart1.Series.Clear();  //清除默认的Series
            Series Strength = new Series("力量");  //new 一个叫做【Strength】的系列
            Strength.ChartType = SeriesChartType.Spline;  //设置chart的类型，spline样条图 Line折线图
            Strength.IsValueShownAsLabel = true; //把值当做标签展示（默认false）
            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 10;  //设置网格间隔（这里设成0.5，看得更直观一点）
            ////chart1.ChartAreas[0].AxisX.Maximum = 100;//设定x轴的最大值
            //chart1.ChartAreas[0].AxisY.Maximum = 100;//设定y轴的最大值
            //chart1.ChartAreas[0].AxisX.Minimum = 0;//设定x轴的最小值
            //chart1.ChartAreas[0].AxisY.Minimum = 0;//设定y轴的最小值
            chart1.ChartAreas[0].AxisY.Interval = 10; //设置Y轴每个刻度的跨度
            //给系列上的点进行赋值，分别对应横坐标和纵坐标的值
            for (int i = 2; i <= arr.Length; i++)
            {
                Strength.Points.AddXY(i, arr[i - 1]);
            }
            //把series添加到chart上
            chart1.Series.Add(Strength);
            

        }


    }
}
