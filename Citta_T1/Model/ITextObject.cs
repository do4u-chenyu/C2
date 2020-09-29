using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Citta_T1.Core
{
    public interface ITextObject
    {
        string Text { get; set; }

        Font Font { get; }

        Rectangle Bounds { get; }
    }
}
