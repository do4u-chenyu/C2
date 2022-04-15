using C2.Controls;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.VPN
{
    partial class BatchAddVPNServerForm : StandardDialog
    {
        readonly int maxRow;
        string FilePath { get => this.filePathTextBox.Text; set => this.filePathTextBox.Text = value; }
        public List<VPNTaskConfig> Tasks;
        public BatchAddVPNServerForm()
        {
            InitializeComponent();
            maxRow = 10000;
            FilePath = string.Empty;
            Tasks = new List<VPNTaskConfig>();
            OKButton.Size = new System.Drawing.Size(75, 27);
            CancelBtn.Size = new System.Drawing.Size(75, 27);
        }

        private void PasteModeCB_CheckedChanged(object sender, EventArgs e)
        {
            this.wsTextBox.Clear();
            this.filePathTextBox.Clear();

            this.browserButton.Enabled = !this.pasteModeCB.Checked;

            this.wsTextBox.Enabled = this.pasteModeCB.Checked;
            this.wsTextBox.ReadOnly = !this.pasteModeCB.Checked;
        }

        private void BrowserButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog = new OpenFileDialog
            {
                Filter = "文件 | *.txt",
                FileName = FilePath
            };
            if (OpenFileDialog.ShowDialog() != DialogResult.OK)
                return;

            this.FilePath = OpenFileDialog.FileName;
        }

        protected override bool OnOKButtonClick()
        {
            return (this.pasteModeCB.Checked ? GenTasksFromPaste() : GetTasksFromFile()) && base.OnOKButtonClick();
        }

        private bool GenTasksFromPaste()
        {
            if (this.wsTextBox.Text.Trim().IsEmpty())
                return false;
            // 如果粘贴文件不合格,就别清空旧数据了
            Tasks.Clear();

            string[] lines = this.wsTextBox.Text.SplitLine();
            for (int i = 0; i < Math.Min(lines.Length, maxRow); i++)
                AddTasksByLine(lines[i]);

            return true;
        }


        private void AddTasksByLine(string line)
        {
            string[] contentArray = Regex.Split(line.Trim(new char[] { '\r', '\n' }), @"://");

            if (contentArray.Length < 2 || contentArray[0].IsNullOrEmpty() || contentArray[1].IsNullOrEmpty())
                return;
            string[] array = new string[9];
            switch (contentArray[0])
            {
                case "ss":
                    array = GetSSLine(array, contentArray);
                    Tasks.Add(new VPNTaskConfig(array));
                    break;
                case "ssr":
                    array = GetSSRLine(array, contentArray);
                    Tasks.Add(new VPNTaskConfig(array));
                    break;
                case "vmess":
                    Tasks.Add(new VPNTaskConfig(array));
                    break;
            }
        }

        private bool GetTasksFromFile()
        {
            Tasks.Clear();

            if (!File.Exists(FilePath))
            {
                HelpUtil.ShowMessageBox("该数据文件不存在");
                return false;
            }
            try
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                    for (int row = 0; row < maxRow && !sr.EndOfStream; row++)
                        AddTasksByLine(sr.ReadLine());
            }
            catch
            {
                HelpUtil.ShowMessageBox(FilePath + ",文件加载出错，请检查文件内容。");
                return false;
            }
            return true;
        }
        private string[] GetSSLine(string[] array, string[] contentArray)
        {
            string[] ipAndport = contentArray[1].Split('@');
            if (ipAndport.Length < 2 || ipAndport[0].IsNullOrEmpty() || ipAndport[1].IsNullOrEmpty())
                return array;
            string methodAndPwd = Encoding.UTF8.GetString(Convert.FromBase64String(ipAndport[0]));
            if (methodAndPwd.Contains(":") == false)
                return array;
            array[0] = ST.NowString();
            array[1] = string.Empty;
            array[2] = ipAndport[1].Split(':')[0];
            array[3] = ipAndport[1].Split(':')[1].Replace("/", string.Empty);
            array[4] = methodAndPwd.Split(':')[1];
            array[5] = methodAndPwd.Split(':')[0];
            array[7] = "Shadowsocks";
            array[8] = "probeInfo";
            array[6] = CheckAliveOneTaskAsyn(array);
            return array;
        }

        private string[] GetSSRLine(string[] array, string[] contentArray)
        {
            string info = GetBase64Str(contentArray[1]);
            if (info.Contains(":") == false)
                return array;
            string[] infoArray = info.Split(':');
            if (infoArray.Length < 5 || infoArray[0].IsNullOrEmpty() || infoArray[1].IsNullOrEmpty())
                return array;
            array[0] = ST.NowString();
            array[1] = string.Empty;
            array[2] = infoArray[0];
            array[3] = infoArray[1];
            array[4] = infoArray[2];
            array[5] = infoArray[3];
            array[6] = "√";
            array[7] = "ShadowsocksR";
            array[8] = "probeInfo";
            return array;
        }

        public string GetBase64Str(string base64Str)
        {
            string info = string.Empty;
            base64Str = Uri.UnescapeDataString(Uri.UnescapeDataString(Uri.UnescapeDataString(base64Str)));
            if (IsBase64Formatted(base64Str))
                info = Encoding.UTF8.GetString(Convert.FromBase64String(base64Str));
            else
            {
                int baseLengh = base64Str.Length;
                int i;
                for (i = 0; i < baseLengh; i++)
                {
                    base64Str = base64Str.Substring(0, baseLengh - i);
                    if (!IsBase64Formatted(base64Str))
                        continue;
                    info = Encoding.UTF8.GetString(Convert.FromBase64String(base64Str));
                    break;
                }
            }
            return info;
        }

        public static bool IsBase64Formatted(string input)
        {
            try
            {
                Convert.FromBase64String(input);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private string CheckAliveOneTaskAsyn(string[] array)
        {
            if (CheckAlive(array))
                return "√";
            return "×";
        }

        private bool CheckAlive(string[] array)
        {
            try
            {
                return true;
            }
            catch { return false; }
        }
    }
}
