using System.Drawing;
using System.IO;
using System.Text;
using Citta_T1.Controls.MapViews;
using Citta_T1.Core.Documents;
using Citta_T1.Model;
using Citta_T1.Model.Documents;
using Citta_T1.Model.MindMaps;

namespace Citta_T1.Core.Exports
{
    class TxtEngine : ChartsExportEngine
    {
        public override string TypeMime
        {
            get { return "text/plain"; }
        }

        /*public override bool Export(MindMapView view, string filename)
        {
            string str = string.Empty;

            if (view.Map != null && view.Map.Root != null)
            {
                TextSerializer ts = new TextSerializer();
                str = ts.SerializeObjects(new ChartObject[] { view.Map.Root });
            }

            using (StreamWriter sw = new StreamWriter(filename, false, Encoding.Default))
            {
                sw.Write(str);
                sw.Close();
            }

            return true;
        }*/

        protected override bool ExportChartToFile(Document document, ChartPage chart, string filename)
        {
            string str = string.Empty;

            if (chart is MindMap)
            {
                var mindMap = (MindMap)chart;
                if (mindMap.Root != null)
                {
                    var ts = new TextSerializer();
                    str = ts.SerializeObjects(new ChartObject[] { mindMap.Root }, true);
                }

                using (var sw = new StreamWriter(filename, false, Encoding.Default))
                {
                    sw.Write(str);
                    sw.Close();
                }

                return true;
            }

            return false;
            //return base.ExportChartToFile(document, chart, filename);
        }
    }
}
