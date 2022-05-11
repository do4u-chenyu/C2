using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
                GC.Collect();//Http相关的资源没有正确释放引起操作超时
                ServicePointManager.DefaultConnectionLimit = 200;//系统支持同时存在http的connection个数过少引起操作超时

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(RemoteCertificateValidate);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
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
        public string Get(string url, Dictionary<string, string> dic,int timeout = 10000)
        {
            string result = string.Empty;
            StringBuilder builder = new StringBuilder();
            builder.Append(url);
            if (dic.Count > 0)
            {
                builder.Append("?");
                int i = 0;
                foreach (var item in dic)
                {
                    if (i > 0)
                        builder.Append("&");
                    builder.AppendFormat("{0}={1}", item.Key, item.Value);
                    i++;
                }
            }
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(builder.ToString());
            req.Method = "GET";
            req.ContentType = "application/x-www-form-urlencoded";
            req.Timeout = timeout;
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            try
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            finally
            {
                stream.Close();
            }
            return result;
        }

            public Response PostCode(string url, string postData, int timeout = 10000,bool keepAlive = true)
        {
            Response resp = new Response();
            try
            {
                GC.Collect();//Http相关的资源没有正确释放引起操作超时
                ServicePointManager.DefaultConnectionLimit = 200;//系统支持同时存在http的connection个数过少引起操作超时

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(RemoteCertificateValidate);

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                req.Timeout = timeout;
                byte[] data = Encoding.UTF8.GetBytes(postData);

                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = data.Length;

                req.KeepAlive = keepAlive;//解决GetResponse操作超时问题

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

        public Response ObjDicPost(string url, Dictionary<string, object> postData, string token = "", int timeout = 10000)
        {
            Response resp = new Response();
            try
            {
                GC.Collect();//Http相关的资源没有正确释放引起操作超时
                ServicePointManager.DefaultConnectionLimit = 200;//系统支持同时存在http的connection个数过少引起操作超时

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(RemoteCertificateValidate);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                req.Timeout = timeout;
                string content = ObjectDicToJson(postData);
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

        private string DictionaryToJson(Dictionary<string, string> dict)
        {
            if (dict.Count == 0)
                return string.Empty;

            return JsonConvert.SerializeObject(dict);
        }
        
        private string ObjectDicToJson(Dictionary<string, object> dict)
        {
            if (dict.Count == 0)
                return string.Empty;

            return JsonConvert.SerializeObject(dict);
        }
    }
}
