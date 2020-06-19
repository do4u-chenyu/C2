using Citta_T1.Business.Model;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Citta_T1.Business.Option
{

    public class OperatorOption
    {
        private Dictionary<string, string> optionDict;
        public OperatorOption()
        {
            optionDict = new Dictionary<string, string>();
           
        }

        public Dictionary<string, string> OptionDict { get => optionDict; set => this.optionDict = value; }

        public string GetOption(string OpKey, string defaultValue = "")
        {
            if (!OptionDict.ContainsKey(OpKey) || String.IsNullOrEmpty(OptionDict[OpKey]))
                return defaultValue;
            return OptionDict[OpKey];
        }

        public string this[string key] { get { return optionDict[key]; } set { optionDict[key] = value; } }
        public List<string> Keys { get { return optionDict.Keys.ToList(); } }

        public override string ToString()
        {
            return string.Join(",", OptionDict.ToList());
        }

        public string[] GetOptionSplit(string OpKey, char separator = '\t', string defaultValue = "")
        {
            string[] ret = GetOption(OpKey, defaultValue).Split(separator);
            if (ret.Length == 1 && String.IsNullOrEmpty(ret[0]))
                ret = new string[0];
            return ret;
        }

        public void SetOption(string OpKey, string OpVaule)
        {
            OptionDict[OpKey] = OpVaule;
        }

        public void SetOption(string OpKey, int OpVaule)
        {
            SetOption(OpKey, OpVaule.ToString());
        }

        public void SetOption(string OpKey, bool OpVaule)
        {
            SetOption(OpKey, OpVaule.ToString());
        }

        public void SetOption(string OpKey, string[] OpVauleList)
        {
            SetOption(OpKey, String.Join("\t", OpVauleList));
        }

        public void SetOption(string OpKey, List<int> OpVauleList)
        {
            SetOption(OpKey, String.Join("\t", OpVauleList));
        }

        public void SetOption(string OpKey, List<string> OpVauleList)
        {
            SetOption(OpKey, String.Join("\t", OpVauleList));
        }

        public int KeysCount(string name)
        {
            int count = 0;
            List<string> keys = this.OptionDict.Keys.ToList();
            foreach (string key in keys)
            {
                if (key.Contains(name))
                    count += 1;
            }
            return count;
        }
        public void Clear()
        {
            OptionDict.Clear();
        }

    }
    

}