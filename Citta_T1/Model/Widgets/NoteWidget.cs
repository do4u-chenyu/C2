﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Xml;
using C2.Controls;
using C2.Controls.MapViews;
using C2.Core;
using C2.Dialogs;
using C2.Globalization;
using C2.Model.Documents;
using C2.Model.MindMaps;

namespace C2.Model.Widgets
{
    [DefaultProperty("Text")]
    class NoteWidget : Widget, IRemark
    {
        public const string TypeID = "NOTES";
        Color _BackColor;
        Color _ForeColor;

        public NoteWidget()
        {
            DisplayIndex = 5;
            widgetIcon = Properties.Resources.备注;
        }

        [DefaultValue(WidgetAlignment.Right)]
        public override WidgetAlignment Alignment
        {
            get
            {
                return base.Alignment;
            }
            set
            {
                base.Alignment = value;
            }
        }

        [DefaultValue(typeof(Color), "Empty"), Browsable(false)]
        [DesignOnly(true), LocalDisplayName("Back Color"), LocalCategory("Color")]
        public Color BackColor
        {
            get { return _BackColor; }
            set 
            {
                if (_BackColor != value)
                {
                    Color old = _BackColor;
                    _BackColor = value;
                    OnPropertyChanged("BackColor", old, _BackColor, ChangeTypes.Data | ChangeTypes.Visual);
                }
            }
        }

        [DefaultValue(typeof(Color), "Empty"), Browsable(false)]
        [DesignOnly(true), LocalDisplayName("Fore Color"), LocalCategory("Color")]
        public Color ForeColor
        {
            get { return _ForeColor; }
            set 
            {
                if (_ForeColor != value)
                {
                    Color old = _ForeColor;
                    _ForeColor = value;
                    OnPropertyChanged("ForeColor", old, _ForeColor, ChangeTypes.Data | ChangeTypes.Visual);
                }
            }
        }

        [Browsable(false)]
        public override string Text
        {
            get
            {
                return ST.HtmlToText(Remark);
                //return base.Text;
            }
            set
            {
                if (Text != value)
                {
                    Remark = value;
                }
            }
        }

        public override bool CanCopy
        {
            get
            {
                return !string.IsNullOrEmpty(Text);
                //return base.CanCopy;
            }
        }

        public override bool ResponseMouse
        {
            get
            {
                return true;
            }
        }

        protected override string GetTooltip()
        {
            return ST.HtmlToText(Text);
        }

        public override Size CalculateSize(MindMapLayoutArgs e)
        {
                return new Size(32, 32);
        }

        public override string GetTypeID()
        {
            return TypeID;
        }

        public override void Serialize(XmlDocument dom, XmlElement node)
        {
            base.Serialize(dom, node);

            ST.WriteTextNode(node, "text", Remark);
            if (!BackColor.IsEmpty)
                node.SetAttribute("back_color", ST.ToString(BackColor));
            if (!ForeColor.IsEmpty)
                node.SetAttribute("fore_color", ST.ToString(ForeColor));
        }

        public override void Deserialize(Version documentVersion, XmlElement node)
        {
            base.Deserialize(documentVersion, node);

            if(documentVersion < Document.DV_3) // 向后兼容
                Text = ST.ReadTextNode(node, "description");
            else
                Remark = ST.ReadTextNode(node, "text");

            BackColor = ST.GetColor(node.GetAttribute("back_color"), BackColor);
            ForeColor = ST.GetColor(node.GetAttribute("fore_color"), ForeColor);
        }

        public override void Paint(RenderArgs e)
        {
            if (Text == null) // 修改当备注框内容为图片或者空时,备注挂件图标不显示的bug
                return;

            if (this.Hover)
            {
                PaintHelper.DrawHoverBackground(e.Graphics, Bounds,
                    (Container is Topic) ? ((Topic)Container).RealBackColor : SystemColors.Highlight);
            }
            base.Paint(e);
        }

        public override void CopyTo(Widget widget)
        {
            base.CopyTo(widget);

            if (widget is NoteWidget)
            {
                var nw = (NoteWidget)widget;
                //nw.Text = Text;
                nw.Remark = Remark;
                nw.BackColor = BackColor;
                nw.ForeColor = ForeColor;
            }
        }

        public override IWidgetEditDialog CreateEditDialog()
        {
            return new NoteWidgetDialog();
        }

        public override void Copy()
        {
            //base.Copy();
            if (!string.IsNullOrEmpty(Text))
            {
                Clipboard.SetText(this.Text);
            }
        }

        public override void CopyExtendContent(DataObject dataObject)
        {
            base.CopyExtendContent(dataObject);

            if (Remark != null)
            {
                ClipboardHelper.SetHtml(dataObject, Remark);
            }
        }
    }
}
