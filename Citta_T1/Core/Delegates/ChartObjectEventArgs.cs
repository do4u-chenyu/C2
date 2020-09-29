using System;
using System.Collections.Generic;
using System.Text;
using Citta_T1.Model;

namespace Citta_T1.Core
{
    public delegate void ChartObjectEventHandler(object sender, ChartObjectEventArgs e);

    public class ChartObjectEventArgs : EventArgs
    {
        public ChartObjectEventArgs(ChartObject chartObject)
        {
            Object = chartObject;
        }

        public ChartObject Object { get; private set; }
    }
}
