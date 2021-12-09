namespace C2.Business.CastleBravo.WebShellTool.SettingsDialog
{
    partial class WebConfigScan
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
            this.filePath = new System.Windows.Forms.Label();
            this.filePathTextBox = new System.Windows.Forms.TextBox();
            this.help1 = new System.Windows.Forms.Label();
            this.help2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.help3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // filePath
            // 
            this.filePath.AutoSize = true;
            this.filePath.BackColor = System.Drawing.Color.Transparent;
            this.filePath.Location = new System.Drawing.Point(12, 24);
            this.filePath.Name = "filePath";
            this.filePath.Size = new System.Drawing.Size(83, 12);
            this.filePath.TabIndex = 10008;
            this.filePath.Text = "config 路径：";
            // 
            // filePathTextBox
            // 
            this.filePathTextBox.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.filePathTextBox.Location = new System.Drawing.Point(104, 21);
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.Size = new System.Drawing.Size(205, 21);
            this.filePathTextBox.TabIndex = 10007;
            this.filePathTextBox.Text = "/data/config.inc.php";
            // 
            // help1
            // 
            this.help1.AutoSize = true;
            this.help1.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.help1.Location = new System.Drawing.Point(12, 85);
            this.help1.Name = "help1";
            this.help1.Size = new System.Drawing.Size(299, 12);
            this.help1.TabIndex = 10041;
            this.help1.Text = "若配置文件路径D:/wwwroot/star.com/data/config.php";
            // 
            // help2
            // 
            this.help2.AutoSize = true;
            this.help2.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.help2.Location = new System.Drawing.Point(12, 143);
            this.help2.Name = "help2";
            this.help2.Size = new System.Drawing.Size(0, 12);
            this.help2.TabIndex = 10040;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(12, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 12);
            this.label4.TabIndex = 10039;
            this.label4.Text = "此设置不保存,用前每次设置一下";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.label1.Location = new System.Drawing.Point(12, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 10042;
            // 
            // help3
            // 
            this.help3.AutoSize = true;
            this.help3.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.help3.Location = new System.Drawing.Point(12, 107);
            this.help3.Name = "help3";
            this.help3.Size = new System.Drawing.Size(161, 12);
            this.help3.TabIndex = 10043;
            this.help3.Text = "config路径为：/data/config";
            // 
            // WebConfigScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(319, 176);
            this.Controls.Add(this.help3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.help1);
            this.Controls.Add(this.help2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.filePath);
            this.Controls.Add(this.filePathTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "WebConfigScan";
            this.Text = "Mysql探针配置";
            this.Controls.SetChildIndex(this.filePathTextBox, 0);
            this.Controls.SetChildIndex(this.filePath, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.help2, 0);
            this.Controls.SetChildIndex(this.help1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.help3, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label filePath;
        private System.Windows.Forms.TextBox filePathTextBox;
        private System.Windows.Forms.Label help1;
        private System.Windows.Forms.Label help2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label help3;
    }
}