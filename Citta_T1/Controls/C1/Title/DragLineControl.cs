using C2.Core;
using C2.Utils;
using NPOI.SS.Formula.Functions;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace C2.Controls.Title
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
        private Control operatorControl;
        private Control remarkControl;
        private Control currentModelRunBackLab;
        private Control currentModelFinLab;
        private Control progressBar1;
        private Control progressBarLabel;
        private int maxHeight = 500;
        private int minHeight = 100;

        private static LogUtil log = LogUtil.GetInstance("DragLineControl"); // 获取日志模块
        public DragLineControl()
        {
            InitializeComponent();
            this.mouseDown = false;
        }

        private void DragLineControl_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            this.operatorControl = Global.GetOperatorControl();
            this.canvasPanel = Global.GetCanvasPanel();
            this.flowControl = Global.GetFlowControl();
            this.naviViewControl = Global.GetNaviViewControl();
            this.remarkControl = Global.GetRemarkControl();
            this.resetButton = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), "resetButton");
            this.stopButton = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), "stopButton");
            this.rightShowButton = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), "rightShowButton");
            this.rightHideButton = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), "rightHideButton");
            this.bottomViewPanel = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), "bottomViewPanel");
            this.currentModelRunBackLab = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), "currentModelRunBackLab");
            this.currentModelFinLab = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), "currentModelFinLab");
            this.progressBar1 = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), "progressBar1");
            this.progressBarLabel = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), "progressBarLabel");
            //运行按钮在不同状态名字不同  runButton pauseButton  continueButton
            string[] runButtonNameList = { "runButton", "pauseButton", "continueButton" };
            foreach (string buttonName in runButtonNameList)
            {
                if (Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), buttonName) != null)
                {
                    this.runButton = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), buttonName);
                    break;
                }
            }
        }

        private void DragLineControl_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void DragLineControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (mouseDown && this.bottomViewPanel != null)
            {
                int panelOffset = this.bottomViewPanel.Height - e.Y;
                if (panelOffset < minHeight)
                    this.bottomViewPanel.Height = minHeight;
                else if (panelOffset > maxHeight)
                    this.bottomViewPanel.Height = maxHeight;
                else
                    this.bottomViewPanel.Height = panelOffset;
            }
                //this.bottomViewPanel.Height = this.bottomViewPanel.Height - e.Y;
            mouseDown = false;

            int x = this.canvasPanel.Width - 10 - this.naviViewControl.Width;
            int y = this.canvasPanel.Height - 5 - this.naviViewControl.Height;

            // 缩略图定位
            this.naviViewControl.Location = new Point(x, y);
            this.naviViewControl.Invalidate();

            // 底层工具按钮定位
            x = x - (this.canvasPanel.Width) / 2 + 100;
            this.resetButton.Location = new Point(x + 100, y + 50);
            this.stopButton.Location = new Point(x + 50, y + 50);
            this.runButton.Location = new Point(x, y + 50);

            //运行状态动图、进度条定位
            this.currentModelRunBackLab.Location = new Point(x, this.canvasPanel.Height / 2 - 50);
            this.currentModelFinLab.Location = new Point(x, this.canvasPanel.Height / 2 - 50);
            this.progressBar1.Location = new Point(x, this.canvasPanel.Height / 2 + 54);
            this.progressBarLabel.Location = new Point(x + 125, this.canvasPanel.Height / 2 + 50);

            // 算子工具栏、顶层浮动工具栏和右侧工具及隐藏按钮定位
            this.operatorControl.Location = new Point(this.canvasPanel.Width - this.rightShowButton.Width, 50 + this.rightHideButton.Width + 10);
            this.flowControl.Location = new Point(this.canvasPanel.Width - 70 - this.flowControl.Width, 50);
            this.remarkControl.Location = new Point(this.canvasPanel.Width - 70 - this.flowControl.Width, 50 + this.flowControl.Height + 10);
            this.rightShowButton.Location = new Point(this.canvasPanel.Width - this.rightShowButton.Width, 50);
            this.rightHideButton.Location = new Point(this.canvasPanel.Width - this.rightShowButton.Width, 50 + this.rightHideButton.Width + 10);
            
            
        }
    }
}
