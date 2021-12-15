using C2.Core;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace C2.Business.CastleBravo.Binary
{
    class Xise
    {
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
            DESCryptoServiceProvider des;
            des = new DESCryptoServiceProvider
            {
                Mode = CipherMode.ECB,
                Key = Reverse(XOR8Bits("goklong soft")),
                IV = Reverse(XOR8Bits("goklong soft")),
            };
            byte[] bytes = ST.HexStringToBytes(val);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(bytes, 0, bytes.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Encoding.Default.GetString(ms.ToArray());
        }
    }
}
