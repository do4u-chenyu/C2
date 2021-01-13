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
            this.userLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.serverTextBox = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.userTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.databaseTypeComboBox = new System.Windows.Forms.ComboBox();
            this.serviceRadiobutton = new System.Windows.Forms.RadioButton();
            this.sidRadiobutton = new System.Windows.Forms.RadioButton();
            this.serviceTextBox = new System.Windows.Forms.TextBox();
            this.sidTextBox = new System.Windows.Forms.TextBox();
            this.TestButton = new System.Windows.Forms.Button();
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
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Location = new System.Drawing.Point(19, 96);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(71, 12);
            this.userLabel.TabIndex = 10006;
            this.userLabel.Text = "用户名（U）";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(19, 134);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(59, 12);
            this.passwordLabel.TabIndex = 10007;
            this.passwordLabel.Text = "密码（P）";
            // 
            // serverTextBox
            // 
            this.serverTextBox.Location = new System.Drawing.Point(100, 56);
            this.serverTextBox.Name = "serverTextBox";
            this.serverTextBox.Size = new System.Drawing.Size(95, 21);
            this.serverTextBox.TabIndex = 10009;
            this.serverTextBox.Text = "114.55.248.85";
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(216, 61);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(59, 12);
            this.portLabel.TabIndex = 10010;
            this.portLabel.Text = "端口（R）";
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(281, 56);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(84, 21);
            this.portTextBox.TabIndex = 10012;
            this.portTextBox.Text = "1521";
            this.portTextBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PortTextBox_MouseUp);
            // 
            // userTextBox
            // 
            this.userTextBox.Location = new System.Drawing.Point(100, 93);
            this.userTextBox.Name = "userTextBox";
            this.userTextBox.Size = new System.Drawing.Size(265, 21);
            this.userTextBox.TabIndex = 10013;
            this.userTextBox.Text = "test";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(100, 131);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(265, 21);
            this.passwordTextBox.TabIndex = 10014;
            this.passwordTextBox.Text = "test";
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
            this.databaseTypeComboBox.TextChanged += new System.EventHandler(this.DatabaseTypeComboBox_TextChanged);
            // 
            // serviceRadiobutton
            // 
            this.serviceRadiobutton.AutoSize = true;
            this.serviceRadiobutton.Location = new System.Drawing.Point(21, 168);
            this.serviceRadiobutton.Name = "serviceRadiobutton";
            this.serviceRadiobutton.Size = new System.Drawing.Size(59, 16);
            this.serviceRadiobutton.TabIndex = 10017;
            this.serviceRadiobutton.Text = "服务名";
            this.serviceRadiobutton.UseVisualStyleBackColor = true;
            // 
            // sidRadiobutton
            // 
            this.sidRadiobutton.AutoSize = true;
            this.sidRadiobutton.Checked = true;
            this.sidRadiobutton.Location = new System.Drawing.Point(21, 200);
            this.sidRadiobutton.Name = "sidRadiobutton";
            this.sidRadiobutton.Size = new System.Drawing.Size(41, 16);
            this.sidRadiobutton.TabIndex = 10018;
            this.sidRadiobutton.TabStop = true;
            this.sidRadiobutton.Text = "SID";
            this.sidRadiobutton.UseVisualStyleBackColor = true;
            // 
            // serviceTextBox
            // 
            this.serviceTextBox.Location = new System.Drawing.Point(100, 167);
            this.serviceTextBox.Name = "serviceTextBox";
            this.serviceTextBox.Size = new System.Drawing.Size(265, 21);
            this.serviceTextBox.TabIndex = 10019;
            // 
            // sidTextBox
            // 
            this.sidTextBox.Location = new System.Drawing.Point(100, 199);
            this.sidTextBox.Name = "sidTextBox";
            this.sidTextBox.Size = new System.Drawing.Size(265, 21);
            this.sidTextBox.TabIndex = 10020;
            this.sidTextBox.Text = "orcl";
            // 
            // TestButton
            // 
            this.TestButton.Location = new System.Drawing.Point(150, 231);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new System.Drawing.Size(75, 27);
            this.TestButton.TabIndex = 10022;
            this.TestButton.Text = "测试";
            this.TestButton.UseVisualStyleBackColor = true;
            this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // AddDatabaseDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(403, 270);
            this.Controls.Add(this.TestButton);
            this.Controls.Add(this.sidTextBox);
            this.Controls.Add(this.serviceTextBox);
            this.Controls.Add(this.sidRadiobutton);
            this.Controls.Add(this.serviceRadiobutton);
            this.Controls.Add(this.databaseTypeComboBox);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.userTextBox);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.serverTextBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.userLabel);
            this.Controls.Add(this.serverLabel);
            this.Controls.Add(this.databaseTypeLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AddDatabaseDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库连接设置";
            this.Controls.SetChildIndex(this.databaseTypeLabel, 0);
            this.Controls.SetChildIndex(this.serverLabel, 0);
            this.Controls.SetChildIndex(this.userLabel, 0);
            this.Controls.SetChildIndex(this.passwordLabel, 0);
            this.Controls.SetChildIndex(this.serverTextBox, 0);
            this.Controls.SetChildIndex(this.portLabel, 0);
            this.Controls.SetChildIndex(this.portTextBox, 0);
            this.Controls.SetChildIndex(this.userTextBox, 0);
            this.Controls.SetChildIndex(this.passwordTextBox, 0);
            this.Controls.SetChildIndex(this.databaseTypeComboBox, 0);
            this.Controls.SetChildIndex(this.serviceRadiobutton, 0);
            this.Controls.SetChildIndex(this.sidRadiobutton, 0);
            this.Controls.SetChildIndex(this.serviceTextBox, 0);
            this.Controls.SetChildIndex(this.sidTextBox, 0);
            this.Controls.SetChildIndex(this.TestButton, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label databaseTypeLabel;
        private System.Windows.Forms.Label serverLabel;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox serverTextBox;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.TextBox userTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.ComboBox databaseTypeComboBox;
        private System.Windows.Forms.RadioButton serviceRadiobutton;
        private System.Windows.Forms.RadioButton sidRadiobutton;
        private System.Windows.Forms.TextBox serviceTextBox;
        private System.Windows.Forms.TextBox sidTextBox;
        private System.Windows.Forms.Button TestButton;
    }
}