using C2.Business.Model;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;

namespace C2.Model.Widgets
{
    class AttachmentWidget : C2BaseWidget, IRemark
    {
        public const string TypeID = "ATTACHMENT";

        [Browsable(false)]
        public List<string> FullFilePaths { get; set; }= new List<string>();
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
        public override void Serialize(XmlDocument dom, XmlElement node)
        {
            base.Serialize(dom, node);
            XmlElement attachItems = node.OwnerDocument.CreateElement("attach_items");
            foreach (string attachPath in FullFilePaths)
            {
                new ModelXmlWriter("attach_item", attachItems).WriteAttribute("path", attachPath);              
            }
            node.AppendChild(attachItems);

        }
        public override void Deserialize(Version documentVersion, XmlElement node)
        {
            base.Deserialize(documentVersion, node);
            string path;
            var attach_items = node.SelectNodes("attach_items/attach_item");
            foreach (XmlElement attach_item in attach_items)
            {
                path = attach_item.GetAttribute("path");
                if (!string.IsNullOrEmpty(path))
                { 
                    FullFilePaths.Add(path);
                }
                  
            }
        }

    }
}
