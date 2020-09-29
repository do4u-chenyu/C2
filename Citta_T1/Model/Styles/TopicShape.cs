using System.ComponentModel;
using System.Drawing.Design;
using Citta_T1.Design;

namespace Citta_T1.Model.Styles
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
