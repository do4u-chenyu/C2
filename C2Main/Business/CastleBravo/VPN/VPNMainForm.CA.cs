using C2.Utils;
using C2.Core;
using System;
using System.Collections;
using System.Linq;
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
        private readonly int CI_状态     = 6;
        private readonly int CI_客户端   = 7;
        private readonly int CI_探测信息 = 8;
        private readonly int CI_其他信息 = 9;
        private readonly int CI_IP地址   = 10;
        private readonly int CI_归属地   = 11;
        private readonly int CI_分享地址 = 12;

        private readonly string Succ = "√";
        private readonly string Fail = "×";
        private readonly string Todo = "待";


        // 验活类型
        enum CATypeEnum
        {
            DNS,
            Ping,
            TCP,
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
            _ = CI_探测信息;
            _ = CI_其他信息;
            _ = CI_分享地址;

        }
        private void ResetDnsSubItems(IList items)
        {
            foreach (ListViewItem lvi in items)
            {
                lvi.SubItems[CI_状态].Text = Todo;
                lvi.SubItems[CI_IP地址].Text = string.Empty;
                lvi.SubItems[CI_归属地].Text = string.Empty;
            }
        }

        // DNS验活
        private void DoItemsDNS(IList Items)
        {
            //  进度条重置
            ResetProgressMenuValue(Items.Count);
            //  相关内容域重置
            ResetDnsSubItems(Items);
            //  DNS反查
            Run_DNS_CA(Items);
            //  收尾
            EndCheckAlive();
        }

        // Ping验活
        private void DoItemsPing(IList Items)
        {
            //  进度条重置
            ResetProgressMenuValue(Items.Count);
            //  相关内容域重置
            ResetDnsSubItems(Items);
            
            Run_Ping_CA(Items);
            //  收尾
            EndCheckAlive();
        }

        // TCP验活
        private void DoItemsTcp(IList Items)
        {
            //  进度条重置
            ResetProgressMenuValue(Items.Count);
            //  相关内容域重置
            ResetDnsSubItems(Items);
        
            Run_Tcp_CA(Items);
            //  收尾
            EndCheckAlive();
        }

        private void Run_Ping_CA(IList items)
        {
            Run_XXX_CA(items, CATypeEnum.Ping);
        }

        private void Run_Tcp_CA(IList items)
        {
            Run_XXX_CA(items, CATypeEnum.TCP);
        }

        private void Run_DNS_CA(IList items)
        {
            Run_XXX_CA(items, CATypeEnum.DNS);
        }

        private void Run_XXX_CA(IList items, CATypeEnum type)
        {

            s = DateTime.Now;
            using (new ControlEnableGuarder(this.contextMenuStrip))
            using (new ToolStripItemEnableGuarder(this.enableItems))
            using (new ToolStripItemTextGuarder(this.actionStatusLabel, "进行中", "已完成"))
                foreach (ListViewItem lvi in items)
                {
                    if (actionNeedStop)
                        break;

                    switch (type)
                    {
                        case CATypeEnum.DNS:
                            Run_DNS_One(lvi);
                            break;
                        case CATypeEnum.Ping:
                            Run_Ping_One(lvi);
                            break;
                        case CATypeEnum.TCP:
                            Run_Tcp_One(lvi);
                            break;
                    }
                    
                    UpdateProgress();
                    CheckSavePoint(); // 5分钟保存一次
                }
        }

        private void Run_DNS_One(ListViewItem lvi)
        {
            VPNTaskConfig task = lvi.Tag as VPNTaskConfig;

            IPAddressUpdate(task);

            UpdateRedrawItem(lvi);
        }

        private void UpdateRedrawItem(ListViewItem lvi)
        {
            VPNTaskConfig task = lvi.Tag as VPNTaskConfig;
            this.NumberOfAlive++;
            lvi.SubItems[CI_状态].Text = task.Status;
            lvi.SubItems[CI_IP地址].Text = task.IP;
            lvi.SubItems[CI_归属地].Text = task.Country;
            lvi.SubItems[CI_探测信息].Text = task.ProbeInfo;
            lvi.ListView.RedrawItems(lvi.Index, lvi.Index, false);
        }

        private void Run_Ping_One(ListViewItem lvi)
        {
            VPNTaskConfig task = lvi.Tag as VPNTaskConfig;

            // 先dns出IP地址
            if (task.IP.IsNullOrEmpty())
                IPAddressUpdate(task);  

            // 回写task
            PingInfoUpdate(task);

            // 回刷界面
            UpdateRedrawItem(lvi);
        }

        private void Run_Tcp_One(ListViewItem lvi)
        {
            VPNTaskConfig task = lvi.Tag as VPNTaskConfig;

            // 先dns出IP地址
            if (task.IP.IsNullOrEmpty())
                IPAddressUpdate(task);

            RefreshTcpInfo(task);

            // 回刷界面
            UpdateRedrawItem(lvi);
        }

        private void IPAddressUpdate(VPNTaskConfig task)
        {
            string[] ipList = NetUtil.GetHostAddressList(task.Host);
            task.IP = ipList[0];

            // DNS出多个IP时,放到探针信息里
            if (ipList.Length > 1)
                task.ProbeInfo = ipList.Skip(1).JoinString(",");

            task.Status = NetUtil.IPCheck(task.IP) == task.IP ? Succ : Fail;
            task.Country = NetUtil.IPQuery_WhoIs(task.IP);
            
            Application.DoEvents();
        }

        private void PingInfoUpdate(VPNTaskConfig task)
        {
            if (!NetUtil.IsIPAddress(task.IP))
            {
                task.ProbeInfo = "IP地址格式不对";
                return;
            }

            long replyTime = NetUtil.Ping(task.IP);

            task.Status = replyTime == -1 ? Fail : Succ;
            task.ProbeInfo = replyTime == -1 ? "Ping不通" : "Ping通:" + replyTime + "ms";
  
            Application.DoEvents();
        }

        private void RefreshTcpInfo(VPNTaskConfig task)
        {
            if (!NetUtil.IsIPAddress(task.IP))
            {
                task.ProbeInfo = "IP地址格式不对";
                return;
            }

            Application.DoEvents();
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
