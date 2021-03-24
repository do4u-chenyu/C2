using System;

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

        public String BaseStationLocate(String input)
        {
            string url = "http://218.94.117.234:8484/Test01/station.do";
            string location = WifiMac.WifiMac.GetInstance().GetInfo(url, input,"station");
            location = location.Replace("\"", String.Empty);
            return string.Format("{0}\t{1}\n", input, location);
        }
    }
}
