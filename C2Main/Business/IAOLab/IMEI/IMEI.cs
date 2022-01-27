using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.IAOLab.IMEI
{
    public class IMEI
    {
        private static IMEI instance;
        public static IMEI GetInstance()
        {
            if (instance == null)
                instance = new IMEI();
            return instance;
        }
        public string IMEISearch(string input)
        {
            return input;
        }
    }
}
