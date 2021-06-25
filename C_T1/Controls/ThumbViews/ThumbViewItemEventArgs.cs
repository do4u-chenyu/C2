﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2.Controls
{
    class ThumbViewItemEventArgs : EventArgs
    {
        public ThumbViewItemEventArgs(ThumbItem item)
        {
            Item = item;
        }

        public ThumbItem Item { get; private set; }
    }

    delegate void ThumbViewItemEventHandler(object sender, ThumbViewItemEventArgs e);
}
