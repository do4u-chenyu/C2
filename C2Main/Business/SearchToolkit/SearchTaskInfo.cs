using C2.Business.SSH;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace C2.SearchToolkit
{
    public enum SearchTaskMethod
    {
        QueryClient,
        DSQ
    }

    public class SearchTaskInfo
    {
        public static readonly Dictionary<String, String> TaskDescriptionTable = new Dictionary<String, String>
        {
            ["涉赌模型"] = "gamble",
            ["涉枪模型"] = "gun",
            ["涉黄模型"] = "yellow",
            ["飞机场模型"] = "plane",
            ["黑客模型"] = "hack",
            ["宝塔面板"] = "bt",
            ["apk模型"] = "apk",
            ["ddos模型"] = "ddos",
            ["xss模型"] = "xss",
            ["侵公模型"] = "qg",
            ["四方模型"] = "sf",
            ["秒播vps"] = "vps",
            //["测试模型"] = "test",
            ["购置境外网络资产模型"] = "email",
            ["PASS分析"] = "md5",
            ["黑吃黑模型"] = "hostDD",
            ["盗洞模型"] = "hackDD",
            ["大马模型"] = "dm",
            ["网赌受骗者模型"] = "dbqt",
            ["自定义查询"] = "custom",
            ["DSQ查询"] = "dsq",
        };

        private static readonly Dictionary<String, String> TaskScriptTable = new Dictionary<String, String>
        {
            ["涉赌模型"] = "batchquery_db_accountPass_C2_20210324_{0}.py",
            ["涉枪模型"] = "batchquery_gun_accountPass_C2_20200908_{0}.py",
            ["涉黄模型"] = "batchquery_yellow_accountPass_C2_eml_pic_web_20210414_{0}.py",
            ["飞机场模型"] = "batchquery_plane_accountPass_C2_20210414_{0}.py",
            ["黑客模型"] = "batchquery_hack_accountPass_C2_20210604_{0}.py",
            ["宝塔面板"] = "batchquery_hack_accountPass_C2_20210604_{0}.py",
            ["apk模型"] = "batchquery_hack_accountPass_C2_20210604_{0}.py",
            ["ddos模型"] = "batchquery_hack_accountPass_C2_20210604_{0}.py",
            ["xss模型"] = "batchquery_hack_accountPass_C2_20210604_{0}.py",
            ["侵公模型"] = "batchquery_hack_accountPass_C2_20210604_{0}.py",
            ["四方模型"] = "batchquery_hack_accountPass_C2_20210604_{0}.py",
            ["秒播vps"] = "batchquery_hack_accountPass_C2_20210604_{0}.py",
            //["测试模型"] = "batchquery_db_accountPass_C2_Test_Running_{0}.py",
            ["购置境外网络资产模型"] = "batchquery_email_accountPass_C2_20211111_{0}.py",
            ["PASS分析"] = "batchquery_code_accountPass_C2_20210624_{0}.py",
            ["黑吃黑模型"] = "batchquery_hostDD_accountPass_C2_20211126_{0}.py",
            ["盗洞模型"] = "batchquery_hackDD_accountPass_C2_20211126_{0}.py",
            ["大马模型"] = "batchquery_dm_accountPass_C2_20220107_{0}.py",
            ["网赌受骗者模型"] = "batchquery_dbqt_accountPass_C2_20220121_{0}.py",
            ["自定义查询"] = "batchquery_custom_accountPass_C2_20210831_{0}.py",
            ["DSQ查询"] = "main_rule_http_xxxx.py",
        };

        private static readonly Dictionary<String, String> TaskResultPatternTable = new Dictionary<String, String>
        {
            ["涉赌模型"] = @"([^\n\r]+000000_queryResult_db_\d+_\d+.tgz)",
            ["涉枪模型"] = @"([^\n\r]+000000_queryResult_gun_\d+_\d+.tgz)",
            ["涉黄模型"] = @"([^\n\r]+000000_queryResult_yellow_\d+_\d+.tgz)",
            ["飞机场模型"] = @"([^\n\r]+000000_queryResult_plane_\d+_\d+.tgz)",
            ["黑客模型"] = @"([^\n\r]+000000_queryResult_hack_\d+_\d+.tgz)",
            ["宝塔面板"] = @"([^\n\r]+000000_queryResult_bt_\d+_\d+.tgz)",
            ["apk模型"] = @"([^\n\r]+000000_queryResult_apk_\d+_\d+.tgz)",
            ["ddos模型"] = @"([^\n\r]+000000_queryResult_ddos_\d+_\d+.tgz)",
            ["xss模型"] = @"([^\n\r]+000000_queryResult_xss_\d+_\d+.tgz)",
            ["侵公模型"] = @"([^\n\r]+000000_queryResult_qg_\d+_\d+.tgz)",
            ["四方模型"] = @"([^\n\r]+000000_queryResult_sf_\d+_\d+.tgz)",
            ["秒播vps"] = @"([^\n\r]+000000_queryResult_vps_\d+_\d+.tgz)",
            //["测试模型"] = @"([^\n\r]+000000_queryResult_test_\d+_\d+.tgz)",
            ["PASS分析"] = @"([^\n\r]+000000_queryResult_code_\d+_\d+.tgz)",
            ["购置境外网络资产模型"] = @"([^\n\r]+000000_queryResult_email_\d+_\d+.tgz)",
            ["黑吃黑模型"] = @"([^\n\r]+000000_queryResult_hostDD_\d+_\d+.tgz)",
            ["盗洞模型"] = @"([^\n\r]+000000_queryResult_hackDD_\d+_\d+.tgz)",
            ["大马模型"] = @"([^\n\r]+000000_queryResult_dm_\d+_\d+.tgz)",
            ["网赌受骗者模型"] = @"([^\n\r]+000000_queryResult_dbqt_\d+_\d+.tgz)",
            ["自定义查询"] = @"([^\n\r]+000000_queryResult_custom_\d+_\d+.tgz)",
            ["DSQ查询"] = @"([^\n\r]+000000_queryResult_dsq_\d+_\d+.tgz)",
        };

        public static readonly Dictionary<String, String> TaskHelpInfoTable = new Dictionary<String, String>
        {
            ["涉赌模型"] = "JS的经典涉赌模型",
            ["涉枪模型"] = "JS的经典涉枪模型",
            ["涉黄模型"] = "JS的经典涉黄模型",
            ["飞机场模型"] = "私搭境外翻墙主机",
            ["黑客模型"] = "传统黑客模型",
            ["宝塔面板"] = "一键搭站,黑灰产领域的热门工具",
            ["apk模型"] = "侦测黑灰黄赌诈APK",
            ["ddos模型"] = "传统黑客模型:有DDOS攻击行为",
            ["xss模型"] = "传统黑客模型:有XSS攻击行为",
            ["侵公模型"] = "外泄公民个人信息",
            ["四方模型"] = "第四方支付,给黑灰产和上下游提供资金结算服务",
            ["秒播vps"] = "侦测非正规VPS",
            ["PASS分析"] = "内部测试用,忽略",
            ["购置境外网络资产模型"] = "购置境外域名,VPN,云主机,服务器和矿池用于黑灰产",
            ["黑吃黑模型"] = "新一代黑客专项模型:黑客和黑产之间的黑吃黑",
            ["盗洞模型"] = "新一代黑客专项模型:侦测木马打洞后的各种痕迹",
            ["大马模型"] = "新一代黑客专项模型:侦测高端黑客使用的大马",
            ["自定义查询"] = "上面详细设置里自己填查询关键词",
            ["DSQ查询"] = "内部施工中，忽略",
        };

        public static readonly List<string> DSQRelateModelList = new List<string>() { "DSQ查询" };

        public String LocalPyScriptPath
        {
            get
            {
                String s = TaskScriptTable[TaskModel].Replace("_{0}", String.Empty);
                return Path.Combine(Application.StartupPath, "Resources", "Script", "IAO_Search_gamble", s);
            }
        }
        private SearchTaskInfo() { }

        public String LocalPyZipPath { get => LocalPyScriptPath + ".zip"; }

        public String TaskResultShellPattern { get => "000000_queryResult_*_*_*.tgz"; }

        public static readonly String SearchWorkspace = @"/tmp/iao/search_toolkit/";

        public static readonly SearchTaskInfo EmptyTaskInfo = new SearchTaskInfo();


        public SearchTaskMethod SearchMethod { get => DSQRelateModelList.Contains(TaskModel) ? SearchTaskMethod.DSQ : SearchTaskMethod.QueryClient; }
        public List<string> DaemonIP { get; set; } = new List<string>();

        public String LastErrorMsg { get; set; } = String.Empty;

        public int LastErrorCode { get; set; } = BastionCodePage.Success;

        public bool IsEmpty() { return this == EmptyTaskInfo; }

        public String BcpFFP
        {
            get => Path.Combine(
                Global.SearchToolkitPath,
                String.Format("{0}_{1}_{2}.bcp", TaskName, PID, TaskCreateTime));
        }


        public String TargetScript { get => String.Format(TaskScriptTable[TaskModel], TaskCreateTime); }
        public String TaskDirectory { get => String.Format("{0}/{1}_{2}", RemoteWorkspace, TaskName, TaskCreateTime); }

        public String TaskResultRegexPattern { get => TaskResultPatternTable[TaskModel]; }


        private static readonly String HeadColumnLine = String.Join(OpUtil.TabSeparatorString, new string[] {
            "PID" ,
            "TaskName",
            "TaskCreateTime",
            "TaskModel",
            "TaskStatus",
            "Username",
            "Password",
            "BastionIP",
            "SearchAgentIP",
            "RemoteWorkspace",
            "InterfaceIP",
            "Settings.StartTime",
            "Settings.EndTime",
            "Settings.QueryStr",
            "SearchPassword"
        });

        public String Username { get; private set; }
        public String Password { get; private set; }
        public String BastionIP { get; private set; }
        public String SearchAgentIP { get; private set; }

        public String RemoteWorkspace { get; private set; }

        public String InterfaceIP { get; private set; }  // 这个是后期加的,为了兼容性只能追到屁股后面

        public String SearchPassword { get; private set; }  // 这个是后期加的,为了兼容性只能追到屁股后面

        public String TaskModel { get; private set; }

        public String TaskStatus { get; set; } // 状态后期需要更新

        public String PID { get; set; } // PID要在远程实际创建后才有

        public String TaskName { get; private set; }

        public String TaskCreateTime { get; private set; }

        public String BastionInfo
        {
            get => String.Format("用户名:{0}, 堡垒机IP:{1}, 全文机IP:{2}, 界面机{6} 结果目录:{3}/{4}_{5}",
                Username,
                BastionIP,
                SearchAgentIP,
                RemoteWorkspace,
                TaskName,
                TaskCreateTime,
                InterfaceIP);
        }

        public SearchModelSettingsInfo Settings { get; private set; } = new SearchModelSettingsInfo();


        public override String ToString()
        {
            return String.Format("{0}{1}{2}", HeadColumnLine, OpUtil.LineSeparator, ContentLine());
        }

        private String ContentLine()
        {
            return String.Join("\t", new string[] {
                PID ,
                TaskName,
                TaskCreateTime,
                TaskModel,
                TaskStatus,
                Username,
                EncryptPassword(Password),     // 密码要加密保存
                BastionIP,
                SearchAgentIP,
                RemoteWorkspace,
                InterfaceIP,
                Settings.StartTime,
                Settings.EndTime,
                Settings.QueryStr,
                EncryptPassword(SearchPassword) // 密码要加密保存
            });
        }

        private static String EncryptPassword(String password)
        {   // 颠倒，Base64编码，颠倒
            return ST.EncodeBase64(password.ReverseString()).ReverseString();
        }

        private static String DecryptPassword(String password)
        {   // 颠倒，Base64解码，颠倒
            return ST.DecodeBase64(password.ReverseString()).ReverseString();
        }

        public static SearchTaskInfo StringToTaskInfo(String content, bool needDecryptPass = false)
        {
            if (String.IsNullOrEmpty(content))
                return SearchTaskInfo.EmptyTaskInfo;

            // 有表头的话 取第二行
            String[] buf = content.Split(OpUtil.LineSeparator);
            content = buf.Length == 1 ? buf[0].TrimEnd() : buf[1].TrimEnd();

            // 小于10列不处理
            buf = content.Split(OpUtil.TabSeparator);
            if (buf.Length < 10)
                return SearchTaskInfo.EmptyTaskInfo;

            for (int i = 0; i < buf.Length; i++)
                buf[i] = buf[i].Trim();

            SearchTaskInfo taskInfo = new SearchTaskInfo()
            {
                PID      = buf[0],
                TaskName = buf[1],
                TaskCreateTime = buf[2],
                TaskModel  = buf[3],
                TaskStatus = buf[4],
                Username   = buf[5],
                Password   = needDecryptPass ? DecryptPassword(buf[6]) : buf[6],  // 堡垒机密码加密保存,反序列化时解密
                BastionIP  = buf[7],
                SearchAgentIP   = buf[8],
                RemoteWorkspace = buf[9],
                InterfaceIP = buf.Length < 11 ? String.Empty : buf[10],           // 兼容早期版本
                Settings    = buf.Length < 13 ? new SearchModelSettingsInfo() : 
                              buf.Length < 14 ? new SearchModelSettingsInfo(buf[11], buf[12]) : new SearchModelSettingsInfo(buf[11], buf[12], buf[13]),
                SearchPassword = buf.Length < 15 ? String.Empty : 
                                 needDecryptPass ? DecryptPassword(buf[14]) : buf[14]        // 兼容早期版本
            };
            return taskInfo;
        }

        public bool Save()
        {
            try
            {
                String path = Path.GetDirectoryName(BcpFFP);
                if (!Directory.Exists(path))
                    FileUtil.CreateDirectory(path);

                using (StreamWriter sw = new StreamWriter(BcpFFP))
                    sw.WriteLine(ToString());
            }
            catch (Exception ex)
            {
                LastErrorMsg = ex.Message;
                return false;
            }
            return true;
        }

    }
}
