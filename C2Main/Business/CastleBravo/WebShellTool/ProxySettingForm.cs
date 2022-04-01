using C2.Controls;
using C2.Core;
using C2.Utils;

namespace C2.Business.CastleBravo.WebShellTool
{
    partial class ProxySettingForm : StandardDialog
    {
        public ProxySettingForm(ProxySetting proxy)
        {
            InitializeComponent();
            InitializeProxySetting(proxy);
        }

        private void InitializeProxySetting(ProxySetting proxy)
        {
            if (proxy == ProxySetting.Empty)
            {
                this.proxyTypeCombox.SelectedIndex = 0;
                this.enableComboBox.SelectedIndex = 0;
            }
            else
            {
                this.proxyTypeCombox.SelectedIndex = proxy.Type == "HTTP" ? 0 : 1;
                this.enableComboBox.SelectedIndex = proxy.Enable ? 1 : 0;
                this.IPTextBox.Text = proxy.IP;
                this.PortTextBox.Text = proxy.Port.ToString();
            }

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
                    enableComboBox.SelectedIndex == 1,   // 0 否 1是
                    IPTextBox.Text.Trim(),
                    ConvertUtil.TryParseInt(PortTextBox.Text.Trim(), 1080),
                    proxyTypeCombox.SelectedItem.ToString()) : ProxySetting.Empty;
        }

    }

    public class ProxySetting
    {
        public static ProxySetting Empty = new ProxySetting();

        public bool Enable;
        public string IP;
        public int Port;
        public string Type;
        public ProxySetting()
        {

        }

        public ProxySetting(bool enable, string ip, int port, string type)
        {
            Enable = enable;
            IP = ip;
            Port = port;
            Type = type;
        }
    }

}
