using C2.Utils;

namespace C2.Business.CastleBravo.VPN.Probe
{
    class ProbeFactory
    {
        public static byte[] RandomBytes(int length)
        {
            return RandomUtil.RandomBytes(length);
        } 
    }
}
