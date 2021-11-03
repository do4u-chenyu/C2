using C2.Utils;

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
            string IP = input.Trim(' ');
            if (!NetUtil.IsIPAddress(IP))
                return ("请正确输入IP号");
            string result = NetUtil.IPQuery_WhoIs(IP);
            return string.Format("{0}\t{1}\n",input,result);
        }
    }
}
