using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace MD5Plugin
{
    class Utils
    {
        public static void UncompressFile(string fileName, byte[] content)
        {
            FileStream destinationFile = File.Create(fileName);

            // Because the uncompressed size of the file is unknown, 
            // we are using an arbitrary buffer size.
            byte[] buffer = new byte[4096];
            int n;

            using (GZipStream input = new GZipStream(new MemoryStream(content),
                CompressionMode.Decompress, false))
            {
                while (true)
                {
                    n = input.Read(buffer, 0, buffer.Length);
                    if (n == 0)
                    {
                        break;
                    }
                    destinationFile.Write(buffer, 0, n);
                }
            }
            destinationFile.Close();
        }

        public static string TryGetTempDir()
        {
            string tempDir = string.Empty;
            try
            {
                tempDir = Path.GetTempPath();
            }
            catch  { }

            if (string.IsNullOrEmpty(tempDir))
                return Path.Combine(@"C:\FiberHomeIAOModelDocument", "FiberHomeIAOTemp");
            return Path.Combine(tempDir, "FiberHomeIAOTemp");
            
        }

        public static byte[] HexStringToBytes(string str)
        {
            str = str.ToLower().Trim();
            str = str.StartsWith("0x") ? str.Substring(2) : str;

            byte[] arrByte = new byte[str.Length >> 1];
            str = str.Substring(0, arrByte.Length << 1);

            try
            {
                for (int i = 0; i < arrByte.Length; i++)
                    arrByte[i] = Convert.ToByte(str.Substring(i << 1, 2), 16);
            }
            catch
            {
                return new byte[0];
            }

            return arrByte;
        }

        public static string BytesToHexString(byte[] bytes, int length = -1, string prefix = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(prefix);

            if (length < 0)
                length = bytes.Length;
            else
                length = Math.Min(length, bytes.Length);

            for (int i = 0; i < length; i++)
            {
                sb.Append(string.Format("{0:X2}", bytes[i]));
            }
            return sb.ToString();
        }
    }
}
