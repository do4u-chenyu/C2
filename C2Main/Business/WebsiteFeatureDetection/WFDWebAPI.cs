using C2.Business.HTTP;
using C2.Dialogs.WebsiteFeatureDetection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
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
        string CategoryUrl;

        Dictionary<string, string> PredictionCodeDict;
        Dictionary<string, string> FraudCodeDict;

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
            PredictionCodeDict = new Dictionary<string, string>();
            FraudCodeDict = new Dictionary<string, string>();

            //APIUrl = "https://113.31.114.239:53371/apis/";//测试    
            APIUrl = "https://113.31.119.85:53374/apis/";//正式
            LoginUrl = APIUrl + "Login";
            ProClassifierUrl = APIUrl + "pro_classifier_api";//任务下发
            TaskResultUrl = APIUrl + "detection/task/result";//获取任务结果
            ScreenshotUrl = APIUrl + "Screenshot";//获取网站截图的base64值
            CategoryUrl = APIUrl + "category";//获取类别编码字典

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
                    return "认证失败(工号没开权限或动态口令错误, 先检查下电脑时间不要有偏差)。";
            }
            catch (Exception ex)
            {
                return "用户认证：" + ex.Message;
            }
        }

        // 任务下发  网站分类
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
                else if(resDict.TryGetValue("desc", out string error))
                    result.RespMsg = error;
                else
                    result.RespMsg = "任务下发失败。";
            }
            catch (Exception ex)
            {
                result.RespMsg = "任务下发：" + ex.Message;
            }
            return true;
        }

        public bool UpdateCategoryDict(out Dictionary<string, string> predictionCodeDict, out Dictionary<string, string> fraudCodeDict)
        {
            predictionCodeDict = PredictionCodeDict;
            fraudCodeDict = FraudCodeDict;

            if (predictionCodeDict.Count > 0 && fraudCodeDict.Count > 0)//上次查询有值，后面都不需要查了
                return true;

            if (!ReAuthBeforeQuery())
                return false;

            Dictionary<string, string> pairs = new Dictionary<string, string>();
            try
            {
                Response resp = httpHandler.Post(CategoryUrl, pairs, Token);
                if (resp.StatusCode != HttpStatusCode.OK)
                    return false;

                Dictionary<string, string> resDict = resp.ResDict;

                if (resDict.TryGetValue("operate_status", out string status) && status == "success")
                {
                    resDict.TryGetValue("data", out string datas);
                    GenCodeDict(datas);
                    predictionCodeDict = PredictionCodeDict;
                    fraudCodeDict = FraudCodeDict;
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        private bool GenCodeDict(string datas)
        {
            PredictionCodeDict.Clear();
            FraudCodeDict.Clear();

            JArray json = JArray.Parse(datas);
            foreach (JObject item in json)
            {
                Dictionary<string,string> tmpDict = item.ToObject<Dictionary<string, string>>();
                if(tmpDict.TryGetValue("lable", out string lable))
                {
                    PredictionCodeDict.Add(lable, tmpDict.TryGetValue("chinese_lable", out string chinese_lable) ? chinese_lable : string.Empty);
                    FraudCodeDict.Add(lable, tmpDict.TryGetValue("fraud", out string fraud) ? fraud : string.Empty);
                }
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
                    result.RespMsg = "Token expired, please login";
                if (resp.StatusCode != HttpStatusCode.OK)
                    result.RespMsg = string.Format("错误http状态：{0}。", resp.StatusCode.ToString());

                Dictionary<string, string> resDict = resp.ResDict;

                if (resDict.TryGetValue("operate_status", out string status))
                {
                    resDict.TryGetValue("data", out string datas);
                    resDict.TryGetValue("taskinfo", out string taskInfo);
                    result.Datas = datas;
                    result.RespMsg = status;
                    result.TaskInfo = taskInfo;
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
                result.RespMsg = "获取网站截图失败：" + ex.Message;
            }
            return result;
        }

        // 根据任务id返回出错的url列表
        public void GetFailUrlsById(string id)
        {

        }

        public bool ReAuthBeforeQuery(bool flag = false)
        {
            //后台尝试3次登陆，均认证失败后前台弹出认证窗口#
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
            if (flag)
            {
                UAdialog.StartPosition = FormStartPosition.CenterScreen;
                UAdialog.Text = "C2-用户认证";
            }
            if (UAdialog.ShowDialog() != DialogResult.OK)
                return false;
            UserName = UAdialog.UserName;
            return true;
        }
    }
}
