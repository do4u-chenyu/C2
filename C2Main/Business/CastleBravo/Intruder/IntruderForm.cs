using Amib.Threading;
using C2.Business.CastleBravo.WebScan.Tools;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.Intruder
{
    public partial class IntruderForm : Form
    {
        private SmartThreadPool stp = new SmartThreadPool();
        private Thread sThread = null;
        private Config.Config config;
        delegate void VoidDelegate();
        delegate void update();

        private int scanRunTime = 0;//已扫描时间
        private long lastCount = 0;
        private long scanSumCount = 0;//扫描目录总数

        string[] splitLine;
        string lastLine = string.Empty;
        //Boolean flag = true;
        string dictDirectory;
        Dictionary<string, List<string>> dictContent;
        //private List<string> domainList;



        public IntruderForm()
        {
            InitializeComponent();
            config = new Config.Config();
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
            var lines = tBReqMess.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
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

            /*
            int ss = tBReqMess.Text.LastIndexOf("=");
            int sst = tBReqMess.Text.LastIndexOf("\n");
            int ttt = tBReqMess.Text.Length;
            */

            
            if (lastLine.Contains("=") && splitLine.Length >= 2 &&
                (splitLine[splitLine.Length - 2] == string.Empty || splitLine[splitLine.Length - 2] == "\r"))
            {
                tBReqMessSetting(tBReqMess.Text.LastIndexOf("\n") + 1, tBReqMess.Text.LastIndexOf("=") - tBReqMess.Text.LastIndexOf("\n") - 1, Color.Blue);
                if(!tBReqMess.Text.Contains("§"))
                    tBReqMessSetting(tBReqMess.Text.LastIndexOf("=") + 1, tBReqMess.Text.Length - tBReqMess.Text.LastIndexOf("=") - 1, Color.Red);
                tBReqMessSetting(tBReqMess.Text.LastIndexOf("") + 1, tBReqMess.Text.Length - tBReqMess.Text.LastIndexOf("=") - 1, Color.Black);
            }
            //flag = true;
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
        }

        private bool CheckStartOption()
        {
            if (stp.InUseThreads > 0)
            {
                HelpUtil.ShowMessageBox("上次任务还没停止，请停止上次任务！");
                return false;
            }

            /*
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
            */
            

            if (this.dictListView.CheckedItems.Count == 0)
            {
                HelpUtil.ShowMessageBox("请选择扫描字典！");
                return false;
            }
            if (this.tBReqMess.Text == string.Empty)
            {
                HelpUtil.ShowMessageBox("请输入请求报文！");
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
            try
            {
                tBReqMess.Text = tBReqMess.Text.Replace(tBReqMess.SelectedText, "§" + tBReqMess.SelectedText + "§");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
    }
}