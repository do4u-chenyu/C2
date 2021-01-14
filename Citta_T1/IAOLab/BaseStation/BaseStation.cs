using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C2.IAOLab;
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
            string baseStation = input;
            string location = new WifiMac.WifiMac().GetInfo("http://218.94.117.234:8484/Test01/station.do", baseStation,"station");
            location = string.Join("", location.Split('"'));
            StringBuilder baseStationLocationStringBuilder = new StringBuilder();
            string baseStationLocation = baseStation + "\t" + location + "\n";
            baseStationLocationStringBuilder.Append(baseStationLocation);
            string baseStationLocationString = baseStationLocationStringBuilder.ToString();
            return baseStationLocationString;
        }
    }
}
