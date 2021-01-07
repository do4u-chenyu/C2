using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            widgetIcon = Properties.Resources.图表; 
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

        public override void OnDoubleClick(HandledEventArgs e)
        {
            if (DataItems.Count > 0)
                DoViewDataChart(DataItems[0]);
            base.OnDoubleClick(e);
        }

        public static void DoViewDataChart(DataItem dataItem)
        {
            string path = dataItem.FilePath;
            OpUtil.Encoding encoding = dataItem.FileEncoding;
            // 获取选中输入、输出各列数据
            string fileContent;
            if (dataItem.FileType == OpUtil.ExtType.Excel)
                fileContent = BCPBuffer.GetInstance().GetCachePreviewExcelContent(path);
            else if (dataItem.FileType == OpUtil.ExtType.Database)
                fileContent = BCPBuffer.GetInstance().GetCachePreviewOracleTable(dataItem.DBItem);
            else
                fileContent = BCPBuffer.GetInstance().GetCachePreviewBcpContent(path, encoding);
            List<string> rows = new List<string>(fileContent.Split(OpUtil.DefaultLineSeparator));
            // 最多绘制前100行数据
            int upperLimit = Math.Min(rows.Count, 100);
            List<List<string>> columnValues = FileUtil.GetColumns(dataItem.SelectedIndexs, dataItem, rows, upperLimit);
            if (columnValues.IsEmpty())
                return;
            ControlUtil.PaintChart(columnValues, dataItem.SelectedItems, dataItem.ChartType);
        }
    }
}
