using C2.Core;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace C2.Controls.Left
{
    public partial class IAOModelControl : UserControl
    {
        public IAOModelControl()
        {
            InitializeComponent();
            startPoint = new Point(ButtonLeftX, -ButtonBottomOffsetY);
            startPoint.Y -= 12;
        }

  

        private static readonly int ButtonLeftX = 18;
        private static readonly int ButtonBottomOffsetY = 23;
        private Point startPoint;
        public void AddMindMapModel(string modelName)
        {
            MindMapModelButton mb = new MindMapModelButton(modelName);
            // 获得当前要添加的model button的初始位置
            LayoutModelButtonLocation(mb);
            //this.Controls.Add(mb);
            this.IaoModelPanel.Controls.Add(mb);
        }

        private void LayoutModelButtonLocation(Control ct)
        {
            if (this.IaoModelPanel.Controls.Count > 0)
            {
                this.startPoint = this.IaoModelPanel.Controls[this.IaoModelPanel.Controls.Count - 1].Location;
            }
            this.startPoint.Y += ct.Height + ButtonBottomOffsetY;
            ct.Location = this.startPoint;
        }

        public bool ContainModel(string modelTitle)
        {
            foreach (Control ct in this.IaoModelPanel.Controls)
            {
                if ((ct is MindMapModelButton) && (ct as MindMapModelButton).ModelTitle == modelTitle)
                    return true;
            }
            return false;
        }
        // 文档关闭后, 菜单栏可以打开,删除,重命名
        public void EnableClosedDocumentMenu(string modelTitle)
        {
            foreach (ModelButton mb in this.Controls)
                if (mb.ModelTitle == modelTitle)
                {
                    mb.EnableOpenDocumentMenu();
                    mb.EnableDeleteDocumentMenu();
                    mb.EnableRenameDocumentMenu();
                }

        }

        public void RemoveModelButton(MindMapModelButton modelButton)
        {
            // panel左上角坐标随着滑动条改变而改变，以下就是将panel左上角坐标校验
            if (this.IaoModelPanel.Controls.Count > 0)
                this.startPoint.Y = this.IaoModelPanel.Controls[0].Location.Y - this.IaoModelPanel.Controls[0].Height - ButtonBottomOffsetY;

            this.IaoModelPanel.Controls.Remove(modelButton);
            // 重新布局
            ReLayoutLocalFrame();
        }

        private void ReLayoutLocalFrame()
        {
            // 先暂停布局,然后调整button位置,最后恢复布局,可以避免闪烁
            this.IaoModelPanel.SuspendLayout();
            // 清空位置
            List<Control> tmp = new List<Control>();
            foreach (Control ct in this.IaoModelPanel.Controls)
                tmp.Add(ct);

            this.IaoModelPanel.Controls.Clear();
            // 重新排序
            foreach (Control ct in tmp)
            {
                LayoutModelButtonLocation(ct);
                this.Controls.Add(ct);
            }


            this.IaoModelPanel.ResumeLayout(false);
            this.IaoModelPanel.PerformLayout();
        }

        private void IAOModelControl_MouseDown(object sender, MouseEventArgs e)
        {
            // 强制编辑控件失去焦点,触发重命名控件的Leave事件 
            Global.GetMainForm().BlankButtonFocus();
        }

        private void IAOModelControl_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.FromArgb(195, 195, 195), 1)
            {
                DashStyle = System.Drawing.Drawing2D.DashStyle.Custom,
                DashPattern = new float[] { 4, 4 }　 //画笔 1宽度.
            }; 
            g.DrawLine(p, 0, 30, 200, 30);//x1,y1,x2,y2
        }     
    }
}
