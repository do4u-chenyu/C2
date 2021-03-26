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

        public String LastErrorMsg { get; set; } = String.Empty;

        public bool IsEmpty() { return this == EmptyTaskInfo; }

        public String BcpFilename { get => String.Format("{0}_{1}_{2}.bcp", TaskName, TaskID, TaskCreateTime); }

        private static readonly String HeadColumnLine = String.Join(OpUtil.TabSeparatorString, new string[] {
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

        public String TaskID { get; set; }

        public String TaskName { get; private set; }

        public String TaskCreateTime { get; private set; }

        public String BastionInfo 
        { 
            get => String.Format("用户名:{0}, 堡垒机IP:{1}, 全文机IP:{2}, 结果目录:{3}", 
                Username, 
                BastionIP, 
                SearchAgentIP,
                RemoteWorkspace); 
        }
        

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

        private static String EncryptPassword(String password)
        {   // 颠倒，Base64编码，颠倒
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password.ReverseString())).ReverseString();
        }

        private static String DecryptPassword(String value)
        {   // 颠倒，Base64解码，颠倒
            return Encoding.UTF8.GetString(Convert.FromBase64String(value.ReverseString())).ReverseString();
        }

        public static TaskInfo StringToTaskInfo(String content, bool needDecryptPass = false)
        {
            if (String.IsNullOrEmpty(content))
                return TaskInfo.EmptyTaskInfo;

            // 有表头的话 取第二行
            String[] buf = content.Split(OpUtil.DefaultLineSeparator);
            content = buf.Length == 1 ? buf[0].TrimEnd() : buf[1].TrimEnd();

            // 小于10列不处理
            buf = content.Split(OpUtil.TabSeparator);
            if (buf.Length < 10)
                return TaskInfo.EmptyTaskInfo;

            TaskInfo taskInfo = new TaskInfo()
            {
                TaskID = buf[0],
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

    }
}
