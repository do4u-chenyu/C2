using C2.Business.Model;
using C2.Utils;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;

namespace C2.Model.Widgets
{
    class VedioWidget : C2BaseWidget, IRemark
    {
        public const string TypeID = "VEDIO";

        [Browsable(false)]
        public string VedioFullFilePath { get; set; }

        private string VedioFileName { get => Path.GetFileName(VedioFullFilePath); }

        public override string Description { get => string.Format(HelpUtil.VedioWidgetHelpInfo, VedioFileName); }

        public VedioWidget()
        {
            DisplayIndex = 11;
            Alignment = WidgetAlignment.Left;
            widgetIcon = Properties.Resources.附件;
        }

        public override string GetTypeID()
        {
            return TypeID;
        }

        public override void Serialize(XmlDocument dom, XmlElement node)
        {
            base.Serialize(dom, node);
            XmlElement vedioItems = node.OwnerDocument.CreateElement("vedio_items");
            new ModelXmlWriter("vedio_item", vedioItems).WriteAttribute("path", VedioFullFilePath);
            node.AppendChild(vedioItems);
        }

        public override void Deserialize(Version documentVersion, XmlElement node)
        {
            base.Deserialize(documentVersion, node);
            var attach_items = node.SelectNodes("vedio_items/vedio_item");
            foreach (XmlElement attach_item in attach_items)
            {
                string path = attach_item.GetAttribute("path");
                if (!string.IsNullOrEmpty(path))
                    VedioFullFilePath = path;       // 当前视频挂件只有一个
            }
        }

        public override void OnDoubleClick(HandledEventArgs e)
        {
            DoOpenVedio(VedioFullFilePath);
            base.OnDoubleClick(e);
        }

        public static void DoOpenVedio(string ffp)
        {

        }
    }


}
