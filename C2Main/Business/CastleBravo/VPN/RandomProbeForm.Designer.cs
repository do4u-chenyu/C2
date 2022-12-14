namespace C2.Business.CastleBravo.VPN
{
    partial class RandomProbeForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.timeoutBox = new System.Windows.Forms.TextBox();
            this.lengthBox = new System.Windows.Forms.TextBox();
            this.probeContentBox = new System.Windows.Forms.TextBox();
            this.numBox = new System.Windows.Forms.TextBox();
            this.infoBox = new System.Windows.Forms.TextBox();
            this.contentCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 10003;
            this.label1.Text = "超时(秒):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 10004;
            this.label2.Text = "长度区间:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 10005;
            this.label3.Text = "重复次数:";
            // 
            // timeoutBox
            // 
            this.timeoutBox.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.timeoutBox.Location = new System.Drawing.Point(87, 6);
            this.timeoutBox.Name = "timeoutBox";
            this.timeoutBox.Size = new System.Drawing.Size(89, 21);
            this.timeoutBox.TabIndex = 10006;
            this.timeoutBox.Text = "5";
            // 
            // lengthBox
            // 
            this.lengthBox.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lengthBox.Location = new System.Drawing.Point(87, 37);
            this.lengthBox.Name = "lengthBox";
            this.lengthBox.Size = new System.Drawing.Size(281, 21);
            this.lengthBox.TabIndex = 10008;
            this.lengthBox.Text = "1-53,80,100,110,220,221,222";
            // 
            // probeContentBox
            // 
            this.probeContentBox.BackColor = System.Drawing.SystemColors.Menu;
            this.probeContentBox.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.probeContentBox.Location = new System.Drawing.Point(5, 122);
            this.probeContentBox.MaxLength = 16777216;
            this.probeContentBox.Multiline = true;
            this.probeContentBox.Name = "probeContentBox";
            this.probeContentBox.ReadOnly = true;
            this.probeContentBox.Size = new System.Drawing.Size(385, 166);
            this.probeContentBox.TabIndex = 10029;
            this.probeContentBox.WordWrap = false;
            // 
            // numBox
            // 
            this.numBox.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.numBox.Location = new System.Drawing.Point(278, 6);
            this.numBox.Name = "numBox";
            this.numBox.Size = new System.Drawing.Size(89, 21);
            this.numBox.TabIndex = 10030;
            this.numBox.Text = "1";
            // 
            // infoBox
            // 
            this.infoBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.infoBox.ForeColor = System.Drawing.Color.Red;
            this.infoBox.Location = new System.Drawing.Point(17, 62);
            this.infoBox.Name = "infoBox";
            this.infoBox.Size = new System.Drawing.Size(351, 14);
            this.infoBox.TabIndex = 10033;
            this.infoBox.Text = "说明: \',\'表示离散区间    \'-\'表示连续区间";
            // 
            // contentCheckBox
            // 
            this.contentCheckBox.AutoSize = true;
            this.contentCheckBox.Location = new System.Drawing.Point(16, 100);
            this.contentCheckBox.Name = "contentCheckBox";
            this.contentCheckBox.Size = new System.Drawing.Size(174, 16);
            this.contentCheckBox.TabIndex = 10034;
            this.contentCheckBox.Text = "自定义探针内容 (Hex格式):";
            this.contentCheckBox.UseVisualStyleBackColor = true;
            this.contentCheckBox.CheckedChanged += new System.EventHandler(this.ContentCheckBox_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(17, 79);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(257, 12);
            this.label4.TabIndex = 10035;
            this.label4.Text = "如长度区间是1到6,8,10到12则写作1-6,8,10-12";
            // 
            // RandomProbeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(394, 339);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.contentCheckBox);
            this.Controls.Add(this.infoBox);
            this.Controls.Add(this.numBox);
            this.Controls.Add(this.probeContentBox);
            this.Controls.Add(this.lengthBox);
            this.Controls.Add(this.timeoutBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "RandomProbeForm";
            this.Text = "随机探针配置";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.timeoutBox, 0);
            this.Controls.SetChildIndex(this.lengthBox, 0);
            this.Controls.SetChildIndex(this.probeContentBox, 0);
            this.Controls.SetChildIndex(this.numBox, 0);
            this.Controls.SetChildIndex(this.infoBox, 0);
            this.Controls.SetChildIndex(this.contentCheckBox, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox timeoutBox;
        private System.Windows.Forms.TextBox lengthBox;
        private System.Windows.Forms.TextBox probeContentBox;
        private System.Windows.Forms.TextBox numBox;
        private System.Windows.Forms.TextBox infoBox;
        private System.Windows.Forms.CheckBox contentCheckBox;
        private System.Windows.Forms.Label label4;
    }
}