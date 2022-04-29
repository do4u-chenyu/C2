using C2.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;

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

            if (input == "基站号" || input == "WiFiMac号" || input == "银行卡号" || input == "IP")
                return string.Empty;
            foreach (var i in input)//检测输入是否为纯数字
            {
                if (!char.IsDigit(i))
                    return string.Empty;
            }
            string location = GetBankTool(input);
            return string.Format("{0}{1}{2}{3}", input, "\t", location, "\n");
        }

        public string GetBankTool(string bankCardNum)
        {
            Thread.Sleep(500);
            //string strURL = "http://www.teldata2018.com/cha/kapost.php?ka="+ bankCard.Replace(" ", string.Empty);
            //string strURL = "http://www.guabu.com/bank/?cardid=" + bankCard.Replace(OpUtil.StringBlank, string.Empty);//新接口，不好用，查得慢，输入中文会返回锟斤拷
            string url = "http://113.31.114.239:53373/api/spider/bank_info";
            Dictionary<string, string> pairs = new Dictionary<string, string> { { "bankcard", bankCardNum } };
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 200000;
            string content = JsonConvert.SerializeObject(pairs);
            byte[] data = Encoding.UTF8.GetBytes(content);

            request.Method = "POST";
            request.ContentType = "application/json";
            //request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";
            //request.Headers.Set("cookie", "td_cookie=1206126369; t=ed9584c3ee86740383a6dd94af963b16; r=461; UM_distinctid=17e2f0d4f7950c-0b8d03a6e92b87-3b39580e-1fa400-17e2f0d4f7a530; ASPSESSIONIDSSRDRBDB=CAJABDJAFEBEAAAHIOFCLBBC; CNZZDATA1279885053=887337253-1641463516-null%7C1643249436; security_session_verify=d256ef371a6fb7ffda35fec7c01e5c64");

            
            string nameAndType = string.Empty;
            string city = string.Empty;
            try
            {
                using (var stream = request.GetRequestStream())
                    stream.Write(data, 0, data.Length);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string postContent = new StreamReader(response.GetResponseStream()).ReadToEnd();
                JObject json = JObject.Parse(postContent);

                if (json["status"].ToString() != "success")
                    return json["masg"].ToString();

                var gList = json["data"];
                nameAndType = gList["type"].ToString();
                city = gList["city"].ToString().Replace("-", string.Empty).Replace(" ", string.Empty);
            }
            catch
            {
                return "网络连接中断";
            }
            /*
             * 458123038888888
             * <a href=http://www.guabu.com/bank/card/458123038888888.htm>吉林省 - 四平</a>
             * 交通银行 - 太平洋双币贷记卡VISA
             * 95559
             * <a href="http://www.bankcomm.com" target="_blank">http://www.bankcomm.com</a>
             */

            
            string cardName = string.Empty;
            string cardType = string.Empty;
            
            if (nameAndType.Length > 1)
            {
                cardName = nameAndType.Split('-')[0];
                cardType = nameAndType.Replace(cardName + "-", string.Empty).Replace(" ", string.Empty);
            }
            List<string> cardInfo = new List<string>();
            try
            {
                cardInfo.Add(cardName.Replace(" ", string.Empty));
                cardInfo.Add(cardType);
                cardInfo.Add(city);
            }
           
            catch { }

            if(cardInfo.Count ==3)
            {
                cardInfo[0] = cardInfo[0] == string.Empty ? "未知" : cardInfo[0];
                cardInfo[1] = cardInfo[1] == string.Empty ? "未知" : cardInfo[1];
                cardInfo[2] = cardInfo[2] == string.Empty ? "未知" : cardInfo[2];
                return string.Join("\t", cardInfo);
            }

            return "查询失败";

            /*
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
            */
        }

        /*
        public string GetChinese(string str)
        {
            //声明存储结果的字符串
            string chineseString = string.Empty;
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
        */
    }
}
