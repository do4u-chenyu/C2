using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.SearchToolkit
{
    public partial class SearchToolkitForm : Form
    {
        private TaskInfo CurrentTaskInfo { get; set; }

        public SearchToolkitForm()
        {
            InitializeComponent();
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void LinuxWorkspaceTB_Enter(object sender, EventArgs e)
        {
            linuxWorkspaceTB.ForeColor = Color.Black;
        }
    }
}
