namespace C2.Controls.Top
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
            this.formatButton = new System.Windows.Forms.Button();
            this.moreButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.undoButton = new System.Windows.Forms.Button();
            this.redoButton = new System.Windows.Forms.Button();
            this.ImportModel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // formatButton
            // 
            this.formatButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.formatButton.ForeColor = System.Drawing.Color.GhostWhite;
            this.formatButton.Image = ((System.Drawing.Image)(resources.GetObject("formatButton.Image")));
            this.formatButton.Location = new System.Drawing.Point(161, 1);
            this.formatButton.Name = "formatButton";
            this.formatButton.Size = new System.Drawing.Size(32, 32);
            this.formatButton.TabIndex = 0;
            this.formatButton.TabStop = false;
            this.formatButton.UseVisualStyleBackColor = true;
            this.formatButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormatButton_MouseClick);
            // 
            // moreButton
            // 
            this.moreButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.moreButton.ForeColor = System.Drawing.Color.GhostWhite;
            this.moreButton.Image = ((System.Drawing.Image)(resources.GetObject("moreButton.Image")));
            this.moreButton.Location = new System.Drawing.Point(201, 1);
            this.moreButton.Name = "moreButton";
            this.moreButton.Size = new System.Drawing.Size(32, 32);
            this.moreButton.TabIndex = 0;
            this.moreButton.TabStop = false;
            this.toolTip1.SetToolTip(this.moreButton, "首选项配置,配置程序的各项参数");
            this.moreButton.UseVisualStyleBackColor = true;
            this.moreButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MoreButton_MouseClick);
            // 
            // undoButton
            // 
            this.undoButton.Enabled = false;
            this.undoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.undoButton.ForeColor = System.Drawing.Color.GhostWhite;
            this.undoButton.Image = ((System.Drawing.Image)(resources.GetObject("undoButton.Image")));
            this.undoButton.Location = new System.Drawing.Point(81, 1);
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(32, 32);
            this.undoButton.TabIndex = 0;
            this.undoButton.TabStop = false;
            this.undoButton.UseVisualStyleBackColor = true;
            this.undoButton.Click += new System.EventHandler(this.UndoButton_Click);
            // 
            // redoButton
            // 
            this.redoButton.BackColor = System.Drawing.Color.GhostWhite;
            this.redoButton.Enabled = false;
            this.redoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.redoButton.ForeColor = System.Drawing.Color.GhostWhite;
            this.redoButton.Image = ((System.Drawing.Image)(resources.GetObject("redoButton.Image")));
            this.redoButton.Location = new System.Drawing.Point(121, 1);
            this.redoButton.Name = "redoButton";
            this.redoButton.Size = new System.Drawing.Size(32, 32);
            this.redoButton.TabIndex = 0;
            this.redoButton.TabStop = false;
            this.redoButton.UseVisualStyleBackColor = false;
            this.redoButton.Click += new System.EventHandler(this.RedoButton_Click);
            // 
            // ImportModel
            // 
            this.ImportModel.BackColor = System.Drawing.Color.GhostWhite;
            this.ImportModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ImportModel.ForeColor = System.Drawing.Color.GhostWhite;
            this.ImportModel.Image = global::C2.Properties.Resources.importmodel;
            this.ImportModel.Location = new System.Drawing.Point(241, 1);
            this.ImportModel.Name = "ImportModel";
            this.ImportModel.Size = new System.Drawing.Size(32, 32);
            this.ImportModel.TabIndex = 1;
            this.toolTip1.SetToolTip(this.ImportModel, "导入iao模型");
            this.ImportModel.UseVisualStyleBackColor = false;
            this.ImportModel.Click += new System.EventHandler(this.ImportModel_Click);
            // 
            // TopToolBarControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.Controls.Add(this.ImportModel);
            this.Controls.Add(this.redoButton);
            this.Controls.Add(this.undoButton);
            this.Controls.Add(this.moreButton);
            this.Controls.Add(this.formatButton);
            this.Name = "TopToolBarControl";
            this.Size = new System.Drawing.Size(990, 33);
            this.Load += new System.EventHandler(this.TopToolBarControl_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button formatButton;
        private System.Windows.Forms.Button moreButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button undoButton;
        private System.Windows.Forms.Button redoButton;
        private System.Windows.Forms.Button ImportModel;
    }
}
