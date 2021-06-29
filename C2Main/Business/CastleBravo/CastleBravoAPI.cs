using C2.Business.WebsiteFeatureDetection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.CastleBravo
{
    class CastleBravoAPI
    {
        private string APIUrl;
        string SearchUrl;
        string ResultUrl;

        private static CastleBravoAPI CastleBravoAPIInstance;

        HttpHandler httpHandler;

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


        public bool StartTask(List<string> md5List, out CastleBravoAPIResult result)
        {
            result = new CastleBravoAPIResult();
            //Dictionary<string, string> pairs = new Dictionary<string, string> { { "md5List", JsonConvert.SerializeObject(md5List) } };
            //Dictionary<string, string> pairs = new Dictionary<string, string> { { "md5List", string.Join(",",md5List) } };
            string pairs = "md5List=" + string.Join(",", md5List);
            try
            {
                Response resp = httpHandler.PostCode(SearchUrl, pairs);
                if (resp.StatusCode != HttpStatusCode.OK)
                    result.RespMsg = string.Format("错误http状态：{0}。", resp.StatusCode.ToString());

                Dictionary<string, string> resDict = resp.ResDict;

                if (resDict.TryGetValue("status", out string status) && status == "Success")
                {
                    resDict.TryGetValue("taskid", out string taskId);
                    result.Datas = taskId;
                    result.RespMsg = status;
                }
                else
                    result.RespMsg = "任务下发失败。";
            }
            catch (Exception ex)
            {
                result.RespMsg = ex.Message;
            }
            return true;
        }


        public bool QueryTaskResultsById(string taskId, out CastleBravoAPIResult result)
        {
            result = new CastleBravoAPIResult();

            //目前默认输出全部分类结果，flag默认为1，后期有需求再改flag参数
            //Dictionary<string, string> pairs = new Dictionary<string, string> { { "taskId", taskId } };
            string pairs = "taskId=" + taskId;
            try
            {
                Response resp = httpHandler.PostCode(ResultUrl, pairs);
                if (resp.StatusCode != HttpStatusCode.OK)
                    result.RespMsg = string.Format("错误http状态：{0}。", resp.StatusCode.ToString());

                Dictionary<string, string> resDict = resp.ResDict;

                if (resDict.TryGetValue("status", out string status))
                {
                    resDict.TryGetValue("content", out string datas);
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
    }
}
