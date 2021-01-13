using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
                string macList = input;
                string[] macArr = macList.Split('\n');
                int j = macArr.Length+1;
                for (int i = 0; i < j; i++)
                {
                    string mac = macArr[i];
                    string location = getInfo(mac);
                    location = string.Join("",location.Split('{', '}','"'));
                    StringBuilder macLocation = new StringBuilder();
                    string m_macLocation = mac + "\t" + location + "\n";
                    macLocation.Append(m_macLocation);
                    string s_macLocation = macLocation.ToString();
                    return s_macLocation;
                }
                return null;
           
        }
        public string getInfo(string mac)
        {

            string strURL = "http://218.94.117.234:8484/Test01/search.do";
            //创建一个HTTP请求  
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);
            //Post请求方式  
            request.Method = "POST";
            //内容类型
            request.ContentType = "application/x-www-form-urlencoded";

            //设置参数，并进行URL编码 

            string paraUrlCoded = "mac="+ mac;//System.Web.HttpUtility.UrlEncode(jsonParas);   

            byte[] payload;
            //将Json字符串转化为字节  
            payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            //设置请求的ContentLength   
            request.ContentLength = payload.Length;
            //发送请求，获得请求流 

            Stream writer;
            try
            {
                writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
            }
            catch (Exception)
            {
                writer = null;
                Console.Write("连接服务器失败!");
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
            Stream s = response.GetResponseStream();
            //  Stream postData = Request.InputStream;
            StreamReader sRead = new StreamReader(s);
            string postContent = sRead.ReadToEnd();
            sRead.Close();
            Console.WriteLine(postContent);//返回Json数据
            return (postContent);
        }

    }
}
