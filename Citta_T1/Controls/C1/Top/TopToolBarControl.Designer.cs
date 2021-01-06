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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TopToolBarControl));
            this.toolStrip1 = new C2.Controls.ToolStripPro();
            this.ImportModelButton = new System.Windows.Forms.ToolStripButton();
            this.SaveModelButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.UndoButton = new System.Windows.Forms.ToolStripButton();
            this.RedoButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.FormatButton = new System.Windows.Forms.ToolStripButton();
            this.RemarkButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ZoomUpButton = new System.Windows.Forms.ToolStripButton();
            this.ZoomDownButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.MoveButton = new System.Windows.Forms.ToolStripButton();
            this.FrameButton = new System.Windows.Forms.ToolStripButton();
            this.LinePanel = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(90)))), ((int)(((byte)(177)))));
            this.toolStrip1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportModelButton,
            this.SaveModelButton,
            this.toolStripSeparator1,
            this.UndoButton,
            this.RedoButton,
            this.toolStripSeparator2,
            this.FormatButton,
            this.RemarkButton,
            this.toolStripSeparator3,
            this.ZoomUpButton,
            this.ZoomDownButton,
            this.toolStripSeparator4,
            this.MoveButton,
            this.FrameButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(990, 29);
            this.toolStrip1.TabIndex = 2;
            // 
            // ImportModelButton
            // 
            this.ImportModelButton.AutoSize = false;
            this.ImportModelButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ImportModelButton.Enabled = false;
            this.ImportModelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(90)))), ((int)(((byte)(177)))));
            this.ImportModelButton.Image = global::C2.Properties.Resources.importModel;
            this.ImportModelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ImportModelButton.Margin = new System.Windows.Forms.Padding(0);
            this.ImportModelButton.Name = "ImportModelButton";
            this.ImportModelButton.Size = new System.Drawing.Size(24, 25);
            this.ImportModelButton.Click += new System.EventHandler(this.ImportModel_Click);
            // 
            // SaveModelButton
            // 
            this.SaveModelButton.AutoSize = false;
            this.SaveModelButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveModelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(90)))), ((int)(((byte)(177)))));
            this.SaveModelButton.Image = global::C2.Properties.Resources.save;
            this.SaveModelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveModelButton.Margin = new System.Windows.Forms.Padding(0);
            this.SaveModelButton.Name = "SaveModelButton";
            this.SaveModelButton.Size = new System.Drawing.Size(24, 25);
            this.SaveModelButton.Click += new System.EventHandler(this.SaveModelButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Padding = new System.Windows.Forms.Padding(2);
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 29);
            // 
            // UndoButton
            // 
            this.UndoButton.AutoSize = false;
            this.UndoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UndoButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(90)))), ((int)(((byte)(177)))));
            this.UndoButton.Image = ((System.Drawing.Image)(resources.GetObject("UndoButton.Image")));
            this.UndoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UndoButton.Margin = new System.Windows.Forms.Padding(0);
            this.UndoButton.Name = "UndoButton";
            this.UndoButton.Size = new System.Drawing.Size(24, 25);
            this.UndoButton.Click += new System.EventHandler(this.UndoButton_Click);
            // 
            // RedoButton
            // 
            this.RedoButton.AutoSize = false;
            this.RedoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RedoButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(90)))), ((int)(((byte)(177)))));
            this.RedoButton.Image = ((System.Drawing.Image)(resources.GetObject("RedoButton.Image")));
            this.RedoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RedoButton.Margin = new System.Windows.Forms.Padding(0);
            this.RedoButton.Name = "RedoButton";
            this.RedoButton.Size = new System.Drawing.Size(24, 25);
            this.RedoButton.Click += new System.EventHandler(this.RedoButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 29);
            // 
            // FormatButton
            // 
            this.FormatButton.AutoSize = false;
            this.FormatButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FormatButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(90)))), ((int)(((byte)(177)))));
            this.FormatButton.Image = ((System.Drawing.Image)(resources.GetObject("FormatButton.Image")));
            this.FormatButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FormatButton.Margin = new System.Windows.Forms.Padding(0);
            this.FormatButton.Name = "FormatButton";
            this.FormatButton.Size = new System.Drawing.Size(24, 25);
            this.FormatButton.Click += new System.EventHandler(this.FormatButton_Click);
            // 
            // RemarkButton
            // 
            this.RemarkButton.AutoSize = false;
            this.RemarkButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RemarkButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(90)))), ((int)(((byte)(177)))));
            this.RemarkButton.Image = global::C2.Properties.Resources.notes;
            this.RemarkButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RemarkButton.Margin = new System.Windows.Forms.Padding(0);
            this.RemarkButton.Name = "RemarkButton";
            this.RemarkButton.Size = new System.Drawing.Size(24, 25);
            this.RemarkButton.Click += new System.EventHandler(this.RemarkButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 29);
            // 
            // ZoomUpButton
            // 
            this.ZoomUpButton.AutoSize = false;
            this.ZoomUpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomUpButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(90)))), ((int)(((byte)(177)))));
            this.ZoomUpButton.Image = global::C2.Properties.Resources.zoom_in;
            this.ZoomUpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomUpButton.Margin = new System.Windows.Forms.Padding(0);
            this.ZoomUpButton.Name = "ZoomUpButton";
            this.ZoomUpButton.Size = new System.Drawing.Size(24, 25);
            this.ZoomUpButton.Click += new System.EventHandler(this.ZoomUpButton_Click);
            // 
            // ZoomDownButton
            // 
            this.ZoomDownButton.AutoSize = false;
            this.ZoomDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomDownButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(90)))), ((int)(((byte)(177)))));
            this.ZoomDownButton.Image = global::C2.Properties.Resources.zoom_out;
            this.ZoomDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomDownButton.Margin = new System.Windows.Forms.Padding(0);
            this.ZoomDownButton.Name = "ZoomDownButton";
            this.ZoomDownButton.Size = new System.Drawing.Size(24, 25);
            this.ZoomDownButton.Click += new System.EventHandler(this.ZoomDownButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 29);
            // 
            // MoveButton
            // 
            this.MoveButton.AutoSize = false;
            this.MoveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MoveButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(90)))), ((int)(((byte)(177)))));
            this.MoveButton.Image = global::C2.Properties.Resources.hand;
            this.MoveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MoveButton.Margin = new System.Windows.Forms.Padding(0);
            this.MoveButton.Name = "MoveButton";
            this.MoveButton.Size = new System.Drawing.Size(24, 25);
            this.MoveButton.Click += new System.EventHandler(this.MoveButton_Click);
            // 
            // FrameButton
            // 
            this.FrameButton.AutoSize = false;
            this.FrameButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FrameButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(90)))), ((int)(((byte)(177)))));
            this.FrameButton.Image = global::C2.Properties.Resources.cursor;
            this.FrameButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FrameButton.Margin = new System.Windows.Forms.Padding(0);
            this.FrameButton.Name = "FrameButton";
            this.FrameButton.Size = new System.Drawing.Size(24, 25);
            this.FrameButton.Click += new System.EventHandler(this.FrameButton_Click);
            // 
            // LinePanel
            // 
            this.LinePanel.BackColor = System.Drawing.Color.DarkGray;
            this.LinePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LinePanel.ForeColor = System.Drawing.Color.Black;
            this.LinePanel.Location = new System.Drawing.Point(0, 28);
            this.LinePanel.Name = "LinePanel";
            this.LinePanel.Size = new System.Drawing.Size(990, 1);
            this.LinePanel.TabIndex = 3;
            // 
            // TopToolBarControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.LinePanel);
            this.Controls.Add(this.toolStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(90)))), ((int)(((byte)(177)))));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TopToolBarControl";
            this.Size = new System.Drawing.Size(990, 29);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripButton FormatButton;
        private System.Windows.Forms.ToolStripButton UndoButton;
        private System.Windows.Forms.ToolStripButton RedoButton;
        private System.Windows.Forms.ToolStripButton ImportModelButton;
        private System.Windows.Forms.ToolStripButton RemarkButton;
        private System.Windows.Forms.ToolStripButton ZoomUpButton;
        private System.Windows.Forms.ToolStripButton ZoomDownButton;
        private System.Windows.Forms.ToolStripButton MoveButton;
        private System.Windows.Forms.ToolStripButton FrameButton;
        private System.Windows.Forms.ToolStripButton SaveModelButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private Controls.ToolStripPro toolStrip1;
        private System.Windows.Forms.Panel LinePanel;
    }
}
