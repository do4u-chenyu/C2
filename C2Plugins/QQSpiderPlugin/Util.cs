using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace QQSpiderPlugin
{
    static class Util
    {
        public static string GenQrToken(string qrSig)
        {
            int e = 0;
            for (int i = 0; i < qrSig.Length; i++)
                e += (e << 5) + Convert.ToInt32(qrSig[i]);
            int qrToken = (e & 2147483647);
            return qrToken.ToString();
        }
        public static string GenBkn(string skey)
        {
            int b = 5381;
            for (int i = 0; i < skey.Length; i++)
                b += (b << 5) + Convert.ToInt32(skey[i]);
            int bkn = (b & 2147483647);
            return bkn.ToString();
        }
        public static string GenRwWTS(string content)
        {
            string pattern = @"\[em\]e\d{4}\[/em\]|&nbsp;|<br>|[\r\n\t]";
            string result = Regex.Replace(content, pattern, "");
            return result.Replace("&amp;", "&").Trim();
        }
        /// <summary>
        /// 毫米级别的时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetTimeStamp()
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
        }
    }
}
