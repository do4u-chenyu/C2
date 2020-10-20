using System.Drawing;
using System.IO;
using System.Text;
using C2.Controls.MapViews;
using C2.Core.Documents;
using C2.Model;
using C2.Model.Documents;
using C2.Model.MindMaps;

namespace C2.Core.Exports
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
