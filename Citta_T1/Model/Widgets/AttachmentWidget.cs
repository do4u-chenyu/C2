using C2.Controls.MapViews;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Model.Widgets
{
    class AttachmentWidget : Widget, IRemark
    {
        public const string TypeID = "ATTACHMENT";

        public AttachmentWidget()
        {
            DisplayIndex = 6;
            widgetIcon = Properties.Resources.attachment_w_icon;

        }

        public override Size CalculateSize(MindMapLayoutArgs e)
        {
            return new Size(20, 20);
        }

        public override string GetTypeID()
        {
            return TypeID;
        }
    }
}
