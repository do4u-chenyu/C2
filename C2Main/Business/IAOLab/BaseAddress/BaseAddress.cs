using System;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Net.Http;

namespace C2.IAOLab.BaseAddress
{
    public class BaseAddress
    {
        private static BaseAddress instance;
        public static BaseAddress GetInstance()
        {
            if (instance == null)
                instance = new BaseAddress();
            return instance;
        }

        public String BaseAddressLocate(string input)
        {
            if (input == "基站号" || input == "WiFiMac号" || input == "银行卡号" || input == "IP")
                return null;

            string jsonLat = string.Empty;
            string jsonLng = string.Empty;
            string reverseAddress = string.Empty;
            int index = 0;
            string currentkey = "sxv5P7yMawt6vFIG0Gv5Lhps5Cefk0C7";
            MatchCollection matches = Regex.Matches(input, "市");
            foreach (Match item in matches)
            {
                index = item.Index;
            }
            string city;
            string address;
            JObject jo;
            try
            {
                city = input.Substring(0, index + 1);
                address = input.Substring(index + 1, input.Length - index - 1);
            }
            catch 
            {
                city = String.Empty;
                address = String.Empty;
            }
            
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string url = String.Format("http://api.map.baidu.com/geocoding/v3/?ak={0}&address={1}&city={2}&output=json", currentkey, address, city);
            try
            {
                jo = ((JObject)JsonConvert.DeserializeObject(client.DownloadString(url)));
            }
            catch 
            {
                jo = null;
            }
            try
            {
                jsonLat = Convert.ToDouble(jo["result"]["location"]["lat"]).ToString("0.00000");
                jsonLng = Convert.ToDouble(jo["result"]["location"]["lng"]).ToString("0.00000");
                reverseAddress = GetLocation(currentkey, jsonLat, jsonLng);
            }
            catch 
            {
                if (input == String.Empty)
                {
                    return String.Empty;
                }
                return string.Format("{0}\t{1}\n", input, "查询失败");
            }
            return string.Format("{0}\t{1}\t{2}\t{3}\n", input, jsonLat, jsonLng, reverseAddress);
        }

        public static string GetLocation(string currentkey,string lng, string lat)
        {
            HttpClient client = new HttpClient();
            string bdUrl = string.Format("http://api.map.baidu.com/reverse_geocoding/v3/?ak={0}&output=json&coordtype=wgs84ll&location={1},{2}", currentkey,lng, lat);
        
            string result = client.GetStringAsync(bdUrl).Result;
            var locationResult = (JObject)JsonConvert.DeserializeObject(result);

            if (locationResult == null || locationResult["result"] == null || locationResult["result"]["formatted_address"] == null)
                return string.Empty;
            var address = Convert.ToString(locationResult["result"]["formatted_address"]);
            if (locationResult["result"]["sematic_description"] != null)
                address += " " + Convert.ToString(locationResult["result"]["sematic_description"]);
            return address;
        }
    }
}
