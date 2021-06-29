﻿using C2.Core;
using C2.Utils;
using NPOI.SS.Formula.Functions;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace C2.Business.Model.World
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
        public WorldMapInfo(WorldMapInfo wmi)
        {
            this.mapOrigin = wmi.mapOrigin;
            this.screenFactor = wmi.screenFactor;
            this.sizeLevel = wmi.sizeLevel;
        }
        public Point MapOrigin { get => mapOrigin; set => mapOrigin = value; }
        public float ScreenFactor { get => screenFactor; set => screenFactor = value; }
        public int SizeLevel { get => sizeLevel; set => sizeLevel = value; }

    }
    public class WorldMap
    {
        private static LogUtil log = LogUtil.GetInstance("CanvasPanel");
        private static readonly bool canvasUse = false;
        private readonly WorldMapInfo wmInfo = new WorldMapInfo();

        public Point MapOrigin { get => wmInfo.MapOrigin; set => wmInfo.MapOrigin = value; }
        public float ScreenFactor { get => wmInfo.ScreenFactor; set => wmInfo.ScreenFactor = value; }

        public int SizeLevel { get => wmInfo.SizeLevel; set => wmInfo.SizeLevel = value; }

        public WorldMap() 
        {
            this.MapOrigin = Point.Empty;
        }
        public WorldMap(Point mo)
        {
            this.MapOrigin = mo;
        }
        public WorldMap(WorldMap wm)
        {
            this.wmInfo = new WorldMapInfo(wm.wmInfo);
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
            float factor = Global.GetCurrentModelDocument().WorldMap.ScreenFactor;
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
            
            if (Pw.X < 30)
            {
                dragOffset.X = 30 - Pw.X;
            }
            if (Pw.Y < 30)
            {
                dragOffset.Y = 30 - Pw.Y;
            }
            if (Pw.X > 2030 - Convert.ToInt32(width / factor))
            {
                dragOffset.X = 2030 - Convert.ToInt32(width / factor) - Pw.X;
            }
            if (Pw.Y > 1030 - Convert.ToInt32(height / factor))
            {
                dragOffset.Y = 1030 - Convert.ToInt32(height / factor) - Pw.Y;
            }
            return dragOffset;
        }
        public void WorldBoundControl(Point Pm, Control ct)
        {
            Point Pw = ScreenToWorld(Pm, true);
            if (Pw.X < 15)
            {
                Pm.X = 15;
            }
            if (Pw.Y < 5)
            {
                Pm.Y = 5;
            }
            if (Pw.X > 2000 - ct.Width)
            {
                Pm.X = ct.Parent.Width - ct.Width;
            }
            if (Pw.Y > 1000 - ct.Height)
            {
                Pm.Y = ct.Parent.Height - ct.Height;
            }
            ct.Location = Pm;
        }
        public Point WorldBoundControl(Point Pm, Rectangle minBoundingBox)
        {
            Point off = new Point(0, 0);
            if (Pm.X < 10)
            {
                off.X = 10 - Pm.X;
            }
            if (Pm.Y < 10)
            {
                off.Y = 10 - Pm.Y;
            }
            if (Pm.X > Convert.ToInt32(2000 * ScreenFactor) - minBoundingBox.Width)
            {
                off.X = Convert.ToInt32(2000 * ScreenFactor) - minBoundingBox.Width - Pm.X;
            }
            if (Pm.Y > Convert.ToInt32(1000 * ScreenFactor) - minBoundingBox.Height)
            {
                off.Y = Convert.ToInt32(1000 * ScreenFactor) - minBoundingBox.Height - Pm.Y;
            }
            return off;
        }
        #endregion

    }
}
