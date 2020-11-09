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
                    dataNode.SetAttribute("charttype", dataItem.ChartType);
                    dataNode.SetAttribute("selectedindexs", string.Join(",", dataItem.SelectedIndexs));
                    dataNode.SetAttribute("file_path", dataItem.FilePath);
                    dataNode.SetAttribute("file_name", dataItem.FileName);
                    dataNode.SetAttribute("file_separator", dataItem.FileSep.ToString());
                    dataNode.SetAttribute("file_encoding", dataItem.FileEncoding.ToString());
                    dataNode.SetAttribute("file_type", dataItem.FileType.ToString());
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
                   dataItem.GetAttribute("file_path"),
                   dataItem.GetAttribute("file_name"),
                   ConvertUtil.TryParseAscii(dataItem.GetAttribute("file_separator")),
                   OpUtil.EncodingEnum(dataItem.GetAttribute("file_encoding")),
                   OpUtil.ExtTypeEnum(dataItem.GetAttribute("file_type")));
                item.ChartType = dataItem.GetAttribute("charttype");
                item.SelectedIndexs = Utils.ConvertUtil.TryParseIntList(dataItem.GetAttribute("selectedindexs"));
                this.DataItems.Add(item);
            }
        }
    }
}
