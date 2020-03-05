using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Controls.Title
{
    public partial class DragLineControl : UserControl
    {
        private bool mouseDown;
        private Control bottomViewPanel;
        private Control canvasPanel;
        private Control naviViewControl;
        private Control downloadButton;
        private Control stopButton;
        private Control runButton;
        private Control flowControl;
        private Control rightShowButton;
        private Control rightHideButton;
        private Control remarkControl;

        public DragLineControl()
        {
            InitializeComponent();
            this.mouseDown = false;
            this.bottomViewPanel = null;
            this.canvasPanel = null;
        }

        private void DragLineControl_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            this.bottomViewPanel = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), "bottomViewPanel");
            this.canvasPanel = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), "canvasPanel");
            this.naviViewControl = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), "naviViewControl");
            this.downloadButton = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), "downloadButton");
            this.stopButton = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), "stopButton");
            this.runButton = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), "runButton");
            this.flowControl = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), "flowControl");
            this.rightShowButton = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), "rightShowButton");
            this.rightHideButton = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), "rightHideButton");
            this.remarkControl = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), "remarkControl");

        }

        private void DragLineControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown && this.bottomViewPanel != null)
                this.bottomViewPanel.Height = this.bottomViewPanel.Height - e.Y;
        }

        private void DragLineControl_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;

            Point org = new Point(this.canvasPanel.Width, 0);
            Point org2 = new Point(0, this.canvasPanel.Height);
            int x = org.X - 10 - this.naviViewControl.Width;
            int y = org2.Y - 10 - this.naviViewControl.Height;

            // 缩略图定位
            this.naviViewControl.Location = new Point(x, y);

            // 底层工具按钮定位
            x = x - (this.canvasPanel.Width) / 2 + 100;
            this.downloadButton.Location = new Point(x + 100, y + 50);
            this.stopButton.Location = new Point(x + 50, y + 50);
            this.runButton.Location = new Point(x, y + 50);

            // 顶层浮动工具栏和右侧工具及隐藏按钮定位
            Point loc = new Point(org.X - 70 - this.flowControl.Width, org.Y + 50);
            Point loc_flowcontrol2 = new Point(org.X - this.rightShowButton.Width, loc.Y);
            Point loc_flowcontrol3 = new Point(loc_flowcontrol2.X, loc.Y + this.rightHideButton.Width + 10);
            Point loc_panel3 = new Point(loc.X, loc.Y + this.flowControl.Height + 10);
            this.flowControl.Location = loc;
            this.rightShowButton.Location = loc_flowcontrol2;
            this.rightHideButton.Location = loc_flowcontrol3;
            this.remarkControl.Location = loc_panel3;
        }
    }
}
