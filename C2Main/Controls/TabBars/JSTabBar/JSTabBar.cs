using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Linq;
using C2.Core;
using C2.Model;
using C2.Globalization;
using C2.Forms;

namespace C2.Controls
{
    [DefaultEvent("SelectedItemChanged")]
    public partial class JSTabBar : TabBar
    {
       
        public JSTabBar()
        {
            
        }
        public override int setX(TabItem ti)
        {
            return ti.Size.Width + ItemSpace + 30; //调整各个Tabitem之间的距离
        }



    }
}
