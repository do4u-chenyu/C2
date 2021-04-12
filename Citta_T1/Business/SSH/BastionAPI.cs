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
        private const string CrLf = "\r\n";
        private const int SecondsTimeout = 7;
        private const String SeparatorString = "5L2Z55Sf5aaC5LiH5Y+k6ZW/5aSc";
        
        private static readonly Regex SeparatorRegex = new Regex(Wrap(Regex.Escape(SeparatorString)));
        private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(SecondsTimeout);
        
        private static readonly String TgzHead = Encoding.UTF8.GetString(new byte[] { 0x1f, 0x8b, 0x08 }); // 1f 8b 08 .tgz的文件头

        private readonly TaskInfo task;

        private SshClient ssh;
        private ShellStream shell;

        private String TargetGambleScript { get => String.Format("batchquery_db_accountPass_C2_20210324_{0}.py", task.TaskCreateTime); }
        // {workspace}/pid_taskcreatetime
        private String GambleTaskDirectory { get => String.Format("{0}/{1}_{2}", task.RemoteWorkspace, task.TaskName, task.TaskCreateTime); }

        private static String Wrap(String pattern)
        {
            return String.Format(@"\r?\n{0}\r?\n", pattern);
        }
        public BastionAPI(TaskInfo task)
        {
            this.task = task;
            this.ssh = new SshClient(task.BastionIP, task.Username, task.Password);
            //this.ssh = new SshClient("114.55.248.85", "root", "aliyun.123");
            this.ssh.ConnectionInfo.Timeout = Timeout; 
        }

        public BastionAPI Login()
        {
            try 
            { 
                ssh.Connect(); 
            }   
            catch (Exception ex) 
            {
                task.LastErrorMsg = String.Format("登陆【{0}】失败:{1}", ssh.ConnectionInfo.Host, ex.Message);
            }
     
            try 
            { 
                Jump(); 
            }
            catch (Exception ex)
            {
                task.LastErrorMsg = String.Format("登陆【{0}】成功，跳转全文机【{1}】失败：{2}",
                    ssh.ConnectionInfo.Host, 
                    task.SearchAgentIP, 
                    ex.Message);
            }
                
            return this;
        }
        private void Jump()
        {
            if (IsError())
                return;

            task.LastErrorMsg = String.Format("登陆堡垒机【{0}】成功，但未能跳转全文机【{1}】", task.BastionIP, task.SearchAgentIP);
            shell = ssh.CreateShellStream(String.Empty, 0, 0, 0, 0, 4096);
            // 等待目标机准备好
            _ = shell.ReadLine(Timeout);

            // 跳转到目标机器
            shell.WriteLine(task.SearchAgentIP);
            // 等待跳转成功,出现root用户提示符
            if (null == shell.Expect(new Regex(@"\[root@[^\]]+\]#"), Timeout))
                return;
            task.LastErrorMsg = String.Empty; 
            _ = shell.Read(); // 清空buffer
        }

        
        private String RunCommand(String command, ShellStream ssm, int timeout = SecondsTimeout)   
        {
            try 
            {
                // 清理缓存
                _ = ssm.Read();
                // 执行命令
                ssm.WriteLine(command);
                // 打印分隔符
                ssm.WriteLine(String.Format("echo {0}", SeparatorString));
                // 根据分隔符和timeout确定任务输出结束
                String ret = ssm.Expect(SeparatorRegex, TimeSpan.FromSeconds(timeout));
                if (ret != null)
                    return ret;
            } catch { }

            return String.Empty;
        }

        private bool Cat(String ffp, String dst, long len, ShellStream ssm)
        {
            try
            {
                // 清理缓存
                _ = ssm.Read();
                // 执行命令
                ssm.WriteLine(String.Format("cat {0}", ffp));
                // 打印分隔符
                ssm.WriteLine(String.Format("echo {0}", SeparatorString));

                String begin = ssm.Expect(new Regex(TgzHead), Timeout);  
                if (null == begin)
                {
                    task.LastErrorMsg = String.Format("任务【{0}】下载失败;文件格式损坏", task.TaskName);
                    return false;
                }

                long bytesLeft = Math.Max(len - TgzHead.Length, 0); // 去掉文件头
                while(bytesLeft > 0)
                {
                    // TODO 处理\r\n => \n问题 还是要手工写read函数
                    String line = ssm.ReadLine(Timeout);
                    
                    if (null == line) // 超时
                        break;

                    long bytesRead = Math.Min(line.Length + CrLf.Length, bytesLeft);
                    bytesLeft = bytesLeft - bytesRead;

                    System.Console.WriteLine(line.ToCharArray());
                    System.Console.WriteLine(Encoding.UTF8.GetBytes(line));
                    System.Console.WriteLine(Encoding.Unicode.GetBytes(line));
                    // TODO write
                }
                // TODO 校验

            }
            catch { }

            return false;
        }

        private String GetRemoteFilename(String s)
        {
            String command = String.Format("ls -l {0} | awk '{{print $9}}' | head -n 1", s);
            String content = RunCommand(command, shell);

            Match mat = Regex.Match(content, Wrap(@"([^\n\r]+000000_queryResult_db_\d+_\d+.tgz)"));
            if (mat.Success && mat.Groups[1].Success)
                return mat.Groups[1].Value;
            return String.Empty;
        }

        private long GetRemoteFileSize(String s)
        {
            String command = String.Format("ls -l {0} | awk '{{print $5}}' | head -n 1", s);
            String content = RunCommand(command, shell);

            Match mat = Regex.Match(content, Wrap(@"(\d+)")); 
            if (mat.Success && mat.Groups[1].Success)
                return ConvertUtil.TryParseLong(mat.Groups[1].Value);
            return 0;
        }

        public bool DownloadGambleTaskResult(String d)
        {
            // 000000_queryResult_db_开始时间_结束时间.tgz
            String s = GambleTaskDirectory + "/000000_queryResult_db_*_*.tgz";

            if (IsError())
                return false;

            String ffp = GetRemoteFilename(s);
            long len = GetRemoteFileSize(ffp);  // 文件有可能超过2G,不能用int
            if (len <= 0)
                return false;  // 文件不存在或空文件  
            
            // 已Cat的方式下载文件， 根据lxf的情报, 40M为中位数
            return Cat(ffp, d, len, shell);
        }

        private bool IsError()
        {
            return !(ssh.IsConnected && task.LastErrorMsg.IsEmpty());
        }

        public BastionAPI UploadGambleScript()
        {
            if (IsError())
                return this;

            String s = Global.GambleScriptPath;
            String content = FileUtil.FileReadToEnd(s);


            // 0)  不能有释义字符 `
            // 1)  \\ \a \b \c \e \f \n \r \t 等转义字符的\全部替换成\\\
            // 2)  " 替换成 \"
            // 3)  换行回车替换成转义字符
            // 4)  这里的逻辑需要优化,个人感觉当前的实现有隐患, TODO 根据lxf的建议改成Base64编码
            //     转义字符的安全性，效率，形式美感上都很差
            //     尤其是转义字符，如果目标脚本有rm动作, 转义字符在处理\, /, 空格等符号时如果出问题
            //     运气不好会造成删库
            if (String.IsNullOrEmpty(content) || content.Contains("`"))
                return this;

            content = Regex.Replace(content, @"\\([\\abcefnrtvx0])", @"\\\$1")
                           .Replace("\"", "\\\"")
                           .Replace("\r", @"\r")
                           .Replace("\n", @"\n");

            String d = GambleTaskDirectory + "/" + TargetGambleScript;
            // 这里可能还有超出shell缓冲区的问题
            String command = String.Format("echo -e \"{0}\" > {1}", content, d);
            if (RunCommand(command, shell).IsEmpty())
                task.LastErrorMsg = String.Format("登陆【{0}】失败:{1}", ssh.ConnectionInfo.Host, "上传脚本失败");
            return this;
        }

        private String GetPID(String content)
        {
            Match mat = Regex.Match(content, Wrap(@"\[\d+\]\s*(\d+)")); // 匹配类似 [1] 7177
            if (mat.Success && mat.Groups[1].Success)
                return mat.Groups[1].Value;
            return String.Empty;
        }

        public String RunGambleTask()
        {
            if (IsError())
                return String.Empty;

            EnterGambleTaskDirectory();
            String command = String.Format("python {0}", TargetGambleScript);
            //String command = "sleep 3600";
            String ret = RunCommand(String.Format("{0} & disown -a", command), shell);
            String pid = GetPID(ret);
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
            if (IsError())
                return this;
            // 删除 临时目录
            if (IsSafe(GambleTaskDirectory))
                RunCommand(String.Format("rm -rf {0};", GambleTaskDirectory), shell);
            return this;
        }

        public BastionAPI CreateGambleTaskDirectory()
        {
            if (IsError())
                return this;

            String command = String.Format("mkdir -p {0}", GambleTaskDirectory);
            RunCommand(command, shell);
            return this;
        }


        private bool IsAliveGambleTask()
        {
            String result = RunCommand(String.Format("ps -q {0} -o cmd | grep {1}", task.PID, TargetGambleScript), shell);
            return Regex.IsMatch(result, Wrap(TargetGambleScript));
        }

        private bool IsGambleResultFileReady()
        {
            String result = RunCommand(String.Format("ls {0} | grep tgz | tail -n 1", GambleTaskDirectory), shell);
            return Regex.IsMatch(result, @"000000_queryResult_db_\d+_\d+.tgz\r?\n");
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
            if (!ssh.IsConnected)
                return "连接失败";

            bool isTimeout = IsTaskTimeout();
            bool isAlive = IsAliveGambleTask();
            bool isGRFReady = IsGambleResultFileReady();

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
