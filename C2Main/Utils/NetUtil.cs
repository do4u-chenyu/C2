using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using C2.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace C2.Utils
{
    class NetUtil
    {
        public static string GetHostAddresses(string url)
        {
            try 
            {   // 遇到host就是ip的,返回, Dns.GetHostEntry此时会报错
                string host = new Uri(FormatUrl(url)).Host.Trim();
                if (Regex.IsMatch(host, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$"))
                    return host;
                return Dns.GetHostEntry(host).AddressList[0].ToString();
            }
            catch 
            {
                return "0.0.0.0";
            }
        }

        public static string FormatUrl(string url)
        {
            string str = url.ToLower().TrimStart();
            if (str.StartsWith("http://") || str.StartsWith("https://"))
                return url;
            else
                return "http://" + url.TrimStart();
        }

        private static string IPCheck(string ip)
        {
            if (ip.IsNullOrEmpty())
                return "空地址";
            if (ip.StartsWith("127.0.0."))
                return "本机回环地址";
            if (ip.Contains("0.0.0.0"))
                return "空地址";
            if (ip.StartsWith("192.168."))
                return "内网IP";
            if (ip.StartsWith("10."))
                return "内网IP";
            if (ip.StartsWith("172.16."))  // 172.16 - 172.31都是,赶时间先凑合
                return "内网IP";
            if (ip.Contains("%"))
                return "暂不支持IPV6";
            return ip;
        }

        public static string IPQuery_WhoIs(string ip)
        {
            ip = ip.ToLower().Trim();
            if (ip != IPCheck(ip))
                return IPCheck(ip);

            string url = "http://whois.pconline.com.cn/jsFunction.jsp?callback=jsShow&ip=" + ip;
            string result = "";
            WebRequest wrt = null;
            WebResponse wrp = null;
            try
            {
                wrt = WebRequest.Create(url);
                wrt.Credentials = CredentialCache.DefaultCredentials;
                wrt.Timeout = 5000;

                wrp = wrt.GetResponse();
                StreamReader sr = new StreamReader(wrp.GetResponseStream(), Encoding.Default);
                string html = sr.ReadToEnd();
                string pattern = @"\{jsShow\('(?<country>.*?)',";
                Match match = Regex.Match(html, pattern);
                string country = match.Groups["country"].Value.Trim();
                result = country;
            }
            catch (Exception)
            {
            }
            finally
            {
                if (wrp != null)
                    wrp.Close();
                if (wrt != null)
                    wrt.Abort();
            }
            return result;

        }

        public static string IPQuery_TaoBao(string ip)
        {
            ip = ip.ToLower().Trim();
            if (ip != IPCheck(ip))
                return IPCheck(ip);

            string url = "http://ip.taobao.com/outGetIpInfo?ip=" + ip + "&accessKey=alibaba-inc";
            string result = "";
            WebRequest wrt = null;
            WebResponse wrp = null;
            try
            {
                wrt = WebRequest.Create(url);
                wrt.Credentials = CredentialCache.DefaultCredentials;
                wrt.Timeout = 5000;

                wrp = wrt.GetResponse();
                StreamReader sr = new StreamReader(wrp.GetResponseStream(), Encoding.UTF8);
                string html = sr.ReadToEnd();
                string pattern = "\"country\":\"(?<country>.*?)\"[\\s\\S]*?\"city\":\"(?<city>.*?)\"[\\s\\S]*?\"region\":\"(?<province>.*?)\"";
                Regex regex = new Regex(pattern, RegexOptions.None);
                Match match = regex.Match(html);
                string city = match.Groups["city"].Value;
                string province = match.Groups["province"].Value;
                string country = match.Groups["country"].Value;
                result = (city.Equals(province) && province.Equals(country)) ? city : (country + province + city);
            }
            catch (Exception)
            {
            }
            finally
            {
                if (wrp != null)
                    wrp.Close();
                if (wrt != null)
                    wrt.Abort();
            }
            return result;

        }

        public static string IPQuery_IpApi(string ip)
        {
            ip = ip.ToLower().Trim();
            if (ip != IPCheck(ip))
                return IPCheck(ip);

            string url = "http://ip-api.com/json/" + ip + "?lang=zh-CN";
            string result = "";
            WebRequest wrt = null;
            WebResponse wrp = null;
            try
            {
                wrt = WebRequest.Create(url);
                wrt.Credentials = CredentialCache.DefaultCredentials;
                wrt.Timeout = 5000;

                wrp = wrt.GetResponse();
                StreamReader sr = new StreamReader(wrp.GetResponseStream(), Encoding.UTF8);
                //获取到的是Json数据
                string html = sr.ReadToEnd();

                //Newtonsoft.Json读取数据
                JObject obj = JsonConvert.DeserializeObject<JObject>(html);
                string city = obj["city"].ToString();
                string province = obj["regionName"].ToString();
                string country = obj["country"].ToString();
                result = (city.Equals(province) && province.Equals(country)) ? city : (country + province + city);
            }
            catch (Exception)
            {
            }
            finally
            {
                if (wrp != null)
                    wrp.Close();
                if (wrt != null)
                    wrt.Abort();
            }
            return result;
        }
    }
}
