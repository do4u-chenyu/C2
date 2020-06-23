using Citta_T1.Core;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Citta_T1.Controls.Flow
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
            this.label1.ForeColor = Color.White;
        }

        private void RightHideButton_MouseLeave(object sender, EventArgs e)
        {
            this.BackgroundImage = global::Citta_T1.Properties.Resources.shadow;
            this.label1.ForeColor = Color.Black;
        }

        private void RightHideButton_Click(object sender, EventArgs e)
        {
            FlowControlHidden();
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
            FlowControlHidden();
        }
        private void FlowControlHidden()
        {
            Global.GetFlowControl().Visible = false;
            Global.GetFlowControl().SelectRemark = false;
            Global.GetRemarkControl().Visible = false;
            Global.GetCurrentDocument().RemarkVisible = false;
            Global.GetFlowControl().RemarkChange(false);

        }

    }
}
