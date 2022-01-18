using C2.Business.CastleBravo.WebScan.Tools;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.Intruder
{
    public partial class IntruderForm : Form
    {
        private Config.Config config;
        string[] splitLine;
        string lastLine = string.Empty;
        //Boolean flag = true;
        string dictDirectory;
        Dictionary<string, List<string>> dictContent;
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
            this.dictLV.Items.Clear();

            int dictCount = 0;

            foreach (string dictPath in config.GetDictByPath(this.dictDirectory))
            {
                dictCount++;

                ListViewItem lvi = new ListViewItem(dictCount + "");
                lvi.Tag = Path.GetFileName(dictPath);
                lvi.SubItems.Add(Path.GetFileName(dictPath));
                lvi.SubItems.Add(config.GetFileLines(dictPath, this.dictContent));
                lvi.SubItems.Add(config.GetFileSize(dictPath));

                this.dictLV.Items.Add(lvi);
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
        

        private void startButton_Click(object sender, System.EventArgs e)
        {

        }

        private void suspendButton_Click(object sender, System.EventArgs e)
        {

        }

        private void stopButton_Click(object sender, System.EventArgs e)
        {

        }
        //验证IP是否可用
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
            foreach (ListViewItem lvi in dictLV.Items)
                lvi.Checked = true;
        }
        //全不选
        private void noSelected_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in dictLV.Items)
                lvi.Checked = false;
        }
        private void dictLV_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            groupBox2.Text = string.Format("字典（激活{0}个）", dictLV.CheckedItems.Count);
        }
    }
}