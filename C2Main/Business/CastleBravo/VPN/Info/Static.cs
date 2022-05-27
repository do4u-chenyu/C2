using C2.Utils;
using C2.Core;
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
            HashSet<string> rssSet = new HashSet<string>();
            HashSet<string> hostSet = new HashSet<string>();
            HashSet<string> iportSet = new HashSet<string>();
            

            Dictionary<string, int> methodDict  = new Dictionary<string, int>();
            Dictionary<string, int> countryDict = new Dictionary<string, int>();

            foreach (ListViewItem lv in LV.Items)
            {
                VPNTaskConfig task = lv.Tag as VPNTaskConfig;
                ipSet.Add(task.IP);
                rssSet.Add(task.rssAddress);
                hostSet.Add(task.Host);
                iportSet.Add(task.IP + task.Port);

                if (methodDict.ContainsKey(task.Method))
                    methodDict[task.Method]++;
                else
                    methodDict[task.Method] = 1;

                _ = NetUtil.IsMainlandOfChina(task.Country) ? numberOfMainlandVPN++ :
                    NetUtil.IsAbroadChina(task.Country) ? numberofForeinVPN++ : 0;

                // 简易统计,头2个字就有足够的区分度了
                string country = task.Country.TrySubstring(0, 2);
                if (countryDict.ContainsKey(country))
                    countryDict[country]++;
                else
                    countryDict[country] = 1;
            }

            ipSet.Remove(string.Empty);
            rssSet.Remove(string.Empty);

            int numberOfIP = ipSet.Count;
            int numberOfRSS = rssSet.Count;
            int numberOfHost = hostSet.Count;
            int numberOfIPort = iportSet.Count;
            

            sb.AppendLine(string.Format("总计: {0}", total))
              .AppendLine()
              .AppendLine(string.Format("域名数: {0}", numberOfHost))
              .AppendLine(string.Format("独立IP数: {0}", numberOfIP))
              .AppendLine(string.Format("IP/端口数: {0}", numberOfIPort))
              .AppendLine(string.Format("订阅地址数: {0}", numberOfRSS))
              .AppendLine()
              .AppendLine(string.Format("境内: {0}({1:P2})", numberOfMainlandVPN, total < 1 ? 0 : (float)numberOfMainlandVPN / total))
              .AppendLine(string.Format("境外: {0}({1:P2})", numberofForeinVPN, total < 1 ? 0 : (float)numberofForeinVPN / total))
              .AppendLine()
              .AppendLine(string.Format("加密算法分布:{1}{2}{0}", GenStaticMethodString(methodDict, total), System.Environment.NewLine, OpUtil.Blank))
              .AppendLine()
              .AppendLine(string.Format("Stream流加密:{0}", GenStaticStreamString(methodDict, total)))
              .AppendLine()
              .AppendLine(string.Format("协议类型分布:{1}{2}{0}", StaticSS(LV), System.Environment.NewLine, OpUtil.Blank))
              .AppendLine()
              .AppendLine(string.Format("服务器地区分布:{1}{2}{0}", GenStaticCountryString(countryDict), System.Environment.NewLine, OpUtil.Blank));

            return sb.ToString();
        }

        private static string GenStaticStreamString(Dictionary<string, int> methodDict, int total)
        {
            int a = 0;
            foreach (var kv in methodDict)
            {
                switch (kv.Key)
                {
                    case "chacha20-ietf":
                    case "rc4-md5":
                    case "salsa20":
                    case "chacha20":
                    case "bf-cfb":
                    case "aes-256-cfb":
                    case "aes-192-cfb":
                    case "aes-128-cfb":
                    case "aes-256-ctr":
                    case "aes-192-ctr":
                    case "aes-128-ctr":
                    case "camellia-256-cfb":
                    case "camellia-192-cfb":
                    case "camellia-128-cfb":
                        a += kv.Value;
                        break;
                }
            }

            return string.Format("{0}({1:P2})", a, total < 1 ? 0 : (float)a / total);
        }

        private static string GenStaticMethodString(Dictionary<string, int> methodDict, int total)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var kv in methodDict)
            {
                sb.Append(string.Format("{0}:{1}({2:P2})|", 
                    kv.Key.IsNullOrEmpty() ? "空白" : kv.Key, 
                    kv.Value, 
                    total < 1 ? 0 : (float)kv.Value / total));
            }
            return sb.ToString().TrimEnd('|');
        }

        private static string GenStaticCountryString(Dictionary<string, int> dict)
        {

            // Method为空的不参与统计
            dict.Remove(string.Empty);
            StringBuilder sb = new StringBuilder();

            foreach (var kv in dict)
                sb.Append(string.Format("{0}:{1}|", kv.Key, kv.Value));

            return sb.ToString().TrimEnd('|');
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
