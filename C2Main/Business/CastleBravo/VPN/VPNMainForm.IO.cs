using C2.Business.CastleBravo.VPN.Info;
using C2.Core;
using C2.Utils;
using System;
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


        private void SaveResultToLocal(string path, int[] columns = null, Func<VPNTaskConfig, bool> filter = null)
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

                    // 自定义条件过滤
                    if (filter != null && !filter(lvi.Tag as VPNTaskConfig))
                        continue;

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
            StatusLabel2.Text = Static.StaticSS(LV);
        }

        private void StaticCA()
        {
            int dead = 0;
            int alive = 0;

            foreach (ListViewItem lvi in LV.Items)
            {
                switch (lvi.SubItems[CI_状态].Text)
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
