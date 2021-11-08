using C2.Utils;
using System.Text.RegularExpressions;

namespace C2.IAOLab.IPAddress
{
    public class IPAddress
    {
        private static IPAddress instance;
        public static IPAddress GetInstance()
        {
            if (instance == null)
                instance = new IPAddress();
            return instance;
        }
        public string GetIPAddress(string input) 
        {
            if (input == "基站号" || input == "WiFiMac号" || input == "银行卡号" || input == "IP")
                return null;
            //Regex reg3 = new Regex(@"^((?:(?:25[0-5]|2[0-4]\d|[01]?\d?\d)\.){3}(?:25[0-5]|2[0-4]\d|[01]?\d?\d))$");
            //var IPs = reg3.Matches(input);
            //string IP = IPs[0].Value;
            string IP = input.Trim(' ');
            if (!NetUtil.IsIPAddress(IP))
                return ("请正确输入IP号\n");
            string result = NetUtil.IPQuery_ChunZhen(IP);
            return string.Format("{0}\n",result);
        }
    }
}
