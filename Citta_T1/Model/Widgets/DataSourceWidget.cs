using C2.Business.Model;
using C2.Utils;
using System;
using System.Xml;

namespace C2.Model.Widgets
{
    public class DataSourceWidget : C2BaseWidget, IRemark
    {
        public const string TypeID = "DATASOURCE";
        public DataSourceWidget()
        {
            DisplayIndex = 0;
            widgetIcon = Properties.Resources.data_w_icon;
        }
        public override string GetTypeID()
        {
            return TypeID;
        }

        public override void Serialize(XmlDocument dom, XmlElement node)
        {
            base.Serialize(dom, node);           
            WriteDataItem(node, this.DataItems, "data_items");
        }

        public override void Deserialize(Version documentVersion, XmlElement node)
        {
            base.Deserialize(documentVersion, node);
            var data_items = node.SelectNodes("data_items/data_item");
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
