using C2.Business.HTTP;
using C2.Business.WebsiteFeatureDetection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace C2.Log
{
    class Log
    {
        HttpHandler httpHandler = new HttpHandler();
        public string Token;
        private const string APIUrl = "https://113.31.119.85:53374/apis/";//正式
        private string LoginUrl = APIUrl + "Login";
        private string uploadUrl = "http://47.94.39.209:8000/api/log/upload";

        //日志：工号/功能模块/动作/时间/IP
        public void LogManualButton(string modelName, string type)
        {
            DateTime e = DateTime.Now;
            string startTime = e.ToString("yyyyMMddHHmmss");
            string userName = WFDWebAPI.GetInstance().UserName;
            string ip = IPGet();
            if (IsInternetAvailable())
            {
                KibaRabbitMQSend(SToJson(userName, modelName, type, startTime, ip));//client
                KibaRabbitMQReceived();//server
            }   
        }

        private void KibaRabbitMQSend(string sendMessage)
        {
            var factory = new ConnectionFactory
            {
                HostName = "10.1.126.4",//主机名
                UserName = "admin",//RabbitMQ自定义用户名
                Password = "admin"//RabbitMQ自定义自定义密码
            };

            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    /*//创建一个名称为AnTiQueue的消息队列
                     *  QueueDeclare 第二个参数中设置为false，queue发送的message只会存留于内存中，
                     *  server 重启，数据丢失，设置为true，永久存储在硬盘中
                     */
                    channel.QueueDeclare("AnTiQueue", false, false, false, null);
                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 1;
                    string message = sendMessage; //传递的消息内容
                    channel.BasicPublish("", "AnTiQueue", properties, Encoding.UTF8.GetBytes(message)); //生产消息
                }
            }
        }
        private void KibaRabbitMQReceived()
        {
            var factory = new ConnectionFactory
            {
                HostName = "10.1.126.4",
                UserName = "admin",
                Password = "admin"
            };

            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare("AnTiQueue", false, false, false, null);
                    var consumer = new EventingBasicConsumer(channel);
                    BasicGetResult result = channel.BasicGet("AnTiQueue", true);
                    if (result != null)
                    {
                        string data =Encoding.UTF8.GetString(result.Body);
                        LogUpload(data);
                    }
                }
            }
        }

        private void GetToken()
        {
            Dictionary<string, string> pairsL = new Dictionary<string, string> { { "user_id", "X7619" }, { "password", TOTP.GetInstance().GetTotp("X7619") } };
            Response resp = httpHandler.Post(LoginUrl, pairsL);
            Dictionary<string, string> resDict = resp.ResDict;
            resDict.TryGetValue("token", out Token);
        }
      
        private void LogUpload(string reciveMessage)
        {
            // {"工号":"X7619","功能模块":"战术手册-ddos模型","动作":"01","时间":"20220523100058","IP":"10.1.203.5"}
            string[] sArray = reciveMessage.Split(',');
            GetToken();
            Dictionary<string, string> pairs = new Dictionary<string, string> { 
                { "userid", sArray[0].Split(':')[1].Replace(@"""",string.Empty)}, 
                { "tasktypename", sArray[1].Split(':')[1].Replace(@"""",string.Empty)},
                { "submit_time", sArray[3].Split(':')[1].Replace(@"""",string.Empty)},
                { "action",sArray[2].Split(':')[1].Replace(@"""",string.Empty)},
                { "ip",sArray[4].Split(':')[1].Replace("}",string.Empty).Replace(@"""",string.Empty)} 
            };
            try
            {
                Response resp = httpHandler.Post(uploadUrl, pairs, Token);
                //Dictionary<string, string> resDict = resp.ResDict;
                //if (resDict.TryGetValue("status", out string status) && status == "success"){}
            }
            catch{}
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

