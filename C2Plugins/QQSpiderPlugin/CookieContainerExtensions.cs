using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QQSpiderPlugin
{
    public static class CookieContainerExtensions
    {
        public static List<Cookie> List(this CookieContainer container)
        {
            var cookies = new List<Cookie>();

            var table = (Hashtable)container.GetType().InvokeMember("m_domainTable",
                                                                    BindingFlags.NonPublic |
                                                                    BindingFlags.GetField |
                                                                    BindingFlags.Instance,
                                                                    null,
                                                                    container,
                                                                    new object[] { });

            foreach (var key in table.Keys)
            {
                var domain = key as string;

                if (domain == null)
                    continue;

                if (domain.StartsWith("."))
                    domain = domain.Substring(1);

                var httpAddress = string.Format("http://{0}/", domain);
                var httpsAddress = string.Format("https://{0}/", domain);

                if (Uri.TryCreate(httpAddress, UriKind.RelativeOrAbsolute, out var httpUri))
                {
                    foreach (Cookie cookie in container.GetCookies(httpUri))
                    {
                        cookies.Add(cookie);
                    }
                }
                if (Uri.TryCreate(httpsAddress, UriKind.RelativeOrAbsolute, out var httpsUri))
                {
                    foreach (Cookie cookie in container.GetCookies(httpsUri))
                    {
                        cookies.Add(cookie);
                    }
                }
            }

            return cookies;
        }
        public static string GetCookieValue(this CookieContainer container, string name)
        {
            List<Cookie> cookies = container.List();
            foreach (Cookie c in cookies)
                if (c.Name == name)
                    return c.Value;
            return String.Empty;
        }
    }
}
