using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.IAOLab.BankTool
{
    public class BankTool
    {
        private static BankTool instance;
        public static BankTool GetInstance()
        {
            if (instance == null)
                instance = new BankTool();
            return instance;
        }
        public string BankToolSearch(string input)
        {
            string bankCard = input;
            string location = GetBankTool(bankCard);
            //location = string.Join("", location.Split('{', '}', '"'));
            StringBuilder bankCardLocation = new StringBuilder();
            string m_bankCardLocation = bankCard + "\t" + location + "\n";
            bankCardLocation.Append(m_bankCardLocation);
            string s_bankCardLocation = bankCardLocation.ToString();
            return s_bankCardLocation;
        }

        public string GetBankTool(string bankCard)
        {

            return null;
        }
    }
   
}
