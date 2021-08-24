﻿using C2.Business.HTTP;
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
            OCRUrl = "http://10.1.126.186:8970/HI_CV/OCR";
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

            Dictionary<string, string> pairs = new Dictionary<string, string> { { "imageBase64", base64Str } };
            string data = string.Empty;

            try
            {
                Response resp = httpHandler.PostCode(OCRUrl, "imageBase64="+ HttpUtility.UrlEncode(base64Str));

                HttpStatusCode statusCode = resp.StatusCode;

                if (statusCode != HttpStatusCode.OK)
                {
                    HelpUtil.ShowMessageBox(string.Format("http 状态码：{0}.", statusCode));
                    return data;
                }
                    

                Dictionary<string, string> resDict = resp.ResDict;

                if (resDict.TryGetValue("status", out string status))
                {
                    resDict.TryGetValue("results", out string datas);
                    data = datas;
                    if(status != "200")
                        HelpUtil.ShowMessageBox("状态码非200");
                }
                else
                    HelpUtil.ShowMessageBox("查询失败");
            }
            catch (Exception e)
            {
                HelpUtil.ShowMessageBox(e.Message);
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
            textCell1.Value = DealData(result);
            dr.Cells.Add(textCell1);

            dataGridView1.Rows.Add(dr);
        }

        private string DealData(string data)
        {
            List<String> resultList = new List<string>();

            JArray json = JArray.Parse(data);
            foreach (JValue item in json)
            {
                resultList.Add(item.ToString());
            }

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
