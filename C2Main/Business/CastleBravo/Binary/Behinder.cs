using C2.Business.CastleBravo.Binary.Info;
using C2.Core;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace C2.Business.CastleBravo.Binary
{
    class Behinder
    {
        readonly RijndaelManaged cbc;  // AES128解密器 CBC模式
        public Behinder()
        {
            IteratorCount = 0;
            HitPassword = string.Empty;
            cbc = new RijndaelManaged
            {
                BlockSize = 128,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.None,
            };
        }
        public int IteratorCount { get; set; }
        public string HitPassword { get; set; }

        public bool Success { get; set; }

        public event EventHandler<EventArgs> OnIteratorCount;

        public string Descrypt(string text)
        {
            if (text.IsNullOrEmpty())
                return string.Empty;

            IteratorCount = 0;
            HitPassword = string.Empty;
            Success = false;

            // 加载字典
            // 尝试 XOR 解密
            // 尝试 AES 128解密            
            Password dict = Password.GetInstance();
            foreach (string p in dict.Pass)
            {
                IteratorCount++;

                if (IteratorCount % (1024 * 2) == 0)
                    OnIteratorCount?.Invoke(this, new EventArgs());

                HitPassword = p;
                string pass = ST.GenerateMD5(p).Substring(0, 16);

                string ret = XOR_Decrypt(text, pass);
                if (IsDecryptCorrect(ret))
                    return ret;

                ret = AES128_Decrypt(text, pass);
                if (IsDecryptCorrect(ret))
                    return ret;
            }

            HitPassword = string.Empty;
            Success = false;
            OnIteratorCount?.Invoke(this, new EventArgs());
            return string.Empty;
        }



        private string XOR_Decrypt(string text, string pass)
        {
            StringBuilder sb = new StringBuilder(text.Length);
            text = ST.TryDecodeBase64(text);

            for (int i = 0; i < text.Length; i++)
            {
                sb.Append((char)(text[i] ^ pass[(i + 1) & 15]));
            }
            return sb.ToString();
        }

        private string AES128_Decrypt(string text, string pass)
        {   // 默认密码 e45e329feb5d925b
            byte[] key_bytes = Encoding.ASCII.GetBytes(pass);  // 定然是128位
            byte[] text_bytes = ST.DecodeBase64ToBytes(text);

            ICryptoTransform transform = cbc.CreateDecryptor(key_bytes, key_bytes);
            byte[] ret_bytes = transform.TransformFinalBlock(text_bytes, 0, text_bytes.Length);
            ret_bytes = ret_bytes.Skip(16).ToArray();  // 去掉前16位

            return "assert|eval(base" + Encoding.ASCII.GetString(ret_bytes);
        }

        private bool IsDecryptCorrect(string value)
        {
            Success = value.Contains("assert|eval(base64_decode");
            return Success;
        }
    }
}
