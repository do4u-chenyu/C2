using C2.Core;
using C2.SearchToolkit;
using C2.Utils;
using Renci.SshNet;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace C2.Business.SSH
{
    public class BastionAPI
    {
        private const int SecondsTimeout = 10;
        private const String Separator = "5L2Z55Sf5aaC5LiH5Y+k6ZW/5aSc";

        private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(SecondsTimeout);
        private static readonly String TgzHead = Encoding.ASCII.GetString(new byte[] { 0x1f, 0x8b, 0x08 }); // 1f 8b 08 .tgz的文件头

        private readonly TaskInfo task;

        private SshClient ssh;
        private ShellStream shell;

        private String TargetGambleScript { get => String.Format("batchquery_db_accountPass_C2_20210324_{0}.py", task.TaskCreateTime); }
        // {workspace}/pid_taskcreatetime
        private String GambleTaskDirectory { get => String.Format("{0}/{1}_{2}", task.RemoteWorkspace, task.PID, task.TaskCreateTime); }
        public BastionAPI(TaskInfo task)
        {
            this.task = task;
            this.ssh = new SshClient("114.55.248.85", "root", "aliyun.123");
            this.ssh.ConnectionInfo.Timeout = Timeout; // 10秒超时
        }

        public BastionAPI Login()
        {
            try
            {
                ssh.Connect();
                Jump();
            }
            catch (Exception ex)
            {
                task.LastErrorMsg = String.Format("登陆【{0}】失败:{1}", ssh.ConnectionInfo.Host, ex.Message);
            }
            
            return this;
        }
        private void Jump()
        {
            shell = ssh.CreateShellStream(String.Empty, 0, 0, 0, 0, 4096);
            shell.ReadTimeout = shell.WriteTimeout = (int)Timeout.TotalMilliseconds;
            
            task.LastErrorMsg = String.Format("登陆【{0}】失败:{1}", ssh.ConnectionInfo.Host, "未能跳转全文机");

            // 等待Shell环境准备好
            for (int i = 0; i < 3 && !shell.CanWrite; i++)
                Thread.Sleep(500);

            // 确认进入烽火堡垒机欢迎标题
            if (null == shell.Expect(new Regex("###[^#]+###"), Timeout))
                return;

            // 跳转到目标机器
            shell.WriteLine(task.SearchAgentIP);

            // 等待跳转成功
            if (null == shell.Expect(new Regex(@"\[root@[^\]]+\]#"), Timeout))
                return;

            task.LastErrorMsg = String.Empty;
            shell.Read(); // 清空buffer
        }


        private String RunCommand(String command, ShellStream ssm, int timeout = SecondsTimeout)
             
        {
            try 
            {
                // 清理缓存
                ssm.Read();
                // 执行命令
                ssm.WriteLine(command);
                // 打印分隔符
                ssm.WriteLine(String.Format("echo {0}", Separator));
                // 根据分隔符和timeout确定任务输出结束
                String ret = ssm.Expect(Separator, TimeSpan.FromSeconds(timeout));
                if (ret != null)
                    return ret.Replace(Separator, String.Empty).TrimEnd();
            } catch { }

            return String.Empty;
        }


        private String GetRemoteFilename(String s)
        {
            String command = String.Format("ls -l {0} | awk '{{print $9}}' | tail -n 1", s);
            return RunCommand(command, shell).Trim();
        }
        private int GetRemoteFileSize(String s)
        {
            String command = String.Format("ls -l {0} | awk '{{print $5}}' | head -n 1", s);
            String result = RunCommand(command, shell).Trim();
            return ConvertUtil.TryParseInt(result);
        }

        public bool DownloadGambleTaskResult(String d)
        {
            // 000000_queryResult_db_开始时间_结束时间.tgz
            String s = GambleTaskDirectory + "/000000_queryResult_db_*_*.tgz";

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
            if (!ssh.IsConnected || !task.LastErrorMsg.IsEmpty())
                return this;

            String s = Global.GambleScriptPath;
            String content = FileUtil.FileReadToEnd(s);
            if (String.IsNullOrEmpty(content) || content.Contains("`")) // 不能有释义字符
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

            String d = GambleTaskDirectory + "/" + TargetGambleScript;
            // 这里可能还有超出shell缓冲区的问题
            String command = String.Format("echo -e \"{0}\" > {1}", content, d);
            RunCommand(command, shell);
            return this;
        }

        public String GetPID(String cmdLine)
        {
            String command = String.Format(@"pgrep -f '{0}' | head -n 1", cmdLine);
            String result = RunCommand(command, shell);
            return Regex.IsMatch(result, @"^\d+$") ? result.Trim() : String.Empty;
        }

        public String RunGambleTask()
        {
            if (!ssh.IsConnected || !task.LastErrorMsg.IsEmpty())
                return String.Empty;

            EnterGambleTaskDirectory();
            //String command = String.Format("python {0}", TargetGambleScript);
            String command = "sleep 300";
            shell.WriteLine(String.Format("{0} & disown -a", command));

            String pid = GetPID(command);
            // 未获取到pid，当作模型脚本执行失败
            if (pid.IsEmpty())
                task.LastErrorMsg = "全文机已连接但执行涉赌脚本失败";
            return pid;
        }

        private void EnterGambleTaskDirectory()
        {
            String command = String.Format("cd {0}", GambleTaskDirectory);
            RunCommand(command, shell);
        }

        public BastionAPI DeleteGambleTaskDirectory()
        {
            if (!ssh.IsConnected || !task.LastErrorMsg.IsEmpty())
                return this;
            // 删除 临时目录
            if (IsSafe(GambleTaskDirectory))
                RunCommand(String.Format("rm -rf {0};", GambleTaskDirectory), shell);
            return this;
        }

        public BastionAPI CreateGambleTaskDirectory()
        {
            if (!ssh.IsConnected || !task.LastErrorMsg.IsEmpty())
                return this;
            String command = String.Format("mkdir -p {0}", GambleTaskDirectory);
            RunCommand(command, shell);
            return this;
        }


        private bool IsAliveGambleTask()
        {
            String result = RunCommand(String.Format("ps -q {0} -o cmd | grep {1}", task.PID, TargetGambleScript), shell);
            return result.Contains(TargetGambleScript);
        }

        private bool IsGambleResultFileReady()
        {
            String ffp = GambleTaskDirectory + "/000000_queryResult_db_*_*.tgz";
            String command = String.Format("ls {0}", ffp);
            return RunCommand(command, shell).Contains("000000_queryResult_db_");
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

        public BastionAPI KillGambleTask()
        {
            if (IsAliveGambleTask()) // 确保不要误删其他复用进程
            {
                String command = String.Format("kill -9 {0}", task.PID);
                RunCommand(command, shell);
            }
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

    }
}
