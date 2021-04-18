﻿using System.Windows.Forms;

namespace C2.Dialogs
{
    public partial class UpdateProgressBar : Form
    {
        public UpdateProgressBar()
        {
            InitializeComponent();
        }
       
        public string Status { get => this.status.Text; set => this.status.Text = value; }
        public int MinimumValue { get => this.proBarDownload.Minimum; set => this.proBarDownload.Minimum = value; }
        public int MaximumValue { get => this.proBarDownload.Maximum; set => this.proBarDownload.Maximum = value; }
        public int CurrentValue { get => this.proBarDownload.Value; set => this.proBarDownload.Value = value; }
        public string ProgressPercentage { get => this.speedValue.Text; set => this.speedValue.Text = value; }
        public int ProgressValue { get; set; }
        public void SetDownloadSize(string value) { this.downloadSizeLabel.Text = "文件大小:" + value; }

        private void UpdateProgressBar_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ProgressPercentage.Equals("100%"))
                return;
            DialogResult result = MessageBox.Show("下载正在进行中，是否取消本次下载", "下载提示", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                e.Cancel = true;                     
        }
    }
}
