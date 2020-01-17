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
    public partial class FlowControl3 : UserControl
    {
        public FlowControl3()
        {
            InitializeComponent();
        }

        private void FlowControl3_MouseEnter(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile(Application.StartupPath + "\\res\\displaypanel\\yin2.png");
            this.label1.ForeColor = Color.White;
        }

        private void FlowControl3_MouseLeave(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile(Application.StartupPath + "\\res\\displaypanel\\yin.png");
            this.label1.ForeColor = Color.Black;
        }

        private void FlowControl3_Click(object sender, EventArgs e)
        {
            foreach (Control ct in this.Parent.Controls)
            {
                if (ct.Name == "flowControl")
                    ct.Visible = false;
                if (ct.Name == "panel3")
                    ct.Visible = false;
            }
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile(Application.StartupPath + "\\res\\displaypanel\\yin2.png");
            this.label1.ForeColor = Color.White;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile(Application.StartupPath + "\\res\\displaypanel\\yin.png");
            this.label1.ForeColor = Color.Black;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            foreach (Control ct in this.Parent.Controls)
            {
                if (ct.Name == "flowControl")
                    ct.Visible = false;
                if (ct.Name == "panel3")
                    ct.Visible = false;
            }
        }
    }
}
