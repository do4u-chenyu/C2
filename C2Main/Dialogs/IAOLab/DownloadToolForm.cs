using C2.Controls;
using C2.Core;
using C2.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; 
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
            this.check.Enabled = false;
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
            //string path = @"C:\Users\Administrator\Desktop\test2.eml";
            //ReadEML(path);
            this.Cursor = Cursors.WaitCursor;
            string firstLine;

            if (tabControl1.SelectedTab == DouYinTabPage)
            {
                if (richTextBox1.Text.IsNullOrEmpty())
                {
                    MessageBox.Show("请先导入下载链接。");
                    this.Cursor = Cursors.Arrow;
                    return;
                }

                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "请选择下载结果保存路径";
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("请先选择下载结果保存路径。");
                    this.Cursor = Cursors.Arrow;
                    return;
                }

                string currentTime = DateTime.Now.ToString("yyyyMMddhhmmss");

                SavePath = Path.Combine(dialog.SelectedPath, "抖音视频下载结果_" + currentTime);
                Directory.CreateDirectory(SavePath);
                this.check.Enabled = true;
                new Log.Log().LogManualButton("实验楼" + "-" + DouYinTabPage.Text, "运行");

                string[] inputArray = richTextBox1.Text.Split('\n');
                progressBar1.Value = 0;
                progressBar1.Maximum = GetRelLengthOfArry(inputArray);
                progressBar1.Minimum = 0;
                firstLine = "抖音视频链接\t下载状态\t下载结果文件名\n";
                tmpResult.Append(firstLine);
                foreach (string douyinLink in inputArray)
                {
                    ShowResult(douyinLink, tmpResult);
                    if (progressBar1.Value == progressBar1.Maximum && progressBar1.Maximum != 0)
                    {
                        MessageBox.Show("抖音视频下载完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        progressBar1.Value = 0;
                    }
                }
                SaveStatusResult(tmpResult.ToString(), currentTime);
            }
            this.Cursor = Cursors.Arrow;
        }

        //private void ReadEML(string emailPath)
        //{
        //    string file = emailPath;
        //    CDO.Message oMsg = new CDO.Message();
        //    ADODB.Stream stm = null;
        //    //读取EML文件到CDO.MESSAGE
        //    try
        //    {
        //        stm = new ADODB.Stream();
        //        stm.Open(System.Reflection.Missing.Value,
        //        ADODB.ConnectModeEnum.adModeUnknown,
        //        ADODB.StreamOpenOptionsEnum.adOpenStreamUnspecified,
        //        "", "");
        //        stm.Type = ADODB.StreamTypeEnum.adTypeText;//二进制方式读入

        //        stm.LoadFromFile(file); //将EML读入数据流

        //        oMsg.DataSource.OpenObject(stm, "_stream"); //将EML数据流载入到CDO.Message，要做解析的话，后面就可以了。
        //        StringBuilder sb = new StringBuilder();
        //        //邮件的内容
        //        sb.Append(oMsg.TextBody+"\t");
        //        //发件人
        //        sb.Append(oMsg.From + "\t");

        //        //收件人
        //        sb.Append(oMsg.To + "\t");

        //        //标题
        //        sb.Append(oMsg.Subject + "\t");
        //        //时间
        //        sb.Append(oMsg.ReceivedTime.ToString() + "\t");

        //        sb.Append(oMsg.Attachments.Count.ToString() + "\t");

        //        int count = oMsg.Attachments.Count;
        //        for (int i = 1; i <= count; i++)
        //        {
        //            ////获取到附件的文件名称+后缀
        //            object FileName = oMsg.Attachments[i].FileName;

        //            //将附件存储到指定位置
        //            oMsg.Attachments[i].SaveToFile(@"F:\" + FileName);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("邮件解析失败，" + ex.Message, "ERROR");
        //    }
        //    finally
        //    {
        //        stm.Close();
        //    }

        //}
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
            if (!string.IsNullOrEmpty(input) && progressBar1.Value < 501 && !string.IsNullOrEmpty(input.Split('\t')[0].Replace(OpUtil.Blank.ToString(), string.Empty)))
            {
                if (progressBar1.Value % 100 == 0)
                    Thread.Sleep(500);
                if (input.Contains("抖音视频链接"))
                {
                    progressBar1.Value += 1;
                    return;
                }
                if (input.Split('\t').Length == 3 && input.Split('\t')[0].StartsWith("https:"))
                    input = input.Split('\t')[0];
                string result = string.Empty;
                for(int i=0; i < 2; i++)
                {
                    result = GetDownLoadTool(input);
                    if (!result.Contains("远程服务器返回错误"))
                        break;
                }
                tmpResult.Append(string.Format("{0}\t{1}\n", input, result));
                richTextBox1.Text = tmpResult.ToString();
                progressBar1.Value += 1;
            }
        }
        

        public string GetDownLoadTool(string link)
        {
            Match match = Regex.Match(link, @"video/(.*?)/");
            if (!match.Groups[1].Success || match.Groups[1].Value.Length != 19 || !link.StartsWith("https:"))
                return "下载链接格式错误" + "\t" + string.Empty;

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
                List<JToken> itemList = json["item_list"].ToList();
                if (itemList.Count == 0)
                    return  "下载失败,作品不存在\t" + id + ".mp4";
                string videoURL = itemList[0]["video"]["play_addr"]["url_list"].ToList()[0].ToString();
                videoURL = videoURL.Replace("playwm", "play");
                string savePath = Path.Combine(SavePath, id + ".mp4");

                HttpWebRequest web = (HttpWebRequest)WebRequest.Create(videoURL);
                web.Method = "GET";
                web.UserAgent = userAgent;
                HttpWebResponse response = (HttpWebResponse)web.GetResponse();
                FileStream file = new FileStream(savePath, FileMode.Create);
                response.GetResponseStream().CopyTo(file);
                file.Close();
            }
            catch(Exception ex)
            {
                string error = ex.Message.Replace(Environment.NewLine, string.Empty).Replace("\t", string.Empty);
                return "下载失败,"+ error + "\t" + id + ".mp4";
            }
            return  "已完成\t" + id + ".mp4";
        }

        private void SaveStatusResult(string result, string time)
        {
            string statusFilePath = Path.Combine(SavePath, "下载详情结果_" + time + ".txt");
            try
            {
                using (StreamWriter fs = new StreamWriter(statusFilePath))
                {
                    if (result == string.Empty)
                        return;
                    fs.Write(result);
                    fs.Flush();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出下载详情结果失败，"+ ex.Message, "ERROR");
            }
        }

        private void Check_Click(object sender, EventArgs e)
        {
            ProcessUtil.TryOpenDirectory(this.SavePath);
        }
    }
}
