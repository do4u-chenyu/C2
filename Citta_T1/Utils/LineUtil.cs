using C2.Business.Model;
using C2.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace C2.Utils
{
    public static class MyPens
    {
        public static Pen Gray = new Pen(Color.Gray, 1);
        public static Pen GrayBold = new Pen(Color.Green, 2);
        public static Pen DarkGray = new Pen(Color.DarkGray, 1);
        public static Pen GreenDash2f = new Pen(Color.Green, 2f)
        {
            DashStyle = DashStyle.Dash
        };
    }
    public static class LineUtil
    {
        public static float THRESHOLD = 5;
        public static float DISTNOTONLINE = -1;
        public static int CUTPOINTNUM = 10;
        public static int CUTPTSOFFSET = 10;

        public enum LineStatus
        {
            Null,
            Selected
        }
        public static void ChangeLoc(float dx, float dy)
        {
            List<ModelRelation> modelRelations = Global.GetCurrentDocument().ModelRelations;
            foreach (ModelRelation mr in modelRelations)
            {
                mr.ChangeLoc(dx, dy);
            }
        }

        public static void DrawBezier(Graphics g, PointF s, PointF a, PointF b, PointF e, bool isBold)
        {
            if (isBold)
                g.DrawBezier(MyPens.GrayBold, s, a, b, e);
            else
                g.DrawBezier(MyPens.Gray, s, a, b, e);
        }

        public static Rectangle ConvertRect(RectangleF r)
        {
            return new Rectangle((int)r.Left, (int)r.Top, (int)r.Width, (int)r.Height);
        }

        public static double DistanceOf2P(double x1, double y1, double x2, double y2)
        {
            double lineLength = 0;
            lineLength = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));

            return lineLength;
        }

        /// <summary>
        /// 点到线段距离  
        /// </summary>
        /// <param name="x1">线段起点</param>
        /// <param name="y1">线段起点</param>
        /// <param name="x2">线段终点</param>
        /// <param name="y2">线段终点</param>
        /// <param name="x0">点</param>
        /// <param name="y0">点</param>
        /// <returns></returns>
        public static double PointToLineD(double x1, double y1, double x2, double y2, double x0, double y0)
        {
            double a = DistanceOf2P(x1, y1, x2, y2);// 线段的长度    
            double b = DistanceOf2P(x1, y1, x0, y0);// (x1,y1)到点的距离    
            double c = DistanceOf2P(x2, y2, x0, y0);// (x2,y2)到点的距离    
            // 距离太近时
            if (c <= 0.000001 || b <= 0.000001)
                return 0;

            // 线太短时
            if (a <= 0.000001)
                return b;
            /*
             *        P 
             *           B-----C       
             */
            if (c * c >= a * a + b * b)
                return b;
            /*
             *           P
             *  B-----C       
             */
            if (b * b >= a * a + c * c)
                return c;

            double p = (a + b + c) / 2;// 半周长    
            double s = Math.Sqrt(p * (p - a) * (p - b) * (p - c));// 海伦公式求面积    
            return 2 * s / a;// 返回点到线的距离（利用三角形面积公式求高）    
        }
        public static float PointToLine(PointF p1, PointF lineStartP, PointF lineEndP)
        {
            return (float)PointToLineD(lineStartP.X, lineStartP.Y, lineEndP.X, lineEndP.Y, p1.X, p1.Y);
        }
    }

    // 划线类
    public class Bezier
    {
        private PointF startP;
        private PointF endP;
        private PointF a;
        private PointF b;
        private RectangleF rect;
        private PointF[] points;
        private PointF[] cutPointFs;
        private int cutPointNum = LineUtil.CUTPOINTNUM;


        public PointF StartP { get => startP; set => startP = value; }
        public PointF EndP { get => endP; set => endP = value; }
        public PointF[] Points { get => points; set => points = value; }
        public PointF[] CutPointFs { get => cutPointFs; set => cutPointFs = value; }
        public PointF A { get => a; set => a = value; }
        public PointF B { get => b; set => b = value; }

        //Pen pen;
        public Bezier(PointF p1, PointF p2)
        {
            startP = p1;
            endP = p2;
            UpdatePoints();
        }
        public Bezier(PointF p1, PointF p2, PointF p3, PointF p4)
        {
            startP = p1;
            this.a = p2;
            this.b = p3;
            endP = p4;
        }

        public void DrawBezier(Graphics g, bool isBold = false)
        {
            Pen p;
            if (isBold)
                p = MyPens.GrayBold;
            else
                p = MyPens.Gray;
            g.DrawBezier(p, this.startP, this.a, this.b, this.endP);
            p.Dispose();
        }
        public void DrawNaviewBezier(Graphics g)
        {
            g.DrawBezier(MyPens.Gray, this.startP, this.a, this.b, this.endP);
        }
        public RectangleF GetBoundingRect()
        {
            return GetRect(startP, endP);
        }
        private RectangleF GetRect(PointF p1, PointF p2, float width = 5)
        {
            float x = Math.Min(p1.X, p2.X);
            float y = Math.Min(p1.Y, p2.Y);
            float w = Math.Abs(p1.X - p2.X);
            float h = Math.Abs(p1.Y - p2.Y);
            RectangleF rect = new RectangleF(x, y, w, h); ;
            rect.Inflate(width, width);
            return rect;
        }
        public static RectangleF GetRect(float x, float y, float w, float h)
        {
            return new RectangleF(x, y, w, h);
        }
        public void OnMouseMove(PointF p)
        {
            endP = p;
        }

        public void UpdatePoints()
        {
            this.a = new PointF((startP.X + endP.X) / 2, startP.Y);
            this.b = new PointF((startP.X + endP.X) / 2, endP.Y);
            this.rect = this.GetRect(this.StartP, this.EndP);
            this.points = this.GetPoints();
            this.cutPointFs = this.GetCutPointFs();
        }

        #region 线的采样点以及线选中的判定
        /// <summary>
        /// 遍历所有的线，判定在哪一条线上，如果在某一条线附近则返回该线的ModelElementIndex，否则返回-1
        /// 对线而言，判断某个点到自己的距离，如果超过距离阈值，返回-1，表示该点不在线附近
        /// 对点而言，需要知道自己距离哪个线最近，如果都是-1则表示该点不在任一条线附近
        /// </summary>
        /// <returns></returns>
        public float Distance(PointF p)
        {
            float threshold = LineUtil.THRESHOLD;
            PointF lineStartP;
            PointF lineEndP;
            float pToLine;
            float distNotOnLine = LineUtil.DISTNOTONLINE;
            if (!this.rect.Contains(p))
                return distNotOnLine;
            // 将线切割成 cutPointNum - 1 段，其实只要存cutPointNum个点就行了
            // 可能和若干个线都靠得很近
            for (int i = 0; i < cutPointFs.Length - 1; i++)
            {
                lineStartP = cutPointFs[i];
                lineEndP = cutPointFs[i + 1];
                pToLine = LineUtil.PointToLine(p, lineStartP, lineEndP);
                if (pToLine < threshold && pToLine > 0.001)
                    return pToLine;
            }
            return distNotOnLine;
        }
        /// <summary>
        /// 获得两个点为斜边的矩形区域
        /// </summary>
        /// <param name="p1">起点</param>
        /// <param name="p2">重点</param>
        /// <param name="width">边缘</param>
        /// <returns></returns>
        private static RectangleF GetRectF(PointF p1, PointF p2, double width = 1.0F)
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
        /// <summary>
        /// 获取线的采样点
        /// </summary>
        /// <returns></returns>
        private PointF[] GetPoints()
        {
            PointF[] pointList = new PointF[] { new PointF(this.StartP.X, this.StartP.Y), a, b, new PointF(this.EndP.X, this.EndP.Y) };
            return this.DrawBezierCurves(pointList, pointList.Length, 0.01F);
        }
        private PointF[] GetCutPointFs()
        {
            PointF[] pts = new PointF[cutPointNum];
            int index;
            for (int i = 0; i < cutPointNum; i++)
            {
                index = (int)Math.Floor((double)(i * 1.0 / (cutPointNum - 1) * this.points.Length));
                if (index == this.points.Length)
                    index -= 1;
                pts[i] = points[index];
            }
            // 第一个点和最后一个点向中间靠拢，解决点Pin的时候误触的问题
            pts[0].X += LineUtil.CUTPTSOFFSET;
            pts[cutPointNum - 1].X -= LineUtil.CUTPTSOFFSET;
            return pts;
        }
        /// <summary>
        /// 绘制n阶贝塞尔曲线路径
        /// </summary>
        /// <param name="points">输入点</param>
        /// <param name="count">点数(n+1)</param>
        /// <param name="step">步长,步长越小，轨迹点越密集</param>
        /// <returns></returns>
        public PointF[] DrawBezierCurves(PointF[] points, int count, float step)
        {
            List<PointF> bezier_curves_points = new List<PointF>();
            float t = 0F;
            do
            {
                PointF temp_point = BezierInterpolationFunc(t, points, count);    // 计算插值点
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
        private PointF BezierInterpolationFunc(float t, PointF[] points, int count)
        {
            PointF Point = new PointF();
            float[] part = new float[count];
            float sum_x = 0, sum_y = 0;
            for (int i = 0; i < count; i++)
            {
                ulong tmp;
                int n_order = count - 1;    // 阶数
                tmp = CalcCombinationNumber(n_order, i);
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
        private ulong CalcCombinationNumber(int n, int k)
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
        #endregion
    }
}
