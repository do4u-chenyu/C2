using C2.Business.CastleBravo.Binary.Info;
using C2.Core;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

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
                Padding = PaddingMode.Zeros,
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
            byte[] text_bytes = ST.DecodeBase64ToBytes(text);
            string text_deb64 = ST.TryDecodeBase64(text);
            if (text_bytes.IsNullOrEmpty() && text_deb64.IsNullOrEmpty())
                return "格式错误:不是Base64编码";

            string des = text_bytes.Length % 16 == 0 ?
                string.Empty : "报文有缺损,但勉强可以解码:" + Environment.NewLine;

            // 缩进到128位整倍数
            text_bytes = text_bytes.Take((text_bytes.Length >> 4) << 4).ToArray();

            foreach (string p in dict.Pass)
            {
                IteratorCount++;

                if (IteratorCount % (1024 * 2) == 0)
                    OnIteratorCount?.Invoke(this, new EventArgs());

                HitPassword = p;
                string pass = ST.GenerateMD5(p).Substring(0, 16);

                string ret = XOR_Decrypt(text_deb64, pass);
                if (IsDecryptCorrect(ret))
                    return des + ret;

                ret = AES128_Decrypt(text_bytes, pass);
                if (IsDecryptCorrect(ret))
                    return des + ret;
            }

            HitPassword = string.Empty;
            Success = false;
            OnIteratorCount?.Invoke(this, new EventArgs());
            return "没有命中";
        }

        // 原生的behinder报文一般人看不懂,需要格式化一下
        public string Format(string plain)
        {
            string tpl = "assert\\|eval\\(base64_decode\\('([0-9A-Za-z+/]+=*)'";
            Match mat = Regex.Match(plain, tpl);
            if (mat.Success)
            {
                string payload = mat.Groups[1].Value;
                payload = ST.TryDecodeBase64(payload);
                if (!payload.IsNullOrEmpty())
                {
                    return "解密成功, 攻击载荷:" + Environment.NewLine +
                        "=======================================" +
                        string.Format("{1}{0}{1}", payload, Environment.NewLine) +
                        "=================原生报文==============" +
                        string.Format("{1}{0}{1}", plain, Environment.NewLine) +
                        "=======================================";

                }
            }
            return plain;
        }



        private string XOR_Decrypt(string text, string pass)
        {
            StringBuilder sb = new StringBuilder(text.Length);
            for (int i = 0; i < text.Length; i++)
            {
                sb.Append((char)(text[i] ^ pass[(i + 1) & 15]));
            }
            return sb.ToString();
        }

        private string AES128_Decrypt(byte[] text_bytes, string pass)
        {   
            
            byte[] key_bytes = Encoding.ASCII.GetBytes(pass);  // 定然是128位
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
