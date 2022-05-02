using C2.Business.CastleBravo.WebShellTool;
using C2.Business.CastleBravo.WebShellTool.SettingsDialog;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static C2.Utils.GuarderUtil;

namespace C2.Business.CastleBravo.VPN
{
    public partial class VPNMainForm : Form
    {
        public static ProxySetting Proxy { get; set; } = ProxySetting.Empty;
        List<VPNTaskConfig> tasks = new List<VPNTaskConfig>();
        private FindSet finder;

        //private bool actionNeedStop = false;

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
                this.验活204Menu,
                this.checkAliveDDB,
                this.infoCollectionMenu,
                this.passwdBlastingMenuItem,
            };
        }

        private void 添加ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            VPNTaskConfig config = new AddVPNServerForm().ShowDialogNew(ST.NowString());
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
            using (GuarderUtil.WaitCursor)
                LV.Items.AddRange(NewLVIS(dialog.Tasks));
            tasks.AddRange(dialog.Tasks);
            SaveDB();
        }

        private void 查找ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            finder.FindHit();
        }

        private void 验活204_重新开始_ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {

        }

        private void ProxySettingMenu_Click(object sender, System.EventArgs e)
        {
            Proxy = new ProxySettingForm(Proxy).ShowDialog();
            ResetSLabel();
        }



        
       // private bool actionNeedStop = false;
        private void StopMenu_Click(object sender, System.EventArgs e)
        {
            //actionNeedStop = true;
        }

        static bool isAlertnatingRows = true;
        static readonly Color SingleRowColor = Color.FromArgb(255, 217, 225, 242);
        static readonly Color AltertnatingRowColor = Color.FromArgb(255, 208, 206, 206);




        private void 重新开始ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            RandomProbeConfig rpConfig = new RandomProbeForm().ShowDialog();
            if (rpConfig.Equals(RandomProbeConfig.Empty))
                return;

 
        }

        private void 继续上次ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {

        }

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.LV.SelectedItems.Count == 0)
                return;

            VPNTaskConfig old = LV.SelectedItems[0].Tag as VPNTaskConfig;
            VPNTaskConfig cur = new AddVPNServerForm().ShowDialogEdit(old);

            if (cur == VPNTaskConfig.Empty)
                return;

            LV.SelectedItems[0].Tag = cur;
            LV.SelectedItems[0].SubItems[1].Text = cur.Remark;
            LV.SelectedItems[0].SubItems[2].Text = cur.Host;
            LV.SelectedItems[0].SubItems[3].Text = cur.Port;   
            LV.SelectedItems[0].SubItems[4].Text = cur.Password; 
            LV.SelectedItems[0].SubItems[5].Text = cur.Method;
            LV.SelectedItems[0].SubItems[6].Text = cur.Status;
            LV.SelectedItems[0].SubItems[7].Text = cur.SSVersion;
            LV.SelectedItems[0].SubItems[8].Text = cur.ProbeInfo;
            LV.SelectedItems[0].SubItems[9].Text = cur.OtherInfo;
            LV.SelectedItems[0].SubItems[10].Text = cur.IP;
            LV.SelectedItems[0].SubItems[11].Text = cur.Country;
            LV.SelectedItems[0].SubItems[12].Text = cur.Content;
            // 按道理不会出现索引越界
            tasks[tasks.IndexOf(old)] = cur;
        }

        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in LV.SelectedItems)
                lvi.Tag = VPNTaskConfig.Empty;

            RefreshTasks(false);
            using (new LayoutGuarder(LV))
                foreach (ListViewItem lvi in LV.SelectedItems)
                    lvi.Remove();
        }

        private void RefreshTasks(bool create = true)
        {
            tasks.Clear();
            foreach (ListViewItem lvi in LV.Items)
            {
                VPNTaskConfig config = create ? new VPNTaskConfig(GetSubItemsTextArray(lvi)) :
                    lvi.Tag as VPNTaskConfig;
                // 针对删除菜单的优化,删除先置Empty后删除  
                if (config != VPNTaskConfig.Empty)
                {
                    lvi.Tag = config; // 关联
                    tasks.Add(config);
                }
            }
        }

        private string[] GetSubItemsTextArray(ListViewItem lvi)
        {
            List<string> array = new List<string>(lvi.SubItems.Count);
            for (int i = 0; i < lvi.SubItems.Count; i++)
                array.Add(lvi.SubItems[i].Text);
            return array.ToArray();
        }

        private void ClearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tasks.Clear();
            LV.Items.Clear();  // 不能删表头的clear方法
            using (GuarderUtil.WaitCursor)
                LV.Items.AddRange(NewLVIS(tasks));
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyToClipboard();
        }

        private void CopyToClipboard(int[] columns = null)
        {
            // 复制当前的选中单元格到粘贴板
            if (this.LV.SelectedItems.Count == 0)
                return;

            columns = columns ?? (new int[0]);
       
            StringBuilder sb = new StringBuilder();
            foreach (ListViewItem lvi in this.LV.SelectedItems)
            { 
                for (int i = 0; i < lvi.SubItems.Count; i++)
                    if (columns.Length == 0 || columns._Contains(i))
                        sb.Append(lvi.SubItems[i].Text).Append(OpUtil.TabSeparator);      
                sb.TrimEndT().AppendLine();
            }
            FileUtil.TryClipboardSetText(sb.ToString());
        }

        private void SaveResultsMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "BCP文件|*.bcp";
            dialog.FileName = "VPN专项-所有字段-" + DateTime.Now.ToString("yyyyMMddHHmm") + ".bcp";

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            using (GuarderUtil.WaitCursor)
                SaveResultToLocal(dialog.FileName);
        }



        private void RefreshBackColor()
        {
            foreach (ListViewItem lvi in LV.Items)
            {
                lvi.BackColor = isAlertnatingRows ? SingleRowColor : AltertnatingRowColor;
                isAlertnatingRows = !isAlertnatingRows;
            }
        }


        private void VPNMainForm_Load(object sender, EventArgs e)
        {
            LoadDB();
            RefreshLV();
            ResetSLabel();
            StaticItems();
        }

        private void 导出IP端口_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "BCP文件|*.bcp";
            dialog.FileName = "VPN专项-IP端口-" + DateTime.Now.ToString("yyyyMMddHHmm") + ".bcp";

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            using (GuarderUtil.WaitCursor)
                SaveResultToLocal(dialog.FileName, new int[] { 2, 3, 10 });
        }

        private void 导出分享地址_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "BCP文件|*.bcp";
            dialog.FileName = "VPN专项-分享地址-" + DateTime.Now.ToString("yyyyMMddHHmm") + ".bcp";

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            using (GuarderUtil.WaitCursor)
                SaveResultToLocal(dialog.FileName, new int[] { 12 });
        }

        private void CopySSMenuItem_Click(object sender, EventArgs e)
        {
            CopyToClipboard(new int[] { 12 });
        }

        private void CopyOtherMenuItem_Click(object sender, EventArgs e)
        {
            CopyToClipboard(new int[] { 9 });
        }

        private void CopyIPPortMenuItem_Click(object sender, EventArgs e)
        {
            // 先10后3, 待验证
            CopyToClipboard(new int[] { 2, 3, 10 });
        }

        private void LV_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditToolStripMenuItem_Click(sender, e);
        }

        private void VPNMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.actionNeedStop = true;
            using (GuarderUtil.WaitCursor)
            {
                RefreshTasks();
                SaveDB();
            }
        }

        private void 验活配置_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CAForm().ShowDialog();
        }

        private void 域名查IP_重新开始_ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 域名查IP_继续上次_ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 验活204_继续上次_ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void TCP验活_重新开始_ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void TCP验活_继续上次_ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Ping验活_重新开始_ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Ping验活_继续上次_ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 帮助文档_ToolStripLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
