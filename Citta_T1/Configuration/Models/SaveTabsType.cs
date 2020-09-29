using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using Citta_T1.Core;
using Citta_T1.Design;
using Citta_T1.Globalization;

namespace Citta_T1.Configuration
{
    [Serializable]
    [Editor(typeof(EnumEditor<SaveTabsType>), typeof(UITypeEditor))]
    [TypeConverter(typeof(EnumConverter<SaveTabsType>))]
    enum SaveTabsType
    {
        [LanguageID("Confirm")]
        Ask,
        [LanguageID("Yes")]
        Yes,
        [LanguageID("NO")]
        No,
    }
}
