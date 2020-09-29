using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Citta_T1.Controls;
using Citta_T1.Controls.MapViews;
using Citta_T1.Core;
using Citta_T1.Model.Documents;
using Citta_T1.Model.MindMaps;

namespace Citta_T1.Core.Exports
{
    abstract class ImageExportEngine : ChartsExportEngine
    {
        protected abstract void SaveImage(Image image, string filename);

        protected override bool ExportChartToFile(Document document, ChartPage chart, string filename)
        {
            if (chart == null)
                throw new ArgumentNullException();

            if (string.IsNullOrEmpty(filename))
                throw new ArgumentException("filename");

            if (!OptionsInitalizated && !GetOptions())
                return false;

            //ChartPage = chart;

            //chart.EnsureChartLayouted();

            Size contentSize = chart.GetContentSize();
            if (contentSize.Width <= 0 || contentSize.Height <= 0)
                return false;

            Bitmap bmp = new Bitmap(contentSize.Width, contentSize.Height);
            using (Graphics grf = Graphics.FromImage(bmp))
            {
                PaintHelper.SetHighQualityRender(grf);

                if (!TransparentBackground)
                {
                    grf.Clear(chart.BackColor);
                }

                var render = new GeneralRender();
                var args = new RenderArgs(RenderMode.Export, grf, (MindMap)chart, ChartBox.DefaultChartFont);
                render.Paint((MindMap)chart, args);
            }

            SaveImage(bmp, filename);

            return true;
        }
    }
}
