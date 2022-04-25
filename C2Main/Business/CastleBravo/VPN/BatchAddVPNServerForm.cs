using C2.Controls;
using C2.Core;
using C2.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

            string[] array = Global.EmptyStringArray6;
            switch (version)
            {
                case "ss":
                    array = GenSSLine(content);
                    break;
                case "ssr":
                    array = GenSSRLine(content);
                    break;
                case "vmess":
                    array = GenVmessLine(content);
                    break;
                case "vless":
                    array = GenVlessLine(content);
                    break;
                case "trojan":
                    array = GenTrojanLine(content);
                    break;
            }

            Tasks.Add(new VPNTaskConfig(ST.NowString(),
                                        array[0].Trim(),
                                        array[1].Trim(),
                                        array[2].Trim(),
                                        array[3].Trim(),
                                        array[4].Trim(),
                                        string.Empty,       // CheckAliveOneTaskAsyn(contentArray),
                                        version.ToUpper(),
                                        "探针结果:未测",
                                        array.Length > 5 ? array[5].Trim(): string.Empty,
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

        private string[] GenSSLine(string value)
        {
            // 第一步: url解码
            value = ST.UrlDecode(value);
            // 第二步: 按#分割, 取出remark
            string[] array = value.Split("#");
            string remarks = array.Length > 1 ? array[1] : string.Empty;
            // 第三步: 按@分割, 取出base64和地址信息
            array = array[0].Split("@");
            string addr = array.Length > 1 ? array[1] : string.Empty;
            // 第四步: base64解码
            value = ST.TryDecodeBase64(array[0]);
            // 如果addr为空则从base64中补充addr
            if (addr.IsNullOrEmpty())
            {
                array = value.Split("@");
                addr = array.Length > 1 ? array[1] : addr;
                value = array[0];
            }
            // 第五步: addr中分割IP和端口
            array = addr.Split(":");
            addr = array[0];
            string port = array.Length > 1 ? array[1] : string.Empty;
            // 第六步: method和password分割
            array = value.Split(":");
            string method = array[0];
            string pass = array.Length > 1 ? array[1] : string.Empty;
            // 第七步: 返回构造数组
            return new string[] { remarks, addr, port, pass, method };
        }

        private string[] GenSSRLine(string value)
        {
            value = ST.UrlDecode(TryDecodeBase64(value));
            string[] array = value.Split("/?");

            string left  = array[0];
            string right = array.Length > 1 ? array[1] : string.Empty;

            array = left.Split(":");

            string remarks = string.Empty;
            string addr    = array[0];
            string port    = array.Length > 1 ? array[1] : string.Empty;
            string proto   = array.Length > 2 ? array[2] : string.Empty;
            string method  = array.Length > 3 ? array[3] : string.Empty;
            string obfs    = array.Length > 4 ? array[4] : string.Empty;
            string pass    = array.Length > 5 ? TryDecodeBase64(array[5]) : string.Empty;
            string other   = string.Format("协议={0};混淆={1};", proto, obfs);

            if (right.IsNullOrEmpty())
                return new string[] { remarks, addr, port, pass, method, other };

            NameValueCollection lParams = NetUtil.ParseQueryStringUTF8(right);

            remarks           = TryDecodeBase64(lParams["remarks"]    ?? string.Empty); 
            string obfsparam  = TryDecodeBase64(lParams["obfsparam"]  ?? string.Empty);
            string protoparam = TryDecodeBase64(lParams["protoparam"] ?? string.Empty);
            string group      = TryDecodeBase64(lParams["group"]      ?? string.Empty);

            other += string.Format("协议参数={0};混淆参数={1};Group={2}", protoparam, obfsparam, group);

            return new string[] { remarks, addr, port, pass, method, other };
        }

        private string[] GenVmessLine(string value)
        {
            value = ST.UrlDecode(value);

            string[] array = value.Split("?");
            string left  = TryDecodeBase64(array[0]);
            string right = array.Length > 1 ? array[1] : string.Empty;

            string remarks = string.Empty;
            string addr    = string.Empty;
            string port    = string.Empty;
            string pass    = string.Empty;
            string method  = string.Empty;
            string other   = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (!right.IsNullOrEmpty()) //第一种情况，remark和其他信息在？后面
            {
                string content = right.Split('&')[0];
                remarks = content.Contains("=") ? content.Split('=')[1] : remarks;

                content += "&";
                other   = right.Replace(content, string.Empty); //不确定其他信息有几个字段,除了remark都是

                array = left.Split('@');
                string methodAndPass = array[0];
                string addrAndPort   = array.Length > 1 ? array[1] : string.Empty;

                array  = methodAndPass.Split(':');
                method = array[0];
                pass = array.Length > 1 ? array[1] : string.Empty;

                array = addrAndPort.Split(':');
                addr  = array[0];
                port  = array.Length > 1 ? array[1] : string.Empty;
            }           

            else if (IsDictFormatted(left)) //第二种情况，没有？，base64解码后是字典形式
            {
                var dict = JsonUtil.JsonToDictionary(left);

                remarks = dict.ContainsKey("ps")   ? dict["ps"]   : string.Empty;
                addr    = dict.ContainsKey("add")  ? dict["add"]  : string.Empty;
                port    = dict.ContainsKey("port") ? dict["port"] : string.Empty;
                pass    = dict.ContainsKey("id")   ? dict["id"]   : string.Empty;
                method  = dict.ContainsKey("v") ? dict["v"] == "2" ? "auto" : dict["v"] : string.Empty;  //这个字段不确定
                
                List<string> leftList = new List<string> { "ps", "add", "port", "id", "v" };
                foreach (string infoKey in dict.Keys)
                {
                    if (!dict.ContainsKey(infoKey) || leftList.Contains(infoKey))
                        continue;
                    sb.Append(string.Format("{0}={1};", infoKey, dict[infoKey]));
                }
            }

            else //第三种情况，没有？base64解码后是逗号分割
            {
                array = left.Split(", ");
                
                remarks = array[0];
                addr    = array.Length > 1 ? array[1] : string.Empty;
                port    = array.Length > 2 ? array[2] : string.Empty;
                method  = array.Length > 3 ? array[3] : string.Empty;
                pass    = array.Length > 4 ? array[4] : string.Empty;

                if (array.Length > 5)
                {
                    for (int i = 5; i < array.Length; i++)
                        sb.Append(array[i]);
                }
            }

            other = sb.ToString();

            return new string[] { remarks, addr, port, pass, method, other };
        }
        

        private string[] GenVlessLine(string value)
        {

            value = ST.UrlDecode(value);
            string[] array = value.Split("#");

            string remarks = array.Length > 1 ? array[1] : string.Empty;
            array = array[0].Split("?");

            string method = string.Empty;
            StringBuilder sb = new StringBuilder();
            string info = array.Length > 1 ? array[1] : string.Empty;

            if (!info.IsNullOrEmpty())
            {
                NameValueCollection latterParams = NetUtil.ParseQueryStringUTF8(info);

                List<string> paramList = new List<string> { "type", "security", "path", "headerType" };
                foreach (string param in paramList)
                    sb.Append(string.Format("{0}={1};", param, latterParams[param]));
                method = latterParams["encryption"];
            }

            array = array[0].Split(":");
            string port = array.Length > 1 ? array[1] : string.Empty;

            array = array[0].Split("@");
            string pass = array[0];

            string addr = array.Length > 1 ? array[1] : string.Empty;
            addr = addr.Replace("/", string.Empty);
            string otherInfo = sb.ToString();

            return new string[] { remarks, addr, port, pass, method, otherInfo };

        }

        private string[] GenTrojanLine(string value)
        {
            value = ST.UrlDecode(value);
            
            string[] array = value.Split("#");
            string remarks = array.Length > 1 ? array[1] : string.Empty;

            array = array[0].Split("?");
            string otherInfo = array.Length > 1 ? array[1] : string.Empty;

            array = array[0].Split(":");
            string port = array.Length > 1 ? array[1] : string.Empty;

            array = array[0].Split("@");
            string pass = array[0];

            string addr = array.Length > 1 ? array[1] : string.Empty;
            addr = addr.Replace("/", string.Empty);
            string method = string.Empty;

            return new string[] { remarks, addr, port, pass, method, otherInfo };
        }


        public string TryDecodeBase64(string value)
        {
            return ST.TryDecodeBase64(value.Replace("_", "/").Replace("-", "+"));
        }

        public static bool IsDictFormatted(string input)
        {
            try
            {
                JsonUtil.JsonToDictionary(input);
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
