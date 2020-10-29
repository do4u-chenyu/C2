using C2.Business.Model;
using C2.Business.Option;
using C2.Controls;
using C2.Controls.Bottom;
using C2.Controls.Flow;
using C2.Controls.Left;
using C2.Controls.Title;
using C2.Controls.Top;
using C2.Forms;
using System.Windows.Forms;

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
        private static Panel bottomViewPanle;



        public static MainForm GetMainForm() { return mainForm; }
        public static DataSourceControl GetDataSourceControl() { return dataSourceControl; }
        public static BottomConsoleControl GetBottomPythonConsoleControl() { return bottomPythonConsoleControl; }
        public static Control GetBottomViewPanel() { return bottomViewPanle; }
        public static MyModelControl GetMyModelControl() { return myModelControl; }
        public static ModelTitlePanel GetModelTitlePanel() { return modelTitlePanel; }
        public static CanvasForm GetCanvasForm()
        {
            CanvasForm cf = null;
            if (mainForm != null && mainForm.MdiClient != null && mainForm.MdiClient.ActivedMdiForm is CanvasForm)
            {
                cf = mainForm.MdiClient.ActivedMdiForm as CanvasForm;
            }
            return cf;
        }
        public static CanvasPanel GetCanvasPanel()
        {
            CanvasPanel ret = null;
            if (Global.GetCanvasForm() != null)
                ret = Global.GetCanvasForm().CanvasPanel;
            return ret;
        }
        public static NaviViewControl GetNaviViewControl()
        {
            NaviViewControl ret = null;
            if (Global.GetCanvasPanel() != null)
            {
                ret = Global.GetCanvasPanel().NaviViewControl;
            }
            return ret;
        }
        public static ModelDocumentDao GetModelDocumentDao() {
            ModelDocumentDao ret = null;
            if (Global.GetCanvasForm() != null)
                ret = Global.GetCanvasForm().ModelDocumentDao;
            return ret;
        }

        public static ModelDocument GetCurrentDocument()
        {
            ModelDocument ret = null;
            if (Global.GetCanvasForm() != null)
                ret = Global.GetCanvasForm().Document;
            return ret;
        }
        public static FlowControl GetFlowControl()
        {
            FlowControl ret = null;
            if (Global.GetCanvasForm() != null)
                ret = Global.GetCanvasForm().FlowControl;
            return ret;
        }
        public static OperatorControl GetOperatorControl() {
            OperatorControl ret = null;
            if (Global.GetCanvasForm() != null)
                ret = Global.GetCanvasForm().OperatorControl;
            return ret;
        }
        public static RemarkControl GetRemarkControl()
        {
            RemarkControl ret = null;
            if (Global.GetCanvasForm() != null)
                ret = Global.GetCanvasForm().RemarkControl;
            return ret;
        }
        public static BottomLogControl GetLogView(){ return logView;}
        public static OptionDao GetOptionDao() {
            OptionDao ret = null;
            if (Global.GetCanvasForm() != null)
                ret = Global.GetCanvasForm().OptionDao;
            return ret;
        }

        public static TopToolBarControl GetTopToolBarControl()
        {
            TopToolBarControl ret = null;
            if (Global.GetCanvasForm() != null)
                ret = Global.GetCanvasForm().TopToolBarControl;
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
        public static void SetBottomViewPanel(Panel bv) { bottomViewPanle = bv; }

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
