namespace Citta_T1.OperatorViews
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.sortByString = new System.Windows.Forms.RadioButton();
            this.sortByNum = new System.Windows.Forms.RadioButton();
            this.endRow = new System.Windows.Forms.TextBox();
            this.firstRow = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.repetition = new System.Windows.Forms.RadioButton();
            this.noRepetition = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.descendingOrder = new System.Windows.Forms.RadioButton();
            this.ascendingOrder = new System.Windows.Forms.RadioButton();
            this.bottomPanel.SuspendLayout();
            this.keyPanel.SuspendLayout();
            this.valuePanel.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(32, 142);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 22);
            this.label3.TabIndex = 0;
            this.label3.Text = "排序方式：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(32, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 22);
            this.label2.TabIndex = 0;
            this.label2.Text = "排序字段：";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(32, 0);
            this.label1.Size = new System.Drawing.Size(90, 22);
            // 
            // topPanel
            // 
            this.topPanel.Size = new System.Drawing.Size(405, 19);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Location = new System.Drawing.Point(0, 272);
            this.bottomPanel.Size = new System.Drawing.Size(405, 40);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(330, 7);
            this.cancelButton.Size = new System.Drawing.Size(63, 27);
            this.cancelButton.TabIndex = 1;
            // 
            // confirmButton
            // 
            this.confirmButton.Location = new System.Drawing.Point(240, 7);
            this.confirmButton.Size = new System.Drawing.Size(60, 27);
            this.confirmButton.TabIndex = 0;
            // 
            // keyPanel
            // 
            this.keyPanel.Controls.Add(this.label9);
            this.keyPanel.Controls.Add(this.label5);
            this.keyPanel.Controls.Add(this.label4);
            this.keyPanel.Controls.Add(this.label3);
            this.keyPanel.Controls.Add(this.label2);
            this.keyPanel.Controls.Add(this.label1);
            this.keyPanel.Location = new System.Drawing.Point(0, 19);
            this.keyPanel.Size = new System.Drawing.Size(116, 253);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(32, 95);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 22);
            this.label9.TabIndex = 1;
            this.label9.Text = "排序内容：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(32, 227);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 22);
            this.label5.TabIndex = 0;
            this.label5.Text = "输出条数：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(32, 187);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 22);
            this.label4.TabIndex = 0;
            this.label4.Text = "是否去重：";
            // 
            // valuePanel
            // 
            this.valuePanel.Controls.Add(this.groupBox3);
            this.valuePanel.Controls.Add(this.endRow);
            this.valuePanel.Controls.Add(this.firstRow);
            this.valuePanel.Controls.Add(this.dataSourceTB0);
            this.valuePanel.Controls.Add(this.comboBox0);
            this.valuePanel.Controls.Add(this.label8);
            this.valuePanel.Controls.Add(this.label7);
            this.valuePanel.Controls.Add(this.label6);
            this.valuePanel.Controls.Add(this.groupBox2);
            this.valuePanel.Controls.Add(this.groupBox1);
            this.valuePanel.Location = new System.Drawing.Point(116, 19);
            this.valuePanel.Size = new System.Drawing.Size(289, 253);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.sortByString);
            this.groupBox3.Controls.Add(this.sortByNum);
            this.groupBox3.Location = new System.Drawing.Point(0, 87);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(199, 30);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            // 
            // sortByString
            // 
            this.sortByString.AutoSize = true;
            this.sortByString.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sortByString.Location = new System.Drawing.Point(91, 10);
            this.sortByString.Margin = new System.Windows.Forms.Padding(2);
            this.sortByString.Name = "sortByString";
            this.sortByString.Size = new System.Drawing.Size(98, 21);
            this.sortByString.TabIndex = 1;
            this.sortByString.Text = "按字符串排序";
            this.sortByString.UseVisualStyleBackColor = true;
            // 
            // sortByNum
            // 
            this.sortByNum.AutoSize = true;
            this.sortByNum.Checked = true;
            this.sortByNum.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sortByNum.Location = new System.Drawing.Point(5, 10);
            this.sortByNum.Margin = new System.Windows.Forms.Padding(2);
            this.sortByNum.Name = "sortByNum";
            this.sortByNum.Size = new System.Drawing.Size(86, 21);
            this.sortByNum.TabIndex = 0;
            this.sortByNum.TabStop = true;
            this.sortByNum.Text = "按数字排序";
            this.sortByNum.UseVisualStyleBackColor = true;
            // 
            // endRow
            // 
            this.endRow.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.endRow.Location = new System.Drawing.Point(103, 226);
            this.endRow.Margin = new System.Windows.Forms.Padding(2);
            this.endRow.Name = "endRow";
            this.endRow.Size = new System.Drawing.Size(39, 23);
            this.endRow.TabIndex = 15;
            this.endRow.Leave += new System.EventHandler(this.EndRow_Leave);
            // 
            // firstRow
            // 
            this.firstRow.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.firstRow.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.firstRow.Location = new System.Drawing.Point(26, 226);
            this.firstRow.Margin = new System.Windows.Forms.Padding(2);
            this.firstRow.Name = "firstRow";
            this.firstRow.Size = new System.Drawing.Size(39, 23);
            this.firstRow.TabIndex = 14;
            this.firstRow.Text = "1";
            this.firstRow.Leave += new System.EventHandler(this.FirstRow_Leave);
            // 
            // dataInfo
            // 
            this.dataSourceTB0.Location = new System.Drawing.Point(2, 0);
            this.dataSourceTB0.Name = "dataInfo";
            this.dataSourceTB0.Size = new System.Drawing.Size(134, 23);
            this.dataSourceTB0.TabIndex = 13;
            // 
            // sortField
            // 
            this.comboBox0.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox0.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox0.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox0.FormattingEnabled = true;
            this.comboBox0.Location = new System.Drawing.Point(2, 44);
            this.comboBox0.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox0.Name = "sortField";
            this.comboBox0.Size = new System.Drawing.Size(134, 24);
            this.comboBox0.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(67, 230);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 17);
            this.label8.TabIndex = 10;
            this.label8.Text = "行到";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(150, 230);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(152, 17);
            this.label7.TabIndex = 8;
            this.label7.Text = "行（不填默认输出所有行）";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(4, 229);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "第";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.repetition);
            this.groupBox2.Controls.Add(this.noRepetition);
            this.groupBox2.Location = new System.Drawing.Point(2, 179);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(197, 30);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // repetition
            // 
            this.repetition.AutoSize = true;
            this.repetition.Checked = true;
            this.repetition.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.repetition.Location = new System.Drawing.Point(89, 10);
            this.repetition.Margin = new System.Windows.Forms.Padding(2);
            this.repetition.Name = "repetition";
            this.repetition.Size = new System.Drawing.Size(62, 21);
            this.repetition.TabIndex = 1;
            this.repetition.TabStop = true;
            this.repetition.Text = "不去重";
            this.repetition.UseVisualStyleBackColor = true;
            // 
            // noRepetition
            // 
            this.noRepetition.AutoSize = true;
            this.noRepetition.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.noRepetition.Location = new System.Drawing.Point(4, 9);
            this.noRepetition.Margin = new System.Windows.Forms.Padding(2);
            this.noRepetition.Name = "noRepetition";
            this.noRepetition.Size = new System.Drawing.Size(50, 21);
            this.noRepetition.TabIndex = 0;
            this.noRepetition.Text = "去重";
            this.noRepetition.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.descendingOrder);
            this.groupBox1.Controls.Add(this.ascendingOrder);
            this.groupBox1.Location = new System.Drawing.Point(0, 134);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(199, 30);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // descendingOrder
            // 
            this.descendingOrder.AutoSize = true;
            this.descendingOrder.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.descendingOrder.Location = new System.Drawing.Point(91, 10);
            this.descendingOrder.Margin = new System.Windows.Forms.Padding(2);
            this.descendingOrder.Name = "descendingOrder";
            this.descendingOrder.Size = new System.Drawing.Size(74, 21);
            this.descendingOrder.TabIndex = 1;
            this.descendingOrder.Text = "从大到小";
            this.descendingOrder.UseVisualStyleBackColor = true;
            // 
            // ascendingOrder
            // 
            this.ascendingOrder.AutoSize = true;
            this.ascendingOrder.Checked = true;
            this.ascendingOrder.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ascendingOrder.Location = new System.Drawing.Point(5, 10);
            this.ascendingOrder.Margin = new System.Windows.Forms.Padding(2);
            this.ascendingOrder.Name = "ascendingOrder";
            this.ascendingOrder.Size = new System.Drawing.Size(74, 21);
            this.ascendingOrder.TabIndex = 0;
            this.ascendingOrder.TabStop = true;
            this.ascendingOrder.Text = "从小到大";
            this.ascendingOrder.UseVisualStyleBackColor = true;
            // 
            // SortOperatorView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(405, 312);
            this.ControlBox = false;
            this.Controls.Add(this.valuePanel);
            this.Controls.Add(this.keyPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SortOperatorView";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "排序算子设置";
            this.bottomPanel.ResumeLayout(false);
            this.keyPanel.ResumeLayout(false);
            this.keyPanel.PerformLayout();
            this.valuePanel.ResumeLayout(false);
            this.valuePanel.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
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
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton sortByString;
        private System.Windows.Forms.RadioButton sortByNum;
    }
}