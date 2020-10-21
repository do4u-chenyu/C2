using System;
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
using C2.OperatorViews;

namespace C2.Model.Widgets
{
    [DefaultProperty("Text")]
    class OperatorWidget : Widget, IRemark
    {
        public const string TypeID = "OPERATOR";
        private String operatorName;

        public OperatorWidget(string operatorName)
        {
            Alignment = WidgetAlignment.Left;
            WidgetMenuStrip = new ContextMenuStrip();
            this.operatorName = operatorName;
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
            return new Size(16, 16);
        }

        public override string GetTypeID()
        {
            return TypeID;
        }

        public override void Serialize(XmlDocument dom, XmlElement node)
        {
            base.Serialize(dom, node);
            //TODO
        }

        public override void Deserialize(Version documentVersion, XmlElement node)
        {
            base.Deserialize(documentVersion, node);
            //TODO
        }

        public override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || e.Clicks != 1)
                return;

            //弹出自己的菜单栏
            
            WidgetMenuStrip.SuspendLayout();

            // MenuOpenHyperlink
            ToolStripMenuItem MenuOpenHyperlink = new ToolStripMenuItem();
            ToolStripMenuItem MenuAddIcon = new ToolStripMenuItem();
            ToolStripMenuItem MenuAddProgressBar = new ToolStripMenuItem();
            ToolStripMenuItem MenuAddRemark = new ToolStripMenuItem();
            ToolStripMenuItem MenuAddOperator = new ToolStripMenuItem();

            MenuOpenHyperlink.Image = C2.Properties.Resources.hyperlink;
            MenuOpenHyperlink.Name = operatorName;
            MenuOpenHyperlink.Text = operatorName;
            MenuOpenHyperlink.DropDownItems.AddRange(new ToolStripItem[] {
                MenuAddIcon,
                MenuAddProgressBar,
                MenuAddRemark,
                MenuAddOperator});

            // MenuAddIcon
            MenuAddIcon.Image = C2.Properties.Resources.image;
            MenuAddIcon.Name = "MenuAddIcon";
            MenuAddIcon.Text = "修改";

            // MenuAddProgressBar
            MenuAddProgressBar.Image = C2.Properties.Resources.progress_bar;
            MenuAddProgressBar.Name = "MenuAddProgressBar";
            MenuAddProgressBar.Text = "运行";

            // MenuAddRemark
            MenuAddRemark.Image = C2.Properties.Resources.notes;
            MenuAddRemark.Name = "MenuAddRemark";
            MenuAddRemark.Text = "发布";

            // MenuAddOperator
            MenuAddOperator.Image = C2.Properties.Resources.notes;
            MenuAddOperator.Name = "MenuAddOperator";
            MenuAddOperator.Text = "删除";


            //清空菜单栏，重新生成
            WidgetMenuStrip = new ContextMenuStrip();
            WidgetMenuStrip.Items.Add(MenuOpenHyperlink);

            WidgetMenuStrip.ResumeLayout();

            WidgetMenuStrip.Show(e.X+330,e.Y+110);
        }

        public override void Paint(RenderArgs e)
        {
            //base.Paint(e);
            //if (string.IsNullOrEmpty(Text))
            //    return;

            Rectangle rect = DisplayRectangle;
            Image iconRemark = Properties.Resources.note_small;
            rect.X += Math.Max(0, (rect.Width - iconRemark.Width) / 2);
            rect.Y += Math.Max(0, (rect.Height - iconRemark.Height) / 2);
            rect.Width = Math.Min(rect.Width, iconRemark.Width);
            rect.Height = Math.Min(rect.Height, iconRemark.Height);
            e.Graphics.DrawImage(iconRemark, rect, 0, 0, iconRemark.Width, iconRemark.Height);
        }


        public override IWidgetEditDialog CreateEditDialog()
        {
            return null;
        }

    }
}
