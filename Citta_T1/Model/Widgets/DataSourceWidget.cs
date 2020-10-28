using C2.Controls.MapViews;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace C2.Model.Widgets
{
    class DataSourceWidget : Widget, IRemark
    {
        public const string TypeID = "DATASOURCE";
        public List<DataItem> DataItems { get; set; }
        public DataSourceWidget()
        {
            DisplayIndex = 0;
            DataItems = new List<DataItem>();
        }

        public override bool ResponseMouse
        {
            get
            {
                return true;
            }
        }

        public override Size CalculateSize(MindMapLayoutArgs e)
        {
            return new Size(20, 20);
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
                XmlElement dataItemsNode = node.OwnerDocument.CreateElement("data_items");
                foreach (var dataItem in this.DataItems)
                {
                    var dataNode = node.OwnerDocument.CreateElement("data_item");
                    dataNode.SetAttribute("path", dataItem.FilePath);
                    dataNode.SetAttribute("name", dataItem.FileName);
                    dataNode.SetAttribute("separator", dataItem.FileSep.ToString());
                    dataNode.SetAttribute("encoding", dataItem.FileEncoding.ToString());
                    dataNode.SetAttribute("file_type", dataItem.FileType.ToString());
                    dataItemsNode.AppendChild(dataNode);
                }
                node.AppendChild(dataItemsNode);
            }
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

        public override void Paint(RenderArgs e)
        {
            //base.Paint(e);

            Rectangle rect = DisplayRectangle;
            Image iconRemark = Properties.Resources.data_w_icon;
            rect.X += Math.Max(0, (rect.Width - iconRemark.Width) / 2);
            rect.Y += Math.Max(0, (rect.Height - iconRemark.Height) / 2);
            rect.Width = Math.Min(rect.Width, iconRemark.Width);
            rect.Height = Math.Min(rect.Height, iconRemark.Height);
            e.Graphics.DrawImage(iconRemark, rect, 0, 0, iconRemark.Width, iconRemark.Height);
        }

    }
}
