using C2.Utils;
using C2.Core;
using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using static C2.Utils.GuarderUtil;
using System.Collections.Generic;
using v2rayN.Handler;

namespace C2.Business.CastleBravo.VPN
{
    public partial class VPNMainForm
    {
        public static readonly int CI_创建时间 = 0;
        public static readonly int CI_备注     = 1;
        public static readonly int CI_主机地址 = 2;
        public static readonly int CI_端口     = 3;
        public static readonly int CI_密码     = 4;
        public static readonly int CI_加密算法 = 5;
        public static readonly int CI_状态     = 6;
        public static readonly int CI_客户端   = 7;
        public static readonly int CI_探测信息 = 8;
        public static readonly int CI_其他信息 = 9;
        public static readonly int CI_IP地址   = 10;
        public static readonly int CI_归属地   = 11;
        public static readonly int CI_分享地址 = 12;

        private readonly string Succ = "√";
        private readonly string Fail = "×";
        private readonly string Todo = "待";
        private readonly string Done = "Done";


        // 验活类型
        enum CATypeEnum
        {
            DNS,
            Ping,
            TCP,
            HTTP204,
            HTTP204_重新开始,
            HTTP204_继续上次,
        }

        private bool actionNeedStop = false;
        private int NumberOfAlive { get; set; }
        private int NumberOfChina { get; set; }

