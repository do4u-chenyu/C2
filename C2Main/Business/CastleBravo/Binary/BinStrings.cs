using System.Collections.Generic;
using System.IO;
using System.Text;

namespace C2.Business.CastleBravo.Binary
{
    class BinStrings
    {
        private byte[] bytes;
        private readonly List<string> ls;
        private readonly StringBuilder sb;
        private readonly int min;  // 长度小于这个数的不要
        private readonly int max;  // 长度大于这个数的截断
        
        public BinStrings()
        {
            bytes = new byte[0];
            ls = new List<string>();
            sb = new StringBuilder();
            min = 4;
            max = 1024;
        }

        public string[] Strings(string ffp)
        {
            Reset();

            using (FileStream fs = new FileStream(ffp, FileMode.Open))
            using (BinaryReader br = new BinaryReader(fs))
            while(Read4M(br))
                Consume();

            
            return ls.ToArray();
        }

        private void Reset()
        {
            bytes = new byte[0];
            ls.Clear();
            sb.Clear();
        }

        private bool Read4M(BinaryReader br)
        {
            bytes = br.ReadBytes(1024 * 1024 * 4);
            return bytes.Length > 0;
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
                ConsumeOne(b);
        }

        private bool IsChar(byte b)
        {
            // 可见字符和 /r /n /t
            return (b >= 0x20 && b <= 0x7E) || (b >= 0x09 && b <= 0x13);
        }

        private bool IsChar(byte l, byte r)
        {
            return IsChar(l) && r == 0x00;
        }

        private void Consume16LE()
        {
            for (int i = 0; i < bytes.Length; i += 2)
            {
                byte l = bytes[i + 0];
                byte r = bytes[i + 1];
                ConsumeOne(l, r);
            }
        }

        private void Consume16BE()
        {
            for (int i = 0; i < bytes.Length; i += 2)
            {
                byte l = bytes[i + 1];
                byte r = bytes[i + 0];
                ConsumeOne(l, r);
            }
        }

        private void ConsumeOne(byte l, byte r = 0x00)
        {
            if (IsChar(l, r))
            {
                sb.Append(l);
                if (sb.Length < max)
                    return;
            }

            if (sb.Length > min)
                ls.Add(sb.ToString());

            sb.Clear();
        }
    }
}
