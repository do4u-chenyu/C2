using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.WebsiteFeatureDetection
{
    class WFDApi
    {
        /// <summary>
        /// 用户信息认证
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="otp"></param>
        /// <returns></returns>
        public static string UserAuthentication(string userName, string otp)
        {
            string token = string.Empty;

            //TODO http请求，通过返回状态设置token，返回的token为空说明认证失败
            //string respStatus = "success";
            //if (respStatus == "success")
            //    token = "11111";
            return "111";
        }

        /// <summary>
        /// 网站分类
        /// </summary>
        /// <param name="urls"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string ClassifierUrls(List<string> urls, string token)
        {
            string id = string.Empty;

            return "ce38f-ac939-efb2e";
        }

        /// <summary>
        /// 根据任务id返回任务结果
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <param name="flag">是否输出全部结果</param>
        public static void GetTaskResultsById(string id, string token, string flag = "0")
        {

        }

        /// <summary>
        /// 根据任务id返回异常网站截图
        /// </summary>
        /// <param name="id"></param>
        public static void GetScreenshotsById(string id)
        {

        }

        /// <summary>
        /// 根据任务id返回出错的url列表
        /// </summary>
        /// <param name="id"></param>
        public static void GetFailUrlsById(string id)
        {

        }
    }
}
