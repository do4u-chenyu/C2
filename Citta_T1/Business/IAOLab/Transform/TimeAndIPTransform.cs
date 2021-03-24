using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;


namespace C2.IAOLab.Transform
{
    class TimeAndIPTransform
    {
        private readonly string wrong = "输入有误\n";

        private readonly string input;
        public static TimeAndIPTransform GetInstance(string input)
        {
            return new TimeAndIPTransform(input);
        }
        private TimeAndIPTransform(string input)
        {
            this.input = input;
        }
        public string TimeIPTransform(string type)
        {
            string result;
            if (string.IsNullOrEmpty(input.Trim()))
                return wrong;
            switch (type)
            {
                case "绝对秒转真实时间":
                    result = ConvertToRealTime(input);
                    break;
                case "真实时间转绝对秒":
                    result = ConvertToAbsoluteSecond(input);
                    break;
                case "IP转整形":
                    result = ConvertToIntegralIP(input);
                    break;
                case "整形转IP":
                    result = ConvertToNormalIP(input);
                    break;
                default:
                    result = wrong;
                    break;
            }
            return string.Join(" ", input, result);
        }

        #region 时间转换

        private string ConvertToRealTime(string Second)
        {
            try
            {
                uint sec = uint.Parse(Second);
                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                return "真实时间:"+startTime.AddSeconds(sec).ToString()+"\n";
            }
            catch
            {
                return wrong;
            }
        }
        private string ConvertToAbsoluteSecond(string Date)
        {
            try
            {
                string[] strArr = Date.Split(new char[] { '/', '-', ' ', ':'});
                strArr = strArr.Where(s => !string.IsNullOrEmpty(s)).ToArray();
                if (strArr.Length != 6)
                    return wrong;
                DateTime dt = new DateTime(int.Parse(strArr[0]),int.Parse(strArr[1]),int.Parse(strArr[2]),int.Parse(strArr[3]),int.Parse(strArr[4]),int.Parse(strArr[5]));
                if(int.Parse(strArr[0]) > 2105)
                {
                    return wrong;
                }
                else
                {
        
                    DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));             
                    return ("绝对秒:" + (dt - startTime).TotalSeconds.ToString() + "\n");
                }
            }
            catch
            {
                return wrong;
            }
        }

        private string ConvertToIntegralIP(string Dot)
        {
            string[] DotArray = Dot.Split('.');
            if (DotArray.Length != 4)
                return wrong;

            try
            {

                long a = uint.Parse(DotArray[0]);
                long b = uint.Parse(DotArray[1]);
                long c = uint.Parse(DotArray[2]);
                long d = uint.Parse(DotArray[3]);

                if (a > 255 || b > 255 || c > 255 || d > 255)
                {
                    return wrong;
                }
                else
                {

                    long result = a << 24 | b << 16 | c << 8 | d;
                    return ("整型:" + result.ToString() + "\n");
                }

            }
            catch
            {
                return wrong;
            }
        }       
        private string ConvertToNormalIP(string Num)
        {
            try
            {
                uint n = uint.Parse(Num);
                StringBuilder dot = new StringBuilder();
                dot.Append((n >> 24) & 0xFF)
                   .Append(".")
                   .Append((n >> 16) & 0xFF)
                   .Append(".")
                   .Append((n >> 8) & 0xFF)
                   .Append(".")
                   .Append(n & 0xFF);
                return string.Format("IP:{0}\n", dot);
             
            }
            //catch (Exception ex)
            catch
            {
                //return ex.ToString();
                return wrong;
            }

        }

   
        #endregion
       
    }
}
