using C2.Business.WebsiteFeatureDetection;
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
            string url = "http://113.31.114.239:53373/api/spider/bank_info";
            Dictionary<string, string> pairs = new Dictionary<string, string> { { "bankcard", bankCardNum } };
            HttpWebRequest request = WFDWebAPI.GetInstance().ConfigPost(url);
            string content = JsonConvert.SerializeObject(pairs);
            byte[] data = Encoding.UTF8.GetBytes(content);
            string nameAndType;
            string city;
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
            catch(Exception ex)
            {
                return ex.Message;
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
        }
    }
}
