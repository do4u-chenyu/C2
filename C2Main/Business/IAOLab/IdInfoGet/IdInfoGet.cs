using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using C2.Utils;
using C2.Core;

namespace C2.IAOLab.IDInfoGet
{
    public class IDInfoGet
    {
        private static IDInfoGet instance;
        private readonly Dictionary<string, string> table;

        public static IDInfoGet GetInstance()
        {
            if (instance == null)
                instance = new IDInfoGet();
            return instance;
        }

        public IDInfoGet()
        {
            table = new Dictionary<string, string>(1024 * 4);
            string ret = FileUtil.FileReadToEnd(Path.Combine(Application.StartupPath, "Resources/Templates/AreaNum.txt"));
            foreach(string line in ret.Split(System.Environment.NewLine))
            {
                string[] lineSplit = line.Split(":");
                if (lineSplit.Length >= 2)
                    table[lineSplit[0].Trim()] = lineSplit[1].Trim();
            }
        }

        private string TryGetValue(string key)
        {
            return table.ContainsKey(key) ? table[key] : string.Empty;
        }

        public string IDSearch(string input)
        {
            input = input.Trim();  
            if (!Regex.IsMatch(input, @"(^\d{15}$)|(^\d{6}(19|20)\d{9}[\dXx]$)"))
                return string.Format("{0}\t{1}{2}", input, "身份证号错误", System.Environment.NewLine);
            string id15 = Convert18To15(input);
            string id18 = Convert15To18(input);
            string province = TryGetValue(id18.Substring(0, 2) + "0000");
            string city = TryGetValue(id18.Substring(0, 4) + "00");
            string village = TryGetValue(id18.Substring(0, 6));
            string description = province + city + village;
            string birthday = id18.Substring(6, 8);
            string gender = id18[16] % 2 == 0 ? "女" : "男";
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}" + System.Environment.NewLine,
                id15,                // 15
                id18,                // 18
                birthday,            // 生日
                gender,              // 性别
                description);        // 归属地            
        }

        private string Convert15To18(string input)
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
                sum += (strTemp[i] - 0x30) * JQ[i];
            return strTemp + JY[sum % 11];
        }

        private string Convert18To15(string input)
        {
            if (input.Length == 15)
                return input;
            if (input.Length != 18)
                return string.Empty;
            return input.Substring(0, 6) + input.Substring(8, 9);
        }
    }
}

