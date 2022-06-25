using C2.Business.CastleBravo.WebShellTool;
using C2.Controls;
using C2.Core;
using C2.Dialogs;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.VPN
{
    partial class BatchAddVPNServerForm : StandardDialog
    {
        private readonly string ssline   = @"^(?i)(ss|ssr|vmess|vless|trojan)(?-i)://(.+)$";
        private readonly string addrline = @"^(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})[:\s]+(\d{1,5})$";
        private int maxRow;
        private int mode;
        private int vpnCount;
        string FilePath { get => this.filePathTextBox.Text; set => this.filePathTextBox.Text = value; }
        public List<VPNTaskConfig> Tasks;

        private List<string> rssFailHist;
        private int rssSuccCnt;

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
            rssFailHist = new List<string>();
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
            bool ret = (this.pasteModeCB.Checked ? GenTasksFromPaste() : GenTasksFromFile()) && base.OnOKButtonClick();

            if (rssFailHist.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("有{0}个订阅地址解析失败，需要手工复核:", rssFailHist.Count));
                
                foreach (string line in rssFailHist)
                    sb.AppendLine(line);

                new MessageDialog(sb.ToString()).ShowDialog();
            }
            return ret;
        }

        private bool GenTasksFromPaste()
        {
            if (this.wsTextBox.Text.Trim().IsEmpty())
                return false;
            // 如果粘贴文件不合格,就别清空旧数据了
            Tasks.Clear();

            string[] lines = this.wsTextBox.Text.SplitLine();
            ResetProgressBar(lines.Length);

            for (int i = 0; i < Math.Min(lines.Length, maxRow); i++)
                AddTasksByLine(lines[i].Trim());

            return true;
        }

        private void ResetProgressBar(int max)
        {
            this.rssPB.Minimum = 0;
            this.rssPB.Maximum = max;
            this.rssPB.Value = 0;
            this.rssLB.Text = string.Empty;
            this.vpnCount = 0;
            this.rssFailHist.Clear();
        }

        private void VisibleProgressBar()
        {
            this.rssLable.Visible = true;
            this.rssPB.Visible = true;
            this.rssLB.Visible = true;
            this.vpnPB.Visible = true;
            this.vpnLB.Visible = true;
            this.sucLB.Visible = true;
            this.sucPB.Visible = true;
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
                    VisibleProgressBar();
                    DoRSSLine(line);
                    break;
                case 3:
                    VisibleProgressBar();
                    DoClashLine(line);
                    break;

            }
        }

        private void DoAddrLine(string line, Match mat = null)
        {
            mat = mat ?? Regex.Match(line, addrline);
            if (!mat.Success)
                return;

            string ip   = mat.Groups[1].Value.Trim();
            string port = mat.Groups[2].Value.Trim();

            // 实锤一下必须是IP和端口
            if (NetUtil.IsIPAddress(ip) && NetUtil.IsPort(port))
                Tasks.Add(new VPNTaskConfig(ST.NowString(),
                                string.Empty,
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
                                ip + ":" + port,
                                string.Empty
                                ));
        }
        private void DoRSSLine(string line)
        {
            UpdateProgressBar();
            line = line.Trim('"');
            bool isRss = line.StartsWith("http://") || line.StartsWith("https://");
            if (!isRss)
                return;

            using (GuarderUtil.WaitCursor)
            {
                string ret = TryDecodeBase64(WebClientEx.TryGet(line, 
                                                                Global.WebClientDefaultTimeout,
                                                                ProxySetting.Empty));
                if (ret.IsNullOrEmpty())
                    rssFailHist.Add(line);
                else
                    rssSuccCnt++;

                foreach (string ss in ret.SplitLine())
                {
                    DoSSLine(ss, line);
                    vpnCount++;
                }
                    
            }
        }

        private void DoClashLine(string line)
        {
            UpdateProgressBar();
            line = line.Trim('"');
            bool isRss = line.StartsWith("http://") || line.StartsWith("https://");
            if (!isRss)
                return;

            using (GuarderUtil.WaitCursor)
            {
                string ret = WebClientEx.TryGet(line, Global.WebClientDefaultTimeout, ProxySetting.Empty);
                Dictionary<object, object> obj = YamlUtil.YamlStringToDictionary(ret);

                List<object> list = obj.ContainsKey("Proxy") ? obj["Proxy"] as List<object> :
                                    obj.ContainsKey("proxies") ? obj["proxies"] as List<object> :
                                    new List<object>();

                if (list.IsNullOrEmpty())
                {
                    rssFailHist.Add(line);
                    return;
                }     

                foreach (Dictionary<object, object> vmess in list)
                {
                    string addr = vmess.ContainsKey("server") ? vmess["server"] as string : string.Empty;
                    string port = vmess.ContainsKey("port") ? vmess["port"] as string : string.Empty;
                    string pass = vmess.ContainsKey("uuid") ? vmess["uuid"] as string : string.Empty;
                    string method = vmess.  ContainsKey("cipher") ? vmess["cipher"] as string : string.Empty;
                    string version = vmess.ContainsKey("type") ? vmess["type"] as string : string.Empty;
                    string remarks = vmess.ContainsKey("name") ? vmess["name"] as string : string.Empty;

                    vmess.Remove("server", "port", "uuid", "cipher", "type", "name");

                    StringBuilder sb = new StringBuilder();
                    foreach (var kv in vmess)
                        sb.Append(string.Format("{0}={1};", kv.Key as string, kv.Value.ToString()));
                    string other = sb.ToString();
                    
                    vpnCount++;
                    Tasks.Add(new VPNTaskConfig(ST.NowString(),
                            remarks.Trim(),
                            addr.Trim(),
                            port.Trim(),
                            pass.Trim(),
                            method.Trim(),
                            string.Empty,
                            version.ToUpper(),
                            string.Empty,
                            other.Trim(),
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty
                            ));
                }
            }
        }

        private void UpdateProgressBar()
        {
            int val = rssPB.Value < rssPB.Maximum ? rssPB.Value++ : rssPB.Maximum;
            this.rssLB.Text = string.Format("{0}/{1}", val, rssPB.Maximum);
            this.vpnLB.Text = string.Format("{0}", this.vpnCount);
            this.sucLB.Text = string.Format("{0}/{1}", rssSuccCnt, rssFailHist.Count);
            Application.DoEvents();
        }

        private void DoSSLine(string line, string ssAddress = "")
        {
            Match mat = Regex.Match(line, ssline);
            if (!mat.Success)
            {
                // 妥协一下,很多时候就直接粘贴IP:端口了,没选cb按钮
                mat = Regex.Match(line, addrline);
                if (mat.Success)
                    DoAddrLine(line, mat);
                else
                {
                    VisibleProgressBar();
                    // clash类订阅地址
                    if (line.ToLower().Contains("clash="))
                        DoClashLine(line);
                    // 非clash类订阅地址
                    else
                        DoRSSLine(line);
                }
                    
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
                                        string.Empty,
                                        version.ToUpper(),
                                        string.Empty,
                                        array.Length > 5 ? array[5].Trim() : string.Empty,
                                        string.Empty,
                                        string.Empty,
                                        version + "://" + content,  // 分享链接 
                                        ssAddress
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
                {
                    List<string> lines = new List<string>();
                    for (int row = 0; row < maxRow && !sr.EndOfStream; row++)
                        lines.Add(sr.ReadLine().Trim());

                    ResetProgressBar(lines.Count);

                    foreach (string line in lines)
                        AddTasksByLine(line);
                }

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
                value = array[0].IsNull() ? string.Empty : array[0];
            }
            // 第五步: addr中分割IP和端口
            array = addr.Split(":");
            addr  = array[0].IsNull() ? string.Empty : array[0];
            string port = array.Length > 1 ? array[1] : string.Empty;

            // 有些地方这里会遇到
            array = port.Split("/?");
            port  = array[0].IsNull() ? string.Empty : array[0];
            string other = array.Length > 1 ? array[1] : string.Empty;

            // 第六步: method和password分割
            array = value.Split(":");
            string method = array[0];
            string pass = array.Length > 1 ? array[1] : string.Empty;
            // 第七步: 返回构造数组
            return new string[] { ST.N(remarks), ST.N(addr), ST.N(port), ST.N(pass), ST.N(method), ST.N(other) };
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
                return new string[] { ST.N(remarks), ST.N(addr), ST.N(port), ST.N(pass), ST.N(method), ST.N(other) };

            NameValueCollection lParams = NetUtil.ParseQueryStringUTF8(right);

            remarks           = TryDecodeBase64(lParams["remarks"]    ?? string.Empty); 
            string obfsparam  = TryDecodeBase64(lParams["obfsparam"]  ?? string.Empty);
            string protoparam = TryDecodeBase64(lParams["protoparam"] ?? string.Empty);
            string group      = TryDecodeBase64(lParams["group"]      ?? string.Empty);

            other += string.Format("协议参数={0};混淆参数={1};Group={2}", protoparam, obfsparam, group);

            return new string[] { ST.N(remarks), ST.N(addr), ST.N(port), ST.N(pass), ST.N(method), ST.N(other) };
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
            string v       = string.Empty;

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
                v       = dict.ContainsKey("v")    ? dict["v"]    : string.Empty;

                method = method.IsNullOrEmpty() ? 
                         v == "0" ? "aes-128-gcm" : 
                         v == "1" ? "chacha20-poly1305" : 
                         v == "2" ? "auto" : 
                         v == "3" ? "none" :
                         "auto" : 
                         method;

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

            return new string[] { ST.N(remarks), ST.N(addr), ST.N(port), ST.N(pass), ST.N(method), ST.N(other) };
        }
        

        private string[] GenVlessLine(string value)
        {
            string[] array = ST.UrlDecode(value).Split("#");

            string method    = string.Empty;
            string other = string.Empty;
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
                
                other = sb.ToString();
            }

            array = array[0].Split(":");
            string port = array.Length > 1 ? array[1] : string.Empty;

            array = array[0].Split("@");
            string pass = array[0];
            string addr = array.Length > 1 ? array[1] : string.Empty;
            addr = addr.Replace("/", string.Empty);

            return new string[] { ST.N(remarks), ST.N(addr), ST.N(port), ST.N(pass), ST.N(method), ST.N(other) };
        }

        private string[] GenTrojanLine(string value)
        {
            value = ST.UrlDecode(value);
            
            string[] array = value.Split("#");
            string remarks = array.Length > 1 ? array[1] : string.Empty;

            array = array[0].Split("?");
            string other = array.Length > 1 ? array[1] : string.Empty;

            array = array[0].Split(":");
            string port = array.Length > 1 ? array[1] == "0" ? 
                "443" :        // 默认443
                array[1] :     
                string.Empty;

            array = array[0].Split("@");
            string pass = array[0];

            string addr = array.Length > 1 ? array[1] : string.Empty;
            addr = addr.Replace("/", string.Empty);
            string method = string.Empty;

            return new string[] {ST.N(remarks), ST.N(addr), ST.N(port), ST.N(pass), ST.N(method), ST.N(other) };
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
                return JsonUtil.JsonStringToDictionary(input);
            }
            catch
            {
                return new Dictionary<string, string>();
            }
        }


        private void RadioButton_Click(object sender, EventArgs e)
        {
            new Label[] { label0, label1, label2, label3 }.ToList().ForEach(v => v.Visible = false);
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
                case "clash":
                    label3.Visible = true;
                    mode = 3;
                    break;
                default:
                    break;
            }

        }
    }
}
