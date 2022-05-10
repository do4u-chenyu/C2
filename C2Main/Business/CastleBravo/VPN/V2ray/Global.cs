
namespace v2rayN
{
    class Global
    {
        public const string DefaultNetwork = "tcp";
        public const string None = "none";

        public const string InboundSocks = "socks";
        public const string InboundHttp = "http";
        public const string Loopback = "127.0.0.1";

        public const string SpeedPingTestUrl = @"https://www.google.com/generate_204";

        public static Job processJob
        {
            get; set;
        }
    }
}
