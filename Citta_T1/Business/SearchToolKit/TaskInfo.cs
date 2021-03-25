using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.SearchToolkit
{
    public class TaskInfo
    {
        public static readonly String GambelModelDescription = "胶水系统全文涉赌后台模型";
        public static readonly String GunModelDescription = "胶水系统全文涉枪模型";
        public static readonly String YellowModelDescription = "胶水系统全文涉黄模型";
        public static readonly String PlaneModelDescription = "胶水系统飞机场模型";


        public static readonly TaskInfo EmptyTaskInfo = new TaskInfo();

        private static readonly String HeadColumnLine = String.Join("\t", new string[] {
            "TaskID" ,
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

        public String TaskStatus { get; private set; }

        public String TaskID { get; private set; }

        public String TaskName { get; private set; }

        public String TaskCreateTime { get; private set; }

        public override String ToString()
        {
            return String.Format("{0}{1}{2}", HeadColumnLine, OpUtil.DefaultLineSeparator, ContentLine());
        }

        private String ContentLine()
        {
            return String.Join("\t", new string[] {
                TaskID ,
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

        private String EncryptPassword(String password)
        {   // 颠倒，Base64编码，颠倒
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password.ReverseString())).ReverseString();
        }

        private String DecryptPassword(String value)
        {   // 颠倒，Base64解码，颠倒
            return Encoding.UTF8.GetString(Convert.FromBase64String(value.ReverseString())).ReverseString();
        }

        public static TaskInfo GenTaskInfo(String line)
        {
            return TaskInfo.EmptyTaskInfo;
        }

    }
}
