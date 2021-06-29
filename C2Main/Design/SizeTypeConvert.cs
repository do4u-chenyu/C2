using C2.Controls;
using C2.Globalization;
using C2.Model.Widgets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Design
{
    class SizeTypeConvert : BaseTypeConverter
    {
        protected override object ConvertValueToString(object value)
        {
            if (value is PictureSizeType)
            {
                switch ((PictureSizeType)value)
                {
                    case PictureSizeType.Customize:
                        return Lang._("Customize");
                    case PictureSizeType.Original:
                        return Lang._("Original");
                    case PictureSizeType.Thumb:
                        return Lang._("Thumb");
                }
            }

            return base.ConvertValueToString(value);
        }
    }
    class SizeTypeEditor : EnumEditor<PictureSizeType>
    {
        protected override void DrawListItem(DrawItemEventArgs e, Rectangle rect, ListItem<PictureSizeType> listItem)
        {               
            DrawItemText(e, rect, Lang._(listItem.Value.ToString()));
        }
    }
}
