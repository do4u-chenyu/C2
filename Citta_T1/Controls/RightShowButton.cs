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
    public partial class RightShowButton : UserControl
    {
        public RightShowButton()
        {
            InitializeComponent();
        }

        private void RightShowButton_MouseEnter(object sender, EventArgs e)
        {
            this.BackgroundImage = global::Citta_T1.Properties.Resources.blueshadow;
            this.label1.ForeColor = Color.White;
        }

        private void RightShowButton_MouseLeave(object sender, EventArgs e)
        {
            this.BackgroundImage = global::Citta_T1.Properties.Resources.shadow;
            this.label1.ForeColor = Color.Black;
        }

        private void RightShowButton_Click(object sender, EventArgs e)
        {
            foreach (Control ct in this.Parent.Controls)
            {
                if (ct.Name == "flowControl")
                    ct.Visible = true;
            }
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            this.BackgroundImage = global::Citta_T1.Properties.Resources.blueshadow;
            this.label1.ForeColor = Color.White;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            this.BackgroundImage = global::Citta_T1.Properties.Resources.shadow;
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
