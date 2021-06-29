﻿using System.ComponentModel;

namespace C2.Globalization
{
    class LocalDisplayNameAttribute : DisplayNameAttribute
    {
        public LocalDisplayNameAttribute()
        {
        }

        public LocalDisplayNameAttribute(string displayName)
            : base(displayName)
        {
        }

        public override string DisplayName
        {
            get
            {
                return LanguageManage.GetText(base.DisplayName);
            }
        }
    }
}
