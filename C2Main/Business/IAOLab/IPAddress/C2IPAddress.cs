using C2.Utils;

namespace C2.IAOLab.IPAddress
{
    public class C2IPAddress // 此处和.net自带的IPAddress重名, 加上C2前缀区别
    {
        private static C2IPAddress instance;
        public static C2IPAddress GetInstance()
        {
            if (instance == null)
                instance = new C2IPAddress();
            return instance;
        }
        public string GetIPAddress(string input) 
        {
            if (input == "基站号" || input == "WiFiMac号" || input == "银行卡号" || input == "IP")
                return string.Empty;
            //Regex reg3 = new Regex(@"^(([0-9][1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]$");
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
