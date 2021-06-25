﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using C2.Core;
using C2.Design;
using C2.Globalization;

namespace C2.Configuration
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
