using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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

            Stream writer = null;
            try
            {
                writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
            }
            catch (Exception)
            {
                return "网络连接失败 ";
            }
            writer.Close();//关闭请求流
                           // String strValue = "";//strValue为http响应所返回的字符流
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
                return "网络连接中断";
            }
            //  Stream postData = Request.InputStream;
            StreamReader sRead = new StreamReader(s);
            string postContent = sRead.ReadToEnd();
            PhoneInfo rt = JsonConvert.DeserializeObject<PhoneInfo>(postContent);
            if (rt.city != string.Empty)
                return string.Format("{0}\t{1}\n",
                                        rt.phoneType,
                                        rt.city);
            return "查询失败";
            
        }
    }
}
