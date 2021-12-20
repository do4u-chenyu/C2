using C2.Core;
using C2.Utils;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace C2.Business.CastleBravo.Binary
{
    class Xise
    {
        private byte[] XOR8(string pass)
        {
            int i = 0;
            byte[] bytes8 = new byte[8];
            byte[] bytesP = Encoding.Default.GetBytes(pass);
            foreach(byte b in bytesP)
            {
                bytes8[i] = (byte)(b ^ bytes8[i]);
                i = i < 7 ? i + 1 : 0;
            }
            return bytes8;
        }
        public string Decrypt(string val)
        {
            byte[] bytes = ST.DecimalHexStringToBytes("122?57?118?39?232?250?196?214?", "?");
            return DESDecrypt(bytes) ;
        }

        private string DESDecrypt(byte[] bytes)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider
            {
                Mode = CipherMode.ECB,
                Key = ConvertUtil.ReverseBytes(XOR8("goklong soft")),
            };
            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(bytes, 0, bytes.Length);
                try
                { 
                    cs.FlushFinalBlock();  // 这里无论如何都解不出来
                                           // 只能硬刚解密算法了, 正在进行中
                }
                catch
                {
                    return "密码串解密错误";
                }
                return Encoding.Default.GetString(ms.ToArray());
            }
        }
    }
}
