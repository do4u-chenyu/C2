using C2.Business.WebsiteFeatureDetection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;

namespace C2.Log
{
    class Log
    {
        //日志：工号/功能模块/动作/时间/IP
        public void LogManualButton(string modelName, string type)
        {
            // new Log.Log().LogManualButton(modelName,"01");
            DateTime e = DateTime.Now;
            string startTime = e.ToString("yyyyMMddHHmmss");
            string userName = WFDWebAPI.GetInstance().UserName;
            string ip = IPGet();
            /*
            if(IsInternetAvailable())
                MessageBox.Show(SToJson(userName, modelName, type, startTime, ip));
            */
        }

        private string IPGet()
        {
            IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress[] addr = ipEntry.AddressList;
            return addr[1].ToString();
        }

        private string SToJson(string userName, string featureModel, string action, string time, string ip)
        {
            Dictionary<string, string> myDic = new Dictionary<string, string>
            {
                { "工号", userName },
                { "功能模块", featureModel },
                { "动作", action },
                { "时间", time },
                { "IP", ip }
            };
            string contentjson = JsonConvert.SerializeObject(myDic);
            return contentjson;
        }
        private bool IsInternetAvailable()
        {
            try
            {
                Dns.GetHostEntry("www.baidu.com"); 
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

