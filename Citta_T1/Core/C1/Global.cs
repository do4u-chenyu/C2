﻿using C2.Business.Model;
using C2.Business.Option;
using C2.Controls;
using C2.Controls.Bottom;
using C2.Controls.C1.Left;
using C2.Controls.Flow;
using C2.Controls.Left;
using C2.Controls.Right;
using C2.Controls.Top;
using C2.Forms;
using C2.Model.Documents;
using C2.Model.MindMaps;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace C2.Core
{
    class Global
    {
        private static string username;
        private static MainForm mainForm;
        private static MyModelControl myModelControl;
        private static SearchToolkitControl searchToolkitControl;
        private static WebsiteFeatureDetectionControl websiteFeatureDetectionControl;
        private static BottomLogControl logView;
        private static DataSourceControl dataSourceControl; // 左侧数据源面板
        private static Panel bottomViewPanle;
        private static Panel workSpacePanel;
        private static Panel leftToolBoxPanel;
        private static TaskBar taskBar;
        private static MyMindMapControl mindMapModelControl;
        private static IAOLabControl iaoLabControl;



        public static string GetUsername() { return username; }
        public static MainForm GetMainForm() { return mainForm; }
        public static TaskBar GetTaskBar() { return taskBar; }
        public static Panel GetLeftToolBoxPanel() { return leftToolBoxPanel; }
        public static DataSourceControl GetDataSourceControl() { return dataSourceControl; }
        public static MyModelControl GetMyModelControl() { return myModelControl; }
        public static WebsiteFeatureDetectionControl GetWebsiteFeatureDetectionControl() { return websiteFeatureDetectionControl; }
        public static SearchToolkitControl GetSearchToolkitControl() { return searchToolkitControl; }
        public static IAOLabControl GetIAOLabControl() { return iaoLabControl; }
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

        public static OperatorControl GetOperatorControl()
        {
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
        public static OptionDao GetOptionDao()
        {
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

        public static void SetUsername(string un) { username = un; }
        public static void SetMainForm(MainForm mf) { mainForm = mf; }
        public static void SetTaskBar(TaskBar tb) { taskBar = tb; }
        public static void SetLeftToolBoxPanel(Panel ltbp) { leftToolBoxPanel = ltbp; }
        public static void SetDataSourceControl(DataSourceControl dsc) { dataSourceControl = dsc; }
        public static void SetMyModelControl(MyModelControl mmc) { myModelControl = mmc; }
        public static void SetWebsiteFeatureDetectionControl(WebsiteFeatureDetectionControl wfdc) { websiteFeatureDetectionControl = wfdc; }
        public static void SetSearchToolkitControl(SearchToolkitControl stc) { searchToolkitControl = stc; }
        public static void SetIAOLabControl(IAOLabControl ilc) { iaoLabControl = ilc; }
        public static void SetLogView(BottomLogControl lv) { logView = lv; }
        public static void SetBottomViewPanel(Panel bv) { bottomViewPanle = bv; }
        public static void SetWorkSpacePanel(Panel ws) { workSpacePanel = ws; }
        public static void SetMindMapModelControl(MyMindMapControl mmmc) { mindMapModelControl = mmmc; }

        public static string WorkspaceDirectory { get; set; } // 用户空间根目录
        public static string UserWorkspacePath { get => Path.Combine(WorkspaceDirectory, username); }
        public static string BusinessViewPath { get => Path.Combine(UserWorkspacePath, "业务视图"); }
        public static string MarketViewPath { get => Path.Combine(UserWorkspacePath, "模型市场"); }
        public static string SearchToolkitPath { get => Path.Combine(UserWorkspacePath, "全文工具箱"); }
        public static string TempDirectory { get; set; }
        public const float Factor = 1.3F;

        public static string VersionType { get; set; }
        public const string GreenLevel = "Green";
        public const string Nolanding = "NoLogin";
        public const string GreenPath = "source";
        public const string IAOLab = "APK, BaseStation, Wifi, Card, Tude, Ip ";
        public const string regPath = @"^(?<fpath>([a-zA-Z]:\\)([\s\.\-\w]+\\)*)(?<fname>[\w]+.[\w]+)";
        public const int ChartNum = 3;
        public static string LocalPluginsPath = Path.Combine(Application.StartupPath, "plugins");
        public static string SoftwareSavePath = Path.Combine(Application.StartupPath, "update", "install");
        public static List<string> ChartNames = new List<string> { "业务拓展视图", "组织架构视图", "运作模式视图" };
        public const string DLLHostUrl = @"http://218.94.117.234:8484/C2Plugins/";
        public const string SoftwareUrl = @"http://218.94.117.234:8484/C2Software/";
        public const string DLLPackageUrl = DLLHostUrl + @"packages/";

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
