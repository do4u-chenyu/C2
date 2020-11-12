using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace C2.Model.Widgets
{
    class AttachmentWidget : C2BaseWidget, IRemark
    {
        public const string TypeID = "ATTACHMENT";
        [Browsable(false)]
        public List<string> FullFilePaths { get; set; }

        public override string Description => HelpUtil.AttachmentWidgetHelpInfo;

        public AttachmentWidget()
        {
            DisplayIndex = 4;
            Alignment = WidgetAlignment.Right;//默认位置改成右侧,让图标挂件和主题文字紧挨着
            widgetIcon = Properties.Resources.attachment_w_icon;
        }
        public override string GetTypeID()
        {
            return TypeID;
        }


    }
}
