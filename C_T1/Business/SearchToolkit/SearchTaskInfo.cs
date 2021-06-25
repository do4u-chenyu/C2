using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace C2.SearchToolkit
{
    public class SearchTaskInfo
    {
        public static readonly Dictionary<String, String> TaskDescriptionTable = new Dictionary<String, String>
        {
            ["涉赌模型"] = "gamble",
            ["涉枪模型"] = "gun",
            ["涉黄模型"] = "yellow",
            ["飞机场模型"] = "plane",
            ["黑客模型"] = "hack",
            ["宝塔面板模型"] = "btmb",
            ["应用分发模型"] = "yyff",
            ["ddos模型"] = "ddos",
            ["xss模型"] = "xss",
            ["侵公模型"] = "qg",
            ["四方模型"] = "sf",
            ["秒播vps模型"] = "vps",
            ["测试模型"] = "test",
            ["密码模型"] = "code"
        };

        private static readonly Dictionary<String, String> TaskScriptTable = new Dictionary<String, String>
        {
            ["涉赌模型"] = "batchquery_db_accountPass_C2_20210324_{0}.py",
            ["涉枪模型"] = "batchquery_gun_accountPass_C2_20200908_{0}.py",
            ["涉黄模型"] = "batchquery_yellow_accountPass_C2_eml_pic_web_20210414_{0}.py",
            ["飞机场模型"] = "batchquery_plane_accountPass_C2_20210414_{0}.py",
            ["黑客模型"] = "batchquery_hack_accountPass_C2_20210604_{0}.py",
            ["宝塔面板模型"] = "batchquery_hack_accountPass_C2_20210604_{0}.py",
            ["应用分发模型"] = "batchquery_hack_accountPass_C2_20210604_{0}.py",
            ["ddos模型"] = "batchquery_hack_accountPass_C2_20210604_{0}.py",
            ["xss模型"] = "batchquery_hack_accountPass_C2_20210604_{0}.py",
            ["侵公模型"] = "batchquery_hack_accountPass_C2_20210604_{0}.py",
            ["四方模型"] = "batchquery_hack_accountPass_C2_20210604_{0}.py",
            ["秒播vps模型"] = "batchquery_hack_accountPass_C2_20210604_{0}.py",
            ["测试模型"] = "batchquery_db_accountPass_C2_Test_Running_{0}.py",
            ["密码模型"] = "batchquery_code_accountPass_C2_20210624_{0}.py",
            
        };

        private static readonly Dictionary<String, String> TaskResultPatternTable = new Dictionary<String, String>
        {
            ["涉赌模型"] = @"([^\n\r]+000000_queryResult_db_\d+_\d+.tgz)",
            ["涉枪模型"] = @"([^\n\r]+000000_queryResult_gun_\d+_\d+.tgz)",
            ["涉黄模型"] = @"([^\n\r]+000000_queryResult_yellow_\d+_\d+.tgz)",
            ["飞机场模型"] = @"([^\n\r]+000000_queryResult_plane_\d+_\d+.tgz)",
            ["黑客模型"] = @"([^\n\r]+000000_queryResult_hack_\d+_\d+.tgz)",
            ["宝塔面板模型"] = @"([^\n\r]+000000_queryResult_hack_\d+_\d+.tgz)",
            ["应用分发模型"] = @"([^\n\r]+000000_queryResult_hack_\d+_\d+.tgz)",
            ["ddos模型"] = @"([^\n\r]+000000_queryResult_hack_\d+_\d+.tgz)",
            ["xss模型"] = @"([^\n\r]+000000_queryResult_hack_\d+_\d+.tgz)",
            ["侵公模型"] = @"([^\n\r]+000000_queryResult_hack_\d+_\d+.tgz)",
            ["四方模型"] = @"([^\n\r]+000000_queryResult_hack_\d+_\d+.tgz)",
            ["秒播vps模型"] = @"([^\n\r]+000000_queryResult_hack_\d+_\d+.tgz)",
            ["测试模型"] = @"([^\n\r]+000000_queryResult_test_\d+_\d+.tgz)",
            ["密码模型"] = @"([^\n\r]+000000_queryResult_code_\d+_\d+.tgz)",
        };

        public String LocalPyScriptPath
        {
            get
            {
                String s = TaskScriptTable[TaskModel].Replace("_{0}", String.Empty);
                return Path.Combine(Application.StartupPath, "Resources", "Script", "IAO_Search_gamble", s);
            }
        }

        public String LocalPyZipPath { get => LocalPyScriptPath + ".zip"; }

        public String TaskResultShellPattern { get => "000000_queryResult_*_*_*.tgz"; }

        public static readonly String SearchWorkspace = @"/tmp/iao/search_toolkit/";

        public static readonly SearchTaskInfo EmptyTaskInfo = new SearchTaskInfo();



        public String LastErrorMsg { get; set; } = String.Empty;

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
            "InterfaceIP"
        });

        public String Username { get; private set; }
        public String Password { get; private set; }
        public String BastionIP { get; private set; }
        public String SearchAgentIP { get; private set; }

        public String RemoteWorkspace { get; private set; }

        public String InterfaceIP { get; private set; }  // 这个是后期加的,为了兼容性只能追到屁股后面

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
                EncryptPassword(Password),  // 密码要加密保存
                BastionIP,
                SearchAgentIP,
                RemoteWorkspace,
                InterfaceIP
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

            SearchTaskInfo taskInfo = new SearchTaskInfo()
            {
                PID = buf[0],
                TaskName = buf[1],
                TaskCreateTime = buf[2],
                TaskModel = buf[3],
                TaskStatus = buf[4],
                Username = buf[5],
                Password = needDecryptPass ? DecryptPassword(buf[6]) : buf[6],  // 堡垒机密码加密保存,反序列化时解密
                BastionIP = buf[7],
                SearchAgentIP = buf[8],
                RemoteWorkspace = buf[9],
                InterfaceIP = buf.Length < 11 ? String.Empty : buf[10]  // 兼容早期版本
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
