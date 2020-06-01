using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Business.Model.World
{
    class WorldMapInfo
    {
        private Point mapOrigin;
        private float screenFactor;
        private int sizeLevel;

        public WorldMapInfo()
        {
            mapOrigin = new Point(-600, -300);
            screenFactor = 1;
            sizeLevel = 0;
        }
        public Point MapOrigin { get => mapOrigin; set => mapOrigin = value; }
        public float ScreenFactor { get => screenFactor; set => screenFactor = value; }
        public int SizeLevel { get => sizeLevel; set => sizeLevel = value; }

    }
    class WorldMap
    {
        private static readonly bool canvasUse = false;
        private readonly WorldMapInfo wmInfo = new WorldMapInfo();
        
        public Point MapOrigin { get => wmInfo.MapOrigin; set => wmInfo.MapOrigin = value; }
        public float ScreenFactor { get => wmInfo.ScreenFactor; set => wmInfo.ScreenFactor = value; }

        public int SizeLevel { get => wmInfo.SizeLevel; set => wmInfo.SizeLevel = value; }


        //  Pw = Ps / Factor - Pm
        public Point ScreenToWorld(Point Ps,bool mode)
        {
            return mode.Equals(canvasUse)
                ? new Point
                {
                    X = Convert.ToInt32(Ps.X - MapOrigin.X * ScreenFactor),
                    Y = Convert.ToInt32(Ps.Y - MapOrigin.Y * ScreenFactor)
                }
                :new Point
                {
                    X = Convert.ToInt32(Ps.X / ScreenFactor - MapOrigin.X),
                    Y = Convert.ToInt32(Ps.Y / ScreenFactor - MapOrigin.Y)
                };
        }

        // Ps = (Pw + Pm) * Factor
        public Point WorldToScreen(Point Pw)
        {
            Point Ps = new Point
            {
                X = Convert.ToInt32((Pw.X + MapOrigin.X) * ScreenFactor),
                Y = Convert.ToInt32((Pw.Y + MapOrigin.Y) * ScreenFactor)
            };
            return Ps;
        }
        public PointF ScreenToWorldF(PointF Ps,bool mode)
        {
            return mode.Equals(canvasUse)
                ? new PointF
                {
                    X = Convert.ToInt32(Ps.X - MapOrigin.X * ScreenFactor),
                    Y = Convert.ToInt32(Ps.Y - MapOrigin.Y * ScreenFactor)
                }
                : new PointF
                {
                    X = Convert.ToInt32(Ps.X / ScreenFactor - MapOrigin.X),
                    Y = Convert.ToInt32(Ps.Y / ScreenFactor - MapOrigin.Y)
                };
        }
        public PointF WorldToScreenF(Point Pw)
        {
            PointF Ps = new PointF
            {
                X = Convert.ToInt32((Pw.X + MapOrigin.X) * ScreenFactor),
                Y = Convert.ToInt32((Pw.Y + MapOrigin.Y) * ScreenFactor)
            };
            return Ps;
        }
        #region 边界控制---lxf专用&&算子边界控制&&画布拖动边界控制
        public Point WorldBoundRSControl(Control moc)
        {
            /*
             * 结果算子位置不超过地图右边界、下边界
             */

            int rightBorder = 2000 - 2 * moc.Width;
            int lowerBorder = 980 - moc.Height;
            int interval = moc.Height + 5;

            Point Pm = new Point(moc.Location.X + moc.Width + 25, moc.Location.Y);
            Point Pw = ScreenToWorld(Pm, true);

            if (Pw.X > rightBorder)
            {
                Pm.X = moc.Location.X;
                Pm.Y = moc.Location.Y + interval;
            }
            if (Pw.Y > lowerBorder)
            {
                Pm.Y = moc.Location.Y - interval;
            }
            return Pm;
        }

        public Point WorldBoundControl(float factor, int width, int height)
        {

            Point dragOffset = new Point(0, 0);
            Point Pw = ScreenToWorld(new Point(50, 30), true);

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
                dragOffset.Y = 980 - Convert.ToInt32(height / factor) - Pw.Y;
            }
            return dragOffset;
        }
        public Point WorldBoundControl(Point Ps)
        {

            Point dragOffset = new Point(0, 0);
            float screenFactor = ScreenFactor;

            if (Ps.Y < 70 * screenFactor)
            {
                dragOffset.Y = Ps.Y - 70;
            }
            if (Ps.X > 2000 * screenFactor)
            {
                dragOffset.X = Ps.X - 2000;
            }
            if (Ps.Y > 900 * screenFactor)
            {
                dragOffset.Y = Ps.Y - 900;
            }
            return dragOffset;
        }
        #endregion

    }
}
