using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using C2.Utils;
using System.Collections;

namespace C2.IAOLab.IDInfoGet
{
    public class IDInfoGet
    {
        private static IDInfoGet instance;
        private static string path = Path.Combine(Application.StartupPath, "Resources/Templates/AreaNum.txt");
        StreamReader file = new StreamReader(path);
        Hashtable table = new Hashtable();
        public static IDInfoGet GetInstance()
        {
            if (instance == null)
                instance = new IDInfoGet();
            return instance;
        }

        public string IDSearch(string input)
        {
            input = input.Trim();  
            string idStr = @"(^\d{15}$)|(^\d{6}(19|20)\d{9}[\dXx]$)";  
            if (!Regex.IsMatch(input, idStr))
                return string.Format("{0}\t{1}" + System.Environment.NewLine, input, "身份证号错误");
            string eighteen = CovertTo18(input);
            string line;
            while((line = file.ReadLine()) != null)
            {
                string[] entry = line.Split(':');
                table.Add(entry[0], entry[1]);
            }
            List<string> result = new List<string>();
            List<string> municipality = new List<string>() { "110000", "120000", "310000", "500000" };   //直辖市编号
            string areaCode = eighteen.Substring(0, 6);
            string birth = eighteen.Substring(6, 8);
            string province = areaCode.Substring(0, 2) + "0000";
            string city = areaCode.Substring(0, 4) + "00";
            string year = birth.Substring(0, 4);
            string month = birth.Substring(4, 2);
            string day = birth.Substring(6, 2);      
            if (!table.ContainsKey(province))   
                return string.Format("{0}\t{1}" + System.Environment.NewLine, input, "身份证号错误");   // System.Environment.NewLine代替\n
            if (!table.ContainsKey(city) && !municipality.Contains(province))    // 第一个版本, N个版本, 不用写的不写,不该算的不算
                result.Add(table[province].ToString());
            else if (!table.ContainsKey(areaCode)) 
            {
                if (municipality.Contains(province))
                    result.Add(table[province].ToString());
                else
                    result.Add(table[province].ToString() + table[city].ToString());
            }
            else
            {
                if (municipality.Contains(province))
                    result.Add(table[province].ToString() + table[areaCode].ToString());
                else
                    result.Add(table[province].ToString() + table[city].ToString() + table[areaCode].ToString());
            }
            result.Add(year + "年" + month + "月" + day + "日");
            result.Add(eighteen[16] % 2 == 0 ? "女" : "男");
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}" + System.Environment.NewLine,  // system.evnironment.newline
                input,               //原始数据， 功能上缺失了
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
            char[] JY = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
            int[] JQ = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1 };
            int sum = 0;
            int yy = ConvertUtil.TryParseInt(input.Substring(6, 2));
            string strTemp = input.Substring(0, 6) + (yy <= 20 ? "20" : "19") + input.Substring(6);
            for (int i = 0; i < strTemp.Length; i++)
                sum += int.Parse(strTemp.Substring(i, 1)) * JQ[i];
            return strTemp + JY[sum % 11];
        }
    }
}

