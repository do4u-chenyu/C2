using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Citta_T1.Utils;

namespace Citta_T1.Controls.Title
{
    public partial class DragLineControl : UserControl
    {
        private bool mouseDown;
        private Control bottomViewPanel;
        private Control canvasPanel;
        private Control naviViewControl;
        private Control resetButton;
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
        }

        private void DragLineControl_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            this.canvasPanel = Global.GetCanvasPanel();
            this.flowControl = Global.GetFlowControl();
            this.naviViewControl = Global.GetNaviViewControl();
            this.remarkControl = Global.GetRemarkControl();
            this.resetButton = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), "resetButton");
            this.stopButton = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), "stopButton");
            this.runButton = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), "runButton");
            this.rightShowButton = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), "rightShowButton");
            this.rightHideButton = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), "rightHideButton");
            this.bottomViewPanel = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), "bottomViewPanel");


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
            this.resetButton.Location = new Point(x + 100, y + 50);
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
