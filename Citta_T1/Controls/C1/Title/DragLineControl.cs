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
        }

        private void DragLineControl_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void DragLineControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (mouseDown && Global.GetBottomViewPanel() != null)
            {
                int panelOffset = Global.GetBottomViewPanel().Height - e.Y;
                if (panelOffset < minHeight)
                    Global.GetBottomViewPanel().Height = minHeight;
                else if (panelOffset > maxHeight)
                    Global.GetBottomViewPanel().Height = maxHeight;
                else
                    Global.GetBottomViewPanel().Height = panelOffset;
            }
                //this.bottomViewPanel.Height = this.bottomViewPanel.Height - e.Y;
            mouseDown = false;

            Global.GetCanvasForm()?.InitializeControlsLocation();
        }
    }
}
