using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;

namespace C2.IAOLab.IdInfoGet
{
    public class IdInfoGet {
        private static IdInfoGet instance;
        public static IdInfoGet GetInstance() {
            if (instance == null)
                instance = new IdInfoGet();
            return instance;
        }

        public string IdSearch(string input)
        {
            foreach (var i in input)
            {
                if (!char.IsDigit(i))
                    return string.Empty;
            }
            string input1 = input.Trim();
            if(C15to18(input1) ==string.Empty)
                return string.Format("{0}{1}{2}{3}",input1,"\t","身份证号错误","\n");
            List<string> result;
            result = GetResult(C15to18(input1));
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}", input1,
                "\t", C15to18(input1), "\t", result[0], "\t", result[1], "\t", result[2], "\n");
        }

        private static string C15to18(string input){
            if (input.Length == 15)
            {
                char[] strJY = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
                int[] intJQ = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1 };
                int intTemp = 0;
                string strTemp;
                if (int.Parse(input.Substring(6, 2)) <= 20)
                    strTemp = input.Substring(0, 6) + "20" + input.Substring(6);
                else
                    strTemp = input.Substring(0, 6) + "19" + input.Substring(6);
                for (int i = 0; i <= strTemp.Length - 1; i++)
                    intTemp += int.Parse(strTemp.Substring(i, 1)) * intJQ[i];
                intTemp %= 11;
                return strTemp + strJY[intTemp];
            }
            else
                return input.Length == 18 ? input : string.Empty; 
        }

        private static List<string> GetResult(string input)
        {
            string jsonfile = Path.Combine(Application.StartupPath, "Resources/Templates/AreaNum.json");
            using (StreamReader file = File.OpenText(jsonfile))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject o = (JObject)JToken.ReadFrom(reader);
                    List<string> location = new List<string>();
                    foreach (var i in o)
                        location.Add(i.Key);
                    List<string> result = new List<string>();
                    List<string> municipality = new List<string>() { "11", "12", "31", "50" };
                    string locNum = input.Substring(0, 6);
                    string birth = input.Substring(6, 8);
                    int gender = int.Parse(input.Substring(16, 1));
                    if(!location.Contains(locNum.Substring(0, 2) + "0000"))   
                        result.Add("归属地错误");
                    else if (!location.Contains(locNum.Substring(0, 4) + "00")  && !municipality.Contains(locNum.Substring(0, 2)))  
                        result.Add(o[locNum.Substring(0, 2) + "0000"].ToString());
                    else if (!location.Contains(locNum))    
                    {
                        if (municipality.Contains(locNum.Substring(0, 2)))
                            result.Add(o[locNum.Substring(0, 2) + "0000"].ToString());
                        else
                            result.Add(o[locNum.Substring(0, 2) + "0000"].ToString() + o[locNum.Substring(0, 4) + "00"].ToString());
                    }
                    else         
                    {
                        if (municipality.Contains(locNum.Substring(0, 2)))
                            result.Add(o[locNum.Substring(0, 2) + "0000"].ToString() + o[locNum].ToString());
                        else
                            result.Add(o[locNum.Substring(0, 2) + "0000"].ToString() + o[locNum.Substring(0, 4) + "00"].ToString() + o[locNum].ToString());
                    }
                    result.Add(birth.Substring(0, 4) + "年" + birth.Substring(4, 2) + "月" + birth.Substring(6, 2) + "日");
                    result.Add(gender % 2 != 0 ? "男" : "女");
                    return result;
                }
            }
        }      
    }
}