        private void ResetProgressMenuValue(int progressMaxValue)
        {
            this.progressMenu.Text = string.Empty;
            this.progressBar.Value = 0;
            this.progressBar.Maximum = progressMaxValue;
            this.actionNeedStop = false;
            this.NumberOfAlive = 0;
            this.NumberOfChina = 0;
            this.setOfIPAddress.Clear();
            this.setOfHost.Clear();
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
        private void ResetSubItemsTodo(IList items)
        {
            ResetSubItem(items, Todo, string.Empty, string.Empty, string.Empty);
        }

        private void ResetSubItemsEmpty(IList items)
        {
            ResetSubItem(items, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        private void ResetSubItem(IList items, string s1, string s2, string s3, string s4)
        {
            foreach (ListViewItem lvi in items)
            {
                VPNTaskConfig task = lvi.Tag as VPNTaskConfig;
                task.Status    = lvi.SubItems[CI_状态].Text = s1;
                task.IP        = lvi.SubItems[CI_IP地址].Text = s2;
                task.Country   = lvi.SubItems[CI_归属地].Text = s3;
                task.ProbeInfo = lvi.SubItems[CI_探测信息].Text = s4;
            }
        }

        // DNS验活
        private void DoItemsDNS(IList items)
        {
            //  进度条重置
            ResetProgressMenuValue(items.Count);
            //  相关内容域重置
            ResetSubItemsTodo(items);
            //  DNS反查
            Run_DNS_CA(items);
            //  收尾
            EndCheckAlive();
        }

        // Ping验活
        private void DoItemsPing(IList items)
        {
            //  进度条重置
            ResetProgressMenuValue(items.Count);
            //  相关内容域重置
            ResetSubItemsTodo(items);
            
            Run_Ping_CA(items);
            //  收尾
            EndCheckAlive();
        }

        // TCP验活
        private void DoItemsTcp(IList items)
        {
            //  进度条重置
            ResetProgressMenuValue(items.Count);
            //  相关内容域重置
            ResetSubItemsTodo(items);
        
            Run_Tcp_CA(items);
            //  收尾
            EndCheckAlive();
        }

        // HTTP验活整个逻辑跟其他的都不一样
        // 需要定制化
        private void DoItemHttp204(IList items)
        {
            //  进度条重置
            ResetProgressMenuValue(items.Count);
            //  相关内容域重置
            ResetSubItemsTodo(items);
            
            Run_Http204_CA(items);

            //  收尾
            EndCheckAlive();
        }

        private void DoItemHttp204Continue(IList items)
        {
            List<ListViewItem> itemsContinue = new List<ListViewItem>();

            foreach (ListViewItem lvi in items)
            {
                if (lvi.SubItems[CI_状态].Text == Todo)
                    itemsContinue.Add(lvi);

                if (lvi.SubItems[CI_状态].Text == string.Empty)
                    itemsContinue.Add(lvi);
            }

            DoItemHttp204(itemsContinue);
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

        // 批量并发 HTTP204 验活用的控制变量
        ListViewItem last204 = new ListViewItem();                  // 结尾标识
        List<ListViewItem> buffer204 = new List<ListViewItem>();    // 堆积缓存

        private void Run_Http204_CA(IList items)
        {
            // 跑 HTTP204 之前的工作
            buffer204.Clear();
            last204 = items[items.Count - 1] as ListViewItem;
            Run_XXX_CA(items, CATypeEnum.HTTP204);
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
                            using (WaitCursor)         // TCP 时间比较长
                                Run_Tcp_One(lvi);
                            break;
                        case CATypeEnum.HTTP204:
                            Run_Http204_One(lvi);
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

        private void Run_Http204_One(ListViewItem lvi)
        {
            buffer204.Add(lvi);
            if (buffer204.Count >= 20 || lvi == last204)
            {
                Run_Http204_V2ray(buffer204);
                buffer204.Clear();
            }
        }

        private void Run_Http204_V2ray(List<ListViewItem> lv)
        {
            int pid = -1;
            _ = lv;
            int startPort = v2rayN.Utils.GetAvailablePort();
            _ = startPort;

            pid = new V2rayHandler().LoadV2rayConfigString(lv);
            _ = pid;

            //  选择端口
            //  构造v2ray配置文件 
            //  启动v2ray进程
            //  并发访问代理端口,设置好回调更新函数
            //  Join等待并发结束
        }

        private void UpdateRedrawItem(ListViewItem lvi)
        {
            VPNTaskConfig task = lvi.Tag as VPNTaskConfig;
            NumberOfAlive = task.Status == Succ ? NumberOfAlive + 1 : NumberOfAlive;
            NumberOfChina = NetUtil.IsMainlandOfChina(task.Country) ? NumberOfChina + 1 : NumberOfChina;

            lvi.SubItems[CI_状态].Text = task.Status;
            lvi.SubItems[CI_IP地址].Text = task.IP;
            lvi.SubItems[CI_归属地].Text = task.Country;
            lvi.SubItems[CI_探测信息].Text = task.ProbeInfo;
            lvi.ListView.RedrawItems(lvi.Index, lvi.Index, false);
        }

        private void Run_Ping_One(ListViewItem lvi)
        {
            VPNTaskConfig task = lvi.Tag as VPNTaskConfig;


            IPAddressUpdate(task);  

            // 回写task
            PingInfoUpdate(task);

            // 回刷界面
            UpdateRedrawItem(lvi);
        }

        private void Run_Tcp_One(ListViewItem lvi)
        {
            VPNTaskConfig task = lvi.Tag as VPNTaskConfig;


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

            task.Status = NetUtil.IPCheck(task.IP) == task.IP ? Done : Fail;
            task.Country = NetUtil.IPQuery_WhoIs(task.IP);

            this.setOfHost.Add(task.Host);
            this.setOfIPAddress.UnionWith(ipList);
            
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

            if (!NetUtil.IsPort(task.Port))
            {
                task.ProbeInfo = "端口格式不对";
                return;
            }

            Tuple<long, string> ret = NetUtil.Tcp(task.IP, task.Port);
            long replyTime = ret.Item1;
            string message = ret.Item2;

            task.Status = replyTime == -1 ? Fail : Succ;
            task.ProbeInfo = replyTime == -1 ? "Tcp不通:" + message : "Tcp通:" + replyTime + "ms";
            Application.DoEvents();
        }

        private void UpdateProgress()
        {
            this.progressMenu.Text = string.Format("{0}/{1} {3} - 活 {2} - 站 {5} - IP {6} - 国内 {4}",
                ++progressBar.Value,
                progressBar.Maximum,
                NumberOfAlive,
                progressBar.Value == progressBar.Maximum ? "完成" : string.Empty,
                NumberOfChina,
                NumberOfHost,
                NumberOfIPAddress);
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
