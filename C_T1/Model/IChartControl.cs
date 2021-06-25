using System;
using System.Collections.Generic;
using System.Text;
using C2.Core;

namespace C2.Model
{
    interface IChartControl
    {
        void OnChartObjectPropertyChanged(ChartObject chartObject, PropertyChangedEventArgs e);
    }
}
