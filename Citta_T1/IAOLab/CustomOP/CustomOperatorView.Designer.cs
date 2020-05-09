namespace Citta_T1.OperatorViews
{
    partial class CustomOperatorView
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
            this.confirmButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dataSource0 = new System.Windows.Forms.TextBox();
            this.dataSource1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.fixRadioButton = new System.Windows.Forms.RadioButton();
            this.fixSecondTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.randomRadioButton = new System.Windows.Forms.RadioButton();
            this.randomBeginTextBox = new System.Windows.Forms.TextBox();
            this.randomEndTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.outList0 = new UserControlDLL.ComCheckBoxList();
            this.outList1 = new UserControlDLL.ComCheckBoxList();
            this.label7 = new System.Windows.Forms.Label();
            this.rsFullFilePathTextBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // confirmButton
            // 
            this.confirmButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.confirmButton.FlatAppearance.BorderSize = 0;
            this.confirmButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.confirmButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.confirmButton.Location = new System.Drawing.Point(295, 261);
            this.confirmButton.Margin = new System.Windows.Forms.Padding(2);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(60, 27);
            this.confirmButton.TabIndex = 1;
            this.confirmButton.Text = "确认";
            this.confirmButton.UseVisualStyleBackColor = false;
            this.confirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.cancelButton.Location = new System.Drawing.Point(391, 261);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(63, 27);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(5, 40);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 22);
            this.label1.TabIndex = 3;
            this.label1.Text = "数据信息：";
            // 
            // dataSource0
            // 
            this.dataSource0.Location = new System.Drawing.Point(96, 41);
            this.dataSource0.Margin = new System.Windows.Forms.Padding(2);
            this.dataSource0.Name = "dataSource0";
            this.dataSource0.ReadOnly = true;
            this.dataSource0.Size = new System.Drawing.Size(150, 21);
            this.dataSource0.TabIndex = 10;
            this.dataSource0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dataSource1
            // 
            this.dataSource1.Location = new System.Drawing.Point(302, 41);
            this.dataSource1.Margin = new System.Windows.Forms.Padding(2);
            this.dataSource1.Name = "dataSource1";
            this.dataSource1.ReadOnly = true;
            this.dataSource1.Size = new System.Drawing.Size(150, 21);
            this.dataSource1.TabIndex = 11;
            this.dataSource1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataSource1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(5, 89);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 22);
            this.label2.TabIndex = 12;
            this.label2.Text = "设定时间：";
            // 
            // fixRadioButton
            // 
            this.fixRadioButton.AutoSize = true;
            this.fixRadioButton.Checked = true;
            this.fixRadioButton.Location = new System.Drawing.Point(96, 93);
            this.fixRadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.fixRadioButton.Name = "fixRadioButton";
            this.fixRadioButton.Size = new System.Drawing.Size(47, 16);
            this.fixRadioButton.TabIndex = 13;
            this.fixRadioButton.TabStop = true;
            this.fixRadioButton.Text = "固定";
            this.fixRadioButton.UseVisualStyleBackColor = true;
            // 
            // fixSecondTextBox
            // 
            this.fixSecondTextBox.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fixSecondTextBox.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.fixSecondTextBox.Location = new System.Drawing.Point(142, 89);
            this.fixSecondTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.fixSecondTextBox.Name = "fixSecondTextBox";
            this.fixSecondTextBox.Size = new System.Drawing.Size(31, 22);
            this.fixSecondTextBox.TabIndex = 15;
            this.fixSecondTextBox.Text = "30";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(178, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 17);
            this.label3.TabIndex = 16;
            this.label3.Text = "秒(默认30秒)";
            // 
            // randomRadioButton
            // 
            this.randomRadioButton.AutoSize = true;
            this.randomRadioButton.Location = new System.Drawing.Point(291, 93);
            this.randomRadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.randomRadioButton.Name = "randomRadioButton";
            this.randomRadioButton.Size = new System.Drawing.Size(47, 16);
            this.randomRadioButton.TabIndex = 17;
            this.randomRadioButton.Text = "随机";
            this.randomRadioButton.UseVisualStyleBackColor = true;
            // 
            // randomBeginTextBox
            // 
            this.randomBeginTextBox.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.randomBeginTextBox.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.randomBeginTextBox.Location = new System.Drawing.Point(342, 89);
            this.randomBeginTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.randomBeginTextBox.Name = "randomBeginTextBox";
            this.randomBeginTextBox.Size = new System.Drawing.Size(31, 22);
            this.randomBeginTextBox.TabIndex = 18;
            this.randomBeginTextBox.Text = "10";
            // 
            // randomEndTextBox
            // 
            this.randomEndTextBox.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.randomEndTextBox.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.randomEndTextBox.Location = new System.Drawing.Point(412, 89);
            this.randomEndTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.randomEndTextBox.Name = "randomEndTextBox";
            this.randomEndTextBox.Size = new System.Drawing.Size(31, 22);
            this.randomEndTextBox.TabIndex = 19;
            this.randomEndTextBox.Text = "60";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(384, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 17);
            this.label4.TabIndex = 20;
            this.label4.Text = "到";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(449, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 17);
            this.label5.TabIndex = 21;
            this.label5.Text = "秒";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(5, 138);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 22);
            this.label6.TabIndex = 22;
            this.label6.Text = "输出字段：";
            // 
            // outList0
            // 
            this.outList0.DataSource = null;
            this.outList0.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.outList0.Location = new System.Drawing.Point(96, 138);
            this.outList0.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.outList0.Name = "outList0";
            this.outList0.Size = new System.Drawing.Size(150, 22);
            this.outList0.TabIndex = 23;
            // 
            // outList1
            // 
            this.outList1.DataSource = null;
            this.outList1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.outList1.Location = new System.Drawing.Point(302, 138);
            this.outList1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.outList1.Name = "outList1";
            this.outList1.Size = new System.Drawing.Size(150, 22);
            this.outList1.TabIndex = 24;
            this.outList1.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(5, 187);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 22);
            this.label7.TabIndex = 25;
            this.label7.Text = "结果文件：";
            // 
            // rsFullFilePathTextBox
            // 
            this.rsFullFilePathTextBox.BackColor = System.Drawing.Color.White;
            this.rsFullFilePathTextBox.Location = new System.Drawing.Point(96, 187);
            this.rsFullFilePathTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.rsFullFilePathTextBox.Name = "rsFullFilePathTextBox";
            this.rsFullFilePathTextBox.ReadOnly = true;
            this.rsFullFilePathTextBox.Size = new System.Drawing.Size(320, 21);
            this.rsFullFilePathTextBox.TabIndex = 26;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(421, 186);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(43, 23);
            this.browseButton.TabIndex = 27;
            this.browseButton.Text = "浏览+";
            this.browseButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.browseButton.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // CustomOperatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 305);
            this.ControlBox = false;
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.rsFullFilePathTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.outList1);
            this.Controls.Add(this.outList0);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.randomEndTextBox);
            this.Controls.Add(this.randomBeginTextBox);
            this.Controls.Add(this.randomRadioButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.fixSecondTextBox);
            this.Controls.Add(this.fixRadioButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataSource1);
            this.Controls.Add(this.dataSource0);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.confirmButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomOperatorView";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自定义算子设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox dataSource0;
        private System.Windows.Forms.TextBox dataSource1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton fixRadioButton;
        private System.Windows.Forms.TextBox fixSecondTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton randomRadioButton;
        private System.Windows.Forms.TextBox randomBeginTextBox;
        private System.Windows.Forms.TextBox randomEndTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private UserControlDLL.ComCheckBoxList outList0;
        private UserControlDLL.ComCheckBoxList outList1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox rsFullFilePathTextBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}