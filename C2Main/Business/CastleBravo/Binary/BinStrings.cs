using System.Collections.Generic;
using System.IO;
using System.Text;

namespace C2.Business.CastleBravo.Binary
{
    class BinStrings
    {
        private byte[] bytes;
        private List<string> ls;
        private StringBuilder sb;
        private int min;  // 长度小于这个数的不要
        private int max;  // 长度大于这个数的截断
        
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
            {
                bool isChar = IsChar(b);
                if (isChar)
                {
                    sb.Append(b);
                    if (sb.Length < max)
                        continue;
                }
                        
                if (sb.Length > min)
                    ls.Add(sb.ToString());

                sb.Clear();
            }
        }

        private bool IsChar(byte b)
        {
            // 可见字符和 /r /n /t
            return (b >= 0x20 && b <= 0x7E) || (b >= 0x09 && b <= 0x13);
        }

        private void Consume16LE()
        {
            for (int i = 0; i < bytes.Length; i += 2)
            {

            }
        }

        private void Consume16BE()
        {
            for (int i = 0; i < bytes.Length; i += 2)
            {

            }
        }
    }
}
