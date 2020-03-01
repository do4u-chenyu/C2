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
    public partial class RightHideButton : UserControl
    {
        public RightHideButton()
        {
            InitializeComponent();
        }

        private void RightHideButton_MouseEnter(object sender, EventArgs e)
        {
            this.BackgroundImage = global::Citta_T1.Properties.Resources.blueshadow;
           // this.BackgroundImage = Image.FromFile(Application.StartupPath + "\\res\\displaypanel\\blueshadow.png");
            this.label1.ForeColor = Color.White;
        }

        private void RightHideButton_MouseLeave(object sender, EventArgs e)
        {
            this.BackgroundImage = global::Citta_T1.Properties.Resources.shadow;
            //this.BackgroundImage = Image.FromFile(Application.StartupPath + "\\res\\displaypanel\\shadow.png");
            this.label1.ForeColor = Color.Black;
        }

        private void RightHideButton_Click(object sender, EventArgs e)
        {
            FlowControlHidden();
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            this.BackgroundImage = global::Citta_T1.Properties.Resources.blueshadow;
           // this.BackgroundImage = Image.FromFile(Application.StartupPath + "\\res\\displaypanel\\blueshadow.png");
            this.label1.ForeColor = Color.White;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            this.BackgroundImage = global::Citta_T1.Properties.Resources.shadow;
           // this.BackgroundImage = Image.FromFile(Application.StartupPath + "\\res\\displaypanel\\shadow.png");
            this.label1.ForeColor = Color.Black;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            FlowControlHidden();
        }
        private void FlowControlHidden()
        {
            foreach (Control ct in this.Parent.Controls)
            {
                if (ct.Name == "flowControl")
                {
                    ct.Visible = false;
                    (ct as FlowControl).SelectRemark = true;
                }

                if (ct.Name == "remarkControl")
                {
                    ct.Visible = false;
                }
            }
        }
    }
}
