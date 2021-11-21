namespace C2.Business.CastleBravo.WebShellTool
{
    partial class InfoCollectionSet
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
            this.addrTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.addr = new System.Windows.Forms.Label();
            this.help3 = new System.Windows.Forms.Label();
            this.help2 = new System.Windows.Forms.Label();
            this.help1 = new System.Windows.Forms.Label();
            this.accountItem = new System.Windows.Forms.Label();
            this.mysqlAccount = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addrTextBox
            // 
            this.addrTextBox.ForeColor = System.Drawing.Color.Black;
            this.addrTextBox.Location = new System.Drawing.Point(77, 51);
            this.addrTextBox.Name = "addrTextBox";
            this.addrTextBox.Size = new System.Drawing.Size(205, 21);
            this.addrTextBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 10023;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(12, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 12);
            this.label4.TabIndex = 10027;
            this.label4.Text = "此设置不保存,用前每次设置一下";
            // 
            // addr
            // 
            this.addr.AutoSize = true;
            this.addr.Location = new System.Drawing.Point(12, 54);
            this.addr.Name = "addr";
            this.addr.Size = new System.Drawing.Size(59, 12);
            this.addr.TabIndex = 10028;
            this.addr.Text = "字典地址:";
            // 
            // help3
            // 
            this.help3.AutoSize = true;
            this.help3.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.help3.Location = new System.Drawing.Point(12, 142);
            this.help3.Name = "help3";
            this.help3.Size = new System.Drawing.Size(131, 12);
            this.help3.TabIndex = 10031;
            this.help3.Text = "2）C:/www/usr/db_dict";
            // 
            // help2
            // 
            this.help2.AutoSize = true;
            this.help2.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.help2.Location = new System.Drawing.Point(12, 126);
            this.help2.Name = "help2";
            this.help2.Size = new System.Drawing.Size(197, 12);
            this.help2.TabIndex = 10032;
            this.help2.Text = "1）http://103.43.17.9/wk/db_dict";
            // 
            // help1
            // 
            this.help1.AutoSize = true;
            this.help1.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.help1.Location = new System.Drawing.Point(12, 108);
            this.help1.Name = "help1";
            this.help1.Size = new System.Drawing.Size(149, 12);
            this.help1.TabIndex = 10033;
            this.help1.Text = "输入地址类型(远程/本地):";
            // 
            // accountItem
            // 
            this.accountItem.AutoSize = true;
            this.accountItem.Location = new System.Drawing.Point(12, 23);
            this.accountItem.Name = "accountItem";
            this.accountItem.Size = new System.Drawing.Size(59, 12);
            this.accountItem.TabIndex = 10034;
            this.accountItem.Text = "素描账户:";
            // 
            // mysqlAccount
            // 
            this.mysqlAccount.ForeColor = System.Drawing.Color.Black;
            this.mysqlAccount.Location = new System.Drawing.Point(77, 20);
            this.mysqlAccount.Name = "mysqlAccount";
            this.mysqlAccount.Size = new System.Drawing.Size(205, 21);
            this.mysqlAccount.TabIndex = 1;
            // 
            // InfoCollectionSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(315, 218);
            this.Controls.Add(this.mysqlAccount);
            this.Controls.Add(this.accountItem);
            this.Controls.Add(this.help1);
            this.Controls.Add(this.help2);
            this.Controls.Add(this.help3);
            this.Controls.Add(this.addr);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.addrTextBox);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "InfoCollectionSet";
            this.Text = "后信息收集配置";
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.addrTextBox, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.addr, 0);
            this.Controls.SetChildIndex(this.help3, 0);
            this.Controls.SetChildIndex(this.help2, 0);
            this.Controls.SetChildIndex(this.help1, 0);
            this.Controls.SetChildIndex(this.accountItem, 0);
            this.Controls.SetChildIndex(this.mysqlAccount, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox addrTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label addr;
        private System.Windows.Forms.Label help3;
        private System.Windows.Forms.Label help2;
        private System.Windows.Forms.Label help1;
        private System.Windows.Forms.Label accountItem;
        private System.Windows.Forms.TextBox mysqlAccount;
    }
}