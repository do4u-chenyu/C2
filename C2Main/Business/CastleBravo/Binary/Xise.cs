using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace C2.Business.CastleBravo.Binary
{
    class Xise
    {
        readonly DESCryptoServiceProvider des; 

        public Xise()
        {
            des = new DESCryptoServiceProvider
            {
                Mode = CipherMode.ECB,
                Key = Reverse(XOR8Bits("goklong soft"))
            };
        }
        private byte[] Reverse(byte[] bytes)
        {
            return bytes;
        }
        private byte[] XOR8Bits(string pass)
        {
            return Encoding.Default.GetBytes(pass);
        }
        public string Decrypt(string val)
        {
            return DESDecrypt(val) ;
        }

        private string DESDecrypt(string val)
        {
            MemoryStream ms = new MemoryStream();
            return string.Empty;
        }
    }
}
