using C2.Core;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.VPN
{
    public partial class VPNMainForm
    {
        readonly string configFFP = Path.Combine(Global.ResourcesPath, "WebShellConfig", "vpnconfig.db");

        private void ResetSLabel()
        {
            ItemCountSLabel.Text = string.Format("共{0}项", LV.Items.Count);
            ProxyEnableSLabel.Text = "代理" + (Proxy.Enable ? "启用" : "关闭");
        }

        private void StaticItems()
        {
            int dead  = 0;
            int alive = 0;

            int ss     = 0;
            int ssr    = 0;
            int vmess  = 0;
            int vless  = 0;
            int trojan = 0;
            int count  = LV.Items.Count;

            foreach (ListViewItem lvi in LV.Items)
            {
                switch (lvi.SubItems[6].Text)
                {
                    case "√":
                        alive++;
                        break;
                    case "×":
                        dead++;
                        break;
                }

                switch(lvi.SubItems[7].Text)
                {
                    case "SS":
                        ss++;
                        break;
                    case "SSR":
                        ssr++;
                        break;
                    case "VMESS":
                        vmess++;
                        break;
                    case "VLESS":
                        vless++;
                        break;
                    case "TROJAN":
                        trojan++;
                        break;
                }
            }

            StatusLabel.Text = string.Format("活 {0} - 死 {1}  ss:{2}|ssr:{3}|vmess:{4}|vless:{5}|trojan:{6}", 
                alive, 
                dead,
                string.Format("{0}({1:P2})", ss,     count < 1 ? 0 : (float)ss     / count),
                string.Format("{0}({1:P2})", ssr,    count < 1 ? 0 : (float)ssr    / count),
                string.Format("{0}({1:P2})", vmess,  count < 1 ? 0 : (float)vmess  / count),
                string.Format("{0}({1:P2})", vless,  count < 1 ? 0 : (float)vless  / count),
                string.Format("{0}({1:P2})", trojan, count < 1 ? 0 : (float)trojan / count));
        }

        private void SaveDB()
        {
            ResetSLabel();
            StaticItems();
            try
            {
                using (Stream stream = File.Open(configFFP, FileMode.Create))
                    new BinaryFormatter().Serialize(stream, tasks);
            }
            catch { }
        }
    }
}
