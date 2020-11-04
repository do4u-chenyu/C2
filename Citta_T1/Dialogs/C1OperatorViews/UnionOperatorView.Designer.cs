namespace C2.OperatorViews
{
    partial class UnionOperatorView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnionOperatorView));
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox0 = new System.Windows.Forms.TextBox();
            this.repetition = new System.Windows.Forms.RadioButton();
            this.noRepetition = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lable_line1 = new System.Windows.Forms.Label();
            this.label_line2 = new System.Windows.Forms.Label();
            this.label_line3 = new System.Windows.Forms.Label();
            this.bottomPanel.SuspendLayout();
            this.valuePanel.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataSourceTB1
            // 
            this.dataSourceTB1.Location = new System.Drawing.Point(290, 36);
            this.dataSourceTB1.Size = new System.Drawing.Size(179, 23);
            this.dataSourceTB1.TabIndex = 8;
            this.dataSourceTB1.Visible = true;
            // 
            // dataSourceTB0
            // 
            this.dataSourceTB0.Location = new System.Drawing.Point(60, 36);
            this.dataSourceTB0.Size = new System.Drawing.Size(179, 23);
            this.dataSourceTB0.TabIndex = 7;
            // 
            // cancelButton
            // 
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Location = new System.Drawing.Point(453, 8);
            this.cancelButton.Size = new System.Drawing.Size(63, 27);
            // 
            // confirmButton
            // 
            this.confirmButton.FlatAppearance.BorderSize = 0;
            this.confirmButton.Location = new System.Drawing.Point(349, 8);
            this.confirmButton.Size = new System.Drawing.Size(60, 27);
            // 
            // comboBox0
            // 
            this.comboBox0.Font = new System.Drawing.Font("宋体", 10F);
            this.comboBox0.Location = new System.Drawing.Point(2, 5);
            this.comboBox0.Size = new System.Drawing.Size(146, 21);
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("宋体", 10F);
            this.comboBox1.Location = new System.Drawing.Point(152, 5);
            this.comboBox1.Size = new System.Drawing.Size(146, 21);
            this.comboBox1.Visible = true;
            // 
            // topPanel
            // 
            this.topPanel.Size = new System.Drawing.Size(534, 10);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Location = new System.Drawing.Point(0, 317);
            this.bottomPanel.Size = new System.Drawing.Size(534, 44);
            // 
            // valuePanel
            // 
            this.valuePanel.Controls.Add(this.label_line3);
            this.valuePanel.Controls.Add(this.label_line2);
            this.valuePanel.Controls.Add(this.lable_line1);
            this.valuePanel.Controls.Add(this.panel2);
            this.valuePanel.Controls.Add(this.label3);
            this.valuePanel.Controls.Add(this.label2);
            this.valuePanel.Controls.Add(this.label1);
            this.valuePanel.Controls.Add(this.dataSourceTB1);
            this.valuePanel.Controls.Add(this.dataSourceTB0);
            this.valuePanel.Controls.Add(this.panel1);
            this.valuePanel.Location = new System.Drawing.Point(0, 10);
            this.valuePanel.Size = new System.Drawing.Size(534, 307);
            this.valuePanel.Controls.SetChildIndex(this.panel1, 0);
            this.valuePanel.Controls.SetChildIndex(this.dataSourceTB0, 0);
            this.valuePanel.Controls.SetChildIndex(this.dataSourceTB1, 0);
            this.valuePanel.Controls.SetChildIndex(this.label1, 0);
            this.valuePanel.Controls.SetChildIndex(this.label2, 0);
            this.valuePanel.Controls.SetChildIndex(this.label3, 0);
            this.valuePanel.Controls.SetChildIndex(this.panel2, 0);
            this.valuePanel.Controls.SetChildIndex(this.lable_line1, 0);
            this.valuePanel.Controls.SetChildIndex(this.label_line2, 0);
            this.valuePanel.Controls.SetChildIndex(this.label_line3, 0);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Size = new System.Drawing.Size(63, 14);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F);
            this.label3.Location = new System.Drawing.Point(1, 72);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "是否去重";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F);
            this.label2.Location = new System.Drawing.Point(3, 142);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "并集条件";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 150F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 150F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.Controls.Add(this.button1, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.comboBox0, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBox0, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.comboBox1, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1, 41);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(415, 32);
            this.tableLayoutPanel2.TabIndex = 6;
            this.tableLayoutPanel2.Controls.SetChildIndex(this.comboBox1, 0);
            this.tableLayoutPanel2.Controls.SetChildIndex(this.textBox0, 0);
            this.tableLayoutPanel2.Controls.SetChildIndex(this.comboBox0, 0);
            this.tableLayoutPanel2.Controls.SetChildIndex(this.button1, 0);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.SystemColors.Window;
            this.button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::C2.Properties.Resources.add;
            this.button1.Location = new System.Drawing.Point(392, 4);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(21, 24);
            this.button1.TabIndex = 5;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // textBox0
            // 
            this.textBox0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox0.Font = new System.Drawing.Font("宋体", 10F);
            this.textBox0.ForeColor = System.Drawing.Color.Gray;
            this.textBox0.Location = new System.Drawing.Point(302, 4);
            this.textBox0.Margin = new System.Windows.Forms.Padding(2);
            this.textBox0.Name = "textBox0";
            this.textBox0.Size = new System.Drawing.Size(86, 23);
            this.textBox0.TabIndex = 6;
            this.textBox0.Text = "别名";
            // 
            // repetition
            // 
            this.repetition.AutoSize = true;
            this.repetition.Checked = true;
            this.repetition.Font = new System.Drawing.Font("宋体", 10F);
            this.repetition.Location = new System.Drawing.Point(231, 6);
            this.repetition.Margin = new System.Windows.Forms.Padding(2);
            this.repetition.Name = "repetition";
            this.repetition.Size = new System.Drawing.Size(67, 18);
            this.repetition.TabIndex = 1;
            this.repetition.TabStop = true;
            this.repetition.Text = "不去重";
            this.repetition.UseVisualStyleBackColor = true;
            // 
            // noRepetition
            // 
            this.noRepetition.AutoSize = true;
            this.noRepetition.Font = new System.Drawing.Font("宋体", 10F);
            this.noRepetition.Location = new System.Drawing.Point(2, 6);
            this.noRepetition.Margin = new System.Windows.Forms.Padding(2);
            this.noRepetition.Name = "noRepetition";
            this.noRepetition.Size = new System.Drawing.Size(53, 18);
            this.noRepetition.TabIndex = 0;
            this.noRepetition.Text = "去重";
            this.noRepetition.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Location = new System.Drawing.Point(57, 169);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(465, 123);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.noRepetition);
            this.panel2.Controls.Add(this.repetition);
            this.panel2.Location = new System.Drawing.Point(59, 104);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(414, 34);
            this.panel2.TabIndex = 9;
            // 
            // lable_line1
            // 
            this.lable_line1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lable_line1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lable_line1.Location = new System.Drawing.Point(0, 26);
            this.lable_line1.Name = "lable_line1";
            this.lable_line1.Size = new System.Drawing.Size(550, 2);
            this.lable_line1.TabIndex = 42;
            // 
            // label_line2
            // 
            this.label_line2.BackColor = System.Drawing.SystemColors.Highlight;
            this.label_line2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_line2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label_line2.Location = new System.Drawing.Point(0, 95);
            this.label_line2.Name = "label_line2";
            this.label_line2.Size = new System.Drawing.Size(550, 2);
            this.label_line2.TabIndex = 43;
            // 
            // label_line3
            // 
            this.label_line3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_line3.Location = new System.Drawing.Point(0, 164);
            this.label_line3.Name = "label_line3";
            this.label_line3.Size = new System.Drawing.Size(550, 2);
            this.label_line3.TabIndex = 44;
            // 
            // UnionOperatorView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(534, 361);
            this.ControlBox = true;
            this.Controls.Add(this.valuePanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(550, 400);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(550, 400);
            this.Name = "UnionOperatorView";
            this.ShowIcon = true;
            this.Text = "取并集算子设置";
            this.bottomPanel.ResumeLayout(false);
            this.valuePanel.ResumeLayout(false);
            this.valuePanel.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.RadioButton repetition;
        private System.Windows.Forms.RadioButton noRepetition;
        private System.Windows.Forms.TextBox textBox0;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lable_line1;
        private System.Windows.Forms.Label label_line2;
        private System.Windows.Forms.Label label_line3;
    }
}