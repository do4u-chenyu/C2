using C2.Controls.C1.Left;
using C2.Controls.Left;
using System;
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

        private DataGridViewRow AddItem(string name, string desc)
        {
            DataGridViewRow dgvr = new DataGridViewRow();
            dgvr.Cells.Add(new DataGridViewTextBoxCell
            {
                Value = name,
                ToolTipText = desc,
            });
            DGV.Rows.Add(dgvr);
            return dgvr;
        }

        protected void AddItem(string name, string desc, UserControl button)
        {
            AddItem(name, desc).Tag = button;
        }
        protected void AddItem(string name, string desc, PluginButton button)
        {
            AddItem(name, desc).Tag = button;
        }
        

        protected void AddItem(string name, string desc, Form form)
        {
            AddItem(name, desc).Tag = form;
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
            closeTimer.Enabled = true;
            active = false;
            Height = 800;
            int cnt = DGV.Rows.Count;
            int mid = cnt / 2;
            if (mid < cnt)
            {
                DGV.Rows[mid].Selected = true;
                int height = DGV.Rows[mid].Height * cnt;
                Height = Math.Min(height + topPanel.Height + 5, Height);
            }            
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

        private void DGV_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            if (e.Clicks != 2)
                return;
            if (e.RowIndex > DGV.Rows.Count - 1)
                return;
            OpenItem(DGV.Rows[e.RowIndex].Tag);
        }

        protected virtual void OpenItem(object item)
        {
            if (item is IAOButton)
                (item as IAOButton).OpenToolForm();
            if (item is Form)
                (item as Form).ShowDialog();
        }
    }
}
