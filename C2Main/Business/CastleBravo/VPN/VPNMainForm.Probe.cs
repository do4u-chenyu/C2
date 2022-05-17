using C2.Business.CastleBravo.VPN.Probe;
using C2.Core;
using C2.Utils;
using System;
using System.Collections;
using System.IO;
using System.Text;
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

        private void SendRandomProbe(IList items, bool isContinue = false)
        {
            foreach (ListViewItem lvi in items)
            {
                if (actionNeedStop)
                    break;
                
                if (isContinue && lvi.SubItems[CI_状态].Text != Todo)
                    continue;

                UpdateOneRandomProbeResult(lvi);
                Application.DoEvents();
                CheckSavePoint(1);// 1分钟保存一次
            }
        }

        /// <summary>
        /// 测试用
        /// </summary>
        /// <param name="items"></param>
        /// <param name="isContinue"></param>
        private void SendRandomProbeMock(IList items, bool isContinue = false)
        {
            return;
        }

        private bool SetRandomProbeConfig(IList items)
        {
            RndProbeConfig = new RandomProbeForm().ShowDialog();
            return !RndProbeConfig.IsEmpty() && ResetSubItemEmpty(items, CI_探测信息);
        }

        private void DoRandomProbe(IList items, bool isContinue = false)
        {
            s = DateTime.Now;
            if (!SetRandomProbeConfig(items))
                return;
            using (new CursorGuarder(Cursors.WaitCursor))
            using (new ToolStripItemTextGuarder(this.actionStatusLabel, "进行中", "已完成"))
                SendRandomProbe(items, isContinue);
                //SendRandomProbeMock(items, isContinue);
        }

        private void UpdateOneRandomProbeResult(ListViewItem lvi)
        {
            VPNTaskConfig task = lvi.Tag as VPNTaskConfig;
            int port = ConvertUtil.TryParseInt(task.Port);

            if (port == 0)
            {
                lvi.SubItems[CI_探测信息].Text = "端口不正确";
                return;
            }

            if (NetUtil.IsIPAddress(task.IP))
            {
                lvi.SubItems[CI_探测信息].Text = "IP字段格式错误";
                return;
            }

            if (NetUtil.IPCheck(task.IP) != task.IP)
            {
                lvi.SubItems[CI_探测信息].Text = string.Format("IP字段格式错误:{0}", NetUtil.IPCheck(task.IP));
                return;
            }

            string ffp = TouchResultFile(task.IP, port);

            using (StreamWriter sw = new StreamWriter(ffp))
            {
                foreach (int length in RndProbeConfig.LengthValues)
                {
                    for (int i = 0; i < RndProbeConfig.SendCount; i++)
                    {
                        Application.DoEvents(); // 缓卡
                        sw.WriteLine(SocketClient.RndProbeResponse(task.IP, port, GenRndProbeRequest(length), RndProbeConfig.TimeoutSeconds));
                    }
                    sw.Flush();
                }
            }           
            lvi.SubItems[CI_探测信息].Text = ffp;
        }

        private byte[] GenRndProbeRequest(int length)
        {
            if (RndProbeConfig.LengthValues.Count == 0)
                return new byte[0];  // TODO 实现

            return ProbeFactory.RandomBytes(length);
        }
        private string TouchResultFile(string ip, int port)
        {
            string time = DateTime.Now.ToString("yyyyMMddhhmmss");
            string path = Path.Combine(Global.UserWorkspacePath, "探针结果采集", "随机探针");
            string filename = string.Format("{0}_{1}_{2}.txt", ip, port, time);
            FileUtil.CreateDirectory(path);
            return Path.Combine(path, filename);
        }

        #endregion


        #region 菜单

        private void LV_MouseClick(object sender, MouseEventArgs e)
        {
            this.LV.ContextMenuStrip = this.contextMenuStrip;

            if (e.Button != MouseButtons.Right || e.Clicks != 1 || LV.SelectedItems.Count == 0)
                return;

            ListViewItem item = LV.SelectedItems[0];
            ListViewItem.ListViewSubItem subItem = item.GetSubItemAt(e.X, e.Y);

            if (subItem == null || item.SubItems.IndexOf(subItem) != CI_探测信息)
                return;

            if (!subItem.Text.StartsWith(Path.Combine(Global.UserWorkspacePath, "探针结果采集")))
                return;

            this.LV.ContextMenuStrip = this.contextMenuStrip1;

        }

        private void OpenFileMenu_Click(object sender, EventArgs e)
        {
            ProcessUtil.ProcessOpen(CurrentFilePath());
        }

        private void OpenDirMenu_Click(object sender, EventArgs e)
        {
            FileUtil.ExploreDirectory(CurrentFilePath());
        }

        private void CopyDirMenu_Click(object sender, EventArgs e)
        {
            FileUtil.TryClipboardSetText(CurrentFilePath());
        }
        private string CurrentFilePath()
        {
            return LV.SelectedItems[0].SubItems[CI_探测信息].Text;
        }
        #endregion
    }
}
