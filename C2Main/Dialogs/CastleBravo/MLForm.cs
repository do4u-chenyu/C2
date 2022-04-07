using C2.Controls;

namespace C2.Dialogs.CastleBravo
{
    partial class MLForm : StandardDialog
    {
        private readonly string[] ML;
        public MLForm(string[] ml)
        {
            this.ML = ml;
            InitializeComponent();
            InitializeOther();
            InitializeDGV();
        }

        private void InitializeOther()
        {

            this.OKButton.Size = new System.Drawing.Size(75, 27);
            this.CancelBtn.Size = new System.Drawing.Size(75, 27);
            this.DigButton.Size = new System.Drawing.Size(75, 27);
            this.ResetButton.Size = new System.Drawing.Size(75, 27);
        }

        private void InitializeDGV()
        {
            Reset();
        }

        private void Reset()
        {
            this.DGV.Rows.Clear();
            this.DGV.Rows.Add(new string[] { string.Empty, "123456", string.Empty, "admin"});
            this.DGV.Rows.Add();
            this.DGV.Rows.Add();
            this.DGV.Rows.Add();
            this.textBox1.Text = string.Join(System.Environment.NewLine, ML);
        }

        private void DigButton_Click(object sender, System.EventArgs e)
        {

        }

        private void ResetButton_Click(object sender, System.EventArgs e)
        {
            Reset();
        }
    }
}
