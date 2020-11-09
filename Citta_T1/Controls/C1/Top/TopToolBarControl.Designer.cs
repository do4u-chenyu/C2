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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip1 = new C2.Controls.ToolStripPro();
            this.saveAllButton = new System.Windows.Forms.ToolStripButton();
            this.saveModelButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.undoButton = new System.Windows.Forms.ToolStripButton();
            this.redoButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ImportModel = new System.Windows.Forms.ToolStripButton();
            this.formatButton = new System.Windows.Forms.ToolStripButton();
            this.remarkPictureBox = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.zoomUpPictureBox = new System.Windows.Forms.ToolStripButton();
            this.zoomDownPictureBox = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.movePictureBox = new System.Windows.Forms.ToolStripButton();
            this.framePictureBox = new System.Windows.Forms.ToolStripButton();
            this.moreButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(90)))), ((int)(((byte)(177)))));
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(10, 0, 2, 0);
            this.toolStrip1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAllButton,
            this.saveModelButton,
            this.toolStripSeparator1,
            this.undoButton,
            this.redoButton,
            this.toolStripSeparator2,
            this.ImportModel,
            this.formatButton,
            this.remarkPictureBox,
            this.toolStripSeparator3,
            this.zoomUpPictureBox,
            this.zoomDownPictureBox,
            this.toolStripSeparator4,
            this.movePictureBox,
            this.framePictureBox,
            this.moreButton});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(990, 28);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 2;
            // 
            // saveAllButton
            // 
            this.saveAllButton.AutoSize = false;
            this.saveAllButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveAllButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.saveAllButton.Image = global::C2.Properties.Resources.saveAllButton;
            this.saveAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveAllButton.Margin = new System.Windows.Forms.Padding(0);
            this.saveAllButton.Name = "saveAllButton";
            this.saveAllButton.Size = new System.Drawing.Size(24, 25);
            this.saveAllButton.Click += new System.EventHandler(this.formatButton_Click);
            // 
            // saveModelButton
            // 
            this.saveModelButton.AutoSize = false;
            this.saveModelButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveModelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.saveModelButton.Image = global::C2.Properties.Resources.save;
            this.saveModelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveModelButton.Margin = new System.Windows.Forms.Padding(0);
            this.saveModelButton.Name = "saveModelButton";
            this.saveModelButton.Size = new System.Drawing.Size(24, 25);
            this.saveModelButton.Click += new System.EventHandler(this.SaveModelButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Padding = new System.Windows.Forms.Padding(2);
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // undoButton
            // 
            this.undoButton.AutoSize = false;
            this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.undoButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.undoButton.Image = ((System.Drawing.Image)(resources.GetObject("undoButton.Image")));
            this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undoButton.Margin = new System.Windows.Forms.Padding(0);
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(24, 25);
            this.undoButton.Click += new System.EventHandler(this.UndoButton_Click);
            // 
            // redoButton
            // 
            this.redoButton.AutoSize = false;
            this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.redoButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.redoButton.Image = ((System.Drawing.Image)(resources.GetObject("redoButton.Image")));
            this.redoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.redoButton.Margin = new System.Windows.Forms.Padding(0);
            this.redoButton.Name = "redoButton";
            this.redoButton.Size = new System.Drawing.Size(24, 25);
            this.redoButton.Click += new System.EventHandler(this.RedoButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // ImportModel
            // 
            this.ImportModel.AutoSize = false;
            this.ImportModel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ImportModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.ImportModel.Image = global::C2.Properties.Resources.importmodel;
            this.ImportModel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ImportModel.Margin = new System.Windows.Forms.Padding(0);
            this.ImportModel.Name = "ImportModel";
            this.ImportModel.Size = new System.Drawing.Size(24, 25);
            this.ImportModel.Click += new System.EventHandler(this.ImportModel_Click);
            // 
            // formatButton
            // 
            this.formatButton.AutoSize = false;
            this.formatButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.formatButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.formatButton.Image = ((System.Drawing.Image)(resources.GetObject("formatButton.Image")));
            this.formatButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.formatButton.Margin = new System.Windows.Forms.Padding(0);
            this.formatButton.Name = "formatButton";
            this.formatButton.Size = new System.Drawing.Size(24, 25);
            this.formatButton.Click += new System.EventHandler(this.formatButton_Click);
            // 
            // remarkPictureBox
            // 
            this.remarkPictureBox.AutoSize = false;
            this.remarkPictureBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.remarkPictureBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.remarkPictureBox.Image = global::C2.Properties.Resources.notes;
            this.remarkPictureBox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.remarkPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.remarkPictureBox.Name = "remarkPictureBox";
            this.remarkPictureBox.Size = new System.Drawing.Size(24, 25);
            this.remarkPictureBox.Click += new System.EventHandler(this.RemarkPictureBox_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 28);
            // 
            // zoomUpPictureBox
            // 
            this.zoomUpPictureBox.AutoSize = false;
            this.zoomUpPictureBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomUpPictureBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.zoomUpPictureBox.Image = global::C2.Properties.Resources.zoom_in;
            this.zoomUpPictureBox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomUpPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.zoomUpPictureBox.Name = "zoomUpPictureBox";
            this.zoomUpPictureBox.Size = new System.Drawing.Size(24, 25);
            this.zoomUpPictureBox.Click += new System.EventHandler(this.ZoomUpPictureBox_Click);
            // 
            // zoomDownPictureBox
            // 
            this.zoomDownPictureBox.AutoSize = false;
            this.zoomDownPictureBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomDownPictureBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.zoomDownPictureBox.Image = global::C2.Properties.Resources.zoom_out;
            this.zoomDownPictureBox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomDownPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.zoomDownPictureBox.Name = "zoomDownPictureBox";
            this.zoomDownPictureBox.Size = new System.Drawing.Size(24, 25);
            this.zoomDownPictureBox.Click += new System.EventHandler(this.ZoomDownPictureBox_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 28);
            // 
            // movePictureBox
            // 
            this.movePictureBox.AutoSize = false;
            this.movePictureBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.movePictureBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.movePictureBox.Image = global::C2.Properties.Resources.hand;
            this.movePictureBox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.movePictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.movePictureBox.Name = "movePictureBox";
            this.movePictureBox.Size = new System.Drawing.Size(24, 25);
            this.movePictureBox.Click += new System.EventHandler(this.MovePictureBox_Click);
            // 
            // framePictureBox
            // 
            this.framePictureBox.AutoSize = false;
            this.framePictureBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.framePictureBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.framePictureBox.Image = global::C2.Properties.Resources.cursor;
            this.framePictureBox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.framePictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.framePictureBox.Name = "framePictureBox";
            this.framePictureBox.Size = new System.Drawing.Size(24, 25);
            this.framePictureBox.Click += new System.EventHandler(this.FramePictureBox_Click);
            // 
            // moreButton
            // 
            this.moreButton.AutoSize = false;
            this.moreButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moreButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.moreButton.Image = ((System.Drawing.Image)(resources.GetObject("moreButton.Image")));
            this.moreButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moreButton.Margin = new System.Windows.Forms.Padding(0);
            this.moreButton.Name = "moreButton";
            this.moreButton.Size = new System.Drawing.Size(24, 25);
            this.moreButton.Click += new System.EventHandler(this.moreButton_Click);
            // 
            // TopToolBarControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.toolStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TopToolBarControl";
            this.Size = new System.Drawing.Size(990, 28);
            this.Load += new System.EventHandler(this.TopToolBarControl_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripButton formatButton;
        //private System.Windows.Forms.Button formatButton;
        private System.Windows.Forms.ToolStripButton moreButton;
        private System.Windows.Forms.ToolStripButton undoButton;
        private System.Windows.Forms.ToolStripButton redoButton;
        private System.Windows.Forms.ToolStripButton ImportModel;
        private System.Windows.Forms.ToolStripButton remarkPictureBox;
        private System.Windows.Forms.ToolStripButton zoomUpPictureBox;
        private System.Windows.Forms.ToolStripButton zoomDownPictureBox;
        private System.Windows.Forms.ToolStripButton movePictureBox;
        private System.Windows.Forms.ToolStripButton framePictureBox;
        private System.Windows.Forms.ToolStripButton saveAllButton;
        private System.Windows.Forms.ToolStripButton saveModelButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private Controls.ToolStripPro toolStrip1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
