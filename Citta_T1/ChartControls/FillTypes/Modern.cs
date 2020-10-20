using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using C2.Canvas;
using C2.Controls;

namespace C2.ChartControls.FillTypes
{
    class Modern : FillType
    {
        public override IBrush CreateBrush(IGraphics graphics, Color backColor, Rectangle rectangle)
        {
            return graphics.LinearGradientBrush(rectangle,
                PaintHelper.GetLightColor(backColor, 0.1f),
                PaintHelper.GetDarkColor(backColor, 0.1f),
                LinearGradientMode.Vertical);
        }
    }
}
