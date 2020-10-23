using C2.Controls.MapViews;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace C2.Model.Widgets
{
    class DataSourceWidget : Widget, IRemark
    {
        public const string TypeID = "DATASOURCE";

        public DataSourceWidget()
        {
            DisplayIndex = 0;
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
            Image iconRemark = Properties.Resources.data_w_icon;
            rect.X += Math.Max(0, (rect.Width - iconRemark.Width) / 2);
            rect.Y += Math.Max(0, (rect.Height - iconRemark.Height) / 2);
            rect.Width = Math.Min(rect.Width, iconRemark.Width);
            rect.Height = Math.Min(rect.Height, iconRemark.Height);
            e.Graphics.DrawImage(iconRemark, rect, 0, 0, iconRemark.Width, iconRemark.Height);
        }

    }
}
