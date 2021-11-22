namespace C2.OperatorViews
{
    partial class PreprocessingOperatorView
    {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreprocessingOperatorView));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dirSampleLabel = new System.Windows.Forms.Label();
            this.fileSampleLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataSourceTB0
            // 
            this.dataSourceTB0.Location = new System.Drawing.Point(134, 65);
            this.dataSourceTB0.Size = new System.Drawing.Size(212, 23);
            this.dataSourceTB0.TabIndex = 12;
            // 
            // cancelButton
            // 
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Location = new System.Drawing.Point(295, 199);
            this.cancelButton.Size = new System.Drawing.Size(63, 27);
            this.cancelButton.TabIndex = 2;
            // 
            // confirmButton
            // 
            this.confirmButton.FlatAppearance.BorderSize = 0;
            this.confirmButton.Location = new System.Drawing.Point(217, 199);
            this.confirmButton.Size = new System.Drawing.Size(60, 27);
            this.confirmButton.TabIndex = 1;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Location = new System.Drawing.Point(0, 193);
            this.bottomPanel.Size = new System.Drawing.Size(369, 40);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(27, 68);
            this.label1.Size = new System.Drawing.Size(63, 14);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 10F);
            this.groupBox1.Location = new System.Drawing.Point(22, 110);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 68);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据处理";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(235, 33);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(68, 18);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.Text = "去图片";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(123, 33);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(82, 18);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "去长文本";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(22, 33);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(68, 18);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "去广告";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F);
            this.label2.Location = new System.Drawing.Point(25, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 15;
            this.label2.Text = "数据样例";
            // 
            // dirSampleLabel
            // 
            this.dirSampleLabel.AutoSize = true;
            this.dirSampleLabel.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Underline);
            this.dirSampleLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.dirSampleLabel.Location = new System.Drawing.Point(138, 26);
            this.dirSampleLabel.Name = "dirSampleLabel";
            this.dirSampleLabel.Size = new System.Drawing.Size(77, 14);
            this.dirSampleLabel.TabIndex = 16;
            this.dirSampleLabel.Text = "文件夹模板";
            this.dirSampleLabel.Click += new System.EventHandler(this.DirSampleLabel_Click);
            // 
            // fileSampleLabel
            // 
            this.fileSampleLabel.AutoSize = true;
            this.fileSampleLabel.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Underline);
            this.fileSampleLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.fileSampleLabel.Location = new System.Drawing.Point(250, 26);
            this.fileSampleLabel.Name = "fileSampleLabel";
            this.fileSampleLabel.Size = new System.Drawing.Size(63, 14);
            this.fileSampleLabel.TabIndex = 17;
            this.fileSampleLabel.Text = "文件模板";
            this.fileSampleLabel.Click += new System.EventHandler(this.FileSampleLabel_Click);
            // 
            // PreprocessingOperatorView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(369, 233);
            this.ControlBox = true;
            this.Controls.Add(this.fileSampleLabel);
            this.Controls.Add(this.dirSampleLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.dataSourceTB0);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreprocessingOperatorView";
            this.ShowIcon = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据预处理算子配置";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.dataSourceTB0, 0);
            this.Controls.SetChildIndex(this.bottomPanel, 0);
            this.Controls.SetChildIndex(this.confirmButton, 0);
            this.Controls.SetChildIndex(this.cancelButton, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.dirSampleLabel, 0);
            this.Controls.SetChildIndex(this.fileSampleLabel, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label dirSampleLabel;
        private System.Windows.Forms.Label fileSampleLabel;
    }
}