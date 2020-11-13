using C2.Business.Model;
using C2.Utils;
using System;
using System.Xml;

namespace C2.Model.Widgets
{
    public class DataSourceWidget : C2BaseWidget, IRemark
    {
        public const string TypeID = "DATASOURCE";
        public override string Description => HelpUtil.DataSourceWidgetHelpInfo;
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
            ReadAttribute(data_items, this.DataItems);         
        }

    }
}
