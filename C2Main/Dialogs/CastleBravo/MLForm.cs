using C2.Controls;

namespace C2.Dialogs.CastleBravo
{
    partial class MLForm : StandardDialog
    {
        public MLForm(string[] ml)
        {
            InitializeComponent();
            this.textBox1.Text = string.Join(System.Environment.NewLine, ml);
            this.OKButton.Size = new System.Drawing.Size(75, 27);
            this.CancelBtn.Size = new System.Drawing.Size(75, 27);
        }
    }
}
