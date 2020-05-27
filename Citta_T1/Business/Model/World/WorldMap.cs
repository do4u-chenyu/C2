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
            sizeLevel = 1;
        }

        public Point MapOrigin { get => mapOrigin; set => mapOrigin = value; }
        public float ScreenFactor { get => screenFactor; set => screenFactor = value; }
        public int SizeLevel { get => sizeLevel; set => sizeLevel = value; }
    }
    class WorldMap
    {
        //internal WorldMapInfo WmInfo { get; set; }
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
        public Point ScreenToWorld(Point Ps)
        {
            Point Pw = new Point
            {
                X = Convert.ToInt32(Ps.X / GetWmInfo().ScreenFactor - GetWmInfo().MapOrigin.X),
                Y = Convert.ToInt32(Ps.Y / GetWmInfo().ScreenFactor - GetWmInfo().MapOrigin.Y)
            };
            return Pw;
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
    }
}
