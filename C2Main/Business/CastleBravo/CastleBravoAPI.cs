using C2.Business.HTTP;
using C2.Core;
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
            APIUrl = Global.CastleIDLEUrl +  "/Castle/";//正式

            SearchUrl = APIUrl + "search";
            ResultUrl = APIUrl + "result";
            httpHandler = new HttpHandler();
        }


        public bool StartTask(List<string> md5List, out CastleBravoAPIResponse cbaResp)
        {
            cbaResp = new CastleBravoAPIResponse();
            List<string> transMd5List = new List<string>();
            foreach(string md5 in md5List)
                transMd5List.Add(ST.UrlEncode(md5));

            string pairs = "md5List=" + string.Join(",", transMd5List);
            try
            {
                Response resp = httpHandler.PostCode(SearchUrl, pairs);
                cbaResp.StatusCode = resp.StatusCode;
                if (cbaResp.StatusCode != HttpStatusCode.OK)
                    cbaResp.Message = string.Format("任务下发失败, Http状态码 ：{0}。", cbaResp.StatusCode.ToString());

                Dictionary<string, string> resDict = resp.ResDict;

                if (resDict.TryGetValue("status", out string status) && status == "Success")
                {
                    resDict.TryGetValue("taskid", out string taskId);
                    cbaResp.Data = taskId;
                    cbaResp.Message = String.Format("任务下发成功 [{0}]", taskId); 
                }
                else
                    cbaResp.Message = "网络OK，但向服务器下发任务失败";
            }
            catch (Exception ex)
            {
                cbaResp.Message = ex.Message;
            }
            return true;
        }


        public CastleBravoAPIResponse QueryTaskResultsById(string taskId)
        {
            CastleBravoAPIResponse result = new CastleBravoAPIResponse();

            string pairs = "taskId=" + taskId;
            try
            {
                Response resp = httpHandler.PostCode(ResultUrl, pairs);
                result.StatusCode = resp.StatusCode;

                if (result.StatusCode != HttpStatusCode.OK)
                    result.Message = string.Format("http 状态码：{0}.", result.StatusCode);

                Dictionary<string, string> resDict = resp.ResDict;

                if (resDict.TryGetValue("status", out string status))
                {
                    resDict.TryGetValue("content", out string datas);
                    result.Data = datas;
                    result.Message = status;
                }
                else
                    result.Message = "任务结果查询失败：返回数据格式不对,没有status字段";
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.Unused;  // 此处抛出异常时,不知道到底发生了什么
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
