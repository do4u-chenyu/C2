using C2.Controls.MapViews;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace C2.Model.Widgets
{
    class ResultWidget : Widget, IRemark
    {
        public const string TypeID = "RESULT";

        public ResultWidget()
        {
            DisplayIndex = 2;
            widgetIcon = Properties.Resources.result_w_icon;
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



    }
}
