using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Citta_T1.Core;
using Citta_T1.Globalization;

namespace Citta_T1.Model
{
    [Serializable]
    enum PageOrientation
    {
        [LanguageID("Portrait")]
        Portrait = 0,

        [LanguageID("Landscape")]
        Landscape = 1,
    }
}
