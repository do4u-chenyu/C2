using C2.Business.Model;
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
        private static string username = "IAO";
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

        public static string WorkspaceDirectory { get; set; } = string.Empty; // 用户空间根目录
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
        public static string GambleScriptPath = Path.Combine(Application.StartupPath, "Resources", "Script", "IAO_Search_gamble", "batchquery_db_accountPass_C2_20210324.py");
        public static List<string> ChartNames = new List<string> { "业务拓展视图", "组织架构视图", "运作模式视图" };
        public const string DLLHostUrl = @"http://218.94.117.234:8484/C2Plugins/";
        public const string SoftwareUrl = @"http://218.94.117.234:8484/C2Software/";
        public const string DLLPackageUrl = DLLHostUrl + @"packages/";
        public static Dictionary<string, string> WFDPredictionCodeDict = new Dictionary<string, string>
            {
                {"101090101", "贷款-P2P"},{"101090102", "贷款-抵押"},{"101090103", "贷款-小额"},{"101090104", "贷款-资讯"},{"101090105", "贷款-综合"},{"101090106", "贷款-租赁"},
                {"1010301", "赌-彩票预测"},{"1010302", "赌-赌场系"},{"1010303","赌-购彩系"},{"1010304", "赌-电子游戏"},{"1010305", "赌-球"},{"1010101", "黄-视频"},{"1010102", "黄-成人用品用药"},
                {"10111", "签名网站"},{"1010103", "黄-小说漫画"},{"1010104", "黄-性感图"},{"1010105", "黄-直播"},{"101020301", "宗教-场所"},{"101020302", "宗教-机构"},{"101020303", "宗教-文化"},
                {"101020304", "宗教-用品"},{"1010401", "Vpn-非法"},{"1010402", "Vpn-商务"},{"10106", "打码"},{"10112", "VPS"},{"10107", "短链接"},{"10108", "配资"},
                {"10105", "其他"},{"10113", "四方支付"},{"10114", "云发卡"},{"10115", "流量刷单"},{"10116", "微交易"},{"10117", "云呼"},{"10118","CDN"},{"10119","第三方维护助手"},
                {"101110","广告联盟"},{"101111","代刷"},{"1010106","黄—外围"},{"101114","接码平台"},{"101112","后台登录"},{"101115","机场"},{"101116","政府"},{"101117","学校"},{"101118","医院"},
                {"101119","虚拟币交易"},{"101121","证券期货交易"},{"101124","网游加速器"},{"101122","外汇交易"},{"101120","游戏交易网站"},{"101123","客服"}
            };

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
