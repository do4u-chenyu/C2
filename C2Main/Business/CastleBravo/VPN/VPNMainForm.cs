using C2.Business.CastleBravo.WebShellTool;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.VPN
{
    public partial class VPNMainForm : Form
    {
        public static ProxySetting Proxy { get; set; } = ProxySetting.Empty;

        public VPNMainForm()
        {
            InitializeComponent();
        }

        private void 添加ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {

        }

        private void 批量添加ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {

        }

        private void 查找ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {

        }

        private void 重新开始_批量验活_ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {

        }

        private void 继续上次_二刷不活_ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {

        }

        private void ProxySettingMenu_Click(object sender, System.EventArgs e)
        {
            Proxy = new ProxySettingForm(Proxy).ShowDialog();
            ResetSLabel();
        }

        private void ResetSLabel()
        {
            ItemCountSLabel.Text = string.Format("共{0}项", LV.Items.Count);
            ProxyEnableSLabel.Text = "代理" + (Proxy.Enable ? "启用" : "关闭");
        }

        private void StopMenu_Click(object sender, System.EventArgs e)
        {

        }
    }
}
