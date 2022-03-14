using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using C2.Utils;
using System.Threading;

namespace C2.IAOLab.BaseAddress
{
    public class RecordNumber
    {
        private static RecordNumber instance;
        private static int sleepInterval;
        string reverseAddress;
        string contentICP;
 
        public static RecordNumber GetInstance(int si = 2)
        {
            if (instance == null)
                instance = new RecordNumber();
            sleepInterval = si;
            return instance;
        }

        public String WebUrlLocate(string input)
        {
            if (input == "基站号" || input == "WiFiMac号" || input == "银行卡号" || input == "IP" || input == "备案号" || input == string.Empty)
                return null;
            reverseAddress = GetLocation(input.Trim());
            contentICP = Content(input.Trim());
            return string.Format("{0}\t{1}\t{2}\n", input, reverseAddress, contentICP);
        }

        public static string GetLocation(string url)
        {
            HttpClient client = new HttpClient();
            string address = string.Empty;

            string bdUrl = string.Format("https://api.vvhan.com/api/icp?url={0}", url);
            string result = client.GetStringAsync(bdUrl).Result;
            Thread.Sleep(sleepInterval);
            try 
            {
                var locationResult = (JObject)JsonConvert.DeserializeObject(result);
                if (locationResult.Property("message") != null && !url.Contains("网站域名")
                     || Convert.ToString(locationResult["info"]["icp"]) == string.Empty)
                {
                    address = "此域名未备案";
                }
                else if (locationResult.Property("info") != null)
                {
                    address = Convert.ToString(locationResult["info"]["icp"]);
                    address = OpUtil.StringBlank + address;
                }
            }
            catch(Exception ex)
            {
                address = ex.Message;
            }
            return address;
        }

        public static string Content(string url)
        {
            string htmlStr = string.Empty;

            try
            {
                WebRequest request =  WebRequest.Create(url.StartsWith("http://") ? url : "http://" + url);
                WebResponse response = request.GetResponse();
                Stream datastream = response.GetResponseStream();
                StreamReader reader = new StreamReader(datastream, Encoding.UTF8);
                htmlStr = Regex.Replace(reader.ReadToEnd(), @"\s", string.Empty);
                if (htmlStr.Contains("ICP备"))
                {
                    int cp = htmlStr.IndexOf("ICP备");
                    htmlStr = htmlStr.Substring(cp - 1, htmlStr.Length - cp - 1);
                    htmlStr = htmlStr.Split('<')[0].Replace("<", string.Empty).Replace(">", string.Empty);
                }
                else
                {
                    return "网页无备案号";
                }
                reader.Close();
                datastream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                htmlStr = ex.Message;
            }
            return htmlStr;
        }
    }
}
