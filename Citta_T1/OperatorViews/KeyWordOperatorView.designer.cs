namespace Citta_T1.OperatorViews
{
    partial class KeyWordOperatorView
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
            this.label1 = new System.Windows.Forms.Label();
            this.topPanel = new System.Windows.Forms.Panel();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.keyWordPreviewBox = new System.Windows.Forms.TextBox();
            this.keyPanel = new System.Windows.Forms.Panel();
            this.valuePanel = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.keyWordColBox = new System.Windows.Forms.ComboBox();
            this.conditionSelectBox = new System.Windows.Forms.ComboBox();
            this.dataColumnBox = new System.Windows.Forms.ComboBox();
            this.bottomPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.keyPanel.SuspendLayout();
            this.valuePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataSourceTB1
            // 
            this.dataSourceTB1.Location = new System.Drawing.Point(218, 4);
            this.dataSourceTB1.Margin = new System.Windows.Forms.Padding(2);
            this.dataSourceTB1.Size = new System.Drawing.Size(121, 23);
            this.dataSourceTB1.TabIndex = 10;
            this.dataSourceTB1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.confirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // outListCCBL0
            // 
            this.outListCCBL0.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.outListCCBL0.Location = new System.Drawing.Point(4, 86);
            this.outListCCBL0.Size = new System.Drawing.Size(125, 22);
            this.outListCCBL0.TabIndex = 8;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(11, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据信息：";
            // 
            // topPanel
            // 
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Margin = new System.Windows.Forms.Padding(2);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(461, 22);
            this.topPanel.TabIndex = 0;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.groupBox1);
            this.bottomPanel.Controls.Add(this.cancelButton);
            this.bottomPanel.Controls.Add(this.confirmButton);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 145);
            this.bottomPanel.Margin = new System.Windows.Forms.Padding(2);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(461, 212);
            this.bottomPanel.TabIndex = 1;
            this.bottomPanel.Controls.SetChildIndex(this.confirmButton, 0);
            this.bottomPanel.Controls.SetChildIndex(this.cancelButton, 0);
            this.bottomPanel.Controls.SetChildIndex(this.groupBox1, 0);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.keyWordPreviewBox);
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
            this.label7.Location = new System.Drawing.Point(6, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(408, 17);
            this.label7.TabIndex = 3;
            this.label7.Text = "[3] 当前算子仅支持100行关键词组合与过滤，超出的我们会做默认忽略处理";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(7, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(376, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "[2] AND,\'空格\'表示与运算;  OR,\'|\'表示或运算； NOT,\'!\'表示非运算。";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(7, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(387, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "[1] 行与行之间按或运算处理；每行支持逻辑或、逻辑与、逻辑非运算。";
            // 
            // keyWordPreviewBox
            // 
            this.keyWordPreviewBox.BackColor = System.Drawing.SystemColors.Control;
            this.keyWordPreviewBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.keyWordPreviewBox.Location = new System.Drawing.Point(0, 81);
            this.keyWordPreviewBox.Multiline = true;
            this.keyWordPreviewBox.Name = "keyWordPreviewBox";
            this.keyWordPreviewBox.ReadOnly = true;
            this.keyWordPreviewBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.keyWordPreviewBox.Size = new System.Drawing.Size(434, 89);
            this.keyWordPreviewBox.TabIndex = 0;
            // 
            // keyPanel
            // 
            this.keyPanel.Controls.Add(this.label3);
            this.keyPanel.Controls.Add(this.label2);
            this.keyPanel.Controls.Add(this.label1);
            this.keyPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.keyPanel.Location = new System.Drawing.Point(0, 22);
            this.keyPanel.Margin = new System.Windows.Forms.Padding(2);
            this.keyPanel.Name = "keyPanel";
            this.keyPanel.Size = new System.Drawing.Size(102, 123);
            this.keyPanel.TabIndex = 2;
            // 
            // valuePanel
            // 
            this.valuePanel.Controls.Add(this.label6);
            this.valuePanel.Controls.Add(this.keyWordColBox);
            this.valuePanel.Controls.Add(this.conditionSelectBox);
            this.valuePanel.Controls.Add(this.dataColumnBox);
            this.valuePanel.Controls.Add(this.dataSourceTB1);
            this.valuePanel.Controls.Add(this.dataSourceTB0);
            this.valuePanel.Controls.Add(this.outListCCBL0);
            this.valuePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valuePanel.Location = new System.Drawing.Point(102, 22);
            this.valuePanel.Margin = new System.Windows.Forms.Padding(2);
            this.valuePanel.Name = "valuePanel";
            this.valuePanel.Size = new System.Drawing.Size(359, 123);
            this.valuePanel.TabIndex = 3;
            this.valuePanel.Controls.SetChildIndex(this.outListCCBL0, 0);
            this.valuePanel.Controls.SetChildIndex(this.dataSourceTB0, 0);
            this.valuePanel.Controls.SetChildIndex(this.dataSourceTB1, 0);
            this.valuePanel.Controls.SetChildIndex(this.dataColumnBox, 0);
            this.valuePanel.Controls.SetChildIndex(this.conditionSelectBox, 0);
            this.valuePanel.Controls.SetChildIndex(this.keyWordColBox, 0);
            this.valuePanel.Controls.SetChildIndex(this.label6, 0);
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
            // keyWordColBox
            // 
            this.keyWordColBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.keyWordColBox.FormattingEnabled = true;
            this.keyWordColBox.Location = new System.Drawing.Point(218, 44);
            this.keyWordColBox.Name = "keyWordColBox";
            this.keyWordColBox.Size = new System.Drawing.Size(121, 25);
            this.keyWordColBox.TabIndex = 13;
            this.keyWordColBox.SelectedIndexChanged += new System.EventHandler(this.keyWordColBox_SelectedIndexChanged);
            // 
            // conditionSelectBox
            // 
            this.conditionSelectBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.conditionSelectBox.FormattingEnabled = true;
            this.conditionSelectBox.Items.AddRange(new object[] {
            "命中提取",
            "过滤去噪"});
            this.conditionSelectBox.Location = new System.Drawing.Point(135, 44);
            this.conditionSelectBox.Name = "conditionSelectBox";
            this.conditionSelectBox.Size = new System.Drawing.Size(74, 25);
            this.conditionSelectBox.TabIndex = 12;
            // 
            // dataColumnBox
            // 
            this.dataColumnBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataColumnBox.FormattingEnabled = true;
            this.dataColumnBox.Location = new System.Drawing.Point(2, 44);
            this.dataColumnBox.Name = "dataColumnBox";
            this.dataColumnBox.Size = new System.Drawing.Size(126, 25);
            this.dataColumnBox.TabIndex = 11;
            // 
            // KeyWordOperatorView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(461, 357);
            this.Controls.Add(this.valuePanel);
            this.Controls.Add(this.keyPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "KeyWordOperatorView";
            this.Text = "关键词过滤算子设置";
            this.bottomPanel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.keyPanel.ResumeLayout(false);
            this.keyPanel.PerformLayout();
            this.valuePanel.ResumeLayout(false);
            this.valuePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Panel keyPanel;
        private System.Windows.Forms.Panel valuePanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox keyWordColBox;
        private System.Windows.Forms.ComboBox conditionSelectBox;
        private System.Windows.Forms.ComboBox dataColumnBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox keyWordPreviewBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}