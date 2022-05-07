using System.Collections.Generic;
using System.Text;

namespace C2.Business.CastleBravo.VPN.Info
{
    class Static
    {
        public static string DoStatic(IList<VPNTaskConfig> tasks)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("总计:")
              .AppendLine()
              .AppendLine("域名数:")
              .AppendLine("独立IP数:")
              .AppendLine("IP/端口数:")
              .AppendLine()
              .AppendLine("境内VPN服务器:")
              .AppendLine("境外VPN服务器:")
              .AppendLine()
              .AppendLine("加密算法分布: TODO")
              .AppendLine("协议类型分布: SS/SSR/VMESS/VLESS/Trojan   TODO")
              .AppendLine()
              .AppendLine("境内服务器省份分布:  TODO")
              .AppendLine("境外服务器国家分布:  TODO");

            return sb.ToString();
        }


    }
}
