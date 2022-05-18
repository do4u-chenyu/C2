using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Xml;

namespace C2.Log
{
    class Log
    {
        private static string xmlDirectory = Path.Combine(@"C:\Users\xk\AppData\Local\Temp", "tmpRedisASK");
        private static string xmlPath = Path.Combine(xmlDirectory, "tmpRedisASK.xml");
        private static readonly XmlDocument xDoc = new XmlDocument();

        //日志：工号/功能模块/动作/时间/IP
        public void LogManualButton(string modelName,string type)
        {
            DateTime e = DateTime.Now;
            string startTime = e.ToString("yyyyMMddHHmmss");
            xDoc.Load(xmlPath);
            string userName = xDoc.SelectSingleNode(@"IdenInformation/userInfo/userName").InnerText;
            string ip = IPGet();

            if (IsInternetAvailable())
                MessageBox.Show(SToJson(userName, modelName, type, startTime, ip));
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
