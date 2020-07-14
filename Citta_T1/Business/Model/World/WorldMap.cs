using Citta_T1.Core;
using Citta_T1.Utils;
using NPOI.SS.Formula.Functions;
using System;
using System.Drawing;
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
        private static LogUtil log = LogUtil.GetInstance("CanvasPanel");
        private static readonly bool canvasUse = false;
        private readonly WorldMapInfo wmInfo = new WorldMapInfo();

        public Point MapOrigin { get => wmInfo.MapOrigin; set => wmInfo.MapOrigin = value; }
        public float ScreenFactor { get => wmInfo.ScreenFactor; set => wmInfo.ScreenFactor = value; }

        public int SizeLevel { get => wmInfo.SizeLevel; set => wmInfo.SizeLevel = value; }

        public WorldMap() { }
        public WorldMap(Point mo)
        {
            this.MapOrigin = mo;
        }
        //  Pw = Ps / Factor - Pm
        public Point ScreenToWorld(Point Ps, bool mode)
        {
            return mode.Equals(canvasUse)
                ? new Point
                {
                    X = Convert.ToInt32(Ps.X - MapOrigin.X * ScreenFactor),
                    Y = Convert.ToInt32(Ps.Y - MapOrigin.Y * ScreenFactor)
                }
                : new Point
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
        public PointF ScreenToWorldF(PointF Ps, bool mode)
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
            float factor = Global.GetCurrentDocument().WorldMap.ScreenFactor;
            int rightBorder = 2000 - 2 * moc.Width;
            int lowerBorder = 980 - moc.Height;
            int interval = moc.Height + Convert.ToInt32(factor * 5);

            Point Pm = new Point(moc.Location.X + moc.Width + Convert.ToInt32(factor * 25), moc.Location.Y);
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
        public void WorldBoundControl(Point Pm, Control ct)
        {
            Point Pw = ScreenToWorld(Pm, true);
            if (Pw.X < 20)
            {
                Pm.X = 20;
            }
            if (Pw.Y < 70)
            {
                Pm.Y = 70;
            }
            if (Pw.X > 2000 - ct.Width)
            {
                Pm.X = ct.Parent.Width - ct.Width;
            }
            if (Pw.Y > 980 - ct.Height)
            {
                Pm.Y = ct.Parent.Height - ct.Height;
            }
            ct.Location = Pm;
        }
        public Point WorldBoundControl(Point Pm, Rectangle minBoundingBox)
        {
            Point off = new Point(0, 0);
            if (Pm.X < 20)
            {
                off.X = 20 - Pm.X;
            }
            if (Pm.Y < 70)
            {
                off.Y = 70 - Pm.Y;
            }
            if (Pm.X > Convert.ToInt32(2000 * ScreenFactor) - minBoundingBox.Width)
            {
                off.X = Convert.ToInt32(2000 * ScreenFactor) - minBoundingBox.Width - Pm.X;
            }
            if (Pm.Y > Convert.ToInt32(980 * ScreenFactor) - minBoundingBox.Height)
            {
                off.Y = Convert.ToInt32(980 * ScreenFactor) - minBoundingBox.Height - Pm.Y;
            }
            return off;
        }
        #endregion

    }
}
