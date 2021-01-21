using System;
using System.IO;
using System.Net;

namespace C2.IAOLab.BankTool
{
    public class BankTool
    {
        private static BankTool instance;
        public static BankTool GetInstance()
        {
            if (instance == null)
                instance = new BankTool();
            return instance;
        }
        public string BankToolSearch(string input)
        {
            string location = GetBankTool(input);
            return string.Format("{0}{1}{2}{3}", input, "\t", location, "\n");
        }

        public string GetBankTool(string bankCard)
        {
            string strURL = "http://www.teldata2018.com/cha/kapost.php?ka="+ bankCard;
            //创建一个HTTP请求  
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);
            //Post请求方式  
            request.Method = "POST";
            //内容类型
            request.ContentType = "application/x-www-form-urlencoded";

            Stream writer = null;
            try
            {
                writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
            }
            catch (Exception ex)
            {
                if (writer == null)
                    writer.Close(); 
                return "网络连接失败: " + ex.Message;
            }

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
            postContent = string.Join("", postContent.Split('\r','\n','\t'));
            postContent = postContent.Replace("<br />", "\t");
            String[] postContentArry = postContent.Split('\t','?');

            if (postContentArry.Length >= 9)
                return String.Format("{0}\t{1}\t{2}", 
                    postContentArry[1],  //  卡号
                    postContentArry[3],  //  地址
                    postContentArry[5]); //  blabla

            return "银行卡格式不正确";
        }
    }
   
}
