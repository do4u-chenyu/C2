using System;
using System.IO;
using System.Net;
using C2.Business.IAOLab.PhoneLocation;
using C2.Core;
using Newtonsoft.Json;
using C2.Utils;

namespace C2.IAOLab.PhoneLocation
{
    public class PhoneLocation
    {
        private static PhoneLocation instance;
        public static PhoneLocation GetInstance()
        {
            if (instance == null)
                instance = new PhoneLocation();
            return instance;
        }
        public string GetPhoneLocation(string input) 
        {
            if (input == string.Empty)
                return null;
            
            string phoneNum = input.Trim(' ');
            if (!NetUtil.IsPhoneNum(phoneNum))
                return "请输入手机号\n";
            string url = Global.ServerUrl + "/Castle/phone?phone=" + phoneNum;
            string location = Post(url);
            return location;
        }

        private string Post(string url) 
        {
            //创建一个HTTP请求  
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //Post请求方式  
            request.Method = "POST";
            //内容类型
            request.ContentType = "application/x-www-form-urlencoded";

            Stream writer;
            try
            {
                writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
            }
            catch (Exception)
            {
                return "网络连接失败\n";
            }
            writer.Close();//关闭请求流
                           // String strValue = string.Empty;//strValue为http响应所返回的字符流
            HttpWebResponse response;
            try
            {
                //获得响应流
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
            }
            Stream s;
            try
            {
                s = response.GetResponseStream();
            }
            catch
            {
                return "网络连接中断\n";
            }
            StreamReader sRead = new StreamReader(s);
            string postContent = sRead.ReadToEnd();
            try
            {
                PhoneInfo rt = JsonConvert.DeserializeObject<PhoneInfo>(postContent);
                if (!string.IsNullOrEmpty(rt.city))
                    return string.Format("{0}\t{1}\n",
                                            rt.phoneType,
                                            rt.city);
            }
            catch { }
            

            return "无此手机号段归属地信息\n";
        }
    }
}
