using C2.Business.Model;
using C2.Business.Option;
using C2.Controls;
using C2.Controls.Bottom;
using C2.Controls.Flow;
using C2.Controls.Left;
using C2.Controls.Title;
using C2.Controls.Top;
using C2.Forms;

namespace C2.Core
{
    class Global
    {
        private static MainForm mainForm;
        private static ModelTitlePanel modelTitlePanel;
        private static NaviViewControl naviViewControl;
        private static CanvasPanel canvasPanel;
        private static ModelDocumentDao modelDocumentDao;   // 用户模型工具类
        private static FlowControl flowControl;
        private static OperatorControl operatorControl; 
        private static MyModelControl myModelControl;
        private static RemarkControl remarkControl;
        private static BottomLogControl logView;
        private static OptionDao optionDao;                 // 模型配置工具类
        private static DataSourceControl dataSourceControl; // 左侧数据源面板
        private static BottomConsoleControl bottomPythonConsoleControl; //底层控制台面板
        private static TopToolBarControl topToolBarControl; // 顶层右侧工具栏



        public static MainForm GetMainForm() { return mainForm; }
        public static CanvasForm GetCanvsaForm()
        {
            CanvasForm cf = null;
            if (mainForm != null && mainForm.MdiClient.ActivedMdiForm is CanvasForm)
            {
                cf = mainForm.MdiClient.ActivedMdiForm as CanvasForm;
            }
            return cf;
        }
        public static ModelTitlePanel GetModelTitlePanel() { return modelTitlePanel; }
        public static NaviViewControl GetNaviViewControl()
        {
            NaviViewControl ret = null;
            CanvasPanel cp;
            if (mainForm != null && mainForm.MdiClient.ActivedMdiForm is CanvasForm)
            {
                cp = (mainForm.MdiClient.ActivedMdiForm as CanvasForm).CanvasPanel;
                ret = cp.NaviViewControl;
            }
            return ret;
        }
        public static CanvasPanel GetCanvasPanel()
        {
            CanvasPanel ret = null;
            if (mainForm != null && mainForm.MdiClient.ActivedMdiForm is CanvasForm)
                ret = (mainForm.MdiClient.ActivedMdiForm as CanvasForm).CanvasPanel;
            return ret;
        }
        public static ModelDocumentDao GetModelDocumentDao() {
            ModelDocumentDao ret = null;
            if (mainForm != null && mainForm.MdiClient.ActivedMdiForm is CanvasForm)
                ret = (mainForm.MdiClient.ActivedMdiForm as CanvasForm).ModelDocumentDao;
            return ret;
        }

        public static ModelDocument GetCurrentDocument()
        {
            ModelDocument ret = null;
            if (mainForm != null && mainForm.MdiClient.ActivedMdiForm is CanvasForm)
                ret = (mainForm.MdiClient.ActivedMdiForm as CanvasForm).Document;
            return ret;
        }
        public static FlowControl GetFlowControl()
        {
            FlowControl ret = null;
            CanvasForm cf;
            if (mainForm != null && mainForm.MdiClient.ActivedMdiForm is CanvasForm)
            {
                cf = mainForm.MdiClient.ActivedMdiForm as CanvasForm;
                ret = cf.FlowControl;
            }
            return ret;
        }
        public static OperatorControl GetOperatorControl() {
            OperatorControl ret = null;
            CanvasForm cf;
            if (mainForm != null && mainForm.MdiClient.ActivedMdiForm is CanvasForm)
            {
                cf = mainForm.MdiClient.ActivedMdiForm as CanvasForm;
                ret = cf.OperatorControl;
            }
            return ret;
        }
        public static MyModelControl GetMyModelControl() { return myModelControl; }
        public static RemarkControl GetRemarkControl()
        {
            RemarkControl ret = null;
            CanvasForm cf;
            if (mainForm != null && mainForm.MdiClient.ActivedMdiForm is CanvasForm)
            {
                cf = mainForm.MdiClient.ActivedMdiForm as CanvasForm;
                ret = cf.RemarkControl;
            }
            return ret;
        }
        public static BottomLogControl GetLogView()
        {
            BottomLogControl ret = null;
            CanvasPanel cp;
            if (mainForm != null && mainForm.MdiClient.ActivedMdiForm is CanvasForm)
            {
                cp = (mainForm.MdiClient.ActivedMdiForm as CanvasForm).CanvasPanel;
                //ret = cp.BottomLogControl;
            }
            return ret;
        }
        public static OptionDao GetOptionDao() {
            OptionDao ret = null;
            if (mainForm != null && mainForm.MdiClient.ActivedMdiForm is CanvasForm)
            {
                CanvasForm cf = mainForm.MdiClient.ActivedMdiForm as CanvasForm;
                ret = cf.OptionDao;
            }
            return ret;
        }
        public static DataSourceControl GetDataSourceControl() { return dataSourceControl; }
        public static BottomConsoleControl GetBottomPythonConsoleControl()
        {
            BottomConsoleControl ret = null;
            CanvasPanel cp;
            if (mainForm != null && mainForm.MdiClient.ActivedMdiForm is CanvasForm)
            {
                cp = (mainForm.MdiClient.ActivedMdiForm as CanvasForm).CanvasPanel;
                //ret = cp.bottomConsoleControl;
            }
            return ret;
        }

        public static TopToolBarControl GetTopToolBarControl()
        {
            TopToolBarControl ret = null;
            if (mainForm != null && mainForm.MdiClient.ActivedMdiForm is CanvasForm)
            {
                CanvasForm cf = mainForm.MdiClient.ActivedMdiForm as CanvasForm;
                ret = cf.TopToolBarControl;
            }
            return ret;
        }



        public static void SetMainForm(MainForm mf) { mainForm = mf; }
        public static void SetModelTitlePanel(ModelTitlePanel mtp) { modelTitlePanel = mtp; }
        public static void SetNaviViewControl(NaviViewControl nvc) { naviViewControl = nvc; }
        public static void SetCanvasPanel(CanvasPanel cp) { canvasPanel = cp; }
        public static void SetModelDocumentDao(ModelDocumentDao mdd) { modelDocumentDao = mdd; }
        public static void SetFlowControl(FlowControl fc) { flowControl = fc; }
        public static void SetOperatorControl(OperatorControl oc) { operatorControl = oc; }
        public static void SetMyModelControl(MyModelControl mmc) { myModelControl = mmc; }
        public static void SetRemarkControl(RemarkControl rc) { remarkControl = rc; }
        public static void SetLogView(BottomLogControl lv) { logView = lv; }
        public static void SetOptionDao(OptionDao od) { optionDao = od; }
        public static void SetDataSourceControl(DataSourceControl dsc) { dataSourceControl = dsc; }
        public static void SetBottomPythonConsoleControl(BottomConsoleControl bpcc) { bottomPythonConsoleControl = bpcc; }
        public static void SetTopToolBarControl(TopToolBarControl ttbc) { topToolBarControl = ttbc; }

        private static string workspaceDirectory;           // 用户模型工作目录
        public static string WorkspaceDirectory { get => workspaceDirectory; set => workspaceDirectory = value; }

        public const float Factor = 1.3F;
        private static string versionType;
        public static string VersionType { get => versionType; set => versionType = value; }
        public const string GreenLevel = "Green";
        public const string Nolanding = "NoLogin";
        public const string GreenPath = "source";
        public const string regPath = @"^(?<fpath>([a-zA-Z]:\\)([\s\.\-\w]+\\)*)(?<fname>[\w]+.[\w]+)";
    }
}
