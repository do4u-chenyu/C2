using C2.Controls.C1.Left;
using C2.Core;
using C2.Utils;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace C2.Controls.Left
{
    public partial class ManualControl : UserControl
    {
        public ManualControl()
        {
            InitializeComponent();
            InitializeOther();
        }

        private void InitializeOther()
        {
            startPoint = new Point(ButtonLeftX, -ButtonBottomOffsetY);
            startPoint.Y -= 12;
        }

        private static readonly int ButtonLeftX = 18;
        private static readonly int ButtonBottomOffsetY = 23;
        private static readonly int ButtonGapY = 50;
        private Point startPoint;
        public virtual void AddButton(string modelName)
        {
            RemoveLabel();

            ManualButton mb = new ManualButton(modelName);
            // 获得当前要添加的model button的初始位置
            LayoutButton(mb);
            this.PaintPanel.Controls.Add(mb);
        }

        public List<ManualButton> ManualButtons
        {
            get
            {
                List<ManualButton> buttons = new List<ManualButton>();
                foreach (Control ct in PaintPanel.Controls)
                {
                    if (ct is ManualButton)
                        buttons.Add(ct as ManualButton);
                }
                return buttons;
            }
        }

        private void RemoveLabel()
        {
            this.PaintPanel.Controls.Remove(this.textBox1);
        }

        protected void LayoutButton(Control ct)
        {
            if (this.PaintPanel.Controls.Count > 0)
            {
                this.startPoint = this.PaintPanel.Controls[this.PaintPanel.Controls.Count - 1].Location;
            }
            else
            {
                this.PaintPanel.VerticalScroll.Value = 0;
                startPoint = new Point(ButtonLeftX, -ButtonBottomOffsetY);
            }
            this.startPoint.Y += ButtonGapY;
            ct.Location = this.startPoint;
        }

        public bool ContainModel(string modelTitle)
        {
            foreach (Control ct in this.PaintPanel.Controls)
            {
                if ((ct is C2Button) && (ct as C2Button).ModelTitle == modelTitle)
                    return true;
            }
            return false;
        }

        private string FindFFP(string modelTitle)
        {
            foreach (Control ct in this.PaintPanel.Controls)
            {
                if ((ct is C2Button) && (ct as C2Button).ModelTitle == modelTitle)
                    return (ct as C2Button).FullFilePath;
            }
            return string.Empty;
        }

        public bool TryOpen(string modelTitle)
        {
            if (!ContainModel(modelTitle))
                return false;
            string ffp = FindFFP(modelTitle);
            Global.GetMainForm().OpenManualDocument(ffp);
            return true;
        }

        public void RemoveButton(UserControl button)
        {
            // panel左上角坐标随着滑动条改变而改变，以下就是将panel左上角坐标校验
            if (this.PaintPanel.Controls.Count > 0)
                this.startPoint.Y = this.PaintPanel.Controls[0].Location.Y - this.PaintPanel.Controls[0].Height - ButtonBottomOffsetY;

            int idx = this.PaintPanel.Controls.IndexOf(button);
            // 先暂停布局,然后调整button位置,最后恢复布局,可以避免闪烁
            using (new GuarderUtil.LayoutGuarder(PaintPanel))
            {
                RelayoutButtons(idx); // 重新布局
                this.PaintPanel.Controls.Remove(button); // 删除控件
            }
        }

        private void RelayoutButtons(int index)
        {
            for (int i = index + 1; i < this.PaintPanel.Controls.Count; i++)
            {
                Control ct = this.PaintPanel.Controls[i];
                ct.Location = new Point(ct.Location.X, ct.Location.Y - ButtonGapY);
            }
        }

        private void ManualControl_MouseDown(object sender, MouseEventArgs e)
        {
            // 强制编辑控件失去焦点,触发重命名控件的Leave事件 
            Global.GetMainForm().BlankButtonFocus();
        }

        private void ManualControl_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.FromArgb(195, 195, 195), 1)
            {
                DashStyle = System.Drawing.Drawing2D.DashStyle.Custom,
                DashPattern = new float[] { 4, 4 }
            };  //画笔 1宽度.
            g.DrawLine(p, 0, 30, 200, 30);//x1,y1,x2,y2

        }
    }
}
