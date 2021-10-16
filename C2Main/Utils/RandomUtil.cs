using System;
using System.Text;

namespace C2.Utils
{
    class RandomUtil
    {
        private static readonly Random rd = new Random();
        private static readonly string table_hex = "abcedf0123456789ABCDEF";
        private static readonly string table_str = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly StringBuilder sb = new StringBuilder();

        // 返回随机HEX字符串, 不支持多线程
        // caseMode
        //  0  纯小写
        //  1  纯大写
        //  2  大小写混合
        public static string RandomHexString(int length = 9, int caseMode = 0, string prefix = "")
        {
            sb.Clear();
            sb.Append(prefix);
            for(int i = 0; i < length; i++)  // 效率不高,凑合着用
            {
                switch(caseMode)
                {
                    case 0:
                        sb.Append(table_hex[rd.Next(0, 16)]);
                        break;
                    case 1:
                        sb.Append(table_hex[rd.Next(6, 22)]);
                        break;
                    case 2:
                        sb.Append(table_hex[rd.Next(0, 22)]);
                        break;
                    default:
                        sb.Append('0');
                        break;
                }
            }
            return sb.ToString();
        }
        
        // 返回随机字符串, 不支持多线程
        // caseMode
        // 0 纯小写
        // 1 纯大写
        // 2 大小写混合
        public static string RandomString(int length = 2, int caseMode = 0)
        {
            sb.Clear();
            for (int i = 0; i < length; i++)  // 效率不高,凑合着用
            {
                switch (caseMode)
                {
                    case 0:
                        sb.Append(table_str[rd.Next(0, 36)]);
                        break;
                    case 1:
                        sb.Append(table_str[rd.Next(26, 62)]);
                        break;
                    case 2:
                        sb.Append(table_str[rd.Next(0, 62)]);
                        break;
                    default:
                        sb.Append('0');
                        break;
                }
            }
            return sb.ToString();
        }
    }
}
