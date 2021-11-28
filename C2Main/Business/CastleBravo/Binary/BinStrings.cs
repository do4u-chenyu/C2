using C2.Core;
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

        public string Strings(string ffp)
        {
            Reset();

            using (FileStream fs = new FileStream(ffp, FileMode.Open))
            using (BinaryReader br = new BinaryReader(fs))
            while(Read4M(br))
                Consume();

            // TODO 后续处理, 如 打标, 排序
            return ls.JoinString(System.Environment.NewLine);
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
            ConsumeAscii();
            Consume16LE();
            Consume16BE();
        }

        private void ConsumeAscii()
        {         
            foreach (byte b in bytes)
                TryConsumeOne(b);
        }

        private bool IsVisibleChar(byte l, byte r)
        {
            return l >= 0x20 && l <= 0x7E && r == 0x00;
        }

        private void Consume16LE()
        {
            for (int i = 0; i < bytes.Length; i += 2)
                TryConsumeOne(bytes[i + 0], bytes[i + 1]);
        }

        private void Consume16BE()
        {
            for (int i = 0; i < bytes.Length; i += 2)
                TryConsumeOne(bytes[i + 1], bytes[i + 0]);
        }

        private void TryConsumeOne(byte l, byte r = 0x00)
        {
            if (IsVisibleChar(l, r))
            {
                sb.Append((char)l);    // 前面判断肯定在可见字符集,这里大胆转
                if (sb.Length < 1024)  // 超出截断
                    return;
            }

            if (sb.Length > 4)             // 太小放弃
                UniqueAdd(sb.ToString());  // 局部去重

            sb.Clear();
        }
    }
}
