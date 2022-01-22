using Amib.Threading;
using C2.Business.CastleBravo.Intruder.Config;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace C2.Business.CastleBravo.Intruder
{
    public partial class IntruderForm : Form
    {
        private SmartThreadPool stp = new SmartThreadPool();
        private Thread sThread = null;
        private ToolIntruder tools;
        private ConfigIntruder config = null;

        string dictDirectory;
        Dictionary<string, List<string>> dictContent;

        private List<string> refererList;//存放报文中的Referer

        //private string scanURL;
        private long scanDirCount = 0;
        private long scanSumCount = 0;//扫描目录总数
        private List<string> selectedDict;

        private long lastCount = 0;
        private int scanRunTime = 0;//已扫描时间

        delegate void VoidDelegate();
        delegate void update();
        delegate void DelegateAddItemToListView(Config.ServerInfo svinfo);

        string[] lines;
        string[] splitLine;
        string lastLine = string.Empty;

        bool markIsClick = false;//判断是否点击了标记按钮

        ServerInfo tmpSvinfo;
        ServerInfo svinfo;



        public IntruderForm()
        {
            InitializeComponent();
            tools = new ToolIntruder();
            config = new ConfigIntruder();
            refererList = new List<string>();

            this.dictDirectory = Path.Combine(Application.StartupPath, "Resources", "IntruderDict");
            RefreshDict();
        }

        //字典配置
        private void RefreshDict()
        {
            //TODO 考虑一下把文件内容加入字典，计算行数和放入字典可以写在一起
            this.dictContent = new Dictionary<string, List<string>>();
            this.dictListView.Items.Clear();

            int dictCount = 0;

            foreach (string dictPath in config.GetDictByPath(this.dictDirectory))
            {
                dictCount++;

                ListViewItem lvi = new ListViewItem(dictCount + "");
                lvi.Tag = Path.GetFileName(dictPath);
                lvi.SubItems.Add(Path.GetFileName(dictPath));
                lvi.SubItems.Add(config.GetFileLines(dictPath, this.dictContent));
                lvi.SubItems.Add(config.GetFileSize(dictPath));

                this.dictListView.Items.Add(lvi);
            }
        }
        
        //目标地址自动解析
        private void textBoxRequestMessage_TextChanged(object sender, EventArgs e)
        {
            lines = tBReqMess.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            splitLine = tBReqMess.Text.Split(new char[] { '\n' });
            lastLine = splitLine[splitLine.Length - 1].Trim();


            if (tBReqMess.Text.Contains("Host")) 
            {
                var address = Array.Find(lines, line => line.IndexOf("Host") != -1).Replace("Host:", "").Trim();
                if (address.Contains(":"))
                {
                    textBoxUrl.Text = address.Split(':')[0];
                    textBoxPort.Text = address.Substring(address.IndexOf(':') + 1);
                }
                else 
                {
                    textBoxUrl.Text = address;
                    textBoxPort.Text = "8080";
                }   
            }

            if (tBReqMess.Text.Contains("Referer"))
            {
                var referer = Array.Find(lines, line => line.IndexOf("Referer") != -1).Replace("Host:", "").Trim();
                TextBoxReferer.Text = referer.Substring(referer.IndexOf(':') + 1);
            }

            if (lastLine.Contains("=") && splitLine.Length >= 2 &&
                (splitLine[splitLine.Length - 2] == string.Empty || splitLine[splitLine.Length - 2] == "\r"))
            {
                tBReqMessSetting(tBReqMess.Text.LastIndexOf("\n") + 1, tBReqMess.Text.LastIndexOf("=") - tBReqMess.Text.LastIndexOf("\n") - 1, Color.Blue);
                if(!tBReqMess.Text.Contains("§"))
                    tBReqMessSetting(tBReqMess.Text.LastIndexOf("=") + 1, tBReqMess.Text.Length - tBReqMess.Text.LastIndexOf("=") - 1, Color.Red);
                tBReqMessSetting(tBReqMess.Text.LastIndexOf("") + 1, tBReqMess.Text.Length - tBReqMess.Text.LastIndexOf("=") - 1, Color.Black);
            }
        }

        public void tBReqMessSetting(int start, int end, Color color)
        {
            this.tBReqMess.SelectionStart = start;
            this.tBReqMess.SelectionLength = end;
            this.tBReqMess.SelectionColor = color;
        }

        #region 开始扫描
        /*
         * 启动功能
         */
        private void startButton_Click(object sender, System.EventArgs e)
        {
            if (!CheckStartOption())
                return;

            this.startButton.Enabled = false;
            this.listView1.Items.Clear();
            this.logTextBox.Text = string.Empty;

            scanSumCount = 0;
            selectedDict = new List<string>();//字典名称
            foreach (ListViewItem name in this.dictListView.CheckedItems)
            {
                selectedDict.Add(name.Tag.ToString());
                scanSumCount += int.Parse(name.SubItems[2].Text);
            }
            scanSumCount = scanSumCount * refererList.Count() + refererList.Count();//扫Referer本身和其字典目录

            SetConfig();

            stp = new SmartThreadPool
            {
                MaxThreads = config.ThreadSize//最大线程数
            };

            sThread = new Thread(new ThreadStart(ScanThread));
            sThread.Start();
        }

        private void ScanThread()
        {
            this.scanRunTime = 0;
            this.Invoke(new VoidDelegate(this.scanTimer.Start));

            this.scanDirCount = 0;
            foreach (string referer in refererList)
            {
                this.scanDirCount++;
                tmpSvinfo = new ServerInfo
                {
                    host = tools.UpdateUrl(referer, false),
                    id = scanDirCount,
                    type = "指纹"  
                };
                tmpSvinfo.url = tmpSvinfo.host;
                stp.WaitFor(1000, 10000);
                stp.QueueWorkItem<ServerInfo>(ScanReferer, tmpSvinfo);
                LogMessage(tmpSvinfo.url + "加载完成，开始扫描目录");
                stp.WaitForIdle();

                
                foreach (string dictName in selectedDict)
                {
                    if (!this.dictContent.ContainsKey(dictName))
                    {
                        LogError("字典" + dictName + "未加载成功！");
                        continue;
                    }

                    this.dictContent.TryGetValue(dictName, out List<string> passwordList);
                    foreach (string singlePassword in passwordList)
                    {
                        this.scanDirCount++;

                        svinfo = new ServerInfo();
                        svinfo.target = referer;
                        //svinfo.host = tools.UpdateUrl(referer, true);
                        svinfo.host = referer;
                        svinfo.id = this.scanDirCount;
                        svinfo.type = "目录";
                        svinfo.password = singlePassword;
                        svinfo.url = svinfo.host + singlePassword;

                        stp.WaitFor(1000, 10000);
                        stp.QueueWorkItem<ServerInfo>(ScanExistsDirs, svinfo);
                    }
                }
            }

            stp.WaitForIdle();
            stp.Shutdown();
            this.Invoke(new VoidDelegate(StopScan));

        }

        private void ScanExistsDirs(ServerInfo svinfo)
        {
            ServerInfo result = new ServerInfo();
            LogInfo("开始扫描-----" + svinfo.password);
            result = HttpRequest.SendRequestGetBody(config, svinfo.url, config.TimeOut, config.keeAlive, 
                                                                    lastLine.Split('=')[0]+"="+svinfo.password);
            if (result.code != 0)
            {
                svinfo.code = result.code;
                String location = result.location;
                svinfo.contentType = result.contentType;
                svinfo.length = result.length == -1 ? result.body.Length : result.length;
                svinfo.server = result.server;
                svinfo.powerBy = result.powerBy;
                svinfo.runTime = result.runTime;
                svinfo.mistake = "否";
                svinfo.timeout = "否";
                this.Invoke(new DelegateAddItemToListView(AddItemToListView), svinfo);
            }
            else
                LogError("扫描失败-----" + svinfo.password + "-----" + result.contentType);

            Thread.Sleep(config.SleepTime * 1000);
        }


        private void ScanReferer(ServerInfo svinfo)
        {
            ServerInfo result = HttpRequest.SendRequestGetBody(config, svinfo.url, config.TimeOut,config.keeAlive,lastLine.Replace("§", ""));

            if (result.code != 0)
            {
                svinfo.password = (lastLine.Split('=')[1]).Replace("§", "");//密码
                svinfo.code = result.code;//状态码
                svinfo.ip = tools.GetIP(svinfo.host);
                svinfo.contentType = result.contentType;
                svinfo.length = result.length;//长度
                svinfo.server = result.server;
                svinfo.powerBy = result.powerBy;
                svinfo.runTime = result.runTime;
                svinfo.mistake = "否";
                svinfo.timeout = "否";
                this.Invoke(new DelegateAddItemToListView(AddItemToListView), svinfo);
            }
            else
                LogError(svinfo.url + "-----" + result.contentType);
        }

        private void AddItemToListView(ServerInfo svinfo)
        {
            //过滤类型不符合的
            if (!svinfo.contentType.StartsWith(config.contentType, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            ListViewItem lvi = new ListViewItem(svinfo.id + "");//序号
            lvi.Tag = svinfo.type;
            lvi.SubItems.Add(svinfo.password);//密码值
            lvi.SubItems.Add(svinfo.code + "");//状态码
            lvi.SubItems.Add(svinfo.mistake + "");//错误
            lvi.SubItems.Add(svinfo.timeout + "");//超时
            lvi.SubItems.Add(svinfo.length + "");//长度
            //lvi.SubItems.Add(svinfo.ip + "");
            if (svinfo.code.ToString().StartsWith("2"))
            {
                lvi.ForeColor = Color.Green;
            }
            else if (svinfo.code.ToString().StartsWith("3"))
            {
                lvi.ForeColor = Color.Blue;
            }
            else if (svinfo.code.ToString().StartsWith("4"))
            {
                lvi.ForeColor = Color.Gray;
            }
            else if (svinfo.code.ToString().StartsWith("5"))
            {
                lvi.ForeColor = Color.Red;
            }
            this.listView1.Items.Add(lvi);
        }


        private void SetConfig()
        {
            //TODO 其他配置项待加入

            config.ShowCodes = "200,301,302,303,403,404";//状态码
            config.Method = "POST";//http方法
            config.ThreadSize = int.Parse(this.threadSizeDown.Text);//线程数
            config.TimeOut = int.Parse(this.timeOutDown.Text);//超时
            config.SleepTime = int.Parse(this.sleepTimeDown.Text);//延时
        }

        private bool CheckStartOption()
        {
            if (stp.InUseThreads > 0)
            {
                HelpUtil.ShowMessageBox("上次任务还没停止，请停止上次任务！");
                return false;
            }

            refererList.Clear();
            var lines = tBReqMess.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (tBReqMess.Text.Contains("Referer"))
            {
                var referer = Array.Find(lines, line => line.IndexOf("Referer") != -1).Replace("Host:", "").Trim();
                refererList.Add(referer.Substring(referer.IndexOf(':') + 1).Trim());
            }
            else 
            {
                MessageBox.Show("请输入含有Referer的报文", "报文错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (this.tBReqMess.Text == string.Empty)
            {
                MessageBox.Show("请输入请求报文!", "配置错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (this.dictListView.CheckedItems.Count == 0)
            {
      
                MessageBox.Show("请选择扫描字典!", "配置错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!markIsClick)
            {
                MessageBox.Show("没有标记变体!", "配置错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        #endregion




        private void suspendButton_Click(object sender, System.EventArgs e)
        {

        }

        /*
         * 停止功能
         * stopButton_Click
         */
        private void stopButton_Click(object sender, System.EventArgs e)
        {
            Thread t = new Thread(BreakScan);
            t.Start();
        }
        public void BreakScan()
        {
            if (stp != null && !stp.IsShuttingdown && this.sThread != null)
            {
                LogWarning("等待线程结束...");
                stp.Cancel();
                this.sThread.Abort();
                while (stp.InUseThreads > 0)
                {
                    Thread.Sleep(50);
                }

                //更新状态
                this.Invoke(new VoidDelegate(StopScan));
            }
        }

        public void StopScan()
        {
            this.Invoke(new VoidDelegate(this.scanTimer.Stop));
            this.Invoke(new update(UpdateStatus));
            this.startButton.Enabled = true;
            LogMessage("扫描结束");
        }

        #region 日志
        public delegate void LogAppendDelegate(Color color, string text);
       /*
        * 显示追加信息
        * color:文本颜色
        * text：显示文本
        */
        public void LogAppend(Color color, string text)
        {
            if (this.logTextBox.Text.Length > 10000)
            {
                this.logTextBox.Clear();
            }
            this.logTextBox.SelectionColor = color;
            this.logTextBox.HideSelection = false;
            this.logTextBox.AppendText(text + Environment.NewLine);
        }


        //显示一般信息 
        public void LogMessage(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            this.logTextBox.Invoke(la, Color.Black, DateTime.Now + "----" + text);
        }

        
        //显示警告信息 
        public void LogWarning(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            this.logTextBox.Invoke(la, Color.Violet, DateTime.Now + "----" + text);
        }

        /// <summary> 
        /// 显示错误日志 
        /// </summary> 
        /// <param name="text"></param> 
        public void LogError(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            this.logTextBox.Invoke(la, Color.Red, DateTime.Now + "----" + text);
        }

        /// <summary> 
        /// 显示正确信息 
        /// </summary> 
        /// <param name="text"></param> 
        public void LogInfo(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            this.logTextBox.Invoke(la, Color.Green, DateTime.Now + "----" + text);
        }
        #endregion


        #region 底部进度
        private void ScanTimer_Tick(object sender, EventArgs e)
        {
            this.scanRunTime++;
            this.Invoke(new update(UpdateStatus));
        }

        private void UpdateStatus()
        {
            try
            {
                if (stp != null)
                {
                    long processedCount = stp.WorkItemsProcessedCount;
                    this.scanSpeed.Text = (processedCount - this.lastCount) + "";
                    this.lastCount = processedCount;

                    this.scanThreadStatus.Text = stp.InUseThreads + "/" + stp.Concurrency;
                    this.threadPoolStatus.Text = processedCount.ToString() + "/" + scanSumCount;

                    int c = 0;
                    if (this.scanSumCount != 0)
                    {
                        c = (int)Math.Floor((processedCount * 100 / (double)this.scanSumCount));
                        c = c >= 100 ? 100 : c;
                    }
                    this.progressPercent.Text = c + "%";
                    this.progressBar.Value = c;
                }

                this.scanUseTime.Text = this.scanRunTime + "";
            }
            catch (Exception e)
            {
                LogWarning(e.Message);
            }
        }
        #endregion

      
        /*
         * 验证IP是否可用
         * proxyTestButton_Click
         */
        private void proxyTestButton_Click(object sender, System.EventArgs e)
        {
            if (proxyIPTB.Text != string.Empty && proxyPortTB.Text != string.Empty)
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(proxyTestUrlTB.Text);
                config.ConfigurationPostGet(req, proxyIPTB.Text, proxyPortTB.Text);
                try 
                { 
                    HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                    config.GetResultParam(resp);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else 
            {
                MessageBox.Show("请输入服务器地址或端口", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }  
        }

      
        private void enableProxyCB_CheckedChanged(object sender, System.EventArgs e)
        {

        }

        //设置标记
        private void markSbutton_Click(object sender, System.EventArgs e)
        {
            markIsClick = true;
            try
            {
                var originReferer = Array.Find(lines, line => line.IndexOf("Referer") != -1).Replace("Host:", "").Trim();
                tBReqMess.Text = tBReqMess.Text.Replace(tBReqMess.SelectedText, "§" + tBReqMess.SelectedText + "§");
                var newReferer = Array.Find(lines, line => line.IndexOf("Referer") != -1).Replace("Host:", "").Trim();
                tBReqMess.Text = tBReqMess.Text.Replace(newReferer, originReferer);
            }
            catch
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("请输入正确的报文", "报文错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
            if (lastLine.Contains("=") && splitLine.Length >= 2 &&
               (splitLine[splitLine.Length - 2] == string.Empty || splitLine[splitLine.Length - 2] == "\r"))
            {
                tBReqMessSetting(tBReqMess.Text.LastIndexOf("=") + 1, tBReqMess.Text.Length - tBReqMess.Text.LastIndexOf("=") - 1, Color.Purple);
                tBReqMessSetting(tBReqMess.Text.LastIndexOf("") + 1, tBReqMess.Text.Length - tBReqMess.Text.LastIndexOf("=") - 1, Color.Black);
            }
        }

        //清除标记
        private void delSignButton_Click(object sender, System.EventArgs e)
        {
            if(tBReqMess.Text.Contains("§"))
                tBReqMess.Text = tBReqMess.Text.Replace("§",string.Empty);
        }

        //清空报文
        private void packageTBCbutton_Click(object sender, System.EventArgs e)
        {
            TextBoxReferer.Text = string.Empty;
            tBReqMess.Text = string.Empty;
        }

        private void pasteTBCButton_Click(object sender, System.EventArgs e)
        {
            this.pasteTextBox.Text = string.Empty;
        }

        private void sslCB_CheckedChanged(object sender, System.EventArgs e)
        {

        }

        //字典目录
        private void openDictPathBtn_Click(object sender, EventArgs e)
        {
            ProcessUtil.ProcessOpen(this.dictDirectory);
        }

        //刷新字典
        private void refreshDictBtn_Click(object sender, EventArgs e)
        {
            RefreshDict();
        }
        //全选
        private void allSelected_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in dictListView.Items)
                lvi.Checked = true;
        }
        //全不选
        private void noSelected_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in dictListView.Items)
                lvi.Checked = false;
        }
        private void dictLV_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            groupBox2.Text = string.Format("字典（激活{0}个）", dictListView.CheckedItems.Count);
        }

        public void updateRequestHeadersLV()
        {
            lines = requestHeaderTextBox.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string[][] responseHeaders = new string[lines.Length-1][];
            var post = Array.Find(lines, line => line.IndexOf("POST") != -1).Replace("POST", "").Trim();
            responseHeaders[0] = new string[] { "0", "POST", post };
            for (int i = 1; i < lines.Length-1; i++)
            {
                responseHeaders[i] = new string[] { i.ToString(), lines[i].Split(':')[0], lines[i].Split(':')[1] };
            }
            for (int i = 0; i < responseHeaders.Length; i++)
            {
                ListViewItem lvi = new ListViewItem(responseHeaders[i]);
                requestHeadersLV.Items.Add(lvi);
            }
            this.requestHeadersLV.EndUpdate();
        }

        private void updateResponseHeadersTextBox(string password)
        {
            ServerInfo result = HttpRequest.SendRequestGetBody(config, svinfo.host, config.TimeOut, config.keeAlive, lastLine.Split('=')[0] + "=" + password);
            fctb.Text = result.responseHeaders + "\n" + result.body;
        }

        private void updatewebBrowser(string password)
        {
            ServerInfo result = HttpRequest.SendRequestGetBody(config, svinfo.host, config.TimeOut, config.keeAlive, lastLine.Split('=')[0] + "=" + password);
            if (result.body.Contains("<script src='?login=geturl'></script><meta http-equiv='refresh' content='0;URL=?'>"))
                result.body = result.body.Replace("<script src='?login=geturl'></script><meta http-equiv='refresh' content='0;URL=?'>", "");
            this.webBrowser.DocumentText = result.body;
        }

        //点击listview事件
        private void listView1_Click(object sender, EventArgs e)
        {
            int selectCount = listView1.SelectedItems.Count;
            if (selectCount > 0)
            {
                string password = listView1.SelectedItems[0].SubItems[1].Text;
                requestHeaderTextBox.Text = tBReqMess.Text;
                requestHeaderTextBox.Text = requestHeaderTextBox.Text.Replace(lastLine, lastLine.Split('=')[0] + "=" + password);

                updateRequestHeadersLV();
                updateResponseHeadersTextBox(password);

                updatewebBrowser(password);

               
            }
        }
    }
}