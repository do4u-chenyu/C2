using C2.Core;
using C2.SearchToolkit;
using C2.Utils;
using Renci.SshNet;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace C2.Business.SSH
{
    public class BastionAPI
    {
        private const byte CR = 0x13;
        private const byte NL = 0x10;

        private const int M40 = 1024 * 1024 * 40;
        private const int K2 = 4096 * 2;
        private const int K512 = 1024 * 512;
        

        private const int SecondsTimeout = 10;
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
            if (Oops())
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

        private bool Cat(String ffp, FileStream fs, long fileLength, ShellStream ssm)
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

                int bufferSize = fileLength > M40 ? K512 : K2;  // 40M 以上的文件用大缓存，减少进程切换的消耗   
                byte[] buffer = new byte[bufferSize + 1]; // 预留一个空白位用来存储预读字节

                Shell shell = new Shell(ssm);
                long left = fileLength - TgzHead.Length;  // 去掉文件头
                int offset = 0;                           // 缓存起始位置

                while (left > 0)
                {
                    int bytesRead = shell.Read(buffer, offset, bufferSize, Timeout);
                    offset = 0; // 读完一次, 起始位置复位

                    if (bytesRead == 0) // 超时
                    {
                        task.LastErrorMsg = String.Format("任务【{0}】下载失败：网络超时", task.TaskName);
                        break;
                    }
                    // 策略:
                    // 0) cat 回传时，会把字节流的NL全部替换成CRNL,需要再替换回来
                    // 1) 最后一个字节不是CR，替换CRNL
                    // 2) 新字节不是CR,将新字符追加buffer位，替换CRNL
                    // 3) 最后一个字节是CR，  再读一个字节, 新字节还是CR, 先替换CRNL，然后将CR作为buffer第一个字节，继续循环

                    if (buffer[bytesRead - 1] != CR)  // 情况1
                    {
                        left = Math.Max(left - ReplaceCRNLWrite(buffer, bytesRead, fs), 0);
                        continue;
                    }
  
                    byte one = 0;
                    if (!shell.ReadByte(ref one))
                        break;
                        
                    if (one != CR)  // 情况2
                    {
                        buffer[bytesRead] = one;
                        left = Math.Max(left - ReplaceCRNLWrite(buffer, bytesRead + 1, fs), 0);
                    }
                    else            // 情况3
                    {
                        left = Math.Max(left - ReplaceCRNLWrite(buffer, bytesRead, fs), 0);
                        buffer[offset++] = one;
                    }
                }
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

            if (Oops())
                return false;

            String ffp = GetRemoteFilename(s);
            long len = GetRemoteFileSize(ffp);  // 文件有可能超过2G,不能用int
            if (len <= 0)
            {
                task.LastErrorMsg = String.Format("任务【{0}】: 文件不存在或空文件", task.TaskName);
                return false; 
            }

            // Cat的方式下载文件， 根据lxf的情报, 40M 为中位数
            bool ret = false;
            try
            {
                using (FileStream fs = new FileStream(d, FileMode.Create, FileAccess.Write))
                    ret = Cat(ffp, fs, len, shell);
            } 
            catch (Exception ex)
            {
                task.LastErrorMsg = ex.Message;
                ret = false;
            }

            return ret;
        }

        private bool IsCRNL(byte[] buffer, int offset)
        {
            return buffer[offset] == CR && buffer[offset + 1] == NL;
        }

        private int ReplaceCRNLWrite(byte[] buffer, int count, FileStream fs)
        {
            count = Math.Min(buffer.Length, count); // 保险一下，下载错误的文件比程序崩强
            int real = count;

            int curr = 0; 
            int head = 0;


            if (count < 2)  // 不足2个字节,不可能含有CRNL
            {
                fs.Write(buffer, head, curr + 1 - head);
                return real;
            }
   
            do
            {
                // 找到 下一个 /r/n
                while (curr + 1 < count && !IsCRNL(buffer, curr))
                    curr++;
                // 没找到, 直接到结尾处,退出
                if (curr + 1 < count) 
                {
                    fs.Write(buffer, head, curr + 1 - head);
                    return real += curr + 1 - head;
                }
                // 找到CRNL
                buffer[curr] = NL;
                fs.Write(buffer, head, curr - head);

                real += curr - head;
                head = ++curr;      // 游标置于当前位置
           
            } while (curr + 1 < count); 

            return real;
        }

        private bool Oops()
        {
            return !(ssh.IsConnected && task.LastErrorMsg.IsEmpty());
        }

        public BastionAPI UploadGambleScript()
        {
            if (Oops()) return this;

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
                task.LastErrorMsg = String.Format("上传脚本到全文机【{0}】失败", task.SearchAgentIP);
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
            if (Oops()) return String.Empty;

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
            if (Oops()) return this;
            // 删除 临时目录
            if (IsSafe(GambleTaskDirectory))
                RunCommand(String.Format("rm -rf {0};", GambleTaskDirectory), shell);
            return this;
        }

        public BastionAPI CreateGambleTaskDirectory()
        {
            if (Oops()) return this;
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
