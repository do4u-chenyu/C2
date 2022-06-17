using C2.Controls;
using C2.Core;
using C2.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Dialogs.IAOLab
{
    public partial class DownloadToolForm : BaseDialog
    {
        private string SavePath;
        public DownloadToolForm()
        {
            InitializeComponent();
            this.SavePath = string.Empty;
        }

        private void Import_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog1 = new OpenFileDialog
            {
                Filter = "文本文档 | *.txt;*.csv;*.bcp;*.tsv"
            };
            if (OpenFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            try
            {
                string path = OpenFileDialog1.FileName;
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    StringBuilder sb = new StringBuilder();
                    // 从文件读取并显示行，直到文件的末尾 
                    while ((line = sr.ReadLine()) != null)
                    {
                        sb.Append(line);
                        sb.Append("\n");
                    }

                    richTextBox1.Text = sb.TrimEndN().ToString();
                    if (tabControl1.Visible == false)
                        richTextBox1.Text = sb.TrimEndN().ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            StringBuilder tmpResult = new StringBuilder();

            this.Cursor = Cursors.WaitCursor;
            string firstLine;

            if (tabControl1.SelectedTab == DouYinTabPage && tabControl1.Visible == true)
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("请先选择结果保存路径。");
                }
                SavePath = dialog.SelectedPath;
                new Log.Log().LogManualButton("实验楼" + "-" + DouYinTabPage.Text, "运行");
                string[] inputArray = this.richTextBox1.Text.Split('\n');
                progressBar1.Value = 0;
                progressBar1.Maximum = GetRelLengthOfArry(inputArray);
                progressBar1.Minimum = 0;
                firstLine = "抖音视频链接\t下载结果文件名\t下载状态\n";
                tmpResult.Append(firstLine);
                foreach (string douyinLink in inputArray)
                {
                    ShowResult(douyinLink, tmpResult);
                    if (progressBar1.Value == progressBar1.Maximum && progressBar1.Maximum != 0)
                    {
                        MessageBox.Show("下载完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        progressBar1.Value = 0;
                    }
                }
            }
            this.Cursor = Cursors.Arrow;
        }

        private int GetRelLengthOfArry(string[] arry)
        {
            int relLength = 0;
            foreach (string i in arry) 
            {
                if (!string.IsNullOrEmpty(i.Split('\t')[0].Replace(OpUtil.Blank.ToString(), string.Empty)))
                    relLength++;
            }
            return relLength;
        }

        private void ShowResult(string input, StringBuilder tmpResult)
        {
            if (!string.IsNullOrEmpty(input) && progressBar1.Value < 5001 && !string.IsNullOrEmpty(input.Split('\t')[0].Replace(OpUtil.Blank.ToString(), string.Empty)))
            {
                if (progressBar1.Value % 100 == 0)
                    Thread.Sleep(500);
                tmpResult.Append(string.Format("{0}{1}{2}", input, "\t", GetDownLoadTool(input), "\n"));
                richTextBox1.Text = tmpResult.ToString();
                progressBar1.Value += 1;
            }
        }
        

        public string GetDownLoadTool(string link)
        {
            Match match = Regex.Match(link, @"video/(.*?)/");
            if (!match.Groups[1].Success || match.Groups[1].Value.Length != 19)
                return string.Empty + "\t" + "链接格式错误";

            string id = match.Groups[1].Value;
            try
            {
                string userAgent = "Mozilla/ 5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 87.0.4280.88 Safari / 537.36";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://www.iesdouyin.com/web/api/v2/aweme/iteminfo/?item_ids=" + id);
                request.Method = "GET";
                request.Accept = "*/*";
                request.Timeout = 10000;
                request.AllowAutoRedirect = false;
                request.UserAgent = userAgent;
                WebResponse begurl = (WebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(begurl.GetResponseStream(), Encoding.UTF8);
                string ss = reader.ReadToEnd();

                JObject json = JObject.Parse(ss);
                JToken itemList = json["item_list"].ToList()[0];
                string videoURL = itemList["video"]["play_addr"]["url_list"].ToList()[0].ToString();
                videoURL = videoURL.Replace("playwm", "play");
                string savePath = SavePath + id + ".mp4";

                HttpWebRequest web = (HttpWebRequest)WebRequest.Create(videoURL);
                web.Method = "GET";
                web.UserAgent = userAgent;
                HttpWebResponse response = (HttpWebResponse)web.GetResponse();
                FileStream file = new FileStream(savePath, FileMode.Create);
                response.GetResponseStream().CopyTo(file);
            }
            catch(Exception ex)
            {
                return id + ".mp4\t" + "下载失败,"+ ex.Message;
            }
            return id + ".mp4\t" + "已完成";
        }

        private void Check_Click(object sender, EventArgs e)
        {
            ProcessUtil.TryOpenDirectory(this.SavePath);
        }
    }
}
