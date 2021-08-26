using Amib.Threading;
using C2.Business.CastleBravo.WebScan.Model;
using C2.Business.CastleBravo.WebScan.Tools;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebScan
{
    public partial class WebScanForm : Form
    {
        private SmartThreadPool stp = new SmartThreadPool();
        private Thread sThread = null;
        private Tool tools;
        private Config config = null;

        string dictDirectory;
        Dictionary<string, List<string>> dictContent;

        private string scanURL;
        private long scanDirCount = 0;//扫描目录总数
        private List<string> selectedDict;

        delegate void VoidDelegate();
        delegate void update();
        delegate void DelegateAddItemToListView(ServerInfo svinfo);

        public WebScanForm()
        {
            InitializeComponent();
            tools = new Tool();
            config = new Config();

            this.headerCombo.SelectedIndex = 0;
            this.httpMethodCombo.SelectedIndex = 0;
            this.threadSizeCombo.SelectedIndex = 2;
            this.timeOutCombo.SelectedIndex = 2;
            this.sleepTimeCombo.SelectedIndex = 0;

            this.dictDirectory = Path.Combine(Application.StartupPath, "Resources", "WebScanDict");
            RefreshDict();
        }

        private void RefreshDict()
        {
            //TODO 考虑一下把文件内容加入字典，计算行数和放入字典可以写在一起
            this.dictContent = new Dictionary<string, List<string>>();
            this.dictListView.Items.Clear();

            int dictCount = 0;

            foreach(string dictPath in GetDictByPath(this.dictDirectory))
            {
                dictCount++;

                ListViewItem lvi = new ListViewItem(dictCount + "");
                lvi.Tag = Path.GetFileName(dictPath);
                lvi.SubItems.Add(Path.GetFileName(dictPath));
                lvi.SubItems.Add(GetFileLines(dictPath));
                lvi.SubItems.Add(tools.GetFileSize(dictPath));

                this.dictListView.Items.Add(lvi);
            }
        }

        private List<string> GetDictByPath(string path)
        {
            List<string> dictPathList = new List<string>();
            if (Directory.Exists(path))
            {
                foreach (FileSystemInfo fsinfo in new DirectoryInfo(path).GetFiles())
                {
                    if (Path.GetExtension(fsinfo.FullName) == ".txt")
                        dictPathList.Add(fsinfo.FullName);
                }
            }

            return dictPathList;
        }

        #region 日志
        public delegate void LogAppendDelegate(Color color, string text);

        /// <summary> 
        /// 追加显示文本 
        /// </summary> 
        /// <param name="color">文本颜色</param> 
        /// <param name="text">显示文本</param> 
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
        /// 显示警告信息 
        /// </summary> 
        /// <param name="text"></param> 
        public void LogWarning(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            this.logTextBox.Invoke(la, Color.Violet, DateTime.Now + "----" + text);
        }
        /// <summary> 
        /// 显示一般信息 
        /// </summary> 
        /// <param name="text"></param> 
        public void LogMessage(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            this.logTextBox.Invoke(la, Color.Black, DateTime.Now + "----" + text);
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

        private void SetConfig()
        {
            //TODO 其他配置项待加入

            config.ShowCodes = this.statusCodeTextBox.Text;
            config.Method = this.httpMethodCombo.Text;
            config.ThreadSize = int.Parse(this.threadSizeCombo.Text);
            config.TimeOut = int.Parse(this.timeOutCombo.Text);
            config.SleepTime = int.Parse(this.sleepTimeCombo.Text);
        }


        private void StartBtn_Click(object sender, EventArgs e)
        {
            if (!CheckStartOption())
                return;
            //this.startBtn.Enabled = false;

            this.listView1.Items.Clear();
            this.logTextBox.Text = string.Empty;

            SetConfig();

            //TODO 先默认单个吧
            scanURL = tools.UpdateUrl(this.urlTextBox.Text.Trim(), false);

            selectedDict = new List<string>();
            foreach(ListViewItem name in this.dictListView.CheckedItems)
            {
                selectedDict.Add(name.Tag.ToString());
            }

            stp = new SmartThreadPool
            {
                MaxThreads = config.ThreadSize
            };

            this.scanTimer.Start();
            sThread = new Thread(new ThreadStart(ScanThread));
            sThread.Start();
        }


        private bool CheckStartOption()
        {
            if(stp.InUseThreads > 0)
            {
                HelpUtil.ShowMessageBox("上次任务还没停止，请停止上次任务！");
                return false;
            }

            return true;
        }



        private void ScanThread()
        {
            scanDirCount = 0;

            ServerInfo tmpSvinfo = new ServerInfo
            {
                host = tools.UpdateUrl(scanURL, false),
                id = this.scanDirCount,
                type = "指纹"  //TODO ?
            };
            tmpSvinfo.url = tmpSvinfo.host;
            stp.WaitFor(1000, 10000);
            stp.QueueWorkItem<ServerInfo>(ScanUrl, tmpSvinfo);
            stp.WaitForIdle();

            LogMessage(tmpSvinfo.url + "加载完成，开始扫描目录。");
            
            

            

            foreach (string dictName in selectedDict)
            {
                if (!this.dictContent.ContainsKey(dictName))
                {
                    LogError("字典" + dictName + "未加载成功！");
                    continue;
                }

                this.dictContent.TryGetValue(dictName, out List<string> urlList);
                foreach(string url in urlList)
                {
                    this.scanDirCount++;

                    ServerInfo svinfo = new ServerInfo();
                    svinfo.target = scanURL;
                    svinfo.host = tools.UpdateUrl(scanURL, true);
                    svinfo.id = this.scanDirCount;
                    svinfo.type = "目录";
                    svinfo.path = url;
                    svinfo.url = svinfo.host + url;

                    stp.WaitFor(1000, 10000);
                    stp.QueueWorkItem<ServerInfo>(ScanExistsDirs, svinfo);
                }
            }

            stp.WaitForIdle();
            stp.Shutdown();
            StopScan();

        }

        private void ScanUrl(Object obj)
        {
            ServerInfo svinfo = (ServerInfo)obj;

            ServerInfo result = HttpRequest.SendRequestGetHeader(config, svinfo.url, config.TimeOut, config.keeAlive);
            svinfo.code = result.code;
            svinfo.ip = tools.GetIP(svinfo.host);
            svinfo.contentType = result.contentType;
            svinfo.length = result.length;
            svinfo.server = result.server;
            svinfo.powerBy = result.powerBy;
            svinfo.runTime = result.runTime;
            //this.Invoke(new DelegateAddItemToListView(AddItemToListView), svinfo);
            //Thread.Sleep(config.SleepTime * 1000);
        }

        private void ScanExistsDirs(ServerInfo svinfo)
        {
            ServerInfo result = new ServerInfo();
            LogInfo("开始扫描-----" + svinfo.url);

            if (config.scanMode == 2)
            {
                result = HttpRequest.SendRequestGetBody(config, svinfo.url, config.TimeOut, config.keeAlive);
            }
            else
            {
                result = HttpRequest.SendRequestGetHeader(config, svinfo.url, config.TimeOut, config.keeAlive);
            }

            svinfo.code = result.code;

            if (svinfo.code != 0)
            {
                String location = result.location;
                svinfo.contentType = result.contentType;
                svinfo.length = result.length;
                svinfo.server = result.server;
                svinfo.powerBy = result.powerBy;
                svinfo.runTime = result.runTime;
                //关键字扫描
                if (config.scanMode >= 1)
                {
                    if (svinfo.code == 302 && location.IndexOf(svinfo.path) != -1)
                    {
                        svinfo.code = 301;
                    }

                    else if (config.scanMode == 2 && result.body != null)
                    {
                        String[] keys404 = config.key.Split(',');
                        if (keys404.Count() > 0)
                        {
                            foreach (String key in keys404)
                            {
                                //TODO 这个逻辑得再整理一下
                                if(svinfo.code == 404)
                                {
                                    break;
                                }
                                if (result.body.IndexOf(key) != -1 && config.isExists == 1)
                                {
                                    svinfo.code = 404;
                                    break;
                                }
                                if (result.body.IndexOf(key) == -1 && config.isExists == 0 && svinfo.code == 200)
                                {
                                    svinfo.code = 404;
                                    break;
                                }

                                if (result.body.IndexOf(key) != -1 && config.isExists == 0)
                                {
                                    svinfo.code = 200;
                                    break;
                                }

                            }
                        }
                    }
                }

                if (config.show == 0)
                {
                    if (config.ShowCodes.IndexOf(svinfo.code.ToString()) != -1)
                    {
                        this.Invoke(new DelegateAddItemToListView(AddItemToListView), svinfo);
                    }
                }
                else
                {
                    if (config.ShowCodes.IndexOf(svinfo.code.ToString()) == -1)
                    {
                        this.Invoke(new DelegateAddItemToListView(AddItemToListView), svinfo);
                    }
                }
            }
            Thread.Sleep(config.SleepTime * 1000);
        }


        public void StopScan()
        {
            this.Invoke(new VoidDelegate(this.scanTimer.Stop));
            this.Invoke(new update(UpdateStatus));
            //this.startBtn.Enabled = true;
            LogMessage("扫描结束");
        }

        private void UpdateStatus()
        {
            //try
            //{
            //    if (stp != null)
            //    {
            //        long workCount = allCrackCount;

            //        this.stxt_speed.Text = (workCount - this.lastCount) + "";
            //        this.lastCount = workCount;
            //        this.stxt_threadStatus.Text = stp.InUseThreads + "/" + stp.Concurrency;

            //        int c = 0;
            //        if (this.creackerSumCount != 0)
            //        {
            //            c = (int)Math.Floor((workCount * 100 / (double)this.creackerSumCount));
            //            this.stxt_threadPoolStatus.Text = allCrackCount + "/" + this.creackerSumCount;
            //        }
            //        if (c <= 0)
            //        {
            //            c = 0;
            //        }
            //        if (c >= 100)
            //        {
            //            c = 100;
            //        }
            //        this.stxt_percent.Text = c + "%";
            //        this.tools_proBar.Value = c;
            //    }
            //    this.stxt_crackerSuccessCount.Text = successCount + "";
            //    this.stxt_useTime.Text = runTime + "";
            //    this.tssl_notScanPortsSumCount.Text = this.scanPortsSumCount + "";
            //}
            //catch (Exception e)
            //{
            //    LogWarning(e.Message);
            //}
        }


        public void AddItemToListView(ServerInfo svinfo)
        {
            //过滤类型不符合的
            if (!svinfo.contentType.StartsWith(config.contentType, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            //过滤长度不符合的
            bool filter = false;
            if (config.contentLength > -2)
            {

                switch (config.contentSelect)
                {
                    case 0:
                        if (svinfo.length < config.contentLength)
                        {
                            filter = true;
                        }

                        break;
                    case 1:
                        if (svinfo.length == config.contentLength)
                        {
                            filter = true;
                        }

                        break;
                    case 2:
                        if (svinfo.length > config.contentLength)
                        {
                            filter = true;
                        }
                        break;
                }

            }
            if (filter)
            {
                return;
            }
            ListViewItem lvi = new ListViewItem(svinfo.id + "");
            lvi.Tag = svinfo.type;
            lvi.SubItems.Add(svinfo.url);
            lvi.SubItems.Add(svinfo.code + "");
            lvi.SubItems.Add(svinfo.contentType + "");
            lvi.SubItems.Add(svinfo.length + "");
            lvi.SubItems.Add(svinfo.server + "");
            lvi.SubItems.Add(svinfo.runTime + "");
            //lvi.SubItems.Add(svinfo.ip + "");
            String result = svinfo.url + "----" + svinfo.code;
            lvi.Tag = svinfo.type;
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
            //FileTool.AppendLogToFile("logs/scan_" + DateTime.Now.ToString("yyyy-MM-dd") + ".log", result);
            this.listView1.Items.Add(lvi);
        }

        private void DictListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            groupBox2.Text = string.Format("字典（激活{0}个）", dictListView.CheckedItems.Count);
        }

        private void OpenDictPathBtn_Click(object sender, EventArgs e)
        {
            ProcessUtil.ProcessOpen(this.dictDirectory);
        }

        private void RefreshDictBtn_Click(object sender, EventArgs e)
        {
            RefreshDict();
        }

        private string GetFileLines(string path)
        {
            int lineCount = 0;

            List<string> contentList = new List<string>();
            FileStream fs_dir = null;
            StreamReader reader = null;
            try
            {
                fs_dir = new FileStream(path, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(fs_dir);

                string lineStr;

                while ((lineStr = reader.ReadLine()) != null)
                {
                    if (!lineStr.Equals(""))
                    {
                        contentList.Add(lineStr);
                        lineCount++;
                    }
                }
            }
            catch {  }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (fs_dir != null)
                {
                    fs_dir.Close();
                }
                dictContent.Add(Path.GetFileName(path), contentList);
            }


            return lineCount.ToString();
        }

        private void StopBtn_Click(object sender, EventArgs e)
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
                StopScan();
            }
        }
    }
}
