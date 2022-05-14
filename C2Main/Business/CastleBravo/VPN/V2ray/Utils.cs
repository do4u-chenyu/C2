using C2.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;

namespace v2rayN
{
    // 这是一个为了跟v2ray源码移植兼容的临时类
    class Utils
    {
        /// <summary>
        /// 逗号分隔的字符串,转List<string>
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<string> String2List(string str)
        {
            try
            {
                str = str.Replace(Environment.NewLine, "");
                return new List<string>(str.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
            }
            catch
            {
                return new List<string>();
            }
        }

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
                    sampleClientText = reader.ReadToEnd();
            }
            catch { }
            return sampleClientText;
        }

        private static string sampleHttprequestText;
        public static string GetSampleHttprequestEmbedText()
        {
            if (!string.IsNullOrEmpty(sampleHttprequestText))
                return sampleHttprequestText;

            try
            {
                string ffp = Path.Combine(C2.Core.Global.VPNPath, "SampleHttprequest.txt");
                using (StreamReader reader = new StreamReader(ffp))
                    sampleHttprequestText = reader.ReadToEnd();
            }
            catch { }
            return sampleHttprequestText;
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
                return JsonConvert.DeserializeObject<T>(strJson);
            }
            catch
            {
                return JsonConvert.DeserializeObject<T>(string.Empty);
            }
        }

        public static string ToJson(Object obj)
        {
            string result = string.Empty;
            try
            {
                result = JsonConvert.SerializeObject(obj,
                                           Formatting.Indented,
                                           new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch
            {
            }
            return result;
        }
        // 为了和v2ray代码形式上兼容
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
