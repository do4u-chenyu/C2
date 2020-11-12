using System;
using System.Windows.Forms;

namespace C2.Controls.Flow
{
    public delegate void RemarkChangeEventHandler(RemarkControl ct);
    public partial class RemarkControl : UserControl
    {
        public event RemarkChangeEventHandler RemarkChangeEvent;
        public RemarkControl()
        {
            InitializeComponent();
        }
        public string RemarkDescription { get => remarkCtrTextBox.Text; set => remarkCtrTextBox.Text = value; }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            RemarkChangeEvent?.Invoke(this);
        }
    }
}
