using C2.Business.HTTP;
using C2.Controls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using C2.Utils;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Web;
using System.Linq;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using C2.Core;

namespace C2.Business.HIBU.InfoExtraction
{
    partial class InfoExtractionForm : StandardDialog
    {
        private string textPath;
        readonly HttpHandler httpHandler;
        private readonly string InfoExtractionUrl;
        public InfoExtractionForm()
        {
            InitializeComponent();
            this.OKButton.Text = "保存结果";
            this.CancelBtn.Text = "退出";

            httpHandler = new HttpHandler();
            InfoExtractionUrl = Global.ServerHIUrl + "/HI_NLP/InformationExtraction";
        }

        private void SingleButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog = new OpenFileDialog
            {
                Filter = "文档 | *.txt"
            };
            if (OpenFileDialog.ShowDialog() != DialogResult.OK)
                return;

            this.textPathTextBox.Text = OpenFileDialog.FileName;
            textPath = OpenFileDialog.FileName;
        }

        private void FolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textPathTextBox.Text = dialog.SelectedPath;
                textPath = dialog.SelectedPath;
            }
        }
        private void ExtractButton_Click(object sender, EventArgs e)
        {
            new Log.Log().LogManualButton(this.Text, "运行");
            this.dataGridView1.Rows.Clear();

            using (GuarderUtil.WaitCursor)
            {
                List<string> textPathList = GetPicsByPath(textPath);
                foreach (string singleTextPath in textPathList)
                {
                    List<string> contentPathList = GetFileLines(singleTextPath);
                    for (int i = 0; i < contentPathList.Count; i++)
                    {
                        string result = StartTask(contentPathList[i]);
                        FillDGV(singleTextPath, result);
                    }
                }
            }
            HelpUtil.ShowMessageBox("信息抽取完成。");
        }

        private List<string> GetPicsByPath(string path)
        {
            string[] textSuffix = new string[] { ".txt" };
            List<string> textPathList = new List<string>();
            if (Directory.Exists(path))
            {
                foreach (FileSystemInfo fsinfo in new DirectoryInfo(path).GetFiles())
                {
                    if (textSuffix.Contains(Path.GetExtension(fsinfo.FullName)))
                        textPathList.Add(fsinfo.FullName);
                }
            }
            else if (File.Exists(path))
            {
                textPathList.Add(path);
            }

            return textPathList;
        }
        public string StartTask(string base64Str)
        {
            string data = string.Empty;
            List<string> returnList = new List<string>();
            try
            {
                Response resp = httpHandler.PostCode(InfoExtractionUrl, "sentence= " + HttpUtility.UrlEncode(base64Str), 60000);

                HttpStatusCode statusCode = resp.StatusCode;
                if (statusCode != HttpStatusCode.OK)
                {
                    returnList.Add(string.Format("查询失败! http状态码为：{0}.", statusCode));
                }

                Dictionary<string, string> resDict = resp.ResDict;

                if (resDict.TryGetValue("status", out string status))
                {
                    resDict.TryGetValue("results", out string datas);
                    data = status == "200" ? DealData(datas) : datas;
                }
                else
                {
                    data = "查询失败! status不存在。";
                }
            }
            catch (Exception e)
            {
                data = "查询失败!" + e.Message;
            }
            return data;
        }

        private void FillDGV(string singlePicPath, string result)
        {
            DataGridViewRow dr = new DataGridViewRow();
            DataGridViewTextBoxCell textCell0 = new DataGridViewTextBoxCell();
            textCell0.Value = Path.GetFileName(singlePicPath);
            dr.Cells.Add(textCell0);
            try
            {
                for (int i = 0; i < contentList.Count; i++)
                {
                    DataGridViewTextBoxCell textCell = new DataGridViewTextBoxCell();
                    textCell.Value = listRealData[i];
                    dr.Cells.Add(textCell);
                }
            }
            catch
            {
            }
            dataGridView1.Rows.Add(dr);
        }
        List<String> listRealData = new List<string>();
        List<string> contentList = new List<string>() { "人名", "地点",  "手机号", "支付宝", "银行卡", "邮箱", "网址", "ip", "QQ号码", "QQ群名", "微信群名", "微信账号", "微信群号", "APP", "机构", "短链接" };

        private string DealData(string data)
        {
            List<String> resultList = new List<string>();
            if (string.IsNullOrEmpty(data))//jarray.parse解析空字符串报错
                return string.Empty;
            try
            {
                data = "[" + data + "]";
                JArray ja = (JArray)JsonConvert.DeserializeObject(data);
                for (int i =0; i < contentList.Count; i++)
                {
                    if (ja[0].ToString().Contains(contentList[i]) && ja[0][contentList[i]].ToString() != "[]")
                    {
                        string content = ja[0][contentList[i]].ToString().Split(new string[] { "[", "]" }, StringSplitOptions.RemoveEmptyEntries)[0].Replace("'", "");
                        resultList.Add(content);
                    }
                    else
                        resultList.Add(string.Empty);
                }

                listRealData = listData(resultList);
            }
            catch { return "解析出错，可尝试重新识别。"; }

            return string.Join("\n", resultList);
        }

        private List<string> GetFileLines(string path)
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
                    if (!string.IsNullOrEmpty(lineStr))
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
            }
            return contentList;
        }
        public List<String> listData(List<string> resultList)
        {
            return resultList;
        }

        protected override bool OnOKButtonClick()
        {
            if (this.dataGridView1.Rows.Count == 0)
            {
                HelpUtil.ShowMessageBox("结果为空，无法保存。");
                return false;
            }

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "文本文件|*.txt";
            dialog.FileName = "文本信息抽取结果" + DateTime.Now.ToString("yyyyMMddHHmm") + ".txt";

            if (dialog.ShowDialog() != DialogResult.OK)
                return false;

            using (GuarderUtil.WaitCursor)
            {
                SaveResultToLocal(dialog.FileName);
            }
            HelpUtil.ShowMessageBox("保存完毕。");

            return false;
        }

        private void SaveResultToLocal(string path)
        {
            string write = string.Empty;
            for (int i = 0; i < contentList.Count; i++)
            {
                write = write + contentList[i]+"\t";
            }
            StreamWriter sw = new StreamWriter(path, true);
            sw.Write("文件名" + "\t" + write + "\n");
            try
            {

                foreach (DataGridViewRow row in this.dataGridView1.Rows)
                {
                    string cellContent = string.Empty;
                    for (int i =0; i< row.Cells.Count; i++)
                    {
                        
                        if (row.Cells[i].Value == null)
                        {
                            cellContent = cellContent + "\t";
                            sw.Close();
                        }
                        else
                        {
                            cellContent = cellContent + row.Cells[i].Value.ToString() + "\t";
                        }
                    }
                   sw.Write(cellContent + "\n");
                }
                if (sw != null)
                    sw.Close();
            }
            catch { }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }
    }
}
