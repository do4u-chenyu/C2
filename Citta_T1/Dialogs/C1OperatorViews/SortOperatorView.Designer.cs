namespace C2.OperatorViews
{
    partial class SortOperatorView
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
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.sortByString = new System.Windows.Forms.RadioButton();
            this.sortByNum = new System.Windows.Forms.RadioButton();
            this.endRow = new System.Windows.Forms.TextBox();
            this.firstRow = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.repetition = new System.Windows.Forms.RadioButton();
            this.noRepetition = new System.Windows.Forms.RadioButton();
            this.descendingOrder = new System.Windows.Forms.RadioButton();
            this.ascendingOrder = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.bottomPanel.SuspendLayout();
            this.keyPanel.SuspendLayout();
            this.valuePanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataSourceTB0
            // 
            this.dataSourceTB0.Location = new System.Drawing.Point(2, 0);
            this.dataSourceTB0.Size = new System.Drawing.Size(134, 28);
            this.dataSourceTB0.TabIndex = 13;
            // 
            // cancelButton
            // 
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Location = new System.Drawing.Point(330, 7);
            this.cancelButton.Size = new System.Drawing.Size(63, 27);
            // 
            // confirmButton
            // 
            this.confirmButton.FlatAppearance.BorderSize = 0;
            this.confirmButton.Location = new System.Drawing.Point(240, 7);
            this.confirmButton.Size = new System.Drawing.Size(60, 27);
            // 
            // comboBox0
            // 
            this.comboBox0.Location = new System.Drawing.Point(2, 39);
            this.comboBox0.Size = new System.Drawing.Size(132, 26);
            this.comboBox0.TabIndex = 12;
            // 
            // topPanel
            // 
            this.topPanel.Size = new System.Drawing.Size(403, 19);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Location = new System.Drawing.Point(0, 262);
            this.bottomPanel.Size = new System.Drawing.Size(403, 40);
            // 
            // keyPanel
            // 
            this.keyPanel.Controls.Add(this.panel1);
            this.keyPanel.Controls.Add(this.label9);
            this.keyPanel.Controls.Add(this.label5);
            this.keyPanel.Controls.Add(this.label4);
            this.keyPanel.Controls.Add(this.label3);
            this.keyPanel.Controls.Add(this.label2);
            this.keyPanel.Controls.Add(this.label1);
            this.keyPanel.Location = new System.Drawing.Point(0, 19);
            this.keyPanel.Size = new System.Drawing.Size(116, 243);
            this.keyPanel.Controls.SetChildIndex(this.label1, 0);
            this.keyPanel.Controls.SetChildIndex(this.label2, 0);
            this.keyPanel.Controls.SetChildIndex(this.label3, 0);
            this.keyPanel.Controls.SetChildIndex(this.label4, 0);
            this.keyPanel.Controls.SetChildIndex(this.label5, 0);
            this.keyPanel.Controls.SetChildIndex(this.label9, 0);
            this.keyPanel.Controls.SetChildIndex(this.panel1, 0);
            // 
            // valuePanel
            // 
            this.valuePanel.Controls.Add(this.label10);
            this.valuePanel.Controls.Add(this.panel3);
            this.valuePanel.Controls.Add(this.panel2);
            this.valuePanel.Controls.Add(this.sortByString);
            this.valuePanel.Controls.Add(this.sortByNum);
            this.valuePanel.Controls.Add(this.endRow);
            this.valuePanel.Controls.Add(this.firstRow);
            this.valuePanel.Controls.Add(this.dataSourceTB0);
            this.valuePanel.Controls.Add(this.comboBox0);
            this.valuePanel.Controls.Add(this.label8);
            this.valuePanel.Controls.Add(this.label7);
            this.valuePanel.Controls.Add(this.label6);
            this.valuePanel.Location = new System.Drawing.Point(116, 19);
            this.valuePanel.Size = new System.Drawing.Size(287, 243);
            this.valuePanel.Controls.SetChildIndex(this.label6, 0);
            this.valuePanel.Controls.SetChildIndex(this.label7, 0);
            this.valuePanel.Controls.SetChildIndex(this.label8, 0);
            this.valuePanel.Controls.SetChildIndex(this.comboBox0, 0);
            this.valuePanel.Controls.SetChildIndex(this.dataSourceTB0, 0);
            this.valuePanel.Controls.SetChildIndex(this.firstRow, 0);
            this.valuePanel.Controls.SetChildIndex(this.endRow, 0);
            this.valuePanel.Controls.SetChildIndex(this.sortByNum, 0);
            this.valuePanel.Controls.SetChildIndex(this.sortByString, 0);
            this.valuePanel.Controls.SetChildIndex(this.panel2, 0);
            this.valuePanel.Controls.SetChildIndex(this.panel3, 0);
            this.valuePanel.Controls.SetChildIndex(this.label10, 0);
            // 
            // label1
            // 
            this.label1.Size = new System.Drawing.Size(85, 19);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F);
            this.label3.Location = new System.Drawing.Point(32, 130);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "排序方式";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F);
            this.label2.Location = new System.Drawing.Point(32, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "排序字段";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10F);
            this.label9.Location = new System.Drawing.Point(32, 87);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 19);
            this.label9.TabIndex = 1;
            this.label9.Text = "排序内容";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10F);
            this.label5.Location = new System.Drawing.Point(32, 219);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 19);
            this.label5.TabIndex = 0;
            this.label5.Text = "输出条数";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10F);
            this.label4.Location = new System.Drawing.Point(32, 176);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 19);
            this.label4.TabIndex = 0;
            this.label4.Text = "是否去重";
            // 
            // sortByString
            // 
            this.sortByString.AutoSize = true;
            this.sortByString.Font = new System.Drawing.Font("宋体", 10F);
            this.sortByString.Location = new System.Drawing.Point(106, 85);
            this.sortByString.Margin = new System.Windows.Forms.Padding(2);
            this.sortByString.Name = "sortByString";
            this.sortByString.Size = new System.Drawing.Size(144, 23);
            this.sortByString.TabIndex = 1;
            this.sortByString.Text = "按字符串排序";
            this.sortByString.UseVisualStyleBackColor = true;
            // 
            // sortByNum
            // 
            this.sortByNum.AutoSize = true;
            this.sortByNum.Checked = true;
            this.sortByNum.Font = new System.Drawing.Font("宋体", 10F);
            this.sortByNum.Location = new System.Drawing.Point(3, 85);
            this.sortByNum.Margin = new System.Windows.Forms.Padding(2);
            this.sortByNum.Name = "sortByNum";
            this.sortByNum.Size = new System.Drawing.Size(125, 23);
            this.sortByNum.TabIndex = 0;
            this.sortByNum.TabStop = true;
            this.sortByNum.Text = "按数字排序";
            this.sortByNum.UseVisualStyleBackColor = true;
            // 
            // endRow
            // 
            this.endRow.Font = new System.Drawing.Font("宋体", 10F);
            this.endRow.Location = new System.Drawing.Point(93, 216);
            this.endRow.Margin = new System.Windows.Forms.Padding(2);
            this.endRow.Name = "endRow";
            this.endRow.Size = new System.Drawing.Size(39, 28);
            this.endRow.TabIndex = 15;
            // 
            // firstRow
            // 
            this.firstRow.Font = new System.Drawing.Font("宋体", 10F);
            this.firstRow.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.firstRow.Location = new System.Drawing.Point(22, 216);
            this.firstRow.Margin = new System.Windows.Forms.Padding(2);
            this.firstRow.Name = "firstRow";
            this.firstRow.Size = new System.Drawing.Size(39, 28);
            this.firstRow.TabIndex = 14;
            this.firstRow.Text = "1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10F);
            this.label8.Location = new System.Drawing.Point(61, 220);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 19);
            this.label8.TabIndex = 10;
            this.label8.Text = "行到";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 8F);
            this.label7.Location = new System.Drawing.Point(154, 222);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(165, 15);
            this.label7.TabIndex = 8;
            this.label7.Text = "(不填默认输出所有行）";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10F);
            this.label6.Location = new System.Drawing.Point(1, 219);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 19);
            this.label6.TabIndex = 7;
            this.label6.Text = "第";
            // 
            // repetition
            // 
            this.repetition.AutoSize = true;
            this.repetition.Checked = true;
            this.repetition.Font = new System.Drawing.Font("宋体", 10F);
            this.repetition.Location = new System.Drawing.Point(105, -2);
            this.repetition.Margin = new System.Windows.Forms.Padding(2);
            this.repetition.Name = "repetition";
            this.repetition.Size = new System.Drawing.Size(87, 23);
            this.repetition.TabIndex = 1;
            this.repetition.TabStop = true;
            this.repetition.Text = "不去重";
            this.repetition.UseVisualStyleBackColor = true;
            // 
            // noRepetition
            // 
            this.noRepetition.AutoSize = true;
            this.noRepetition.Font = new System.Drawing.Font("宋体", 10F);
            this.noRepetition.Location = new System.Drawing.Point(2, -2);
            this.noRepetition.Margin = new System.Windows.Forms.Padding(2);
            this.noRepetition.Name = "noRepetition";
            this.noRepetition.Size = new System.Drawing.Size(68, 23);
            this.noRepetition.TabIndex = 0;
            this.noRepetition.Text = "去重";
            this.noRepetition.UseVisualStyleBackColor = true;
            // 
            // descendingOrder
            // 
            this.descendingOrder.AutoSize = true;
            this.descendingOrder.Font = new System.Drawing.Font("宋体", 10F);
            this.descendingOrder.Location = new System.Drawing.Point(102, 3);
            this.descendingOrder.Margin = new System.Windows.Forms.Padding(2);
            this.descendingOrder.Name = "descendingOrder";
            this.descendingOrder.Size = new System.Drawing.Size(106, 23);
            this.descendingOrder.TabIndex = 1;
            this.descendingOrder.Text = "从大到小";
            this.descendingOrder.UseVisualStyleBackColor = true;
            // 
            // ascendingOrder
            // 
            this.ascendingOrder.AutoSize = true;
            this.ascendingOrder.Checked = true;
            this.ascendingOrder.Font = new System.Drawing.Font("宋体", 10F);
            this.ascendingOrder.Location = new System.Drawing.Point(1, 3);
            this.ascendingOrder.Margin = new System.Windows.Forms.Padding(2);
            this.ascendingOrder.Name = "ascendingOrder";
            this.ascendingOrder.Size = new System.Drawing.Size(106, 23);
            this.ascendingOrder.TabIndex = 0;
            this.ascendingOrder.TabStop = true;
            this.ascendingOrder.Text = "从小到大";
            this.ascendingOrder.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(116, 80);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(175, 25);
            this.panel1.TabIndex = 16;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ascendingOrder);
            this.panel2.Controls.Add(this.descendingOrder);
            this.panel2.Location = new System.Drawing.Point(2, 125);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 34);
            this.panel2.TabIndex = 16;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.noRepetition);
            this.panel3.Controls.Add(this.repetition);
            this.panel3.Location = new System.Drawing.Point(1, 174);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(221, 35);
            this.panel3.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10F);
            this.label10.Location = new System.Drawing.Point(133, 220);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(28, 19);
            this.label10.TabIndex = 18;
            this.label10.Text = "行";
            // 
            // SortOperatorView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(403, 302);
            this.ControlBox = true;
            this.Controls.Add(this.valuePanel);
            this.Controls.Add(this.keyPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = global::C2.Properties.Resources.sort_icon;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(421, 351);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(421, 351);
            this.Name = "SortOperatorView";
            this.ShowIcon = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "排序算子设置";
            this.bottomPanel.ResumeLayout(false);
            this.keyPanel.ResumeLayout(false);
            this.keyPanel.PerformLayout();
            this.valuePanel.ResumeLayout(false);
            this.valuePanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton repetition;
        private System.Windows.Forms.RadioButton noRepetition;
        private System.Windows.Forms.RadioButton descendingOrder;
        private System.Windows.Forms.RadioButton ascendingOrder;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox endRow;
        private System.Windows.Forms.TextBox firstRow;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton sortByString;
        private System.Windows.Forms.RadioButton sortByNum;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label10;
    }
}