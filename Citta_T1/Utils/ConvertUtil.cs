using NPOI.SS.Formula.Functions;
using System;
using System.Text;
using System.Windows.Forms;

namespace Citta_T1.Utils
{
    class ConvertUtil
    {

        public static readonly Encoding GB2312 = System.Text.Encoding.GetEncoding("GB2312");
        public static bool TryParseBool(string value, bool defaultValue = false)
        {
            try
            {
                return bool.Parse(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static int TryParseInt(string value, int defaultValue = 0)
        {
            try
            {
                return int.Parse(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static bool IsInt(string value)
        {
            try
            {
                int.Parse(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ControlTextTryParseInt(Control ct)
        {
            bool notInt = false;
            try
            {
                int value = int.Parse(ct.Text);
                if (value > 0)
                    ct.Text = int.Parse(ct.Text).ToString();
                else
                {
                    ct.Text = String.Empty;
                    notInt = true;
                }                  
            }
            catch
            {
                notInt = true;
                ct.Text = String.Empty;
            }
            return notInt;
        }

        public static char TryParseAscii(string asciiChar, char defaultValue = '\t')
        {
            int ascii = TryParseInt(asciiChar, defaultValue);

            if (ascii < 0 || ascii > 255)
                return defaultValue;

            try
            {
                return Convert.ToChar(ascii);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static string SubstringByte(string text, int startIndex, int length)
        {
            byte[] bytes = ConvertUtil.GB2312.GetBytes(text);
            length = Math.Min(bytes.Length, length);
            return ConvertUtil.GB2312.GetString(bytes, startIndex, length);
        }

        public static int CountTextWidth(int chineseRatio, int otherRatio)
        {
            int padding = 3;
            int addValue = 10;
            if (chineseRatio == 1 && otherRatio == 0)   // chineseRatio = 1 && otherRatio = 0
                addValue -= 10;
            return padding * 2 + chineseRatio * 12 + otherRatio * 7 + addValue;
        }
    }
}
