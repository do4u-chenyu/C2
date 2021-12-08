namespace C2.Business.CastleBravo.WebShellTool
{
    partial class MSFSet
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
            this.help1 = new System.Windows.Forms.Label();
            this.help2 = new System.Windows.Forms.Label();
            this.addr = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rhTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // help1
            // 
            this.help1.AutoSize = true;
            this.help1.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.help1.Location = new System.Drawing.Point(12, 76);
            this.help1.Name = "help1";
            this.help1.Size = new System.Drawing.Size(89, 12);
            this.help1.TabIndex = 10038;
            this.help1.Text = "输入地址,例如:";
            // 
            // help2
            // 
            this.help2.AutoSize = true;
            this.help2.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.help2.Location = new System.Drawing.Point(12, 94);
            this.help2.Name = "help2";
            this.help2.Size = new System.Drawing.Size(101, 12);
            this.help2.TabIndex = 10037;
            this.help2.Text = "103.43.17.9:8889";
            // 
            // addr
            // 
            this.addr.AutoSize = true;
            this.addr.Location = new System.Drawing.Point(13, 24);
            this.addr.Name = "addr";
            this.addr.Size = new System.Drawing.Size(53, 12);
            this.addr.TabIndex = 10036;
            this.addr.Text = "MSF地址:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(12, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 12);
            this.label4.TabIndex = 10035;
            this.label4.Text = "此设置不保存,用前每次设置一下";
            // 
            // rhTextBox
            // 
            this.rhTextBox.ForeColor = System.Drawing.Color.Black;
            this.rhTextBox.Location = new System.Drawing.Point(77, 19);
            this.rhTextBox.Name = "rhTextBox";
            this.rhTextBox.Size = new System.Drawing.Size(205, 21);
            this.rhTextBox.TabIndex = 10034;
            // 
            // MSFSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 167);
            this.Controls.Add(this.help1);
            this.Controls.Add(this.help2);
            this.Controls.Add(this.addr);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rhTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MSFSet";
            this.Text = "MSF配置";
            this.Controls.SetChildIndex(this.rhTextBox, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.addr, 0);
            this.Controls.SetChildIndex(this.help2, 0);
            this.Controls.SetChildIndex(this.help1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label help1;
        private System.Windows.Forms.Label help2;
        protected System.Windows.Forms.Label addr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox rhTextBox;
    }
}