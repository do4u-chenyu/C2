using System;
using System.Collections;
using System.Windows.Forms;
using static C2.Utils.GuarderUtil;

namespace C2.Business.CastleBravo.VPN
{

    public partial class VPNMainForm
    {
        private DateTime s;  // 自动保存
        #region 随机探针
        // 选定项发探针
        // 重新发探针
        // 继续发探针

        private void SendRandomProbe(IList items)
        {
            s = DateTime.Now;
            using (new ToolStripItemTextGuarder(this.actionStatusLabel, "进行中", "已完成"))
                foreach (ListViewItem lvi in items)
                {
                    if (actionNeedStop)
                        break;
                    UpdateRandomProbeItem(lvi);
                                          // 5分钟保存一次
                }
        }
        private void UpdateRandomProbeItem(ListViewItem lvi)
        {
            // VPNTaskConfig task = lvi.Tag as VPNTaskConfig;
            lvi.SubItems[CI_探测信息].Text = string.Empty;
        }
        #endregion
        #region 重放探针
        #endregion
    }
}
