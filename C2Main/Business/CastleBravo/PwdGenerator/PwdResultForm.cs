using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.PwdGenerator
{
    public partial class PwdResultForm : Form
    {

        public string richTextBoxText { set { this.richTextBox1.Text = value; } }
        public PwdResultForm()
        {
            InitializeComponent();
        }
    }
}
