using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C2.Model;

namespace C2.Core
{
    public class ChartObjectPropertyEventArgs : EventArgs
    {
        public ChartObjectPropertyEventArgs(ChartObject chartObject, string propertyName, ChangeTypes changeTypes)
        {
            Object = chartObject;
            PropertyName = propertyName;
            Changes = changeTypes;
        }

        public ChartObject Object { get; private set; }

        public string PropertyName { get; private set; }

        public ChangeTypes Changes { get; private set; }
    }

    public delegate void ChartObjectPropertyEventHandler(object sender, ChartObjectPropertyEventArgs e);
}
