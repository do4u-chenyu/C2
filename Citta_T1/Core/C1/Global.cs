using C2.Business.Model;
using C2.Business.Option;
using C2.Controls;
using C2.Controls.Bottom;
using C2.Controls.Flow;
using C2.Controls.Left;
using C2.Controls.Right;
using C2.Controls.Top;
using C2.Database;
using C2.Forms;
using C2.Model.Documents;
using C2.Model.MindMaps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace C2.Core
{
    class Global
    {
        private static MainForm mainForm;
        private static MyModelControl myModelControl;
        private static BottomLogControl logView;
        private static DataSourceControl dataSourceControl; // 左侧数据源面板
        private static Panel bottomViewPanle;
        private static Panel workSpacePanel;
        private static Panel leftToolBoxPanel;
        private static TaskBar taskBar;
        private static MyMindMapControl mindMapModelControl;





        public static MainForm GetMainForm() { return mainForm; }
        public static TaskBar GetTaskBar() { return taskBar; }
        public static Panel GetLeftToolBoxPanel() { return leftToolBoxPanel; }
        public static DataSourceControl GetDataSourceControl() { return dataSourceControl; }
        public static MyModelControl GetMyModelControl() { return myModelControl; }
        public static BottomLogControl GetLogView() { return logView; }
        public static Control GetBottomViewPanel() { return bottomViewPanle; }
        public static Control GetWorkSpacePanel() { return workSpacePanel; }
        public static MyMindMapControl GetMindMapModelControl() { return mindMapModelControl; }

        public static CanvasForm GetCanvasForm()
        {
            CanvasForm cf = null;
            if (mainForm != null && mainForm.MdiClient != null && mainForm.MdiClient.ActivedMdiForm is CanvasForm)
            {
                cf = mainForm.MdiClient.ActivedMdiForm as CanvasForm;
            }
            return cf;
        }
        public static DocumentForm GetDocumentForm()
        {
            DocumentForm df = null;
            if (mainForm != null && mainForm.MdiClient != null && mainForm.MdiClient.ActivedMdiForm is DocumentForm)
            {
                df = mainForm.MdiClient.ActivedMdiForm as DocumentForm;
            }
            return df;
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
            if (Global.GetCanvasForm() != null)
            {
                ret = Global.GetCanvasForm().NaviViewControl;
            }
            return ret;
        }

        public static ModelDocument GetCurrentModelDocument()
        {
            ModelDocument ret = null;
            if (Global.GetCanvasForm() != null)
                ret = Global.GetCanvasForm().Document;
            return ret;
        }

        public static Document GetCurrentDocument()
        {
            Document ret = null;
            if (Global.GetDocumentForm() != null)
                ret = Global.GetDocumentForm().Document;
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
        public static void SetTaskBar(TaskBar tb) { taskBar = tb; }
        public static void SetLeftToolBoxPanel(Panel ltbp) { leftToolBoxPanel = ltbp; }
        public static void SetDataSourceControl(DataSourceControl dsc) { dataSourceControl = dsc; }
        public static void SetMyModelControl(MyModelControl mmc) { myModelControl = mmc; }
        public static void SetLogView(BottomLogControl lv) { logView = lv; }
        public static void SetBottomViewPanel(Panel bv) { bottomViewPanle = bv; }
        public static void SetWorkSpacePanel(Panel ws) { workSpacePanel = ws; }
        public static void SetMindMapModelControl(MyMindMapControl mmmc) { mindMapModelControl = mmmc; }
        private static string workspaceDirectory;           // 用户模型工作目录
        public static string WorkspaceDirectory { get => workspaceDirectory; set => workspaceDirectory = value; }
        public static string UserWorkspacePath { get => Path.Combine( workspaceDirectory,mainForm.UserName); }
        public static string BusinessViewPath { get => Path.Combine(UserWorkspacePath, "业务视图"); }
        public static string MarketViewPath { get => Path.Combine(UserWorkspacePath, "模型市场"); }

        public const float Factor = 1.3F;
        private static string versionType;
        public static string VersionType { get => versionType; set => versionType = value; }
        public const string GreenLevel = "Green";
        public const string Nolanding = "NoLogin";
        public const string GreenPath = "source";
        public const string regPath = @"^(?<fpath>([a-zA-Z]:\\)([\s\.\-\w]+\\)*)(?<fname>[\w]+.[\w]+)";
        public const int ChartNum = 3;
        public static List<string> ChartNames = new List<string> { "业务拓展视图", "组织架构视图", "运作模式视图" };
        public static Dictionary<string, MindMapLayoutType> ChartOptions = new Dictionary<string, MindMapLayoutType>
        {
            { "业务拓展视图", MindMapLayoutType.MindMap}, {"组织架构视图", MindMapLayoutType.OrganizationDown}, { "运作模式视图", MindMapLayoutType.MindMap}
        };
        public static void OnModifiedChange()
        {
            if (GetCurrentDocument() == null)
                return;
            if (!GetCurrentDocument().Modified)
                GetCurrentDocument().Modified = true;
        }

        public static List<BaseDocumentForm> SearchDocumentForm(string formName)
        {
            if (GetMainForm() == null)
                return new List<BaseDocumentForm>();
            return GetMainForm().SearchDocument(formName);
        }
    }
}
