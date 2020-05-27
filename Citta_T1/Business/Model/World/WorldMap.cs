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
        private static bool naviewUse = true;
        private static bool canvasUse = false;
        private WorldMapInfo wmInfo = new WorldMapInfo();
        
        public WorldMapInfo GetWmInfo()
        {
            return wmInfo;
        }
        public void SetWmInfo(WorldMapInfo value)
        {
            wmInfo = value;
        }

        //  Pw = Ps / Factor - Pm
        public Point ScreenToWorld(Point Ps,bool mode)
        {

            if(mode.Equals(naviewUse))
            {
                return new Point
                {
                    X = Convert.ToInt32(Ps.X / GetWmInfo().ScreenFactor - GetWmInfo().MapOrigin.X),
                    Y = Convert.ToInt32(Ps.Y / GetWmInfo().ScreenFactor - GetWmInfo().MapOrigin.Y)
                };
            }

            if(mode.Equals(canvasUse))
            {
                return new Point
                {
                    X = Convert.ToInt32(Ps.X - GetWmInfo().MapOrigin.X * GetWmInfo().ScreenFactor),
                    Y = Convert.ToInt32(Ps.Y - GetWmInfo().MapOrigin.Y * GetWmInfo().ScreenFactor)
                };
            }               
            return new Point(0, 0);
        }

        // Ps = (Pw + Pm) * Factor
        public Point WorldToScreen(Point Pw)
        {
            Point Ps = new Point
            {
                X = Convert.ToInt32((Pw.X + GetWmInfo().MapOrigin.X) * GetWmInfo().ScreenFactor),
                Y = Convert.ToInt32((Pw.Y + GetWmInfo().MapOrigin.Y) * GetWmInfo().ScreenFactor)
            };
            return Ps;
        }
        public PointF ScreenToWorldF(PointF Ps)
        {
            PointF Pw = new PointF
            {
                X = Convert.ToInt32(Ps.X / GetWmInfo().ScreenFactor - GetWmInfo().MapOrigin.X),
                Y = Convert.ToInt32(Ps.Y / GetWmInfo().ScreenFactor - GetWmInfo().MapOrigin.Y)
            };
            return Pw;
        }
        public PointF WorldToScreenF(Point Pw)
        {
            PointF Ps = new PointF
            {
                X = Convert.ToInt32((Pw.X + GetWmInfo().MapOrigin.X) * GetWmInfo().ScreenFactor),
                Y = Convert.ToInt32((Pw.Y + GetWmInfo().MapOrigin.Y) * GetWmInfo().ScreenFactor)
            };
            return Ps;
        }
    }
}
