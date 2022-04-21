using C2.Controls;
using C2.Core;
using C2.Utils;
using Newtonsoft.Json;
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
            array[0] = ST.NowString();
            array[6] = CheckAliveOneTaskAsyn(array);
            array[8] = "probeInfo";
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
                    array = GetVmessLine(array, contentArray);
                    Tasks.Add(new VPNTaskConfig(array));
                    break;
                case "vless":
                    array = GetVlessLine(array, contentArray);
                    Tasks.Add(new VPNTaskConfig(array));
                    break;
                case "trojan":
                    array = GetTrojanLine(array, contentArray);
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
            array[1] = string.Empty;
            array[2] = ipAndport[1].Split(':')[0];
            array[3] = ipAndport[1].Split(':')[1].Replace("/", string.Empty);
            array[4] = methodAndPwd.Split(':')[1];
            array[5] = methodAndPwd.Split(':')[0];
            array[7] = "Shadowsocks";
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
            array[1] = string.Empty;
            array[2] = infoArray[0];
            array[3] = infoArray[1];
            array[4] = infoArray[2];
            array[5] = infoArray[3];
            array[7] = "ShadowsocksR";
            return array;
        }
        
        private string[] GetVmessLine(string[] array, string[] contentArray)
        {
            string info = GetBase64Str(contentArray[1]);
            try
            {
                var dict = JsonConvert.DeserializeObject<Dictionary<object, object>>(info);
                array[1] = dict["ps"].ToString().Replace(" ", string.Empty);
                array[2] = dict["add"].ToString();
                array[3] = dict["port"].ToString();
                array[4] = dict["id"].ToString();
                array[5] = dict["v"].ToString().Replace("2", "auto").Replace("0", "ase-128-gcm").Replace("1", "chacha20-poly1305");
            }
            catch
            {
                array[1] = info.Split(',')[0];
                array[2] = info.Split(',')[1];
                array[3] = info.Split(',')[2];
                array[4] = info.Split(',')[4];
                array[5] = info.Split(',')[3];
            }
            
            array[7] = "Vmess";
            return array;
        }
        private string[] GetVlessLine(string[] array, string[] contentArray)
        {
            string[] infoArray = contentArray[1].Split('?');
            if (infoArray.Length < 2 || !infoArray[0].Contains("@") || !infoArray[0].Contains(":"))
                return array;

            array[1] = infoArray[1].Split('#')[1];
            array[1] = HexDecode_16(array[1]);
            array[2] = infoArray[0].Split('@')[1].Split(':')[0];
            array[3] = infoArray[0].Split('@')[1].Split(':')[1];
            array[4] = infoArray[0].Split('@')[0];
            array[5] = infoArray[1].Split('&')[0].Replace("encryption=", string.Empty);
            array[7] = "Vless";
            return array;
        }

        private string[] GetTrojanLine(string[] array, string[] contentArray)
        {
            string[] infoArray = contentArray[1].Split('#');
            if (infoArray.Length < 2 || !infoArray[0].Contains("@") || !infoArray[0].Contains(":"))
                return array;

            array[1] = HexDecode_16(infoArray[1]);
            array[2] = infoArray[0].Split('@')[1].Split(':')[0];
            array[3] = infoArray[0].Split('@')[1].Split(':')[1];
            array[4] = infoArray[0].Split('@')[0];
            array[5] = string.Empty;
            array[7] = "Trojan";
            
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

        private string HexDecode_16(string str)
        {
            str = str.Replace("%", string.Empty);
            string resStr = string.Empty;
            if (str.Length % 2 != 0)
            {
                resStr = str.Substring(str.Length - 1);
                str = str.Substring(0, str.Length - 1);
            } 
            byte[] arrByte = new byte[str.Length / 2];
            string notByteStr = string.Empty;
            int index = 0;
            
            for (int i = 0; i < str.Length; i += 2)
            {
                try
                {
                    arrByte[index] = Convert.ToByte(str.Substring(i, 2), 16);
                    index++;
                }
                catch
                {
                    notByteStr += str.Substring(i, str.Length-i);
                    break;
                }
            }
            byte[] arrByteCopy = new byte[index];
            Array.Copy(arrByte, arrByteCopy, index);
            str = Encoding.UTF8.GetString(arrByteCopy) + notByteStr + resStr;
            return str;
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
