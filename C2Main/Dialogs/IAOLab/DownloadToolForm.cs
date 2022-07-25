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
        private string emailPath;
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

        private void BrowserBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog = new OpenFileDialog
            {
                Filter = "邮件 | *.eml"
            };
            if (OpenFileDialog.ShowDialog() != DialogResult.OK)
                return;

            this.filePathTextBox.Text = OpenFileDialog.FileName;
            emailPath = OpenFileDialog.FileName;
        }

        private void FolderBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.filePathTextBox.Text = dialog.SelectedPath;
                emailPath = dialog.SelectedPath;
            }
        }

        private void TransBtn_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.emailDataGridView.Rows.Clear();
            new Log.Log().LogManualButton(emailParaseTabPage.Text, "运行");
            using (GuarderUtil.WaitCursor)
            {
                List<string> emlPathList = GetEmlsByPath(emailPath);
                if (emlPathList.Count > 1000)
                    emlPathList.RemoveRange(1000, emlPathList.Count - 1000);
                foreach (string singleEmailPath in emlPathList)
                {
                    if (Path.GetExtension(singleEmailPath) != ".eml")
                        continue;
                    string result = ParaseEML(singleEmailPath);
                    FillDGV(singleEmailPath, result);
                }
            }
            this.Cursor = Cursors.Arrow;
            HelpUtil.ShowMessageBox("邮件信息提取完成。");
        }

        private List<string> GetEmlsByPath(string path)
        {
            List<string> picPathList = new List<string>();
            if (Directory.Exists(path))
            {
                foreach (FileSystemInfo fsinfo in new DirectoryInfo(path).GetFiles())
                {
                    if (Path.GetExtension(fsinfo.FullName) == ".eml")
                        picPathList.Add(fsinfo.FullName);
                }
            }
            else if (File.Exists(path))
            {
                picPathList.Add(path);
            }

            return picPathList;
        }

        private string ParaseEML(string singleEmailPath)
        {
            StringBuilder sb = new StringBuilder();
            CDO.Message oMsg = new CDO.Message();
            ADODB.Stream stm = null;
            //读取EML文件到CDO.MESSAGE
            try
            {
                stm = new ADODB.Stream();
                stm.Open(System.Reflection.Missing.Value,
                ADODB.ConnectModeEnum.adModeUnknown,
                ADODB.StreamOpenOptionsEnum.adOpenStreamUnspecified,
                "", "");
                stm.Type = ADODB.StreamTypeEnum.adTypeText;//二进制方式读入
                stm.LoadFromFile(singleEmailPath); //将EML读入数据流
                oMsg.DataSource.OpenObject(stm, "_stream"); //将EML数据流载入到CDO.Message，要做解析的话，后面就可以了。

                sb.Append(oMsg.From.Replace("\t", string.Empty).Replace("\"", string.Empty) + "\t");   //发件人
                sb.Append(oMsg.To.Replace("\t", string.Empty).Replace("\"", string.Empty) + "\t");   //收件人  
                sb.Append(oMsg.Subject.Replace("\t", string.Empty) + "\t");   //标题
                sb.Append(oMsg.ReceivedTime.ToString().Replace("\t", string.Empty) + "\t");  //时间

                string contentType = "未知";
                try
                {
                    string content = string.Empty;
                    using (FileStream fs = new FileStream(singleEmailPath, FileMode.Open, FileAccess.Read))
                    using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                        content = sr.ReadToEnd().Trim();
                    if (oMsg.TextBody.IsNullOrEmpty())
                    {
                        oMsg.TextBody = "正文内容提取失败";
                        string[] array = content.Split("\r\nContent-Type:");
                        if (array.Length > 2)
                        {
                            oMsg.TextBody = array[array.Length - 1].Replace("text/html", string.Empty)
                                                                    .Replace("text/plain", string.Empty)
                                                                    .Replace("multipart/alternative", string.Empty)
                                                                    .Replace("\n", " ").Replace("\r", " ");
                            contentType = "multipart/alternative";
                        }
                        else if (array.Length == 2)
                        {
                            oMsg.TextBody = array[1].Replace("text/html", string.Empty)
                                                    .Replace("text/plain", string.Empty)
                                                    .Replace("\n", " ").Replace("\r", " ");
                            Match match = Regex.Match(content, @"\r\nContent-Type:(.*?)\r\n");
                            if (match.Groups[match.Groups.Count - 1].Success)
                                contentType = match.Groups[match.Groups.Count - 1].Value;
                            else if (array[1].Trim() == "text/html" || array[1].Trim() == "text/plain")
                                contentType = array[1];
                        }
                    }
                    else
                    {
                        oMsg.TextBody = oMsg.TextBody.Replace("\n", " ").Replace("\r", " ");

                        Match match = Regex.Match(content, @"\r\nContent-Type:(.*?)\r\n");
                        if (match.Groups[match.Groups.Count - 1].Success)
                            contentType = match.Groups[match.Groups.Count - 1].Value;
                    }
                }
                catch { }

                sb.Append(contentType.Replace("\t", string.Empty).Trim(';') + "\t");   //邮件的正文类型
                sb.Append(oMsg.TextBody.Replace("\t", string.Empty).Trim().Trim(';'));   //邮件的正文

                int count = oMsg.Attachments.Count;
                for (int i = 1; i <= count; i++)
                {
                    ////获取到附件的文件名称+后缀
                    object FileName = oMsg.Attachments[i+1].FileName;
                    if (FileName.ToString().IsNullOrEmpty())
                        continue;
                    //将附件存储到指定位置
                    oMsg.Attachments[i].SaveToFile(Path.Combine(Path.GetDirectoryName(singleEmailPath), FileName.ToString()));
                }
            }
            catch (Exception ex)
            {
                sb.Append("邮件解析失败，" + ex.Message + "\t" + "\t" + "\t" + "\t" + "\t");
            }
            finally
            {
                stm.Close();
            }
            return sb.ToString();
        }

        private void FillDGV(string singleEmailPath, string result)
        {
            string[] array = result.Split('\t');
            if (array.Length != 6)
                return;
            DataGridViewRow dr = new DataGridViewRow();
            DataGridViewTextBoxCell textCell0 = new DataGridViewTextBoxCell();
            textCell0.Value = Path.GetFileName(singleEmailPath);
            dr.Cells.Add(textCell0);

            foreach(string value in array)
            {
                DataGridViewTextBoxCell textCell = new DataGridViewTextBoxCell();
                textCell.Value = value.IsNullOrEmpty() ? "未知" : value;
                dr.Cells.Add(textCell);
            }
            emailDataGridView.Rows.Add(dr);
        }
        private void SaveEmail_Click(object sender, EventArgs e)
        {
            if (this.emailDataGridView.Rows.Count == 0)
            {
                HelpUtil.ShowMessageBox("结果为空，无法保存。");
                return;
            }
            using (GuarderUtil.WaitCursor)
            {
                string savePath = string.Empty;
                if (Directory.Exists(emailPath))
                    savePath = Path.Combine(emailPath, "邮件提取结果_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".txt");
                else if (File.Exists(emailPath))
                    savePath = Path.Combine(Path.GetDirectoryName(emailPath), "邮件提取结果_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".txt");
                else
                {
                    var dialog = new FolderBrowserDialog();
                    if (dialog.ShowDialog() != DialogResult.OK)
                        return;
                    savePath = dialog.SelectedPath;
                }
                SaveResultToLocal(savePath);
            }
        }

        private void SaveResultToLocal(string path)
        {
            StreamWriter sw = null;
            try
            {
                List<string> headerList = new List<string> { "邮件名", "发件人", "收件人", "邮件主题", "发送时间", "正文类型", "正文" };
                sw = new StreamWriter(path);
                sw.WriteLine(string.Join("\t", headerList));
                foreach (DataGridViewRow row in this.emailDataGridView.Rows)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (DataGridViewTextBoxCell textCell in row.Cells)
                    {
                        if (textCell.Value != null)
                            sb.Append(textCell.Value.ToString().Replace("\r\n", string.Empty) + "\t");
                        else
                            sb.Append(string.Empty);
                    }
                    sw.WriteLine(sb.ToString());
                }
                if (sw != null)
                    sw.Close();
                HelpUtil.ShowMessageBox("提取结果已保存至"+ path);
            }
            catch(Exception ex)
            {
                HelpUtil.ShowMessageBox("邮件信息提取结果保存失败，" + ex.Message);
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }
    }
}
