using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace C2.Globalization
{
    class LocalCategoryAttribute : CategoryAttribute
    {
        public LocalCategoryAttribute()
        {
        }

        public LocalCategoryAttribute(string category)
            : base(category)
        {
        }

        protected override string GetLocalizedString(string value)
        {
            return LanguageManage.GetText(value);
            //return base.GetLocalizedString(value);
        }
    }
}
