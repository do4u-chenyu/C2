using C2.Controls;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Linq;
using C2.Business.CastleBravo.WebShellTool;

namespace C2.Business.CastleBravo.VPN
{
    partial class BatchAddVPNServerForm : StandardDialog
    {
        private readonly string ssline   = @"^(?i)(ss|ssr|vmess|vless|trojan)(?-i)://(.+)$";
        private readonly string addrline = @"^(\d{1,2}\.\d{1,2}\.\d{1,2}\.\d{1,2})[:\s]+(\d{1,5})$";
        private int maxRow;
        private int mode;
        string FilePath { get => this.filePathTextBox.Text; set => this.filePathTextBox.Text = value; }
        public List<VPNTaskConfig> Tasks;
        public BatchAddVPNServerForm()
        {
            InitializeComponent();
            InitializeOther();
        }

        public void InitializeOther()
        {
            mode = 0;
            maxRow = 10000 * 20;
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
                AddTasksByLine(lines[i].Trim());

            return true;
        }


        private void AddTasksByLine(string line)
        {
            switch(mode)
            {
                case 0:
                    DoSSLine(line);
                    break;
                case 1:
                    DoAddrLine(line);
                    break;
                case 2:
                    DoRSSLine(line);
                    break;
            } 
        }

        private void DoAddrLine(string line, Match mat = null)
        {
            mat = mat ?? Regex.Match(line, addrline);
            if (!mat.Success)
                return;
            string ip   = mat.Groups[1].Value;
            string port = mat.Groups[2].Value;

            Tasks.Add(new VPNTaskConfig(ST.NowString(),
                            "候选目标",
                            ip,
                            port,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            ip,
                            string.Empty,
                            ip + ":" + port
                            ));
        }
        private void DoRSSLine(string line)
        {
            bool isRss = line.StartsWith("http://") || line.StartsWith("https://");
            if (!isRss)
                return;

            using (GuarderUtil.WaitCursor)
            {
                string ret = TryDecodeBase64(WebClientEx.TryGet(line, 
                                                                Global.WebClientDefaultTimeout,
                                                                ProxySetting.Empty));
                foreach (string ss in ret.SplitLine())
                    DoSSLine(ss);
            }
        }

        private void DoSSLine(string line)
        {
            Match mat = Regex.Match(line, ssline);
            if (!mat.Success)
            {
                // 妥协一下,很多时候就直接粘贴IP:端口了,没选cb按钮
                mat = Regex.Match(line, addrline);
                if (mat.Success)
                    DoAddrLine(line, mat);
                else
                    DoRSSLine(line);
                return;
            }

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
                                        string.Empty,              // CheckAliveOneTaskAsyn(contentArray),
                                        version.ToUpper(),
                                        string.Empty,
                                        array.Length > 5 ? array[5].Trim() : string.Empty,
                                        string.Empty,
                                        string.Empty,
                                        version + "://" + content  // 原始连接 
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
            value = TryDecodeBase64(array[0]);
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
            string[] array = ST.UrlDecode(value).Split("?");

            string left  = TryDecodeBase64(array[0]);
            string right = array.Length > 1 ? array[1] : string.Empty;

            string remarks = string.Empty;
            string addr    = string.Empty;
            string port    = string.Empty;
            string pass    = string.Empty;
            string method  = string.Empty;
            string other   = string.Empty;

            if (right.IsEmpty()) //第一种情况，base64解码后是字典形式
            {
                var dict = TryJsonToDictionary(left);
                // remark里可能有逗号，不能直接用逗号分隔
                array = dict.IsEmpty() ? left.Split(", ") : Global.EmptyStringArray5;

                remarks = dict.ContainsKey("ps")   ? dict["ps"]   : array.Length > 0 ? array[0] : string.Empty;
                addr    = dict.ContainsKey("add")  ? dict["add"]  : array.Length > 1 ? array[1] : string.Empty;
                port    = dict.ContainsKey("port") ? dict["port"] : array.Length > 2 ? array[2] : string.Empty;
                method  = dict.ContainsKey("scy")  ? dict["scy"]  : array.Length > 3 ? array[3] : string.Empty;
                pass    = dict.ContainsKey("id")   ? dict["id"]   : array.Length > 4 ? array[4] : string.Empty;
                method = method.IsNullOrEmpty() ? "auto" : method;

                dict.Remove("ps", "add", "port", "scy", "id");

                StringBuilder sb = new StringBuilder();
                foreach (var kv in dict)
                    sb.Append(string.Format("{0}={1};", kv.Key, kv.Value));
                other = sb.Append(array.Skip(5).JoinString()).ToString();
            }
            else  //第二种情况  remark和其他信息在？后面
            {
                string content = right.Split('&')[0];
                remarks = content.Contains("=") ? content.Split('=')[1] : remarks;

                content += "&";
                other = right.Replace(content, string.Empty); //不确定其他信息有几个字段,除了remark都是

                array = left.Split('@');
                string methodAndPass = array[0];
                string addrAndPort = array.Length > 1 ? array[1] : string.Empty;

                array = methodAndPass.Split(':');
                method = array[0];
                pass = array.Length > 1 ? array[1] : string.Empty;

                array = addrAndPort.Split(':');
                addr = array[0];
                port = array.Length > 1 ? array[1] : string.Empty;
            }

            return new string[] { remarks, addr, port, pass, method, other };
        }
        

        private string[] GenVlessLine(string value)
        {
            string[] array = ST.UrlDecode(value).Split("#");

            string method    = string.Empty;
            string otherInfo = string.Empty;
            string remarks   = array.Length > 1 ? array[1] : string.Empty;
            
            array = array[0].Split("?");
            string info = array.Length > 1 ? array[1] : string.Empty;

            if (!info.IsNullOrEmpty())
            {
                NameValueCollection latterParams = NetUtil.ParseQueryStringUTF8(info);
                method = latterParams["encryption"];

                StringBuilder sb = new StringBuilder();
                string[] paramsList =  { "type", "security", "path", "headerType" };
                
                foreach (string param in paramsList)
                    sb.Append(string.Format("{0}={1};", param, latterParams[param]));
                
                otherInfo = sb.ToString();
            }

            array = array[0].Split(":");
            string port = array.Length > 1 ? array[1] : string.Empty;

            array = array[0].Split("@");
            string pass = array[0];
            string addr = array.Length > 1 ? array[1] : string.Empty;
            addr = addr.Replace("/", string.Empty);

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
            // base64: 0-9 a-z A-Z / +  
            return ST.TryDecodeBase64(value.Replace("_", "/").Replace("-", "+"));
        }

        public static Dictionary<string, string> TryJsonToDictionary(string input)
        {
            try
            {
                return JsonUtil.JsonToDictionary(input);
            }
            catch
            {
                return new Dictionary<string, string>();
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


        private void RadioButton_Click(object sender, EventArgs e)
        {
            new Label[] { label0, label1, label2 }.ToList().ForEach(v => v.Visible = false);
            RadioButton rb = sender as RadioButton;
            switch (rb.Name)
            {
                case "ss":
                    label0.Visible = true;
                    mode = 0;
                    break;
                case "addr":
                    label1.Visible = true;
                    mode = 1;
                    break;
                case "rss":
                    label2.Visible = true;
                    mode = 2;
                    break;
                default:
                    break;
            }

        }
    }
}
