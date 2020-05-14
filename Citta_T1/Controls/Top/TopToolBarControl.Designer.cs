namespace Citta_T1.Controls.Top
{
    partial class TopToolBarControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TopToolBarControl));
            this.relateButton = new System.Windows.Forms.Button();
            this.collideButton = new System.Windows.Forms.Button();
            this.unionButton = new System.Windows.Forms.Button();
            this.differButton = new System.Windows.Forms.Button();
            this.filterButton = new System.Windows.Forms.Button();
            this.randomButton = new System.Windows.Forms.Button();
            this.formatButton = new System.Windows.Forms.Button();
            this.moreButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // relateButton
            // 
            this.relateButton.Image = ((System.Drawing.Image)(resources.GetObject("relateButton.Image")));
            this.relateButton.Location = new System.Drawing.Point(44, 0);
            this.relateButton.Name = "relateButton";
            this.relateButton.Size = new System.Drawing.Size(61, 32);
            this.relateButton.TabIndex = 2;
            this.relateButton.UseVisualStyleBackColor = true;
            this.relateButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CommonUse_MouseDown);
            // 
            // collideButton
            // 
            this.collideButton.Image = ((System.Drawing.Image)(resources.GetObject("collideButton.Image")));
            this.collideButton.Location = new System.Drawing.Point(107, 0);
            this.collideButton.Name = "collideButton";
            this.collideButton.Size = new System.Drawing.Size(61, 32);
            this.collideButton.TabIndex = 6;
            this.collideButton.UseVisualStyleBackColor = true;
            this.collideButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CommonUse_MouseDown);
            // 
            // unionButton
            // 
            this.unionButton.Image = ((System.Drawing.Image)(resources.GetObject("unionButton.Image")));
            this.unionButton.Location = new System.Drawing.Point(170, 0);
            this.unionButton.Name = "unionButton";
            this.unionButton.Size = new System.Drawing.Size(61, 32);
            this.unionButton.TabIndex = 7;
            this.unionButton.UseVisualStyleBackColor = true;
            this.unionButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CommonUse_MouseDown);
            // 
            // differButton
            // 
            this.differButton.Image = ((System.Drawing.Image)(resources.GetObject("differButton.Image")));
            this.differButton.Location = new System.Drawing.Point(233, 0);
            this.differButton.Name = "differButton";
            this.differButton.Size = new System.Drawing.Size(61, 32);
            this.differButton.TabIndex = 8;
            this.differButton.UseVisualStyleBackColor = true;
            this.differButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CommonUse_MouseDown);
            // 
            // filterButton
            // 
            this.filterButton.Image = ((System.Drawing.Image)(resources.GetObject("filterButton.Image")));
            this.filterButton.Location = new System.Drawing.Point(296, 0);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(61, 32);
            this.filterButton.TabIndex = 9;
            this.filterButton.UseVisualStyleBackColor = true;
            this.filterButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CommonUse_MouseDown);
            // 
            // randomButton
            // 
            this.randomButton.Image = ((System.Drawing.Image)(resources.GetObject("randomButton.Image")));
            this.randomButton.Location = new System.Drawing.Point(359, 0);
            this.randomButton.Name = "randomButton";
            this.randomButton.Size = new System.Drawing.Size(69, 32);
            this.randomButton.TabIndex = 10;
            this.randomButton.UseVisualStyleBackColor = true;
            this.randomButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CommonUse_MouseDown);
            // 
            // formatButton
            // 
            this.formatButton.Image = ((System.Drawing.Image)(resources.GetObject("formatButton.Image")));
            this.formatButton.Location = new System.Drawing.Point(430, 0);
            this.formatButton.Name = "formatButton";
            this.formatButton.Size = new System.Drawing.Size(89, 32);
            this.formatButton.TabIndex = 12;
            this.formatButton.UseVisualStyleBackColor = true;
            this.formatButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormatButton_MouseClick);
            // 
            // moreButton
            // 
            this.moreButton.Image = ((System.Drawing.Image)(resources.GetObject("moreButton.Image")));
            this.moreButton.Location = new System.Drawing.Point(523, 4);
            this.moreButton.Name = "moreButton";
            this.moreButton.Size = new System.Drawing.Size(25, 24);
            this.moreButton.TabIndex = 13;
            this.toolTip1.SetToolTip(this.moreButton, "首选项配置,配置程序的各项参数");
            this.moreButton.UseVisualStyleBackColor = true;
            this.moreButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MoreButton_MouseClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(-2, 5);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 21);
            this.label7.TabIndex = 23;
            this.label7.Text = "常用";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TopToolBarControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.label7);
            this.Controls.Add(this.moreButton);
            this.Controls.Add(this.formatButton);
            this.Controls.Add(this.randomButton);
            this.Controls.Add(this.filterButton);
            this.Controls.Add(this.differButton);
            this.Controls.Add(this.unionButton);
            this.Controls.Add(this.collideButton);
            this.Controls.Add(this.relateButton);
            this.Name = "TopToolBarControl";
            this.Size = new System.Drawing.Size(554, 33);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button relateButton;
        private System.Windows.Forms.Button collideButton;
        private System.Windows.Forms.Button unionButton;
        private System.Windows.Forms.Button differButton;
        private System.Windows.Forms.Button filterButton;
        private System.Windows.Forms.Button randomButton;
        private System.Windows.Forms.Button formatButton;
        private System.Windows.Forms.Button moreButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
