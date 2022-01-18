using C2.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Dialogs
{
    partial class ModelRunDialog : StandardDialog
    {
        private string runType = "continue";
        public ModelRunDialog()
        {
            InitializeComponent();
        }
        protected override bool OnOKButtonClick()
        {
            if (this.restartRun.Checked)
                runType = "restart";
            this.DialogResult = DialogResult.OK;
            return true;
        }

        public new string ShowDialog()
        {
            return base.ShowDialog() == System.Windows.Forms.DialogResult.OK ? runType : "cancle";
        }

        private void groupBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }
    }
}
