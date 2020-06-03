namespace Citta_T1.OperatorViews
{
    partial class KeyWordOperatorView
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
            this.components = new System.ComponentModel.Container();
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.confirmButton = new System.Windows.Forms.Button();
            this.keyPanel = new System.Windows.Forms.Panel();
            this.valuePanel = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.keyWordColBox = new System.Windows.Forms.ComboBox();
            this.conditionSelectBox = new System.Windows.Forms.ComboBox();
            this.dataColumnBox = new System.Windows.Forms.ComboBox();
            this.keyWordBox = new System.Windows.Forms.TextBox();
            this.dataSourceBox = new System.Windows.Forms.TextBox();
            this.outList = new Citta_T1.Controls.Common.ComCheckBoxList();
            this.dataSourceTip = new System.Windows.Forms.ToolTip(this.components);
            this.keyWordTip = new System.Windows.Forms.ToolTip(this.components);
            this.bottomPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.keyPanel.SuspendLayout();
            this.valuePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(34, 94);
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
            this.label2.Location = new System.Drawing.Point(32, 51);
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
            this.label1.Location = new System.Drawing.Point(32, 2);
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
            this.topPanel.Size = new System.Drawing.Size(461, 37);
            this.topPanel.TabIndex = 0;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.groupBox1);
            this.bottomPanel.Controls.Add(this.cancelButton);
            this.bottomPanel.Controls.Add(this.confirmButton);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 163);
            this.bottomPanel.Margin = new System.Windows.Forms.Padding(2);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(461, 194);
            this.bottomPanel.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.keyWordPreviewBox);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(28, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(437, 143);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "过滤条件预览：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(6, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(408, 17);
            this.label7.TabIndex = 3;
            this.label7.Text = "[3] 当前算子仅支持100行关键词组合与过滤，超出的我们会做默认忽略处理";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(7, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(376, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "[2] AND,\'空格\'表示与运算;  OR,\'|\'表示或运算； NOT,\'!\'表示非运算。";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(6, 23);
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
            this.keyWordPreviewBox.Size = new System.Drawing.Size(434, 55);
            this.keyWordPreviewBox.TabIndex = 0;
            // 
            // cancelButton
            // 
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.cancelButton.Location = new System.Drawing.Point(391, 163);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(63, 27);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // confirmButton
            // 
            this.confirmButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.confirmButton.FlatAppearance.BorderSize = 0;
            this.confirmButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.confirmButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.confirmButton.Location = new System.Drawing.Point(309, 163);
            this.confirmButton.Margin = new System.Windows.Forms.Padding(2);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(60, 27);
            this.confirmButton.TabIndex = 0;
            this.confirmButton.Text = "确认";
            this.confirmButton.UseVisualStyleBackColor = false;
            this.confirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // keyPanel
            // 
            this.keyPanel.Controls.Add(this.label3);
            this.keyPanel.Controls.Add(this.label2);
            this.keyPanel.Controls.Add(this.label1);
            this.keyPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.keyPanel.Location = new System.Drawing.Point(0, 37);
            this.keyPanel.Margin = new System.Windows.Forms.Padding(2);
            this.keyPanel.Name = "keyPanel";
            this.keyPanel.Size = new System.Drawing.Size(124, 126);
            this.keyPanel.TabIndex = 2;
            // 
            // valuePanel
            // 
            this.valuePanel.Controls.Add(this.label6);
            this.valuePanel.Controls.Add(this.keyWordColBox);
            this.valuePanel.Controls.Add(this.conditionSelectBox);
            this.valuePanel.Controls.Add(this.dataColumnBox);
            this.valuePanel.Controls.Add(this.keyWordBox);
            this.valuePanel.Controls.Add(this.dataSourceBox);
            this.valuePanel.Controls.Add(this.outList);
            this.valuePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valuePanel.Location = new System.Drawing.Point(124, 37);
            this.valuePanel.Margin = new System.Windows.Forms.Padding(2);
            this.valuePanel.Name = "valuePanel";
            this.valuePanel.Size = new System.Drawing.Size(337, 126);
            this.valuePanel.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(135, 3);
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
            this.keyWordColBox.Location = new System.Drawing.Point(218, 54);
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
            this.conditionSelectBox.Location = new System.Drawing.Point(137, 54);
            this.conditionSelectBox.Name = "conditionSelectBox";
            this.conditionSelectBox.Size = new System.Drawing.Size(74, 25);
            this.conditionSelectBox.TabIndex = 12;
            // 
            // dataColumnBox
            // 
            this.dataColumnBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataColumnBox.FormattingEnabled = true;
            this.dataColumnBox.Location = new System.Drawing.Point(5, 54);
            this.dataColumnBox.Name = "dataColumnBox";
            this.dataColumnBox.Size = new System.Drawing.Size(126, 25);
            this.dataColumnBox.TabIndex = 11;
            // 
            // keyWordBox
            // 
            this.keyWordBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.keyWordBox.Location = new System.Drawing.Point(218, 2);
            this.keyWordBox.Margin = new System.Windows.Forms.Padding(2);
            this.keyWordBox.Name = "keyWordBox";
            this.keyWordBox.ReadOnly = true;
            this.keyWordBox.Size = new System.Drawing.Size(121, 23);
            this.keyWordBox.TabIndex = 10;
            this.keyWordBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dataSourceBox
            // 
            this.dataSourceBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataSourceBox.Location = new System.Drawing.Point(4, 2);
            this.dataSourceBox.Margin = new System.Windows.Forms.Padding(2);
            this.dataSourceBox.Name = "dataSourceBox";
            this.dataSourceBox.ReadOnly = true;
            this.dataSourceBox.Size = new System.Drawing.Size(127, 23);
            this.dataSourceBox.TabIndex = 9;
            this.dataSourceBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // outList
            // 
            this.outList.DataSource = null;
            this.outList.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.outList.Location = new System.Drawing.Point(6, 96);
            this.outList.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.outList.Name = "outList";
            this.outList.Size = new System.Drawing.Size(125, 22);
            this.outList.TabIndex = 8;
            // 
            // KeyWordOperatorView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(461, 357);
            this.ControlBox = false;
            this.Controls.Add(this.valuePanel);
            this.Controls.Add(this.keyPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "KeyWordOperatorView";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Citta_T1.Controls.Common.ComCheckBoxList outList;
        private System.Windows.Forms.TextBox keyWordBox;
        private System.Windows.Forms.TextBox dataSourceBox;
        private System.Windows.Forms.ToolTip dataSourceTip;
        private System.Windows.Forms.ToolTip keyWordTip;
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