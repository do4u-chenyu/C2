using System.ComponentModel;
using System.Drawing.Design;
using C2.Design;
using C2.Globalization;

namespace C2.Model.MindMaps
{
    [Editor(typeof(MindMapLayoutTypeEditor), typeof(UITypeEditor))]
    [TypeConverter(typeof(MindMapLayoutTypeConverter))]
    public enum MindMapLayoutType
    {
        [LanguageID("Mind Map Chart")]
        MindMap,
        [LanguageID("Organization Chart (Down)")]
        OrganizationDown,
        [LanguageID("Organization Chart (Up)")]
        OrganizationUp,
        [LanguageID("Tree Chart (Left)")]
        TreeLeft,
        [LanguageID("Tree Chart (Right)")]
        TreeRight,
        [LanguageID("Logic Chart (Left)")]
        LogicLeft,
        [LanguageID("Logic Chart (Right)")]
        LogicRight,
    }
}
