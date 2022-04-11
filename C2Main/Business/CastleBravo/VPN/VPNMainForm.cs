using C2.Business.CastleBravo.WebShellTool;
using C2.Business.CastleBravo.WebShellTool.SettingsDialog;
using C2.Core;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.VPN
{
    public partial class VPNMainForm : Form
    {
        public static ProxySetting Proxy { get; set; } = ProxySetting.Empty;
        List<VPNTaskConfig> tasks = new List<VPNTaskConfig>();
        private FindSet finder;

        private ToolStripItem[] enableItems;
        public VPNMainForm()
        {
            InitializeComponent();
            InitializeOther();
            InitializeToolStrip();
        }

        private void InitializeOther()
        {
            finder = new FindSet(LV);
            LV.ListViewItemSorter = new LVComparer();
        }

        private void InitializeToolStrip()
        {
            // 批量验活时, 与其他菜单项互斥
            enableItems = new ToolStripItem[]
            {
                this.editDDB,
                this.proxySettingMenu,
                this.refreshAllShellMenu,
                this.secondRefreshMenu,
                this.checkAliveDDB,
                this.infoCollectionMenu,
                this.passwdBlastingMenuItem,
            };
        }

        private void 添加ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            VPNTaskConfig config = new AddVPNServerForm().ShowDialog(ST.NowString());
            if (config == VPNTaskConfig.Empty)
                return;
            LV.Items.Add(NewLVI(config));
            tasks.Add(config);
        }

        private void 批量添加ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            BatchAddVPNServerForm dialog = new BatchAddVPNServerForm();
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
        }

        private void 查找ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            finder.FindHit();
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

        
       // private bool actionNeedStop = false;
        private void StopMenu_Click(object sender, System.EventArgs e)
        {
            //actionNeedStop = true;
        }

        static bool isAlertnatingRows = true;
        static readonly Color SingleRowColor = Color.FromArgb(255, 217, 225, 242);
        static readonly Color AltertnatingRowColor = Color.FromArgb(255, 208, 206, 206);
        private ListViewItem NewLVI(VPNTaskConfig config)
        {
            ListViewItem lvi = new ListViewItem(config.CreateTime);
            lvi.SubItems.Add(config.Remark);
            lvi.SubItems.Add(config.Host);
            lvi.SubItems.Add(config.Port);
            lvi.SubItems.Add(config.Password);
            lvi.SubItems.Add(config.Method);
            lvi.SubItems.Add(config.Status);
            lvi.SubItems.Add(config.SSVersion);
            lvi.SubItems.Add(config.ProbeInfo);
            lvi.SubItems.Add(config.Country);
            lvi.SubItems.Add(config.IP);

            // 指针关联
            lvi.Tag = config;
            // 设置间隔行背景色
            lvi.BackColor = isAlertnatingRows ? SingleRowColor : AltertnatingRowColor;
            isAlertnatingRows = !isAlertnatingRows;
            return lvi;
        }
    }
}
