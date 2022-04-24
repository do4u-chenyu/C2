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
            return (this.pasteModeCB.Checked ? GenTasksFromPaste() : GenTasksFromFile()) && base.OnOKButtonClick();
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
            Match mat = Regex.Match(line, @"^(?i)(ss|ssr|vmess|vless|trojan)(?-i)://(.+)$");
            if (!mat.Success)
                return;

            string version = mat.Groups[1].Value.ToLower();
            string content = mat.Groups[2].Value.Trim();

            string[] infoArray = Global.EmptyStringArray6;
            switch (version)
            {
                case "ss":
                    infoArray = GetSSLine(content);
                    break;
                case "ssr":
                    infoArray = GetSSRLine(content);
                    break;
                case "vmess":
                    infoArray = GetVmessLine(content);
                    break;
                case "vless":
                    infoArray = GetVlessLine(content);
                    break;
                case "trojan":
                    infoArray = GetTrojanLine(content);
                    break;
            }

            Tasks.Add(new VPNTaskConfig(ST.NowString(),
                                        infoArray[0].Trim(),
                                        infoArray[1].Trim(),
                                        infoArray[2].Trim(),
                                        infoArray[3].Trim(),
                                        infoArray[4].Trim(),
                                        string.Empty,         // CheckAliveOneTaskAsyn(contentArray),
                                        version.ToUpper(),
                                        "探针结果:未测",
                                        infoArray[5].Trim(),
                                        string.Empty,
                                        string.Empty
                                        ));
        }

        private bool GenTasksFromFile()
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
                        AddTasksByLine(sr.ReadLine().Trim());
            }
            catch
            {
                HelpUtil.ShowMessageBox(FilePath + ",文件加载出错，请检查文件内容。");
                return false;
            }
            return true;
        }

        private string[] GetSSLine(string content)
        {
            StringBuilder sb = new StringBuilder();
            string info;
            string ipAndport = string.Empty;
            string methodAndPwd = string.Empty;
            if (content.Contains("@"))
            {
                ipAndport = content.Split('@')[1];
                if (ipAndport.Contains("#"))
                    ipAndport = ipAndport.Split('#')[0];
                info = content.Split('@')[0];
                methodAndPwd = GetBase64Str(info); // 这一步
            }
            
            else if (content.Contains("#"))
            {
                info = GetBase64Str(content.Split("#")[0]);
                if (info.Contains("@"))
                {
                    ipAndport = info.Split('@')[1];
                    methodAndPwd = info.Split('@')[0];
                }
            }

            if (methodAndPwd.Contains(":") || ipAndport.Contains(":"))
            {
                try
                {
                    sb.Append(HexDecode(content.Split("#")[1]).Replace("\t", string.Empty) + "\t");
                }
                catch
                {
                    sb.Append("\t");
                }
                sb.Append(ipAndport.Split(':')[0]).Append('\t');
                sb.Append(ipAndport.Split(':')[1].Replace("/", string.Empty) + "\t");
                sb.Append(methodAndPwd.Split(':')[1] + "\t");
                sb.Append(methodAndPwd.Split(':')[0] + "\t");
            }

            return sb.Length == 0 ? "\t\t\t\t\t\t".Split('\t') : sb.ToString().Split('\t');
        }

        private string[] GetSSRLine(string content)
        {
            StringBuilder sb = new StringBuilder();
            string[] infoArray = GetBase64Str(content).Split(':');
            if (infoArray.Length < 6 || infoArray[0].IsNullOrEmpty() || infoArray[1].IsNullOrEmpty())
                return "\t\t\t\t\t\t".Split('\t');
            string remark = string.Empty;
            string otherinfo = string.Empty;
            
            if (infoArray[5].Contains("/?"))
            {
                string[] otherArray = infoArray[5].Split("/?")[1].Split('&');
                if (otherArray.Length == 4)
                {
                    remark = otherArray[2].Replace("remarks=", string.Empty).Replace("-", "+").Replace("_", "+");
                    otherinfo = "协议=" + infoArray[2] + ";协议参数=" + GetBase64Str(otherArray[1].Replace("protoparam=", string.Empty).Replace("-", "+").Replace("_", "+"))
                                + ";混淆=" + infoArray[4] + ";混淆参数=" + GetBase64Str(otherArray[0].Replace("obfsparam=", string.Empty).Replace("-", "+").Replace("_", "+"))
                                + ";Group=" + GetBase64Str(otherArray[3].Replace("group=", string.Empty).Replace("-", "+").Replace("_", "+"));
                    infoArray[5] = infoArray[5].Split("/?")[0];
                }
            }
            sb.Append(GetBase64Str(remark).Replace("\t", string.Empty) + "\t");
            sb.Append(infoArray[0] + "\t");
            sb.Append(infoArray[1] + "\t");
            sb.Append(GetBase64Str(infoArray[5]) + "\t");
            sb.Append(infoArray[3] + "\t");
            sb.Append(otherinfo + "\t");
            return sb.ToString().Split('\t');
        }
        
        private string[] GetVmessLine(string content)
        {
            StringBuilder sb = new StringBuilder();
            string info;
            if (content.Contains("?"))
            {
                info = GetBase64Str(content.Split('?')[0]);
                if (info.Split('@').Length < 2 || !info.Split('@')[0].Contains(":") || !info.Split('@')[0].Contains(":"))
                    return "\t\t\t\t\t\t".Split('\t');
                sb.Append(HexDecode(content.Split('?')[1].Split('&')[0].Replace("remarks=", string.Empty).Replace("remark=", string.Empty).Replace("\t", string.Empty)) + "\t");
                sb.Append(info.Split('@')[1].Split(':')[0].Replace("/", string.Empty) + "\t");
                sb.Append(info.Split('@')[1].Split(':')[1] + "\t");
                sb.Append(info.Split('@')[0].Split(':')[1] + "\t");
                sb.Append(info.Split('@')[0].Split(':')[0] + "\t");
                sb.Append(string.Join(";",content.Split('?')[1].Split('&')).Replace(content.Split('?')[1].Split('&')[0] + ";",string.Empty));
            }
            else
            {
                info = GetBase64Str(content);
                try
                {
                    var dict = JsonUtil.JsonToDictionary(info);
                    sb.Append(dict["ps"]  ).Append('\t')
                      .Append(dict["add"] ).Append('\t')
                      .Append(dict["port"]).Append('\t')
                      .Append(dict["id"]  ).Append('\t')
                      .Append(dict["v"] == "0" ? "ase-128-gcm" :
                              dict["v"] == "1" ? "chacha20-poly1305" :
                                                 "auto")
                      .Append('\t');
                }
                catch
                {
                    string[] array = info.Split(',');
                    sb.Append(array[0]).Append('\t')
                      .Append(array[1]).Append('\t')
                      .Append(array[2]).Append('\t')
                      .Append(array[4]).Append('\t')
                      .Append(array[3]).Append('\t');
                }
            }
            
            return sb.ToString().Split('\t');
        }
        private string[] GetVlessLine(string content)
        {
            StringBuilder sb = new StringBuilder();
            string[] infoArray = content.Split('?');
            if (infoArray.Length < 2 || !infoArray[0].Contains("@") || !infoArray[0].Contains(":"))
                return "\t\t\t\t\t\t".Split('\t');

            sb.Append(HexDecode(infoArray[1].Split('#')[1]).Replace("\t", string.Empty) + "\t");
            sb.Append(infoArray[0].Split('@')[1].Split(':')[0].Replace("/",string.Empty) + "\t");
            sb.Append(infoArray[0].Split('@')[1].Split(':')[1] + "\t");
            sb.Append(infoArray[0].Split('@')[0] + "\t");
            sb.Append(infoArray[1].Split('&')[0].Replace("encryption=", string.Empty) + "\t");
            sb.Append(string.Join(";", infoArray[1].Split('&')).Replace(infoArray[1].Split('&')[0]+";",string.Empty));

            return sb.ToString().Split('\t');
        }

        private string[] GetTrojanLine(string content)
        {
            StringBuilder sb = new StringBuilder();
            string[] infoArray = content.Split('#');
            if (infoArray.Length < 2 || !infoArray[0].Contains("@") || !infoArray[0].Contains(":"))
                return "\t\t\t\t\t\t".Split('\t');
            string ipAndport = infoArray[0].Split('@')[1];
            string otherInfo = string.Empty;
            if (infoArray[0].Contains("?"))
            {
                otherInfo = ipAndport.Split('?')[1];
                ipAndport = ipAndport.Split('?')[0];
            }
            sb.Append(HexDecode(infoArray[1]).Replace("+", string.Empty).Replace("\t", string.Empty) + "\t");
            sb.Append(ipAndport.Split(':')[0] + "\t");
            sb.Append(ipAndport.Split(':')[1] + "\t");
            sb.Append(infoArray[0].Split('@')[0] + "\t");
            sb.Append(string.Empty + "\t");
            sb.Append(otherInfo);

            return sb.ToString().Split('\t'); ;
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
                for (i = 1; i < 4; i++)
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
            StringBuilder headStr = new StringBuilder();
            if (str.Contains("-") && !str.Split('%')[str.Split('%').Length-1].Contains("-"))
            {
                string head = str.Split('-')[0].Replace("%20", string.Empty);
                if (!head.Contains("%"))
                {
                    headStr.Append(head.Replace("+", string.Empty));
                    str = str.Split('-')[1].Replace("+", string.Empty);
                }     
            }
            string[] content = str.Split('%');
            byte[] arrByte = new byte[content.Length-1];

            StringBuilder notByteStr = new StringBuilder();
            int index = 0;
            headStr.Append(content[0]);
            for (int i = 1; i < content.Length; i ++)
            {
                if (content[i].Length != 2)
                {
                    notByteStr.Append(content[i].Substring(2, content[i].Length - 2));
                    content[i] = content[i].Substring(0, 2);
                }
                arrByte[index] = Convert.ToByte(content[i], 16);
                index++;
            }
            //arrByte[content.Length - 2] = Convert.ToByte(content[content.Length-1].Substring(0, 2), 16);
            //notByteStr += content[content.Length-1].Substring(2, content[content.Length-1].Length - 2);
            str = headStr.ToString() + Encoding.UTF8.GetString(arrByte) + notByteStr.ToString().Replace("+",string.Empty);
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
