namespace C2.Business.CastleBravo.WebShellTool
{
    partial class WebShellDetails
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
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("C:");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("work");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("D:", new System.Windows.Forms.TreeNode[] {
            treeNode5});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebShellDetails));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.fileManagerListView = new System.Windows.Forms.ListView();
            this.flvIcon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.flvName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.flvTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.flvSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.flvChmod = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.filePathTb = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.messageLog = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 242);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 216);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "基础信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitContainer1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(792, 216);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "文件管理";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
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
            this.splitContainer1.Size = new System.Drawing.Size(786, 210);
            this.splitContainer1.SplitterDistance = 262;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode4.Name = "节点0";
            treeNode4.Text = "C:";
            treeNode5.Name = "节点2";
            treeNode5.Text = "work";
            treeNode6.Name = "节点1";
            treeNode6.Text = "D:";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode6});
            this.treeView1.Size = new System.Drawing.Size(262, 210);
            this.treeView1.TabIndex = 0;
            // 
            // fileManagerListView
            // 
            this.fileManagerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.flvIcon,
            this.flvName,
            this.flvTime,
            this.flvSize,
            this.flvChmod});
            this.fileManagerListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileManagerListView.HideSelection = false;
            this.fileManagerListView.Location = new System.Drawing.Point(0, 21);
            this.fileManagerListView.Name = "fileManagerListView";
            this.fileManagerListView.Size = new System.Drawing.Size(520, 189);
            this.fileManagerListView.TabIndex = 1;
            this.fileManagerListView.UseCompatibleStateImageBehavior = false;
            this.fileManagerListView.View = System.Windows.Forms.View.Details;
            // 
            // flvIcon
            // 
            this.flvIcon.Text = ">";
            // 
            // flvName
            // 
            this.flvName.Text = "名字";
            this.flvName.Width = 162;
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
            // filePathTb
            // 
            this.filePathTb.Dock = System.Windows.Forms.DockStyle.Top;
            this.filePathTb.Location = new System.Drawing.Point(0, 0);
            this.filePathTb.Name = "filePathTb";
            this.filePathTb.Size = new System.Drawing.Size(520, 21);
            this.filePathTb.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(792, 216);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "数据库管理";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 216);
            this.tabPage2.TabIndex = 4;
            this.tabPage2.Text = "ShellCmd";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder");
            this.imageList1.Images.SetKeyName(1, "file");
            this.imageList1.Images.SetKeyName(2, "text");
            this.imageList1.Images.SetKeyName(3, "image");
            // 
            // messageLog
            // 
            this.messageLog.BackColor = System.Drawing.Color.White;
            this.messageLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageLog.Location = new System.Drawing.Point(0, 242);
            this.messageLog.Multiline = true;
            this.messageLog.Name = "messageLog";
            this.messageLog.ReadOnly = true;
            this.messageLog.Size = new System.Drawing.Size(800, 208);
            this.messageLog.TabIndex = 1;
            // 
            // WebShellDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.messageLog);
            this.Controls.Add(this.tabControl1);
            this.Name = "WebShellDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WebShell详情";
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ListView fileManagerListView;
        private System.Windows.Forms.ColumnHeader flvIcon;
        private System.Windows.Forms.ColumnHeader flvName;
        private System.Windows.Forms.ColumnHeader flvTime;
        private System.Windows.Forms.ColumnHeader flvSize;
        private System.Windows.Forms.ColumnHeader flvChmod;
        private System.Windows.Forms.TextBox filePathTb;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox messageLog;
        private System.Windows.Forms.TabPage tabPage2;
    }
}