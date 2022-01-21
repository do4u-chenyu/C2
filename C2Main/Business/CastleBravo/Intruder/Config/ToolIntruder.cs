using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace C2.Business.CastleBravo.Intruder.Config
{
    class ToolIntruder
    {
        public string UpdateUrl(string weburl, bool delEnd)
        {
            if (!weburl.StartsWith("http://") && !weburl.StartsWith("https://"))
            {
                weburl = "http://" + weburl;
            }
            if (delEnd && weburl.EndsWith("/"))
            {
                weburl = weburl.Substring(0, weburl.Length - 1);
            }

            return weburl;
        }
        public string GetIP(string url)
        {
            try
            {

                Uri uri = new Uri(url);
                String host = uri.Host;
                bool isIP = Regex.IsMatch(url, @"[\d]{1,3}\.[\d]{1,3}\.[\d]{1,3}\.[\d]{1,3}");
                if (isIP)
                {
                    return host;
                }
                IPHostEntry hostinfo = Dns.GetHostEntry(host);
                IPAddress[] aryIP = hostinfo.AddressList;

                return aryIP[0].ToString();

            }
            catch { }
            return "";
        }
    }
}
