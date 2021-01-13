using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime;
using System.Threading.Tasks;

namespace C2.IAOLab.Transform
{
    class TimeAndIPTransform
    {

        private string SData(uint Second)
        {
            uint Day = Second/86400;
            String Date = ReTime("1970-1-1", Day);
            uint Time = Second % 86400;
            byte Hour = (byte)(Time / 3600);
            byte Min = (byte)((Time % 3600)/60);
            byte Sec = (byte)(Time % 60);
            string result = Date + " " + Hour + ":" + Min + ":" + Sec;
            return result;
        }
        private uint DataS(string Date)
        {
            DateTime ShuRu = Convert.ToDateTime(Date);
            DateTime Ini = DateTime.Parse("1970-1-1 00:00:00");
            //uint Sec = TimeSpan.FromSeconds(ShuRu - Ini);
            return 0;
        }

        private static string ReTime(string data, uint str)
        {
            DateTime dt = DateTime.Parse(data);
            int year = dt.Year;
            int month = dt.Month;
            int day = dt.Day;
            int n = DateTime.DaysInMonth(year, month);
            int k = (int)(day + str);
            if (k > n)
            {
                day = (int)(str - (n - day));
                month = month + 1;
                if (month > 12)
                {
                    month = 1;
                    year = year + 1;
                }
            }
            else
            {
                day = (int)(day + str);
            }
            string c = year + "-" + month + "-" + day;
            return c;
        }
    }
}
