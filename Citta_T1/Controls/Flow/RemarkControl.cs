using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Controls.Flow
{
    public delegate void RemarkChangeEventHandler(RemarkControl ct);
    public partial class RemarkControl : UserControl
    {
        public event RemarkChangeEventHandler RemarkChangeEvent;
        public RemarkControl()
        {
            InitializeComponent();
        }
        public string RemarkDescription { get => textBox1.Text; set => textBox1.Text = value; }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            RemarkChangeEvent?.Invoke(this);
        }
    }
}
