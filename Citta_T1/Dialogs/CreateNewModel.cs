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
    public partial class CreateNewModel : Form
    {
        private string modelTitle;
        public CreateNewModel()
        {
            InitializeComponent();
            modelTitle = "";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            this.modelTitle = this.textBoxEx1.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}
