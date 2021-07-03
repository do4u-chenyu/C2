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
        private static string username = "IAO";
        private static MainForm mainForm;
        private static MyModelControl myModelControl;
        private static SearchToolkitControl searchToolkitControl;
        private static WebsiteFeatureDetectionControl websiteFeatureDetectionControl;
        private static CastleBravoControl castleBravoControl;
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
        public static CastleBravoControl GetCastleBravoControl() { return castleBravoControl; }
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
        public static void SetCastleBravoControl(CastleBravoControl cbc) { castleBravoControl = cbc; }
        public static void SetSearchToolkitControl(SearchToolkitControl stc) { searchToolkitControl = stc; }
        public static void SetIAOLabControl(IAOLabControl ilc) { iaoLabControl = ilc; }
        public static void SetLogView(BottomLogControl lv) { logView = lv; }
        public static void SetBottomViewPanel(Panel bv) { bottomViewPanle = bv; }
        public static void SetWorkSpacePanel(Panel ws) { workSpacePanel = ws; }
        public static void SetMindMapModelControl(MyMindMapControl mmmc) { mindMapModelControl = mmmc; }

        public static string WorkspaceDirectory { get; set; } = string.Empty; // 用户空间根目录
        public static string UserWorkspacePath { get => Path.Combine(WorkspaceDirectory, username); }
        public static string BusinessViewPath { get => Path.Combine(UserWorkspacePath, "业务视图"); }
        public static string MarketViewPath { get => Path.Combine(UserWorkspacePath, "聚沙成塔"); }
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
        public static Dictionary<string, string> WFDPredictionCodeDict = new Dictionary<string, string>
        {
            {"0010001","贷款-P2P"},{"0010002","贷款-抵押"},{"0010003","贷款-小额"},{"0010004","贷款-资讯"},{"0010005","贷款-综合"},{"0010006","贷款-租赁"},{"0020007","赌-彩票预测"},{"0020008","赌-赌场系"},
            {"0020009","赌-购彩系"},{"0020010","赌-电子游戏"},{"0020011","赌-球"},{"0030012","黄-视频"},{"0030013","黄-成人用品用药"},{"0030014","黄-小说漫画"},{"0030015","黄-性感图"},{"0030016","黄-直播"},
            {"0030031","黄-外围"},{"0050017","宗教-场所"},{"0050018","宗教-机构"},{"0050019","宗教-文化"},{"0050020","宗教-用品"},{"0060021","VPN-非法"},{"0060022","VPN-商务"},{"0070000","分发平台"},
            {"0080000","打码"},{"0090000","VPS"},{"0100000","短链接"},{"0110000","配资"},{"0120000","四方支付"},{"0130000","云发卡"},{"0140000","流量刷单"},{"0150000","微交易"},{"0160000","云呼"},
            {"0200000","CDN"},{"0210000","第三方维护助手"},{"0220000","广告联盟"},{"0230000","代刷"},{"0240000","接码平台"},{"0250032","虚拟币交易"},{"0250033","游戏交易平台"},{"0250034","证券期货交易"},
            {"0250035","外汇交易"},{"0250036","购物网站"},{"0250037","红包返利"},{"0250038","充值返利"},{"0260000","客服"},{"0270000","网游加速器"},{"0280045","信用卡"},{"0280046","花呗白条"},
            {"0290039","机票退改签"},{"0300040","招聘网站"},{"0300041","兼职网赚"},{"0310042","婚恋交友"},{"0320043","公检法"},{"0320044","政府机关单位"},{"0330000","IDC服务商"},{"0340000","慈善捐款"},{"9990000","其它"}
        };
        public static Dictionary<string, string> WFDFraudCodeDict = new Dictionary<string, string>
        {
            {"0010001","贷款网站"},{"0010002","贷款网站"},{"0010003","贷款网站"},{"0010004","贷款网站"},{"0010005","贷款网站"},{"0010006","贷款网站"},{"0020007","博彩资讯网站"},{"0020008","博彩网站"},
            {"0020009","博彩网站"},{"0020010","博彩网站"},{"0020011","博彩网站"},{"0030012","涉黄-其他"},{"0030013","涉黄-其他"},{"0030014","涉黄-其他"},{"0030015","涉黄-其他"},{"0030031","涉黄-其他"},
            {"0030016","涉黄-直播"},{"0050017","涉诈无关"},{"0050018","涉诈无关"},{"0050019","涉诈无关"},{"0050020","涉诈无关"},{"0060021","涉诈无关"},{"0060022","涉诈无关"},{"0070000","涉诈无关"},
            {"0080000","涉诈无关"},{"0090000","涉诈无关"},{"0100000","涉诈无关"},{"0160000","涉诈无关"},{"0200000","涉诈无关"},{"0210000","涉诈无关"},{"0220000","涉诈无关"},{"0240000","涉诈无关"},{"0270000","涉诈无关"},
            {"0330000","涉诈无关"},{"0110000","投资理财网站"},{"0150000","投资理财网站"},{"0250032","投资理财网站"},{"0250034","投资理财网站"},{"0250035","投资理财网站"},{"0120000","四方支付"},{"0130000","刷单返利网站"},
            {"0140000","刷单返利网站"},{"0230000","代刷"},{"0250033","游戏交易网站"},{"0250036","购物网站"},{"0250037","红包返利"},{"0250038","充值返利"},{"0260000","客服"},{"0280045","代办信用卡"},{"0280046","提额套现"},
            {"0290039","机票退改签"},{"0300040","招聘网站"},{"0300041","兼职网赚"},{"0310042","婚恋交友"},{"0320043","公检法"},{"0320044","政府机关单位"},{"0340000","慈善捐款"},{"9990000","未知"}
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