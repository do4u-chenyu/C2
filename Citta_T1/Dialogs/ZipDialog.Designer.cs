namespace C2.Dialogs
{
    partial class ZipDialog
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
            this.modelPathLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.modelPathTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.tip1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // modelPathLabel
            // 
            this.modelPathLabel.AutoSize = true;
            this.modelPathLabel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.modelPathLabel.Location = new System.Drawing.Point(23, 42);
            this.modelPathLabel.Name = "modelPathLabel";
            this.modelPathLabel.Size = new System.Drawing.Size(77, 14);
            this.modelPathLabel.TabIndex = 10003;
            this.modelPathLabel.Text = "模型路径：";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passwordLabel.Location = new System.Drawing.Point(23, 96);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(49, 14);
            this.passwordLabel.TabIndex = 10004;
            this.passwordLabel.Text = "密码：";
            // 
            // modelPathTextBox
            // 
            this.modelPathTextBox.BackColor = System.Drawing.Color.White;
            this.modelPathTextBox.Location = new System.Drawing.Point(106, 40);
            this.modelPathTextBox.Name = "modelPathTextBox";
            this.modelPathTextBox.ReadOnly = true;
            this.modelPathTextBox.Size = new System.Drawing.Size(187, 21);
            this.modelPathTextBox.TabIndex = 10005;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(106, 94);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(187, 21);
            this.passwordTextBox.TabIndex = 10006;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(299, 39);
            this.browseButton.Margin = new System.Windows.Forms.Padding(0);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(49, 24);
            this.browseButton.TabIndex = 10007;
            this.browseButton.Text = "浏览";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // tip1
            // 
            this.tip1.AutoSize = true;
            this.tip1.Location = new System.Drawing.Point(301, 99);
            this.tip1.Name = "tip1";
            this.tip1.Size = new System.Drawing.Size(59, 12);
            this.tip1.TabIndex = 10008;
            this.tip1.Text = "(非必填）";
            // 
            // ZipDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(382, 201);
            this.Controls.Add(this.tip1);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.modelPathTextBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.modelPathLabel);
            this.Name = "ZipDialog";
            this.Text = "业务视图";
            this.Controls.SetChildIndex(this.modelPathLabel, 0);
            this.Controls.SetChildIndex(this.passwordLabel, 0);
            this.Controls.SetChildIndex(this.modelPathTextBox, 0);
            this.Controls.SetChildIndex(this.passwordTextBox, 0);
            this.Controls.SetChildIndex(this.browseButton, 0);
            this.Controls.SetChildIndex(this.tip1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label modelPathLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox modelPathTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Label tip1;
    }
}