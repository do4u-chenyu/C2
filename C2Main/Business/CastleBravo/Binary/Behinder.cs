using C2.Business.CastleBravo.Binary.Info;
using C2.Core;
using System;
using System.Text;

namespace C2.Business.CastleBravo.Binary
{
    class Behinder
    {
        public Behinder()
        {
            IteratorCount = 0;
            HitPassword = string.Empty;
        }
        public int IteratorCount { get; set; }
        public string HitPassword { get; set; }

        public event EventHandler<EventArgs> OnIteratorCount;

        public string Descrypt(string text)
        {
            if (text.IsNullOrEmpty())
                return string.Empty;

            IteratorCount = 0;
            HitPassword = string.Empty;

            // 加载字典
            // 尝试 XOR 解密
            // 尝试 AES 128解密            
            Password dict = Password.GetInstance();
            foreach (string p in dict.Pass)
            {
                IteratorCount++;

                if (IteratorCount % (1024 * 2) == 0)
                    OnIteratorCount?.Invoke(this, new EventArgs());

                string pass = ST.GenerateMD5(p).Substring(0, 16);

                string ret = XOR_Decrypt(text, pass);
                if (IsDecryptCorrect(ret))
                    return ret;

                ret = AES128_Decrypt(text, pass);
                if (IsDecryptCorrect(ret))
                    return ret;
            }

            OnIteratorCount?.Invoke(this, new EventArgs());
            return string.Empty;
        }



        private string XOR_Decrypt(string text, string pass)
        {
            StringBuilder sb = new StringBuilder(text.Length);
            text = ST.DecodeBase64(text);

            for (int i = 0; i < text.Length; i++)
            {
                sb.Append((char)(text[i] ^ pass[(i + 1) & 15]));
            }
            return sb.ToString();
        }

        private string AES128_Decrypt(string text, string pass)
        {
            return string.Empty;
        }

        private bool IsDecryptCorrect(string value)
        {
            return value.Contains("base64_decode");
        }
    }
}
