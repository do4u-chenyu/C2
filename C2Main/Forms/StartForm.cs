using C2.Controls;
using C2.Globalization;

namespace C2.Forms
{
    class StartForm : BaseForm
    {
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.Label label5;
        RecentFilesView recentFilesView1;
        public StartForm()
        {
            Text = Lang._("StartPage");

            InitializeComponent();
            //test();

            AfterInitialize();
        }
        
        void InitializeComponent()
        {
            this.recentFilesView1 = new C2.Controls.RecentFilesView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // recentFilesView1
            // 
            this.recentFilesView1.ActiveCellBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.recentFilesView1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.recentFilesView1.CellBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.recentFilesView1.CellSpace = new System.Drawing.Size(20, 60);
            this.recentFilesView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.recentFilesView1.Location = new System.Drawing.Point(0, 0);
            this.recentFilesView1.Name = "recentFilesView1";
            this.recentFilesView1.Size = new System.Drawing.Size(1767, 1054);
            this.recentFilesView1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Window;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(24, 789);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 28);
            this.label2.TabIndex = 2;
            this.label2.Text = "网站侦察兵";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Window;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(25, 826);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(362, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "对网站进行爬取、分类并截图,最终将判别结果返回给用户";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Window;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(24, 317);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 28);
            this.label4.TabIndex = 4;
            this.label4.Text = "分析笔记";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Window;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(25, 356);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(345, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "承载分析师的分析思路，落地分析过程，记录分析结果";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Window;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(468, 356);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(289, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "流程化的战法指导，供选择，可参考、可复用";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.Window;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(906, 317);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 28);
            this.label7.TabIndex = 8;
            this.label7.Text = "喝彩城堡";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.Window;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(907, 356);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(177, 20);
            this.label8.TabIndex = 9;
            this.label8.Text = "模型和闭环配套的安全工具";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.SystemColors.Window;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(1346, 317);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 28);
            this.label9.TabIndex = 10;
            this.label9.Text = "实验楼";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.SystemColors.Window;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(1347, 356);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(135, 20);
            this.label10.TabIndex = 11;
            this.label10.Text = "常用分析小工具集合";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.SystemColors.Window;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(467, 789);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(120, 28);
            this.label11.TabIndex = 12;
            this.label11.Text = "APK监测站";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.SystemColors.Window;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(468, 826);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(387, 20);
            this.label12.TabIndex = 13;
            this.label12.Text = "通过上传APK文件、创建项目、下载结果，使用APK检测服务";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.SystemColors.Window;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(906, 789);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(75, 28);
            this.label13.TabIndex = 14;
            this.label13.Text = "知识库";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.SystemColors.Window;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(907, 826);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(275, 20);
            this.label14.TabIndex = 15;
            this.label14.Text = "各业务方向关键词、线索查询、上传、下载";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.SystemColors.Window;
            this.label15.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(1356, 789);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(66, 28);
            this.label15.TabIndex = 16;
            this.label15.Text = "HIBU";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.SystemColors.Window;
            this.label16.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(1357, 826);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(109, 20);
            this.label16.TabIndex = 17;
            this.label16.Text = "提供23种AI能力";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Window;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(467, 317);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 28);
            this.label5.TabIndex = 6;
            this.label5.Text = "战术手册";
            // 
            // StartForm
            // 
            this.ClientSize = new System.Drawing.Size(1767, 1054);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.recentFilesView1);
            this.IconImage = global::C2.Properties.Resources.首页;
            this.Name = "StartForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        public override bool IsNeedShowBottomViewPanel()
        {
            return false;
        }
    }
}
