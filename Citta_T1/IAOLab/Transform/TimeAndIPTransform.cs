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
        public string timeIPTransform(string input, string type)
        {
            string wrong = "输入格式有误";
            string result;
            switch (type)
            {
                case "sDate":
                    result = sDate(input);
                    break;
                case "dateS":
                    result = dateS(input);
                    break;
                case "dotNum":
                    result = dotNum(input);
                    break;
                case "numDot":
                    result = numDot(input);
                    break;
                default:
                    result = wrong;
                    break;
            }
            return result;
        }

        #region 时间转换
        private string sDate(string Second)
        {
            uint sec = uint.Parse(Second);
            uint Day = sec / 86400;
            String Date = ReTime("1970-1-1", Day);
            uint Time = sec % 86400;
            //byte Hour = (byte)(Time / 3600);
            //byte Min = (byte)((Time % 3600)/60);
            //byte Sec = (byte)(Time % 60);
            uint Hour = Time / 3600;
            uint Min = (Time % 3600) / 60;
            uint Sec = Time % 60;
            Date = ReTime(Date, Day);
            string result = Date + " " + Hour + ":" + Min + ":" + Sec;
            return result;
        }
        private string dateS(string Date)
        {
            DateTime input = Convert.ToDateTime(Date);
            DateTime Ini = DateTime.Parse("1970-1-1 00:00:00");
            uint Sec = (uint)((input - Ini).Seconds);
            return Sec.ToString();
        }
        private string dotNum(string Dot)
        {
            string[] DotArray = Dot.Split('.');
            return (1677216 * uint.Parse(DotArray[0]) + 65536 * uint.Parse(DotArray[1]) + 256 * uint.Parse(DotArray[2]) + uint.Parse(DotArray[3])).ToString();
        }
        private string numDot(string Num)
        {
            uint n = uint.Parse(Num);
            uint a = n / 1677216;
            uint b = (n % 1677216) / 65536;
            uint c = (n % 65536) / 256;
            uint d = n % 256;
            return (a + "." + b + "." + c + "." + d);

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
        #endregion
        #region IP转换




        #endregion
    }
}
