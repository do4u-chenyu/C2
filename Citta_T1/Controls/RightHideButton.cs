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
            foreach (Control ct in this.Parent.Controls)
            {
                if (ct.Name == "flowControl")
                    ct.Visible = false;
                if (ct.Name == "remarkControl")
                {
                    if (ct.Visible)
                    {
                        ct.Visible = false;
                        foreach (Control ct2 in this.Parent.Controls)
                        {
                            if (ct2.Name == "flowControl")
                            {
                                (ct2 as FlowControl).tmpTag = true;
                            }
                        }
                           
                            
                    }
                }
            }
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
            foreach (Control ct in this.Parent.Controls)
            {
                  

                if (ct.Name == "remarkControl")
                {
                    if (ct.Visible)
                    {
                        ct.Visible = false;
                        foreach (Control ct2 in this.Parent.Controls)
                        {
                            if (ct2.Name == "flowControl")
                            {
                                (ct2 as FlowControl).tmpTag = true;
                            }
                        }
                    }
                }
                if (ct.Name == "flowControl")
                    ct.Visible = false;

            }
     
        }
    }
}
