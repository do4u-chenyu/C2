using C2.Utils;
using System;
using System.Xml;

namespace C2.Model.Widgets
{
    class ChartWidget : C2BaseWidget
    {
        public const string TypeID = "CHART";

        public override string Description => HelpUtil.ChartWidgetHelpInfo;
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
            WriteDataItem(node, this.DataItems, "chart_items");          
        }
        public override void Deserialize(Version documentVersion, XmlElement node)
        {
            base.Deserialize(documentVersion, node);
            var data_items = node.SelectNodes("chart_items/chart_item");
            ReadAttribute(data_items, this.DataItems);          
        }
    }
}
