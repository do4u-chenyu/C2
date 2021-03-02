using C2.Dialogs;
using C2.Globalization;
using C2.Model.Documents;
using C2.Model.MindMaps;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Core.Exports
{
    class XmindEngine: ChartsExportEngine
    {
        public override string TypeMime
        {
            get { return DocumentType.Xmind.TypeMime; }
        }

        protected override bool ExportChartToFile(Document document, ChartPage chart, string filename)
        {
            if (chart is MindMap)
            {
                using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
                    XmindFile.SaveFile((MindMap)chart, filename);
                return true;
            }

            return false;
        }
        protected override bool ExportChartsToFile(Document document, IEnumerable<ChartPage> charts, string filename)
        {
            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
                XmindFile.SaveFile(charts, filename);
            return true;
        }
        public override void Export(Document document, IEnumerable<ChartPage> charts)
        {
            if (document == null || charts == null)
                throw new ArgumentNullException();

            if (charts.IsEmpty())
                return;

            var documentType = DocumentType;
            if (documentType == null)
                return;

            if (charts.Count() > 0)
            {
                var dialog = new SaveFileDialog();
                dialog.Filter = documentType.FileDialogFilter;
                dialog.DefaultExt = documentType.DefaultExtension;
                dialog.Title = Lang._("Export");
                dialog.FileName = ST.EscapeFileName(document.Name);
                if (dialog.ShowDialog(Global.GetMainForm()) == DialogResult.OK)
                {
                    if (ExportChartsToFile(document, charts, dialog.FileName))
                    {
                        var fld = new FileLocationDialog(dialog.FileName, dialog.FileName);
                        fld.Text = Lang._("Export Success");
                        fld.ShowDialog(Global.GetMainForm());
                    }
                }
            }

        }
    }
}
