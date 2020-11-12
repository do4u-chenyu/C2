using C2.Controls.MapViews;
using System.Drawing;

namespace C2.Model.Widgets
{
    class AttachmentWidget : C2BaseWidget, IRemark
    {
        public const string TypeID = "ATTACHMENT";

        public AttachmentWidget()
        {
            DisplayIndex = 6;
            Alignment = WidgetAlignment.Right;//默认位置改成右侧,让图标挂件和主题文字紧挨着
            widgetIcon = Properties.Resources.attachment_w_icon;

        }
        public override string GetTypeID()
        {
            return TypeID;
        }
    }
}
