using C2.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace C2.Business.HTTP
{
    class Response
    {
        Dictionary<string, string> resDict;
        string content;
        HttpStatusCode statusCode;
        private HttpWebResponse response;
        public readonly static Response Empty = new Response();
        public string Content
        {
            get { return this.content; }
        }
        public Dictionary<string, string> ResDict
        {
            get { return this.resDict; }
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
            this.resDict = JsonUtil.JsonToDictionary(this.content);
            this.statusCode = GetStatusCode();

        }
        private HttpStatusCode GetStatusCode()
        {
            if (response == null)
                throw new Exception("没有实例化的Response");
            return response.StatusCode;
        }
        private string GetContent()
        {
            if (response == null)
                throw new Exception("没有实例化的Response");

            Stream resStream = null;
            StreamReader reader = null;
            string content = string.Empty;
            try
            {
                using (resStream = response.GetResponseStream())
                {
                    using (reader = new StreamReader(resStream, Encoding.UTF8))
                    {
                        //通过ReadToEnd()把整个HTTP响应作为一个字符串取回，
                        content = reader.ReadToEnd().ToString();
                    }
                }
            }
            catch { }
            finally
            {
                if (resStream != null)
                    resStream.Close();
                if (reader != null)
                    reader.Close();
            }
            return content;
        }
    }
}
