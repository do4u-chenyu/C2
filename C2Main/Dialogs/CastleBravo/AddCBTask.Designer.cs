﻿namespace C2.Dialogs.CastleBravo
{
    partial class AddCBTask
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.taskNameTextBox = new System.Windows.Forms.TextBox();
            this.filePathTextBox = new System.Windows.Forms.TextBox();
            this.browserButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.taskComboBox = new System.Windows.Forms.ComboBox();
            this.modeComboBox = new System.Windows.Forms.ComboBox();
            this.md5TextBox = new System.Windows.Forms.TextBox();
            this.pasteModeCB = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(25, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 19);
            this.label1.TabIndex = 10003;
            this.label1.Text = "任务名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(11, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 19);
            this.label2.TabIndex = 10004;
            this.label2.Text = "查询文件：";
            // 
            // taskNameTextBox
            // 
            this.taskNameTextBox.Location = new System.Drawing.Point(96, 56);
            this.taskNameTextBox.Name = "taskNameTextBox";
            this.taskNameTextBox.Size = new System.Drawing.Size(279, 21);
            this.taskNameTextBox.TabIndex = 10005;
            // 
            // filePathTextBox
            // 
            this.filePathTextBox.Location = new System.Drawing.Point(96, 100);
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.Size = new System.Drawing.Size(279, 21);
            this.filePathTextBox.TabIndex = 10006;
            // 
            // browserButton
            // 
            this.browserButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.browserButton.Location = new System.Drawing.Point(383, 100);
            this.browserButton.Name = "browserButton";
            this.browserButton.Size = new System.Drawing.Size(73, 24);
            this.browserButton.TabIndex = 10007;
            this.browserButton.Text = "+浏览";
            this.browserButton.UseVisualStyleBackColor = true;
            this.browserButton.Click += new System.EventHandler(this.BrowserButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(94, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(203, 12);
            this.label3.TabIndex = 10008;
            this.label3.Text = "*文件内容格式，一个hash字符串一行";
            // 
            // ofd
            // 
            this.ofd.Filter = "数据文件|*.txt;*.bcp;*.csv;*.tsv|所有文件|*.*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(11, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 19);
            this.label4.TabIndex = 10009;
            this.label4.Text = "任务类型：";
            // 
            // taskComboBox
            // 
            this.taskComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.taskComboBox.FormattingEnabled = true;
            this.taskComboBox.ItemHeight = 12;
            this.taskComboBox.Items.AddRange(new object[] {
            "Hash逆向分析",
            "目标侦察(施工中...)"});
            this.taskComboBox.Location = new System.Drawing.Point(96, 13);
            this.taskComboBox.Name = "taskComboBox";
            this.taskComboBox.Size = new System.Drawing.Size(279, 20);
            this.taskComboBox.TabIndex = 10010;
            this.taskComboBox.SelectedIndexChanged += new System.EventHandler(this.TaskComboBox_SelectedIndexChanged);
            // 
            // modeComboBox
            // 
            this.modeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modeComboBox.DropDownWidth = 105;
            this.modeComboBox.Font = new System.Drawing.Font("宋体", 9F);
            this.modeComboBox.FormattingEnabled = true;
            this.modeComboBox.ItemHeight = 12;
            this.modeComboBox.Items.AddRange(new object[] {
            "自动",
            "MD5",
            "MD5(SHA1($pwd))",
            "MD5(MD5($pwd))",
            "MD5(MD5(MD5($pwd)))",
            "MD5(SHA256($pwd))",
            "MD5(SHA512($pwd))",
            "SHA1",
            "SHA1(MD5($pwd))",
            "SHA256",
            "SHA512",
            "宝塔面板",
            "Mysql5"});
            this.modeComboBox.Location = new System.Drawing.Point(383, 57);
            this.modeComboBox.Name = "modeComboBox";
            this.modeComboBox.Size = new System.Drawing.Size(73, 20);
            this.modeComboBox.TabIndex = 10011;
            // 
            // md5TextBox
            // 
            this.md5TextBox.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.md5TextBox.Location = new System.Drawing.Point(96, 149);
            this.md5TextBox.Multiline = true;
            this.md5TextBox.Name = "md5TextBox";
            this.md5TextBox.ReadOnly = true;
            this.md5TextBox.Size = new System.Drawing.Size(279, 117);
            this.md5TextBox.TabIndex = 10012;
            this.md5TextBox.Text = "粘贴模式可以直接Ctrl+V内容后创建任务";
            this.md5TextBox.WordWrap = false;
            // 
            // pasteModeCB
            // 
            this.pasteModeCB.AutoSize = true;
            this.pasteModeCB.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.pasteModeCB.Location = new System.Drawing.Point(6, 149);
            this.pasteModeCB.Name = "pasteModeCB";
            this.pasteModeCB.Size = new System.Drawing.Size(75, 21);
            this.pasteModeCB.TabIndex = 10013;
            this.pasteModeCB.Text = "粘贴模式";
            this.pasteModeCB.UseVisualStyleBackColor = true;
            this.pasteModeCB.CheckedChanged += new System.EventHandler(this.PasteModeCB_CheckedChanged);
            // 
            // AddCBTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(470, 318);
            this.Controls.Add(this.pasteModeCB);
            this.Controls.Add(this.md5TextBox);
            this.Controls.Add(this.modeComboBox);
            this.Controls.Add(this.taskComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.browserButton);
            this.Controls.Add(this.filePathTextBox);
            this.Controls.Add(this.taskNameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AddCBTask";
            this.Text = "喝彩城堡-新建任务";
            this.Load += new System.EventHandler(this.AddCBTask_Load);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.taskNameTextBox, 0);
            this.Controls.SetChildIndex(this.filePathTextBox, 0);
            this.Controls.SetChildIndex(this.browserButton, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.taskComboBox, 0);
            this.Controls.SetChildIndex(this.modeComboBox, 0);
            this.Controls.SetChildIndex(this.md5TextBox, 0);
            this.Controls.SetChildIndex(this.pasteModeCB, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox taskNameTextBox;
        private System.Windows.Forms.TextBox filePathTextBox;
        private System.Windows.Forms.Button browserButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox taskComboBox;
        private System.Windows.Forms.ComboBox modeComboBox;
        private System.Windows.Forms.TextBox md5TextBox;
        private System.Windows.Forms.CheckBox pasteModeCB;
    }
}