using C2.Business.Option;
using C2.Controls.MapViews;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace C2.Model.Widgets
{
    public enum OpStatus
    {
        Null,
        Ready,
        Done
    }
    public class OperatorWidget : Widget, IRemark
    {
        public const string TypeID = "OPERATOR";

        public OperatorWidget()
        {
            Option = new OperatorOption();
            DisplayIndex = 1;
            Status = OpStatus.Null;

        }

        public string OpName { get; set; }
        public DataItem ResultItem { get; set; }
        public OpStatus Status { get; set; }
        public string OpType { get; set; }
        public DataItem DataSourceItem { get; set; }
        public OperatorOption Option { get; set; }
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
            //TODO
            //文档持久化
        }

        public override void Deserialize(Version documentVersion, XmlElement node)
        {
            base.Deserialize(documentVersion, node);
            //TODO
            //文档持久化
        }

        public override void Paint(RenderArgs e)
        {
            //base.Paint(e);

            Rectangle rect = DisplayRectangle;
            Image iconRemark = Properties.Resources.operator_w_icon;
            rect.X += Math.Max(0, (rect.Width - iconRemark.Width) / 2);
            rect.Y += Math.Max(0, (rect.Height - iconRemark.Height) / 2);
            rect.Width = Math.Min(rect.Width, iconRemark.Width);
            rect.Height = Math.Min(rect.Height, iconRemark.Height);
            e.Graphics.DrawImage(iconRemark, rect, 0, 0, iconRemark.Width, iconRemark.Height);
        }

    }
}
