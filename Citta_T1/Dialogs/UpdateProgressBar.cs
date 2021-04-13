using System.Windows.Forms;

namespace C2.Dialogs
{
    public partial class UpdateProgressBar : Form
    {
        public UpdateProgressBar()
        {
            InitializeComponent();
        }
       
        public string Status { get => this.status.Text; set => this.status.Text = value; }
        public int MinimumValue { get => this.proBarDownLoad.Minimum; set => this.proBarDownLoad.Minimum = value; }
        public int MaximumValue { get => this.proBarDownLoad.Maximum; set => this.proBarDownLoad.Maximum = value; }
        public int CurrentValue { get => this.proBarDownLoad.Value; set => this.proBarDownLoad.Value = value; }
        public string ProgressPercentage { get => this.speedValue.Text; set => this.speedValue.Text = value; }
        public int ProgressValue { get; set; }

        private void UpdateProgressBar_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ProgressPercentage.Equals("100%"))
                return;
            DialogResult result = MessageBox.Show("下载未完成，确认结束当前更新任务", "下载提示", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                e.Cancel = true;                     
        }
    }
}
