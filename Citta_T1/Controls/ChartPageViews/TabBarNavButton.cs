using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Citta_T1.Controls;

namespace Citta_T1.ChartPageView
{
    class TabBarNavButton : TabBarButton
    {
        public TabBarNavButton(string text, Image icon)
            : base(text, icon)
        {
            CustomSize = 13;
            CustomSpace = 1;
        }

        //protected override void OnTextChanged()
        //{
        //    base.OnTextChanged();

        //    ToolTipText = Text;
        //}
    }
}
