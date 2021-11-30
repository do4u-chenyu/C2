using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace C2.IAOLab.BaseAddress
{
    public class RecordNumber
    {
        private static RecordNumber instance;
        public static RecordNumber GetInstance()
        {
            if (instance == null)
                instance = new RecordNumber();
            return instance;
        }

        public String WebUrlLocate(string input)
        {
            if (input == "基站号" || input == "WiFiMac号" || input == "银行卡号" || input == "IP" || input == "备案号")
                return null;

            string reverseAddress;
            try
            {
                reverseAddress = GetLocation(input.Trim());
            }
            catch 
            {
                if (input == String.Empty)
                {
                    return String.Empty;
                }
                return string.Format("{0}\t{1}\n", input, "查询失败");
            }
            return string.Format("{0}\t{1}\n", input, reverseAddress);
        }

        public static string GetLocation(string url)
        {
            HttpClient client = new HttpClient();
            string bdUrl = string.Format("https://api.vvhan.com/api/icp?url={0}", url);
        
            string result = client.GetStringAsync(bdUrl).Result;
            var locationResult = (JObject)JsonConvert.DeserializeObject(result);
            //if (locationResult.Property("message") !=null && !url.Contains("网站域名")) {
                //return "此域名未备案";
            //}
            var address = Convert.ToString(locationResult["info"]["icp"]);
            address = " " + address;
            return address;
        }
    }
}
