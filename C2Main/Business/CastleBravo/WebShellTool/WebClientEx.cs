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

        public static WebClientEx Create()
        {
            WebClientEx one = new WebClientEx()
            {
                Timeout = 30000,              // 30秒
                Encoding = Encoding.Default,
                CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
            };

            one.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            return one;
        }

        public static string Post(string url, string payload)
        {
            try
            {
                byte[] bytes = Encoding.Default.GetBytes(payload);
                using (GuarderUtil.WaitCursor)
                    // TODO: 测试时发现webclient必须每次new一个新的才行, 按道理不应该
                    bytes = WebClientEx.Create()
                                       .UploadData(url, "POST", bytes);

                return Encoding.Default.GetString(bytes);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
