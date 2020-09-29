﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PdfSharp.Drawing;

namespace Citta_T1.Canvas.Pdf
{
    class PdfBrush : IBrush
    {
        XBrush Brush;

        public PdfBrush(XBrush brush)
        {
            Brush = brush;
        }

        public object Raw
        {
            get { return Brush; }
        }
    }
}
