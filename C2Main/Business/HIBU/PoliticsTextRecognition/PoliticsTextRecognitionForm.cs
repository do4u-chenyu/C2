using C2.Business.HTTP;
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
using System.Web;
using System.Windows.Forms;

namespace C2.Business.HIBU.PoliticsTextRecognition
{
    partial class PoliticsTextRecognitionForm : StandardDialog
    {
        private string picPath;
        private readonly string OCRUrl;

        public PoliticsTextRecognitionForm()
        {
            InitializeComponent();
            OKButton.Text = "保存结果";
            CancelBtn.Text = "退出";
            //OCRUrl = Global.ServerHIUrl + "/HI_NLP/PoliticsTextRecognition";
            OCRUrl = "http://106.75.225.21:53371/politicDetect";
        }

        /// <summary>
        /// 单文本按钮对应的事件，获取一个txt文件的路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowserBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog = new OpenFileDialog
            {
                Filter = "文件 | *.txt"
            };
            if (OpenFileDialog.ShowDialog() != DialogResult.OK)
                return;
            filePathTextBox.Text = OpenFileDialog.FileName;
            picPath = OpenFileDialog.FileName;
        }

        /// <summary>
        /// 多文本按钮对应的事件，获取一个文件夹的路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FolderBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filePathTextBox.Text = dialog.SelectedPath;
                picPath = dialog.SelectedPath;
            }
        }

        /// <summary>
        /// 开始识别按钮对应的事件，遍历txt文件，获取文件内容，并进行请求接口实现涉政文本识别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TransBtn_Click(object sender, EventArgs e)
        {
            new Log.Log().LogManualButton(this.Text, "运行");
            //清空上一次的查询结果
            dataGridView1.Rows.Clear();

            using (GuarderUtil.WaitCursor)
            {
                List<string> picPathList = GetPicsByPath(picPath);  // 将所有txt文件保存至一个列表中

                foreach (string singlePicPath in picPathList)
                {
                    List<string> contentList = TransBase64(singlePicPath);
                    int.TryParse(contentList[0], out int rows);  // txt文件中共有多少行
                    string content = contentList[1];  // txt文件的文本内容
                    List<string> result = StartTask(content, rows);  // 读取文件并进行识别
                    FillDGV(singlePicPath, result);  // 数据保存至DGV中
                }
            }
            HelpUtil.ShowMessageBox("识别完成。");
        }

        /// <summary>
        /// 获取path中的文件名，将其保存到一个列表中，如果path是目录，遍历目录中的txt文件
        /// </summary>
        /// <param name="path"> 文件名或文件夹名 </param>
        /// <returns></returns>
        private List<string> GetPicsByPath(string path)
        {
            string[] picSuffix = new string[] { ".txt" };  // 仅遍历txt文件
            List<string> picPathList = new List<string>();  // 结果存放列表
            if (Directory.Exists(path))  // 文件夹的处理方式
            {
                foreach (FileSystemInfo fsinfo in new DirectoryInfo(path).GetFiles())
                {
                    if (picSuffix.Contains(Path.GetExtension(fsinfo.FullName)))
                        picPathList.Add(fsinfo.FullName);
                }
            }
            else if (File.Exists(path))  // 文件的处理方式
            {
                picPathList.Add(path);
            }
            return picPathList;
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="singlePicPath"> 文件名 </param>
        /// <returns></returns>
        private List<string> TransBase64(string singlePicPath)
        {
            string base64Str = string.Empty;
            List<string> res = new List<string>();

            using (FileStream filestream = new FileStream(singlePicPath, FileMode.Open))
            {
                StreamReader read = new StreamReader(filestream, Encoding.UTF8);  // 文件为utf8编码
                base64Str = read.ReadToEnd();
            }

            string[] strSplit = base64Str.Split("\n");
            int strLength = strSplit.Length;
            res.Add(strLength.ToString());
            res.Add(base64Str);
            return res;
        }

        /// <summary>
        /// 请求接口，获取识别结果，并对识别结果进行处理
        /// </summary>
        /// <param name="base64Str"> 要识别的字符串 </param>
        /// <param name="rows"> 文本行数 </param>
        /// <returns></returns>
        public List<string> StartTask(string base64Str, int rows)
        {
            List<string> res = new List<string>();
            try
            {
                string getData = "?data=" + base64Str.TrimEnd('\n');
                string targetUrl = OCRUrl + getData;
                HttpWebResponse resp = HttpGet(targetUrl);
                HttpStatusCode statusCode = resp.StatusCode;
                if (statusCode != HttpStatusCode.OK)
                {
                    string s = string.Format("查询失败! http状态码为：{0}.", statusCode);
                    res.Add(s);
                    return res;
                }
                string content = new StreamReader(resp.GetResponseStream()).ReadToEnd();
                JObject resDict = JObject.Parse(content);
                resDict.TryGetValue("status", out JToken status_jtoken);
                resDict.TryGetValue("msg", out JToken msg_jtoken);
                resDict.TryGetValue("result", out JToken result_jtoken);
                string status = status_jtoken.ToString();
                string msg = msg_jtoken.ToString();
                if (status == "200")
                {
                    int politicsNumber = 0;
                    string result = result_jtoken.ToString().Replace("\r\n", "").Replace("\n", "").Replace(" ", "");
                    res.Add(result);
                    if (result != "[]")
                    {
                        politicsNumber = result.Trim('[').Trim(']').Split(",").GetLength(0);
                    }
                    res.Add((politicsNumber*1.0 / rows * 100).ToString("f2")+"%");
                }
                else
                {
                    if (status == "1000")
                    {
                        res.Add("查询失败!数据为空");
                    }
                    else
                    {
                        res.Add("查询失败!" + msg);
                    }
                }
            }
            catch (Exception e)
            {
                res.Add("查询失败!" + e.Message);
            }
            return res;
        }

        public HttpWebResponse HttpGet(string url, int timeout=60000)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.Timeout = timeout;
            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            return response;
        }

        /// <summary>
        /// 填充DataGridView
        /// </summary>
        /// <param name="singlePicPath"> 当前处理txt文件对应的文件路径 </param>
        /// <param name="result"> 涉政文本的识别结果 </param>
        private void FillDGV(string singlePicPath, List<string> result)
        {

            DataGridViewRow dr = new DataGridViewRow();
            DataGridViewTextBoxCell textCell0 = new DataGridViewTextBoxCell();
            textCell0.Value = Path.GetFileName(singlePicPath);
            dr.Cells.Add(textCell0);

            if (result.Count() == 2)
            {
                string rowIndex = result[0];
                string ratio = result[1];

                DataGridViewTextBoxCell textCell1 = new DataGridViewTextBoxCell();
                textCell1.Value = rowIndex;
                dr.Cells.Add(textCell1);

                DataGridViewTextBoxCell textCell2 = new DataGridViewTextBoxCell();
                textCell2.Value = ratio;
                dr.Cells.Add(textCell2);
            }
            else
            {
                string msg = result[0];

                DataGridViewTextBoxCell textCell1 = new DataGridViewTextBoxCell();
                textCell1.Value = msg;
                dr.Cells.Add(textCell1);

                DataGridViewTextBoxCell textCell2 = new DataGridViewTextBoxCell();
                textCell2.Value = string.Empty;
                dr.Cells.Add(textCell2);
            }
            dataGridView1.Rows.Add(dr);
        }

        protected override bool OnOKButtonClick()
        {
            if (dataGridView1.Rows.Count == 0)
            {
                HelpUtil.ShowMessageBox("结果为空，无法保存。");
                return false;
            }
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "文本文件|*.txt";
            dialog.FileName = "涉政本文识别" + DateTime.Now.ToString("yyyyMMddHHmm") + ".txt";


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
            StreamWriter sw = new StreamWriter(path, true);
            sw.Write("文件名称" + OpUtil.StringBlank + "涉政文本所在行" + OpUtil.StringBlank + "涉政文本行数占比" + "\r\n");
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        sw.Write(row.Cells[0].Value.ToString() + OpUtil.StringBlank + row.Cells[1].Value.ToString() + OpUtil.StringBlank + row.Cells[2].Value.ToString() + "\r\n");
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
