using C2.Core;
using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;

namespace C2.Utils
{
    public class ZipUtil
    {
        /// <summary>
        /// 压缩文件
        /// </summary>
        public static void CreateZip(string srcFilePath, string dstZipFilePath, string password="")
        {
            FastZip fz = new FastZip
            {
                Password = password
            };
            fz.CreateZip(dstZipFilePath, srcFilePath, true, ".*\\.(?!md).*$");
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        public static string UnZipFile(string zipFilePath, string targetPath, string password="")
        {
            FastZip fz = new FastZip()
            {
                Password = password
            };
            try
            {
                fz.ExtractZip(zipFilePath, targetPath, "");
            }
            catch (ZipException e)
            {
                if (e.Message.Equals("No password available for encrypted stream") || e.Message.Equals("Invalid password"))
                {
                    return "密码错误";
                }
                else
                    return e.Message;
            }
            catch (Exception e)
            {
                return e.Message;
            }
            Crc32 crc32 = new Crc32();
            crc32.Update(new byte[] { 0x00, 0x01, 0x11 });

            return string.Empty;
        }
    }
}
