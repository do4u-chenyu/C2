namespace C2.Controls.Common
{
    partial class DesignerControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dataSourceCombo = new System.Windows.Forms.ComboBox();
            this.changeOpButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.topicName = new System.Windows.Forms.Label();
            this.operatorCombo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据选择";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "算子选择";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "算子配置";
            // 
            // dataSourceCombo
            // 
            this.dataSourceCombo.FormattingEnabled = true;
            this.dataSourceCombo.Location = new System.Drawing.Point(74, 54);
            this.dataSourceCombo.Name = "dataSourceCombo";
            this.dataSourceCombo.Size = new System.Drawing.Size(99, 20);
            this.dataSourceCombo.TabIndex = 3;
            this.dataSourceCombo.SelectedIndexChanged += new System.EventHandler(this.DataSourceCombo_SelectedIndexChanged);
            // 
            // changeOpButton
            // 
            this.changeOpButton.Location = new System.Drawing.Point(74, 130);
            this.changeOpButton.Name = "changeOpButton";
            this.changeOpButton.Size = new System.Drawing.Size(98, 23);
            this.changeOpButton.TabIndex = 5;
            this.changeOpButton.Text = "修改";
            this.changeOpButton.UseVisualStyleBackColor = true;
            this.changeOpButton.Click += new System.EventHandler(this.Button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "选中主题";
            // 
            // topicName
            // 
            this.topicName.AutoSize = true;
            this.topicName.Location = new System.Drawing.Point(74, 19);
            this.topicName.Name = "topicName";
            this.topicName.Size = new System.Drawing.Size(0, 12);
            this.topicName.TabIndex = 7;
            // 
            // operatorCombo
            // 
            this.operatorCombo.FormattingEnabled = true;
            this.operatorCombo.Items.AddRange(new object[] {
            "最大值",
            "AI实践"});
            this.operatorCombo.Location = new System.Drawing.Point(74, 93);
            this.operatorCombo.Name = "operatorCombo";
            this.operatorCombo.Size = new System.Drawing.Size(99, 20);
            this.operatorCombo.TabIndex = 8;
            this.operatorCombo.SelectedIndexChanged += new System.EventHandler(this.OperatorCombo_SelectedIndexChanged);
            // 
            // DesignerControl
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.operatorCombo);
            this.Controls.Add(this.topicName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.changeOpButton);
            this.Controls.Add(this.dataSourceCombo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "DesignerControl";
            this.Size = new System.Drawing.Size(186, 260);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox dataSourceCombo;
        private System.Windows.Forms.Button changeOpButton;
        private System.Windows.Forms.Label topicName;
        private System.Windows.Forms.ComboBox operatorCombo;
    }
}
