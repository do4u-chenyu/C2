
using C2.Business.CastleBravo.VPN.Client;

namespace v2rayN
{
    class Global
    {
        public const string DefaultNetwork = "tcp";
        public const string None = "none";

        public const string InboundSocks = "socks";
        public const string InboundHttp = "http";
        public const string Loopback = "127.0.0.1";

        public static string AbroadGenerate204 { get => ClientSetting.O204List[0]; }
        public static string ChinaGenerate204  { get => ClientSetting.I204List[0]; }

        public static Job processJob
        {
            get; set;
        }
    }
}
