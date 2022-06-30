
using System;
using System.IO;
using System.Net;
using C2.Business.IAOLab.WifiMac;
using C2.Core;
using C2.Utils;
using Newtonsoft.Json;

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
        public string MacLocate(string input)
        {
            string url = Global.ServerUrl + "/Castle02/mac";
            if (input == "基站号" || input == "WiFiMac号" || input == "银行卡号" || input == "IP")
                return string.Empty;
            string location = GetInfo(url, input,"mac");
            location = location.Replace("\"", string.Empty);            
            return string.Format("{0}\t{1}\n", input,location);
        }
        public string GetInfo(string url,string mac,string type)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            string paraUrlCoded = type + "=" + mac.Replace(OpUtil.StringBlank, string.Empty);
            byte[] payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            request.ContentLength = payload.Length;

            //发送请求，获得请求流 
            Stream writer;
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
            writer.Close();
           
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
            StreamReader sRead = new StreamReader(s);
            string postContent = sRead.ReadToEnd();
            sRead.Close();

            Place rt = JsonConvert.DeserializeObject<Place>(postContent);
            if (rt.state == "ok") 
                return string.Format("{0}\t{1}\t{2}\t{3}\t{4}", 
                                        rt.latitude, 
                                        rt.longitude, 
                                        rt.accuracy, 
                                        rt.tgdid, 
                                        rt.address);
            return "查询失败";
        }
    }
}
