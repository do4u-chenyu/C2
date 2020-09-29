using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Citta_T1.Canvas
{
    public interface IPen
    {
        object Raw { get; }

        Color Color { get; }

        float Width { get; }
    }
}
