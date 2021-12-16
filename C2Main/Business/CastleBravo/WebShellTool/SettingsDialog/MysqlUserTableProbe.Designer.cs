namespace C2.Business.CastleBravo.WebShellTool.SettingsDialog
{
    partial class MysqlUserTableProbe
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
            this.mysqlAccount = new System.Windows.Forms.TextBox();
            this.label0 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mysqlAccount
            // 
            this.mysqlAccount.BackColor = System.Drawing.Color.White;
            this.mysqlAccount.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.mysqlAccount.Location = new System.Drawing.Point(80, 20);
            this.mysqlAccount.Name = "mysqlAccount";
            this.mysqlAccount.Size = new System.Drawing.Size(86, 21);
            this.mysqlAccount.TabIndex = 10035;
            this.mysqlAccount.Text = "用户名,密码";
            // 
            // label0
            // 
            this.label0.AutoSize = true;
            this.label0.Location = new System.Drawing.Point(10, 24);
            this.label0.Name = "label0";
            this.label0.Size = new System.Drawing.Size(65, 12);
            this.label0.TabIndex = 10036;
            this.label0.Text = "Mysql账户:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(175, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 10037;
            this.label1.Text = "(选填)";
            // 
            // MysqlUserTableProbe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(225, 96);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mysqlAccount);
            this.Controls.Add(this.label0);
            this.Name = "MysqlUserTableProbe";
            this.Text = "User表探针";
            this.Controls.SetChildIndex(this.label0, 0);
            this.Controls.SetChildIndex(this.mysqlAccount, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox mysqlAccount;
        private System.Windows.Forms.Label label0;
        private System.Windows.Forms.Label label1;
    }
}