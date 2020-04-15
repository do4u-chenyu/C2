using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using Citta_T1.Utils;

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
            FlowControlHidden();
        }
        private void FlowControlHidden()
        {
            Global.GetFlowControl().Visible = false;
            Global.GetFlowControl().SelectRemark = false;
            Global.GetRemarkControl().Visible = false;

            FlowControl flowControl = Global.GetFlowControl();
            Type type = typeof(FlowControl);
            MethodInfo mInfo = type.GetMethod("RemarkChange",BindingFlags.NonPublic | BindingFlags.Instance);
            mInfo.Invoke(flowControl, new object[] { false });
        }
    }
}
