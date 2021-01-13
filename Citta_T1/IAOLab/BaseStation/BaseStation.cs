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
            string macList = input;
            string[] macArr = macList.Split('\n');
            int j = macArr.Length + 1;
            for (int i = 0; i < j; i++)
            {
                string mac = macArr[i];
                string location = new WifiMac.WifiMac().GetInfo("http://218.94.117.234:8484/Test01/search.do", mac);
                location = string.Join("", location.Split('{', '}', '"'));
                StringBuilder macLocation = new StringBuilder();
                string m_macLocation = mac + "\t" + location + "\n";
                macLocation.Append(m_macLocation);
                string s_macLocation = macLocation.ToString();
                return s_macLocation;
            }
            return null;
            
        }
    }
}
