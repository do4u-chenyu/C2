using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NPOI.XSSF.UserModel;

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

            if (input == "基站号" || input == "WiFiMac号" || input == "银行卡号")
                return null;
            foreach (var i in input)//检测输入是否为纯数字
            {
                if (!char.IsDigit(i))
                    return null;
            }
            string location = GetBankTool(input);
            return string.Format("{0}{1}{2}{3}", input, "\t", location, "\n");
        }

        public string GetBankTool(string bankCard)
        {
            Thread.Sleep(500);
            string strURL = "http://www.teldata2018.com/cha/kapost.php?ka="+ bankCard.Replace(" ", string.Empty);
            //string strURL = "http://www.guabu.com/bank/?cardid=" + bankCard.Replace(" ", string.Empty);//新接口，不好用，查得慢，输入中文会返回锟斤拷
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
            catch (Exception)
            {
                return "网络连接失败 ";
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
            Stream s;
            try
            {
                if(response == null)
                    return "网络连接中断";
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
            //List<string> cardInfo = new List<string>();

            //cardInfo = subString(postContent, "<td>", "</td>"); 

            //if(cardInfo.Count == 5) 
            //{

            //    return String.Format("{0}\t{1}",
            //                        cardInfo[2].Replace(@"""", ""),//卡种
            //                        GetChinese(cardInfo[1])); //归属地，提取值中有噪音，需要处理
            //}
            //if (cardInfo.Count == 1)
            //    return cardInfo[0];

            //return "查询失败";

            postContent = string.Join("", postContent.Split('\r', '\n', '\t'));
            postContent = postContent.Replace("<br />", "\t");
            String[] postContentArry = postContent.Split('\t', '?');

            if (postContentArry.Length >= 9)
                return String.Format("{0}\t{1}\t{2}",
                    postContentArry[1].Replace("银行名称：", ""),  //  卡号
                    postContentArry[3].Replace("银行卡种：", ""),  //  地址
                    postContentArry[5].Replace("银行归属地：", "")); //  blabla
            if (postContentArry.Length == 2)
                return postContentArry[1];
            return "查询失败";
        }
        public string GetChinese(string str)
        {
            //声明存储结果的字符串
            string chineseString = "";
            //将传入参数中的中文字符添加到结果字符串中
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] >= 0x4E00 && str[i] <= 0x9FA5) //汉字
                {
                    chineseString += str[i];
                }
            }
            //返回保留中文的处理结果
            return chineseString;
        }  
        public List<string> subString(string data, string start, string end)
        {

            List<string> info = new List<string>();
            try
            {
                int i1 = data.IndexOf(start);
                int i2 = data.IndexOf(end, i1);
                string tmp = data.Substring(i1 + start.Length, i2 - i1 - start.Length);
                info.Add(tmp);
                while (i1 != -1 && i1 < data.Length - 1)
                {
                    i1 = data.IndexOf(start, i1 + 1);
                    if (i1 == -1)
                        break;
                    i2 = data.IndexOf(end, i1 + 1);
                    tmp = data.Substring(i1 + start.Length, i2 - i1 - start.Length);
                    info.Add(tmp);
                }
                return info;
            }
            catch (Exception)
            {
                info.Add("查询失败");
                return info;
            }
        }
    }
    

}
