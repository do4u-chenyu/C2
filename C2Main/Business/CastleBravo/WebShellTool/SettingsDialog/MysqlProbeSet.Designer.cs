namespace C2.Business.CastleBravo.WebShellTool.SettingsDialog
{
    partial class MysqlProbeSet
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
            this.timeoutTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nameListTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.probeScopeCB = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timeoutTB
            // 
            this.timeoutTB.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this.timeoutTB.Location = new System.Drawing.Point(65, 11);
            this.timeoutTB.Name = "timeoutTB";
            this.timeoutTB.Size = new System.Drawing.Size(63, 22);
            this.timeoutTB.TabIndex = 1;
            this.timeoutTB.Text = "600";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(5, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 10005;
            this.label1.Text = "超时设置:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(5, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 10006;
            this.label2.Text = "目标文件:";
            // 
            // nameListTB
            // 
            this.nameListTB.Font = new System.Drawing.Font("宋体", 9F);
            this.nameListTB.Location = new System.Drawing.Point(65, 40);
            this.nameListTB.Name = "nameListTB";
            this.nameListTB.Size = new System.Drawing.Size(436, 21);
            this.nameListTB.TabIndex = 3;
            this.nameListTB.Text = "config.php|config.inc.php|db.php|db_config.php";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(251, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 17);
            this.label4.TabIndex = 10010;
            this.label4.Text = "探测范围:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(137, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 17);
            this.label5.TabIndex = 10012;
            this.label5.Text = "秒";
            // 
            // probeScopeCB
            // 
            this.probeScopeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.probeScopeCB.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this.probeScopeCB.FormattingEnabled = true;
            this.probeScopeCB.Items.AddRange(new object[] {
            "整个主机",
            "仅当前站点目录",
            "仅1000个文件",
            "仅10000个文件"});
            this.probeScopeCB.Location = new System.Drawing.Point(311, 8);
            this.probeScopeCB.Name = "probeScopeCB";
            this.probeScopeCB.Size = new System.Drawing.Size(107, 24);
            this.probeScopeCB.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(6, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 12);
            this.label3.TabIndex = 10036;
            this.label3.Text = "此设置不保存,用前每次设置一下";
            // 
            // MysqlProbeSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 145);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.probeScopeCB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nameListTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.timeoutTB);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MysqlProbeSet";
            this.Text = "Mysql探针配置";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.timeoutTB, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.nameListTB, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.probeScopeCB, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox timeoutTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nameListTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox probeScopeCB;
        private System.Windows.Forms.Label label3;
    }
}