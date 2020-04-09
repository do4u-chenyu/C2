using System;
using System.Drawing;
using System.Windows.Forms;

using Citta_T1.Utils;

namespace Citta_T1.Controls.Flow
{
    public partial class FlowControl : UserControl
    {
        private bool selectRemark;
        private bool selectFrame;
        private bool selectDrag;
        public bool SelectRemark { get => selectRemark; set => selectRemark = value; }
        public bool SelectDrag { get => selectDrag; set => selectDrag = value; }
        public bool SelectFrame { get => selectFrame; set => selectFrame = value; }

        public FlowControl()
        {
            InitializeComponent();
            SelectDrag = false;
            SelectFrame = false;
            SelectRemark = true;
        }

        #region 拖动
        private void ChangeCursor()
        {
            if (SelectDrag)
            {
                this.Parent.Cursor = Cursors.Cross;
            }
            else if(SelectFrame)
            {
                this.Parent.Cursor = Cursors.Default;
            }
            else
            {
                this.Parent.Cursor = Cursors.Hand;
            }
        }
        private void PictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox1.BackColor = Color.FromArgb(135, 135, 135);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // 1. 点击之后图标变色
            // 2. 鼠标变成手的图标
            // 3. 画布中触发MouseDown MouseMove MouseUp动作
            SelectDrag = !SelectDrag;
            
            SelectFrame = false;
            SelectRemark = false;
            ChangeCursor();
        }
        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox1.BackColor = Color.FromArgb(235, 235, 235);
        }
        #endregion

        #region 放大缩小
        private void PictureBox2_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox2.BackColor = Color.FromArgb(135, 135, 135);
        }

        private void PictureBox2_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox2.BackColor = Color.FromArgb(235, 235, 235);
        }

        private void PictureBox3_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox3.BackColor = Color.FromArgb(135, 135, 135);
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox3.BackColor = Color.FromArgb(235, 235, 235);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ((CanvasPanel)this.Parent).ChangSize(true);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ((CanvasPanel)this.Parent).ChangSize(false);
        }
        #endregion

        #region 备注
        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox4.BackColor = Color.FromArgb(135, 135, 135);
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox4.BackColor = Color.FromArgb(235, 235, 235);
        }

        private void HideRemarkControl()//单击备注按钮，备注出现和隐藏功能
        {
            Global.GetRemarkControl().Visible = false;
        }

        private void ShowRemarkControl()//单击备注按钮，备注出现和隐藏功能
        {
            Global.GetRemarkControl().Visible = true;
        }

        private void pictureBox4_Click(object sender, EventArgs e)//单击备注按钮，备注出现和隐藏功能
        {
            if (SelectRemark)
                ShowRemarkControl();
            else
                HideRemarkControl();

            SelectRemark = !SelectRemark;
            
            SelectDrag = false;
            SelectFrame = false;
            ChangeCursor();
        }
        #endregion

        #region 框选
        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox5.BackColor = Color.FromArgb(0, 0, 0);
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox5.BackColor = Color.FromArgb(235, 235, 235);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            SelectFrame = !SelectFrame;
            
            SelectDrag = false;
            SelectRemark = false;
            ChangeCursor();
        }
        #endregion
    }
}
