using C2.Business.CastleBravo.Binary.Info;
using C2.Core;

namespace C2.Business.CastleBravo.Binary
{
    class Behinder
    {
        public Behinder()
        {
            
        }
        public int DecryptIteratorCount { get; set; } = 0;

        public string Descrypt(string text)
        {
            DecryptIteratorCount = 0;
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
                DecryptIteratorCount++;
                byte[] pass_byte = ST.DecodeBase64ToBytes(pass);
                string ret = XOR_Decrypt(text_bytes, pass_byte);
                if (IsDecryptCorrect(ret))
                    return ret;

                ret = AES128_Decrypt(text_bytes, pass_byte);
                if (IsDecryptCorrect(ret))
                    return ret;
            }

            dict.MissLoad(); // 爆破失败, 加载更多字典
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
            return true;
        }
    }
}
