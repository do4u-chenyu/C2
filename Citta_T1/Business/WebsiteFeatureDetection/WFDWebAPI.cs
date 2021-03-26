using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.WebsiteFeatureDetection
{
    class WFDWebAPI
    {
        //string UserName;
        string Token;
        string APIUrl;
        string LoginUrl;
        HttpHandler httpHandler;

        private static WFDWebAPI WFDWebAPIInstance;
        public static WFDWebAPI GetInstance()
        {
            if (WFDWebAPIInstance == null)
            {
                WFDWebAPIInstance = new WFDWebAPI();
            }
            return WFDWebAPIInstance;
        }

        public WFDWebAPI()
        {
            Token = string.Empty;
            APIUrl = "https://10.1.203.15:12449/apis/";//测试
            //APIUrl = "https://113.31.119.85:53374/apis/";//正式
            LoginUrl = APIUrl + "Login";

            httpHandler = new HttpHandler();
        }

        // 用户信息认证
        public string UserAuthentication(string userName, string otp)
        {
            //string status = string.Empty;

            //Dictionary<string, string> pairs = new Dictionary<string, string>{
            //    { "user_id"  , userName},
            //    { "password" , otp}
            //};
            //try
            //{
            //    Response resp = httpHandler.Post(LoginUrl, pairs);
            //    if (resp.StatusCode != HttpStatusCode.OK)
            //        return "error";
            //    Dictionary<string, string> resDict = resp.ResDict;
            //    if (resDict.ContainsKey("operate_status"))
            //        status = resDict["operate_status"];
            //    if (string.IsNullOrEmpty(status) || status == "fail")
            //        return "fail";
            //    else
            //    {
            //        Token = resDict["token"];
            //        return "success";
            //    }

            //}
            //catch (Exception ex)
            //{
            //    return ex.Message;
            //}

            //TODO http请求，通过返回状态设置token，返回的token为空说明认证失败
            //string respStatus = "success";
            //if (respStatus == "success")
            //    token = "11111";
            return "111";
        }

        // 网站分类
        public string ClassifierUrls(List<string> urls)
        {
            string id = string.Empty;

            return "ce38f-ac939-efb2e";
        }

        // 根据任务id返回任务结果
        public string GetTaskResultsById(string id, string flag = "0")
        {
            string resq = string.Empty;
            return resq;
        }

        // 根据任务id返回异常网站截图
        public void GetScreenshotsById(string id)
        {

        }

        // 根据任务id返回出错的url列表
        public void GetFailUrlsById(string id)
        {

        }

    }
    
    class HttpHandler
    {
        public HttpHandler()
        {
        }

        public Response Post(string url, Dictionary<string, string> postData, int timeout = 20000)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            //req.AllowAutoRedirect = allowDirecte;
            req.Timeout = timeout;
            string content = DictionaryToJson(postData);
            byte[] data = Encoding.UTF8.GetBytes(content);

            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = data.Length;

            using (var stream = req.GetRequestStream())
                stream.Write(data, 0, data.Length);

            return new Response((HttpWebResponse)req.GetResponse());
        }

        private string DictionaryToJson(Dictionary<string, string> dict)
        {
            if (dict.Count == 0)
                return "";

            return JsonConvert.SerializeObject(dict);
        }


    }

    class Response
    {
        Dictionary<string, string> resDict;
        byte[] content;
        HttpStatusCode statusCode;
        private HttpWebResponse response;
        public readonly static Response Empty = new Response();
        public byte[] Content
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
            this.resDict = JsonToDictionary(Encoding.UTF8.GetString(this.content));
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
        private Dictionary<string, string> JsonToDictionary(string jsonStr)
        {
            if (string.IsNullOrEmpty(jsonStr))
                return new Dictionary<string, string>();

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonStr);
        }
    }
}
