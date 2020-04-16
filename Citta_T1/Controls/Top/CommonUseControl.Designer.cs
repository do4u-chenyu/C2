namespace Citta_T1.Controls.Top
{
    partial class CommonUseControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommonUseControl));
            this.collideButton = new System.Windows.Forms.Button();
            this.relateButton = new System.Windows.Forms.Button();
            this.unionButton = new System.Windows.Forms.Button();
            this.differButton = new System.Windows.Forms.Button();
            this.filterButton = new System.Windows.Forms.Button();
            this.randomButton = new System.Windows.Forms.Button();
            this.histogramButton = new System.Windows.Forms.Button();
            this.formatButton = new System.Windows.Forms.Button();
            this.moreButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // connectOpButton
            // 
            this.collideButton.Image = ((System.Drawing.Image)(resources.GetObject("connectOpButton.Image")));
            this.collideButton.Location = new System.Drawing.Point(40, 0);
            this.collideButton.Name = "CollideButton";
            this.collideButton.Size = new System.Drawing.Size(61, 32);
            this.collideButton.TabIndex = 2;
            this.collideButton.UseVisualStyleBackColor = true;
            this.collideButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CommonUse_MouseDown);
            // 
            // interOpButton
            // 
            this.relateButton.Image = ((System.Drawing.Image)(resources.GetObject("interOpButton.Image")));
            this.relateButton.Location = new System.Drawing.Point(103, 0);
            this.relateButton.Name = "RelateButton";
            this.relateButton.Size = new System.Drawing.Size(61, 32);
            this.relateButton.TabIndex = 6;
            this.relateButton.UseVisualStyleBackColor = true;
            this.relateButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CommonUse_MouseDown);
            // 
            // UnionButton
            // 
            this.unionButton.Image = ((System.Drawing.Image)(resources.GetObject("UnionButton.Image")));
            this.unionButton.Location = new System.Drawing.Point(167, 0);
            this.unionButton.Name = "UnionButton";
            this.unionButton.Size = new System.Drawing.Size(61, 32);
            this.unionButton.TabIndex = 7;
            this.unionButton.UseVisualStyleBackColor = true;
            this.unionButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CommonUse_MouseDown);
            // 
            // diffButton
            // 
            this.differButton.Image = ((System.Drawing.Image)(resources.GetObject("diffButton.Image")));
            this.differButton.Location = new System.Drawing.Point(231, 0);
            this.differButton.Name = "DifferButton";
            this.differButton.Size = new System.Drawing.Size(61, 32);
            this.differButton.TabIndex = 8;
            this.differButton.UseVisualStyleBackColor = true;
            this.differButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CommonUse_MouseDown);
            // 
            // filterButton
            // 
            this.filterButton.Image = ((System.Drawing.Image)(resources.GetObject("filterButton.Image")));
            this.filterButton.Location = new System.Drawing.Point(295, 0);
            this.filterButton.Name = "FilterButton";
            this.filterButton.Size = new System.Drawing.Size(61, 32);
            this.filterButton.TabIndex = 9;
            this.filterButton.UseVisualStyleBackColor = true;
            this.filterButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CommonUse_MouseDown);
            // 
            // RandomButton
            // 
            this.randomButton.Image = ((System.Drawing.Image)(resources.GetObject("RandomButton.Image")));
            this.randomButton.Location = new System.Drawing.Point(358, 0);
            this.randomButton.Name = "RandomButton";
            this.randomButton.Size = new System.Drawing.Size(69, 32);
            this.randomButton.TabIndex = 10;
            this.randomButton.UseVisualStyleBackColor = true;
            this.randomButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CommonUse_MouseDown);
            // 
            // histogramButton
            // 
            this.histogramButton.Image = ((System.Drawing.Image)(resources.GetObject("histogramButton.Image")));
            this.histogramButton.Location = new System.Drawing.Point(431, 0);
            this.histogramButton.Name = "HistogramButton";
            this.histogramButton.Size = new System.Drawing.Size(75, 32);
            this.histogramButton.TabIndex = 11;
            this.histogramButton.UseVisualStyleBackColor = true;
            // 
            // formatButton
            // 
            this.formatButton.Image = ((System.Drawing.Image)(resources.GetObject("formatButton.Image")));
            this.formatButton.Location = new System.Drawing.Point(509, 0);
            this.formatButton.Name = "FormatButton";
            this.formatButton.Size = new System.Drawing.Size(89, 32);
            this.formatButton.TabIndex = 12;
            this.formatButton.UseVisualStyleBackColor = true;
            this.formatButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.formatButton_MouseClick);
            // 
            // moreButton
            // 
            this.moreButton.Image = ((System.Drawing.Image)(resources.GetObject("moreButton.Image")));
            this.moreButton.Location = new System.Drawing.Point(602, 4);
            this.moreButton.Name = "MoreButton";
            this.moreButton.Size = new System.Drawing.Size(25, 24);
            this.moreButton.TabIndex = 13;
            this.moreButton.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(-1, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 21);
            this.label7.TabIndex = 23;
            this.label7.Text = "常用";
            // 
            // CommonUseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label7);
            this.Controls.Add(this.moreButton);
            this.Controls.Add(this.formatButton);
            this.Controls.Add(this.histogramButton);
            this.Controls.Add(this.randomButton);
            this.Controls.Add(this.filterButton);
            this.Controls.Add(this.differButton);
            this.Controls.Add(this.unionButton);
            this.Controls.Add(this.relateButton);
            this.Controls.Add(this.collideButton);
            this.Name = "CommonUseControl";
            this.Size = new System.Drawing.Size(654, 33);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button collideButton;
        private System.Windows.Forms.Button relateButton;
        private System.Windows.Forms.Button unionButton;
        private System.Windows.Forms.Button differButton;
        private System.Windows.Forms.Button filterButton;
        private System.Windows.Forms.Button randomButton;
        private System.Windows.Forms.Button histogramButton;
        private System.Windows.Forms.Button formatButton;
        private System.Windows.Forms.Button moreButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
