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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MysqlProbeSet));
            this.timeoutTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.fileList = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.probeStrategyCB = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.fieldList = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
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
            this.label2.Location = new System.Drawing.Point(5, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 10006;
            this.label2.Text = "目标文件:";
            // 
            // fileList
            // 
            this.fileList.Font = new System.Drawing.Font("宋体", 9F);
            this.fileList.Location = new System.Drawing.Point(65, 48);
            this.fileList.Name = "fileList";
            this.fileList.Size = new System.Drawing.Size(436, 21);
            this.fileList.TabIndex = 3;
            this.fileList.Text = "mysql.php,database.php,db.php,database.php,config.php,config.inc.php,config_db.ph" +
    "p,common.inc.php,conn.php,dbconfig.php,config1.php,deploy.php,sql_config.php,wp-" +
    "config.php,c_option.php,config_base.php";
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
            // probeStrategyCB
            // 
            this.probeStrategyCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.probeStrategyCB.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this.probeStrategyCB.FormattingEnabled = true;
            this.probeStrategyCB.Items.AddRange(new object[] {
            "当前站点目录",
            "所有同站目录",
            "前1000个文件",
            "前10000个文件",
            "前100000个文件",
            "基于字典(规划中...)",
            "/www(规划中...)",
            "宝塔面板(规划中...)",
            "小皮面板(规则中...)",
            "自定义(规划中...)"});
            this.probeStrategyCB.Location = new System.Drawing.Point(311, 8);
            this.probeStrategyCB.Name = "probeStrategyCB";
            this.probeStrategyCB.Size = new System.Drawing.Size(107, 24);
            this.probeStrategyCB.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(6, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(269, 12);
            this.label3.TabIndex = 10036;
            this.label3.Text = "此设置不保存,用前每次设置一下,默认不需要更改";
            // 
            // fieldList
            // 
            this.fieldList.Font = new System.Drawing.Font("宋体", 9F);
            this.fieldList.Location = new System.Drawing.Point(65, 75);
            this.fieldList.Name = "fieldList";
            this.fieldList.Size = new System.Drawing.Size(436, 21);
            this.fieldList.TabIndex = 4;
            this.fieldList.Text = resources.GetString("fieldList.Text");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(5, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 17);
            this.label6.TabIndex = 10038;
            this.label6.Text = "目标字段:";
            // 
            // MysqlProbeSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 174);
            this.Controls.Add(this.fieldList);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.probeStrategyCB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.fileList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.timeoutTB);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MysqlProbeSet";
            this.Text = "Mysql探针配置";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.timeoutTB, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.fileList, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.probeStrategyCB, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.fieldList, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox timeoutTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fileList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox probeStrategyCB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox fieldList;
        private System.Windows.Forms.Label label6;
    }
}