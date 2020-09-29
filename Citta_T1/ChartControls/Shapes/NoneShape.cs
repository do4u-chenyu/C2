using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Citta_T1.Canvas;

namespace Citta_T1.ChartControls.Shapes
{
    class NoneShape : Shape
    {
        public override void Fill(IGraphics graphics, IBrush brush, Rectangle rect)
        {
        }

        public override void DrawBorder(IGraphics graphics, IPen pen, Rectangle rect)
        {
        }
    }
}
