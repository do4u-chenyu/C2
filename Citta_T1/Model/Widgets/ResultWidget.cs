using C2.Utils;
using System;
using System.Xml;

namespace C2.Model.Widgets
{
    class ResultWidget : C2BaseWidget, IRemark
    {
        public const string TypeID = "RESULT";
        public override string Description => HelpUtil.ResultWidgetHelpInfo;
        public ResultWidget()
        {
            DisplayIndex = 2;
            widgetIcon = Properties.Resources.result_w_icon;
        }
        public override string GetTypeID()
        {
            return TypeID;
        }

        public override void Serialize(XmlDocument dom, XmlElement node)
        {
            base.Serialize(dom, node);
            WriteDataItem(node, this.DataItems, "result_items");
            
        }

        public override void Deserialize(Version documentVersion, XmlElement node)
        {
            base.Deserialize(documentVersion, node);
            //TODO
            //文档持久化
            var data_items = node.SelectNodes("result_items/result_item");
            foreach (XmlElement dataItem in data_items)
            {
                DataItem item = new DataItem(
                   dataItem.GetAttribute("path"),
                   dataItem.GetAttribute("name"),
                   ConvertUtil.TryParseAscii(dataItem.GetAttribute("separator")),
                   OpUtil.EncodingEnum(dataItem.GetAttribute("encoding")),
                   OpUtil.ExtTypeEnum(dataItem.GetAttribute("file_type")));
                this.DataItems.Add(item);
            }
        }
    }
}
