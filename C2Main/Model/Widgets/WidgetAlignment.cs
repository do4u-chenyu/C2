using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text;
using C2.Core;
using C2.Design;
using C2.Globalization;

namespace C2.Model.Widgets
{
    [Serializable]
    [Editor(typeof(EnumEditor<WidgetAlignment>), typeof(UITypeEditor))]
    [TypeConverter(typeof(WidgetAlignmentConverter))]
    public enum WidgetAlignment
    {
        Left,
        Right,
        Top,
        Bottom,
    }

    class WidgetAlignmentConverter : BaseTypeConverter
    {
        protected override object ConvertValueToString(object value)
        {
            if (value is WidgetAlignment)
            {
                switch ((WidgetAlignment)value)
                {
                    case WidgetAlignment.Left:
                        return Lang._("Left");
                    case WidgetAlignment.Top:
                        return Lang._("Top");
                    case WidgetAlignment.Right:
                        return Lang._("Right");
                    case WidgetAlignment.Bottom:
                        return Lang._("Bottom");
                }
            }

            return base.ConvertValueToString(value);
        }
    }
}
