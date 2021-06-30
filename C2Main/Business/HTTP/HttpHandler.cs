using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace C2.Business.HTTP
{
    class HttpHandler
    {
        public HttpHandler()
        {
        }
        private bool RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            //为了通过证书验证，总是返回true
            return true;
        }

        public Response Post(string url, Dictionary<string, string> postData, string token = "", int timeout = 10000)
        {
            Response resp = new Response();
            try
            {
                System.GC.Collect();//Http相关的资源没有正确释放引起操作超时
                ServicePointManager.DefaultConnectionLimit = 200;//系统支持同时存在http的connection个数过少引起操作超时

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(RemoteCertificateValidate);

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                req.Timeout = timeout;
                string content = DictionaryToJson(postData);
                byte[] data = Encoding.UTF8.GetBytes(content);

                req.Method = "POST";
                req.ContentType = "application/json";
                req.ContentLength = data.Length;
                req.Headers.Add("Authorization", "Bearer " + token);

                req.KeepAlive = true;//解决GetResponse操作超时问题

                using (var stream = req.GetRequestStream())
                    stream.Write(data, 0, data.Length);
                resp = new Response((HttpWebResponse)req.GetResponse());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resp;
        }

        public Response PostCode(string url, string postData, int timeout = 10000)
        {
            Response resp = new Response();
            try
            {
                System.GC.Collect();//Http相关的资源没有正确释放引起操作超时
                ServicePointManager.DefaultConnectionLimit = 200;//系统支持同时存在http的connection个数过少引起操作超时

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(RemoteCertificateValidate);

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                req.Timeout = timeout;
                byte[] data = Encoding.UTF8.GetBytes(postData);

                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = data.Length;

                req.KeepAlive = true;//解决GetResponse操作超时问题

                using (var stream = req.GetRequestStream())
                    stream.Write(data, 0, data.Length);
                resp = new Response((HttpWebResponse)req.GetResponse());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resp;
        }

        private string DictionaryToJson(Dictionary<string, string> dict)
        {
            if (dict.Count == 0)
                return String.Empty;

            return JsonConvert.SerializeObject(dict);
        }


    }
}
