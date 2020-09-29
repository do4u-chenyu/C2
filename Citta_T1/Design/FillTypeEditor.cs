using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Citta_T1.Core;
using Citta_T1.ChartControls.FillTypes;

namespace Citta_T1.Design
{
    class FillTypeEditor : ListEditor<string>
    {
        protected override string[] GetStandardValues()
        {
            return FillType.GeneralFillTypes.Keys.ToArray();
        }
    }
}
