using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace C2.Model.Widgets
{
    class MapWidget : C2BaseWidget, IRemark
    {
        public const string TypeID = "MAP";

        [Browsable(false)]
        public string WebUrl { set; get; }

        public MapWidget()
        {
            DisplayIndex = 9;
            Alignment = WidgetAlignment.Right;//默认位置改成右侧,让图标挂件和主题文字紧挨着
            widgetIcon = Properties.Resources.地图;
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
            ReadAttribute(data_items, this.DataItems);
        }

    }
}
