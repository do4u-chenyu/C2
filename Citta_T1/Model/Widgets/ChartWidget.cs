using C2.Utils;
using System;
using System.Collections.Generic;
using System.Xml;

namespace C2.Model.Widgets
{
    class ChartWidget : C2BaseWidget
    {
        public const string TypeID = "CHART";
        public ChartWidget()
        {
            DisplayIndex = 3;
            widgetIcon = Properties.Resources.chart_w_icon; 
        }
        public override string GetTypeID()
        {
            return TypeID;
        }
        public override void Serialize(XmlDocument dom, XmlElement node)
        {
            base.Serialize(dom, node);
            if (this.DataItems.Count > 0)
            {
                XmlElement dataItemsNode = node.OwnerDocument.CreateElement("chart_items");
                foreach (var dataItem in this.DataItems)
                {
                    var dataNode = node.OwnerDocument.CreateElement("chart_item");
                    dataNode.SetAttribute("chart_type", dataItem.ChartType);
                    dataNode.SetAttribute("selected_indexs", string.Join(",", dataItem.SelectedIndexs));
                    dataNode.SetAttribute("selected_items", string.Join(",", dataItem.SelectedItems));
                    dataNode.SetAttribute("path", dataItem.FilePath);
                    dataNode.SetAttribute("name", dataItem.FileName);
                    dataNode.SetAttribute("separator", dataItem.FileSep.ToString());
                    dataNode.SetAttribute("encoding", dataItem.FileEncoding.ToString());
                    dataNode.SetAttribute("type", dataItem.FileType.ToString());
                    dataItemsNode.AppendChild(dataNode);
                }
                node.AppendChild(dataItemsNode);
            }
        }
        public override void Deserialize(Version documentVersion, XmlElement node)
        {
            base.Deserialize(documentVersion, node);
            var data_items = node.SelectNodes("chart_items/chart_item");
            foreach (XmlElement dataItem in data_items)
            {
                DataItem item = new DataItem(
                   dataItem.GetAttribute("path"),
                   dataItem.GetAttribute("name"),
                   ConvertUtil.TryParseAscii(dataItem.GetAttribute("separator")),
                   OpUtil.EncodingEnum(dataItem.GetAttribute("encoding")),
                   OpUtil.ExtTypeEnum(dataItem.GetAttribute("type")));
                item.ChartType = dataItem.GetAttribute("chart_type");
                item.SelectedIndexs = Utils.ConvertUtil.TryParseIntList(dataItem.GetAttribute("selected_indexs"));
                item.SelectedItems = new List<string>(dataItem.GetAttribute("selected_items").Split(','));
                this.DataItems.Add(item);
            }
        }
    }
}
