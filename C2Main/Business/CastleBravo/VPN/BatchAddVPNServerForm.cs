﻿using C2.Controls;
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
                    sb.Append(StrFormatted(HexDecode(content.Split("#")[1])));
                }
                catch
                {
                    sb.Append("\t");
                }
                sb.Append(StrFormatted(ipAndport.Split(':')[0]));
                sb.Append(StrFormatted(ipAndport.Split(':')[1].Replace("/", string.Empty)));
                sb.Append(StrFormatted(methodAndPwd.Split(':')[1]));
                sb.Append(StrFormatted(methodAndPwd.Split(':')[0]));
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
            sb.Append(StrFormatted(GetBase64Str(remark)));
            sb.Append(StrFormatted(infoArray[0]));
            sb.Append(StrFormatted(infoArray[1]));
            sb.Append(StrFormatted(GetBase64Str(infoArray[5])));
            sb.Append(StrFormatted(infoArray[3]));
            sb.Append(otherinfo);
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
                sb.Append(StrFormatted(HexDecode(content.Split('?')[1].Split('&')[0].Replace("remarks=", string.Empty).Replace("remark=", string.Empty))));
                sb.Append(StrFormatted(info.Split('@')[1].Split(':')[0].Replace("/", string.Empty)));
                sb.Append(StrFormatted(info.Split('@')[1].Split(':')[1]));
                sb.Append(StrFormatted(info.Split('@')[0].Split(':')[1]));
                sb.Append(StrFormatted(info.Split('@')[0].Split(':')[0]));
                sb.Append(string.Join(";", content.Split('?')[1].Split('&')).Replace(content.Split('?')[1].Split('&')[0] + ";", string.Empty));
            }
            else
            {
                info = GetBase64Str(content);
                try
                {
                    var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(info);
                    //"host":"tha01.hntgxxjc.com",
                    //"path":"/v2ray",
                    // "tls":"",
                    // "verify_cert":true,
                    // "add":"teshuzz.jfjfhg.com",
                    // "port":16009,
                    // "aid":0,
                    // "net":"ws",
                    // "headerType":"none",
                    // "v":"2",
                    // "type":"vmess",
                    // "ps":"特殊-V207-0.6倍-泰国*\t",
                    // "remark":"特殊-V207-0.6倍-泰国*\t",
                    // "id":"05386a49-688a-3796-8da6-d5af4d4c54f1",
                    // "class":3}

                    StringBuilder otherInfo = new StringBuilder();
                    if (dict.ContainsKey("host"))
                        otherInfo.Append("伪装域名=" + dict["host"]);
                    if (dict.ContainsKey("type"))
                        otherInfo.Append(";type=" + dict["type"]); //有时候是客户端类型，有时候是伪装类型
                    if (dict.ContainsKey("path"))
                        otherInfo.Append(";路径=" + dict["path"]);
                    if (dict.ContainsKey("tls"))
                        otherInfo.Append(";传输层安全=" + dict["tls"]);
                    if (dict.ContainsKey("net"))
                        otherInfo.Append(";传输协议=" + dict["net"]);
                    if (dict.ContainsKey("aid"))
                        otherInfo.Append(";额外ID=" + dict["aid"]);
                    if (dict.ContainsKey("verify_cert"))
                        otherInfo.Append(";verify_cert=" + dict["verify_cert"]);  //这三个字段不知道干嘛的
                    if (dict.ContainsKey("class"))
                        otherInfo.Append(";class=" + dict["class"]);   //这三个字段不知道干嘛的
                    if (dict.ContainsKey("headerType"))
                        otherInfo.Append(";headerType=" + dict["headerType"]);   //这三个字段不知道干嘛的
                    if (dict.ContainsKey("ps"))
                        sb.Append(StrFormatted(dict["ps"].ToString().Replace(" ", string.Empty)));
                    else
                        sb.Append("\t");
                    sb.Append(StrFormatted(dict["add"].ToString()));
                    sb.Append(StrFormatted(dict["port"].ToString()));
                    sb.Append(StrFormatted(dict["id"].ToString()));
                    if (dict.ContainsKey("v"))
                        sb.Append(StrFormatted(dict["v"].ToString().Replace("2", "auto").Replace("0", "ase-128-gcm").Replace("1", "chacha20-poly1305"))); //这个字段不确定
                    else
                        sb.Append("\t");
                    sb.Append(otherInfo.ToString());
                }
                catch
                {
                    string[] infoArray = info.Split(", ");
                    if (infoArray.Length < 5)
                        return "\t\t\t\t\t\t".Split('\t');
                    string[] arrCopy = new string[infoArray.Length - 5];
                    Array.Copy(infoArray, 5, arrCopy, 0, infoArray.Length - 5);
                    sb.Append(StrFormatted(infoArray[0]));
                    sb.Append(StrFormatted(infoArray[1]));
                    sb.Append(StrFormatted(infoArray[2]));
                    sb.Append(StrFormatted(infoArray[4]));
                    sb.Append(StrFormatted(infoArray[3]));
                    sb.Append(string.Join(";", arrCopy));
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

            sb.Append(StrFormatted(HexDecode(infoArray[1].Split('#')[1])));
            sb.Append(StrFormatted(infoArray[0].Split('@')[1].Split(':')[0].Replace("/", string.Empty)));
            sb.Append(StrFormatted(infoArray[0].Split('@')[1].Split(':')[1]));
            sb.Append(StrFormatted(infoArray[0].Split('@')[0]));
            sb.Append(StrFormatted(infoArray[1].Split('&')[0].Replace("encryption=", string.Empty)));
            sb.Append(string.Join(";", infoArray[1].Split('&')).Replace(infoArray[1].Split('&')[0] + ";", string.Empty));

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
            sb.Append(StrFormatted(HexDecode(infoArray[1]).Replace("+", string.Empty)));
            sb.Append(StrFormatted(ipAndport.Split(':')[0]));
            sb.Append(StrFormatted(ipAndport.Split(':')[1]));
            sb.Append(StrFormatted(infoArray[0].Split('@')[0]));
            sb.Append(StrFormatted(string.Empty));
            sb.Append(otherInfo);

            return sb.ToString().Split('\t'); ;
        }


        public string GetBase64Str(string base64Str)
        {
            string info = string.Empty;
            base64Str = Uri.UnescapeDataString(Uri.UnescapeDataString(Uri.UnescapeDataString(base64Str)));
            if (IsBase64Formatted(base64Str))
                info = Encoding.UTF8.GetString(Convert.FromBase64String(base64Str)); 
            else if (IsBase64Formatted(base64Str + "="))
                info = Encoding.UTF8.GetString(Convert.FromBase64String(base64Str + "="));
            else if (IsBase64Formatted(base64Str + "=="))
                info = Encoding.UTF8.GetString(Convert.FromBase64String(base64Str + "=="));
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

            str = headStr.ToString() + Encoding.UTF8.GetString(arrByte) + notByteStr.ToString().Replace("+",string.Empty);
            return str;
        }

        private string StrFormatted(string input)
        {
            if (input.Contains("\t"))
                input = input.Replace("\t", string.Empty);
            return string.Format("{0}{1}",input,"\t");
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
