using C2.Business.HTTP;
using System;
using System.Collections.Generic;
using System.Net;

namespace C2.Business.CastleBravo
{
    class CastleBravoAPI
    {
        private readonly string APIUrl;
        private readonly string SearchUrl;
        private readonly string ResultUrl;

        private static CastleBravoAPI CastleBravoAPIInstance;
        readonly HttpHandler httpHandler;

        public static CastleBravoAPI GetInstance()
        {
            if (CastleBravoAPIInstance == null)
            {
                CastleBravoAPIInstance = new CastleBravoAPI();
            }
            return CastleBravoAPIInstance;
        }

        public CastleBravoAPI()
        {
            APIUrl = "http://218.94.117.234:8484/Castle/";//正式

            SearchUrl = APIUrl + "search";
            ResultUrl = APIUrl + "result";
            httpHandler = new HttpHandler();
        }


        public bool StartTask(List<string> md5List, out CastleBravoAPIResponse cbaResp)
        {
            cbaResp = new CastleBravoAPIResponse();
            string pairs = "md5List=" + string.Join(",", md5List);
            try
            {
                Response resp = httpHandler.PostCode(SearchUrl, pairs);
                if (resp.StatusCode != HttpStatusCode.OK)
                    cbaResp.Message = string.Format("错误http状态：{0}。", resp.StatusCode.ToString());

                Dictionary<string, string> resDict = resp.ResDict;

                if (resDict.TryGetValue("status", out string status) && status == "Success")
                {
                    resDict.TryGetValue("taskid", out string taskId);
                    cbaResp.Data = taskId;
                    cbaResp.Message = status;
                }
                else
                    cbaResp.Message = "任务下发失败。";
            }
            catch (Exception ex)
            {
                cbaResp.Message = ex.Message;
            }
            return true;
        }


        public bool QueryTaskResultsById(string taskId, out CastleBravoAPIResponse result)
        {
            result = new CastleBravoAPIResponse();

            //目前默认输出全部分类结果，flag默认为1，后期有需求再改flag参数
            //Dictionary<string, string> pairs = new Dictionary<string, string> { { "taskId", taskId } };
            string pairs = "taskId=" + taskId;
            try
            {
                Response resp = httpHandler.PostCode(ResultUrl, pairs);
                if (resp.StatusCode != HttpStatusCode.OK)
                    result.Message = string.Format("http 状态码错误：{0}.", resp.StatusCode.ToString());

                Dictionary<string, string> resDict = resp.ResDict;

                if (resDict.TryGetValue("status", out string status))
                {
                    resDict.TryGetValue("content", out string datas);
                    result.Data = datas;
                    result.Message = status;
                }
                else
                    result.Message = "任务结果查询失败。";
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return true;
        }
    }
}
