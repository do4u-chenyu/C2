using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
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

        public static void WriteToDisk<T>(string file, T obj)
        {
            using (Stream stream = File.Create(file))
            {
                try
                {
                    Console.Out.Write("Writing object to disk... ");
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, obj);
                    Console.Out.WriteLine("Done.");
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine("Problem writing object to disk: " + e.GetType());
                }
            }
        }
        public static T ReadFromDisk<T>(string file)
        {
            try
            {
                using (Stream stream = File.Open(file, FileMode.Open))
                {
                    Console.Out.Write("Reading object from disk... ");
                    BinaryFormatter formatter = new BinaryFormatter();
                    Console.Out.WriteLine("Done.");
                    return (T)formatter.Deserialize(stream);
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Problem reading cookies from disk: " + e.GetType());
                return default(T);
            }
        }
    }
}
