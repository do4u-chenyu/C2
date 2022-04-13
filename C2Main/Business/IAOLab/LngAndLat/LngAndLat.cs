using System;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Net.Http;
using C2.Utils;
using System.Windows.Forms;

namespace C2.Business.IAOLab.LngAndLat
{
    public class LngAndLat
    {
        private static LngAndLat instance;
        public static LngAndLat GetInstance()
        {
            if (instance == null)
                instance = new LngAndLat();
            return instance;
        }

        public String GetLocation(string input)
        {
            if (input == "基站号" || input == "WiFiMac号" || input == "银行卡号" || input == "IP" || input == "地址")
                return null;
            string[] lngandlat;
            if (input.Trim().Contains("\t"))
                lngandlat = input.Trim().Split('\t');
            else
                lngandlat = input.Trim().Split(' ');
            if (lngandlat.Length != 2)
                return string.Format("{0}\t{1}\n", input, "输入的数据格式不正确, 可能有多个空格或\\t分隔符");
            string lng = lngandlat[0];
            string lat = lngandlat[1];
            string currentkey = "sxv5P7yMawt6vFIG0Gv5Lhps5Cefk0C7";
            string address = string.Empty;
            HttpClient client = new HttpClient();
            string bdUrl = string.Format("http://api.map.baidu.com/reverse_geocoding/v3/?ak={0}&output=json&coordtype=wgs84ll&location={1},{2}", currentkey, lat, lng);
            try
            {
                string result = client.GetStringAsync(bdUrl).Result;
                var locationResult = (JObject)JsonConvert.DeserializeObject(result);
                if (locationResult == null || locationResult["result"] == null || locationResult["result"]["formatted_address"].ToString() == string.Empty)
                    return string.Format("{0}\t{1}\n", input, "查询失败");
                address = Convert.ToString(locationResult["result"]["formatted_address"]);
                if (locationResult["result"]["sematic_description"].ToString() != string.Empty)
                    address += OpUtil.StringBlank + Convert.ToString(locationResult["result"]["sematic_description"]);
            }
            catch
            {
                if (input == String.Empty)
                {
                    return String.Empty;
                }
                return string.Format("{0}\t{1}\n", input, "查询失败");
            }
            return string.Format("{0}\t{1}\n", input, address);
        }
    }
}
