using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Citta_T1.Controls.Flow;
using Citta_T1.Controls.Title;
using Citta_T1.Business;
using Citta_T1.Controls;
using Citta_T1.Controls.Left;

namespace Citta_T1.Utils
{
    class Global
    {
        private static MainForm mainForm;
        private static ModelTitlePanel modelTitlePanel;
        private static NaviViewControl naviViewControl;
        private static CanvasPanel canvasPanel;
        private static ModelDocumentDao modelDocumentDao;
        private static FlowControl flowControl;
        private static MyModelControl myModelControl;

        public static MainForm GetMainForm() { return mainForm; }
        public static ModelTitlePanel GetModelTitlePanel() { return modelTitlePanel; }
        public static NaviViewControl GetNaviViewControl() { return naviViewControl; }
        public static CanvasPanel GetCanvasPanel() { return canvasPanel; }
        public static ModelDocumentDao GetModelDocumentDao() { return modelDocumentDao; }

        public static ModelDocument GetCurrentDocument() { return GetModelDocumentDao().CurrentDocument; }
        public static FlowControl GetFlowControl() { return flowControl; }
        public static MyModelControl GetMyModelControl() { return myModelControl; }

        public static void SetMainForm(MainForm mf) { mainForm = mf; }
        public static void SetModelTitlePanel(ModelTitlePanel mtp) { modelTitlePanel = mtp; }
        public static void SetNaviViewControl(NaviViewControl nvc) { naviViewControl = nvc; }
        public static void SetCanvasPanel(CanvasPanel cp) { canvasPanel = cp; }
        public static void SetModelDocumentDao(ModelDocumentDao mdd) { modelDocumentDao = mdd; }
        public static void SetFlowControl(FlowControl fc) { flowControl = fc; }
        public static void SetMyModelControl(MyModelControl mmc) { myModelControl = mmc; }
    }
}
