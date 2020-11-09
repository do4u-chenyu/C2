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
            this.formatButton = new System.Windows.Forms.ToolStripButton();
            this.moreButton = new System.Windows.Forms.ToolStripButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ImportModel = new System.Windows.Forms.ToolStripButton();
            this.remarkPictureBox = new System.Windows.Forms.ToolStripButton();
            this.zoomUpPictureBox = new System.Windows.Forms.ToolStripButton();
            this.zoomDownPictureBox = new System.Windows.Forms.ToolStripButton();
            this.movePictureBox = new System.Windows.Forms.ToolStripButton();
            this.framePictureBox = new System.Windows.Forms.ToolStripButton();
            this.undoButton = new System.Windows.Forms.ToolStripButton();
            this.redoButton = new System.Windows.Forms.ToolStripButton();
            this.saveAllButton = new System.Windows.Forms.ToolStripButton();
            this.saveModelButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new C2.Controls.ToolStripPro();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // formatButton
            // 
            this.formatButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.formatButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.formatButton.Image = ((System.Drawing.Image)(resources.GetObject("formatButton.Image")));
            this.formatButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.formatButton.Name = "formatButton";
            this.formatButton.Size = new System.Drawing.Size(23, 22);
            this.formatButton.Click += new System.EventHandler(this.formatButton_Click);
            // 
            // moreButton
            // 
            this.moreButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.moreButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.moreButton.Image = ((System.Drawing.Image)(resources.GetObject("moreButton.Image")));
            this.moreButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.moreButton.Name = "moreButton";
            this.moreButton.Size = new System.Drawing.Size(23, 22);
            this.moreButton.Click += new System.EventHandler(this.formatButton_Click);
            // 
            // ImportModel
            // 
            this.ImportModel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ImportModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.ImportModel.Image = global::C2.Properties.Resources.importmodel;
            this.ImportModel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ImportModel.Name = "ImportModel";
            this.ImportModel.Size = new System.Drawing.Size(23, 22);
            this.ImportModel.Click += new System.EventHandler(this.ImportModel_Click);
            // 
            // remarkPictureBox
            // 
            this.remarkPictureBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.remarkPictureBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.remarkPictureBox.Image = global::C2.Properties.Resources.notes;
            this.remarkPictureBox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.remarkPictureBox.Name = "remarkPictureBox";
            this.remarkPictureBox.Size = new System.Drawing.Size(23, 22);
            this.remarkPictureBox.Click += new System.EventHandler(this.RemarkPictureBox_Click);
            // 
            // zoomUpPictureBox
            // 
            this.zoomUpPictureBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomUpPictureBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.zoomUpPictureBox.Image = global::C2.Properties.Resources.zoom_in;
            this.zoomUpPictureBox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomUpPictureBox.Name = "zoomUpPictureBox";
            this.zoomUpPictureBox.Size = new System.Drawing.Size(23, 22);
            this.zoomUpPictureBox.Click += new System.EventHandler(this.ZoomUpPictureBox_Click);
            // 
            // zoomDownPictureBox
            // 
            this.zoomDownPictureBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomDownPictureBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.zoomDownPictureBox.Image = global::C2.Properties.Resources.zoom_out;
            this.zoomDownPictureBox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomDownPictureBox.Name = "zoomDownPictureBox";
            this.zoomDownPictureBox.Size = new System.Drawing.Size(23, 22);
            this.zoomDownPictureBox.Click += new System.EventHandler(this.ZoomDownPictureBox_Click);
            // 
            // movePictureBox
            // 
            this.movePictureBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.movePictureBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.movePictureBox.Image = global::C2.Properties.Resources.hand;
            this.movePictureBox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.movePictureBox.Name = "movePictureBox";
            this.movePictureBox.Size = new System.Drawing.Size(23, 22);
            this.movePictureBox.Click += new System.EventHandler(this.formatButton_Click);
            // 
            // framePictureBox
            // 
            this.framePictureBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.framePictureBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.framePictureBox.Image = global::C2.Properties.Resources.cursor;
            this.framePictureBox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.framePictureBox.Name = "framePictureBox";
            this.framePictureBox.Size = new System.Drawing.Size(23, 22);
            this.framePictureBox.Click += new System.EventHandler(this.FramePictureBox_Click);
            // 
            // undoButton
            // 
            this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.undoButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.undoButton.Image = ((System.Drawing.Image)(resources.GetObject("undoButton.Image")));
            this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(23, 22);
            this.undoButton.Click += new System.EventHandler(this.UndoButton_Click);
            // 
            // redoButton
            // 
            this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.redoButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.redoButton.Image = ((System.Drawing.Image)(resources.GetObject("redoButton.Image")));
            this.redoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.redoButton.Name = "redoButton";
            this.redoButton.Size = new System.Drawing.Size(23, 22);
            this.redoButton.Click += new System.EventHandler(this.RedoButton_Click);
            // 
            // saveAllButton
            // 
            this.saveAllButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveAllButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.saveAllButton.Image = global::C2.Properties.Resources.saveAllButton;
            this.saveAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveAllButton.Name = "saveAllButton";
            this.saveAllButton.Size = new System.Drawing.Size(23, 22);
            this.saveAllButton.Click += new System.EventHandler(this.formatButton_Click);
            // 
            // saveModelButton
            // 
            this.saveModelButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveModelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.saveModelButton.Image = global::C2.Properties.Resources.save;
            this.saveModelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveModelButton.Name = "saveModelButton";
            this.saveModelButton.Size = new System.Drawing.Size(23, 22);
            this.saveModelButton.Click += new System.EventHandler(this.SaveModelButton_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(90)))), ((int)(((byte)(177)))));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAllButton,
            this.saveModelButton,
            this.toolStripSeparator1,
            this.undoButton,
            this.redoButton,
            this.toolStripSeparator2,
            this.ImportModel,
            this.formatButton,
            this.moreButton,
            this.remarkPictureBox,
            this.toolStripSeparator3,
            this.zoomUpPictureBox,
            this.zoomDownPictureBox,
            this.movePictureBox,
            this.framePictureBox});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(990, 25);
            this.toolStrip1.TabIndex = 2;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Padding = new System.Windows.Forms.Padding(2);
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // TopToolBarControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.Controls.Add(this.toolStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
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
        private System.Windows.Forms.ToolTip toolTip1;
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
        private Controls.ToolStripPro toolStrip1;

    }
}
