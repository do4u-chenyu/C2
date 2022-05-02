﻿using System;
using System.Collections;
using System.Windows.Forms;
using static C2.Utils.GuarderUtil;

namespace C2.Business.CastleBravo.VPN
{
    public partial class VPNMainForm
    {
        private readonly int CI_创建时间 = 0;
        private readonly int CI_备注     = 1;
        private readonly int CI_主机地址 = 2;
        private readonly int CI_端口     = 3;
        private readonly int CI_密码     = 4;
        private readonly int CI_加密算法 = 5;
        private readonly int CI_验活     = 6;
        private readonly int CI_客户端   = 7;
        private readonly int CI_探针信息 = 8;
        private readonly int CI_其他信息 = 9;
        private readonly int CI_IP地址   = 10;
        private readonly int CI_归属地   = 11;
        private readonly int CI_分享地址 = 12;


        // 验活类型
        enum CATypeEnum
        {
            DNS_重新开始,
            DNS_继续上次,
            Ping_重新开始,
            Ping_继续上次,
            TCP_重新开始,
            TCP_继续上次,
            HTTP204_重新开始,
            HTTP204_继续上次,
        }

        private bool actionNeedStop = false;
        private int NumberOfAlive { get; set; }

        private void ResetProgressMenuValue(int progressMaxValue)
        {
            this.progressMenu.Text = string.Empty;
            this.progressBar.Value = 0;
            this.progressBar.Maximum = progressMaxValue;
            this.actionNeedStop = false;
            this.NumberOfAlive = 0;
        }

        // 哑元函数,过编译器检查用的
        private void Dummy()
        {  
            _ = actionNeedStop;
            _ = NumberOfAlive;
            _ = CI_创建时间;
            _ = CI_备注;
            _ = CI_主机地址;
            _ = CI_端口;
            _ = CI_密码;
            _ = CI_加密算法;
            _ = CI_客户端;
            _ = CI_探针信息;
            _ = CI_其他信息;
            _ = CI_分享地址;

        }
        private void ResetDnsSubItems()
        {
            foreach (ListViewItem lvi in LV.Items)
            {
                lvi.SubItems[CI_IP地址].Text = string.Empty;
                lvi.SubItems[CI_归属地].Text = string.Empty;
            }
        }

        private void Run_DNS_CA(IList items, bool skipAlive)
        {

            s = DateTime.Now;
            using (new ControlEnableGuarder(this.contextMenuStrip))
            using (new ToolStripItemEnableGuarder(this.enableItems))
            using (new ToolStripItemTextGuarder(this.actionStatusLabel, "进行中", "已完成"))
                foreach (ListViewItem lvi in items)
                {
                    if (actionNeedStop)
                        break;
                    // 启用二刷
                    if (skipAlive && lvi.SubItems[CI_验活].Text != "待")
                        continue;
                    Run_DNS_One(lvi);
                    UpdateProgress();
                    CheckSavePoint(); // 5分钟保存一次
                }
        }

        private void Run_DNS_One(ListViewItem lvi)
        {
            VPNTaskConfig task = lvi.Tag as VPNTaskConfig;
            string rts = "√";

            if (rts == "√")
            {
                this.NumberOfAlive++;
            }
            lvi.SubItems[CI_验活].Text = rts;
            lvi.SubItems[CI_IP地址].Text = task.IP;
            lvi.SubItems[CI_归属地].Text = task.Country;
            lvi.ListView.RedrawItems(lvi.Index, lvi.Index, false);
        }

        private void UpdateProgress()
        {
            this.progressMenu.Text = string.Format("{0}/{1} {3} - 活 {2} - CH {4}",
                ++progressBar.Value,
                progressBar.Maximum,
                NumberOfAlive,
                progressBar.Value == progressBar.Maximum ? "完成" : string.Empty,
                "未");
        }

        private void CheckSavePoint()
        {
            TimeSpan gap = DateTime.Now - s;
            if (gap.TotalMinutes >= 5)
            {   // 5分钟保存一次
                RefreshTasks();
                SaveDB();
                s = DateTime.Now;
            }
        }

        private void EndCheckAlive()
        {
            RefreshTasks();
            SaveDB();
        }
    }
}
