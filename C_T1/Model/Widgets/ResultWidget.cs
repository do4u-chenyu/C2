﻿using C2.Utils;
using System;
using System.ComponentModel;
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
            widgetIcon = Properties.Resources.结果;
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
            var data_items = node.SelectNodes("result_items/result_item");
            ReadAttribute(data_items, this.DataItems);
            
        }

        public override void OnDoubleClick(HandledEventArgs e)
        {
            if (DataItems.Count > 0)
                DoPreViewDataSource(DataItems[0]);
            base.OnDoubleClick(e);
        }
    }
}
