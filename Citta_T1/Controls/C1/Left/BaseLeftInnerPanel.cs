using C2.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace C2.Controls.C1.Left
{
    public partial class BaseLeftInnerPanel : UserControl
    {
        private static readonly int ButtonGapY = 50;//上下间隔
        private static readonly int ButtonLeftX = 18;
        private static readonly int ButtonBottomOffsetY = 23;
        private Point startPoint;

        public BaseLeftInnerPanel()
        {
            InitializeComponent();
            startPoint = new Point(ButtonLeftX, -ButtonGapY);
        }

        public void AddInnerButton(Control innerButton)
        {
            // 获得当前要添加的model button的初始位置
            LayoutButton(innerButton);
            this.manageButtonPanel.Controls.Add(innerButton);
        }

        private void LayoutButton(Control ct)
        {
            if (this.manageButtonPanel.Controls.Count > 0)
            {
                this.startPoint = this.manageButtonPanel.Controls[this.manageButtonPanel.Controls.Count - 1].Location;
            }
            else
            {
                this.manageButtonPanel.VerticalScroll.Value = 0;
                startPoint = new Point(ButtonLeftX, -ButtonBottomOffsetY);
            }
            this.startPoint.Y += ButtonGapY;
            ct.Location = this.startPoint;
        }

        public bool ContainButton(string modelTitle)
        {
            foreach (BaseLeftInnerButton ct in this.manageButtonPanel.Controls)
            {
                if(ct.ButtonText == modelTitle)
                    return true;
            }
            return false;
        }

        public void RemoveButton(Control innerButton)
        {
            // panel左上角坐标随着滑动条改变而改变，以下就是将panel左上角坐标校验
            if (this.manageButtonPanel.Controls.Count > 0)
                this.startPoint.Y = this.manageButtonPanel.Controls[0].Location.Y - this.manageButtonPanel.Controls[0].Height - ButtonBottomOffsetY;

            
            // 先暂停布局,然后调整button位置,最后恢复布局,可以避免闪烁
            using (new GuarderUtil.LayoutGuarder(manageButtonPanel))
            {
                ReLayoutButtons(innerButton); // 重新布局
                this.manageButtonPanel.Controls.Remove(innerButton); // 删除控件
            }
        }

        private void ReLayoutButtons(Control innerButton)
        {
            int idx = this.manageButtonPanel.Controls.IndexOf(innerButton);
            for (int i = idx + 1; i < this.manageButtonPanel.Controls.Count; i++)
            {
                Control ct = this.manageButtonPanel.Controls[i];
                ct.Location = new Point(ct.Location.X, ct.Location.Y - ButtonGapY);
            }
        }

        private void BaseLeftInnerPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.FromArgb(195, 195, 195), 1);  //画笔 1宽度.
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            p.DashPattern = new float[] { 4, 4 };
            g.DrawLine(p, 0, 30, 200, 30);//x1,y1,x2,y2
        }

    }
}
