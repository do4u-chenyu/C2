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
            this.Icon = Properties.Resources.logo;
            this.BackColor = Color.FromArgb(30, 56, 79);
        }
        public Image ConvertToImage(System.Windows.Forms.DataVisualization.Charting.Chart chart)
        {
            MemoryStream mstream = new MemoryStream();
            chart.SaveImage(mstream, System.Drawing.Imaging.ImageFormat.Png);
            Image chartImage = Image.FromStream(mstream);
            mstream.Close();
            return chartImage;
        }
        public void GetbarChart()
        {

            BarChart barChart = new BarChart(xyData, titles);
            barChart.Location = new Point(15, 15);
            barChart.Name = "barChart";
            barChart.Size = new Size(500, 450);
            barChart.TabIndex = 0;
            barChart.GetChart.BackColor = Color.FromArgb(30, 56, 79);
            this.Image= ConvertToImage(barChart.GetChart);


        }
        public void GetPieChart()
        {

            PieChart pieChart = new PieChart(xyData, titles);
            pieChart.GetChart.BackColor = Color.FromArgb(30, 56, 79);
            pieChart.Location = new Point(321, 15);
            pieChart.Name = "pieChart";
            pieChart.Size = new Size(500, 450);
            pieChart.TabIndex = 1;
            this.Image = ConvertToImage(pieChart.GetChart);
        }
        public void GetRadarChart()
        {
            RadarChart radarChart=new RadarChart(xyData, titles);
            radarChart.GetChart.BackColor = Color.FromArgb(30, 56, 79);
            radarChart.Location = new Point(15, 250);
            radarChart.Name = "radarChart";
            radarChart.Size = new Size(500, 450);
            radarChart.TabIndex = 1;
            this.Image = ConvertToImage(radarChart.GetChart);
        }
        public void GetLineChart()
        {
            LineChart lineChart=new LineChart(xyData, titles);
            lineChart.GetChart.BackColor = Color.FromArgb(30, 56, 79);
            lineChart.Location = new Point(15, 750);
            lineChart.Name = "lineChart";
            lineChart.Size = new Size(500, 450);
            lineChart.TabIndex = 1;
            this.Image = ConvertToImage(lineChart.GetChart);
        }
        public void GetRingChart()
        {
            RingChart ringChart = new RingChart(xyData, titles);
            ringChart.GetChart.BackColor = Color.FromArgb(30, 56, 79);
            ringChart.Location = new Point(635, 250);
            ringChart.Name = "ringChart";
            ringChart.Size = new Size(500, 450);
            ringChart.TabIndex = 1;
            this.Image = ConvertToImage(ringChart.GetChart);
        }
    }
}
