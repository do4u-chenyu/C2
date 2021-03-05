using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QQSpiderPlugin
{
    [Serializable]
    class Session
    {
        CookieContainer cookies;
        Dictionary<string, string> headers;
        string ldw;
        public CookieContainer Cookies
        {
            get { return cookies; }
        }
        public Dictionary<string, string> Headers
        {
            get { return headers; }
        }
        public string Ldw
        {
            get { return this.ldw; }
            set { ldw = value; }
        }
        public Session()
        {
            this.headers = new Dictionary<string, string>();
            this.cookies = new CookieContainer();
            this.ldw = String.Empty;
        }
        public Session(Dictionary<string, string> headers)
        {
            this.headers = headers;
            this.cookies = new CookieContainer();
        }
        public Session(Dictionary<string, string> headers, CookieContainer cookies)
        {
            this.headers = headers;
            this.cookies = cookies;
        }
        public bool IsEmpty()
        {
            return this.headers.Keys.Count == 0 || this.cookies.Count == 0 || String.IsNullOrEmpty(this.ldw);
        }
        public void UpdateHeaders(string key, string value)
        {
            if (headers.ContainsKey(key))
                headers[key] = value;
            headers.Add(key, value);
        }
        public void UpdateHeaders(Dictionary<string, string> newHeaders)
        {
            foreach (string key in newHeaders.Keys)
            {
                if (headers.ContainsKey(key))
                    headers[key] = newHeaders[key];
                else
                    headers.Add(key, newHeaders[key]);
            }
        }
        public HttpWebResponse Get(string url, int timeout = 20000)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.CookieContainer = cookies;
            req.Timeout = timeout;
            return (HttpWebResponse)req.GetResponse();
        }
        public Response Get(string url, Dictionary<string, string> dic, int timeout = 20000)
        {
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
            req.CookieContainer = cookies;
            //req.AllowAutoRedirect = allowDirecte;
            req.Timeout = timeout;
            return new Response((HttpWebResponse)req.GetResponse());
        }
        public Response Post(string url, Dictionary<string, string> postData, int timeout = 20000)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.CookieContainer = cookies;
            //req.AllowAutoRedirect = allowDirecte;
            req.Timeout = timeout;
            string content = FormUrlEncodedContent(postData);
            byte[] data = Encoding.UTF8.GetBytes(content);

            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = data.Length;

            using (var stream = req.GetRequestStream())
                stream.Write(data, 0, data.Length);

            return new Response((HttpWebResponse)req.GetResponse());
        }

        private string FormUrlEncodedContent(Dictionary<string, string> postData)
        {
            StringBuilder sb = new StringBuilder();
            foreach(string key in postData.Keys)
                sb.Append(key).Append("=").Append(postData[key]).Append("&");
            return sb.ToString().Trim('&');
        }
        public void Serialize(string filePath)
        {
            Util.WriteToDisk<Session>(filePath, this);
        }
        public void Deserialize(string filePath)
        {
            if (File.Exists(filePath))
            {
                Session newSession = Util.ReadFromDisk<Session>(filePath);
                if (newSession != null)
                {
                    this.cookies = newSession.Cookies;
                    this.headers = newSession.Headers;
                    this.ldw = newSession.ldw;
                }
            }       
        }
    }
    class Response
    {
        string text;
        byte[] content;
        HttpStatusCode statusCode;
        private HttpWebResponse response;
        public readonly static Response Empty = new Response();
        private static readonly HttpClient client = new HttpClient();
        public byte[] Content
        {
            get { return this.content; }
        }
        public string Text
        {
            get { return this.text; }
        }
        public HttpStatusCode StatusCode
        {
            get { return this.statusCode; }
        }
        public Response()
        {
            this.response = null;
        }
        public Response(HttpWebResponse resp)
        {
            this.response = resp;
            this.content = GetContent();
            this.text = System.Text.Encoding.UTF8.GetString(this.content);
            this.statusCode = GetStatusCode();
        }
        private HttpStatusCode GetStatusCode()
        {
            if (response == null)
                throw new Exception("没有实例化的Response");
            return response.StatusCode;
        }
        private byte[] GetContent()
        {
            if (response == null)
                throw new Exception("没有实例化的Response");
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    Stream responseStream = this.response.GetResponseStream();
                    byte[] buffer = new byte[64 * 1024];
                    int i;
                    while ((i = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, i);
                    }
                    return ms.ToArray();
                }
            }
            catch
            {
                return new byte[0];
            }
        }
        /// <summary>
        /// 异步调用
        /// [How do I set a cookie on HttpClient's HttpRequestMessage](https://stackoverflow.com/questions/12373738/how-do-i-set-a-cookie-on-httpclients-httprequestmessage)
        /// [Make Https call using HttpClient](https://stackoverflow.com/questions/22251689/make-https-call-using-httpclient)
        /// [.Net(C#)后台发送Get和Post请求的几种方法总结]https://www.cjavapy.com/article/50/
        /// </summary>
        async private void Test()
        {
            var responseString = await client.GetStringAsync("http://www.example.com/recepticle.aspx");
        }
    }
}
