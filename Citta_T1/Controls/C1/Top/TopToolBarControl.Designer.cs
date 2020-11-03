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
            this.ImportModel = new System.Windows.Forms.Button();
            this.remarkPictureBox = new System.Windows.Forms.Button();
            this.zoomUpPictureBox = new System.Windows.Forms.Button();
            this.zoomDownPictureBox = new System.Windows.Forms.Button();
            this.movePictureBox = new System.Windows.Forms.Button();
            this.framePictureBox = new System.Windows.Forms.Button();
            this.undoButton = new System.Windows.Forms.Button();
            this.redoButton = new System.Windows.Forms.Button();
            this.saveAllButton = new System.Windows.Forms.Button();
            this.saveModelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // formatButton
            // 
            this.formatButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.formatButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.formatButton.Image = ((System.Drawing.Image)(resources.GetObject("formatButton.Image")));
            this.formatButton.Location = new System.Drawing.Point(231, 0);
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
            this.moreButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.moreButton.Image = ((System.Drawing.Image)(resources.GetObject("moreButton.Image")));
            this.moreButton.Location = new System.Drawing.Point(421, 0);
            this.moreButton.Name = "moreButton";
            this.moreButton.Size = new System.Drawing.Size(32, 32);
            this.moreButton.TabIndex = 0;
            this.moreButton.TabStop = false;
            this.toolTip1.SetToolTip(this.moreButton, "首选项配置,配置程序的各项参数");
            this.moreButton.UseVisualStyleBackColor = true;
            this.moreButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MoreButton_MouseClick);
            // 
            // ImportModel
            // 
            this.ImportModel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.ImportModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ImportModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.ImportModel.Image = global::C2.Properties.Resources.importmodel;
            this.ImportModel.Location = new System.Drawing.Point(79, 0);
            this.ImportModel.Name = "ImportModel";
            this.ImportModel.Size = new System.Drawing.Size(32, 32);
            this.ImportModel.TabIndex = 1;
            this.toolTip1.SetToolTip(this.ImportModel, "导入iao模型");
            this.ImportModel.UseVisualStyleBackColor = false;
            this.ImportModel.Click += new System.EventHandler(this.ImportModel_Click);
            // 
            // remarkPictureBox
            // 
            this.remarkPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.remarkPictureBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.remarkPictureBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.remarkPictureBox.Image = global::C2.Properties.Resources.notes;
            this.remarkPictureBox.Location = new System.Drawing.Point(193, 0);
            this.remarkPictureBox.Name = "remarkPictureBox";
            this.remarkPictureBox.Size = new System.Drawing.Size(32, 32);
            this.remarkPictureBox.TabIndex = 2;
            this.toolTip1.SetToolTip(this.remarkPictureBox, "编写备注信息");
            this.remarkPictureBox.UseVisualStyleBackColor = false;
            this.remarkPictureBox.Click += new System.EventHandler(this.RemarkPictureBox_Click);
            // 
            // zoomUpPictureBox
            // 
            this.zoomUpPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.zoomUpPictureBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zoomUpPictureBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.zoomUpPictureBox.Image = global::C2.Properties.Resources.zoom_in;
            this.zoomUpPictureBox.Location = new System.Drawing.Point(269, 0);
            this.zoomUpPictureBox.Name = "zoomUpPictureBox";
            this.zoomUpPictureBox.Size = new System.Drawing.Size(32, 32);
            this.zoomUpPictureBox.TabIndex = 3;
            this.zoomUpPictureBox.TabStop = false;
            this.toolTip1.SetToolTip(this.zoomUpPictureBox, "放大屏幕中算子并支持三级放大");
            this.zoomUpPictureBox.UseVisualStyleBackColor = false;
            this.zoomUpPictureBox.Click += new System.EventHandler(this.ZoomUpPictureBox_Click);
            // 
            // zoomDownPictureBox
            // 
            this.zoomDownPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.zoomDownPictureBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zoomDownPictureBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.zoomDownPictureBox.Image = global::C2.Properties.Resources.zoom_out;
            this.zoomDownPictureBox.Location = new System.Drawing.Point(307, 0);
            this.zoomDownPictureBox.Name = "zoomDownPictureBox";
            this.zoomDownPictureBox.Size = new System.Drawing.Size(32, 32);
            this.zoomDownPictureBox.TabIndex = 4;
            this.zoomDownPictureBox.TabStop = false;
            this.toolTip1.SetToolTip(this.zoomDownPictureBox, "缩小当前屏幕中算子");
            this.zoomDownPictureBox.UseVisualStyleBackColor = false;
            this.zoomDownPictureBox.Click += new System.EventHandler(this.ZoomDownPictureBox_Click);
            // 
            // movePictureBox
            // 
            this.movePictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.movePictureBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.movePictureBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.movePictureBox.Image = global::C2.Properties.Resources.hand;
            this.movePictureBox.Location = new System.Drawing.Point(383, 0);
            this.movePictureBox.Name = "movePictureBox";
            this.movePictureBox.Size = new System.Drawing.Size(32, 32);
            this.movePictureBox.TabIndex = 5;
            this.movePictureBox.TabStop = false;
            this.toolTip1.SetToolTip(this.movePictureBox, "拖动当前视野屏幕");
            this.movePictureBox.UseVisualStyleBackColor = false;
            this.movePictureBox.Click += new System.EventHandler(this.MovePictureBox_Click);
            // 
            // framePictureBox
            // 
            this.framePictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.framePictureBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.framePictureBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.framePictureBox.Image = global::C2.Properties.Resources.cursor;
            this.framePictureBox.Location = new System.Drawing.Point(345, 0);
            this.framePictureBox.Name = "framePictureBox";
            this.framePictureBox.Size = new System.Drawing.Size(32, 32);
            this.framePictureBox.TabIndex = 6;
            this.framePictureBox.TabStop = false;
            this.toolTip1.SetToolTip(this.framePictureBox, "框选屏幕中算子进行整体拖动");
            this.framePictureBox.UseVisualStyleBackColor = false;
            this.framePictureBox.Click += new System.EventHandler(this.FramePictureBox_Click);
            // 
            // undoButton
            // 
            this.undoButton.Enabled = false;
            this.undoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.undoButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.undoButton.Image = ((System.Drawing.Image)(resources.GetObject("undoButton.Image")));
            this.undoButton.Location = new System.Drawing.Point(117, 0);
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(32, 32);
            this.undoButton.TabIndex = 0;
            this.undoButton.TabStop = false;
            this.undoButton.UseVisualStyleBackColor = true;
            this.undoButton.Click += new System.EventHandler(this.UndoButton_Click);
            // 
            // redoButton
            // 
            this.redoButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.redoButton.Enabled = false;
            this.redoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.redoButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.redoButton.Image = ((System.Drawing.Image)(resources.GetObject("redoButton.Image")));
            this.redoButton.Location = new System.Drawing.Point(155, 0);
            this.redoButton.Name = "redoButton";
            this.redoButton.Size = new System.Drawing.Size(32, 32);
            this.redoButton.TabIndex = 0;
            this.redoButton.TabStop = false;
            this.redoButton.UseVisualStyleBackColor = false;
            this.redoButton.Click += new System.EventHandler(this.RedoButton_Click);
            // 
            // saveAllButton
            // 
            this.saveAllButton.Enabled = false;
            this.saveAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveAllButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.saveAllButton.Image = global::C2.Properties.Resources.saveAllButton;
            this.saveAllButton.Location = new System.Drawing.Point(3, 0);
            this.saveAllButton.Name = "saveAllButton";
            this.saveAllButton.Size = new System.Drawing.Size(32, 32);
            this.saveAllButton.TabIndex = 7;
            this.saveAllButton.TabStop = false;
            this.saveAllButton.UseVisualStyleBackColor = true;
            // 
            // saveModelButton
            // 
            this.saveModelButton.Enabled = false;
            this.saveModelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveModelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.saveModelButton.Image = global::C2.Properties.Resources.save;
            this.saveModelButton.Location = new System.Drawing.Point(41, 0);
            this.saveModelButton.Name = "saveModelButton";
            this.saveModelButton.Size = new System.Drawing.Size(32, 32);
            this.saveModelButton.TabIndex = 8;
            this.saveModelButton.TabStop = false;
            this.saveModelButton.UseVisualStyleBackColor = true;
            // 
            // TopToolBarControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.Controls.Add(this.saveModelButton);
            this.Controls.Add(this.saveAllButton);
            this.Controls.Add(this.framePictureBox);
            this.Controls.Add(this.movePictureBox);
            this.Controls.Add(this.zoomDownPictureBox);
            this.Controls.Add(this.zoomUpPictureBox);
            this.Controls.Add(this.remarkPictureBox);
            this.Controls.Add(this.ImportModel);
            this.Controls.Add(this.redoButton);
            this.Controls.Add(this.undoButton);
            this.Controls.Add(this.moreButton);
            this.Controls.Add(this.formatButton);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
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
        private System.Windows.Forms.Button remarkPictureBox;
        private System.Windows.Forms.Button zoomUpPictureBox;
        private System.Windows.Forms.Button zoomDownPictureBox;
        private System.Windows.Forms.Button movePictureBox;
        private System.Windows.Forms.Button framePictureBox;
        private System.Windows.Forms.Button saveAllButton;
        private System.Windows.Forms.Button saveModelButton;
    }
}
