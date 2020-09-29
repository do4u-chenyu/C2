using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Citta_T1.Canvas
{
    public interface IFont
    {
        object Raw { get; }

        FontStyle Style { get; }

        float Size { get; }
    }
}
