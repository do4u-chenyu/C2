using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

using System.Windows.Forms;

namespace C2.IAOLab.IDcardGet
{
    public class IDcardGet
    {
        private static IDcardGet instance;
        public static IDcardGet GetInstance()
        {
            if (instance == null)
                instance = new IDcardGet();
            return instance;
        }
        public string IDcardSearch(string input)
        {

            if (input == "基站号" || input == "WiFiMac号" || input == "银行卡号" || input == "IP" || input == "备案号" || input =="身份证号查询")
                return null;
            foreach (var i in input)//检测输入是否为纯数字
            {
                if (!char.IsDigit(i))
                    return null;
            }

            string eighteen = Convert15to18(input);
            List<string> result = new List<string>();
            if (Convert15to18(input) == "请正确输入身份证号")
            {
                return string.Format("{0}{1}{2}{3}", input,"\t","身份证号有误", "\n");
            }
            else
            {
                result = IdInfo(eighteen);
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}", input, "\t", eighteen, "\t", result[0], "\t", result[1], "\t", result[2], "\n");
                //"身份证号\t转18位身份证号\t归属地\t出生日期\t性别\n"
            }
        }


        /// 读取JSON文件  <param name="key">JSON文件中的key值</param>  <returns>JSON文件中的value值</returns>
        public static string Readjson(string key)
        {
            string jsonfile = Path.Combine(System.Windows.Forms.Application.StartupPath, "Resources/Templates/AreaNum.json");
            using (System.IO.StreamReader file = System.IO.File.OpenText(jsonfile.Replace("bin\\Debug\\","")))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject o = (JObject)JToken.ReadFrom(reader);
                    JObject obj = JObject.Parse(o.ToString());
                    List<string> locate = new List<string>();
                    foreach (var x in obj)
                    {
                        locate.Add(x.Key);
                    }
                    if (locate.Contains(key))
                    {
                        var value = o[key].ToString();
                        return value;
                    }
                    else {
                        return String.Empty;
                    }              
                }
            }
            
        }

        public static string Convert15to18(string idCard)   //15位转18位
        {
            string code = idCard.Trim();//获得身份证号码
            if (code.Length == 15)
            {
                char[] strJY = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
                int[] intJQ = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1 };
                string strTemp;
                int intTemp = 0;
                strTemp = code.Substring(0, 6) + "19" + code.Substring(6);
                for (int i = 0; i <= strTemp.Length - 1; i++)
                {
                    intTemp = intTemp + int.Parse(strTemp.Substring(i, 1)) * intJQ[i];
                }
                intTemp = intTemp % 11;
                return strTemp + strJY[intTemp];
            }
            else
            {
                if (code.Length == 18)//如果是18位直接返回
                {
                    return code;
                }
                return "请正确输入身份证号";//如果即不是15位也不是18位则返回提示
            }
        }

        public static List<string> IdInfo(string idCard) 
        {
            List<string> result = new List<string>();
            string code = idCard.Trim();//获得身份证号码
            string locaNum = code.Substring(0,6);   //县级归属
            string cityNum = code.Substring(0, 4);  //市级归属
            string privNum = code.Substring(0, 2);  //省级归属
            int gender = int.Parse(code.Substring(16,1));
            string birth_y = code.Substring(6,4);
            string birth_m = code.Substring(10,2);
            string birth_d = code.Substring(12,2);

            if (Readjson(privNum + "0000") == string.Empty)  //归属地错误
            {
                string location = "归属地错误";
                result.Add(location);
            }
            else if (Readjson(cityNum + "00") == string.Empty)  //省级没错市级错了
            {
                string location = Readjson(privNum + "0000");
                result.Add(location);
            }
            else if (Readjson(locaNum) == string.Empty)  //省市没错，县错了
            {
                if (privNum == "11" || privNum == "12" || privNum == "31" || privNum == "50")
                {
                    string location = Readjson(privNum + "0000");
                    result.Add(location);
                }
                else
                {
                    string location = Readjson(privNum + "0000") + Readjson(cityNum + "00");
                    result.Add(location);
                }     
            }
            else
            {
                if (privNum == "11" || privNum == "12" || privNum == "31" || privNum == "50")
                {
                    string location = Readjson(privNum + "0000")  + Readjson(locaNum);
                    result.Add(location);
                }
                else
                {
                    string location = Readjson(privNum + "0000") + Readjson(cityNum + "00") + Readjson(locaNum);
                    result.Add(location);
                }
            }
            result.Add(birth_y + "年"+ birth_m + "月"+ birth_d + "日");
            if (gender % 2 == 0)
            {
                result.Add("女");
            }
            else
            {
                result.Add("男");
            }
            return result;
        }
    }  
}
