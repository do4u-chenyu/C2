using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using C2.Core;
using C2.Utils;

namespace C2.Business.IAOLab.IMEI
{
    public class IMEI
    {
        private static IMEI instance;
        private   Dictionary<string, string> table;
        public static IMEI GetInstance()
        {
            if (instance == null)
                instance = new IMEI();
            return instance;
        }

        public IMEI()
        {
            table = new Dictionary<string, string>();
            string ret = FileUtil.FileReadToEnd("C:\\Users\\FH\\Desktop\\dev_fin.txt");
            foreach (string line in ret.Split("\n"))
            {
                string[] lineSplit = line.Split("::");
                if (lineSplit.Length >= 2)
                    table[lineSplit[0].Trim()] = lineSplit[1].Trim();
            }
        }
        private string TryGetValue(string key)
        {
            return table.ContainsKey(key) ? table[key] : "未查到对应品牌";
        }
        public string IMEISearch(string input)
        {
            input = input.Trim();
            if (!Regex.IsMatch(input, @"[0-9a-fA-F]{14}|[0-9]{15}|[0-9]{17}"))
                return string.Format("{0}\t{1}{2}", input, "请输入正确的IMEI码或MEID码", System.Environment.NewLine);
            TryGetValue(input);
            return string.Format("{0}\t{1}\t{2}" + System.Environment.NewLine,
                input,
                input.Substring(0,8),
                TryGetValue(input.Substring(0, 8)));
        }
    }
}
