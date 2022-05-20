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
using System.Windows.Forms;

namespace C2.Log
{
    class Log
    {
        //日志：工号/功能模块/动作/时间/IP
        public void LogManualButton(string modelName, string type)
        {
            DateTime e = DateTime.Now;
            string startTime = e.ToString("yyyyMMddHHmmss");
            string userName = WFDWebAPI.GetInstance().UserName;
            string ip = IPGet();
            //if (IsInternetAvailable())
            //KibaRabbitMQSend(SToJson(userName, modelName, type, startTime, ip));//client
            //KibaRabbitMQReceived();//server
        }

        private void KibaRabbitMQSend(string sendMessage)
        {
            var factory = new ConnectionFactory(); 
            factory.HostName = "10.1.126.4";//主机名
            factory.UserName = "admin";//RabbitMQ自定义用户名
            factory.Password = "admin";//RabbitMQ自定义自定义密码

            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    /*//创建一个名称为AnTiQueue的消息队列
                     *  QueueDeclare 第二个参数中设置为false，queue发送的message只会存留于内存中，
                     *  server 重启，数据丢失，设置为true，永久存储在硬盘中
                     */
                    channel.QueueDeclare("AnTiQueue", false, true, false, null);
                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 1;
                    string message = sendMessage; //传递的消息内容
                    channel.BasicPublish("", "AnTiQueue", properties, Encoding.UTF8.GetBytes(message)); //生产消息
                }
            }
        }
        private void KibaRabbitMQReceived()
        {
            var factory = new ConnectionFactory();
            factory.HostName = "10.1.126.4";
            factory.UserName = "admin";
            factory.Password = "admin";

            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare("AnTiQueue", false, false, false, null);
                    /* 定义EventingBasicConsumer类型的对象，该对象有Received事件，该事件会在服务接收到数据时触发。
                     * 
                     */
                    var consumer = new EventingBasicConsumer(channel);//消费者 
                    channel.BasicConsume("AnTiQueue", true, consumer);//消费消息 autoAck参数为消费后是否删除
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray(); // 将内存区域的内容复制到一个新的数组中
                        var message = Encoding.UTF8.GetString(body);

                        /*
                        string newFileName = @"D:\pycharm\pythonProject\test\urbtix\0518\\a.txt";
                        FileStream fs = new FileStream(newFileName, FileMode.Append);
                        StreamWriter sw = new StreamWriter(fs, Encoding.Default);//转码
                        sw.Write(message);
                        sw.Close();
                        */
                    };
                }
            }
        }
        private void logUpload(string reciveMessage)
        {
            // token, username,modeltype,nowtime


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

