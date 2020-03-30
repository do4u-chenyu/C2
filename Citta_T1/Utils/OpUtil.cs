using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Drawing;
using System.Collections.Generic;

namespace Citta_T1.Utils
{
    public class OpUtil
    {
        /// <summary>
        /// MD5字符串加密
        /// </summary>
        /// <param name="txt"></param>
        /// <returns>加密后字符串</returns>
        public static string GenerateMD5(string txt)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(txt);
                //开始加密
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public static float IOU(Rectangle rect1, Rectangle rect2)
        {
            // [top, left, bottom, right]
            List<int> r1 = new List<int> { rect1.Location.Y, rect1.Location.X, rect1.Location.Y + rect1.Height, rect1.Location.X + rect1.Width };
            List<int> r2 = new List<int> { rect2.Location.Y, rect2.Location.X, rect2.Location.Y + rect2.Height, rect1.Location.X + rect1.Width };
            int inHeight = Math.Min(r1[2], r2[2]) - Math.Max(r1[0], r2[0]);
            int inWidth = Math.Min(r1[3], r2[3]) - Math.Max(r1[1], r2[1]);
            return inHeight > 0 && inWidth > 0 ? inHeight * inWidth : 0;
        }

        public static Point WorldBoundControl(Point Pm, float factor, int width, int height)
        {

            Point dragOffset = new Point(0, 0);
            Point Pw = Global.GetCurrentDocument().ScreenToWorld(new Point(50, 30), Pm);
            if (Pw.X < 50)
            {
                dragOffset.X = 50 - Pw.X;
            }
            if (Pw.Y < 30)
            {
                dragOffset.Y = 30 - Pw.Y;
            }
            if (Pw.X > 2000 - Convert.ToInt32(width / factor))
            {
                dragOffset.X = 2000 - Convert.ToInt32(width / factor) - Pw.X;
            }
            if (Pw.Y > 1000 - Convert.ToInt32(height / factor))
            {
                dragOffset.Y = 1000 - Convert.ToInt32(height / factor) - Pw.Y;
            }
            return dragOffset;
        }
    }
}
