using Citta_T1.Core;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Citta_T1.Controls.Flow
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
            FlowControlShow();
        }

        private void Label1_MouseEnter(object sender, EventArgs e)
        {
            this.BackgroundImage = global::Citta_T1.Properties.Resources.blueshadow;
            this.label1.ForeColor = Color.White;
        }

        private void Label1_MouseLeave(object sender, EventArgs e)
        {
            this.BackgroundImage = global::Citta_T1.Properties.Resources.shadow;
            this.label1.ForeColor = Color.Black;
        }

        private void Label1_Click(object sender, EventArgs e)
        {

            FlowControlShow();

        }

        private void FlowControlShow()
        {
            Global.GetFlowControl().Visible = true;
            Global.GetCurrentDocument().FlowControlVisible = true;

        }
    }
}
