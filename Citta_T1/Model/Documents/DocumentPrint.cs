using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using C2.Configuration;
using C2.Controls;
using C2.Controls.MapViews;
using C2.Core;
using C2.Model.MindMaps;

namespace C2.Model.Documents
{
    class DocumentPrint
    {
        public Document Document { get; private set; }
        public ChartPage[] Charts { get; private set; }
        public int PageIndex { get; private set; }

        public DocumentPrint(Document document, IEnumerable<ChartPage> charts)
        {
            if (charts == null)
                throw new ArgumentException();

            Document = document;
            Charts = charts.ToArray();
        }

        public void ResetPageIndex()
        {
            PageIndex = 0;
        }

        public void PrintPage(object sender, PrintPageEventArgs e)
        {
            if (PageIndex < 0)
            {
                return;
            }

            if (PageIndex >= Charts.Length)
            {
                PageIndex = 0;
            }

            var chart = Charts[PageIndex++];
            PrintChart(chart, e);

            e.HasMorePages = PageIndex < Charts.Length;
        }

        void PrintChart(ChartPage chart, PrintPageEventArgs e)
        {
            if (chart == null)
                throw new ArgumentNullException();

            if (chart is MindMap)
            {
                var state = e.Graphics.Save();
                var contentSize = chart.GetContentSize();
                var zoom = PaintHelper.GetZoom(contentSize, e.PageBounds.Size);
                var args = new RenderArgs(RenderMode.Print, e.Graphics, (MindMap)chart, ChartBox.DefaultChartFont);
                e.Graphics.ScaleTransform(zoom, zoom);
                
                var renderer = new GeneralRender();
                renderer.Paint(args.Chart, args);
                e.Graphics.Restore(state);

                // print document title
                if (Options.Current.GetBool(C2.Configuration.OptionNames.PageSettigs.PrintDocumentTitle))
                {
                    var ptTitle = e.MarginBounds.Location;
                    var brush = new SolidBrush(Color.Black);
                    e.Graphics.DrawString(chart.Name, ChartBox.DefaultChartFont, brush, ptTitle.X, ptTitle.Y);
                }
            }
        }
    }
}
