using C2.Controls;
using System;
using System.Drawing;

namespace C2.Business.CastleBravo.VPN
{
    partial class StaticForm : StandardDialog
    {
        private readonly string staticString;
        public StaticForm(string value)
        {
            InitializeComponent();
            InitializeOther();
            staticString = value;
        }

        private void InitializeOther()
        {
            this.OKButton.Size = new Size(75, 27);
            this.CancelBtn.Size = new Size(75, 27);
        }

        private void StaticForm_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = staticString;
        }
    }
}
