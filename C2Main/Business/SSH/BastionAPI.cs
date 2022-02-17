using C2.Core;
using C2.SearchToolkit;
using C2.Utils;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace C2.Business.SSH
{
    public class BastionAPI
    {
        private const int M40 = 1024 * 1024 * 40;
        private const int K8 = 1024 * 8;
        private const int K16 = K8 * 2;
        private const int K32 = K16 * 2;
        private const int K64 = K32 * 2;

        private const int K512 = 1024 * 512;

        private const int SecondsTimeout = 20;
        private const String SeparatorString = "5peg5L2g5L2Z55Sf5aaC5LiH5Y+k6ZW/5aSc";

        private static readonly Regex SeparatorRegex = new Regex(Wrap(Regex.Escape(SeparatorString)));
        private static readonly TimeSpan StandTimeout = TimeSpan.FromSeconds(SecondsTimeout);
        private static readonly TimeSpan HalfTimeout  = TimeSpan.FromSeconds(SecondsTimeout / 2);
        private readonly SearchTaskInfo task;

        private readonly SshClient ssh;
        private ShellStream shell;

        private bool downloadCancel = false;

        private static readonly LogUtil log = LogUtil.GetInstance("SearchToolkit");


        private String TargetScript { get => task.TargetScript; }
        // {workspace}/pid_taskcreatetime
        private String TaskDirectory { get => task.TaskDirectory; }
        public delegate void DownloadProgressEventHandler(String progressValue, long left, long fileLength);
        public event DownloadProgressEventHandler DownloadProgressEvent;
        private String TaskResultShellPattern { get => task.TaskResultShellPattern; }

        private String TaskResultRegexPattern { get => task.TaskResultRegexPattern; }
        private static String Wrap(String pattern)
        {
            return String.Format(@"\r?\n{0}\r?\n", pattern);
        }

        public BastionAPI(SearchTaskInfo task)
        {
            this.task = task;
            string ip   = ConvertUtil.GetIP(task.BastionIP);
            string port = ConvertUtil.GetPort(task.BastionIP); // 没有端口默认22
            this.ssh = new SshClient(ip, ConvertUtil.TryParseInt(port, 22), task.Username, task.Password);
            this.ssh.ConnectionInfo.Timeout = StandTimeout;
            this.ssh.ConnectionInfo.Encoding = Encoding.UTF8;
        }

        // 和Database里的测试联通函数名保持一致
        public bool TestConn()
        {
            Login();
            return task.LastErrorMsg.IsEmpty();
        }
        public BastionAPI Login()
        {
            try
            {
                log.Info(String.Format("任务:{0} 登陆堡垒机 {1}@{2}", task.TaskName, task.BastionIP, task.Username));
                ssh.Connect();  // 登陆堡垒
            }
            catch (Exception ex)
            {
                task.LastErrorMsg = String.Format("登陆【{0}】失败:{1}", ssh.ConnectionInfo.Host, ex.Message);
                task.LastErrorCode = BastionCodePage.LoginBastionFail;
                log.Warn(task.LastErrorMsg);
            }

            try
            {
                log.Info(String.Format("任务:{0} 开始通过堡垒机跳转到下一层", task.TaskName));
                BastionJump();         // 跳转
            }
            catch (Exception ex)
            {
                task.LastErrorMsg = String.Format("登陆【{0}】成功，通过堡垒机跳转到下一层【{1}】失败：{2}",
                    ssh.ConnectionInfo.Host,
                    task.SearchAgentIP,
                    ex.Message);
                task.LastErrorCode = BastionCodePage.JumpOneFail;
                log.Warn(task.LastErrorMsg);
            }

            try
            {
                // 有些地方需要二次跳转
                if (!task.InterfaceIP.IsEmpty())
                {
                    log.Info(String.Format("任务:{0} 界面机SSH跳转 【{1}】", task.TaskName, task.InterfaceIP));
                    SSHJump();
                }
                    
            }
            catch (Exception ex)
            {
                task.LastErrorMsg = String.Format("登陆【{0}】成功，界面机SSH跳转全文机【{1}】失败：{2}",
                    ssh.ConnectionInfo.Host,
                    task.SearchAgentIP,
                    ex.Message);
                task.LastErrorCode = BastionCodePage.JumpTwoFail;
                log.Warn(task.LastErrorMsg);
            }

            // 登陆堡垒机成功了，但是在跳转全文机的过程中出现了问题
            if (!Oops() && !CheckJumpOK())
            {
                task.LastErrorMsg = "登陆堡垒机成功了，但是在跳转全文机的过程中出现了问题";
                task.LastErrorCode = BastionCodePage.JumpSearchFail;
                log.Warn(task.LastErrorMsg);
            }

            return this;
        }

        private bool CheckJumpOK()
        {
            String ip = ConvertUtil.GetIP(task.SearchAgentIP);
            return CheckHostIP(ip); // 目前没有特别好的方法能够检测真正跳转成功
        }

        private bool CheckHostIP(String ip)
        {
            return RunCommand("ifconfig", shell).Contains(ip) || RunCommand("ip addr", shell).Contains(ip);
        }
        private void BastionJump()
        {
            if (Oops())
                return;

            // 跳转的目标机器，如果是2次跳转的情况，第一层应该是界面机
            String targetAddress = task.InterfaceIP.IsEmpty() ? task.SearchAgentIP : task.InterfaceIP;
            String ip   = ConvertUtil.GetIP(targetAddress);

            task.LastErrorMsg = String.Format("登陆堡垒机【{0}】成功，但未能跳转到下一层目标机器【{1}】", task.BastionIP, targetAddress);
            task.LastErrorCode = BastionCodePage.JumpUnknownFail;
            shell = ssh.CreateShellStream(String.Empty, 0, 0, 0, 0, 4096);
            // 等待目标机准备好
            _ = shell.Expect(@"Opt or ID>:", HalfTimeout);
            _ = shell.Read();
            // 打印机器列表
            shell.WriteLine("p");
            shell.Flush();
            // 计时等待机器列表
            String ret = shell.Expect(@"Opt or ID>:", HalfTimeout);
            // 跳转到目标机器
            shell.WriteLine(FindID(ip, ret ?? String.Empty));
            // 等待跳转成功,出现root用户提示符
            if (null == shell.Expect(new Regex(@"\[root@[^\]]+\]#"), HalfTimeout))
            {   // 修复bug:某些机器改了shell提示, 这里如果也不是ifconfig的话才认为失败 
                if (!CheckHostIP(ip)) 
                    return;
            }
            task.LastErrorMsg = String.Empty;
            _ = shell.Read(); // 清空buffer
        }

        private void SSHJump()
        {
            if (Oops())
                return;
            String ip   = ConvertUtil.GetIP(task.SearchAgentIP);
            String port = ConvertUtil.GetPort(task.SearchAgentIP);
            String pwd  = task.SearchPassword;
            String command = String.Format(@"ssh -o ""StrictHostKeyChecking no"" root@{0} -p {1}", ip, port);

            log.Info(String.Format("开始ssh跳转: {0}", command));
            
            //密码不存在，正常执行
            _ = string.IsNullOrEmpty(pwd) ? RunCommand(command, shell) : SSHJumpPwd(command, pwd, shell);
        }

        private String SSHJumpPwd(String command, String pwd, ShellStream ssm)
        {
            try
            {
                // 清理缓存
                _ = ssm.Read();
                // 执行命令
                ssm.WriteLine(command);
                String ret = ssm.Expect(":", TimeSpan.FromSeconds(SecondsTimeout));

                if (ret != null)
                {
                    log.Info(Shell.Format(ret));

                    ssm.WriteLine(pwd);   //输入密码
                    ssm.Flush();
                    log.Info(ssm.Read()); //打印登陆界面
                    
                    return ret;
                }
            }
            catch { }

            return String.Empty;
        }

        public void Close()
        {
            try
            {
                if (ssh != null)      // ssh.net库有bug，这里会报ObjectDisposedException
                    ssh.Disconnect(); // 但不主动调，链接又不释放 
            }
            catch { }

        }
        
        public static string availSize = string.Empty;

        public List<string> SearchDaemonIP()
        {
            List<string> daemonIPList = new List<string>() { };
            if (Oops()) return daemonIPList;

            //判断工作路径下是否有remote.sh和main_rule_http_xxxx.py文件
            string commandPath = String.Format("cd {0};head -n1 main_rule_http_xxxx.py;head -n1 remote.sh", TaskDirectory);
            string dirFile = RunCommand(commandPath, shell);
            if (dirFile.Contains("No such file or directory"))
            {
                HelpUtil.ShowMessageBox(string.Format("{0}目录下未找到remote.sh和main_rule_http_xxxx.py文件", TaskDirectory));
                return daemonIPList;
            }


            // 生成daemon机的有效IP列表
            string defualtPort = ConvertUtil.GetPort(task.SearchAgentIP);
            string commandValidIP = String.Format("sh remote.sh -s 1 -p {0}", defualtPort);
            RunCommand(commandValidIP, shell);

            string validIps = RunCommand("cat valid_ips.txt", shell);
            List<string> resultList = new List<string>(validIps.Split("\r\n"));
            resultList.Remove(task.SearchAgentIP);
            foreach (string ip in resultList)
            {
                if (ip.Contains(":" + defualtPort))
                    daemonIPList.Add(ip);
            }
            if (daemonIPList.Count == 0)
                HelpUtil.ShowMessageBox("无有效Deaemon机，请检查是否为全文主节点!");

            string size = RunCommand("cat size.txt", shell);
            if (size.Split("\r\n").Count() >= 2)
                availSize = size.Split("\r\n")[1];
            return daemonIPList;
        }
        
        private String FindID(String ip, String list)
        {
            String pattern = String.Format(@"\[(a\d+)\]\s+{0}\b", ip);
            Match mat = Regex.Match(list, pattern);
            return mat.Success ? mat.Groups[1].Value : ip;
        }
        private String RunCommand(String command, ShellStream ssm, int timeout = SecondsTimeout, bool writeLog = true)
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
                {
                    if (writeLog)
                        log.Info(Shell.Format(ret));   
                    return ret;
                }
            }
            catch { }

            return String.Empty;
        }
        private String RunDSQCommand(String command, ShellStream ssm, int timeout = SecondsTimeout, bool writeLog = true)
        {
            try
            {
                // 清理缓存
                _ = ssm.Read();
                // 执行命令
                ssm.WriteLine(command);
                // 根据分隔符和timeout确定任务输出结束
                String pwd = ssm.Expect("password:", TimeSpan.FromSeconds(timeout));

                if (pwd == null)
                    return string.Empty;
                for (int i =0;i<10; i++)
                {
                    string ret = ssm.Expect(new Regex(@"\[root@[^\]]+\]#"), TimeSpan.FromSeconds(timeout));
                    if (ret != null)
                        break;
                    else
                        ssm.WriteLine(SeparatorString);                
                }
                if (writeLog)
                    log.Info(Shell.Format(pwd));
                return pwd;
            }
            catch { }
            return String.Empty;
        }
        private bool CatTgzFile(String ffp, FileStream fs, long fileLength, ShellStream ssm)
        {
            try
            {
                // 清理缓存
                _ = ssm.Read();
                // 执行命令
                ssm.WriteLine(String.Format("cat {0}", ffp));
                // 打印分隔符
                ssm.WriteLine(String.Format("echo {0}", SeparatorString));

                // 根据lxf的情报, 40M 为中位数
                int bufferSize = fileLength > M40 ? K512 : K64;  // 40M 以上的文件用大缓存，减少进程切换的消耗   
                byte[] buffer = new byte[bufferSize + 1];        // 预留一个空白位用来存储预读字节

                Shell shell = new Shell(ssm);
                long left = fileLength - shell.ExpectTGZ(buffer, fs, StandTimeout, K32);
                if (left >= fileLength)  // Expect过程中没有写入任何数据,说明没遇到TGZ头
                {
                    task.LastErrorMsg = String.Format("任务【{0}】下载失败;文件格式损坏", task.TaskName);
                    task.LastErrorCode = BastionCodePage.DownloadFileCorrupted;
                    log.Warn(task.LastErrorMsg);
                    return false;
                }
                // 读缓存起始位置, 0 或 1(情况3时)
                int offset = 0;
                while (left > 0)
                {
                    // 计算文件下载进度
                    String progressValue = ((fileLength - left * 1.0f) / fileLength).ToString("P0");
                    DownloadProgressEvent?.Invoke(progressValue, left, fileLength);

                    int bytesRead = shell.Read(buffer, offset, (int)Math.Min(bufferSize, left), StandTimeout);

                    if (bytesRead == 0) // 超时
                    {
                        task.LastErrorMsg = String.Format("任务【{0}】下载失败：网络超时", task.TaskName);
                        task.LastErrorCode = BastionCodePage.DownloadTimeout;
                        log.Warn(task.LastErrorMsg);
                        return false;
                    }

                    if (downloadCancel)
                    {
                        task.LastErrorMsg = String.Format("用户取消任务【{0}】的下载", task.TaskName);
                        task.LastErrorCode = BastionCodePage.DownloadCancel;
                        log.Warn(task.LastErrorMsg);
                        return false;
                    }

                    // 策略:
                    // 0) cat 回传时，会把字节流的NL全部替换成CRNL,需要再替换回来
                    // 1) 最后一个字节不是CR，替换CRNL
                    // 2) 最后一个字节是CR，  再读一个字节, 新字节不是CR, 将新字符追加到buffer, 替换CRNL,继续循环
                    // 3) 最后一个字节是CR，  再读一个字节, 新字节还是CR, 先替换CRNL，然后将CR作为buffer第一个字节，继续循环

                    if (buffer[offset + bytesRead - 1] != OpUtil.CR)  // 情况1
                    {
                        left = Math.Max(left - shell.ReplaceCRNLWrite(buffer, 0, offset + bytesRead, fs), 0);
                        offset = 0; // 读完一次, 起始位置复位
                        continue;
                    }

                    byte one = 0;
                    if (!shell.ReadByte(ref one, StandTimeout))
                    {
                        task.LastErrorMsg = String.Format("任务【{0}】下载失败：远端读错误", task.TaskName);
                        task.LastErrorCode = BastionCodePage.DownloadRemoteReadFail;
                        log.Warn(task.LastErrorMsg);
                        return false;
                    }

                    if (one != OpUtil.CR)  // 情况2
                    {
                        buffer[offset + bytesRead] = one;  // 新字符追加到buffer
                        left = Math.Max(left - shell.ReplaceCRNLWrite(buffer, 0, offset + bytesRead + 1, fs), 0);
                        offset = 0; // 读完一次, 起始位置复位
                    }
                    else            // 情况3
                    {
                        left = Math.Max(left - shell.ReplaceCRNLWrite(buffer, 0, offset + bytesRead, fs), 0);
                        buffer[0] = one;   // 新字符放置到buffer头, 起始位置置为1
                        offset = 1;
                    }

                }
            }
            catch (Exception ex) { task.LastErrorMsg = ex.Message; return false; }

            // 下载100%
            DownloadProgressEvent?.Invoke("100%", 0, fileLength);
            return true;
        }

        private String GetRemoteFilename(String s)
        {
            String command = String.Format("ls -l {0} | awk '{{print $9}}' | head -n 1", s);
            String content = RunCommand(command, shell);

            Match mat = Regex.Match(content, Wrap(TaskResultRegexPattern));
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

        public void StopDownloadAsync()
        {
            downloadCancel = true;
        }

        public bool DownloadTaskResult(String d)
        {
            if (task.SearchMethod == SearchTaskMethod.DSQ)
            {
                String result = RunCommand(String.Format("cd {2};tar -zcvf 000000_queryResult_{0}_{1}_0.tgz results", task.TaskModel, task.TaskCreateTime, TaskDirectory), shell);
                if (!Regex.IsMatch(result, String.Format(@"result")))
                {
                    task.LastErrorMsg = String.Format("任务【{0}】: 文件不存在或空文件", task.TaskName);
                    task.LastErrorCode = BastionCodePage.DownloadFileNotExists;
                    log.Warn(task.LastErrorMsg);
                    return false;
                }
            }

            // 000000_queryResult_db_开始时间_结束时间.tgz
            String s = TaskDirectory + "/" + TaskResultShellPattern;
            //String s = TaskDirectory + "/" + "000000_queryResult_plane_20210604094525_20210902094525.tgz";

            if (Oops()) return false;

            String ffp = GetRemoteFilename(s);
            long len = GetRemoteFileSize(ffp);  // 文件有可能超过2G,不能用int
            if (len <= 0)
            {
                task.LastErrorMsg = String.Format("任务【{0}】: 文件不存在或空文件", task.TaskName);
                task.LastErrorCode = BastionCodePage.DownloadFileNotExists;
                log.Warn(task.LastErrorMsg);
                return false;
            }

            bool ret = false;
            try
            {
                using (FileStream fs = new FileStream(d, FileMode.Create, FileAccess.Write))
                    ret = CatTgzFile(ffp, fs, len, shell);

            }
            catch (Exception ex)
            {
                task.LastErrorMsg = ex.Message;
                log.Warn(task.LastErrorMsg);
                ret = false;
            }

            return ret;
        }

        private bool Oops()
        {
            return !(ssh.IsConnected && task.LastErrorMsg.IsEmpty());
        }

        public BastionAPI UploadSelectValidIP()
        {
            string SelectValidIP = "select_valid_ips.txt";
            if (Oops()) return this;
            log.Info(String.Format("任务:{0} 开始上传模型脚本 {1}", task.TaskName, SelectValidIP));

            String d = TaskDirectory + "/" + SelectValidIP;
            string txtPath = Path.Combine(Global.TempDirectory, "select_valid_ips.txt");
            return UploadScript(txtPath, d);
        }

        public BastionAPI UploadRemote()
        {
            string remote = "remote.sh";
            if (Oops()) return this;
            log.Info(String.Format("任务:{0} 开始上传模型脚本 {1}", task.TaskName, remote));

            String d = TaskDirectory + "/" + remote;
            string txtPath = Path.Combine(Global.ResourcesPath, "Script", "IAO_Search_gamble", remote);
            return UploadScript(txtPath, d);
        }

        public BastionAPI UploadTaskScript()
        {
            if (Oops()) return this;
            log.Info(String.Format("任务:{0} 开始上传模型脚本 {1}", task.TaskName, TargetScript));

            String d = TaskDirectory + "/" + TargetScript;
            return UploadScript(task.LocalPyZipPath, d);
        }

        private BastionAPI UploadScript(String ffp, String d)
        {
            if (!File.Exists(ffp))
            {
                task.LastErrorMsg = String.Format("上传脚本到全文机【{0}】失败, 脚本丢失{1}", task.SearchAgentIP, ffp);
                task.LastErrorCode = BastionCodePage.UploadScriptFail;
                log.Warn(task.LastErrorMsg);
                return this;
            }

            if (ffp.EndsWith(".zip"))
                return UploadZipBase64(ffp, d, d + ".zip");
            else
                return UploadScriptBase64(ffp, d);
        }

        private BastionAPI UploadZipBase64(String s, String dPy, String dZip)
        {
            byte[] buf = FileUtil.FileReadBytesToEnd(s);
            // Base64果然比shell硬转码好用多了,感谢lxf的脑洞
            String b64 = Convert.ToBase64String(buf);
            // 解码，解压
            String command = String.Format("echo -e \"{0}\" | base64 -di > {1}; unzip -op {1}>{2}", b64, dZip, dPy); 
            if (RunCommand(command, shell, SecondsTimeout * 2, false).IsEmpty())  // 上传脚本会回显内容，超时时间要长
            {
                task.LastErrorMsg = String.Format("上传脚本到全文机【{0}】失败", task.SearchAgentIP);
                task.LastErrorCode = BastionCodePage.UploadScriptFail;
                log.Warn(task.LastErrorMsg);
            }
                

            return this;
        }

        private BastionAPI UploadScriptBase64(String ffp, String d)
        {
            String s = FileUtil.FileReadToEnd(ffp);

            if (!String.IsNullOrEmpty(s))
            {
                String b64 = ST.EncodeBase64(s);        // Base64果然比shell硬转码好用多了
                // 这里可能还有超出shell缓冲区的问题
                String command = String.Format("echo -e \"{0}\" | base64 -di > {1}", b64, d);
                if (RunCommand(command, shell, SecondsTimeout * 2, false).IsEmpty())  // 上传脚本会回显内容，超时时间要长
                {
                    task.LastErrorMsg = String.Format("上传脚本到全文机【{0}】失败", task.SearchAgentIP);
                    task.LastErrorCode = BastionCodePage.UploadScriptFail;
                    log.Warn(task.LastErrorMsg);
                }        
            }
            return this;
        }

        private String GetPID(String content)
        {
            Match mat = Regex.Match(content, Wrap(@"\[\d+\]\s*(\d+)")); // 匹配类似 [1] 7177
            if (mat.Success && mat.Groups[1].Success)
                return mat.Groups[1].Value;
            return String.Empty;
        }

        private String ConstructTaskCommand()
        {
            string taskType = SearchTaskInfo.TaskDescriptionTable[task.TaskModel];
            List<string> illegalTypeList = new List<string>() { "hack", "bt", "apk", "ddos", "xss", "qg", "sf", "vps" };

            string parserTime = task.Settings.IsSetQueryTime() ? String.Format("--start {0} --end {1}", task.Settings.StartTime, task.Settings.EndTime) : String.Empty;
            string parserType = illegalTypeList.Contains(taskType) ? String.Format("--model {0}", taskType) : string.Empty;
            string parserQueryStr = task.Settings.IsSetQueryStr() ?  String.Format("--query '{0}'",task.Settings.QueryStr) : String.Empty;
            return String.Format("python {0} {1} {2} {3}",
                    TargetScript,
                    parserTime,
                    parserType,
                    parserQueryStr);
        }

        public BastionAPI CheckHomeSearch()
        {
            if (Oops()) return this;

            String targetPath = "/home/search/sbin/queryclient";
            // 不同的linux下alias不同导致会有颜色控制符,关闭颜色控制符
            String command = String.Format("ls --color=never {0}", targetPath);
            String ret = RunCommand(command, shell);
            if (!Regex.IsMatch(ret, Wrap(targetPath)))
            {
                // 这里不计入错误信息,只计入错误码
                task.LastErrorCode = BastionCodePage.NoHomeSearch;
                log.Warn(task.LastErrorMsg);
            }
            return this;
        }

        public String RunTask()
        {
            if (Oops()) return String.Empty;
            String command = ConstructTaskCommand();

            log.Info(String.Format("任务【{0}】: 全文主节点执行任务命令 {1}", task.TaskName, command));

            String ret = RunCommand(String.Format("{0} & disown -a", command), shell);
            String pid = GetPID(ret);
            // 未获取到pid，当作模型脚本执行失败
            if (pid.IsEmpty())
            {
                task.LastErrorMsg = String.Format("全文机已连接但执行脚本失败:{0}", TargetScript);
                task.LastErrorCode = BastionCodePage.RunTaskFail;
                log.Warn(task.LastErrorMsg);
            }
                
            return pid;
        }

        public bool RunDSQTask()
        {
            if (Oops()) return false;

            string sendCommand = string.Format("sh remote.sh -s 2 -f {0} -r {1}" + task.TaskDirectory, SearchTaskInfo.TaskScriptTable[task.TaskModel].Replace("_{0}", String.Empty));
            log.Info(string.Format("任务【{0}】: 全文主节点执行任务下发命令 {1}", task.TaskName, sendCommand));

            string sendRet = RunCommand(sendCommand, shell);
            if (sendRet == string.Empty)
                sendRet = RunDSQCommand(sendCommand, shell);
            List<string> invalidIPList = new List<string>();
            List<string> noFileIPList = new List<string>();
            List<string> sshFailIPList = new List<string>();
            foreach (string invalidIP in sendRet.Split("\r\n"))
            {
                if (invalidIP.Contains(" connection refused") || invalidIP.Contains(" connection timed out"))
                    invalidIPList.Add(invalidIP.Replace(" connection refused", "").Replace(" connection timed out", ""));
                else if (invalidIP.Contains(" No runmaxtimemonitor.py"))
                    noFileIPList.Add(invalidIP.Replace(" No runmaxtimemonitor.py", ""));
                else if (invalidIP.Contains("'s password: ") && invalidIP.Contains("root@"))
                    sshFailIPList.Add(invalidIP.Replace("'s password: ", "").Replace("root@", ""));
            }
            string invalidIPMessage = invalidIPList.Count() > 0 ? string.Format("Daemon机{0}连接失败，请检查ip和端口号是否正确。", invalidIPList.JoinString("、")) : string.Empty;
            string noFileMessage = noFileIPList.Count() > 0 ? string.Format("Daemon机{0}的home/search/bin/路径下无runmaxtimemonitor.py文件。", noFileIPList.JoinString("、")) : string.Empty;
            string sshFailMessage = sshFailIPList.Count() > 0 ? string.Format("Daemon机{0}无法建立ssh信任，连接失败。", sshFailIPList.JoinString("、")) : string.Empty;
            if (invalidIPMessage + noFileMessage + sshFailMessage != string.Empty)
                HelpUtil.ShowMessageBox(invalidIPMessage + noFileMessage + sshFailMessage);
            if (invalidIPList.Count() + noFileIPList.Count() + sshFailIPList.Count() == ConvertUtil.TryParseInt(task.SelectDaemonIPCount))
            {
                task.LastErrorMsg = "无有效daemond机";
                return false;
            }
            return true;
        }

        public BastionAPI EnterTaskDirectory()
        {
            if (Oops()) return this;
            log.Info(String.Format("任务:{0} cd 进入工作目录{1}", task.TaskName, TaskDirectory));
            
            String command = String.Format("cd {0}", TaskDirectory);
            task.LastErrorMsg = String.Empty;
            if (RunCommand(command, shell).IsEmpty())
            {
                task.LastErrorMsg = "全文机创建工作目录失败";
                task.LastErrorCode = BastionCodePage.TaskDirectoryFail;
                log.Warn(task.LastErrorMsg);
            }
            return this;
        }

        public BastionAPI DeleteTaskDirectory()
        {
            if (Oops()) return this;
            log.Info(String.Format("任务【{0}】: 删除 TaskDirectory", task.TaskName));
            // 删除 临时目录
            if (IsSafePath(TaskDirectory))
                if (task.SearchMethod == SearchTaskMethod.DSQ)
                    RunCommand(String.Format("cd {0}; sh remote.sh -s 3 -f {0}", TaskDirectory), shell);
                RunCommand(String.Format("rm -rf {0};", TaskDirectory), shell);
            return this;
        }

        public BastionAPI CreateTaskDirectory()
        {
            if (Oops()) return this;
            log.Info(String.Format("任务【{0}】: 创建 TaskDirectory : {1}", task.TaskName, TaskDirectory));
            
            String command = String.Format("mkdir -p {0}", TaskDirectory);
            RunCommand(command, shell);
            return this;
        }


        private bool IsAliveTask()
        {
            if (task.SearchMethod == SearchTaskMethod.DSQ)
            {
                String ret = RunCommand(String.Format("ps -ef | grep --color=never {0}", task.RemoteWorkspace), shell);//执行脚本里需要传工作执行路径，可以作为进程存在的判断条件
                return Regex.IsMatch(ret, String.Format(@"sh\s+remote.sh"));
            }
            else
            {
                String ret = RunCommand(String.Format("ps -p {0} -o cmd | grep --color=never {1}", task.PID, TargetScript), shell);
                return Regex.IsMatch(ret, String.Format(@"python\s+{0}", TargetScript));
            }
        }

        private bool IsResultFileReady()
        {
            if (task.SearchMethod == SearchTaskMethod.DSQ)
            {
                String result = RunCommand(String.Format("ls {0} | grep tgz | wc -l", TaskDirectory), shell);
                Match mat = Regex.Match(result, Wrap(@"(\d+)\r?\n")); // 匹配压缩包个数，大于总下发个数的70%认为结果文件已经全部处理完成
                if (mat.Success && mat.Groups[1].Success)
                    return ConvertUtil.TryParseInt(mat.Groups[1].Value) * 100 / task.DaemonIP.Count > 70;

                return false;
            }
            else
            {
                String result = RunCommand(String.Format("ls {0} | grep tgz | tail -n 1", TaskDirectory), shell);
                return Regex.IsMatch(result, @"000000_queryResult_(db|yellow|gun|plane|hack|bt|apk|ddos|xss|qg|sf|vps|email|dbqt|code|hostDD|hackDD|dm|custom|)_\d+_\d+.tgz\r?\n");
            }

        }

        private bool IsTaskTimeout()
        {
            DateTime born = ConvertUtil.TryParseDateTime(task.TaskCreateTime, "yyyyMMddHHmmss");
            TimeSpan ts = DateTime.Now.Subtract(born);
            double timeout = task.SearchMethod == SearchTaskMethod.DSQ ? 10 : 24 * 3;//dsq超时限制在10小时
            return Math.Abs(ts.TotalHours) >= timeout;
        }

        private bool IsSafePath(String v)
        {
            // 在服务器上删东西 尽量严格, 尤其不能有"空格/"，"/空格" 或 "空格/空格"
            return v.StartsWith(SearchTaskInfo.SearchWorkspace) && !Regex.IsMatch(v, @"\s");
        }

        public BastionAPI KillTask()
        {
            if (!Oops() && IsAliveTask()) // 确保不要误删其他复用进程
            {
                String command = String.Format("kill -9 {0}", task.PID);
                RunCommand(command, shell);
            }
            return this;
        }

        public String QueryTaskStatus()
        {
            if (!ssh.IsConnected)
                return "连接失败";
            bool isTimeout = IsTaskTimeout();
            bool isAlive = IsAliveTask();
            bool isRFReady = IsResultFileReady();


            // 1) pid不存在且有结果文件时, 为运行成功
            if (!isAlive && isRFReady)
                return "DONE";

            // 2) pid不存在且没有结果文件时, 为运行失败
            if (!isAlive && !isRFReady)
                return "FAIL";

            // 3) pid存在且没有结果文件且在未超时范围内, 为正在运行
            if (isAlive && !isRFReady && !isTimeout)
                return "RUNNING";

            // 4) pid存在且没有结果文件且超出运行时间(24 * 4小时), 为超时
            if (isAlive && !isRFReady && isTimeout)
                return "TIMEOUT";

            // 5) pid存在但有结果文件, 这种情况按道理不应该发生, 暂时假定运行成功
            if (isAlive && isRFReady)
                return "DONE";

            // 其他情况, 按道理不应该发生, 全部默认为失败
            return "FAIL";
        }

    }
}
