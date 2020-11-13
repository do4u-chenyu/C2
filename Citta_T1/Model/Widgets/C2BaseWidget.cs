using C2.Controls.MapViews;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using C2.Business.Model;

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
                WriteAttribute(dataItemsNode, dataItem, nodeName.TrimEnd('s'));
            }
            node.AppendChild(dataItemsNode);

        }
        protected void WriteAttribute(XmlElement parentNode, DataItem dataItem, string nodeName)
        {
            ModelXmlWriter mexw = new ModelXmlWriter(nodeName, parentNode);
            mexw.WriteAttribute("path", dataItem.FilePath)
                .WriteAttribute("name", dataItem.FileName)
                .WriteAttribute("separator", dataItem.FileSep.ToString())
                .WriteAttribute("encoding", dataItem.FileEncoding)
                .WriteAttribute("file_type", dataItem.FileType);
        }
        #endregion
    }
}
