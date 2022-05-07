using C2.Utils;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.VPN.Info
{
    class Static
    {
        public static string DoStatic(ListView LV)
        {
            StringBuilder sb = new StringBuilder();

            int total = LV.Items.Count;



            int numberofForeinVPN = 0;
            int numberOfMainlandVPN = 0;
            

            HashSet<string> ipSet = new HashSet<string>();
            HashSet<string> hostSet = new HashSet<string>();
            HashSet<string> iportSet = new HashSet<string>();

            foreach (ListViewItem lv in LV.Items)
            {
                VPNTaskConfig task = lv.Tag as VPNTaskConfig;
                ipSet.Add(task.IP);
                hostSet.Add(task.Host);
                iportSet.Add(task.IP + task.Port);

                _ = NetUtil.IsMainlandOfChina(task.Country) ? numberOfMainlandVPN++ : numberofForeinVPN++;
            }

            int numberOfIP = ipSet.Count;
            int numberOfHost = hostSet.Count;
            int numberOfIPort = iportSet.Count;

            sb.AppendLine(string.Format("总计: {0}",      total))
              .AppendLine()
              .AppendLine(string.Format("域名数: {0}",    numberOfHost))
              .AppendLine(string.Format("独立IP数: {0}",  numberOfIP))
              .AppendLine(string.Format("IP/端口数: {0}", numberOfIPort))
              .AppendLine()
              .AppendLine(string.Format("境内VPN服务器: {0}({1:P2})", numberOfMainlandVPN, total < 1 ? 0 : (float)numberOfMainlandVPN / total))
              .AppendLine(string.Format("境外VPN服务器: {0}({1:P2})", numberofForeinVPN, total   < 1 ? 0 : (float)numberofForeinVPN   / total))
              .AppendLine()
              .AppendLine("加密算法分布: TODO")
              .AppendLine()
              .AppendLine(string.Format("协议类型分布:{1}{2}{0}", StaticSS(LV), System.Environment.NewLine, OpUtil.Blank))
              .AppendLine()
              .AppendLine("境内服务器省份分布:  TODO")
              .AppendLine("境外服务器国家分布:  TODO");

            return sb.ToString();
        }

        public static string StaticSS(ListView LV)
        {
            int ss = 0;
            int ssr = 0;
            int vmess = 0;
            int vless = 0;
            int trojan = 0;
            int count = LV.Items.Count;

            foreach (ListViewItem lvi in LV.Items)
            {
                switch (lvi.SubItems[VPNMainForm.CI_客户端].Text)
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

            return string.Format("ss:{0}|ssr:{1}|vmess:{2}|vless:{3}|trojan:{4}",
                                  string.Format("{0}({1:P2})", ss, count < 1 ? 0 : (float)ss / count),
                                  string.Format("{0}({1:P2})", ssr, count < 1 ? 0 : (float)ssr / count),
                                  string.Format("{0}({1:P2})", vmess, count < 1 ? 0 : (float)vmess / count),
                                  string.Format("{0}({1:P2})", vless, count < 1 ? 0 : (float)vless / count),
                                  string.Format("{0}({1:P2})", trojan, count < 1 ? 0 : (float)trojan / count));
        }


    }
}
