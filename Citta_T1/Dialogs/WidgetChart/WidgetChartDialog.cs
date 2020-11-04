using C2.Controls;
using C2.Controls.DataCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Dialogs.WidgetChart
{
    partial class WidgetChartDialog : PictureViewDialog
    {
  
        private List<string> titles;
        List<List<string>> xyData = new List<List<string>>();
        public WidgetChartDialog(List<List<string>> xyValues, List<string> titles)
        {
            this.xyData = xyValues;
            this.titles = titles;
        }

        public void GetbarChart()
        {
          
            //this.SuspendLayout();
            BarChart barChart = new BarChart(xyData, titles);
            barChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(56)))), ((int)(((byte)(79)))));
            barChart.Location = new System.Drawing.Point(15, 15);
            barChart.Name = "barChart";
            barChart.Size = new System.Drawing.Size(500, 431);
            barChart.TabIndex = 0;
            this.Image= barChart.Save();
            //this.ResumeLayout(false);
        }
        public void GetPieChart()
        {
            string[] x=new string[] { }; double[] y=new double[] { };
            PieChart pieChart = new PieChart(x, y);
            pieChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(56)))), ((int)(((byte)(79)))));
            pieChart.Location = new System.Drawing.Point(321, 15);
            pieChart.Name = "pieChart";
            pieChart.Size = new System.Drawing.Size(300, 231);
            pieChart.TabIndex = 1;
            this.Controls.Add(pieChart);
        }
    }
}
