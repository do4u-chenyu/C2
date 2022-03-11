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
        string reverseAddress;
        string contentICP;
        //string isExist;

        public static RecordNumber GetInstance()
        {
            if (instance == null)
                instance = new RecordNumber();
            return instance;
        }

        public String WebUrlLocate(string input)
        {
            if (input == "基站号" || input == "WiFiMac号" || input == "银行卡号" || input == "IP" || input == "备案号" || input == string.Empty)
                return null;
            //try
            //{
                reverseAddress = GetLocation(input.Trim());
                contentICP = content(input.Trim());
            //isExist = isContent(contentICP);
            //}
            //catch { }
            //return string.Format("{0}\t{1}\t{2}\t{3}\n", input, reverseAddress, contentICP, isExist);
            return string.Format("{0}\t{1}\t{2}\n", input, reverseAddress, contentICP);
        }

        public static string GetLocation(string url)
        {
            HttpClient client = new HttpClient();
            string address = string.Empty;

            string bdUrl = string.Format("https://api.vvhan.com/api/icp?url={0}", url);
            string result = client.GetStringAsync(bdUrl).Result;
            Thread.Sleep(2000);
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

        public static string content(string url)
        {
            string htmlStr = string.Empty;

            try
            {
                WebRequest request = url.Contains("http") ? WebRequest.Create(url) : WebRequest.Create("http://" + url);
                WebResponse response = request.GetResponse();
                Stream datastream = response.GetResponseStream();
                StreamReader reader = new StreamReader(datastream, Encoding.UTF8);
                htmlStr = Regex.Replace(reader.ReadToEnd(), @"\s", "");
                if (htmlStr.Contains("ICP备"))
                {
                    int cp = htmlStr.IndexOf("ICP备");
                    htmlStr = htmlStr.Substring(cp - 1, htmlStr.Length - cp - 1);
                    htmlStr = htmlStr.Split('<')[0].Replace("<", "").Replace(">", "");
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
        /*
        public static string isContent(string contentICP)
        {
            string htmlStr = string.Empty;
            string Url = string.Format("http://icp.chinaz.com/{0}", contentICP);
            WebRequest request = WebRequest.Create(Url);
            WebResponse response = request.GetResponse();
            Stream datastream = response.GetResponseStream();
            StreamReader reader = new StreamReader(datastream, Encoding.UTF8);
            htmlStr = reader.ReadToEnd();
            if (htmlStr.Contains("bor - t1s IcpMain01") && htmlStr.Contains("<span>网站备案/许可证号</span>"))
                return "真";
            else
                return "假";
        }
        */
    }
}
