using System.ComponentModel;
using System.Drawing.Design;
using C2.Design;

namespace C2.Model.Styles
{
    [Editor(typeof(TopicShapeEditor), typeof(UITypeEditor))]
    [TypeConverter(typeof(TopicShapeConverter))]
    public enum TopicShape
    {
        Default,
        Rectangle,
        Ellipse,
        BaseLine,
        None,
    }
}
