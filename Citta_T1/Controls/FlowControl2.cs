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
    public partial class FlowControl2 : UserControl
    {
        public FlowControl2()
        {
            InitializeComponent();
        }

        private void FlowControl2_MouseEnter(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile(Application.StartupPath + "\\res\\displaypanel\\yin2.png");
            this.label1.ForeColor = Color.White;
        }

        private void FlowControl2_MouseLeave(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile(Application.StartupPath + "\\res\\displaypanel\\yin.png");
            this.label1.ForeColor = Color.Black;
        }

        private void FlowControl2_Click(object sender, EventArgs e)
        {
            foreach (Control ct in this.Parent.Controls)
            {
                if (ct.Name == "flowControl")
                    ct.Visible = true;
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
                        ct.Visible = true;
                }
            
        }
    }
}
