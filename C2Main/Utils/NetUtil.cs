using System;
using System.Collections.Generic;
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
        private static HashSet<string> china = new HashSet<string>(new string[] {
            "河北", "山西", "辽宁", "吉林", "黑龙", "江苏", "浙江", "安徽", "福建", "江西",
            "山东", "河南", "湖北", "湖南", "广东", "海南", "四川", "贵州", "云南", "陕西",
            "甘肃", "青海", "内蒙", "广西", "西藏", "宁夏", "新疆", "北京", "天津", "上海",
            "重庆", "华北", "华东", "华南", 
        });

        private static HashSet<string> other = new HashSet<string>(new string[] {
            "香港", "台湾", "澳门", "美国", "欧洲", "日本", "韩国", "越南", "英国", "伊朗",
            "荷兰", "德国", "法国", "澳大", "加拿", "新加", "菲律", "巴西", "亚太", "西班",
            "印度", "北美", "意大", "俄罗", "南非", "欧盟", "泰国", "罗马", "马来", "智利",
            "阿根", "埃及", "南美", "瑞士", "冰岛", "丹麦", "挪威", "瑞典", "芬兰", "缅甸",
            "柬埔", "约旦", "以色", "匈牙", "乌克", "古巴", "秘鲁", "梵蒂", "比利", "卢森",
            "波兰", "爱尔", "尼泊", "斯坦", "孟加", "土耳", "沙特", "摩纳", "利亚", "内亚",
            "比亚", "尼亚", "维亚", "苏丹", "立陶", "瑙鲁", "斐济", "哥斯", "拉圭", "海地",
            "群岛", "马里", "乍得", "中非", "北非", "东非", "西非", "东欧", "北欧", "西欧",
            "大洋", "澳洲", "美洲", "尼斯", "捷克", "卡塔", "科威", "也门", "阿曼", "黎巴",
            "巴林", "阿富", "伊拉", "葡萄", "朝鲜", 
        });

        public static bool IsChina(string countryDesc)
        {
            for(int i = 0; i < countryDesc.Length; i += 2)
            {
                string sub = countryDesc.Substring(i, Math.Min(countryDesc.Length - i, 2));
                
                if (other.Contains(sub))
                    return false;
                if (china.Contains(sub))
                    return true;    
            }

            if (countryDesc.Contains("中国"))
                return true;
            
            return false;
        }
        public static string GetHostAddresses(string url)
        {
            try 
            {   // 遇到host就是ip的,返回, Dns.GetHostEntry此时会报错
                string host = new Uri(FormatUrl(url)).Host.Trim();
                if (IsIPAddress(host))
                    return host;
                return Dns.GetHostEntry(host).AddressList[0].ToString();
            }
            catch 
            {
                return "0.0.0.0";
            }
        }

        public static bool IsIPAddress(string url)
        {
            return Regex.IsMatch(url, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        public static bool IsPhoneNum(string num) 
        {
            return Regex.IsMatch(num, @"^(1\d{10})$");
        }
        public static string FormatUrl(string url)
        {
            string str = url.ToLower().TrimStart();
            if (str.StartsWith("http://") || str.StartsWith("https://"))
                return url;
            else
                return "http://" + url.TrimStart();
        }

        public static string GetHostByUrl(string url)
        {
            Uri uri = new Uri(FormatUrl(url));
            return uri.Host;
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
            string result = string.Empty;
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
            catch { }
            finally
            {
                if (wrp != null)
                    wrp.Close();
                if (wrt != null)
                    wrt.Abort();
            }
            return result;

        }

        public static string IPQuery_ChunZhen(string ip)
        {
            ip = ip.ToLower().Trim();
            if (ip != IPCheck(ip))
                return IPCheck(ip);
            
            string url = Global.IpUrl + ip;
            string result = string.Empty;
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
                string pattern = "\"city\": \"(?<city>.*?)\"";
                Regex regex = new Regex(pattern, RegexOptions.None);
                Match match = regex.Match(html);
                string city = match.Groups["city"].Value;

                string pattern1 = "\"isp\": \"(?<isp>.*?)\"";
                Regex regex1 = new Regex(pattern1, RegexOptions.None);
                Match match1 = regex1.Match(html);
                string isp = match1.Groups["isp"].Value;
                result = city + "\t" + isp;
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
            string result = string.Empty;
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
