using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C2.Core;
using C2.Globalization;

namespace C2.Model
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
