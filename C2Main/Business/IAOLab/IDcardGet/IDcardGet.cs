using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
// 初始 163行, 4个小时    101行，2个小时 
namespace C2.IAOLab.IDcardGet
{
    public class IDcardGet {   
        private static IDcardGet instance;
        public static IDcardGet GetInstance() {
            if (instance == null)
                instance = new IDcardGet();
            return instance;
        }
        public string IDsearch(string input) {   //未用static关键词修饰，须用对象调用
            foreach (var i in input)//检测输入是否为纯数字
            {
                if (!char.IsDigit(i))
                    return null;
            }
            if(C15to18(input) ==string.Empty)
                return string.Format("{0}{1}{2}{3}", input, "\t", "身份证有误", "\n");
            List<string> result = new List<string>();
            result = GetResult(C15to18(input));
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}", input, "\t", C15to18(input), "\t", result[0], "\t", result[1], "\t", result[2], "\n");   //按此格式输出,输入，18位，归属地，生日，
        }
        public static string C15to18(string idCard)  //15位转18位
        {
            string code = idCard.Trim();  //获得身份证号码
            if (code.Length == 15)     
            {
                char[] strJY = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
                int[] intJQ = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1 };
                int intTemp = 0;
                string strTemp = int.Parse(code.Substring(6, 2)) <= 20 ? code.Substring(0, 6) + "20" + code.Substring(6) : strTemp = code.Substring(0, 6) + "19" + code.Substring(6);
                for (int i = 0; i <= strTemp.Length - 1; i++)
                    intTemp = intTemp + int.Parse(strTemp.Substring(i, 1)) * intJQ[i];
                intTemp = intTemp % 11;
                return strTemp + strJY[intTemp];
            }else
                return code.Length == 18 ? code : string.Empty;    //如果是18位直接返回,即不是15位也不是18位则返回空
        }
        public static string Readjson(string input)    //读json文件
        {   string jsonfile = Path.Combine(System.Windows.Forms.Application.StartupPath, "Resources/Templates/AreaNum.json");
            using (System.IO.StreamReader file = System.IO.File.OpenText(jsonfile.Replace("bin\\Debug\\","")))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject o = (JObject)JToken.ReadFrom(reader);  //该语句取出每一组json语句  JObject obj = JObject.Parse(o.ToString());
                    List<string> location = new List<string>();
                    foreach (var x in o)
                        location.Add(x.Key);
                    return location.Contains(input) ? o[input].ToString() : String.Empty;
                }
            }
        }
        public static List<string> GetResult(string input) //获取归属地，生日，性别
        {
            string code = input.Trim();
            List<string> result = new List<string>();
            List<string> zhiXia = new List<string>() {"11","12","31","50" };
            string xianNum = code.Substring(0, 6);
            int sex = int.Parse(code.Substring(16, 1));
            string birth_N = code.Substring(6, 8);
            if (Readjson(xianNum.Substring(0, 2) + "0000") == string.Empty)  //归属地错误
                result.Add("归属地错误");
            else if (Readjson(xianNum.Substring(0, 4) + "00") == string.Empty && !zhiXia.Contains(xianNum.Substring(0, 2)))  //省级没错市级错了
                result.Add(Readjson(xianNum.Substring(0, 2) + "0000"));
            else if (Readjson(xianNum) == string.Empty)  //省市没错，县错了
                result.Add(zhiXia.Contains(xianNum.Substring(0, 2)) ? Readjson(xianNum.Substring(0, 2) + "0000") : Readjson(xianNum.Substring(0, 2) + "0000") + Readjson(xianNum.Substring(0, 4) + "00"));
            else  //县级单位没错
                result.Add(zhiXia.Contains(xianNum.Substring(0, 2)) ? Readjson(xianNum.Substring(0, 2) + "0000") + Readjson(xianNum) : Readjson(xianNum.Substring(0, 2) + "0000") + Readjson(xianNum.Substring(0, 4) + "00") + Readjson(xianNum));
            string birth = birth_N.Substring(0, 4) + "年" + birth_N.Substring(4, 2) + "月" + birth_N.Substring(6, 2) + "日";
            result.Add(birth);
            result.Add(sex % 2 != 0 ? "男" : "女");
            return result;
        }
    }
}
