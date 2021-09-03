using C2.Business.HTTP;
using C2.Controls;
using C2.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Windows.Forms;

namespace C2.Business.HIBU.OCR
{
    partial class OCRForm : StandardDialog
    {
        private string picPath;
        readonly HttpHandler httpHandler;
        private readonly string OCRUrl;

        public OCRForm()
        {
            InitializeComponent();
            this.OKButton.Text = "保存结果";
            this.CancelBtn.Text = "退出";

            httpHandler = new HttpHandler();
            OCRUrl = "http://10.1.126.186:9000/HI_CV/OCR";
        }

        private void BrowserBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog = new OpenFileDialog
            {
                Filter = "图片 | *.png;*.jpg"
            };
            if (OpenFileDialog.ShowDialog() != DialogResult.OK)
                return;

            this.filePathTextBox.Text = OpenFileDialog.FileName;
            picPath = OpenFileDialog.FileName;
        }

        private void FolderBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.filePathTextBox.Text = dialog.SelectedPath;
                picPath = dialog.SelectedPath;
            }
        }

        private void TransBtn_Click(object sender, EventArgs e)
        {
            //清空上一次的查询结果
            this.dataGridView1.Rows.Clear();

            using (GuarderUtil.WaitCursor)
            {
                List<string> picPathList = GetPicsByPath(picPath);

                foreach (string singlePicPath in picPathList)
                {
                    string result = StartTask(TransBase64(singlePicPath));
                    FillDGV(singlePicPath, result);
                }
            }
            HelpUtil.ShowMessageBox("识别完成。");
        }

        

        private List<string> GetPicsByPath(string path)
        {
            string[] picSuffix = new string[] { ".png", ".jpg", ".jpeg" };
            List<string> picPathList = new List<string>();
            if(Directory.Exists(path))
            {
                foreach (FileSystemInfo fsinfo in new DirectoryInfo(path).GetFiles())
                {
                    if(picSuffix.Contains(Path.GetExtension(fsinfo.FullName)))
                        picPathList.Add(fsinfo.FullName);
                }
            }
            else if(File.Exists(path))
            {
                picPathList.Add(path);
            }

            return picPathList;
        } 


        private string TransBase64(string singlePicPath)
        {
            string base64Str = string.Empty;

            using (FileStream filestream = new FileStream(singlePicPath, FileMode.Open))
            {
                byte[] bt = new byte[filestream.Length];
                //调用read读取方法
                filestream.Read(bt, 0, bt.Length);
                base64Str = Convert.ToBase64String(bt);
                filestream.Close();
            }

            return base64Str;
        }

        public string StartTask(string base64Str)
        {
            string data = string.Empty;

            try
            {
                Response resp = httpHandler.PostCode(OCRUrl, "imageBase64="+ HttpUtility.UrlEncode(base64Str), 60000);

                HttpStatusCode statusCode = resp.StatusCode;

                if (statusCode != HttpStatusCode.OK)
                {
                    return string.Format("查询失败! http状态码为：{0}.", statusCode);
                }
                    

                Dictionary<string, string> resDict = resp.ResDict;

                if (resDict.TryGetValue("status", out string status))
                {
                    resDict.TryGetValue("results", out string datas);
                    data = status == "200" ? DealData(datas) : datas;
                }
                else
                    data = "查询失败! status不存在。"; 
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

            DataGridViewTextBoxCell textCell1 = new DataGridViewTextBoxCell();
            textCell1.Value = result;
            dr.Cells.Add(textCell1);

            dataGridView1.Rows.Add(dr);
        }

        private string DealData(string data)
        {
            List<String> resultList = new List<string>();

            if (string.IsNullOrEmpty(data))//jarray.parse解析空字符串报错
                return string.Empty;
            try
            {
                JArray json = JArray.Parse(data);
                foreach (JValue item in json)
                {
                    resultList.Add(item.ToString());
                }
            }
            catch { return "解析出错，可尝试重新识别。"; }


            return string.Join("\n", resultList);
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
            StreamWriter sw = null;
            try
            {
                foreach (DataGridViewRow row in this.dataGridView1.Rows)
                {
                    if(row.Cells[0].Value != null && row.Cells[1].Value != null)
                    {
                        sw = new StreamWriter(Path.Combine(path, row.Cells[0].Value.ToString() + ".txt"));
                        sw.Write(row.Cells[1].Value.ToString());
                        if (sw != null)
                            sw.Close();
                    }
                }
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
