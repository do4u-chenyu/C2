using C2.Controls;
using C2.Controls.DataCharts;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

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
            this.Icon = Properties.Resources.logo_icon;
        }
        public Image ConvertToImage(System.Windows.Forms.DataVisualization.Charting.Chart chart)
        {
            MemoryStream mstream = new MemoryStream();
            chart.SaveImage(mstream, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);
            Image chartImage = Image.FromStream(mstream);
            mstream.Close();
            return chartImage;
        }
        public void GetbarChart()
        {
          
            BarChart barChart = new BarChart(xyData, titles);
            barChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(56)))), ((int)(((byte)(79)))));
            barChart.Location = new System.Drawing.Point(15, 15);
            barChart.Name = "barChart";
            barChart.Size = new System.Drawing.Size(500, 431);
            barChart.TabIndex = 0;            
            this.Image= ConvertToImage(barChart.GetChart);

        }
        public void GetPieChart()
        {

            PieChart pieChart = new PieChart(xyData, titles);
            pieChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(56)))), ((int)(((byte)(79)))));
            pieChart.Location = new System.Drawing.Point(321, 15);
            pieChart.Name = "pieChart";
            pieChart.Size = new System.Drawing.Size(300, 231);
            pieChart.TabIndex = 1;
            this.Image = ConvertToImage(pieChart.GetChart);
        }
    }
}
