namespace C2.Business.CastleBravo.WebShellTool
{
    partial class AddAllWebShellForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.filePathTextBox = new System.Windows.Forms.TextBox();
            this.browserButton = new System.Windows.Forms.Button();
            this.pasteModeCB = new System.Windows.Forms.CheckBox();
            this.wsTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.trojan = new System.Windows.Forms.RadioButton();
            this.behinder3 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(16, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 19);
            this.label2.TabIndex = 10005;
            this.label2.Text = "文件路径：";
            // 
            // filePathTextBox
            // 
            this.filePathTextBox.BackColor = System.Drawing.Color.White;
            this.filePathTextBox.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.filePathTextBox.Location = new System.Drawing.Point(96, 29);
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.ReadOnly = true;
            this.filePathTextBox.Size = new System.Drawing.Size(263, 25);
            this.filePathTextBox.TabIndex = 10007;
            // 
            // browserButton
            // 
            this.browserButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.browserButton.Location = new System.Drawing.Point(365, 30);
            this.browserButton.Name = "browserButton";
            this.browserButton.Size = new System.Drawing.Size(59, 23);
            this.browserButton.TabIndex = 10008;
            this.browserButton.Text = "+浏览";
            this.browserButton.UseVisualStyleBackColor = true;
            this.browserButton.Click += new System.EventHandler(this.BrowserButton_Click);
            // 
            // pasteModeCB
            // 
            this.pasteModeCB.AutoSize = true;
            this.pasteModeCB.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.pasteModeCB.Location = new System.Drawing.Point(11, 114);
            this.pasteModeCB.Name = "pasteModeCB";
            this.pasteModeCB.Size = new System.Drawing.Size(75, 21);
            this.pasteModeCB.TabIndex = 10015;
            this.pasteModeCB.Text = "粘贴模式";
            this.pasteModeCB.UseVisualStyleBackColor = true;
            this.pasteModeCB.CheckedChanged += new System.EventHandler(this.PasteModeCB_CheckedChanged);
            // 
            // wsTextBox
            // 
            this.wsTextBox.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.wsTextBox.Location = new System.Drawing.Point(95, 111);
            this.wsTextBox.MaxLength = 4194304;
            this.wsTextBox.Multiline = true;
            this.wsTextBox.Name = "wsTextBox";
            this.wsTextBox.ReadOnly = true;
            this.wsTextBox.Size = new System.Drawing.Size(329, 159);
            this.wsTextBox.TabIndex = 10016;
            this.wsTextBox.WordWrap = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(3, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(431, 12);
            this.label3.TabIndex = 10017;
            this.label3.Text = "* 内容格式:TAB分割,第一列url(http或https开头),第二列密码,一行一个,10W行";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(3, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 10018;
            this.label1.Text = "* 最大 4M 文本";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(16, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 19);
            this.label4.TabIndex = 10019;
            this.label4.Text = "木马类型：";
            // 
            // trojan
            // 
            this.trojan.AutoSize = true;
            this.trojan.Checked = true;
            this.trojan.Location = new System.Drawing.Point(6, 14);
            this.trojan.Name = "trojan";
            this.trojan.Size = new System.Drawing.Size(71, 16);
            this.trojan.TabIndex = 10020;
            this.trojan.TabStop = true;
            this.trojan.Text = "通用木马";
            this.trojan.UseVisualStyleBackColor = true;
            // 
            // behinder3
            // 
            this.behinder3.AutoSize = true;
            this.behinder3.Location = new System.Drawing.Point(105, 14);
            this.behinder3.Name = "behinder3";
            this.behinder3.Size = new System.Drawing.Size(71, 16);
            this.behinder3.TabIndex = 10021;
            this.behinder3.Text = "三代冰蝎";
            this.behinder3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.behinder3);
            this.groupBox1.Controls.Add(this.trojan);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox1.Location = new System.Drawing.Point(96, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 36);
            this.groupBox1.TabIndex = 10022;
            this.groupBox1.TabStop = false;
            this.groupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // AddAllWebShellForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(437, 325);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.wsTextBox);
            this.Controls.Add(this.pasteModeCB);
            this.Controls.Add(this.browserButton);
            this.Controls.Add(this.filePathTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Name = "AddAllWebShellForm";
            this.Text = "批量添加";
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.filePathTextBox, 0);
            this.Controls.SetChildIndex(this.browserButton, 0);
            this.Controls.SetChildIndex(this.pasteModeCB, 0);
            this.Controls.SetChildIndex(this.wsTextBox, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox filePathTextBox;
        private System.Windows.Forms.Button browserButton;
        private System.Windows.Forms.CheckBox pasteModeCB;
        private System.Windows.Forms.TextBox wsTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton trojan;
        private System.Windows.Forms.RadioButton behinder3;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}