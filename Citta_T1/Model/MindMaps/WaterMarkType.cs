using C2.Design;
using C2.Globalization;
using System.ComponentModel;
using System.Drawing.Design;

namespace C2.Model.MindMaps
{
    [Editor(typeof(EnumEditor<WaterMarkType>), typeof(UITypeEditor))]
    [TypeConverter(typeof(WaterMarkTypeConverter))]
    public enum WaterMarkType
    {
        [LanguageID("Default")]
        Default = 0,     // 默认, 居中躺平, 倾斜12度
       
        [LanguageID("Flat")]
        Flat = 1,        // 全屏平铺
       
        [LanguageID("Rain")]
        Rain = 2,        // 全屏倾斜
    }
}
