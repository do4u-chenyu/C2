using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace C2.SearchToolkit
{
    public class SearchTaskInfo
    {
        public static Dictionary<String, String> TaskDescriptionTable = new Dictionary<String, String>
        {
            ["涉赌模型"] = "gamble",
            ["涉枪模型"] = "gun",
            ["涉黄模型"] = "yellow",
            ["飞机场模型"] = "plane"
        };

        private static Dictionary<String, String> taskScriptTable = new Dictionary<String, String>
        {
            ["涉赌模型"] = "batchquery_db_accountPass_C2_20210324_{0}.py",
            ["涉枪模型"] = "batchquery_db_accountPass_C2_20210324_{0}.py",
            ["涉黄模型"] = "batchquery_db_accountPass_C2_20210324_{0}.py",
            ["飞机场模型"] = "batchquery_db_accountPass_C2_20210324_{0}.py"
        };

        private static Dictionary<String, String> taskResultPatternTable = new Dictionary<String, String>
        {
            ["涉赌模型"] = @"([^\n\r]+000000_queryResult_db_\d+_\d+.tgz)",
            ["涉枪模型"] = @"([^\n\r]+000000_queryResult_db_\d+_\d+.tgz)",
            ["涉黄模型"] = @"([^\n\r]+000000_queryResult_db_\d+_\d+.tgz)",
            ["飞机场模型"] = @"([^\n\r]+000000_queryResult_db_\d+_\d+.tgz)"
        };

        public String LocalScriptPath
        {
            get
            {
                String s = taskScriptTable[TaskModel].Replace("_{0}", String.Empty);
                return Path.Combine(Application.StartupPath, "Resources", "Script", "IAO_Search_gamble", s);
            }
        }

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


        public String TargetScript { get => String.Format(taskScriptTable[TaskModel], TaskCreateTime); }
        public String TaskDirectory { get => String.Format("{0}/{1}_{2}", RemoteWorkspace, TaskName, TaskCreateTime); }

        public String TaskResultRegexPattern { get => taskResultPatternTable[TaskModel]; }


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
            "RemoteWorkspace"
        });

        public String Username { get; private set; }
        public String Password { get; private set; }
        public String BastionIP { get; private set; }
        public String SearchAgentIP { get; private set; }

        public String RemoteWorkspace { get; private set; }

        public String TaskModel { get; private set; }

        public String TaskStatus { get; set; } // 状态后期需要更新

        public String PID { get; set; } // PID要在远程实际创建后才有

        public String TaskName { get; private set; }

        public String TaskCreateTime { get; private set; }

        public String BastionInfo
        {
            get => String.Format("用户名:{0}, 堡垒机IP:{1}, 全文机IP:{2}, 结果目录:{3}/{4}_{5}",
                Username,
                BastionIP,
                SearchAgentIP,
                RemoteWorkspace,
                TaskName,
                TaskCreateTime);
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
                RemoteWorkspace
            });
        }

        private static String EncryptPassword(String password)
        {   // 颠倒，Base64编码，颠倒
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password.ReverseString())).ReverseString();
        }

        private static String DecryptPassword(String value)
        {   // 颠倒，Base64解码，颠倒
            return Encoding.UTF8.GetString(Convert.FromBase64String(value.ReverseString())).ReverseString();
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
                RemoteWorkspace = buf[9]
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
