using C2.Core;
using C2.SearchToolkit;
using C2.Utils;
using Renci.SshNet;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace C2.Business.SSH
{
    public class BastionAPI
    {
        private static readonly TimeSpan DefaultTimeout = new TimeSpan(0, 0, 10);
        private static readonly String TgzHead = Encoding.ASCII.GetString(new byte[] { 0x1f, 0x8b, 0x08 }); // 1f 8b 08 .tgz的文件头

        private readonly TaskInfo task;
        private readonly SshClient ssh;
        private readonly ShellStream shell;

        private String TargetGambleScript { get => String.Format("batchquery_db_accountPass_C2_20210324_{0}.py", task.TaskCreateTime); }
        // {workspace}/pid_taskcreatetime
        private String GambleWorkspace { get => String.Format("{0}/{1}_{2}", task.RemoteWorkspace, task.PID, task.TaskCreateTime); }
        public BastionAPI(TaskInfo task)
        {
            this.task = task;
            this.ssh = new SshClient(new PasswordConnectionInfo("114.55.248.85", "root", "aliyun.123"));
            
            //this.ssh = new SshClient(new PasswordConnectionInfo("10.1.126.4", "root", "iao123456"));
        }

        public BastionAPI Login()
        {
            try
            {
                ssh.ConnectionInfo.Timeout = new TimeSpan(0, 0, 10); // 10秒超时
                ssh.Connect();
            }
            catch (Exception ex)
            {
                task.LastErrorMsg = String.Format("登陆【{0}】失败:{1}", ssh.ConnectionInfo.Host, ex.Message);
            }
            
            return this;
        }

        private String RunCommand(String command)
        {
            return ssh.IsConnected ? ssh.RunCommand(command).Result : String.Empty;
        }

        private String RunCommand(String command, ShellStream shell)
        {
            if (!ssh.IsConnected)
                return String.Empty;
            shell.Read();//TODO 需要一个清缓存的函数
            shell.WriteLine(command);
            return String.Empty;
        }

        // 执行命令且必须成功返回
        private bool SuccessRunCommand(String command)
        {
            return ssh.IsConnected && ssh.RunCommand(command).ExitStatus == 0;
        }

        private bool RunCommandBackground(String command)
        {
            if (!ssh.IsConnected)
                return false;

            using (ShellStream ss = ssh.CreateShellStream(String.Empty, 0, 0, 0, 0, 4096))
            {
                int tryCount = 0;
                while (!ss.CanWrite && tryCount++ < 3)
                    System.Threading.Thread.Sleep(1000);  // 等待 ssh 创建好 shell
                if (!ss.CanWrite)                         // 尝试N次失败,退出
                    return false;

                ss.WriteLine(String.Format("{0} & disown -a", command));
            }

            return true;
        }

        private String GetRemoteFilename(String s)
        {
           
            String command = String.Format("ls -l {0} | awk '{{print $9}}' | tail -n 1", s);
            return RunCommand(command).Trim();
        }
        private int GetRemoteFileSize(String s)
        {
            String command = String.Format("ls -l {0} | awk '{{print $5}}' | head -n 1", s);
            String result = RunCommand(command).Trim();
            return ConvertUtil.TryParseInt(result);
        }

        public bool DownloadGambleTaskResult(String d)
        {
            // 000000_queryResult_db_开始时间_结束时间.tgz
            String s = GambleWorkspace + "/000000_queryResult_db_*_*.tgz";

            if (!ssh.IsConnected)
                return false;

            String ffp = GetRemoteFilename(s);
            int size = GetRemoteFileSize(ffp);
            if (size <= 0)
                return false;  // 文件不存在或空文件
        



            return true;
        }

        public BastionAPI UploadGambleScript()
        {
            if (!ssh.IsConnected)
                return this;

            String s = Global.GambleScriptPath;
            String content = FileUtil.FileReadToEnd(s);
            if (String.IsNullOrEmpty(content) || !IsShellCmdSafe(content))
                return this;


            // 1)  \\ \a \b \c \e \f \n \r \t 等转义字符的\全部替换成\\\
            // 2)  " 替换成 \"
            // 3)  换行回车替换成转义字符
            // 4)  这里的逻辑需要优化,个人感觉当前的实现有隐患
            //     转义字符的安全性，效率，形式美感上都很差
            //     尤其是转义字符，如果目标脚本有rm动作, 转义字符在处理\, /, 空格等符号时如果出问题
            //     运气不好会造成删库
            content = Regex.Replace(content, @"\\([\\abcefnrtvx0])", @"\\\$1")
                           .Replace("\"", "\\\"")
                           .Replace("\r", @"\r")
                           .Replace("\n", @"\n");

            String d = GambleWorkspace + "/" + TargetGambleScript;
            // 这里可能还有超出shell缓冲区的问题
            String command = String.Format("echo -e \"{0}\" > {1}", content, d);
            SuccessRunCommand(command);
            return this;
        }

        private bool IsShellCmdSafe(String content)
        {
            // 释义字符会报错
            return content.IndexOfAny("`".ToCharArray()) == -1;
        }

        public String RunGambleTask()
        {
            if (!EnterGambleWorkspace())
                return String.Empty;

            //String command = String.Format("python {0}", TargetGambleScript);
            String command = "sleep 300";

            String pid = RunCommandBackground(command) ? GetPID(command) : String.Empty;
            // 未获取到pid，当作模型脚本执行失败
            if (String.IsNullOrEmpty(pid))
                task.LastErrorMsg = "全文机已连接但执行涉赌脚本失败";
            return pid;
        }

        public BastionAPI DeleteGambleTaskWorkspace()
        {
            // 删除 临时目录
            if (IsSafe(GambleWorkspace))
                RunCommand(String.Format("rm -rf {0};", GambleWorkspace));

            return this;
        }

        private bool IsAliveGambleTask()
        {
            String result = RunCommand(String.Format("ps -q {0} -o cmd | grep {1}", task.PID, TargetGambleScript));
            return result.Contains(TargetGambleScript);
        }

        private bool IsGambleResultFileReady()
        {
            String ffp = GambleWorkspace + "/000000_queryResult_db_*_*.tgz";
            String command = String.Format("ls {0}", ffp);
            return SuccessRunCommand(command);
        }

        private bool IsTaskTimeout()
        {
            DateTime born = ConvertUtil.TryParseDateTime(task.TaskCreateTime, "yyyyMMddHHmmss");
            TimeSpan ts = DateTime.Now.Subtract(born);
            return Math.Abs(ts.TotalHours) >= 24 * 3;
        }

        private bool IsSafe(String v)
        {
            // 在服务器上删东西 尽量严格, 尤其不能有"空格/"或"空格/空格"
            return v.StartsWith("/tmp/iao/search_toolkit/") && !Regex.IsMatch(v, @"\s");
        }

        private bool EnterGambleWorkspace()
        {
            String command = String.Format("cd {0}", GambleWorkspace);
            return SuccessRunCommand(command);
        }

        public BastionAPI KillGambleTask()
        {
            if (IsAliveGambleTask()) // 确保不要误删其他复用进程
            {
                String command = String.Format("kill -9 {0}", task.PID);
                RunCommand(command);
            }
            return this;
        }


        public BastionAPI CreateGambleTaskDirectory()
        {
            String command = String.Format("mkdir -p {0}", GambleWorkspace);
            RunCommand(command);
            return this;
        }

        public String QueryGambleTaskStatus()
        {
            bool isTimeout = IsTaskTimeout();
            bool isAlive = IsAliveGambleTask();
            bool isGRFReady = IsGambleResultFileReady();

            if (!ssh.IsConnected)
                return "连接失败";

            // 1) pid不存在且有结果文件时, 为运行成功
            if (!isAlive && isGRFReady)
                return "DONE";

            // 2) pid不存在且没有结果文件时, 为运行失败
            if (!isAlive && !isGRFReady)
                return "FAIL";

            // 3) pid存在且没有结果文件且在未超时范围内, 为正在运行
            if (isAlive && !isGRFReady && !isTimeout)
                return "RUNNING";

            // 4) pid存在且没有结果文件且超出运行时间(24 * 4小时), 为超时
            if (isAlive && !isGRFReady && isTimeout)
                return "TIMEOUT";

            // 5) pid存在但有结果文件, 这种情况按道理不应该发生, 暂时假定运行成功
            if (isAlive && isGRFReady)
                return "DONE";

            // 其他情况, 按道理不应该发生, 全部默认为失败
            return "FAIL";
        }

        public String GetPID(String cmdLine)
        {
            String command = String.Format(@"pgrep -f '{0}' | head -n 1", cmdLine);
            String result = RunCommand(command);
            return Regex.IsMatch(result, @"^\d+$") ? result.Trim() : String.Empty;
        }
    }
}
