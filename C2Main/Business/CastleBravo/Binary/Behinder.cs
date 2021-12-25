using C2.Business.CastleBravo.Binary.Info;
using C2.Core;
using System;
using System.Text;
using System.Windows.Forms;

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

            // Base64变成byte数组
            // 加载字典
            // 尝试 XOR 解密
            // 尝试 AES 128解密
            byte[] text_bytes = ST.DecodeBase64ToBytes(text);
            if (text_bytes.Length == 0)
                return string.Empty;

            
            Password dict = Password.GetInstance();
            foreach (string pass in dict.Pass)
            {
                IteratorCount++;

                if (IteratorCount % (1024 * 2) == 0)
                    OnIteratorCount?.Invoke(this, new EventArgs());

                byte[] pass_byte = Encoding.Default.GetBytes(pass);
                string ret = XOR_Decrypt(text_bytes, pass_byte);
                if (IsDecryptCorrect(ret))
                    return ret;

                ret = AES128_Decrypt(text_bytes, pass_byte);
                if (IsDecryptCorrect(ret))
                    return ret;
            }

            OnIteratorCount?.Invoke(this, new EventArgs());
            return string.Empty;
        }



        private string XOR_Decrypt(byte[] text_bytes, byte[] pass_bytes)
        {
            return string.Empty;
        }

        private string AES128_Decrypt(byte[] text_bytes, byte[] pass_bytes)
        {
            return string.Empty;
        }

        private bool IsDecryptCorrect(string value)
        {
            return value.Contains("base64_decode");
        }
    }
}
