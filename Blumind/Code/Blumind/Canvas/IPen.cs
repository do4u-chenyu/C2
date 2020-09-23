using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Blumind.Canvas
{
    public interface IPen
    {
        object Raw { get; }

        Color Color { get; }

        float Width { get; }
    }
}
