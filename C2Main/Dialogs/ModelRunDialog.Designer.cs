namespace C2.Dialogs
{
    partial class ModelRunDialog
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
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.continueRun = new System.Windows.Forms.RadioButton();
            this.restartRun = new System.Windows.Forms.RadioButton();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.BackColor = System.Drawing.Color.Transparent;
            this.groupBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox.Controls.Add(this.continueRun);
            this.groupBox.Controls.Add(this.restartRun);
            this.groupBox.ForeColor = System.Drawing.Color.Black;
            this.groupBox.Location = new System.Drawing.Point(17, 14);
            this.groupBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox.Name = "groupBox";
            this.groupBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox.Size = new System.Drawing.Size(219, 40);
            this.groupBox.TabIndex = 10005;
            this.groupBox.TabStop = false;
            this.groupBox.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // continueRun
            // 
            this.continueRun.AutoSize = true;
            this.continueRun.Location = new System.Drawing.Point(125, 12);
            this.continueRun.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.continueRun.Name = "continueRun";
            this.continueRun.Size = new System.Drawing.Size(88, 19);
            this.continueRun.TabIndex = 1;
            this.continueRun.Text = "继续上次";
            this.continueRun.UseVisualStyleBackColor = true;
            // 
            // restartRun
            // 
            this.restartRun.AutoSize = true;
            this.restartRun.Checked = true;
            this.restartRun.Location = new System.Drawing.Point(8, 12);
            this.restartRun.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.restartRun.Name = "restartRun";
            this.restartRun.Size = new System.Drawing.Size(88, 19);
            this.restartRun.TabIndex = 0;
            this.restartRun.TabStop = true;
            this.restartRun.Text = "重新运行";
            this.restartRun.UseVisualStyleBackColor = true;
            // 
            // ModelRunDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 120);
            this.Controls.Add(this.groupBox);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ModelRunDialog";
            this.Text = "多维算子运行";
            this.Controls.SetChildIndex(this.groupBox, 0);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.RadioButton continueRun;
        private System.Windows.Forms.RadioButton restartRun;
    }
}