using System.IO;
using System.IO.Compression;

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
    }
}
