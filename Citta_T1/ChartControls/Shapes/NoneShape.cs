using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using C2.Canvas;

namespace C2.ChartControls.Shapes
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
