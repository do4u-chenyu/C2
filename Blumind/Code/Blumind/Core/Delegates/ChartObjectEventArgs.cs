using System;
using System.Collections.Generic;
using System.Text;
using Blumind.Model;

namespace Blumind.Core
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
