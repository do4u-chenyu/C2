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
            this.useSetComboBox = new System.Windows.Forms.ComboBox();
            this.addrTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.useSet = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.addr = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // useSetComboBox
            // 
            this.useSetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.useSetComboBox.FormattingEnabled = true;
            this.useSetComboBox.Items.AddRange(new object[] {
            "否",
            "是"});
            this.useSetComboBox.Location = new System.Drawing.Point(63, 50);
            this.useSetComboBox.Name = "useSetComboBox";
            this.useSetComboBox.Size = new System.Drawing.Size(205, 20);
            this.useSetComboBox.TabIndex = 10026;
            // 
            // addrTextBox
            // 
            this.addrTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.addrTextBox.Location = new System.Drawing.Point(63, 87);
            this.addrTextBox.Name = "addrTextBox";
            this.addrTextBox.Size = new System.Drawing.Size(205, 21);
            this.addrTextBox.TabIndex = 10024;
            this.addrTextBox.Text = "http://xxx.com/";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 10023;
            // 
            // useSet
            // 
            this.useSet.AutoSize = true;
            this.useSet.Location = new System.Drawing.Point(16, 50);
            this.useSet.Name = "useSet";
            this.useSet.Size = new System.Drawing.Size(41, 12);
            this.useSet.TabIndex = 10021;
            this.useSet.Text = "启用：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(16, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 12);
            this.label4.TabIndex = 10027;
            this.label4.Text = "此设置不保存,用前每次设置一下";
            // 
            // addr
            // 
            this.addr.AutoSize = true;
            this.addr.Location = new System.Drawing.Point(16, 90);
            this.addr.Name = "addr";
            this.addr.Size = new System.Drawing.Size(29, 12);
            this.addr.TabIndex = 10028;
            this.addr.Text = "地址";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::C2.Properties.Resources.help;
            this.pictureBox1.Location = new System.Drawing.Point(274, 85);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 23);
            this.pictureBox1.TabIndex = 10029;
            this.pictureBox1.TabStop = false;
            // 
            // InfoCollectionSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(315, 170);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.addr);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.useSetComboBox);
            this.Controls.Add(this.addrTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.useSet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "InfoCollectionSet";
            this.Text = "后信息收集设置";
            this.Controls.SetChildIndex(this.useSet, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.addrTextBox, 0);
            this.Controls.SetChildIndex(this.useSetComboBox, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.addr, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox useSetComboBox;
        private System.Windows.Forms.TextBox addrTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label useSet;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label addr;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}