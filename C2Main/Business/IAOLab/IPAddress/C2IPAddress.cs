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
            string IP = input.Trim(' ');
            if (!NetUtil.IsIPAddress(IP))
                return "请正确输入IP号\n";
            string result = NetUtil.IPQuery_ChunZhen(IP);
            string province = result.Split('\t')[0];
            if (result.Contains("内网IP"))
            {
                result = result + '\t' + "内网";
            }
            else
            {
                if (NetUtil.IsMainlandOfChina(province))
                {
                    result = result + '\t' + "境内";
                }
                else
                {
                    result = result + '\t' + "境外";
                }
            }
            return string.Format("{0}\n",result);
        }
    }
}
