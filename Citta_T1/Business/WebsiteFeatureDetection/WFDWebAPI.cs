using C2.Dialogs.WebsiteFeatureDetection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace C2.Business.WebsiteFeatureDetection
{

    class WFDWebAPI
    {
        public string UserName { set; get; }
        string Token;
        string APIUrl;
        string LoginUrl;
        string ProClassifierUrl;
        string TaskResultUrl;
        string ScreenshotUrl;
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
            UserName = string.Empty;
            Token = string.Empty;

            APIUrl = "https://10.1.203.15:12449/apis/";//测试
            //APIUrl = "https://113.31.119.85:53374/apis/";//正式
            LoginUrl = APIUrl + "Login";
            ProClassifierUrl = APIUrl + "pro_classifier_api";
            TaskResultUrl = APIUrl + "detection/task/result";
            ScreenshotUrl = APIUrl + "Screenshot";

            httpHandler = new HttpHandler();
        }

        // 用户信息认证
        public string UserAuthentication(string userName, string otp)
        {
            Dictionary<string, string> pairs = new Dictionary<string, string>{ { "user_id", userName }, { "password", otp } };
            try
            {
                Response resp = httpHandler.Post(LoginUrl, pairs);
                if (resp.StatusCode != HttpStatusCode.OK)
                    return string.Format("错误http状态：{0}。", resp.StatusCode.ToString());

                Dictionary<string, string> resDict = resp.ResDict;

                if (resDict.TryGetValue("operate_status", out string status) && status == "success")
                {
                    UserName = userName;
                    resDict.TryGetValue("token", out Token);
                    return "success";
                }
                else
                    return "用户认证失败(用户名或动态口令错误)。";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // 网站分类
        public bool StartTask(List<string> urls, out string respMsg, out string taskId)
        {
            respMsg = string.Empty;
            taskId = string.Empty;

            if (!ReAuthBeforeQuery())
                return false;

            Dictionary<string, string> pairs = new Dictionary<string, string> { { "user_id", UserName }, { "urls", JsonConvert.SerializeObject(urls) } };
            try
            {
                Response resp = httpHandler.Post(ProClassifierUrl, pairs, Token);
                if (resp.StatusCode == HttpStatusCode.Unauthorized)
                    respMsg = "TokenError";
                if (resp.StatusCode != HttpStatusCode.OK)
                    respMsg = string.Format("错误http状态：{0}。", resp.StatusCode.ToString());

                Dictionary<string, string> resDict = resp.ResDict;

                if (resDict.TryGetValue("request_condition", out string status) && status == "success")
                {
                    resDict.TryGetValue("TASKID", out taskId);
                    respMsg = status;
                }
                else if(resDict.TryGetValue("error", out string error))
                    respMsg = error;
                else
                    respMsg = "任务下发失败。";
            }
            catch (Exception ex)
            {
                respMsg = ex.Message;
            }
            return true;
        }

        // 根据任务id返回任务结果
        public bool QueryTaskResultsById(string taskId, out string respMsg, out string datas, string flag = "1")
        {
            datas = string.Empty;
            respMsg = string.Empty;

            if (!ReAuthBeforeQuery())
                return false;

            //目前默认输出全部分类结果，flag默认为1，后期有需求再改flag参数
            Dictionary<string, string> pairs = new Dictionary<string, string> { { "TASKID", taskId }, { "FLAG", flag } };
            try
            {
                Response resp = httpHandler.Post(TaskResultUrl, pairs, Token);
                if (resp.StatusCode == HttpStatusCode.Unauthorized)
                    respMsg = "TokenError";
                if (resp.StatusCode != HttpStatusCode.OK)
                    respMsg = string.Format("错误http状态：{0}。", resp.StatusCode.ToString());

                Dictionary<string, string> resDict = resp.ResDict;

                if (resDict.TryGetValue("operate_status", out string status))
                {
                    resDict.TryGetValue("data", out datas);
                    respMsg = status;
                }
                else
                    respMsg = "任务结果查询失败。";
            }
            catch (Exception ex)
            {
                respMsg = ex.Message;
            }
            return true;
        }

        // 根据任务id返回异常网站截图
        public void DownloadScreenshotById(string screenshotId, out string respMsg, out string datas)
        {
            datas = string.Empty;
            Dictionary<string, string> pairs = new Dictionary<string, string> { { "screenshot_id", screenshotId } };
            try
            {
                Response resp = httpHandler.Post(ScreenshotUrl, pairs, Token);
                if (resp.StatusCode == HttpStatusCode.Unauthorized)
                    respMsg = "TokenError";
                if (resp.StatusCode != HttpStatusCode.OK)
                    respMsg = string.Format("错误http状态：{0}。", resp.StatusCode.ToString());

                Dictionary<string, string> resDict = resp.ResDict;

                if (resDict.TryGetValue("operate_status", out string status))
                {
                    resDict.TryGetValue("data", out datas);
                    respMsg = status;
                }
                else
                    respMsg = "获取网站截图失败。";
            }
            catch (Exception ex)
            {
                respMsg = ex.Message;
            }
        }

        // 根据任务id返回出错的url列表
        public void GetFailUrlsById(string id)
        {

        }

        private bool ReAuthBeforeQuery()
        {
            //后台尝试登陆，失败后前台弹出认证窗口
            if (string.IsNullOrEmpty(UserName) || UserAuthentication(UserName, TOTP.GetInstance().GetTotp(UserName)) != "success")
            {
                var UAdialog = new UserAuth();
                if (UAdialog.ShowDialog() != DialogResult.OK)
                    return false;
                UserName = UAdialog.UserName;
            }

            return true;
        }

    }
    
    class HttpHandler
    {
        public HttpHandler()
        {
        }

        public Response Post(string url, Dictionary<string, string> postData, string token = "", int timeout = 20000)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            req.Timeout = timeout;
            string content = DictionaryToJson(postData);
            byte[] data = Encoding.UTF8.GetBytes(content);

            req.Method = "POST";
            req.ContentType = "application/json";
            req.ContentLength = data.Length;
            req.Headers.Add("Authorization", "Bearer " + token);

            using (var stream = req.GetRequestStream())
                stream.Write(data, 0, data.Length);

            return new Response((HttpWebResponse)req.GetResponse());
        }

        private string DictionaryToJson(Dictionary<string, string> dict)
        {
            if (dict.Count == 0)
                return String.Empty;

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
