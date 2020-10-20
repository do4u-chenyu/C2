using System;
using System.Collections.Generic;
using System.Text;
using C2.Model;

namespace C2.Core
{
    delegate void ChartObjectChangeEventHandler(object sender, ChartObjectChangeEventArgs e);

    class ChartObjectChangeEventArgs : EventArgs
    {
        public ChartObjectChangeEventArgs(ChartObject chartObject)
        {
            Object = chartObject;
        }

        public ChartObjectChangeEventArgs(ChartObject chartObject, ChangeTypes changes)
        {
            Object = chartObject;
            Changes = changes;
        }

        public ChartObject Object { get; private set; }

        public ChangeTypes Changes { get; private set; }
    }
}
