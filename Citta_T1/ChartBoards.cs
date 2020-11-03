using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C2.Configuration;
using C2.Controls;
using C2.Controls.DataCharts;
using C2.Core;
using C2.Globalization;

namespace C2
{
    class ChartBoards : BaseForm
    {
        string[] x = new string[] { "南山大队", "福田大队", "罗湖大队", "宝安大队", "指挥处" };
        double[] y = new double[] { 541, 574, 345, 854, 684 };
        BarChart barChart;
        PieChart pieChart;
        HorizontalBar3D horizontalBar3D;
        RadarChart radarChart;
        RingChart ringChart;
        LineChart lineChart;
        public ChartBoards()
        {
            Text = "Start";
            

            InitializeComponent();
            AfterInitialize();
        }

        void InitializeComponent()
        {
            string[] x = new string[] { "南山大队", "福田大队", "罗湖大队", "宝安大队", "指挥处" };
            double[] y = new double[] { 541, 574, 345, 854, 684 };
            List<string> y1 = new List<string> { "541", "574", "345", "854", "684" };
            List<string> x1 = new List<string> { "南山大队", "福田大队", "罗湖大队", "宝安大队", "指挥处" };
            List<List<string>> data = new List<List<string>>();

            data.Add(x1);
            data.Add(y1);


            this.barChart = new BarChart(data, new List<string> { "柱状图" });
            this.pieChart = new PieChart(data, new List<string> { "饼图" });
            this.horizontalBar3D = new HorizontalBar3D(data, new List<string> { "横条图" });
            this.radarChart = new C2.Controls.DataCharts.RadarChart(data, new List<string> { "雷达图","2015年" });
            this.ringChart = new C2.Controls.DataCharts.RingChart();
            this.lineChart = new C2.Controls.DataCharts.LineChart();
            this.SuspendLayout();
            // 
            // barChart
            // 
            this.barChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(56)))), ((int)(((byte)(79)))));
            this.barChart.Location = new System.Drawing.Point(15, 15);
            this.barChart.Name = "barChart";
            this.barChart.Size = new System.Drawing.Size(300, 231);
            this.barChart.TabIndex = 0;
            // 
            // pieChart
            // 
            this.pieChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(56)))), ((int)(((byte)(79)))));
            this.pieChart.Location = new System.Drawing.Point(321, 15);
            this.pieChart.Name = "pieChart";
            this.pieChart.Size = new System.Drawing.Size(300, 231);
            this.pieChart.TabIndex = 1;
            // 
            // horizontalBar3D
            // 
            this.horizontalBar3D.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(56)))), ((int)(((byte)(79)))));
            this.horizontalBar3D.Location = new System.Drawing.Point(630, 15);
            this.horizontalBar3D.Name = "horizontalBar3D";
            this.horizontalBar3D.Size = new System.Drawing.Size(600, 231);
            this.horizontalBar3D.TabIndex = 1;
            // 
            // radarChart
            // 
            this.radarChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(56)))), ((int)(((byte)(79)))));
            this.radarChart.Location = new System.Drawing.Point(15, 250);
            this.radarChart.Name = "radarChart";
            this.radarChart.Size = new System.Drawing.Size(600, 431);
            this.radarChart.TabIndex = 1;
            // 
            // ringChart
            // 
            this.ringChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(56)))), ((int)(((byte)(79)))));
            this.ringChart.Location = new System.Drawing.Point(635, 250);
            this.ringChart.Name = "ringChart";
            this.ringChart.Size = new System.Drawing.Size(600, 431);
            this.ringChart.TabIndex = 1;
            // 
            // lineChart
            // 
            this.lineChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(56)))), ((int)(((byte)(79)))));
            this.lineChart.Location = new System.Drawing.Point(15, 750);
            this.lineChart.Name = "lineChart";
            this.lineChart.Size = new System.Drawing.Size(600, 431);
            this.lineChart.TabIndex = 1;
            // 
            // ChartBoards
            // 
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1604, 882);
            this.Controls.Add(this.barChart);
            this.Controls.Add(this.pieChart);
            this.Controls.Add(this.horizontalBar3D);
            this.Controls.Add(this.radarChart);
            this.Controls.Add(this.ringChart);
            this.Controls.Add(this.lineChart);
            this.Name = "ChartBoards";
            this.ResumeLayout(false);

        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        void metroBox1_ItemClick(object sender, ThumbViewItemEventArgs e)
        {
            if (e.Item is FileThumbItem)
            {
                var item = (FileThumbItem)e.Item;
                if (Program.MainForm != null && !string.IsNullOrEmpty(item.Filename))
                {
                    Program.MainForm.OpenDocument(item.Filename);
                }
            }
        }
    }
}
