using System.Collections;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.VPN
{
    public partial class VPNMainForm
    {
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
        }
        private void ResetDnsSubItems()
        {
            foreach (ListViewItem lvi in LV.Items)
            {
                lvi.SubItems[10].Text = string.Empty;
                lvi.SubItems[11].Text = string.Empty;
            }
        }

        private void Run_DNS_CA(IList items, bool skipAlive)
        {
            _ = items;
            _ = skipAlive;


            // TODO
        }

        private void EndCheckAlive()
        {
            RefreshTasks();
            SaveDB();
        }
    }
}
