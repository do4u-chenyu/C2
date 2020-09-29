using System;
using System.Collections.Generic;
using System.Text;
using Citta_T1.Core;

namespace Citta_T1.Model
{
    interface IChartControl
    {
        void OnChartObjectPropertyChanged(ChartObject chartObject, PropertyChangedEventArgs e);
    }
}
