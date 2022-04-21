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
            string lineInfo = CheckLineInfo(contentArray);
            Tasks.Add(new VPNTaskConfig(ST.NowString(),
                                        lineInfo.Split('\t')[0],
                                        lineInfo.Split('\t')[1],
                                        lineInfo.Split('\t')[2],
                                        lineInfo.Split('\t')[3],
                                        lineInfo.Split('\t')[4],
                                        CheckAliveOneTaskAsyn(contentArray),
                                        contentArray[0].ToUpper(),
                                        "probeInfo",
                                        lineInfo.Split('\t')[5],
                                        string.Empty,
                                        string.Empty
                                        ));
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

        private string CheckLineInfo(string[] contentArray)
        {
            string info = string.Empty;
            StringBuilder lineInfo = new StringBuilder();
            switch (contentArray[0])
            {
                case "ss":
                    info = GetSSLine(lineInfo, contentArray[1]);
                    break;
                case "ssr":
                    info = GetSSRLine(lineInfo, contentArray[1]);
                    break;
                case "vmess":
                    info = GetVmessLine(lineInfo, contentArray[1]);
                    break;
                case "vless":
                    info = GetVlessLine(lineInfo, contentArray[1]);
                    break;
                case "trojan":
                    info = GetTrojanLine(lineInfo, contentArray[1]);
                    break;
            }
            if (info.Split('\t').Length < 6)
                info = string.Join("\t",new string[6]);
            return info;
        }

        private string GetSSLine(StringBuilder lineInfo, string content)
        {
            string info;
            string ipAndport = string.Empty;
            string methodAndPwd = string.Empty;
            if (content.Contains("#"))
            {
                info = GetBase64Str(content.Split("#")[0]);
                ipAndport = info.Split('@')[1];
                methodAndPwd = info.Split('@')[0];
            }
            else if(content.Contains("@"))
            {
                ipAndport = content.Split('@')[1];
                info = content.Split('@')[0];
                methodAndPwd = GetBase64Str(info);
            }
            if (methodAndPwd.Contains(":") == false && ipAndport.Contains(":") == false)
                return lineInfo.ToString();
            try
            {
                string remark = HexDecode(content.Split("#")[1].Split('-')[1].Replace("+", string.Empty));
                lineInfo.Append(content.Split("#")[1].Split('-')[0].Replace("+", string.Empty) + remark + "\t");
            }
            catch
            {
                lineInfo.Append(string.Empty + "\t");
            }
            lineInfo.Append(ipAndport.Split(':')[0]+"\t");
            lineInfo.Append(ipAndport.Split(':')[1].Replace("/", string.Empty) + "\t");
            lineInfo.Append(methodAndPwd.Split(':')[1] + "\t");
            lineInfo.Append(methodAndPwd.Split(':')[0] + "\t");
            lineInfo.Append(string.Empty);
            return lineInfo.ToString();
        }

        private string GetSSRLine(StringBuilder lineInfo, string content)
        {
            string info = GetBase64Str(content);
            if (info.Contains(":") == false)
                return lineInfo.ToString();
            string[] infoArray = info.Split(':');
            if (infoArray.Length < 5 || infoArray[0].IsNullOrEmpty() || infoArray[1].IsNullOrEmpty())
                return lineInfo.ToString();

            lineInfo.Append(string.Empty + "\t");
            infoArray[4] = string.Empty;
            lineInfo.Append(string.Join("\t", infoArray));
            return lineInfo.ToString();
        }
        
        private string GetVmessLine(StringBuilder lineInfo, string content)
        {
            string info = GetBase64Str(content);
            try
            {
                var dict = JsonConvert.DeserializeObject<Dictionary<object, object>>(info);
                lineInfo.Append(dict["ps"].ToString().Replace(" ", string.Empty).Replace("\t", string.Empty) + "\t");
                lineInfo.Append(dict["add"].ToString() + "\t");
                lineInfo.Append(dict["port"].ToString() + "\t");
                lineInfo.Append(dict["id"].ToString() + "\t");
                lineInfo.Append(dict["v"].ToString().Replace("2", "auto").Replace("0", "ase-128-gcm").Replace("1", "chacha20-poly1305") + "\t");
                lineInfo.Append(string.Empty);
            }
            catch
            {
                lineInfo.Append(info.Split(',')[0] + "\t");
                lineInfo.Append(info.Split(',')[1] + "\t");
                lineInfo.Append(info.Split(',')[2] + "\t");
                lineInfo.Append(info.Split(',')[4] + "\t");
                lineInfo.Append(info.Split(',')[3] + "\t");
                lineInfo.Append(string.Empty);
            }
            return lineInfo.ToString();
        }
        private string GetVlessLine(StringBuilder lineInfo, string content)
        {
            string[] infoArray = content.Split('?');
            if (infoArray.Length < 2 || !infoArray[0].Contains("@") || !infoArray[0].Contains(":"))
                return lineInfo.ToString();

            lineInfo.Append(HexDecode(infoArray[1].Split('#')[1]) + "\t");
            lineInfo.Append(infoArray[0].Split('@')[1].Split(':')[0] + "\t");
            lineInfo.Append(infoArray[0].Split('@')[1].Split(':')[1] + "\t");
            lineInfo.Append(infoArray[0].Split('@')[0] + "\t");
            lineInfo.Append(infoArray[1].Split('&')[0].Replace("encryption=", string.Empty) + "\t");
            lineInfo.Append(string.Empty);

            return lineInfo.ToString();
        }

        private string GetTrojanLine(StringBuilder lineInfo, string content)
        {
            string[] infoArray = content.Split('#');
            if (infoArray.Length < 2 || !infoArray[0].Contains("@") || !infoArray[0].Contains(":"))
                return lineInfo.ToString();

            lineInfo.Append(HexDecode(infoArray[1]).Replace("+", string.Empty) + "\t");
            lineInfo.Append(infoArray[0].Split('@')[1].Split(':')[0] + "\t");
            lineInfo.Append(infoArray[0].Split('@')[1].Split(':')[1] + "\t");
            lineInfo.Append(infoArray[0].Split('@')[0] + "\t");
            lineInfo.Append(string.Empty + "\t");
            lineInfo.Append(string.Empty);

            return lineInfo.ToString();
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

        private string HexDecode(string str)
        {
            string[] content = str.Split('%');
            byte[] arrByte = new byte[content.Length-1];

            string notByteStr = string.Empty;
            int index = 0;
            
            for (int i = 1; i < content.Length-1; i ++)
            {
                arrByte[index] = Convert.ToByte(content[i].Substring(0,2), 16);
                index++;
            }
            arrByte[content.Length - 2] = Convert.ToByte(content[content.Length-1].Substring(0, 2), 16);
            notByteStr += content[content.Length-1].Substring(2, content[content.Length-1].Length - 2);
            str = content[0] + Encoding.UTF8.GetString(arrByte) + notByteStr;
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
