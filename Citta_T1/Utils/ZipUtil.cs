using C2.Core;
using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace C2.Utils
{
    public class ZipUtil
    {
        /// <summary>
        /// 压缩文件
        /// </summary>
        public static void CreateZip(string srcFilePath, string dstZipFilePath)
        {
            (new FastZip()).CreateZip(dstZipFilePath, srcFilePath, true, ".*\\.(?!md).*$");
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        public static void UnZipFile(string zipFilePath)
        {
            if (!File.Exists(zipFilePath))
            {
                HelpUtil.ShowMessageBox("未能找到: " + zipFilePath); ;
                return;
            }
            // 获取模型文件名称
            string modelTitle = string.Empty;
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipFilePath)))
            {

                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    string fileName = Path.GetFileName(theEntry.Name);
                    if (!string.IsNullOrEmpty(fileName) && fileName.EndsWith(".xml"))
                    {
                        modelTitle = Path.GetFileNameWithoutExtension(fileName);
                        break;
                    }
                }
            }
            string workPath = Path.Combine(Global.WorkspaceDirectory, Global.GetMainForm().UserName);
            string targetPath = Path.Combine(workPath, modelTitle);
            Directory.CreateDirectory(workPath);

            (new FastZip()).ExtractZip(zipFilePath, targetPath, "");
            Crc32 crc32 = new Crc32();
            crc32.Update(new byte[] { 0x00, 0x01, 0x11 });

        }


    }
}
