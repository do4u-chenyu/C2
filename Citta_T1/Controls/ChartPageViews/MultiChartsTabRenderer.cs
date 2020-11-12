using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using C2.Controls;

namespace C2.ChartPageView
{
    class MultiChartsTabRenderer : NormalTabBarRenderer
    {
        public MultiChartsTabRenderer(TabBar bar)
            : base(bar)
        {
        }

        protected override void DrawItemBorder(TabItemPaintEventArgs e, Rectangle rect, Color color)
        {
            if (e.Selected)
            {
                base.DrawItemBorder(e, rect, color);
            }
        }
    }
}
