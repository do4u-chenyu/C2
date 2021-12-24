using C2.Core;

namespace C2.Business.CastleBravo.Binary
{
    class Behinder
    {
        public Behinder()
        {

        }

        private byte[] Try(string plainText)
        {
            byte[] bytes = ST.DecodeBase64ToBytes(plainText);
            return bytes;
        }

        private string XOR_Decrypt()
        {
            return string.Empty;
        }

        private string AES128_Decrypt()
        {
            return string.Empty;
        }
    }
}
