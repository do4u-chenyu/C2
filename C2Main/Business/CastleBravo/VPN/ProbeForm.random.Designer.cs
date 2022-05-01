namespace C2.Business.CastleBravo.VPN
{
    partial class ProbeForm
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
            this.probeContextBox = new System.Windows.Forms.TextBox();
            this.numBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.infoBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 10003;
            this.label1.Text = "超时时间 (秒):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 10004;
            this.label2.Text = "探针长度区间:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(215, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 10005;
            this.label3.Text = "重复发送次数:";
            // 
            // timeoutBox
            // 
            this.timeoutBox.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.timeoutBox.Location = new System.Drawing.Point(116, 18);
            this.timeoutBox.Name = "timeoutBox";
            this.timeoutBox.Size = new System.Drawing.Size(89, 21);
            this.timeoutBox.TabIndex = 10006;
            this.timeoutBox.Text = "10";
            // 
            // lengthBox
            // 
            this.lengthBox.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lengthBox.Location = new System.Drawing.Point(116, 49);
            this.lengthBox.Name = "lengthBox";
            this.lengthBox.Size = new System.Drawing.Size(281, 21);
            this.lengthBox.TabIndex = 10008;
            this.lengthBox.Text = "1-225";
            // 
            // probeContextBox
            // 
            this.probeContextBox.BackColor = System.Drawing.Color.White;
            this.probeContextBox.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.probeContextBox.Location = new System.Drawing.Point(0, 128);
            this.probeContextBox.MaxLength = 16777216;
            this.probeContextBox.Multiline = true;
            this.probeContextBox.Name = "probeContextBox";
            this.probeContextBox.ReadOnly = true;
            this.probeContextBox.Size = new System.Drawing.Size(412, 173);
            this.probeContextBox.TabIndex = 10029;
            this.probeContextBox.WordWrap = false;
            // 
            // numBox
            // 
            this.numBox.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.numBox.Location = new System.Drawing.Point(308, 18);
            this.numBox.Name = "numBox";
            this.numBox.Size = new System.Drawing.Size(89, 21);
            this.numBox.TabIndex = 10030;
            this.numBox.Text = "80";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 12);
            this.label4.TabIndex = 10031;
            this.label4.Text = "自定义探针内容 (默认为空):";
            // 
            // infoBox
            // 
            this.infoBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.infoBox.ForeColor = System.Drawing.Color.Red;
            this.infoBox.Location = new System.Drawing.Point(17, 79);
            this.infoBox.Multiline = true;
            this.infoBox.Name = "infoBox";
            this.infoBox.Size = new System.Drawing.Size(378, 31);
            this.infoBox.TabIndex = 10033;
            this.infoBox.Text = "* 说明:\'-\'表示取连续区间,\',\'表示离散区间。如测试探针长度区间是1到6，8，10到12可写为1-6，8，10-12";
            // 
            // ProbeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(413, 346);
            this.Controls.Add(this.infoBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numBox);
            this.Controls.Add(this.probeContextBox);
            this.Controls.Add(this.lengthBox);
            this.Controls.Add(this.timeoutBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ProbeForm";
            this.Text = "随机探针配置";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.timeoutBox, 0);
            this.Controls.SetChildIndex(this.lengthBox, 0);
            this.Controls.SetChildIndex(this.probeContextBox, 0);
            this.Controls.SetChildIndex(this.numBox, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.infoBox, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox timeoutBox;
        private System.Windows.Forms.TextBox lengthBox;
        private System.Windows.Forms.TextBox probeContextBox;
        private System.Windows.Forms.TextBox numBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox infoBox;
    }
}