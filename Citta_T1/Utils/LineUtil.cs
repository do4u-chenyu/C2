using Citta_T1.Controls;
using Citta_T1.Controls.Move;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Utils    
{
    public static class LineUtil
    {
        /// <summary>
        /// 绘制n阶贝塞尔曲线路径
        /// </summary>
        /// <param name="points">输入点</param>
        /// <param name="count">点数(n+1)</param>
        /// <param name="step">步长,步长越小，轨迹点越密集</param>
        /// <returns></returns>
        public static PointF[] draw_bezier_curves(PointF[] points, int count, float step)
        {
            List<PointF> bezier_curves_points = new List<PointF>();
            float t = 0F;
            do
            {
                PointF temp_point = bezier_interpolation_func(t, points, count);    // 计算插值点
                t += step;
                bezier_curves_points.Add(temp_point);
            }
            while (t <= 1 && count > 1);    // 一个点的情况直接跳出.
            return bezier_curves_points.ToArray();  // 曲线轨迹上的所有坐标点
        }
        /// <summary>
        /// n阶贝塞尔曲线插值计算函数
        /// 根据起点，n个控制点，终点 计算贝塞尔曲线插值
        /// </summary>
        /// <param name="t">当前插值位置0~1 ，0为起点，1为终点</param>
        /// <param name="points">起点，n-1个控制点，终点</param>
        /// <param name="count">n+1个点</param>
        /// <returns></returns>
        private static PointF bezier_interpolation_func(float t, PointF[] points, int count)
        {
            PointF Point = new PointF();
            float[] part = new float[count];
            float sum_x = 0, sum_y = 0;
            for (int i = 0; i < count; i++)
            {
                ulong tmp;
                int n_order = count - 1;    // 阶数
                tmp = calc_combination_number(n_order, i);
                sum_x += (float)(tmp * points[i].X * Math.Pow((1 - t), n_order - i) * Math.Pow(t, i));
                sum_y += (float)(tmp * points[i].Y * Math.Pow((1 - t), n_order - i) * Math.Pow(t, i));
            }
            Point.X = sum_x;
            Point.Y = sum_y;
            return Point;
        }
        /// <summary>
        /// 计算组合数公式
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        private static ulong calc_combination_number(int n, int k)
        {
            ulong[] result = new ulong[n + 1];
            for (int i = 1; i <= n; i++)
            {
                result[i] = 1;
                for (int j = i - 1; j >= 1; j--)
                    result[j] += result[j - 1];
                result[0] = 1;
            }
            return result[k];
        }

        public static Rectangle ConvertRect(RectangleF r)
        {
            return new Rectangle((int)r.Left, (int)r.Top, (int)r.Width, (int)r.Height);
        }


    }

    // 划线类
    public class Line
    {
        PointF startP;
        PointF endP;
        PointF a;
        PointF b;
        PointF[] points;

        public PointF StartP { get => startP; set => startP = value; }
        public PointF EndP { get => endP; set => endP = value; }

        //Pen pen;
        public Line(PointF p1, PointF p2)
        {
            startP = p1;
            endP = p2;
            UpdatePoints();
        }

        public void Draw(CanvasWrapper canvas, RectangleF rect)
        {
            // TODO [DK] 这里并不是精准的局部划线
            DrawLine(canvas.Graphics);
        }
        public void DrawLine(Graphics g)
        {
            Pen pen = new Pen(Color.Green);
            g.DrawCurve(pen, points);
            //g.DrawBezier(this.pen, this.startP, this.a, this.b, this.endP);
            pen.Dispose();
        }
        public RectangleF GetBoundingRect()
        {
            // TODO [DK] 没有考虑到坐标系放大系数
            return GetRect(startP, endP, 0);
        }
        public static RectangleF GetRect(PointF p1, PointF p2, double width)
        {
            double x = Math.Min(p1.X, p2.X);
            double y = Math.Min(p1.Y, p2.Y);
            double w = Math.Abs(p1.X - p2.X);
            double h = Math.Abs(p1.Y - p2.Y);
            RectangleF rect = GetRect(x, y, w, h);
            rect.Inflate((float)width, (float)width);
            return rect;
        }
        public static RectangleF GetRect(double x, double y, double w, double h)
        {
            return new RectangleF((float)x, (float)y, (float)w, (float)h);
        }
        public void OnMouseMove(PointF p)
        {
            endP = p;
        }

        public void UpdatePoints()
        {
            this.a = new PointF((startP.X + endP.X) / 2, startP.Y);
            this.b = new PointF((startP.X + endP.X) / 2, endP.Y);
            //pen = new Pen(Color.Green);
            PointF[] pointList = new PointF[] { new PointF(startP.X, startP.Y), a, b, new PointF(endP.X, endP.Y) };
            points = LineUtil.draw_bezier_curves(pointList, pointList.Length, 0.001F);
        }
    }
}
