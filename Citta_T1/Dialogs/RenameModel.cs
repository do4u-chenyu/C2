using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Dialogs
{
    public partial class RenameModel : Form
    {
        public string opControlName;
        public RenameModel()
        {
            InitializeComponent();
        }

        private void RenameModel_Load(object sender, EventArgs e)
        {

        }

        private void YesButton_Click(object sender, EventArgs e)
        {
            if (this.textBoxEx1.Text.Length == 0)
                return;
            this.opControlName = this.textBoxEx1.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        private void textBoxEx1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 按下回车键
            if (e.KeyChar == 13)
            {
                if (this.textBoxEx1.Text.Length == 0)
                    return;
                this.opControlName = this.textBoxEx1.Text;
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
