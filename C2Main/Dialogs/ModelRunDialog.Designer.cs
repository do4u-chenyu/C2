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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.continueRun = new System.Windows.Forms.RadioButton();
            this.restartRun = new System.Windows.Forms.RadioButton();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 12);
            this.label1.TabIndex = 10004;
            this.label1.Text = "模型将重新运行，请确认：";
            // 
            // groupBox
            // 
            this.groupBox.BackColor = System.Drawing.Color.Transparent;
            this.groupBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox.Controls.Add(this.continueRun);
            this.groupBox.Controls.Add(this.restartRun);
            this.groupBox.ForeColor = System.Drawing.Color.Black;
            this.groupBox.Location = new System.Drawing.Point(10, 23);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(164, 32);
            this.groupBox.TabIndex = 10005;
            this.groupBox.TabStop = false;
            this.groupBox.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // continueRun
            // 
            this.continueRun.AutoSize = true;
            this.continueRun.Location = new System.Drawing.Point(94, 10);
            this.continueRun.Name = "continueRun";
            this.continueRun.Size = new System.Drawing.Size(71, 16);
            this.continueRun.TabIndex = 1;
            this.continueRun.Text = "继续运行";
            this.continueRun.UseVisualStyleBackColor = true;
            // 
            // restartRun
            // 
            this.restartRun.AutoSize = true;
            this.restartRun.Checked = true;
            this.restartRun.Location = new System.Drawing.Point(6, 10);
            this.restartRun.Name = "restartRun";
            this.restartRun.Size = new System.Drawing.Size(71, 16);
            this.restartRun.TabIndex = 0;
            this.restartRun.TabStop = true;
            this.restartRun.Text = "重新运行";
            this.restartRun.UseVisualStyleBackColor = true;
            // 
            // ModelRunDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 107);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox);
            this.Name = "ModelRunDialog";
            this.Text = "模型运行";
            this.Controls.SetChildIndex(this.groupBox, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.RadioButton continueRun;
        private System.Windows.Forms.RadioButton restartRun;
    }
}