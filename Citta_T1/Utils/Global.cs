using Citta_T1.Dgvs;
using Citta_T1.Controls.Flow;
using Citta_T1.Controls.Title;
using Citta_T1.Business.Model;
using Citta_T1.Controls;
using Citta_T1.Controls.Left;
using Citta_T1.Business.Option;

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
        private static RemarkControl remarkControl;
        private static LogView logView;
        private static OptionDao optionDao;

        public static MainForm GetMainForm() { return mainForm; }
        public static ModelTitlePanel GetModelTitlePanel() { return modelTitlePanel; }
        public static NaviViewControl GetNaviViewControl() { return naviViewControl; }
        public static CanvasPanel GetCanvasPanel() { return canvasPanel; }
        public static ModelDocumentDao GetModelDocumentDao() { return modelDocumentDao; }

        public static ModelDocument GetCurrentDocument() {
            ModelDocument ret = null;
            if (GetModelDocumentDao() != null)
                ret = GetModelDocumentDao().CurrentDocument;
            return ret; 
        }
        public static FlowControl GetFlowControl() { return flowControl; }
        public static MyModelControl GetMyModelControl() { return myModelControl; }
        public static RemarkControl GetRemarkControl() { return remarkControl; }
        public static LogView GetLogView() { return logView; } 
        public static OptionDao GetOptionDao() { return optionDao; } 

        public static void SetMainForm(MainForm mf) { mainForm = mf; }
        public static void SetModelTitlePanel(ModelTitlePanel mtp) { modelTitlePanel = mtp; }
        public static void SetNaviViewControl(NaviViewControl nvc) { naviViewControl = nvc; }
        public static void SetCanvasPanel(CanvasPanel cp) { canvasPanel = cp; }
        public static void SetModelDocumentDao(ModelDocumentDao mdd) { modelDocumentDao = mdd; }
        public static void SetFlowControl(FlowControl fc) { flowControl = fc; }
        public static void SetMyModelControl(MyModelControl mmc) { myModelControl = mmc; }
        public static void SetRemarkControl(RemarkControl rc) { remarkControl = rc; }
        public static void SetLogView(LogView lv) { logView = lv; }
        public static void SetOptionDao(OptionDao od) { optionDao = od; }  
    }
}
