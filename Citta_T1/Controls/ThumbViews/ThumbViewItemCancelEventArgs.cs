﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Citta_T1.Controls
{
    class ThumbViewItemCancelEventArgs : CancelEventArgs
    {
        public ThumbViewItemCancelEventArgs(ThumbItem item)
        {
            Item = item;
        }

        public ThumbItem Item { get; private set; }
    }

    delegate void ThumbViewItemCancelEventHandler(object sender, ThumbViewItemCancelEventArgs e);
}
