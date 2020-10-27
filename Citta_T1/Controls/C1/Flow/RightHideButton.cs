using C2.Core;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace C2.Controls.Flow
{
    public partial class RightHideButton : UserControl
    {
        bool isActive = true;
        public RightHideButton()
        {
            InitializeComponent();
        }

        private void RightHideButton_MouseEnter(object sender, EventArgs e)
        {
            this.BackgroundImage = global::C2.Properties.Resources.blueshadow;
            this.label1.ForeColor = Color.White;
        }

        private void RightHideButton_MouseLeave(object sender, EventArgs e)
        {
            this.BackgroundImage = global::C2.Properties.Resources.shadow;
            this.label1.ForeColor = Color.Black;
        }

        private void RightHideButton_Click(object sender, EventArgs e)
        {
            if (isActive)
            {
                GetOperatorControlHidden();
                isActive = false;
            }
            else
            {
                OperatorControlShow();
                isActive = true;
            }
        }

        private void Label1_MouseEnter(object sender, EventArgs e)
        {
            this.BackgroundImage = global::C2.Properties.Resources.blueshadow;
            this.label1.ForeColor = Color.White;
        }

        private void Label1_MouseLeave(object sender, EventArgs e)
        {
            this.BackgroundImage = global::C2.Properties.Resources.shadow;
            this.label1.ForeColor = Color.Black;
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            if (isActive)
            {
                GetOperatorControlHidden();
                isActive = false;
            }
            else
            {
                OperatorControlShow();
                isActive = true;
            }
        }
        private void GetOperatorControlHidden()
        {
            Global.GetOperatorControl().Visible = false;
            //Global.GetOperatorControl().SelectRemark = false;
            //Global.GetRemarkControl().Visible = false;
            Global.GetCurrentDocument().RemarkVisible = false;
            Global.GetCurrentDocument().FlowControlVisible = false;
            //Global.GetFlowControl().RemarkChange(false);

        }
        private void OperatorControlShow()
        {
            Global.GetOperatorControl().Visible = true;
            Global.GetCurrentDocument().OperatorControlVisible = true;

        }

        private void RightHideButton_Load(object sender, EventArgs e)
        {

        }
    }
}
