using System.Text.RegularExpressions;
using C2.Controls;
using C2.Core;
using C2.Utils;

namespace C2.Business.CastleBravo.WebShellTool.SettingsDialog
{
    partial class SuperPingSet : StandardDialog
    {
       
        public SuperPingSet()
        {
            InitializeComponent();
        }

        public string Domain { get => searchDomain.Text.Trim(); }
        protected override bool OnOKButtonClick() 
        {
            if (searchDomain.Text.IsNullOrEmpty())
            {
                HelpUtil.ShowMessageBox("域名不能为空");
                return false;
            }
            string domainRegex = @"[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(\.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+\.?";
            if (!Regex.IsMatch(searchDomain.Text, domainRegex))
            {
                HelpUtil.ShowMessageBox("请输入正确的域名，例：www.baidu.com");
                return false;
            }
            return base.OnOKButtonClick();
        }

    }
}
