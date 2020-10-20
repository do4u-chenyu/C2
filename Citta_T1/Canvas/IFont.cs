using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace C2.Canvas
{
    public interface IFont
    {
        object Raw { get; }

        FontStyle Style { get; }

        float Size { get; }
    }
}
