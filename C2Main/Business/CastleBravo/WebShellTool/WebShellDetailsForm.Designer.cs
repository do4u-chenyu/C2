namespace C2.Business.CastleBravo.WebShellTool
{
    partial class WebShellDetailsForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebShellDetailsForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.baseInfoWebBrowser = new System.Windows.Forms.WebBrowser();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.fileManagerListView = new System.Windows.Forms.ListView();
            this.flvName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.flvTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.flvSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.flvChmod = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.浏览ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下载ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filePathTb = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.excuteBtn = new System.Windows.Forms.Button();
            this.cmdTextBox = new System.Windows.Forms.TextBox();
            this.messageLog = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1067, 387);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.baseInfoWebBrowser);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1059, 354);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "基础信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // baseInfoWebBrowser
            // 
            this.baseInfoWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baseInfoWebBrowser.Location = new System.Drawing.Point(4, 4);
            this.baseInfoWebBrowser.Margin = new System.Windows.Forms.Padding(4);
            this.baseInfoWebBrowser.MinimumSize = new System.Drawing.Size(27, 25);
            this.baseInfoWebBrowser.Name = "baseInfoWebBrowser";
            this.baseInfoWebBrowser.Size = new System.Drawing.Size(1051, 346);
            this.baseInfoWebBrowser.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitContainer1);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage3.Size = new System.Drawing.Size(1059, 354);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "文件管理";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(4, 4);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.fileManagerListView);
            this.splitContainer1.Panel2.Controls.Add(this.filePathTb);
            this.splitContainer1.Size = new System.Drawing.Size(1051, 346);
            this.splitContainer1.SplitterDistance = 348;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(348, 346);
            this.treeView1.TabIndex = 0;
            this.treeView1.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView1_BeforeCollapse);
            this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView1_BeforeExpand);
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView1_NodeMouseDoubleClick);
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TreeView1_MouseDown);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder");
            this.imageList1.Images.SetKeyName(1, "file");
            this.imageList1.Images.SetKeyName(2, "text");
            this.imageList1.Images.SetKeyName(3, "image");
            this.imageList1.Images.SetKeyName(4, "driver");
            // 
            // fileManagerListView
            // 
            this.fileManagerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.flvName,
            this.flvTime,
            this.flvSize,
            this.flvChmod});
            this.fileManagerListView.ContextMenuStrip = this.contextMenuStrip1;
            this.fileManagerListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileManagerListView.HideSelection = false;
            this.fileManagerListView.Location = new System.Drawing.Point(0, 27);
            this.fileManagerListView.Margin = new System.Windows.Forms.Padding(4);
            this.fileManagerListView.MultiSelect = false;
            this.fileManagerListView.Name = "fileManagerListView";
            this.fileManagerListView.Size = new System.Drawing.Size(698, 319);
            this.fileManagerListView.SmallImageList = this.imageList1;
            this.fileManagerListView.TabIndex = 1;
            this.fileManagerListView.UseCompatibleStateImageBehavior = false;
            this.fileManagerListView.View = System.Windows.Forms.View.Details;
            this.fileManagerListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FileManagerListView_MouseDoubleClick);
            // 
            // flvName
            // 
            this.flvName.Text = "名字";
            this.flvName.Width = 207;
            // 
            // flvTime
            // 
            this.flvTime.Text = "时间";
            this.flvTime.Width = 156;
            // 
            // flvSize
            // 
            this.flvSize.Text = "文件大小";
            this.flvSize.Width = 69;
            // 
            // flvChmod
            // 
            this.flvChmod.Text = "属性";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.浏览ToolStripMenuItem,
            this.下载ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(109, 52);
            // 
            // 浏览ToolStripMenuItem
            // 
            this.浏览ToolStripMenuItem.Name = "浏览ToolStripMenuItem";
            this.浏览ToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.浏览ToolStripMenuItem.Text = "浏览";
            this.浏览ToolStripMenuItem.Click += new System.EventHandler(this.浏览ToolStripMenuItem_Click);
            // 
            // 下载ToolStripMenuItem
            // 
            this.下载ToolStripMenuItem.Name = "下载ToolStripMenuItem";
            this.下载ToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.下载ToolStripMenuItem.Text = "下载";
            this.下载ToolStripMenuItem.Click += new System.EventHandler(this.下载ToolStripMenuItem_Click);
            // 
            // filePathTb
            // 
            this.filePathTb.BackColor = System.Drawing.Color.White;
            this.filePathTb.Dock = System.Windows.Forms.DockStyle.Top;
            this.filePathTb.Location = new System.Drawing.Point(0, 0);
            this.filePathTb.Margin = new System.Windows.Forms.Padding(4);
            this.filePathTb.Name = "filePathTb";
            this.filePathTb.ReadOnly = true;
            this.filePathTb.Size = new System.Drawing.Size(698, 27);
            this.filePathTb.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dataGridView1);
            this.tabPage4.Controls.Add(this.treeView2);
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage4.Size = new System.Drawing.Size(1059, 354);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "数据库管理";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(203, 4);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(852, 346);
            this.dataGridView1.TabIndex = 1;
            // 
            // treeView2
            // 
            this.treeView2.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView2.Location = new System.Drawing.Point(4, 4);
            this.treeView2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.treeView2.Name = "treeView2";
            this.treeView2.Size = new System.Drawing.Size(200, 346);
            this.treeView2.TabIndex = 0;
            this.treeView2.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView2_NodeMouseDoubleClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.outputTextBox);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(1059, 354);
            this.tabPage2.TabIndex = 4;
            this.tabPage2.Text = "虚拟终端";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // outputTextBox
            // 
            this.outputTextBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.outputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputTextBox.ForeColor = System.Drawing.SystemColors.Window;
            this.outputTextBox.Location = new System.Drawing.Point(4, 35);
            this.outputTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputTextBox.Size = new System.Drawing.Size(1051, 315);
            this.outputTextBox.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.excuteBtn);
            this.panel1.Controls.Add(this.cmdTextBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1051, 31);
            this.panel1.TabIndex = 0;
            // 
            // excuteBtn
            // 
            this.excuteBtn.Location = new System.Drawing.Point(909, -1);
            this.excuteBtn.Margin = new System.Windows.Forms.Padding(4);
            this.excuteBtn.Name = "excuteBtn";
            this.excuteBtn.Size = new System.Drawing.Size(135, 29);
            this.excuteBtn.TabIndex = 1;
            this.excuteBtn.Text = "执行命令";
            this.excuteBtn.UseVisualStyleBackColor = true;
            this.excuteBtn.Click += new System.EventHandler(this.ExcuteBtn_Click);
            // 
            // cmdTextBox
            // 
            this.cmdTextBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmdTextBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmdTextBox.Location = new System.Drawing.Point(0, 0);
            this.cmdTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.cmdTextBox.Name = "cmdTextBox";
            this.cmdTextBox.Size = new System.Drawing.Size(904, 25);
            this.cmdTextBox.TabIndex = 0;
            this.cmdTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmdTextBox_KeyDown);
            // 
            // messageLog
            // 
            this.messageLog.BackColor = System.Drawing.Color.White;
            this.messageLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageLog.Location = new System.Drawing.Point(0, 0);
            this.messageLog.Margin = new System.Windows.Forms.Padding(4);
            this.messageLog.Multiline = true;
            this.messageLog.Name = "messageLog";
            this.messageLog.ReadOnly = true;
            this.messageLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.messageLog.Size = new System.Drawing.Size(1067, 252);
            this.messageLog.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.messageLog);
            this.splitContainer2.Size = new System.Drawing.Size(1067, 644);
            this.splitContainer2.SplitterDistance = 387;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 2;
            // 
            // WebShellDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1067, 644);
            this.Controls.Add(this.splitContainer2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "WebShellDetailsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DD详情";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ListView fileManagerListView;
        private System.Windows.Forms.ColumnHeader flvName;
        private System.Windows.Forms.ColumnHeader flvTime;
        private System.Windows.Forms.ColumnHeader flvSize;
        private System.Windows.Forms.ColumnHeader flvChmod;
        private System.Windows.Forms.TextBox filePathTb;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox messageLog;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.WebBrowser baseInfoWebBrowser;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button excuteBtn;
        private System.Windows.Forms.TextBox cmdTextBox;
        private System.Windows.Forms.TreeView treeView2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 浏览ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下载ToolStripMenuItem;
    }
}