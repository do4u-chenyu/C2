using Amib.Threading;
using C2.Business.Cracker.Model;
using C2.Business.Cracker.rdp;
using C2.Business.Cracker.Tools;
using C2.Utils;
using Mono.Security.Cryptography;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace C2.Business.Cracker.Dialogs
{
    public partial class CrackerForm : Form
    {
        private HashSet<string> list_username = new HashSet<string>();
        private HashSet<string> list_password = new HashSet<string>();
        private HashSet<string> list_target = new HashSet<string>();
        private HashSet<string> list_import_target = new HashSet<string>();
        private HashSet<string> list_ip_break = new HashSet<string>();
        private HashSet<string> list_ip_user_break = new HashSet<string>();
        private HashSet<string> list_success_username = new HashSet<string>();
        private int runTime = 0;
        private int retryCount = 0;
        private int maxThread = 50;
        private int timeOut = 5;

        private Boolean crackerOneCount = true;//只检查一个账户
        public int successCount = 0;
        public long creackerSumCount = 0;
        public long allCrackCount = 0;
        private Boolean notAutoSelectDic = false;
        public string[] servicesName = { };
        public string[] servicesPort = { };

        public long scanPortsSumCount = 0;

        private Dictionary<string, ServiceModel> services = new Dictionary<string, ServiceModel>();//服务列表
        private Dictionary<string, HashSet<string>> dics = new Dictionary<string, HashSet<string>>();//字典列表
        public ConcurrentBag<string> list_cracker = new ConcurrentBag<string>();
        public void InitServices()
        {
            try
            {
                this.servicesName = "SSH:FTP:Windows:MySQL".Split(':');
                this.servicesPort = "22:21:3389:3306".Split(':');
            }
            catch (Exception e)
            {

                LogWarning("加载检查服务配置发生异常" + e.Message);
            }
            services.Clear();
            for (int i = 0; i < servicesName.Length; i++)
            {
                ServiceModel sm = new ServiceModel
                {
                    Name = servicesName[i],
                    Port = servicesPort[i]
                };
                //SSL和非SSL采用一个字典
                string dicname = sm.Name.ToLower().Replace("_ssl", string.Empty);
                sm.DicUserNamePath = "/Resources/CrackerDict/dic_username_" + dicname + ".txt";
                sm.DicPasswordPath = "/Resources/CrackerDict/dic_password_" + dicname + ".txt";
                services.Add(sm.Name, sm);
            }
        }

        public CrackerForm()
        {
            InitializeComponent();
            Form.CheckForIllegalCrossThreadCalls = false;
        }
        public delegate void LogAppendDelegate(Color color, string text);

        /// <summary> 
        /// 追加显示文本 
        /// </summary> 
        /// <param name="color">文本颜色</param> 
        /// <param name="text">显示文本</param> 
        public void LogAppend(Color color, string text)
        {
            if (this.txt_log.Text.Length > 10000)
            {
                this.txt_log.Clear();
            }
            this.txt_log.SelectionColor = color;
            this.txt_log.HideSelection = false;
            this.txt_log.AppendText(text + Environment.NewLine);
        }
        /// <summary> 
        /// 显示错误日志 
        /// </summary> 
        /// <param name="text"></param> 
        public void LogError(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            this.txt_log.Invoke(la, Color.Red, DateTime.Now + "----" + text);
        }
        /// <summary> 
        /// 显示警告信息 
        /// </summary> 
        /// <param name="text"></param> 
        public void LogWarning(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            this.txt_log.Invoke(la, Color.Violet, DateTime.Now + "----" + text);
        }
        /// <summary> 
        /// 显示一般信息 
        /// </summary> 
        /// <param name="text"></param> 
        public void LogMessage(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            this.txt_log.Invoke(la, Color.Black, DateTime.Now + "----" + text);
        }
        /// <summary> 
        /// 显示正确信息 
        /// </summary> 
        /// <param name="text"></param> 
        public void LogInfo(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            this.txt_log.Invoke(la, Color.Green, DateTime.Now + "----" + text);
        }


        private long lastCount = 0;
        private void UpdateStatus()
        {
            try
            {
                if (stp != null)
                {
                    long workCount = allCrackCount;

                    this.stxt_speed.Text = (workCount - this.lastCount) + string.Empty;
                    this.lastCount = workCount;
                    this.stxt_threadStatus.Text = stp.InUseThreads + "/" + stp.Concurrency;

                    int c = 0;
                    if (this.creackerSumCount != 0)
                    {
                        c = (int)Math.Floor((workCount * 100 / (double)this.creackerSumCount));
                        this.stxt_threadPoolStatus.Text = allCrackCount + "/" + this.creackerSumCount;
                    }
                    if (c <= 0)
                    {
                        c = 0;
                    }
                    if (c >= 100)
                    {
                        c = 100;
                    }
                    this.stxt_percent.Text = c + "%";
                    this.tools_proBar.Value = c;
                }
                this.stxt_crackerSuccessCount.Text = successCount + string.Empty;
                this.stxt_useTime.Text = runTime + string.Empty;
                this.tssl_notScanPortsSumCount.Text = this.scanPortsSumCount + string.Empty;
            }
            catch (Exception e)
            {
                LogWarning(e.Message);
            }
        }

        private void Bt_timer_Tick(object sender, EventArgs e)
        {
            runTime++;
            this.Invoke(new update(UpdateStatus));

        }

        delegate void update();


        delegate void DelegateAddItem(ListViewItem lvi);

        private void AddItem(ListViewItem lvi)
        {

            this.list_lvw.Items.Add(lvi);
        }

        public void ScanPort(string ip, string serviceName, int port)
        {

            //直接使用TcpClient类
            TcpClient tc = new TcpClient();
            //设置超时时间
            tc.SendTimeout = tc.ReceiveTimeout = 2000;

            try
            {
                //异步方法
                IAsyncResult oAsyncResult = tc.BeginConnect(ip, port, null, null);
                oAsyncResult.AsyncWaitHandle.WaitOne(2000, true);

                if (tc.Connected)
                {
                    lock (list_cracker)
                    {
                        list_cracker.Add(ip + ":" + port + ":" + serviceName);
                    }
                    tc.Close();
                    LogMessage(ip + " port " + port + " 开放");
                    //FileTool.AppendLogToFile(Directory.GetCurrentDirectory() + "/logs/portscan-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log", ip + ":" + port + ":" + serviceName);
                }
                else
                {

                    LogWarning(ip + " port " + port + " 连接超时");
                }
            }
            catch (SocketException e)
            {

                LogWarning(ip + " port " + port + " 关闭" + e.Message);
            }
            finally
            {
                tc.Close();

            }
            Interlocked.Decrement(ref scanPortsSumCount);
        }

        private void CrackerService(string crakerstring, string username, string password)
        {
            try
            {
                string[] crakers = crakerstring.Split(':');
                string ip = crakers[0];
                int port = int.Parse(crakers[1]);
                string serviceName = crakers[2];

                //跳过无法检查的IP列表，提高效率
                //多线程安全
                lock (list_ip_break)
                {
                    if (list_ip_break.Contains(ip + port))
                    {
                        //LogWarning(ip+"-"+port+"跳过检查!");
                        Interlocked.Increment(ref allCrackCount);
                        return;
                    }
                }
                //多线程安全
                lock (list_ip_user_break)
                {
                    //跳过已经检查的列表，提高效率
                    if (list_ip_user_break.Contains(ip + port + username))
                    {
                        LogWarning(ip + "-" + port + "-" + username + "跳过检查");
                        Interlocked.Increment(ref allCrackCount);
                        return;
                    }
                }


                if (true)
                {
                    Object[] pramars = { ip, port, username, password, timeOut, retryCount };

                    int count = 0;
                    Server server = new Server();

                    while (count <= this.retryCount)
                    {
                        count++;
                        try
                        { //跳过检查，多线程安全
                            bool cconce = false;
                            lock (list_success_username)
                            {
                                cconce = list_success_username.Contains(ip + serviceName + port);
                            }
                            if (this.crackerOneCount && cconce)
                            {
                                break;
                            }
                            Stopwatch sw = new Stopwatch();
                            sw.Start();

                            if (serviceName.Equals("Windows"))
                            {
                                server.ip = ip;
                                server.timeout = timeOut;
                                server.serverName = "Windows";
                                server.port = port;
                                server.username = username;
                                server.password = password;
                                server = CreackRDP(server);
                            }
                            else
                            {
                                CrackService cs = null;
                                if (cs == null)
                                {
                                    Type type = Type.GetType("C2.Business.Cracker.Model.Crack" + serviceName);
                                    if (type != null)
                                    {
                                        cs = (CrackService)Activator.CreateInstance(type);

                                    }
                                }
                                server = cs.creack(ip, port, username, password, timeOut);

                            }
                            sw.Stop();
                            server.userTime = sw.ElapsedMilliseconds;

                        }
                        catch (IPBreakException ie)
                        {
                            string breakip = ie.Message;
                            lock (list_ip_break)
                            {
                                if (!list_ip_break.Contains(breakip))
                                {
                                    list_ip_break.Add(breakip);
                                }
                            }
                        }
                        catch (IPUserBreakException ie)
                        {
                            lock (list_ip_break)
                            {
                                string breakipuser = ie.Message;
                                if (!list_ip_break.Contains(breakipuser))
                                {
                                    list_ip_user_break.Add(breakipuser);
                                }
                            }
                        }
                        catch (TimeoutException)
                        {
                            continue;
                        }
                        catch (Exception)
                        {
                            //string logInfo = "检查" + ip + ":" + serviceName + "登录发生异常！" + e.Message;
                            //LogWarning(logInfo);
                            //FileTool.log(logInfo + e.StackTrace);
                        }
                        break;
                    }
                    if (server.isSuccess)
                    {
                        bool success = false;
                        lock (list_success_username)
                        {
                            success = list_success_username.Contains(ip + serviceName + port + username);
                        }
                        if (!success)
                        {
                            if (this.crackerOneCount)
                            {
                                //多线程安全
                                lock (list_success_username)
                                {
                                    success = list_success_username.Contains(ip + serviceName + port);
                                }
                            }
                            if (!success)
                            {
                                //多线程安全
                                lock (list_success_username)
                                {
                                    list_success_username.Add(ip + serviceName + port);
                                    list_success_username.Add(ip + serviceName + port + username);
                                }
                                Interlocked.Increment(ref successCount);
                                AddItemToListView(successCount, ip, serviceName, port, username, password, server.banner, server.userTime);
                                String sinfo = ip + "-----" + serviceName + "----" + username + "----" + password + "----" + server.banner + "----成功！";
                                LogInfo(sinfo);
                                //FileTool.AppendLogToFile(Directory.GetCurrentDirectory() + "/logs/success-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log", sinfo);

                            }

                        }

                    }
                    else
                    {
                        LogWarning(ip + "-----" + serviceName + "----" + username + "----" + password + "----失败");
                    }

                }

            }
            catch (Exception e)
            {
                LogError(e.Message + e.StackTrace);
            }
            Interlocked.Increment(ref allCrackCount);
        }

        private void AddItemToListView(int successCount, string ip, String serviceName, int port, String username, String password, String banner, long userTime)
        {

            ListViewItem lvi = new ListViewItem(successCount.ToString());
            lvi.SubItems.Add(ip);
            lvi.SubItems.Add(serviceName);
            lvi.SubItems.Add(port.ToString());
            lvi.SubItems.Add(username);
            lvi.SubItems.Add(password);
            lvi.SubItems.Add(banner);
            lvi.SubItems.Add(userTime + String.Empty);
            this.list_lvw.Invoke(new DelegateAddItem(AddItem), lvi);
        }

        delegate void VoidDelegate();
        private SmartThreadPool stp = null;
        private void Cracker()
        {
            CrakerKey();
            this.Invoke(new VoidDelegate(this.bt_timer.Start));
            this.runTime = 0;
            if (InitDic())
            {
                //初始化检查次数
                InitStatusCount();
                //清空检查列表
                this.list_cracker = new ConcurrentBag<string>();
                //清空跳过列表
                this.list_ip_break.Clear();
                this.list_ip_user_break.Clear();
                Boolean isScanport = true;//扫描端口
                stp = new SmartThreadPool
                {
                    MaxThreads = maxThread
                };
                creackerSumCount = 0;
                scanPortsSumCount = 0;


                //计算端口扫描总数
                if (isScanport)
                {
                    foreach (string serviceName in this.services_list.CheckedItems)
                    {
                        string[] ports = this.services[serviceName].Port.Split(',');
                        scanPortsSumCount += this.list_target.Count * ports.Length;
                    }
                }
                //更新状态
                this.Invoke(new update(UpdateStatus));

                foreach (string serviceName in this.services_list.CheckedItems)
                {
                    string[] ports = this.services[serviceName].Port.Split(',');
                    foreach (string sport in ports)
                    {
                        int port = int.Parse(sport);

                        foreach (string ip in list_target)
                        {
                            if (!isScanport)
                            {
                                list_cracker.Add(ip + ":" + port + ":" + serviceName);
                            }
                            else
                            {
                                stp.QueueWorkItem<string, string, int>(ScanPort, ip, serviceName, port);
                                stp.WaitFor(maxThread);
                            }
                        }
                    }

                }

                stp.WaitForIdle();
                if (isScanport)
                {
                    LogMessage("验证端口是否开放完成");
                }
                int c = stp.CurrentWorkItemsCount;
                LogMessage("开始检查" + this.list_cracker.Count + "个目标," + services_list.CheckedItems.Count + "个服务.......");
                //计算检查总数
                if (this.notAutoSelectDic == false)
                {
                    foreach (string serviceName in this.services_list.CheckedItems)
                    {
                        creackerSumCount += list_cracker.Count * this.services[serviceName].ListUserName.Count * this.services[serviceName].ListPassword.Count;
                    }
                }
                else
                {
                    creackerSumCount += list_cracker.Count * this.list_username.Count * this.list_password.Count * this.services_list.CheckedItems.Count;
                }

                foreach (string serviceName in this.services_list.CheckedItems)
                {
                    HashSet<string> clist_username = null;
                    HashSet<string> clist_password = null;
                    if (this.notAutoSelectDic == false)
                    {
                        clist_username = this.services[serviceName].ListUserName;
                        clist_password = this.services[serviceName].ListPassword;
                    }
                    else
                    {
                        clist_username = this.list_username;
                        clist_password = this.list_password;
                    }

                    foreach (string user in clist_username)
                    {
                        //替换变量密码
                        string username = user;
                        HashSet<string> list_current_password = new HashSet<string>();

                        //redis不需要破解账户
                        if (serviceName.Equals("Redis"))
                        {
                            username = "/";
                        }

                        foreach (string cpass in clist_password)
                        {
                            string newpass = cpass.Replace("%user%", user);
                            if (!list_current_password.Contains(newpass))
                            {
                                list_current_password.Add(newpass);

                            }
                            else
                            {
                                //重复密码
                                creackerSumCount -= this.list_cracker.Count * clist_username.Count;
                            }

                        }
                        foreach (string pass in list_current_password)
                        {
                            foreach (string cracker in this.list_cracker)
                            {
                                if (cracker.EndsWith(serviceName))
                                {
                                    stp.QueueWorkItem<string, string, string>(CrackerService, cracker, username, pass);
                                    stp.WaitFor(maxThread);
                                }
                            }
                        }
                        //redis不需要破解账户
                        if (serviceName.Equals("Redis"))
                        {
                            break;
                        }
                    }
                }
                stp.WaitForIdle();
                stp.Shutdown();
                LogInfo("检查完成！");

            }
            //更新状态
            this.Invoke(new update(UpdateStatus));
            this.btn_cracker.Enabled = true;
            this.services_list.Enabled = true;
            this.Invoke(new VoidDelegate(this.bt_timer.Stop));
        }

        public void StopCraker()
        {
            if (stp != null && !stp.IsShuttingdown && this.crackerThread != null)
            {
                LogWarning("等待线程结束...");
                stp.Cancel();
                this.crackerThread.Abort();
                while (stp.InUseThreads > 0)
                {
                    Thread.Sleep(50);
                }

                //更新状态
                this.Invoke(new update(UpdateStatus));
                this.btn_cracker.Enabled = true;
                this.services_list.Enabled = true;
                this.bt_timer.Stop();
                LogWarning("全部线程已停止");
            }
        }
        private Boolean InitDic()
        {

            if (string.IsNullOrEmpty(this.txt_target.Text))
            {
                MessageBox.Show("请设置需要检查的目标的IP地址");
                return false;
            }
            else if (this.services_list.CheckedItems.Count <= 0)
            {
                MessageBox.Show("请选择需要检查服务");
                return false;
            }
            else
            {
                if (!string.IsNullOrEmpty(this.txt_target.Text))
                {

                    bool isTrue = Regex.IsMatch(this.txt_target.Text, "^([\\w\\-\\.]{1,100}[a-zA-Z]{1,8})$|^(\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3})$");
                    if (isTrue)
                    {
                        this.list_target.Clear();
                        this.list_target.Add(this.txt_target.Text);
                    }
                    else
                    {
                        isTrue = Regex.IsMatch(this.txt_target.Text, "^\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\-\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}$");

                        if (isTrue)
                        {
                            this.list_target.Clear();
                            string[] ips = this.txt_target.Text.Split('-');
                            if (ips.Length == 2)
                            {
                                string startip = ips[0];
                                string endip = ips[1];
                                string[] startips = startip.Split('.');
                                string[] endips = endip.Split('.');
                                if (startips.Length == 4 && endips.Length == 4)
                                {
                                    int startips_3 = int.Parse(startips[2]);
                                    int endips_3 = int.Parse(endips[2]);
                                    int startips_4 = int.Parse(startips[3]);
                                    int endips_4 = int.Parse(endips[3]);

                                    if (endips_3 >= startips_3 && endips_3 <= 255 && endips_4 <= 255)
                                    {

                                        for (int i = startips_3; i <= endips_3; i++)
                                        {

                                            if (startips_3 == endips_3)
                                            {
                                                if (startips_4 <= endips_4)
                                                {
                                                    for (int j = startips_4; j <= endips_4; j++)
                                                    {
                                                        string ip = startips[0] + "." + startips[1] + "." + startips[2] + "." + j;
                                                        this.list_target.Add(ip);
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                int index_start = 0;
                                                int index_end = 255;
                                                if (i == startips_3)
                                                {
                                                    index_start = startips_4;
                                                }
                                                if (i == endips_3)
                                                {
                                                    index_end = endips_4;
                                                }

                                                for (int j = index_start; j <= index_end; j++)
                                                {
                                                    string ip = startips[0] + "." + startips[1] + "." + i + "." + j;
                                                    this.list_target.Add(ip);
                                                }

                                            }
                                        }

                                    }
                                }

                            }


                        }
                        else
                        {
                            this.list_target = this.list_import_target;
                        }
                    }
                    if (this.list_target.Count <= 0)
                    {
                        MessageBox.Show("目标格式错误！\r\n格式示例：\r\n192.168.1.1\r\nwww.baidu.com\r\n192.168.1.1-192.168.200.1\r\n192.168.1.1-192.168.1.200");
                        return false;
                    }
                }

            }

            //如果默认字典没有数据则加载默认字典  notAutoSelectDic必为false,自动选择字典
            if (dics.Count <= 0)
            {
                LogMessage("根据选择检查的服务自动加载字典......");

                foreach (string serviceName in this.services_list.CheckedItems)
                {
                    ServiceModel sm = this.services[serviceName];
                    sm.ListUserName = FileTool.readFileToList(Application.StartupPath + sm.DicUserNamePath);
                    sm.ListPassword = FileTool.readFileToList(Application.StartupPath + sm.DicPasswordPath);
                    if (sm.ListUserName.Count <= 0)
                    {
                        LogWarning("加载" + serviceName + "用户名字典未发现数据！");
                    }
                    else if (sm.ListPassword.Count <= 0)
                    {
                        LogWarning("加载" + serviceName + "密码字典未发现数据！");
                    }
                    else
                    {
                        LogWarning("加载" + serviceName + "字典成功，用户名" + sm.ListUserName.Count + "个，密码" + sm.ListPassword.Count + "个！");
                    }
                }
                LogMessage("根据选择检查的服务自动加载字典完成！");
            }
            
            return true;

        }

        private void CrakerKey()
        {
            var forBuild = 65535;
            var validTillDate = DateTime.Now.AddDays(64).Subtract(new DateTime(2010, 12, 31)).TotalDays;

            var forgedBytes = new byte[16];
            forgedBytes[0] = (byte)(validTillDate / 256);
            forgedBytes[1] = (byte)(validTillDate % 256);
            forgedBytes[2] = (byte)(forBuild / 256);
            forgedBytes[3] = (byte)(forBuild % 256);

            var output = new byte[32];
            output[0] = 7;
            var rnd = new Random();
            for (var x = 1; x < 13; x++)
                output[x] = (byte)rnd.Next(1, 256);

            Array.Copy(forgedBytes, 0, output, 14, forgedBytes.Length);

            var rsaManaged2 = new RSAManaged();
            rsaManaged2.FromXmlString("<RSAKeyValue><Modulus>thycVKzZzdxBD6Rl8RoS9MEs1rrLY5qDhse+a+ljfpM=</Modulus><Exponent>AQAB</Exponent><P>xJXNbvuhJEpA647ZChJHMQ==</P><Q>7Sb4m1/8WXGGL/2Zw075Aw==</Q><DP>VtattvbkyfkbEHM7oN1OIQ==</DP><DQ>0kQaatCpErjYDBbjTUro9w==</DQ><InieQ>AVeR8pKZ4H05p7NRb02kNw==</InverseQ><D>ENshFS1Sk51ZYEtFLEXPjzPUmZbbIak0S+dyUK5o/sE=</D></RSAKeyValue>");

            var decrypted = rsaManaged2.DecryptValue(output);

            var key = "==A" + Convert.ToBase64String(decrypted) + "=";
            string a = key.ToString();
            Rebex.Licensing.Key = a;
        }
        Thread crackerThread = null;
        public void RdpResult(ResponseType type, RdpClient rdp, ref Server server)
        {

            //接受事件通知，表示完成后，将当前阻塞线程放过继续执行。
            if (ResponseType.Finished.Equals(type))
            {
                server.isDisConnected = true;
                server.isEndMRE.Set();
            }

        }

        delegate Server addRDPdelegate(Server server);
        private Server AddRDPClient(Server server)
        {

            try
            {
                RdpClient rdp = new RdpClient();
                server.client = rdp;
                server.client.server = server;

                this.rdp_panle.Controls.Add(rdp);
                server.client.OnResponse += RdpResult;
                server.client.ConnectServer(server);

            }
            catch (Exception e)
            {
                FileTool.log(server.ip + ":" + server.port + "-RDP(Windows)操作异常-" + e.Message);
                LogWarning(server.ip + ":" + server.port + "-RDP(Windows)操作异常-" + e.Message);
                server.isDisConnected = true;
                server.isEndMRE.Set();
            }
            return server;

        }

        private delegate void deleteClearRDP(Server server);
        private void ClearRDP(Server server)
        {
            try
            {
                RdpClient rdp = server.client;
                if (rdp.Connected != 0)
                {
                    rdp.RequestClose();
                    rdp.Disconnect();
                }
                rdp.Dispose();
                this.rdp_panle.Controls.Remove(rdp);
            }
            catch (Exception e)
            {
                FileTool.log("RDP(Windows)资源清理异常-" + e.Message);
                LogWarning("RDP(Windows)资源清理异常-" + e.Message);
            }
        }

        private Server CreackRDP(Server server)
        {
            try
            {

                this.rdp_panle.Invoke(new addRDPdelegate(AddRDPClient), server);
                server.isEndMRE.WaitOne(server.timeout * 1000, true);
                this.rdp_panle.Invoke(new deleteClearRDP(ClearRDP), server);

            }
            catch (Exception e)
            {
                FileTool.log("创建RDP(Windows)控件发生错误：" + e.Message);
            }
            return server;
        }




        private void Btn_cracker_Click(object sender, EventArgs e)
        {
            new Log.Log().LogManualButton("弱口令素描", "运行");
            this.btn_cracker.Enabled = false;
            this.list_success_username.Clear();
            this.services_list.Enabled = false;

            crackerThread = new Thread(Cracker);
            crackerThread.Start();
        }

        private void InitStatusCount()
        {
            successCount = 0;
            allCrackCount = 0;
        }


        private void Main_Shown(object sender, EventArgs e)
        {
            this.cbox_reTry.SelectedIndex = 0;
            this.cbox_threadSize.SelectedIndex = 2;
            this.cbox_timeOut.SelectedIndex = 2;

            //加载默认配置
            InitServices();

            foreach (string key in services.Keys)
            {
                this.services_list.Items.Add(key, false);
                this.services_list.SetItemChecked(0, true);  // 默认选择第一个, 放循环里面安全简洁
            }
        }

        private void UpdateThreadSize()
        {
            this.maxThread = int.Parse(this.cbox_threadSize.Text);
            if (stp != null)
            {
                stp.MaxThreads = this.maxThread;
            }
        }

        private void Cbox_timeOut_TextChanged(object sender, EventArgs e)
        {
            this.timeOut = int.Parse(this.cbox_timeOut.Text);
        }

        private void Cbox_reTry_TextChanged(object sender, EventArgs e)
        {
            this.retryCount = int.Parse(this.cbox_reTry.Text);
        }
        private void Btn_stopCracker_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(StopCraker);
            th.Start();
        }

        private void ExportResult()
        {

            //保存文件
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "文本文件|*.csv"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate, System.IO.FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    string columns = string.Empty;
                    foreach (ColumnHeader dc in this.list_lvw.Columns)
                    {
                        columns += ("\"" + dc.Text + "\",");
                    }
                    sw.WriteLine(columns.Substring(0, columns.Length - 1));
                    foreach (ListViewItem sv in this.list_lvw.Items)
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        foreach (ListViewItem.ListViewSubItem subv in sv.SubItems)
                        {
                            sb.Append("\"" + subv.Text + "\",");
                        }
                        sb.Remove(sb.Length - 1, 1);
                        sw.WriteLine(sb.ToString());
                    }
                    sw.Close();
                    MessageBox.Show("导出完成");
                }
                catch (Exception e)
                {
                    FileTool.log("导出数据发生异常" + e.Message);
                    MessageBox.Show("导出数据发生异常");
                }
            }
        }

        private void Tsmi_export_Click(object sender, EventArgs e)
        {
            ExportResult();
        }

        private void Tsmi_deleteSelectItem_Click(object sender, EventArgs e)
        {
            if (this.list_lvw.SelectedItems.Count == 0)
            {
                return;
            }
            foreach (ListViewItem selitem in this.list_lvw.SelectedItems)
            {
                this.list_lvw.Items.Remove(selitem);
            }
        }

        private void Tsmi_copyItem_Click(object sender, EventArgs e)
        {
            if (this.list_lvw.SelectedItems.Count == 0)
            {
                return;
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 1; i <= 5; i++)
            {
                sb.Append("----");
                sb.Append(this.list_lvw.SelectedItems[0].SubItems[i].Text);
            }
            Clipboard.SetText(sb.Remove(0, 4).ToString());
            MessageBox.Show("复制成功");
        }
        private void Tsmi_clearItems_Click(object sender, EventArgs e)
        {
            this.list_lvw.Items.Clear();
        }

        private void Tsmi_openURL_Click(object sender, EventArgs e)
        {
            if (this.list_lvw.SelectedItems.Count == 0)
            {
                return;
            }
            string target = "http://" + this.list_lvw.SelectedItems[0].SubItems[1].Text + ":" + this.list_lvw.SelectedItems[0].SubItems[1].Text;

            try
            {
                System.Diagnostics.Process.Start("IEXPLORE.EXE", target);
            }
            catch (Exception oe)
            {
                MessageBox.Show("打开URL发生异常---" + oe.Message);
            }
        }


        private void Cbox_threadSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateThreadSize();
        }

        private void Btn_export_Click(object sender, EventArgs e)
        {
            ExportResult();
        }

        private void CrackerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.cancel为true表示取消关闭，为false表示可以关闭窗口
            e.Cancel = !CanFormClose();
        }

        private bool CanFormClose()
        {
            if (!this.btn_cracker.Enabled)
            {
                HelpUtil.ShowMessageBox("正在扫描中，请停止检查后再关闭。");
                return false;
            }
            return true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (services.ContainsKey("SSH"))
            {
                String dictPath = Path.GetDirectoryName(Application.StartupPath + services["SSH"].DicPasswordPath);
                ProcessUtil.ProcessOpen(dictPath);
            }
                
        }

        private void services_list_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.CurrentValue == CheckState.Checked) return;//取消选中就不用进行以下操作
            for (int i = 0; i < ((CheckedListBox)sender).Items.Count; i++)
            {
                ((CheckedListBox)sender).SetItemChecked(i, false);//将所有选项设为不选中
            }
            e.NewValue = CheckState.Checked;//刷新
        }
    }
}
