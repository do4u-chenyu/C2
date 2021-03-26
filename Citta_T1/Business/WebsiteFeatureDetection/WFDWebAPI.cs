using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.WebsiteFeatureDetection
{
    class WFDWebAPI
    {
        // 用户信息认证
        public string UserAuthentication(string userName, string otp)
        {
            string token = string.Empty;

            //TODO http请求，通过返回状态设置token，返回的token为空说明认证失败
            //string respStatus = "success";
            //if (respStatus == "success")
            //    token = "11111";
            return "111";
        }

        // 网站分类
        public string ClassifierUrls(List<string> urls, string token)
        {
            string id = string.Empty;

            return "ce38f-ac939-efb2e";
        }

        // 根据任务id返回任务结果
        public string GetTaskResultsById(string id, string token, string flag = "0")
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
}
