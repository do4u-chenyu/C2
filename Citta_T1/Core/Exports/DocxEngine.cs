using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C2.Model.Documents;
using C2.Model.MindMaps;
using C2.Utils;

namespace C2.Core.Exports
{
    class DocxEngine : ChartsExportEngine
    {
        private DocxFile docxFile = new DocxFile();
        public override string TypeMime
        {
            get { return DocumentType.Docx.TypeMime; }
        }
        protected override bool ExportChartToFile(Document document, ChartPage chart, string filename) 
        {
            
            if (chart is MindMap)
            {
                MindMap mindMap = (MindMap)chart;
                Topic root = mindMap.Root;
                using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
                    docxFile.Save(root, filename);
                return true;
            }

            return false;
        }
    }
}
