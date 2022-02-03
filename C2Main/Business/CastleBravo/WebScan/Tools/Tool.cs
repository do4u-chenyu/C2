using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace C2.Business.CastleBravo.WebScan.Tools
{
    class Tool
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
            return string.Empty;
        }

        public string GetFileSize(string sFullName)
        {
            long FactSize = 0;
            if (File.Exists(sFullName))
                FactSize = new FileInfo(sFullName).Length;

            string m_strSize = string.Empty;

            if (FactSize < 1024.00)
                m_strSize = FactSize.ToString("F1");
            else if (FactSize >= 1024.00 && FactSize < 1048576)
                m_strSize = (FactSize / 1024.00).ToString("F1") + " K";
            else if (FactSize >= 1048576 && FactSize < 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00).ToString("F1") + " M";
            else if (FactSize >= 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00 / 1024.00).ToString("F1") + " G";
            return m_strSize;
        }

    }
}
