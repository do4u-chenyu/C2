using C2.Controls.DataCharts;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            if (barChart.EmptyInput)
            {
                this.Image = PaintEmptyFigure("柱状图");
                return;
            }
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
            if (pieChart.EmptyInput)
            {
                this.Image = PaintEmptyFigure("饼图");
                return;
            }
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
            if (radarChart.EmptyInput)
            {
                this.Image = PaintEmptyFigure("雷达图");
                return;
            }
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
            if (lineChart.EmptyInput)
            {
                this.Image = PaintEmptyFigure("折线图");
                return;
            }
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
            if (ringChart.EmptyInput)
            {
                this.Image = PaintEmptyFigure("圆环图");
                return;
            }
            ringChart.GetChart.BackColor = Color.FromArgb(30, 56, 79);
            ringChart.Location = new Point(635, 250);
            ringChart.Name = "ringChart";
            ringChart.Size = new Size(500, 450);
            ringChart.TabIndex = 1;
            this.Image = ConvertToImage(ringChart.GetChart);
        }
        #region 画空白图
        private Image PaintEmptyFigure(string chartType)
        {
            int initialWidth = 500, initialHeight = 450;
            Bitmap theBitmap = new Bitmap(initialWidth, initialHeight);
            Graphics theGraphics = Graphics.FromImage(theBitmap);
            theGraphics.Clear(Color.FromArgb(30, 56, 79));
            theGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Font theFont = new Font("微软雅黑", 13.0f, System.Drawing.FontStyle.Regular);
            Color col = Color.FromArgb(255, 255, 255);
            Brush newBrush = new SolidBrush(col);
            string info = string.Format("数据类型不正确，无法绘制{0}.",chartType);
            theGraphics.DrawString(info, theFont, newBrush, 110, 210);
            return theBitmap;
        }
        #endregion
    }
}
