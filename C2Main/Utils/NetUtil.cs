using System;
using System.Net;

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
    }
}
