using C2.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

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
                return string.Empty;
            string[] lngandlat;
            if (input.Trim().Contains("\t"))
                lngandlat = input.Trim().Split('\t');
            else
                lngandlat = input.Trim().Split(' ');
            if (lngandlat.Length != 2)
                return string.Format("{0}\t{1}\n", input, "输入的数据格式不正确, 可能有多个空格或\\t分隔符");
            string lng = lngandlat[0];
            string lat = lngandlat[1];
            // string currentkey = "sxv5P7yMawt6vFIG0Gv5Lhps5Cefk0C7"; 
            // 备用key
            // FtB873TFjPPzgs7M3fs4oxTPqxr7MGn9; 目测只有单日6000
            // VSg8iL3koEZzG1FeDLmeVvYbvmecTP1W
            // string currentkey = "FtB873TFjPPzgs7M3fs4oxTPqxr7MGn9";
             string currentkey = "VSg8iL3koEZzG1FeDLmeVvYbvmecTP1W"; // 卢琪158的,单日30W




            string formatted_address = string.Empty;
            string sematic_description = string.Empty;

            string country = string.Empty;
            string province = string.Empty;
            string city = string.Empty;
            string district = string.Empty;
            string adcode = string.Empty;

            HttpClient client = new HttpClient();
            string bdUrl = string.Format("http://api.map.baidu.com/reverse_geocoding/v3/?ak={0}&output=json&coordtype=wgs84ll&location={1},{2}", currentkey, lat, lng);
            try
            {
                string result = client.GetStringAsync(bdUrl).Result;
                var locationResult = (JObject)JsonConvert.DeserializeObject(result);
                if (locationResult == null || locationResult["result"] == null || locationResult["result"]["formatted_address"].ToString() == string.Empty)
                {
                    string errMsg = string.Format("{0}\t{1}\n", input, "查询失败");
                    if (locationResult != null && locationResult["status"] != null && locationResult["message"] != null)
                        errMsg = string.Format("{0}\t{1}\t{2}\t{3}\n", 
                            input, 
                            "查询失败", 
                            locationResult["status"].ToString(),
                            locationResult["message"].ToString()
                            );
                    return errMsg;
                }
                    
                formatted_address = Convert.ToString(locationResult["result"]["formatted_address"]);
                if (locationResult["result"]["sematic_description"].ToString() != string.Empty)
                    formatted_address += OpUtil.StringBlank + Convert.ToString(locationResult["result"]["sematic_description"]);

                if (locationResult["result"]["addressComponent"] != null)
                {
                    if (locationResult["result"]["addressComponent"]["country"] != null)
                        country = locationResult["result"]["addressComponent"]["country"].ToString();

                    if (locationResult["result"]["addressComponent"]["province"] != null)
                        province = locationResult["result"]["addressComponent"]["province"].ToString();

                    if (locationResult["result"]["addressComponent"]["city"] != null)
                        city = locationResult["result"]["addressComponent"]["city"].ToString();

                    if (locationResult["result"]["addressComponent"]["district"] != null)
                        district = locationResult["result"]["addressComponent"]["district"].ToString();

                    if (locationResult["result"]["addressComponent"]["adcode"] != null)
                        adcode = locationResult["result"]["addressComponent"]["adcode"].ToString();
                }

            }
            catch
            {
                if (input == String.Empty)
                {
                    return String.Empty;
                }
                return string.Format("{0}\t{1}\n", input, "查询失败");
            }
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\n", input, 
                formatted_address,
                country,
                province,
                city,
                district,
                adcode);
        }
    }
}
