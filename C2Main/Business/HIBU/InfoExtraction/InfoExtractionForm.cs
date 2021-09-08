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
            InfoExtractionUrl = "http://10.1.126.186:9000/HI_NLP/InformationExtraction";
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
            this.dataGridView1.Rows.Clear();

            using (GuarderUtil.WaitCursor)
            {
                List<string> textPathList = GetPicsByPath(textPath);
                foreach (string singleTextPath in textPathList)
                {
                    List<string> contentList = GetFileLines(singleTextPath);

                    for (int i = 0; i < contentList.Count; i++)
                    {
                        List<string> returnList = StartTask(contentList[i]);
                        string result1 = returnList[0];
                        string result2 = returnList[1];
                        FillDGV(singleTextPath, result1, result2);
                    }
                }
            }
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
        public List<string> StartTask(string strContent)
        {
            string data1 = string.Empty;
            string data2 = string.Empty;
            List<string> returnList = new List<string>();
            try
            {
                Response resp = httpHandler.PostCode(InfoExtractionUrl, "sentence=" + HttpUtility.UrlEncode(strContent), 60000);

                HttpStatusCode statusCode = resp.StatusCode;
                if (statusCode != HttpStatusCode.OK)
                {
                    returnList.Add(string.Format("查询失败! http状态码为：{0}.", statusCode));
                }

                Dictionary<string, string> resDict = resp.ResDict;

                if (resDict.TryGetValue("status", out string status))
                {
                    resDict.TryGetValue("results", out string datas);
                    data1 = status == "200" ? DealData(datas)[0] : datas;
                    data2 = status == "200" ? DealData(datas)[1] : datas;
                }
                else
                {
                    data1 = "查询失败! status不存在。";
                    data2 = "查询失败! status不存在。";
                }
            }
            catch (Exception e)
            {
                data1 = "查询失败!" + e.Message;
                data2 = "查询失败!" + e.Message;
            }
            returnList.Add(data1);
            returnList.Add(data2);
            return returnList;
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
            }
            return contentList;
        }
        private List<string> DealData(string data)
        {
            List<String> resultList = new List<string>();

            if (string.IsNullOrEmpty(data))//jarray.parse解析空字符串报错
            {
                resultList.Add(string.Empty);
                resultList.Add(string.Empty);
                return resultList;
            }
            try
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                Dictionary<string, string> json = jss.Deserialize<Dictionary<string, string>>(data);
                if (json.Count == 0)
                {
                    resultList.Add("");
                    resultList.Add("");
                    return resultList;
                }

                List<string> keyList = new List<string>();
                foreach (string item in json.Keys)
                {
                    keyList.Add(item);
                }
                for (int i = 0; i < json.Count; i++)
                {
                    JArray js = JArray.Parse(json[keyList[i]]);
                    string joint = string.Empty;
                    foreach (JValue item in js)
                    {
                        joint = joint + item.ToString();
                    }
                    resultList.Add(joint);
                }
                if (resultList.Count == 1)
                {
                    resultList.Add("");
                }
            }
            catch
            {
                resultList.Add("解析出错，可尝试重新识别。");
                resultList.Add("");
                return resultList;
            }
            return resultList;
        }
        private void FillDGV(string singleTextPath, string result1, string result2)
        {
            DataGridViewRow dr = new DataGridViewRow();

            DataGridViewTextBoxCell textCell0 = new DataGridViewTextBoxCell();
            textCell0.Value = Path.GetFileName(singleTextPath);
            dr.Cells.Add(textCell0);

            DataGridViewTextBoxCell textCell1 = new DataGridViewTextBoxCell();
            textCell1.Value = result1;
            dr.Cells.Add(textCell1);

            DataGridViewTextBoxCell textCell2 = new DataGridViewTextBoxCell();
            textCell2.Value = result2;
            dr.Cells.Add(textCell2);

            dataGridView1.Rows.Add(dr);
        }

        protected override bool OnOKButtonClick()
        {
            if (this.dataGridView1.Rows.Count == 0)
            {
                HelpUtil.ShowMessageBox("结果为空，无法保存。");
                return false;
            }

            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() != DialogResult.OK)
                return false;

            using (GuarderUtil.WaitCursor)
            {
                SaveResultToLocal(dialog.SelectedPath);
            }
            HelpUtil.ShowMessageBox("保存完毕。");

            return false;
        }

        private void SaveResultToLocal(string path)
        {
            StreamWriter sw = new StreamWriter(Path.Combine(path, "信息抽取结果.txt"));
            sw.Write("文件名" + "\t" + "地址" + "\t" + "姓名" + "\n");
            try
            {

                foreach (DataGridViewRow row in this.dataGridView1.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null && row.Cells[2].Value != null)
                    {
                        sw.Write(row.Cells[0].Value.ToString() + "\t" + row.Cells[1].Value.ToString() + "\t" + row.Cells[2].Value.ToString() + "\n");
                    }
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
