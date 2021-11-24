using C2.Controls;
using C2.Core;
using C2.Utils;
using System.Text.RegularExpressions;

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
            return base.OnOKButtonClick();
        }
    }
}
