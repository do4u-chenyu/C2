
using C2.Business.CastleBravo.VPN.Client;

namespace v2rayN
{
    /// <summary>
    /// 大部分内容是从v2ray源码硬移植过来的
    /// </summary>
    class Global
    {
        public const string TcpHeaderHttp = "http";
        public const string DefaultNetwork = "tcp";
        public const string None = "none";

        public const string InboundSocks = "socks";
        public const string InboundHttp = "http";
        public const string agentTag = "proxy";
        public const string Loopback = "127.0.0.1";

        public const string ssProtocolLite = "shadowsocks";
        public const string vmessProtocolLite = "vmess";
        public const string socksProtocolLite = "socks";
        public const string vlessProtocolLite = "vless";
        public const string trojanProtocolLite = "trojan";

        public const string userEMail = "t@t.tt";

        public const string StreamSecurity = "tls";
        public const string StreamSecurityX = "xtls";

        public static string AbroadGenerate204 { get => ClientSetting.O204List[0]; }
        public static string ChinaGenerate204  { get => ClientSetting.I204List[0]; }

        public static Job processJob
        {
            get; set;
        }
    }
}
