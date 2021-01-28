using System;
using System.IO;
using System.Net;

namespace C2.IAOLab.WifiMac
{
    public class WifiMac
    {
        private static WifiMac instance;
        public static WifiMac GetInstance()
        {
            if (instance == null)
                instance = new WifiMac();
            return instance;
        }
        public String MacLocate(String input)
        {
            string url = "http://218.94.117.234:8484/Test01/search.do";
            string location = GetInfo(url, input,"mac");
            location = location.Replace("\"", String.Empty);            
            return string.Format("{0}\t{1}\n", input,location);
        }
        public string GetInfo(string url,string mac,string type)
        {

            //创建一个HTTP请求  
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //Post请求方式  
            request.Method = "POST";
            //内容类型
            request.ContentType = "application/x-www-form-urlencoded";

            //设置参数，并进行URL编码 

            string paraUrlCoded = type + "=" + mac.Replace(" ",string.Empty);

            //将Json字符串转化为字节  
            byte[] payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            //设置请求的ContentLength   
            request.ContentLength = payload.Length;
            //发送请求，获得请求流 

            Stream writer = null;
            try
            {
                writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
            }
            catch (Exception)
            {
                return "网络连接失败 " ;
            }
            //将请求参数写入流
            writer.Write(payload, 0, payload.Length);
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
            Stream s ;
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
            sRead.Close();
            //Console.WriteLine(postContent);//返回Json数据
            
            postContent = postContent.Replace("address", "地址").Replace("latitude", "纬度").Replace("longitude", "经度").Replace("state", "查询结果").Replace("ok", "成功").Replace("error", "失败").Replace("accuracy", "范围");
            string[] postContentArry = postContent.Split('{', '}', ',');
            if (postContentArry.Length > 5) 
                return string.Format("{0},{1}\t{2}米\t{3}\t{4}", 
                                        postContentArry[2], 
                                        postContentArry[6], 
                                        postContentArry[3], 
                                        postContentArry[5], 
                                        postContentArry[1]);
            return "查询失败";
        }

    }
}
