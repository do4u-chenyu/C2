using C2.Controls;
using C2.Core;
using C2.Utils;

namespace C2.Business.CastleBravo.WebShellTool
{
    partial class ProxySettingForm : StandardDialog
    {
        public string IP { get => IPTextBox.Text.Trim(); }
        public string Port { get => PortTextBox.Text.Trim(); }
        public bool Enable { get => enableComboBox.SelectedIndex == 1; }
        public ProxySettingForm()
        {
            InitializeComponent();
            InitializeWebShell();
        }

        private void InitializeWebShell()
        {
            this.proxyTypeCombox.SelectedIndex = 0;
            this.enableComboBox.SelectedIndex = 0;
        }

        protected override bool OnOKButtonClick()
        {
            //TODO 判断必填是否有值
            if(IPTextBox.Text.IsNullOrEmpty() || PortTextBox.Text.IsNullOrEmpty())
            {
                HelpUtil.ShowMessageBox("【IP】 和 【端口】 不能为空。");
                return false;
            }
            return base.OnOKButtonClick();
        }
    }
}
