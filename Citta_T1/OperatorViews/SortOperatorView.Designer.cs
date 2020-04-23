namespace Citta_T1.OperatorViews
{
    partial class SortOperatorView
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.topPanel = new System.Windows.Forms.Panel();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.confirmButton = new System.Windows.Forms.Button();
            this.keyPanel = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.valuePanel = new System.Windows.Forms.Panel();
            this.outList = new System.Windows.Forms.ComboBox();
            this.endRow = new HZH_Controls.Controls.TextBoxEx();
            this.label8 = new System.Windows.Forms.Label();
            this.firstRow = new HZH_Controls.Controls.TextBoxEx();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.noRepetition = new System.Windows.Forms.RadioButton();
            this.repetition = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.descendingOrder = new System.Windows.Forms.RadioButton();
            this.ascendingOrder = new System.Windows.Forms.RadioButton();
            this.dataInfo = new HZH_Controls.Controls.TextBoxEx();
            this.bottomPanel.SuspendLayout();
            this.keyPanel.SuspendLayout();
            this.valuePanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(48, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 31);
            this.label3.TabIndex = 0;
            this.label3.Text = "排序方式：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(48, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 31);
            this.label2.TabIndex = 0;
            this.label2.Text = "输出字段：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(48, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据信息：";
            // 
            // topPanel
            // 
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(653, 56);
            this.topPanel.TabIndex = 0;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.cancelButton);
            this.bottomPanel.Controls.Add(this.confirmButton);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 377);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(653, 96);
            this.bottomPanel.TabIndex = 1;
            // 
            // cancelButton
            // 
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.cancelButton.Location = new System.Drawing.Point(489, 28);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(94, 40);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // confirmButton
            // 
            this.confirmButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.confirmButton.FlatAppearance.BorderSize = 0;
            this.confirmButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.confirmButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.confirmButton.Location = new System.Drawing.Point(354, 28);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(90, 40);
            this.confirmButton.TabIndex = 0;
            this.confirmButton.Text = "确认";
            this.confirmButton.UseVisualStyleBackColor = false;
            // 
            // keyPanel
            // 
            this.keyPanel.Controls.Add(this.label5);
            this.keyPanel.Controls.Add(this.label4);
            this.keyPanel.Controls.Add(this.label3);
            this.keyPanel.Controls.Add(this.label2);
            this.keyPanel.Controls.Add(this.label1);
            this.keyPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.keyPanel.Location = new System.Drawing.Point(0, 56);
            this.keyPanel.Name = "keyPanel";
            this.keyPanel.Size = new System.Drawing.Size(174, 321);
            this.keyPanel.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(48, 240);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 31);
            this.label5.TabIndex = 0;
            this.label5.Text = "输出条数：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(48, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 31);
            this.label4.TabIndex = 0;
            this.label4.Text = "是否去重：";
            // 
            // valuePanel
            // 
            this.valuePanel.Controls.Add(this.outList);
            this.valuePanel.Controls.Add(this.endRow);
            this.valuePanel.Controls.Add(this.label8);
            this.valuePanel.Controls.Add(this.firstRow);
            this.valuePanel.Controls.Add(this.label7);
            this.valuePanel.Controls.Add(this.label6);
            this.valuePanel.Controls.Add(this.groupBox2);
            this.valuePanel.Controls.Add(this.groupBox1);
            this.valuePanel.Controls.Add(this.dataInfo);
            this.valuePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valuePanel.Location = new System.Drawing.Point(174, 56);
            this.valuePanel.Name = "valuePanel";
            this.valuePanel.Size = new System.Drawing.Size(479, 321);
            this.valuePanel.TabIndex = 3;
            // 
            // outList
            // 
            this.outList.FormattingEnabled = true;
            this.outList.Location = new System.Drawing.Point(3, 65);
            this.outList.Name = "outList";
            this.outList.Size = new System.Drawing.Size(199, 26);
            this.outList.TabIndex = 12;
            // 
            // endRow
            // 
            this.endRow.DecLength = 2;
            this.endRow.InputType = HZH_Controls.TextInputType.NotControl;
            this.endRow.Location = new System.Drawing.Point(147, 240);
            this.endRow.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.endRow.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.endRow.MyRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.endRow.Name = "endRow";
            this.endRow.OldText = null;
            this.endRow.PromptColor = System.Drawing.Color.Gray;
            this.endRow.PromptFont = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.endRow.PromptText = "";
            this.endRow.RegexPattern = "";
            this.endRow.Size = new System.Drawing.Size(64, 28);
            this.endRow.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(98, 249);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 18);
            this.label8.TabIndex = 10;
            this.label8.Text = "行到";
            // 
            // firstRow
            // 
            this.firstRow.DecLength = 2;
            this.firstRow.InputType = HZH_Controls.TextInputType.NotControl;
            this.firstRow.Location = new System.Drawing.Point(34, 240);
            this.firstRow.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.firstRow.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.firstRow.MyRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.firstRow.Name = "firstRow";
            this.firstRow.OldText = null;
            this.firstRow.PromptColor = System.Drawing.Color.Gray;
            this.firstRow.PromptFont = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.firstRow.PromptText = "";
            this.firstRow.RegexPattern = "";
            this.firstRow.Size = new System.Drawing.Size(55, 28);
            this.firstRow.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(222, 249);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(224, 18);
            this.label7.TabIndex = 8;
            this.label7.Text = "行（不填默认输出所有行）";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 249);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 18);
            this.label6.TabIndex = 7;
            this.label6.Text = "第";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.noRepetition);
            this.groupBox2.Controls.Add(this.repetition);
            this.groupBox2.Location = new System.Drawing.Point(2, 172);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(232, 45);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox2_Paint);
            // 
            // noRepetition
            // 
            this.noRepetition.AutoSize = true;
            this.noRepetition.Checked = true;
            this.noRepetition.Location = new System.Drawing.Point(128, 15);
            this.noRepetition.Name = "noRepetition";
            this.noRepetition.Size = new System.Drawing.Size(87, 22);
            this.noRepetition.TabIndex = 1;
            this.noRepetition.TabStop = true;
            this.noRepetition.Text = "不去重";
            this.noRepetition.UseVisualStyleBackColor = true;
            // 
            // repetition
            // 
            this.repetition.AutoSize = true;
            this.repetition.Location = new System.Drawing.Point(8, 15);
            this.repetition.Name = "repetition";
            this.repetition.Size = new System.Drawing.Size(69, 22);
            this.repetition.TabIndex = 0;
            this.repetition.Text = "去重";
            this.repetition.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.descendingOrder);
            this.groupBox1.Controls.Add(this.ascendingOrder);
            this.groupBox1.Location = new System.Drawing.Point(0, 110);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(232, 45);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // descendingOrder
            // 
            this.descendingOrder.AutoSize = true;
            this.descendingOrder.Checked = true;
            this.descendingOrder.Location = new System.Drawing.Point(128, 15);
            this.descendingOrder.Name = "descendingOrder";
            this.descendingOrder.Size = new System.Drawing.Size(105, 22);
            this.descendingOrder.TabIndex = 1;
            this.descendingOrder.TabStop = true;
            this.descendingOrder.Text = "从大到小";
            this.descendingOrder.UseVisualStyleBackColor = true;
            // 
            // ascendingOrder
            // 
            this.ascendingOrder.AutoSize = true;
            this.ascendingOrder.Location = new System.Drawing.Point(8, 15);
            this.ascendingOrder.Name = "ascendingOrder";
            this.ascendingOrder.Size = new System.Drawing.Size(105, 22);
            this.ascendingOrder.TabIndex = 0;
            this.ascendingOrder.Text = "从小到大";
            this.ascendingOrder.UseVisualStyleBackColor = true;
            // 
            // dataInfo
            // 
            this.dataInfo.DecLength = 2;
            this.dataInfo.Font = new System.Drawing.Font("微软雅黑", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataInfo.InputType = HZH_Controls.TextInputType.NotControl;
            this.dataInfo.Location = new System.Drawing.Point(0, 0);
            this.dataInfo.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.dataInfo.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.dataInfo.MyRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.dataInfo.Name = "dataInfo";
            this.dataInfo.OldText = null;
            this.dataInfo.PromptColor = System.Drawing.Color.Gray;
            this.dataInfo.PromptFont = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.dataInfo.PromptText = "";
            this.dataInfo.ReadOnly = true;
            this.dataInfo.RegexPattern = "";
            this.dataInfo.Size = new System.Drawing.Size(202, 34);
            this.dataInfo.TabIndex = 0;
            this.dataInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SortOperatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 473);
            this.ControlBox = false;
            this.Controls.Add(this.valuePanel);
            this.Controls.Add(this.keyPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SortOperatorView";
            this.ShowIcon = false;
            this.Text = "排序算子设置";
            this.bottomPanel.ResumeLayout(false);
            this.keyPanel.ResumeLayout(false);
            this.keyPanel.PerformLayout();
            this.valuePanel.ResumeLayout(false);
            this.valuePanel.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Panel keyPanel;
        private System.Windows.Forms.Panel valuePanel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private HZH_Controls.Controls.TextBoxEx dataInfo;
        private HZH_Controls.Controls.TextBoxEx firstRow;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton noRepetition;
        private System.Windows.Forms.RadioButton repetition;
        private System.Windows.Forms.RadioButton descendingOrder;
        private System.Windows.Forms.RadioButton ascendingOrder;
        private HZH_Controls.Controls.TextBoxEx endRow;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox outList;
    }
}