using C2.Business.CastleBravo.WebShellTool;
using C2.Utils;
using System.Collections.Generic;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.VPN
{
    public partial class VPNMainForm
    {

        private ListViewItem[] NewLVIS(IList<VPNTaskConfig> tasks)
        {
            ListViewItem[] lvis = new ListViewItem[tasks.Count];
            for (int i = 0; i < lvis.Length; i++)
                lvis[i] = NewLVI(tasks[i]);
            return lvis;
        }

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
            lvi.SubItems.Add(config.OtherInfo);
            lvi.SubItems.Add(config.IP);
            lvi.SubItems.Add(config.Country);
            lvi.SubItems.Add(config.Content);
            lvi.SubItems.Add(config.ssAddress);

            // 指针关联
            lvi.Tag = config;
            // 设置间隔行背景色
            lvi.BackColor = isAlertnatingRows ? SingleRowColor : AltertnatingRowColor;
            isAlertnatingRows = !isAlertnatingRows;
            return lvi;
        }

        private void LV_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            LVComparer c = LV.ListViewItemSorter as LVComparer;
            c.col = e.Column;
            c.asce = !c.asce;
            using (GuarderUtil.WaitCursor)
            using (new GuarderUtil.LayoutGuarder(LV))
            {
                LV.Sort();
                RefreshTasks(false); // 回写任务, 速度慢, 将来要优化
                RefreshBackColor();  // 重新布局
            }
        }

        private void RefreshLV()
        {
            LV.Items.Clear();  // 不能删表头的clear方法
            using (GuarderUtil.WaitCursor)
                LV.Items.AddRange(NewLVIS(tasks));
        }
    }
}
