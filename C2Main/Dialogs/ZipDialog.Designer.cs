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
            this.passwordCheckBox = new System.Windows.Forms.CheckBox();
            this.showPasswordCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // modelPathLabel
            // 
            this.modelPathLabel.AutoSize = true;
            this.modelPathLabel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.modelPathLabel.Location = new System.Drawing.Point(34, 63);
            this.modelPathLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.modelPathLabel.Name = "modelPathLabel";
            this.modelPathLabel.Size = new System.Drawing.Size(73, 21);
            this.modelPathLabel.TabIndex = 10003;
            this.modelPathLabel.Text = "路径：";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passwordLabel.Location = new System.Drawing.Point(34, 144);
            this.passwordLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(73, 21);
            this.passwordLabel.TabIndex = 10004;
            this.passwordLabel.Text = "密码：";
            // 
            // modelPathTextBox
            // 
            this.modelPathTextBox.BackColor = System.Drawing.Color.White;
            this.modelPathTextBox.Location = new System.Drawing.Point(159, 60);
            this.modelPathTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.modelPathTextBox.Name = "modelPathTextBox";
            this.modelPathTextBox.ReadOnly = true;
            this.modelPathTextBox.Size = new System.Drawing.Size(278, 28);
            this.modelPathTextBox.TabIndex = 10005;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Enabled = false;
            this.passwordTextBox.Location = new System.Drawing.Point(159, 141);
            this.passwordTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(278, 28);
            this.passwordTextBox.TabIndex = 10006;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(448, 58);
            this.browseButton.Margin = new System.Windows.Forms.Padding(0);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(72, 36);
            this.browseButton.TabIndex = 10007;
            this.browseButton.Text = "浏览";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // passwordCheckBox
            // 
            this.passwordCheckBox.AutoSize = true;
            this.passwordCheckBox.Location = new System.Drawing.Point(453, 146);
            this.passwordCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.passwordCheckBox.Name = "passwordCheckBox";
            this.passwordCheckBox.Size = new System.Drawing.Size(70, 22);
            this.passwordCheckBox.TabIndex = 10009;
            this.passwordCheckBox.Text = "密码";
            this.passwordCheckBox.UseVisualStyleBackColor = true;
            this.passwordCheckBox.CheckedChanged += new System.EventHandler(this.PasswordCheckBox_CheckedChanged);
            // 
            // showPasswordCheckBox
            // 
            this.showPasswordCheckBox.AutoSize = true;
            this.showPasswordCheckBox.Location = new System.Drawing.Point(453, 188);
            this.showPasswordCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.showPasswordCheckBox.Name = "showPasswordCheckBox";
            this.showPasswordCheckBox.Size = new System.Drawing.Size(70, 22);
            this.showPasswordCheckBox.TabIndex = 10010;
            this.showPasswordCheckBox.Text = "显示";
            this.showPasswordCheckBox.UseVisualStyleBackColor = true;
            this.showPasswordCheckBox.Visible = false;
            this.showPasswordCheckBox.CheckedChanged += new System.EventHandler(this.ShowPasswordCheckBox_CheckedChanged);
            // 
            // ZipDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(555, 302);
            this.Controls.Add(this.showPasswordCheckBox);
            this.Controls.Add(this.passwordCheckBox);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.modelPathTextBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.modelPathLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ZipDialog";
            this.Text = "分析笔记";
            this.Controls.SetChildIndex(this.modelPathLabel, 0);
            this.Controls.SetChildIndex(this.passwordLabel, 0);
            this.Controls.SetChildIndex(this.modelPathTextBox, 0);
            this.Controls.SetChildIndex(this.passwordTextBox, 0);
            this.Controls.SetChildIndex(this.browseButton, 0);
            this.Controls.SetChildIndex(this.passwordCheckBox, 0);
            this.Controls.SetChildIndex(this.showPasswordCheckBox, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label modelPathLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox modelPathTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.CheckBox passwordCheckBox;
        private System.Windows.Forms.CheckBox showPasswordCheckBox;
    }
}