using C2.Utils;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;

namespace v2rayN
{
    // 这是一个为了跟v2ray源码移植兼容的临时类
    class Utils
    {
        public static bool IsNullOrEmpty(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return true;
            }
            if (text.Equals("null"))
            {
                return true;
            }
            return false;
        }

        private static string sampleClientText;
        public static string GetSampleClientEmbedText()
        {
            if (!string.IsNullOrEmpty(sampleClientText))
                return sampleClientText;

            try
            {
                string ffp = Path.Combine(C2.Core.Global.VPNPath, "SampleClientConfig.txt"); 
                using (StreamReader reader = new StreamReader(ffp))
                {
                    sampleClientText = reader.ReadToEnd();
                }
            }
            catch
            {
            }
            return sampleClientText;
        }
        
        public static int GetAvailablePort(int start = 10912)
        {
            var ps = IPGlobalProperties.
                GetIPGlobalProperties().
                GetActiveTcpListeners().
                Select(e => e.Port).
                Where(e => e > start + RandomUtil.RandomInt(1024, 1024 * 4)).
                OrderBy(e => e).
                ToList();

            var port = ps.Take(ps.Count - 1).
                          Where((e, i) => ps[i + 1] - e > 1).
                          FirstOrDefault();

            return port;
        }

        public static T FromJson<T>(string strJson)
        {
            try
            {
                T obj = JsonConvert.DeserializeObject<T>(strJson);
                return obj;
            }
            catch
            {
                return JsonConvert.DeserializeObject<T>("");
            }
        }

        public static void SaveLog(string strContent)
        {
            SaveLog("info", new Exception(strContent));
        }

        public static void SaveLog(string strTitle, Exception ex)
        {

        }

        public static string V2rayStartupPath()
        {
            return C2.Core.Global.VPNPath;
        }


    }
}
