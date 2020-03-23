using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Citta_T1.Utils
{
    class OpUtil
    {
        /// <summary>
        /// MD5字符串加密
        /// </summary>
        /// <param name="txt"></param>
        /// <returns>加密后字符串</returns>
        public static string GenerateMD5(string txt)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(txt);
                //开始加密
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public static void PreLoadFile(string filePath, bool isUTF8, int maxRow = 100)
        {
            System.IO.StreamReader sr;
            StringBuilder sb = new StringBuilder(1024 * 16);
            if (isUTF8)
            {
                sr = File.OpenText(filePath);
            }
            else
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs, System.Text.Encoding.Default);
            }
            for (int row = 0; row < maxRow; row++)
                sb.AppendLine(sr.ReadLine());

            Program.DataPreviewDict[filePath] = sb.ToString();
        }
    }
}
