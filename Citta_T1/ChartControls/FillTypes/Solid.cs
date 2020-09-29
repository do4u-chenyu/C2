using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Citta_T1.Canvas;

namespace Citta_T1.ChartControls.FillTypes
{
    class Solid : FillType
    {
        public override IBrush CreateBrush(IGraphics graphics, Color backColor, Rectangle rectangle)
        {
            return graphics.SolidBrush(backColor);
        }
    }
}
