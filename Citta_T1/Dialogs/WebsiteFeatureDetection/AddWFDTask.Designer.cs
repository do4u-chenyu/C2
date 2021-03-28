namespace C2.Dialogs.WebsiteFeatureDetection
{
    partial class AddWFDTask
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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(32, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 19);
            this.label1.TabIndex = 10003;
            this.label1.Text = "任务名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(19, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 19);
            this.label2.TabIndex = 10004;
            this.label2.Text = "查询文件：";
            // 
            // taskNameTextBox
            // 
            this.taskNameTextBox.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.taskNameTextBox.Location = new System.Drawing.Point(103, 36);
            this.taskNameTextBox.Name = "taskNameTextBox";
            this.taskNameTextBox.Size = new System.Drawing.Size(247, 25);
            this.taskNameTextBox.TabIndex = 10005;
            // 
            // filePathTextBox
            // 
            this.filePathTextBox.BackColor = System.Drawing.Color.White;
            this.filePathTextBox.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.filePathTextBox.Location = new System.Drawing.Point(103, 84);
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.ReadOnly = true;
            this.filePathTextBox.Size = new System.Drawing.Size(247, 25);
            this.filePathTextBox.TabIndex = 10006;
            // 
            // browserButton
            // 
            this.browserButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.browserButton.Location = new System.Drawing.Point(363, 84);
            this.browserButton.Name = "browserButton";
            this.browserButton.Size = new System.Drawing.Size(59, 23);
            this.browserButton.TabIndex = 10007;
            this.browserButton.Text = "+预览";
            this.browserButton.UseVisualStyleBackColor = true;
            this.browserButton.Click += new System.EventHandler(this.BrowserButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(101, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 12);
            this.label3.TabIndex = 10008;
            this.label3.Text = "* 文件内容格式：一行一个url";
            // 
            // ofd
            // 
            this.ofd.Filter = "\"files|*.txt;*.bcp;*.csv\"";
            // 
            // AddWFDTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(434, 207);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.browserButton);
            this.Controls.Add(this.filePathTextBox);
            this.Controls.Add(this.taskNameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AddWFDTask";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "侦察兵-新建任务";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.taskNameTextBox, 0);
            this.Controls.SetChildIndex(this.filePathTextBox, 0);
            this.Controls.SetChildIndex(this.browserButton, 0);
            this.Controls.SetChildIndex(this.label3, 0);
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
    }
}