using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace C2.Business.CastleBravo.Binary
{
    class BinStrings
    {
        private readonly List<string> ls;
        private readonly StringBuilder sb;

        private byte[] bytes;
        private string uniqueStr;
        
        public BinStrings()
        {
            ls = new List<string>();
            sb = new StringBuilder();
        }

        public string[] Strings(string ffp)
        {
            Reset();

            try
            {
                using (FileStream fs = new FileStream(ffp, FileMode.Open))
                using (BinaryReader br = new BinaryReader(fs))
                    while (Read4M(br))
                        Consume();
            }
            catch (Exception e)
            {
                ls.Add(e.Message);
            }

            return ls.ToArray();
        }

        private void Reset()
        {
            uniqueStr = string.Empty;
            bytes = new byte[0];
            ls.Clear();
            sb.Clear();
        }

        private bool Read4M(BinaryReader br)
        {
            bytes = br.ReadBytes(1024 * 1024 * 4);
            return bytes.Length > 0;
        }

        private void UniqueAdd(string str)
        { 
            if (str != uniqueStr) 
                ls.Add(str);
    
            uniqueStr = str;
        }

        private void Consume()
        {
            ConsumeAscii();  // 单字节
            Consume16LE();   // 双字节小端
            Consume16BE();   // 双字节大端
            Consume32LE();   // 四字节小端  单纯是为了与strings命令一致
            Consume32BE();   // 四字节大端  X86下的win和linux的二进制文件我感觉应该是没有
            // ConsumeZHCN   // 不知道怎么实现
            //ConsumeAscii7Bits();  // 没啥用, 7Bits字符串, 纯属拍脑袋
        }

        private void Consume32LE()
        {
            for (int i = 0; i < (bytes.Length >> 2 << 2); i += 4)  // 模4对齐
                TryConsumeQuad(bytes[i + 0], bytes[i + 1], bytes[i + 2], bytes[i + 3]);
        }

        private void Consume32BE()
        {
            for (int i = 0; i < (bytes.Length >> 2 << 2); i += 4)  // 模4对齐
                TryConsumeQuad(bytes[i + 3], bytes[i + 2], bytes[i + 1], bytes[i + 0]);
        }

        private void ConsumeAscii7Bits()
        {
            foreach (byte b in bytes)
                TryConsumeSingle(b > 0x7F ? (byte)(b & 0x7F) : (byte)0);  // 去掉最高位
        }
        private void ConsumeAscii()
        {         
            foreach (byte b in bytes)
                TryConsumeSingle(b);
        }

        private bool IsVisibleChar(byte o, byte s = 0x00, byte t = 0x00, byte q = 0x00)
        {
            return o >= 0x20 && o <= 0x7E && s == 0x00 && t == 0x00 && q == 0x00;
        }

        private void Consume16LE()
        {
            for (int i = 0; i < (bytes.Length >> 1 << 1); i += 2)  // 模2对齐
                TryConsumeDouble(bytes[i + 0], bytes[i + 1]);
        }

        private void Consume16BE()
        {
            for (int i = 0; i < (bytes.Length >> 1 << 1); i += 2)  // 模2对齐
                TryConsumeDouble(bytes[i + 1], bytes[i + 0]);
        }

        private void TryConsumeDouble(byte l, byte r)
        {
            TryConsumeQuad(l, r, (byte)0x00, (byte)0x00);
        }

        private void TryConsumeSingle(byte l)
        {
            TryConsumeDouble(l, (byte)0x00);
        }

        private void TryConsumeQuad(byte o, byte s, byte t, byte q)
        {
            if (IsVisibleChar(o, s, t, q))
            {
                sb.Append((char)o);    // 前面判断肯定在可见字符集,这里大胆转
                if (sb.Length < 1024)  // 超出截断
                    return;
            }

            if (sb.Length > 4)             // 太小放弃
                UniqueAdd(sb.ToString());  // 局部去重

            sb.Clear();
        }
    }
}
