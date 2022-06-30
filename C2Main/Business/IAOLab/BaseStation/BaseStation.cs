using System;
using C2.Core;

namespace C2.IAOLab.BaseStation
{
    public class BaseStation
    {
        private static BaseStation instance;
        public static BaseStation GetInstance()
        {
            if (instance == null)
                instance = new BaseStation();
            return instance;
        }

        public string BaseStationLocate(string input)
        {
            string url = Global.ServerUrl  + "/Castle02/station";
            if (input == "基站号" || input == "WiFiMac号" || input == "银行卡号" || input == "IP")
                return string.Empty;
            string location = WifiMac.WifiMac.GetInstance().GetInfo(url, input,"station");
            location = location.Replace("\"", string.Empty);
            return string.Format("{0}\t{1}\n", input, location);
        }
    }
}
