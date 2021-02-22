using C2.Model.Documents;
using C2.Model.MindMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Core.Exports
{
    class XmindEngine: ChartsExportEngine
    {
        public override string TypeMime
        {
            get { return DocumentType.FreeMind.TypeMime; }
        }

        protected override bool ExportChartToFile(Document document, ChartPage chart, string filename)
        {
            if (chart is MindMap)
            {
                XmindFile.SaveFile((MindMap)chart, filename);
                return true;
            }

            return false;
        }
        protected override bool ExportChartsToFile(Document document, IEnumerable<ChartPage> charts, string filename)
        {
            XmindFile.SaveFile(charts, filename);
            return true;
        }
    }
}
