using C2.Controls.MapViews;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using C2.Business.Model;
using C2.Utils;
using C2.Core;
using System;

namespace C2.Model.Widgets
{
    public abstract class C2BaseWidget : Widget
    {
        [Browsable(false)]
        public virtual string Description { get;}

        [Browsable(false)]
        public List<DataItem> DataItems { get; set; } = new List<DataItem>();

        public override bool ResponseMouse { get => true; }

        public override Size CalculateSize(MindMapLayoutArgs e)
        {
            return new Size(20, 20);
        }
        #region 挂件 读/写
        protected void WriteDataItem(XmlElement node, List<DataItem> dataItems,string nodeName)
        {
            if (this.DataItems.Count < 0)
                return;
            XmlElement dataItemsNode = node.OwnerDocument.CreateElement(nodeName);
            foreach (var dataItem in dataItems)
            {
                if (dataItem.DataType == DatabaseType.Null)
                    WriteAttribute(dataItemsNode, dataItem, nodeName.TrimEnd('s'));
                else
                    WriteExternalDataSource(dataItemsNode, dataItem);
            }
            node.AppendChild(dataItemsNode);

        }
        protected void WriteExternalDataSource(XmlElement parentNode, DataItem dataItem)
        {
            ModelXmlWriter mexw0 = new ModelXmlWriter("data_item", parentNode);
            mexw0.WriteAttribute("path", dataItem.FilePath)
                 .WriteAttribute("name", dataItem.FileName)
                 .WriteAttribute("separator", Convert.ToInt32(dataItem.FileSep).ToString())
                 .WriteAttribute("data_type", dataItem.DataType)
                 .WriteAttribute("file_type", dataItem.FileType);
            ModelXmlWriter mexw1 = new ModelXmlWriter("DB_item", mexw0.Element);
            mexw1.WriteAttribute("allInfo", dataItem.DBItem.AllDatabaseInfo)
                 .WriteAttribute("group", dataItem.DBItem.Group)
                 .WriteAttribute("password", dataItem.DBItem.Password)
                 .WriteAttribute("port", dataItem.DBItem.Port)
                 .WriteAttribute("pretty_info", dataItem.DBItem.PrettyDatabaseInfo)
                 .WriteAttribute("SID", dataItem.DBItem.SID)
                 .WriteAttribute("server", dataItem.DBItem.Server)
                 .WriteAttribute("service", dataItem.DBItem.Service)
                 .WriteAttribute("type", dataItem.DBItem.Type)
                 .WriteAttribute("user", dataItem.DBItem.User);
            ModelXmlWriter mexw2 = new ModelXmlWriter("table", mexw1.Element);
            mexw2.WriteAttribute("name", dataItem.DBItem.DataTable.Name)
                 .WriteAttribute("user_name", dataItem.DBItem.DataTable.UserName)
                 .WriteAttribute("view", dataItem.DBItem.DataTable.View.ToString());
        }
        protected void WriteAttribute(XmlElement parentNode, DataItem dataItem, string nodeName)
        {
            ModelXmlWriter mexw = new ModelXmlWriter(nodeName, parentNode);
            mexw.WriteAttribute("path", dataItem.FilePath)
                .WriteAttribute("name", dataItem.FileName)
                .WriteAttribute("separator", Convert.ToInt32(dataItem.FileSep).ToString())
                .WriteAttribute("encoding", dataItem.FileEncoding)
                .WriteAttribute("file_type", dataItem.FileType);
            // 结果算子写入类型
            if (dataItem.ResultDataType != DataItem.ResultType.Null)
            {
                mexw.WriteAttribute("result_type", dataItem.ResultDataType);
            }
            // 图标挂件属性写入
            if (!string.IsNullOrEmpty(dataItem.ChartType))
            {
                mexw.WriteAttribute("chart_type", dataItem.ChartType)
                    .WriteAttribute("selected_indexs", string.Join(",", dataItem.SelectedIndexs))
                    .WriteAttribute("selected_items", string.Join(",", dataItem.SelectedItems));
            }
        }
        protected void ReadAttribute(XmlNodeList data_items,List<DataItem> DataItems)
        {
            foreach (XmlElement dataItem in data_items)
            {
                DataItem item = new DataItem(
                   dataItem.GetAttribute("path"),
                   dataItem.GetAttribute("name"),
                   ConvertUtil.TryParseAscii(dataItem.GetAttribute("separator")),
                   OpUtil.EncodingEnum(dataItem.GetAttribute("encoding")),
                   OpUtil.ExtTypeEnum(dataItem.GetAttribute("file_type")));
                // 结果挂件类型读取
                string resultType = dataItem.GetAttribute("result_type");
                if (!string.IsNullOrEmpty(resultType))
                {
                    item.ResultDataType = OpUtil.ResultTypeEnum(resultType);
                }
                // 图表挂件属性读取
                item.ChartType = dataItem.GetAttribute("chart_type");
                if (!string.IsNullOrEmpty(item.ChartType))
                {
                    item.SelectedIndexs = Utils.ConvertUtil.TryParseIntList(dataItem.GetAttribute("selected_indexs"));
                    item.SelectedItems = new List<string>(dataItem.GetAttribute("selected_items").Split(','));
                }                               
                DataItems.Add(item);
            }
        }
        #endregion
        public static void DoPreViewDataSource(DataItem hitItem)
        {
            if (hitItem != null)
                Global.GetMainForm().PreViewDataSource(hitItem);
        }
    }
}
