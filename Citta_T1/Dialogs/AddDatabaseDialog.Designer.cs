namespace C2.Dialogs
{
    partial class AddDatabaseDialog
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
            this.databaseTypeLabel = new System.Windows.Forms.Label();
            this.serverLabel = new System.Windows.Forms.Label();
            this.serviceLabel = new System.Windows.Forms.Label();
            this.userLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.serverTextBox = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.serviceTextBox = new System.Windows.Forms.TextBox();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.userTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.databaseTypeComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // databaseTypeLabel
            // 
            this.databaseTypeLabel.AutoSize = true;
            this.databaseTypeLabel.Location = new System.Drawing.Point(19, 23);
            this.databaseTypeLabel.Name = "databaseTypeLabel";
            this.databaseTypeLabel.Size = new System.Drawing.Size(65, 12);
            this.databaseTypeLabel.TabIndex = 10003;
            this.databaseTypeLabel.Text = "数据库类型";
            // 
            // serverLabel
            // 
            this.serverLabel.AutoSize = true;
            this.serverLabel.Location = new System.Drawing.Point(19, 59);
            this.serverLabel.Name = "serverLabel";
            this.serverLabel.Size = new System.Drawing.Size(71, 12);
            this.serverLabel.TabIndex = 10004;
            this.serverLabel.Text = "服务器（V）";
            // 
            // serviceLabel
            // 
            this.serviceLabel.AutoSize = true;
            this.serviceLabel.Location = new System.Drawing.Point(19, 95);
            this.serviceLabel.Name = "serviceLabel";
            this.serviceLabel.Size = new System.Drawing.Size(53, 12);
            this.serviceLabel.TabIndex = 10005;
            this.serviceLabel.Text = "服务（S)";
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Location = new System.Drawing.Point(19, 132);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(71, 12);
            this.userLabel.TabIndex = 10006;
            this.userLabel.Text = "用户名（U）";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(19, 170);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(59, 12);
            this.passwordLabel.TabIndex = 10007;
            this.passwordLabel.Text = "密码（P）";
            // 
            // serverTextBox
            // 
            this.serverTextBox.Location = new System.Drawing.Point(100, 56);
            this.serverTextBox.Name = "serverTextBox";
            this.serverTextBox.Size = new System.Drawing.Size(265, 21);
            this.serverTextBox.TabIndex = 10009;
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(216, 95);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(59, 12);
            this.portLabel.TabIndex = 10010;
            this.portLabel.Text = "端口（R）";
            // 
            // serviceTextBox
            // 
            this.serviceTextBox.Location = new System.Drawing.Point(100, 90);
            this.serviceTextBox.Name = "serviceTextBox";
            this.serviceTextBox.Size = new System.Drawing.Size(84, 21);
            this.serviceTextBox.TabIndex = 10011;
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(281, 90);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(84, 21);
            this.portTextBox.TabIndex = 10012;
            // 
            // userTextBox
            // 
            this.userTextBox.Location = new System.Drawing.Point(100, 129);
            this.userTextBox.Name = "userTextBox";
            this.userTextBox.Size = new System.Drawing.Size(265, 21);
            this.userTextBox.TabIndex = 10013;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(100, 167);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(265, 21);
            this.passwordTextBox.TabIndex = 10014;
            // 
            // databaseTypeComboBox
            // 
            this.databaseTypeComboBox.FormattingEnabled = true;
            this.databaseTypeComboBox.Items.AddRange(new object[] {
            "Oracle",
            "Hive"});
            this.databaseTypeComboBox.Location = new System.Drawing.Point(100, 20);
            this.databaseTypeComboBox.Name = "databaseTypeComboBox";
            this.databaseTypeComboBox.Size = new System.Drawing.Size(265, 20);
            this.databaseTypeComboBox.TabIndex = 10015;
            // 
            // AddDatabaseDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(403, 270);
            this.Controls.Add(this.databaseTypeComboBox);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.userTextBox);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.serviceTextBox);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.serverTextBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.userLabel);
            this.Controls.Add(this.serviceLabel);
            this.Controls.Add(this.serverLabel);
            this.Controls.Add(this.databaseTypeLabel);
            this.Name = "AddDatabaseDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新增数据源";
            this.Controls.SetChildIndex(this.databaseTypeLabel, 0);
            this.Controls.SetChildIndex(this.serverLabel, 0);
            this.Controls.SetChildIndex(this.serviceLabel, 0);
            this.Controls.SetChildIndex(this.userLabel, 0);
            this.Controls.SetChildIndex(this.passwordLabel, 0);
            this.Controls.SetChildIndex(this.serverTextBox, 0);
            this.Controls.SetChildIndex(this.portLabel, 0);
            this.Controls.SetChildIndex(this.serviceTextBox, 0);
            this.Controls.SetChildIndex(this.portTextBox, 0);
            this.Controls.SetChildIndex(this.userTextBox, 0);
            this.Controls.SetChildIndex(this.passwordTextBox, 0);
            this.Controls.SetChildIndex(this.databaseTypeComboBox, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label databaseTypeLabel;
        private System.Windows.Forms.Label serverLabel;
        private System.Windows.Forms.Label serviceLabel;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox serverTextBox;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.TextBox serviceTextBox;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.TextBox userTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.ComboBox databaseTypeComboBox;
    }
}