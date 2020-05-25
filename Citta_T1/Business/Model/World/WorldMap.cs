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
        private float factor;
        public WorldMapInfo()
        {
            mapOrigin = new Point(-600, -300);
            factor = 1;
        }

        public Point MapOrigin { get => mapOrigin; set => mapOrigin = value; }
        public float Factor { get => factor; set => factor = value; }
    }
    class WorldMap
    {
        internal WorldMapInfo WmInfo { get; set; }

        //  Pw = Ps / Factor - Pm
        public Point ScreenToWorld(Point Ps)
        {
            Point Pw = new Point
            {
                X = Convert.ToInt32(Ps.X / WmInfo.Factor - WmInfo.MapOrigin.X),
                Y = Convert.ToInt32(Ps.Y / WmInfo.Factor - WmInfo.MapOrigin.Y)
            };
            return Pw;
        }

        // Ps = (Pw + Pm) * Factor
        public Point WorldToScreen(Point Pw)
        {
            Point Ps = new Point
            {
                X = Convert.ToInt32((Pw.X + WmInfo.MapOrigin.X) * WmInfo.Factor),
                Y = Convert.ToInt32((Pw.Y + WmInfo.MapOrigin.Y) * WmInfo.Factor)
            };
            return Ps;
        }
    }
}
