using C2.Business.CastleBravo.VPN.Info;
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
        public static RandomProbeConfig RndProbeConfig { get; set; } = RandomProbeConfig.Empty;
        List<VPNTaskConfig> tasks = new List<VPNTaskConfig>();
        private FindSet finder;

        private HashSet<string> setOfIPAddress;
        private HashSet<string> setOfHost;

        private int NumberOfHost { get => setOfHost.Count; }
        private int NumberOfIPAddress { get => setOfIPAddress.Count; }

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

            setOfHost = new HashSet<string>();
            setOfIPAddress = new HashSet<string>();
        }

        private void InitializeToolStrip()
        {
            // 批量验活时, 与其他菜单项互斥
            enableItems = new ToolStripItem[]
            {
                this.editDDB,
                this.pcapDecryptMenu,
                this.helpInfoMenu,
                this.验活204Menu,
                this.checkAliveDDB,
                this.staticsMenu,
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
            actionNeedStop = true;
        }

        static bool isAlertnatingRows = true;
        static readonly Color SingleRowColor = Color.FromArgb(255, 217, 225, 242);
        static readonly Color AltertnatingRowColor = Color.FromArgb(255, 208, 206, 206);




        private void 随机探针_重新开始MenuItem_Click(object sender, System.EventArgs e)
        {
            RndProbeConfig = new RandomProbeForm().ShowDialog();
            if (RndProbeConfig.Equals(RandomProbeConfig.Empty))
                return;
            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
                SendRandomProbe(LV.Items);

        }

        private void 随机探针_继续上次MenuItem_Click(object sender, System.EventArgs e)
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
            HelpUtil.ShowMessageBox("复制【所有字段】到剪切板【成功】");
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

        private void Export(string exportType, int[] exportColumns, Func<VPNTaskConfig, bool> filter, Func<List<string>, List<string>> inplace)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "csv文件|*.csv",
                FileName = string.Format("VPN专项-{0}-{1}.csv", exportType, DateTime.Now.ToString("yyyyMMddHHmm")),
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            using (GuarderUtil.WaitCursor)
                SaveResultToLocal(dialog.FileName, exportColumns, filter, inplace);

            HelpUtil.ShowMessageBox(string.Format("导出到{0}【成功】", dialog.FileName));
        }

        private void Export(string exportType, int[] exportColumns, Func<VPNTaskConfig, bool> filter)
        {
            Export(exportType, exportColumns, filter, null);
        }

        private void Export(string exportType, int[] exportColumns)
        {
            Export(exportType, exportColumns, null);
        }

        private void 导出_IP端口_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export("IP端口", 
                new int[] { CI_端口, CI_IP地址, CI_归属地 },
                null,  
                (t) => { string tmp = t[0]; t[0] = t[1]; t[1] = tmp; return t; });  // 交换 端口 地址 位置
        }

        private void 导出_IP端口分享地址_toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            Export("IP端口分享地址", 
                new int[] { CI_端口, CI_IP地址, CI_归属地, CI_分享地址 }, 
                null, 
                (t) => { string tmp = t[0]; t[0] = t[1]; t[1] = tmp; return t; });  // 交换 端口 地址 位置
        }
        private void 导出_所有字段_MenuItem_Click(object sender, EventArgs e)
        {
            Export("所有字段", new int[0]);
        }

        private void 导出_分享地址_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export("分享地址", new int[] { CI_分享地址 });
        }

        private void 导出_IP归属地_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export("IP归属地", new int[] { CI_IP地址, CI_归属地 });
        }

        private void 导出_境内站点_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export("境内站点", new int[0], (t) => { return NetUtil.IsMainlandOfChina(t.Country); });
        }
        private void 导出_境外站点_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export("境外站点", new int[0], (t) => { return !NetUtil.IsMainlandOfChina(t.Country); });
        }



        private void CopySSMenuItem_Click(object sender, EventArgs e)
        {
            CopyToClipboard(new int[] { CI_分享地址 });
            HelpUtil.ShowMessageBox("复制【分享地址】到剪切板【成功】");
        }

        private void CopyOtherMenuItem_Click(object sender, EventArgs e)
        {
            CopyToClipboard(new int[] { CI_其他信息 });
            HelpUtil.ShowMessageBox("复制【其他信息】到剪切板【成功】");
        }

        private void CopyIPPortMenuItem_Click(object sender, EventArgs e)
        {
            // 先10后3, 待验证
            CopyToClipboard(new int[] { CI_主机地址, CI_端口, CI_IP地址, CI_归属地 });
            HelpUtil.ShowMessageBox("复制【IP和端口信息】到剪切板【成功】");
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

        private void 验活204_继续上次_ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 帮助文档_ToolStripLabel_Click(object sender, EventArgs e)
        {
            try
            {
                string helpfile = Path.Combine(Global.ResourcesPath, "Help", "VPN专项帮助文档.txt");
                Help.ShowHelp(this, helpfile);
            }
            catch { };
        }

        private void 选定项验活_HTTP204_ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 选定项验活_反查IP_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoItemsDNS(LV.SelectedItems);
        }

        private void 域名反查IP_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoItemsDNS(LV.Items);
        }


        private void Ping验活_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoItemsPing(LV.Items);
        }
        private void 选定项验活_Ping_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoItemsPing(LV.SelectedItems);
        }

        private void Tcp验活_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoItemsTcp(LV.Items);
        }

        private void 选定项验活_Tcp_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoItemsTcp(LV.SelectedItems);
        }

        private void ResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetSubItemsEmpty(LV.Items);
        }

        private void 删除重复项_域名_端口_密码_加密算法_客户端ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveDuplicateItems((t) => { return t.Host + t.Port + t.Password + t.Method + t.SSVersion; });
        }

        private void 删除重复项_域名_端口_密码_客户端_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveDuplicateItems((t) => { return t.Host + t.Port + t.Password + t.SSVersion; });
        }

        private void 删除重复项_域名_端口_密码_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveDuplicateItems((t) => { return t.Host + t.Port + t.Password; });
        }
        private void 删除重复项_域名_端口_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveDuplicateItems((t) => { return t.Host + t.Port; });
        }


        private void RemoveDuplicateItems(Func<VPNTaskConfig, string> method)
        {
            Dictionary<string, VPNTaskConfig> dict = new Dictionary<string, VPNTaskConfig>();

            foreach (ListViewItem item in LV.Items)
            {
                VPNTaskConfig task = item.Tag as VPNTaskConfig;
                string key = method(task);
                dict[key] = task;
            }

            tasks.Clear();
            tasks.AddRange(dict.Values);

            RefreshLV();    // 刷新LV
            ResetSLabel();  // 重新计算工具栏,状态栏信息
            StaticItems();  // 
            SaveDB();       // 写入文件
        }

        private void StaticsMenu_Click(object sender, EventArgs e)
        {
            new StaticForm(Static.DoStatic(LV)).ShowDialog();
        }


    }
}
