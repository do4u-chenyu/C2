using System;
using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip;

namespace C2Shell.Utils
{
    public class ZipUtil
    {
        public static string UnZipFile(string zipFilePath, string targetPath, string password = "")
        {
            FastZip fz = new FastZip()
            {
                Password = password
            };
            try
            {
                fz.ExtractZip(zipFilePath, targetPath, String.Empty);
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

            return string.Empty;
        }
    }
}
