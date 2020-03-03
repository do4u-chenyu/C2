using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Controls
{
    public partial class FlowControl : UserControl
    {
        public bool tmpTag;
        public bool selectFrame;
        public bool isClick = false;
        //public bool TmpTag { get => tmpTag; set => tmpTag }
        public FlowControl()
        {
            InitializeComponent();
            selectFrame = false;
            tmpTag = true;
        }

        private void PictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox1.BackColor = Color.FromArgb(135, 135, 135);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (!isClick)
            {
                this.pictureBox1.BackColor = Color.FromArgb(235, 235, 235);
            }   
        }

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

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox4.BackColor = Color.FromArgb(135, 135, 135);
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox4.BackColor = Color.FromArgb(235, 235, 235);
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox5.BackColor = Color.FromArgb(0, 0, 0);
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox5.BackColor = Color.FromArgb(235, 235, 235);
        }
        private void HideFlowControl()//单击备注按钮，备注出现和隐藏功能
        {
            foreach (Control ct in this.Parent.Controls)
            {
                if (ct.Name == "remarkControl")
                    ct.Visible = false;
            }
        }

        private void ShowFlowControl()//单击备注按钮，备注出现和隐藏功能
        {
            foreach (Control ct in this.Parent.Controls)
            {
                if (ct.Name == "remarkControl")
                    ct.Visible = true;
            }

        }

        
        private void pictureBox4_Click(object sender, EventArgs e)//单击备注按钮，备注出现和隐藏功能
        {
            if (tmpTag)
                ShowFlowControl();
            else
                HideFlowControl();

            tmpTag = !tmpTag;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            selectFrame = !selectFrame;
            
        }

        #region 放大与缩小
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ((CanvasPanel)this.Parent).ChangSize(true);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ((CanvasPanel)this.Parent).ChangSize(false);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // 1. 点击之后图标变色
            // 2. 鼠标变成手的图标
            // 3. 画布中触发MouseDown MouseMove MouseUp动作
            this.isClick = !this.isClick;
            if (isClick)
            {
                this.Parent.Cursor = Cursors.Hand;
            }
            else
            {
                this.Parent.Cursor = Cursors.Default;
            }
        }
        #endregion
    }
}
