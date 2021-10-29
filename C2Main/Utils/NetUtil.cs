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
            return Dns.GetHostEntry(new Uri(FormatUrl(url)).Host).AddressList[0].ToString();
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

        public static string IPQuery_126Net(string ip)
        {
            ip = ip.ToLower().Trim();
            if (ip != IPCheck(ip))
                return IPCheck(ip);

            string url = "http://ip.ws.126.net/ipquery?ip=" + ip;
            string result = "";
            WebRequest wrt = null;
            WebResponse wrp = null;
            try
            {
                wrt = WebRequest.Create(url);
                wrt.Credentials = CredentialCache.DefaultCredentials;

                wrp = wrt.GetResponse();
                StreamReader sr = new StreamReader(wrp.GetResponseStream(), Encoding.Default);
                //获取到的数据格式：var lo="江苏省", lc="镇江市"; var localAddress={city:"镇江市", province:"江苏省"}
                string html = sr.ReadToEnd();
                string pattern = "{city:\"(?<city>.*?)\", province:\"(?<province>.*?)\"}";
                Regex regex = new Regex(pattern, RegexOptions.None);
                Match match = regex.Match(html);
                string city = match.Groups["city"].Value;
                string province = match.Groups["province"].Value;
                result = city.Equals(province) ? city : (province + city);
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
    }
}
