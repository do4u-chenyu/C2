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

        public new ProxySetting ShowDialog()
        {
            return base.ShowDialog() == System.Windows.Forms.DialogResult.OK ?
                new ProxySetting(
                    enableComboBox.SelectedIndex == 1,
                    IPTextBox.Text.Trim(),
                    PortTextBox.Text.Trim(),
                    proxyTypeCombox.SelectedItem.ToString()) : ProxySetting.Empty;
        }

    }

    class ProxySetting
    {
        public static ProxySetting Empty = new ProxySetting();

        public bool Enable;
        public string IP;
        public string Port;
        public string Type;
        public ProxySetting()
        {

        }

        public ProxySetting(bool enable, string ip, string port, string type)
        {
            Enable = enable;
            IP = ip;
            Port = port;
            Type = type;
        }
    }

}
