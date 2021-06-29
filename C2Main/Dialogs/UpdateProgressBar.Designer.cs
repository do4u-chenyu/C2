
namespace C2.Dialogs
{
    partial class UpdateProgressBar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateProgressBar));
            this.proBarDownload = new System.Windows.Forms.ProgressBar();
            this.speedValue = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.Label();
            this.downloadSizeLabel = new System.Windows.Forms.Label();
            this.downloadSpeedLabel = new System.Windows.Forms.Label();
            this.bigWarnLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // proBarDownload
            // 
            this.proBarDownload.Location = new System.Drawing.Point(30, 30);
            this.proBarDownload.Name = "proBarDownload";
            this.proBarDownload.Size = new System.Drawing.Size(384, 23);
            this.proBarDownload.TabIndex = 0;
            // 
            // speedValue
            // 
            this.speedValue.AutoSize = true;
            this.speedValue.Location = new System.Drawing.Point(33, 60);
            this.speedValue.Name = "speedValue";
            this.speedValue.Size = new System.Drawing.Size(17, 12);
            this.speedValue.TabIndex = 2;
            this.speedValue.Text = "0%";
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(29, 13);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(59, 12);
            this.status.TabIndex = 3;
            this.status.Text = "下载中...";
            // 
            // downloadSizeLabel
            // 
            this.downloadSizeLabel.AutoSize = true;
            this.downloadSizeLabel.Location = new System.Drawing.Point(235, 60);
            this.downloadSizeLabel.Name = "downloadSizeLabel";
            this.downloadSizeLabel.Size = new System.Drawing.Size(0, 12);
            this.downloadSizeLabel.TabIndex = 4;
            // 
            // downloadSpeedLabel
            // 
            this.downloadSpeedLabel.AutoSize = true;
            this.downloadSpeedLabel.Location = new System.Drawing.Point(119, 60);
            this.downloadSpeedLabel.Name = "downloadSpeedLabel";
            this.downloadSpeedLabel.Size = new System.Drawing.Size(0, 12);
            this.downloadSpeedLabel.TabIndex = 5;
            // 
            // bigWarnLabel
            // 
            this.bigWarnLabel.AutoSize = true;
            this.bigWarnLabel.Location = new System.Drawing.Point(285, 13);
            this.bigWarnLabel.Name = "bigWarnLabel";
            this.bigWarnLabel.Size = new System.Drawing.Size(137, 12);
            this.bigWarnLabel.TabIndex = 6;
            this.bigWarnLabel.Text = "【大文件建议手动下载】";
            this.bigWarnLabel.Visible = false;
            // 
            // UpdateProgressBar
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(434, 81);
            this.Controls.Add(this.bigWarnLabel);
            this.Controls.Add(this.downloadSizeLabel);
            this.Controls.Add(this.status);
            this.Controls.Add(this.speedValue);
            this.Controls.Add(this.proBarDownload);
            this.Controls.Add(this.downloadSpeedLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateProgressBar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "下载任务";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpdateProgressBar_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar proBarDownload;
        private System.Windows.Forms.Label speedValue;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Label downloadSizeLabel;
        private System.Windows.Forms.Label downloadSpeedLabel;
        private System.Windows.Forms.Label bigWarnLabel;
    }
}