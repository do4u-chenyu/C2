using C2.Utils;
using System;
using System.Net;
using System.Net.Cache;
using System.Text;

namespace C2.Business.CastleBravo.WebShellTool
{
    public class WebClientEx : WebClient
    {
        public int Timeout { get; set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            request.Timeout = Timeout;
            return request;
        }

        private static WebClientEx Create(int timeout, ProxySetting setting)
        {
            WebClientEx one = new WebClientEx()
            {
                Timeout = timeout,
                Encoding = Encoding.Default,
                CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore),
            };

            if (setting != ProxySetting.Empty && setting.Enable)
            {
                one.Proxy = new WebProxy(setting.IP, setting.Port);
                one.Timeout += one.Timeout;  // 开代理的情况下速度慢, timeout时间放宽
            }
                
            one.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            one.Headers.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 11_2_3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.114 Safari/537.36");
            return one;
        }

        public static string Post(string url, string payload, int timeout, ProxySetting proxy) 
        {
            // 调用者处理异常
            byte[] bytes = Encoding.Default.GetBytes(payload);
            using (GuarderUtil.WaitCursor)
                // TODO: 测试时发现webclient必须每次new一个新的才行, 按道理不应该
                bytes = WebClientEx.Create(timeout, proxy)
                                   .UploadData(url, "POST", bytes);
             return Encoding.Default.GetString(bytes);   
        }

        public static byte[] PostDownload(string url, string payload, int timeout, ProxySetting proxy)
        {
            // 调用者处理异常
            byte[] bytes = Encoding.Default.GetBytes(payload);
            using (GuarderUtil.WaitCursor)
                bytes = WebClientEx.Create(timeout, proxy)
                                   .UploadData(url, "POST", bytes);
                return bytes;
        }
    }
}
