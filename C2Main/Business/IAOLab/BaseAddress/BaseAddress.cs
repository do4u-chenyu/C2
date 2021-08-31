using System;
using System.Net;
using System.IO;
using C2.Business.IAOLab.WifiMac;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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
            if (input == "基站号" || input == "WiFiMac号" || input == "银行卡号")
                return null;

            string jsonLat;
            string jsonLng;
            int index = 0;
            string currentkey = "sxv5P7yMawt6vFIG0Gv5Lhps5Cefk0C7";
            MatchCollection matches = Regex.Matches(input, "市");
            foreach (Match item in matches)
            {
                index = item.Index;
            }
            string city = input.Substring(0, index + 1);
            string address = input.Substring(index+1, input.Length-index-1);

            
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string url = String.Format("http://api.map.baidu.com/geocoding/v3/?ak={0}&address={1}&city={2}&output=json", currentkey, address, city);

                
            JObject jo = ((JObject)JsonConvert.DeserializeObject(client.DownloadString(url)));
            string status = jo["status"].ToString();
            {
                jsonLat = jo["result"]["location"]["lat"].ToString();
                jsonLng = jo["result"]["location"]["lng"].ToString();
            }
            return string.Format("{0}\t{1}\t{2}\n", input, jsonLat, jsonLng);
        }
    }
}
