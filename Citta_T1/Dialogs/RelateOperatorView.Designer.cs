namespace C2.Dialogs
{
    partial class RelateOperatorView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RelateOperatorView));
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bottomPanel.SuspendLayout();
            this.keyPanel.SuspendLayout();
            this.valuePanel.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataSourceTB1
            // 
            this.dataSourceTB1.Location = new System.Drawing.Point(238, 2);
            this.dataSourceTB1.Size = new System.Drawing.Size(150, 29);
            this.dataSourceTB1.TabIndex = 12;
            this.dataSourceTB1.Visible = true;
            // 
            // dataSourceTB0
            // 
            this.dataSourceTB0.Location = new System.Drawing.Point(4, 2);
            this.dataSourceTB0.Size = new System.Drawing.Size(150, 29);
            this.dataSourceTB0.TabIndex = 11;
            // 
            // cancelButton
            // 
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Location = new System.Drawing.Point(494, 9);
            this.cancelButton.Size = new System.Drawing.Size(63, 27);
            // 
            // confirmButton
            // 
            this.confirmButton.FlatAppearance.BorderSize = 0;
            this.confirmButton.Location = new System.Drawing.Point(394, 9);
            this.confirmButton.Size = new System.Drawing.Size(60, 27);
            // 
            // outListCCBL0
            // 
            this.outListCCBL0.Location = new System.Drawing.Point(4, 263);
            this.outListCCBL0.Size = new System.Drawing.Size(150, 30);
            this.outListCCBL0.TabIndex = 8;
            // 
            // outListCCBL1
            // 
            this.outListCCBL1.Location = new System.Drawing.Point(238, 263);
            this.outListCCBL1.Size = new System.Drawing.Size(150, 30);
            this.outListCCBL1.TabIndex = 13;
            this.outListCCBL1.Visible = true;
            // 
            // comboBox0
            // 
            this.comboBox0.Location = new System.Drawing.Point(2, 2);
            // 
            // comboBox1
            // 
            this.comboBox1.Location = new System.Drawing.Point(182, 2);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.Visible = true;
            // 
            // topPanel
            // 
            this.topPanel.Size = new System.Drawing.Size(575, 37);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Location = new System.Drawing.Point(0, 338);
            this.bottomPanel.Size = new System.Drawing.Size(575, 43);
            // 
            // keyPanel
            // 
            this.keyPanel.Controls.Add(this.label3);
            this.keyPanel.Controls.Add(this.label2);
            this.keyPanel.Controls.Add(this.label1);
            this.keyPanel.Location = new System.Drawing.Point(0, 37);
            this.keyPanel.Size = new System.Drawing.Size(116, 301);
            this.keyPanel.Controls.SetChildIndex(this.label1, 0);
            this.keyPanel.Controls.SetChildIndex(this.label2, 0);
            this.keyPanel.Controls.SetChildIndex(this.label3, 0);
            // 
            // valuePanel
            // 
            this.valuePanel.Controls.Add(this.outListCCBL1);
            this.valuePanel.Controls.Add(this.dataSourceTB1);
            this.valuePanel.Controls.Add(this.dataSourceTB0);
            this.valuePanel.Controls.Add(this.label4);
            this.valuePanel.Controls.Add(this.outListCCBL0);
            this.valuePanel.Controls.Add(this.tableLayoutPanel2);
            this.valuePanel.Controls.Add(this.panel1);
            this.valuePanel.Location = new System.Drawing.Point(116, 37);
            this.valuePanel.Size = new System.Drawing.Size(459, 301);
            this.valuePanel.Controls.SetChildIndex(this.panel1, 0);
            this.valuePanel.Controls.SetChildIndex(this.tableLayoutPanel2, 0);
            this.valuePanel.Controls.SetChildIndex(this.outListCCBL0, 0);
            this.valuePanel.Controls.SetChildIndex(this.label4, 0);
            this.valuePanel.Controls.SetChildIndex(this.dataSourceTB0, 0);
            this.valuePanel.Controls.SetChildIndex(this.dataSourceTB1, 0);
            this.valuePanel.Controls.SetChildIndex(this.outListCCBL1, 0);
            // 
            // label1
            // 
            this.label1.Size = new System.Drawing.Size(123, 30);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(32, 262);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 30);
            this.label3.TabIndex = 0;
            this.label3.Text = "输出字段：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(32, 58);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 30);
            this.label2.TabIndex = 0;
            this.label2.Text = "连接条件：";
            // 
            // label4
            // 
            this.label4.Image = global::C2.Properties.Resources.relation;
            this.label4.Location = new System.Drawing.Point(155, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 27);
            this.label4.TabIndex = 10;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel2.Controls.Add(this.label5, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.button1, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.comboBox0, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.comboBox1, 2, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(95, 54);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(308, 32);
            this.tableLayoutPanel2.TabIndex = 6;
            this.tableLayoutPanel2.Controls.SetChildIndex(this.comboBox1, 0);
            this.tableLayoutPanel2.Controls.SetChildIndex(this.comboBox0, 0);
            this.tableLayoutPanel2.Controls.SetChildIndex(this.button1, 0);
            this.tableLayoutPanel2.Controls.SetChildIndex(this.label5, 0);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(92, 4);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 23);
            this.label5.TabIndex = 7;
            this.label5.Text = "等于=";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::C2.Properties.Resources.add;
            this.button1.Location = new System.Drawing.Point(272, 4);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 24);
            this.button1.TabIndex = 5;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(4, 87);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(457, 160);
            this.panel1.TabIndex = 1;
            // 
            // RelateOperatorView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(575, 381);
            this.ControlBox = true;
            this.Controls.Add(this.valuePanel);
            this.Controls.Add(this.keyPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(593, 430);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(593, 430);
            this.Name = "RelateOperatorView";
            this.ShowIcon = true;
            this.Text = "关联算子设置";
            this.bottomPanel.ResumeLayout(false);
            this.keyPanel.ResumeLayout(false);
            this.keyPanel.PerformLayout();
            this.valuePanel.ResumeLayout(false);
            this.valuePanel.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}