using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.IAOLab.WifiMac
{
    public class WifiMac
    {
        private static WifiMac instance;
        public static WifiMac GetInstance()
        {
            if (instance == null)
                instance = new WifiMac();
            return instance;
        }
        public String MacLocate(String input)
        {
            return String.Empty;
        }

    }
}
