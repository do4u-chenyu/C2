using C2.Business.CastleBravo.WebShellTool;
using C2.Business.CastleBravo.WebShellTool.SettingsDialog;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            using (GuarderUtil.WaitCursor)
                LV.Items.AddRange(NewLVIS(dialog.Tasks));
            tasks.AddRange(dialog.Tasks);
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

        private ListViewItem[] NewLVIS(IList<VPNTaskConfig> tasks)
        {
            ListViewItem[] lvis = new ListViewItem[tasks.Count];
            for (int i = 0; i < lvis.Length; i++)
                lvis[i] = NewLVI(tasks[i]);
            return lvis;
        }

        private void 重新开始ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {

        }

        private void 继续上次ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {

        }

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.LV.SelectedItems.Count == 0)
                return;

            VPNTaskConfig old = LV.SelectedItems[0].Tag as VPNTaskConfig;
            VPNTaskConfig cur = new AddVPNServerForm().ShowDialog(ST.NowString());

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
            LV.SelectedItems[0].SubItems[7].Text = cur.ProbeInfo;
            LV.SelectedItems[0].SubItems[9].Text = cur.IP;         
            LV.SelectedItems[0].SubItems[9].Text = cur.Country;    
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
            // 复制当前的选中单元格到粘贴板
            if (this.LV.SelectedItems.Count == 0)
                return;

            StringBuilder sb = new StringBuilder();
            foreach (ListViewItem lvi in this.LV.SelectedItems)
            {
                for (int i = 0; i < lvi.SubItems.Count; i++)
                    sb.Append(lvi.SubItems[i].Text).Append(OpUtil.TabSeparator);
                sb.TrimEndT().AppendLine();
            }
            FileUtil.TryClipboardSetText(sb.ToString());
        }

        private void SaveResultsMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "BCP文件|*.bcp";
            dialog.FileName = "VPN专项" + DateTime.Now.ToString("yyyyMMddHHmm") + ".bcp";

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            using (GuarderUtil.WaitCursor)
                SaveResultToLocal(dialog.FileName);
        }

        private void SaveResultToLocal(string path)
        {
            StreamWriter sw = new StreamWriter(path, false);
            try
            {
                List<string> tmpLists = new List<string>();
                foreach (ListViewItem lvi in LV.Items)
                {
                    tmpLists.Clear();
                    for (int i = 0; i < lvi.SubItems.Count; i++)
                        tmpLists.Add(lvi.SubItems[i].Text.Replace("\r\n", OpUtil.StringBlank));
                    sw.WriteLine(string.Join("\t", tmpLists.ToArray()));
                }
            }
            catch { }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }
    }
}
