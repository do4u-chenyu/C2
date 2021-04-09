using C2.Dialogs.WebsiteFeatureDetection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

            //APIUrl = "https://10.1.203.15:12347/apis/";//测试
            APIUrl = "https://113.31.119.85:53374/apis/";//正式
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
        public bool StartTask(List<string> urls, out WFDAPIResult result)
        {
            result = new WFDAPIResult();

            if (!ReAuthBeforeQuery())
                return false;

            Dictionary<string, string> pairs = new Dictionary<string, string> { { "user_id", UserName }, { "urls", JsonConvert.SerializeObject(urls) } };
            try
            {
                Response resp = httpHandler.Post(ProClassifierUrl, pairs, Token);
                if (resp.StatusCode == HttpStatusCode.Unauthorized)
                    result.RespMsg = "TokenError";
                if (resp.StatusCode != HttpStatusCode.OK)
                    result.RespMsg = string.Format("错误http状态：{0}。", resp.StatusCode.ToString());

                Dictionary<string, string> resDict = resp.ResDict;

                if (resDict.TryGetValue("request_condition", out string status) && status == "success")
                {
                    resDict.TryGetValue("TASKID", out string datas);
                    result.Datas = datas;
                    result.RespMsg = status;
                }
                else if(resDict.TryGetValue("error", out string error))
                    result.RespMsg = error;
                else
                    result.RespMsg = "任务下发失败。";
            }
            catch (Exception ex)
            {
                result.RespMsg = ex.Message;
            }
            return true;
        }

        // 根据任务id返回任务结果
        public bool QueryTaskResultsById(string taskId, out WFDAPIResult result, string flag = "1")
        {
            result = new WFDAPIResult();

            if (!ReAuthBeforeQuery())
                return false;

            //目前默认输出全部分类结果，flag默认为1，后期有需求再改flag参数
            Dictionary<string, string> pairs = new Dictionary<string, string> { { "TASKID", taskId }, { "FLAG", flag } };
            try
            {
                Response resp = httpHandler.Post(TaskResultUrl, pairs, Token);
                if (resp.StatusCode == HttpStatusCode.Unauthorized)
                    result.RespMsg = "TokenError";
                if (resp.StatusCode != HttpStatusCode.OK)
                    result.RespMsg = string.Format("错误http状态：{0}。", resp.StatusCode.ToString());

                Dictionary<string, string> resDict = resp.ResDict;

                if (resDict.TryGetValue("operate_status", out string status))
                {
                    resDict.TryGetValue("data", out string datas);
                    result.Datas = datas;
                    result.RespMsg = status;
                }
                else
                    result.RespMsg = "任务结果查询失败。";
            }
            catch (Exception ex)
            {
                result.RespMsg = ex.Message;
            }
            return true;
        }

        // 根据任务id返回异常网站截图
        public async Task<WFDAPIResult> DownloadScreenshotById(string screenshotId)
        {
            WFDAPIResult result = new WFDAPIResult();

            Dictionary<string, string> pairs = new Dictionary<string, string> { { "screenshot_id", screenshotId } };
            try
            {
                Response resp = new Response();
                await Task.Run(() => {
                    resp = httpHandler.Post(ScreenshotUrl, pairs, Token);
                });

                if (resp.StatusCode == HttpStatusCode.Unauthorized)
                    result.RespMsg = "TokenError";
                if (resp.StatusCode != HttpStatusCode.OK)
                    result.RespMsg = string.Format("错误http状态：{0}。", resp.StatusCode.ToString());

                Dictionary<string, string> resDict = resp.ResDict;

                if (resDict.TryGetValue("operate_status", out string status))
                {
                    resDict.TryGetValue("data", out string datas);
                    result.Datas = datas;
                    result.RespMsg = status;
                }
                else
                    result.RespMsg = "获取网站截图失败。";
            }
            catch (Exception ex)
            {
                result.RespMsg = ex.Message;
            }
            return result;
        }

        // 根据任务id返回出错的url列表
        public void GetFailUrlsById(string id)
        {

        }

        public bool ReAuthBeforeQuery()
        {
            //后台尝试3次登陆，均认证失败后前台弹出认证窗口
            int maxRetryTime = 3;
            int time = 0;
            if (!string.IsNullOrEmpty(UserName))
            {
                while (time++ < maxRetryTime)
                {
                    if (UserAuthentication(UserName, TOTP.GetInstance().GetTotp(UserName)) == "success")
                        return true;
                    //存在验证时刚好口令跳60秒边界的情况，等待1秒再次申请
                    Thread.Sleep(1000);
                }
            }

            var UAdialog = new UserAuth();
            if (UAdialog.ShowDialog() != DialogResult.OK)
                return false;
            UserName = UAdialog.UserName;
            return true;
        }

    }
    
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
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(RemoteCertificateValidate);

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
                resp = new Response((HttpWebResponse)req.GetResponse());
            }
            catch(Exception ex)
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
            this.resDict = JsonToDictionary(this.content);
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
            catch{}
            finally
            {
                if (resStream != null)
                    resStream.Close();
                if (reader != null)
                    reader.Close();
            }
            return content;
        }
        private Dictionary<string, string> JsonToDictionary(string jsonStr)
        {
            if (string.IsNullOrEmpty(jsonStr))
                return new Dictionary<string, string>();

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonStr);
        }
    }
}
