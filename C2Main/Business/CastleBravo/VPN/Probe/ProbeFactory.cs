using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.CastleBravo.VPN.Probe
{
    class ProbeFactory
    {
        public static string GetRandomString(int length)
        {
            if (length == 0)
                return string.Empty;
            byte[] b = new byte[4];
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string str = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";
            string randomStr = string.Empty;
            for (int i = 0; i < length; i++)
                randomStr += str.Substring(r.Next(0, str.Length), 1);
            return randomStr;
        } 
    }
}
