using System;
using System.Drawing;
using System.Windows.Forms;

namespace C2.Forms.Splash
{
    public partial class BaseSplashForm : Form
    {
        private bool active;

        // 自己拍脑袋实现的一个飞溅窗口特效
        // 土法炼钢
        public BaseSplashForm()
        {
            InitializeComponent();
        }

        public void AddItem(string name, string desc)
        {
            DataGridViewRow dgvr = new DataGridViewRow();
            DataGridViewTextBoxCell dgvtbc = new DataGridViewTextBoxCell
            {
                Value = name
            };

            dgvr.Cells.Add(dgvtbc);
            dgvtbc.ToolTipText = desc;

            DGV.Rows.Add(dgvr);
        }

        private void BaseSplashForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                this.Close();

            this.active = true;
        }

        private void BaseSplashForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                this.Close();

            this.active = true;
        }

        private void BaseSplashForm_Shown(object sender, EventArgs e)
        {
            this.closeTimer.Enabled = true;
            this.active = false;

            int mid = this.DGV.Rows.Count / 2;
            if (mid < this.DGV.Rows.Count)
                this.DGV.Rows[mid].Selected = true;
        }

        private void CloseTimer_Tick(object sender, EventArgs e)
        {
            // 上一个周期里有鼠标活动, 重新计时
            if (this.active)
            {
                this.active = false;
                return;
            }

            this.Close();   
        }

        private void BaseSplashForm_MouseMove(object sender, MouseEventArgs e)
        {
            this.active = true;
        }

        private void CloseLabel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
