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
    partial class MessageDialog : StandardDialog
    {
        public MessageDialog(string msg)
        {
            InitializeComponent();
            InitializeOther(msg);
        }

        private void InitializeOther(string msg)
        {
            this.OKButton.Size = new Size(75, 27);
            this.CancelBtn.Size = new Size(75, 27);
            this.textBox1.Text = msg;
        }
    }
}
