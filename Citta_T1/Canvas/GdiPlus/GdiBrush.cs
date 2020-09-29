using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Citta_T1.Canvas.GdiPlus
{
    class GdiBrush : IBrush
    {
        public Brush Brush { get; private set; }

        public GdiBrush(Brush brush)
        {
            Brush = brush;
        }

        public object Raw
        {
            get { return Brush; }
        }
    }
}
