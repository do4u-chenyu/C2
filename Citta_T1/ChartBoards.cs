﻿using System;
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
            double[] y2 = new double[] { };
            List<string> x1 = new List<string> { "南山大队", "福田大队", "罗湖大队", "宝安大队", "指挥处" };
            List<List<string>> data = new List<List<string>>();
            List<string> titles = new List<string>{"柱状图"};
            data.Add(x1);
            data.Add(y1);

            this.barChart = new BarChart(data, titles);
            this.pieChart = new PieChart(x, y);
            this.horizontalBar3D = new HorizontalBar3D(x, y);

            this.radarChart = new C2.Controls.DataCharts.RadarChart();
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
        /*
        public override void ApplyTheme(UITheme theme)
        {
            base.ApplyTheme(theme);

            this.Font = theme.DefaultFont;
            this.BackColor = theme.Colors. MediumLight;

            if (recentFilesView1 != null)
            {
                recentFilesView1.BackColor = theme.Colors.MediumLight;
                recentFilesView1.CellBackColor = Color.Transparent;// metroBox1.BackColor;
                recentFilesView1.CellForeColor = PaintHelper.FarthestColor(recentFilesView1.BackColor, theme.Colors.Dark, theme.Colors.Light);
                recentFilesView1.ActiveCellBackColor = Color.FromArgb(128, theme.Colors.Sharp);
                recentFilesView1.ActiveCellForeColor = theme.Colors.SharpText;
            }

            if (buttonListBox1 != null)
            {
                buttonListBox1.BackColor = PaintHelper.GetDarkColor(theme.Colors.MediumLight, 0.1f);
                buttonListBox1.ButtonBackColor = Color.Transparent;// theme.Colors.MediumDark;
                buttonListBox1.ButtonForeColor = PaintHelper.FarthestColor(buttonListBox1.BackColor, theme.Colors.Dark, theme.Colors.Light);
                buttonListBox1.ButtonHoverBackColor = theme.Colors.Sharp;
                buttonListBox1.ButtonHoverForeColor = theme.Colors.SharpText;
            }
        }

        protected override void OnCurrentLanguageChanged()
        {
            base.OnCurrentLanguageChanged();

            if (buttonListBox1 != null)
            {
                btnNew.Text = Lang._("New");
                btnOpen.Text = Lang._("Open");
                btnOptions.Text = Lang._("Options");
            }
        }

        void btnOptions_Click(object sender, EventArgs e)
        {
            if (Program.MainForm != null)
            {
                Program.MainForm.ShowOptionsDialog();
            }
        }

        void btnOpen_Click(object sender, EventArgs e)
        {
            if (Program.MainForm != null)
            {
                Program.MainForm.OpenDocument();
            }
        }

        void btnNew_Click(object sender, EventArgs e)
        {
            if (Program.MainForm != null)
            {
                Program.MainForm.NewDocument();
            }
        }
        */
    }
}
