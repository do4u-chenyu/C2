using C2.Business.HTTP;
using C2.Business.WebsiteFeatureDetection;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace C2.Log
{
    partial class Log
    {
        public string Token;
        readonly DateTime e = DateTime.Now;
        string userName = string.Empty;
        LogItem logItem = new LogItem();
        HttpHandler httpHandler = new HttpHandler();
        static readonly string APIUrl = "https://113.31.119.85:53374/apis/";
        readonly string LoginUrl = APIUrl + "Login";
        private readonly string uploadUrl = "http://113.31.114.239:53373/api/log/upload";
        public static ConcurrentQueue<LogItem> ConcurrenLogs = new ConcurrentQueue<LogItem>();
        readonly string logPath = Path.Combine(Path.Combine(new DirectoryInfo(Global.TempDirectory).Parent.FullName, "tmpRedisASK"), "tmpRedisASK.xml");

        //日志：工号/功能模块/动作/时间/IP
        public void LogManualButton(string modelName, string type)
        {
#if !C2_Inner
            string startTime = e.ToString("yyyyMMddHHmmss");
            AddQueueEn(modelName, type, startTime, VersionGet());
            LogThread();
#endif
        }

        private string UserNameGet()
        {
            if (File.Exists(logPath))
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(logPath);
                userName = xDoc.SelectSingleNode(@"IdenInformation/userInfo/userName").InnerText;
                return userName;
            }
            return userName;
        }

        private string VersionGet()
        {
            string v1 = ConfigUtil.TryGetAppSettingsByKey("version", string.Empty);//内网|外网|全量版
            Version v2 = Assembly.LoadFrom(Application.ExecutablePath).GetName().Version;//读取Properties version
            return string.Format("{0}|{1}", v1, v2);
        }

        private void AddQueueEn(string modelName, string type, string startTime,string version)
        {
            logItem.ModelName = modelName;
            logItem.Type = type;
            logItem.StartTime = startTime;
            logItem.Version = version;
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
                { "userid",UserNameGet()},
                { "tasktypename", reciveMessage.ModelName.Replace(@"""",string.Empty)},
                { "submit_time", reciveMessage.StartTime.Replace(@"""",string.Empty)},
                { "action",reciveMessage.Type.Replace(@"""",string.Empty)},
                { "ip",GetPublicIp()},
                { "version",reciveMessage.Version.Replace("}",string.Empty).Replace(@"""",string.Empty)}
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
        private string GetPublicIp()
        {
            var urlList = new List<string>
            {
                "http://ip.qq.com/",
                "http://pv.sohu.com/cityjson?ie=utf-8",
                "http://ip.taobao.com/service/getIpInfo2.php?ip=myip"
            };
            string tempip = string.Empty;
            foreach (var a in urlList)
            {
                try
                {
                    var req = WebRequest.Create(a);
                    req.Timeout = 20000;
                    var response = req.GetResponse();
                    var resStream = response.GetResponseStream();
                    if (resStream != null)
                    {
                        var sr = new StreamReader(resStream, Encoding.UTF8);
                        var htmlinfo = sr.ReadToEnd();
                        //匹配IP的正则表达式
                        var r = new Regex("((25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]\\d|\\d)\\.){3}(25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]\\d|[1-9])", RegexOptions.None);
                        var mc = r.Match(htmlinfo);
                        //获取匹配到的IP
                        tempip = mc.Groups[0].Value;
                        resStream.Close();
                        sr.Close();
                        response.Dispose();
                    }
                    return tempip;
                }
                catch
                {
                    tempip = IPGet();
                }
            }
            return tempip;
        }
    }
    class LogItem
    {
        public string ModelName { get; set; }
        public string Type { get; set; }
        public string StartTime { get; set; }
        public string Version { get; set; }
    }
}

