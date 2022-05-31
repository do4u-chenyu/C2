using C2.Business.HTTP;
using C2.Business.WebsiteFeatureDetection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Log
{
    partial class Log
    {
        HttpHandler httpHandler = new HttpHandler();
        public string Token;
        private const string APIUrl = "https://113.31.119.85:53374/apis/";
        private string LoginUrl = APIUrl + "Login";
        private string uploadUrl = "https://47.94.39.209:8000/api/log/upload";
        DateTime e = DateTime.Now;
        string userName = WFDWebAPI.GetInstance().UserName;
        static LogItem logItem = new LogItem();
        public static ConcurrentQueue<LogItem> ConcurrenLogs = new ConcurrentQueue<LogItem>();

        //日志：工号/功能模块/动作/时间/IP
        public void LogManualButton(string modelName, string type)
        {
            string startTime = e.ToString("yyyyMMddHHmmss");
            string ip = IPGet();

            /*
            Task t = Task.Factory.StartNew(() =>
            {
                AddQueueEn(userName, modelName, type, startTime, ip);
            });
            Task.WaitAll(t);
            LogThread();
            */
            
            MessageBox.Show(userName + modelName + type + startTime + ip);
        }

        private void AddQueueEn(string userName, string modelName, string type, string startTime, string ip)
        {
            logItem.UserName = userName;
            logItem.ModelName = modelName;
            logItem.Type = type;
            logItem.StartTime = startTime;
            logItem.Ip = ip;
            ConcurrenLogs.Enqueue(logItem);//入队
        }

        private void QueueDequeue()
        {
            if (ConcurrenLogs.Count > 0)
            {
                LogItem topElement = ConcurrenLogs.ElementAt(0);
                ConcurrenLogs.TryDequeue(out logItem);//出队
              
                try
                {
                    LogUpload(topElement);
                }
                catch { }
            }
        }

        public void LogThread()
        {
            ThreadStart childref = new ThreadStart(QueueDequeue);
            Thread childThread = new Thread(childref);
            childThread.Start();
        }

        private void GetToken()
        {
            Dictionary<string, string> pairsL = new Dictionary<string, string> 
            { 
                { "user_id", userName }, { "password", TOTP.GetInstance().GetTotp(userName)} 
            };
            Response resp = httpHandler.Post(LoginUrl, pairsL);
            Dictionary<string, string> resDict = resp.ResDict;
            resDict.TryGetValue("token", out Token);
        }

        private void LogUpload(LogItem reciveMessage)
        {
            GetToken();
            Dictionary<string, string> pairs = new Dictionary<string, string> {
                { "userid", reciveMessage.UserName.Replace(@"""",string.Empty)},
                { "tasktypename", reciveMessage.ModelName.Replace(@"""",string.Empty)},
                { "submit_time", reciveMessage.StartTime.Replace(@"""",string.Empty)},
                { "action",reciveMessage.Type.Replace(@"""",string.Empty)},
                { "ip",reciveMessage.Ip.Replace("}",string.Empty).Replace(@"""",string.Empty)}
            };
            try
            {
                Response resp = httpHandler.Post(uploadUrl, pairs, Token);
            }
            catch { }
        }

        private string IPGet()
        {
            string name = Dns.GetHostName();
            IPAddress[] ipadrlist = Dns.GetHostAddresses(name);
            foreach (IPAddress ipa in ipadrlist)
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                    return ipa.ToString();
            }
            return string.Empty;
        }
    }
    class LogItem
    {
        public string UserName { get; set; }
        public string ModelName { get; set; }
        public string Type { get; set; }
        public string StartTime { get; set; }
        public string Ip { get; set; }
    }
}

