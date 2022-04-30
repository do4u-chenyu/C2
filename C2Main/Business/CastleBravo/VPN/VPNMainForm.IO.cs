using C2.Core;
using C2.Utils;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.VPN
{
    public partial class VPNMainForm
    {
        readonly string configFFP = Path.Combine(Global.ResourcesPath, "WebShellConfig", "vpnconfig.db");


        private void SaveResultToLocal(string path, int[] columns = null)
        {
            if (columns == null)
                columns = new int[0];

            StreamWriter sw = new StreamWriter(path, false, Encoding.Default);
            try
            {
                List<string> tmpLists = new List<string>();
                foreach (ListViewItem lvi in LV.Items)
                {
                    tmpLists.Clear();
                    for (int i = 0; i < lvi.SubItems.Count; i++)
                    {
                        if (columns.Length == 0 || columns._Contains(i))
                            tmpLists.Add(lvi.SubItems[i].Text.Replace("\r\n", OpUtil.StringBlank));
                    }         
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
        private void ResetSLabel()
        {
            ItemCountSLabel.Text = string.Format("共{0}项", LV.Items.Count);
            ProxyEnableSLabel.Text = "代理" + (Proxy.Enable ? "启用" : "关闭");
        }

        private void StaticItems()
        {
            // 统计 验活 数据
            StaticCA();
            // 统计 服务器 类型 占比
            StaticSS();
        }

        private void StaticSS()
        {
            int ss = 0;
            int ssr = 0;
            int vmess = 0;
            int vless = 0;
            int trojan = 0;
            int count = LV.Items.Count;

            foreach (ListViewItem lvi in LV.Items)
            {
                switch (lvi.SubItems[7].Text)
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

            StatusLabel2.Text = string.Format("ss:{0}|ssr:{1}|vmess:{2}|vless:{3}|trojan:{4}",
                                               string.Format("{0}({1:P2})", ss,     count < 1 ? 0 : (float)ss     / count),
                                               string.Format("{0}({1:P2})", ssr,    count < 1 ? 0 : (float)ssr    / count),
                                               string.Format("{0}({1:P2})", vmess,  count < 1 ? 0 : (float)vmess  / count),
                                               string.Format("{0}({1:P2})", vless,  count < 1 ? 0 : (float)vless  / count),
                                               string.Format("{0}({1:P2})", trojan, count < 1 ? 0 : (float)trojan / count));
        }

        private void StaticCA()
        {
            int dead = 0;
            int alive = 0;

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
            }

            StatusLabel1.Text = string.Format("活 {0} - 死 {1}  ", alive, dead);
        }

        private void LoadDB()
        {
            try
            {
                using (Stream stream = File.Open(configFFP, FileMode.Open))
                    tasks = new BinaryFormatter().Deserialize(stream) as List<VPNTaskConfig>;
            }
            catch { }
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
