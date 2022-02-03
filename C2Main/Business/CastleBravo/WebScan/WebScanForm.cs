using Amib.Threading;
using C2.Business.CastleBravo.WebScan.Model;
using C2.Business.CastleBravo.WebScan.Tools;
using C2.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
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

        private List<string> domainList;

        //private string scanURL;
        private long scanDirCount = 0;
        private long scanSumCount = 0;//扫描目录总数
        private List<string> selectedDict;

        private long lastCount = 0;
        private int scanRunTime = 0;//已扫描时间

        delegate void VoidDelegate();
        delegate void update();
        delegate void DelegateAddItemToListView(ServerInfo svinfo);

        public WebScanForm()
        {
            InitializeComponent();
            tools = new Tool();
            config = new Config();
            domainList = new List<string>();

            this.headerCombo.SelectedIndex = 0;
            this.httpMethodCombo.SelectedIndex = 0;
            this.threadSizeCombo.SelectedIndex = 4;
            this.timeOutCombo.SelectedIndex = 2;
            this.sleepTimeCombo.SelectedIndex = 0;

            this.dictDirectory = Path.Combine(Application.StartupPath, "Resources", "WebScanDict");
            RefreshDict();
        }

        #region 字典配置
        private void RefreshDict()
        {
            //TODO 考虑一下把文件内容加入字典，计算行数和放入字典可以写在一起
            this.dictContent = new Dictionary<string, List<string>>();
            this.dictListView.Items.Clear();

            int dictCount = 0;

            foreach (string dictPath in GetDictByPath(this.dictDirectory))
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
            catch { }
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
        private void DictListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            groupBox2.Text = string.Format("字典（激活{0}个）", dictListView.CheckedItems.Count);
        }
        //字典目录
        private void OpenDictPathBtn_Click(object sender, EventArgs e)
        {
            ProcessUtil.ProcessOpen(this.dictDirectory);
        }
        //刷新字典
        private void RefreshDictBtn_Click(object sender, EventArgs e)
        {
            RefreshDict();
        }

        #endregion

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

        #region 其他配置、停止按钮
        private void SetConfig()
        {
            //TODO 其他配置项待加入

            config.ShowCodes = this.statusCodeTextBox.Text;//状态码
            config.Method = this.httpMethodCombo.Text;//http方法
            config.ThreadSize = int.Parse(this.threadSizeCombo.Text);//线程数
            config.TimeOut = int.Parse(this.timeOutCombo.Text);//超时
            config.SleepTime = int.Parse(this.sleepTimeCombo.Text);//延时
        }
        private void ExportBtn_Click(object sender, EventArgs e)
        {
            ExportResults();
        }

        //停止扫描功能
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
                this.Invoke(new VoidDelegate(StopScan));
            }
        }
        #endregion

        #region 开始扫描
        private bool CheckStartOption()
        {
            if (stp.InUseThreads > 0)
            {
                HelpUtil.ShowMessageBox("上次任务还没停止，请停止上次任务！");
                return false;
            }

            domainList.Clear();
            foreach (string domain in urlTextBox.Text.Split('\n'))
            {
                string tmpDomain = domain.Trim(new char[] { '\r', '\n' });
                if (string.IsNullOrEmpty(tmpDomain))
                    continue;

                //TODO 这里可以考虑自动拼接一个http头
                if (!tmpDomain.StartsWith("http"))
                {
                    HelpUtil.ShowMessageBox("域名:" + tmpDomain + "未包含http或https");
                    return false;
                }
                domainList.Add(tmpDomain.Split('#')[0]);
            }

            if (this.dictListView.CheckedItems.Count == 0)
            {
                HelpUtil.ShowMessageBox("请选择扫描字典！");
                return false;
            }
            return true;
        }

        //开始扫描功能
        private void StartBtn_Click(object sender, EventArgs e)
        {
            if (!CheckStartOption())
                return;

            this.startBtn.Enabled = false;
            this.listView1.Items.Clear();
            this.logTextBox.Text = string.Empty;

            scanSumCount = 0;
            selectedDict = new List<string>();
            foreach (ListViewItem name in this.dictListView.CheckedItems)
            {
                selectedDict.Add(name.Tag.ToString());
                scanSumCount += int.Parse(name.SubItems[2].Text);
            }
            scanSumCount = scanSumCount * domainList.Count() + domainList.Count();//扫域名本身和其字典目录

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
            foreach (string domain in domainList)
            {
                this.scanDirCount++;
                ServerInfo tmpSvinfo = new ServerInfo
                {
                    host = tools.UpdateUrl(domain, false),
                    id = scanDirCount,
                    type = "指纹"  //TODO ?
                };
                tmpSvinfo.url = tmpSvinfo.host;
                stp.WaitFor(1000, 10000);
                stp.QueueWorkItem<ServerInfo>(ScanDomain, tmpSvinfo);
                LogMessage(tmpSvinfo.url + "加载完成，开始扫描目录");
                stp.WaitForIdle();


                foreach (string dictName in selectedDict)
                {
                    if (!this.dictContent.ContainsKey(dictName))
                    {
                        LogError("字典" + dictName + "未加载成功！");
                        continue;
                    }

                    this.dictContent.TryGetValue(dictName, out List<string> urlList);
                    foreach (string url in urlList)
                    {
                        this.scanDirCount++;

                        ServerInfo svinfo = new ServerInfo();
                        svinfo.target = domain;
                        svinfo.host = tools.UpdateUrl(domain, true);
                        svinfo.id = this.scanDirCount;
                        svinfo.type = "目录";
                        svinfo.path = url;
                        svinfo.url = svinfo.host + url;

                        stp.WaitFor(1000, 10000);
                        stp.QueueWorkItem<ServerInfo>(ScanExistsDirs, svinfo);
                    }
                }
            }

            stp.WaitForIdle();
            stp.Shutdown();
            this.Invoke(new VoidDelegate(StopScan));

        }

        private void ScanDomain(ServerInfo svinfo)
        {
            ServerInfo result = HttpRequest.SendRequestGetBody(config, svinfo.url, config.TimeOut, config.keeAlive);

            if (result.code != 0)
            {
                svinfo.code = result.code;
                svinfo.ip = tools.GetIP(svinfo.host);
                svinfo.contentType = result.contentType;
                svinfo.length = result.length;
                svinfo.server = result.server;
                svinfo.powerBy = result.powerBy;
                svinfo.runTime = result.runTime;
                this.Invoke(new DelegateAddItemToListView(AddItemToListView), svinfo);
            }
            else
                LogError(svinfo.url + "-----" + result.contentType);

            //Thread.Sleep(config.SleepTime * 1000);
        }
        public void AddItemToListView(ServerInfo svinfo)
        {
            //过滤类型不符合的
            if (!svinfo.contentType.StartsWith(config.contentType, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            ListViewItem lvi = new ListViewItem(svinfo.id + "");
            lvi.Tag = svinfo.type;
            lvi.SubItems.Add(svinfo.url);
            lvi.SubItems.Add(svinfo.code + "");
            lvi.SubItems.Add(svinfo.contentType + "");
            lvi.SubItems.Add(svinfo.length + "");
            lvi.SubItems.Add(svinfo.runTime + "");
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
                svinfo.length = result.length == -1 ? result.body.Length : result.length;
                svinfo.server = result.server;
                svinfo.powerBy = result.powerBy;
                svinfo.runTime = result.runTime;

                if (config.ShowCodes.Contains(svinfo.code.ToString()))
                    this.Invoke(new DelegateAddItemToListView(AddItemToListView), svinfo);

                if (loopCheckBox.Checked && svinfo.code == 403 && this.dictContent.ContainsKey("PHP一级目录.txt")) //403遍历模式，命中403的url加入队列再次扫描
                {
                    List<string> urlList = this.dictContent["PHP一级目录.txt"];

                    scanSumCount += urlList.Count;//下方进度条显示总数也要相应增加
                    foreach (string url in urlList)
                    {
                        this.scanDirCount++;

                        ServerInfo loop = new ServerInfo();
                        loop.target = svinfo.url;
                        loop.host = tools.UpdateUrl(svinfo.url, true);
                        loop.id = this.scanDirCount;
                        loop.type = "目录";
                        loop.path = url;
                        loop.url = loop.host + url;

                        stp.WaitFor(1000, 10000);
                        stp.QueueWorkItem<ServerInfo>(ScanExistsDirs, loop);
                    }
                }
                
            }
            else
                LogError("扫描失败-----" + svinfo.url + "-----" + result.contentType);

            Thread.Sleep(config.SleepTime * 1000);
        }


        public void StopScan()
        {
            this.Invoke(new VoidDelegate(this.scanTimer.Stop));
            this.Invoke(new update(UpdateStatus));
            this.startBtn.Enabled = true;
            LogMessage("扫描结束");
        }
        #endregion

        #region 结果展示相关
       

        private void OpenUrl_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0)
            {
                return;
            }
            try
            {
                System.Diagnostics.Process.Start("IEXPLORE.EXE", this.listView1.SelectedItems[0].SubItems[1].Text);
            }
            catch (Exception oe)
            {
                MessageBox.Show("打开URL发生异常---" + oe.Message);
            }
        }

        private void CopyUrl_Click(object sender, EventArgs e)
        {
            CopyUrls();
        }

        private void CopyUrls()
        {
            if (this.listView1.SelectedItems.Count == 0)
                return;

            List<string> copyUrls = new List<string>();
            foreach (ListViewItem lvi in this.listView1.SelectedItems)
                copyUrls.Add(lvi.SubItems[1].Text);

            //顺序反过来会导致剪贴板里面是messagebox内容
            MessageBox.Show("复制选中url成功");
            Clipboard.SetDataObject(string.Join("\n", copyUrls));
        }

        private void ExportResults_Click(object sender, EventArgs e)
        {
            ExportResults();
        }


        private void ExportResults()
        {
            //保存文件
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "文本文件|*.txt"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate, System.IO.FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    string columns = string.Empty;
                    foreach (ColumnHeader dc in this.listView1.Columns)
                    {
                        columns += (dc.Text + "\t");
                    }
                    sw.WriteLine(columns.Substring(0, columns.Length - 1));
                    foreach (ListViewItem sv in this.listView1.Items)
                    {
                        List<string> tmpRow = new List<string>();
                        foreach (ListViewItem.ListViewSubItem subv in sv.SubItems)
                        {
                            tmpRow.Add(subv.Text);
                        }
                        //TODO 似乎多出最后一列
                        tmpRow.Remove(tmpRow.Last());
                        sw.WriteLine(string.Join("\t", tmpRow));
                    }
                    sw.Close();
                    MessageBox.Show("导出完成");
                }
                catch (Exception e)
                {
                    MessageBox.Show("导出数据发生异常" + e.Message);
                }
            }

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

        private void ListView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (listView1.Columns[e.Column].Tag == null)
            {
                listView1.Columns[e.Column].Tag = true;
            }
            bool tabK = (bool)listView1.Columns[e.Column].Tag;
            if (tabK)
            {
                listView1.Columns[e.Column].Tag = false;
            }
            else
            {
                listView1.Columns[e.Column].Tag = true;
            }
            listView1.ListViewItemSorter = new ListViewSort(e.Column, listView1.Columns[e.Column].Tag);
            // 指定排序器并传送列索引与升序降序关键字
            listView1.Sort(); // 对列表进行自定义排序
        }

        public class ListViewSort : IComparer
        {
            private int col;
            private bool descK;

            public ListViewSort()
            {
                col = 0;
            }
            public ListViewSort(int column, object Desc)
            {
                descK = (bool)Desc;
                col = column;  // 当前列,0,1,2...,参数由ListView控件的ColumnClick事件传递
            }
            public int Compare(object x, object y)
            {
                int tempInt = String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
                if (descK)
                {
                    return -tempInt;
                }
                else
                {
                    return tempInt;
                }
            }
        }

        private void ListView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
                CopyUrls();
        }


        private void LoginCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (loginCheckBox.Checked)
            {
                foreach (ListViewItem lvi in dictListView.Items)
                {
                    lvi.Checked = lvi.SubItems[1].Text.StartsWith("入口_") ? true : lvi.Checked;
                }
            }
        }

        private void EditorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (editorCheckBox.Checked)
            {
                foreach (ListViewItem lvi in dictListView.Items)
                {
                    lvi.Checked = lvi.SubItems[1].Text.StartsWith("编辑器_") ? true : lvi.Checked;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in dictListView.Items)
                lvi.Checked = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in dictListView.Items)
                lvi.Checked = false;
            editorCheckBox.Checked = false;
            loginCheckBox.Checked = false;
        }

        private void HelpLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string helpfile = Path.Combine(Application.StartupPath, "Resources", "Help", "WebScan帮助文档.txt");
                Help.ShowHelp(this, helpfile);
            }
            catch { };
        }

    }
}
