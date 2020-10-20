using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using C2.Core;
using C2.ChartControls.FillTypes;

namespace C2.Design
{
    class FillTypeEditor : ListEditor<string>
    {
        protected override string[] GetStandardValues()
        {
            return FillType.GeneralFillTypes.Keys.ToArray();
        }
    }
}
