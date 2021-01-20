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
        string wrong = "输入有误\r\n";

        private string input;
        public static TimeAndIPTransform GetInstance(string input)
        {
            return new TimeAndIPTransform(input);
        }
        private TimeAndIPTransform(string input)
        {
            this.input = input;
        }
        public string timeIPTransform(string type)
        {
            //string wrong = "输入格式有误";
            string result;
            if(input.Trim() == string.Empty)
            {
                return "";
            }
            else
            {
                switch (type)
                {
                    case "绝对秒转真实时间":
                        result = sDate(input);
                        break;
                    case "真实时间转绝对秒":
                        result = dateS(input);
                        break;
                    case "IP转整形":
                        result = dotNum(input);
                        break;
                    case "整形转IP":
                        result = numDot(input);
                        break;
                    default:
                        result = wrong;
                        break;
                }

            }
            return input+" "+result;
        }

        #region 时间转换

        private string sDate(string Second)
        {
            try
            {
                uint sec = uint.Parse(Second);
                return "日期时间为"+unix2datetime(sec).ToString()+"\r\n";
            }
            catch
            {
                return wrong;
            }
        }
        private string dateS(string Date)
        {
            try
            {
                string[] strArr = Date.Split(new char[] { '/','-', ' ', ':'});
                DateTime dt = new DateTime(int.Parse(strArr[0]),int.Parse(strArr[1]),int.Parse(strArr[2]),int.Parse(strArr[3]),int.Parse(strArr[4]),int.Parse(strArr[5]));
                if(int.Parse(strArr[0]) > 2105)
                {
                    return wrong;
                }
                else
                {
                    uint Sec = datetime2unix(dt);
                    return ("绝对秒为" + Sec.ToString() + "\r\n");
                }
            }
            catch
            {
                return wrong;
            }
        }
        private string dotNum(string Dot)
        {
            try
            {
                string[] DotArray = Dot.Split('.');
                long a = uint.Parse(DotArray[0]);
                long b = uint.Parse(DotArray[1]);
                long c = uint.Parse(DotArray[2]);
                long d = uint.Parse(DotArray[3]);
                if (DotArray.Length != 4)
                {
                    return wrong;
                }
                else
                {
                    if (a > 255 || b > 255 || c > 255 || d > 255)
                    {
                        return wrong;
                    }
                    else
                    {
                        string[] items = Dot.Split('.');

                        long result = a << 24 | b << 16 | c << 8 | d;
                        return ("整型为" + result.ToString() + "\r\n");
                    }
                }
            }
            catch
            {
                return wrong;
            }
        }       
        private string numDot(string Num)
        {
            try
            {
                long n = uint.Parse(Num);
                StringBuilder dot = new StringBuilder();
                dot.Append((n >> 24) & 0xFF).Append(".");
                dot.Append((n >> 16) & 0xFF).Append(".");
                dot.Append((n >> 8) & 0xFF).Append(".");
                dot.Append(n & 0xFF);
                string result = dot.ToString();
                return ("IP为" + result + "\r\n");
            }
            catch
            {
                return wrong;
            }

        }
        public static DateTime unix2datetime(uint time1970)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return startTime.AddSeconds(time1970);
        }
        public static uint datetime2unix(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (uint)(time - startTime).TotalSeconds;
        }
        #endregion
        #region IP转换




        #endregion
    }
}
