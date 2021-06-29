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
        private APEDocxFile tmpdocxFile = new APEDocxFile();
        public override string TypeMime
        {
            get { return DocumentType.Docx.TypeMime; }
        }
        public  bool MindMapExportChartToFile(Document document, ChartPage chart, string filename) 
        {
            try
            {
                ExportChartToFile(document, chart, filename);
            }
            catch { }
            return true;

        }
        protected override bool ExportChartToFile(Document document, ChartPage chart, string filename) 
        {
            
            if (chart is MindMap)
            {
                MindMap mindMap = (MindMap)chart;
                Topic root = mindMap.Root;
                try
                {
                    using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
                        //docxFile.Save(root, filename);
                        tmpdocxFile.SaveAsDocx(root, filename);
                    return true;
                }
                catch { return false; }
            }

            return false;
        }
    }
}
