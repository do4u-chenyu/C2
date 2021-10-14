using C2.Core;
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

        public static WebClientEx Create(int timeout = Global.WebClientDefaultTimeout)
        {
            WebClientEx one = new WebClientEx()
            {
                Timeout = timeout,              
                Encoding = Encoding.Default,
                CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
            };

            one.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            return one;
        }

        public static string Post(string url, string payload, int timeout = Global.WebClientDefaultTimeout) // 默认30秒
        {
            // 调用者处理异常
            byte[] bytes = Encoding.Default.GetBytes(payload);
            using (GuarderUtil.WaitCursor)
                // TODO: 测试时发现webclient必须每次new一个新的才行, 按道理不应该
                bytes = WebClientEx.Create(timeout)
                                    .UploadData(url, "POST", bytes);

            return Encoding.Default.GetString(bytes);
        }
    }
}
