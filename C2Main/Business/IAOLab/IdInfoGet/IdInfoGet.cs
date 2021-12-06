using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using C2.Utils;

namespace C2.IAOLab.IdInfoGet
{
    public class IdInfoGet
    {
        private static IdInfoGet instance;
        public static IdInfoGet GetInstance()
        {
            if (instance == null)
                instance = new IdInfoGet();
            return instance;
        }

        public string IdSearch(string input)
        {
            input = input.Trim();  
            string idStr = "(^[0-9]{15}$)|(^[1-9][0-9]{5}(19|20)[0-9]{9}[0-9Xx]$)";  
            if (!Regex.IsMatch(input, idStr))
                return string.Format("{0}\t{1}\n", input, "身份证号错误");
            input = CovertTo18(input);
            string jsonFile = Path.Combine(Application.StartupPath, "Resources/Templates/AreaNum.json");  
            StreamReader file = File.OpenText(jsonFile);
            JsonTextReader reader = new JsonTextReader(file);
            JObject o = (JObject)JToken.ReadFrom(reader);
            List<string> location = new List<string>();
            foreach (var i in o)
                location.Add(i.Key);
            List<string> result = new List<string>();
            List<string> municipality = new List<string>() { "11", "12", "31", "50" };   //直辖市编号
            string areaCode = input.Substring(0, 6);
            string birth = input.Substring(6, 8);
            string province = areaCode.Substring(0, 2);
            string city = areaCode.Substring(0, 4);
            string year = birth.Substring(0, 4);
            string month = birth.Substring(4, 2);
            string day = birth.Substring(6, 2);
            if (!location.Contains(province + "0000"))   
                return string.Format("{0}\t{1}\n", input, "身份证号错误");
            if (!location.Contains(city + "00") && !municipality.Contains(province))  
                result.Add(o[province + "0000"].ToString());
            else if (!location.Contains(areaCode)) 
            {
                if (municipality.Contains(province))
                    result.Add(o[province + "0000"].ToString());
                else
                    result.Add(o[province + "0000"].ToString() + o[city + "00"].ToString());
            }
            else
            {
                if (municipality.Contains(province))
                    result.Add(o[province + "0000"].ToString() + o[areaCode].ToString());
                else
                    result.Add(o[province + "0000"].ToString() + o[city + "00"].ToString() + o[areaCode].ToString());
            }
            result.Add(year + "年" + month + "月" + day + "日");
            int gender = ConvertUtil.TryParseInt(input.Substring(16, 1), 1);
            result.Add(gender % 2 != 0 ? "男" : "女");
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\n",
                input,      //原始数据
                CovertTo18(input),   //转18位
                result[0],    //归属地
                result[1],    //生日
                result[2]);   //性别
        }
        private static string CovertTo18(string input)
        {
            if (input.Length == 18)
                return input;
            if (input.Length != 15)
                return string.Empty;
            else
            {
                char[] JY = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
                int[] JQ = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1 };
                int sum = 0;
                string strTemp;
                int yy = ConvertUtil.TryParseInt(input.Substring(6, 2));
                strTemp = input.Substring(0, 6) + (yy <= 20 ? "20" : "19") + input.Substring(6);
                for (int i = 0; i < strTemp.Length; i++)
                    sum += int.Parse(strTemp.Substring(i, 1)) * JQ[i];
                return strTemp + JY[sum % 11];
            }
        }
    }
}

