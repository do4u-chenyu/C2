namespace Citta_T1.OperatorViews
{
    partial class KeywordOperatorView
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.keywordPreviewBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.conditionSelectBox = new System.Windows.Forms.ComboBox();
            this.bottomPanel.SuspendLayout();
            this.keyPanel.SuspendLayout();
            this.valuePanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataSourceTB1
            // 
            this.dataSourceTB1.Location = new System.Drawing.Point(218, 4);
            this.dataSourceTB1.Size = new System.Drawing.Size(121, 23);
            this.dataSourceTB1.TabIndex = 10;
            this.dataSourceTB1.Visible = true;
            // 
            // dataSourceTB0
            // 
            this.dataSourceTB0.Location = new System.Drawing.Point(0, 4);
            this.dataSourceTB0.Size = new System.Drawing.Size(127, 23);
            this.dataSourceTB0.TabIndex = 9;
            // 
            // cancelButton
            // 
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Location = new System.Drawing.Point(387, 180);
            this.cancelButton.Size = new System.Drawing.Size(63, 27);
            // 
            // confirmButton
            // 
            this.confirmButton.FlatAppearance.BorderSize = 0;
            this.confirmButton.Location = new System.Drawing.Point(320, 180);
            this.confirmButton.Size = new System.Drawing.Size(60, 27);
            // 
            // outListCCBL0
            // 
            this.outListCCBL0.Location = new System.Drawing.Point(4, 86);
            this.outListCCBL0.Size = new System.Drawing.Size(125, 24);
            this.outListCCBL0.TabIndex = 8;
            // 
            // comboBox0
            // 
            this.comboBox0.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox0.Location = new System.Drawing.Point(2, 44);
            this.comboBox0.Size = new System.Drawing.Size(126, 25);
            this.comboBox0.TabIndex = 11;
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.Location = new System.Drawing.Point(218, 44);
            this.comboBox1.Size = new System.Drawing.Size(121, 25);
            this.comboBox1.TabIndex = 13;
            this.comboBox1.Visible = true;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.KeywordComBox_SelectedIndexChanged);
            // 
            // topPanel
            // 
            this.topPanel.Size = new System.Drawing.Size(461, 22);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.groupBox1);
            this.bottomPanel.Location = new System.Drawing.Point(0, 145);
            this.bottomPanel.Size = new System.Drawing.Size(461, 212);
            this.bottomPanel.Controls.SetChildIndex(this.confirmButton, 0);
            this.bottomPanel.Controls.SetChildIndex(this.cancelButton, 0);
            this.bottomPanel.Controls.SetChildIndex(this.groupBox1, 0);
            // 
            // keyPanel
            // 
            this.keyPanel.Controls.Add(this.label3);
            this.keyPanel.Controls.Add(this.label2);
            this.keyPanel.Controls.Add(this.label1);
            this.keyPanel.Location = new System.Drawing.Point(0, 22);
            this.keyPanel.Size = new System.Drawing.Size(102, 123);
            this.keyPanel.Controls.SetChildIndex(this.label1, 0);
            this.keyPanel.Controls.SetChildIndex(this.label2, 0);
            this.keyPanel.Controls.SetChildIndex(this.label3, 0);
            // 
            // valuePanel
            // 
            this.valuePanel.Controls.Add(this.label6);
            this.valuePanel.Controls.Add(this.comboBox1);
            this.valuePanel.Controls.Add(this.conditionSelectBox);
            this.valuePanel.Controls.Add(this.comboBox0);
            this.valuePanel.Controls.Add(this.dataSourceTB1);
            this.valuePanel.Controls.Add(this.dataSourceTB0);
            this.valuePanel.Controls.Add(this.outListCCBL0);
            this.valuePanel.Location = new System.Drawing.Point(102, 22);
            this.valuePanel.Size = new System.Drawing.Size(359, 123);
            this.valuePanel.Controls.SetChildIndex(this.outListCCBL0, 0);
            this.valuePanel.Controls.SetChildIndex(this.dataSourceTB0, 0);
            this.valuePanel.Controls.SetChildIndex(this.dataSourceTB1, 0);
            this.valuePanel.Controls.SetChildIndex(this.comboBox0, 0);
            this.valuePanel.Controls.SetChildIndex(this.conditionSelectBox, 0);
            this.valuePanel.Controls.SetChildIndex(this.comboBox1, 0);
            this.valuePanel.Controls.SetChildIndex(this.label6, 0);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 3);
            this.label1.Size = new System.Drawing.Size(90, 22);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(11, 86);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 22);
            this.label3.TabIndex = 0;
            this.label3.Text = "输出字段：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(10, 44);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 22);
            this.label2.TabIndex = 0;
            this.label2.Text = "过滤条件：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.keywordPreviewBox);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(12, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(437, 170);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "过滤条件预览：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(6, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(204, 17);
            this.label7.TabIndex = 3;
            this.label7.Text = "[2] 当前算子仅支持100行关键词处理";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(7, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "[1] 行与行之间按或运算处理";
            // 
            // keywordPreviewBox
            // 
            this.keywordPreviewBox.BackColor = System.Drawing.SystemColors.Control;
            this.keywordPreviewBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.keywordPreviewBox.Location = new System.Drawing.Point(0, 81);
            this.keywordPreviewBox.Multiline = true;
            this.keywordPreviewBox.Name = "keywordPreviewBox";
            this.keywordPreviewBox.ReadOnly = true;
            this.keywordPreviewBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.keywordPreviewBox.Size = new System.Drawing.Size(434, 89);
            this.keywordPreviewBox.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(141, 3);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 22);
            this.label6.TabIndex = 1;
            this.label6.Text = "关键词：";
            // 
            // conditionSelectBox
            // 
            this.conditionSelectBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.conditionSelectBox.FormattingEnabled = true;
            this.conditionSelectBox.Location = new System.Drawing.Point(135, 44);
            this.conditionSelectBox.Name = "conditionSelectBox";
            this.conditionSelectBox.Size = new System.Drawing.Size(74, 25);
            this.conditionSelectBox.TabIndex = 12;
            // 
            // KeywordOperatorView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(461, 357);
            this.Controls.Add(this.valuePanel);
            this.Controls.Add(this.keyPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(477, 396);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(477, 396);
            this.Name = "KeywordOperatorView";
            this.Text = "关键词过滤算子设置";
            this.bottomPanel.ResumeLayout(false);
            this.keyPanel.ResumeLayout(false);
            this.keyPanel.PerformLayout();
            this.valuePanel.ResumeLayout(false);
            this.valuePanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox conditionSelectBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox keywordPreviewBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}