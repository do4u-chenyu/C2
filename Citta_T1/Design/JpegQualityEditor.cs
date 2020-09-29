﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Citta_T1.Controls;
using Citta_T1.Core;
using Citta_T1.Globalization;

namespace Citta_T1.Design
{
    class JpegQualityEditor : ListEditor<int>
    {
        protected override int[] GetStandardValues()
        {
            return new int[] { 10, 30, 60, 80, 100 };
        }

        protected override IEnumerable<ListItem<int>> GetStandardItems()
        {
            return new ListItem<int>[]{
                new ListItem<int>(Lang._("Low"), 10),
                new ListItem<int>(Lang._("Medium"), 30),
                new ListItem<int>(Lang._("High"), 60),
                new ListItem<int>(Lang._("Good"), 80),
                new ListItem<int>(Lang._("Perfect"), 100)
            };
        }
    }
}
