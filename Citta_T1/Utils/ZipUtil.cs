using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip;
using System;
using Citta_T1.Core;
using System.IO;
using System.Windows.Forms;

namespace Citta_T1.Utils
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
                MessageBox.Show("未能找到: " + zipFilePath); ;
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

        }


    }
}
