
namespace C2.Business.CastleBravo.VPN
{
    partial class BatchAddVPNServerForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.wsTextBox = new System.Windows.Forms.TextBox();
            this.pasteModeCB = new System.Windows.Forms.CheckBox();
            this.browserButton = new System.Windows.Forms.Button();
            this.filePathTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(15, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(221, 12);
            this.label3.TabIndex = 10029;
            this.label3.Text = "* 内容格式:一行一个订阅链接,最多1w行";
            // 
            // wsTextBox
            // 
            this.wsTextBox.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.wsTextBox.Location = new System.Drawing.Point(97, 94);
            this.wsTextBox.MaxLength = 16777216;
            this.wsTextBox.Multiline = true;
            this.wsTextBox.Name = "wsTextBox";
            this.wsTextBox.ReadOnly = true;
            this.wsTextBox.Size = new System.Drawing.Size(329, 159);
            this.wsTextBox.TabIndex = 10028;
            this.wsTextBox.WordWrap = false;
            // 
            // pasteModeCB
            // 
            this.pasteModeCB.AutoSize = true;
            this.pasteModeCB.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.pasteModeCB.Location = new System.Drawing.Point(15, 97);
            this.pasteModeCB.Name = "pasteModeCB";
            this.pasteModeCB.Size = new System.Drawing.Size(75, 21);
            this.pasteModeCB.TabIndex = 10027;
            this.pasteModeCB.Text = "粘贴模式";
            this.pasteModeCB.UseVisualStyleBackColor = true;
            this.pasteModeCB.CheckedChanged += new System.EventHandler(this.PasteModeCB_CheckedChanged);
            // 
            // browserButton
            // 
            this.browserButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.browserButton.Location = new System.Drawing.Point(367, 13);
            this.browserButton.Name = "browserButton";
            this.browserButton.Size = new System.Drawing.Size(59, 23);
            this.browserButton.TabIndex = 10026;
            this.browserButton.Text = "+浏览";
            this.browserButton.UseVisualStyleBackColor = true;
            this.browserButton.Click += new System.EventHandler(this.BrowserButton_Click);
            // 
            // filePathTextBox
            // 
            this.filePathTextBox.BackColor = System.Drawing.Color.White;
            this.filePathTextBox.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.filePathTextBox.Location = new System.Drawing.Point(98, 12);
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.ReadOnly = true;
            this.filePathTextBox.Size = new System.Drawing.Size(263, 25);
            this.filePathTextBox.TabIndex = 10025;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(15, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 19);
            this.label2.TabIndex = 10024;
            this.label2.Text = "文件路径：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(15, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 12);
            this.label1.TabIndex = 10034;
            this.label1.Text = "*  最大[16M]文本，大约10W-20W行";
            // 
            // BatchAddVPNServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 302);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.wsTextBox);
            this.Controls.Add(this.pasteModeCB);
            this.Controls.Add(this.browserButton);
            this.Controls.Add(this.filePathTextBox);
            this.Controls.Add(this.label2);
            this.Name = "BatchAddVPNServerForm";
            this.Text = "BatchAddVPNServerForm";
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.filePathTextBox, 0);
            this.Controls.SetChildIndex(this.browserButton, 0);
            this.Controls.SetChildIndex(this.pasteModeCB, 0);
            this.Controls.SetChildIndex(this.wsTextBox, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox wsTextBox;
        private System.Windows.Forms.CheckBox pasteModeCB;
        private System.Windows.Forms.Button browserButton;
        private System.Windows.Forms.TextBox filePathTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}