using System.Windows.Forms;

namespace C2
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.headPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.helpPictureBox = new System.Windows.Forms.PictureBox();
            this.portraitpictureBox = new System.Windows.Forms.PictureBox();
            this.usernamelabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.leftMainMenuPanel = new System.Windows.Forms.Panel();
            this.flowChartButton = new System.Windows.Forms.Button();
            this.dataButton = new System.Windows.Forms.Button();
            this.operateButton = new System.Windows.Forms.Button();
            this.myModelButton = new System.Windows.Forms.Button();
            this.leftToolBoxPanel = new System.Windows.Forms.Panel();
            this.mindMapModelControl = new C2.Controls.Left.MindMapModelControl();
            this.dataSourceControl = new C2.Controls.Left.DataSourceControl();
            this.myModelControl = new C2.Controls.Left.MyModelControl();
            this.leftFoldPanel = new Controls.C1.Left.LeftFoldButton();
            this.leftFoldButton = new System.Windows.Forms.PictureBox();
            this.commonPanel = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.newModelButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.moreButton = new System.Windows.Forms.Button();
            this.formatButton = new System.Windows.Forms.Button();
            this.groupButton = new System.Windows.Forms.Button();
            this.interOpButton = new System.Windows.Forms.Button();
            this.unionButton = new System.Windows.Forms.Button();
            this.diffButton = new System.Windows.Forms.Button();
            this.filterButton = new System.Windows.Forms.Button();
            this.connectOpButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ImportDataSourceButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.minMaxPictureBox = new System.Windows.Forms.PictureBox();
            this.logLabel = new System.Windows.Forms.Label();
            this.pyControlLabel = new System.Windows.Forms.Label();
            this.previewLabel = new System.Windows.Forms.Label();
            this.dragLineControl = new C2.Controls.Bottom.DragLineControl();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.workSpacePanel = new System.Windows.Forms.Panel();
            this.mdiWorkSpace = new C2.WorkSpace.MdiWorkSpace();
            this.bottomViewPanel = new System.Windows.Forms.Panel();
            this.bottomPreview = new C2.Controls.Bottom.BottomPreviewControl();
            this.bottomLogControl = new C2.Controls.Bottom.BottomLogControl();
            this.bottomPyConsole = new C2.Controls.Bottom.BottomConsoleControl();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.tabPanel = new System.Windows.Forms.Panel();
            this.taskBar = new C2.Controls.TaskBar();
            this.blankButton = new System.Windows.Forms.Button();
            this.headPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.helpPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.portraitpictureBox)).BeginInit();
            this.leftMainMenuPanel.SuspendLayout();
            this.leftToolBoxPanel.SuspendLayout();
            this.leftFoldPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftFoldButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minMaxPictureBox)).BeginInit();
            this.MainPanel.SuspendLayout();
            this.workSpacePanel.SuspendLayout();
            this.bottomViewPanel.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel8.SuspendLayout();
            this.tabPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headPanel
            // 
            this.headPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(67)))), ((int)(((byte)(101)))));
            this.headPanel.Controls.Add(this.panel2);
            this.headPanel.Controls.Add(this.label1);
            this.headPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headPanel.Location = new System.Drawing.Point(0, 0);
            this.headPanel.Name = "headPanel";
            this.headPanel.Size = new System.Drawing.Size(1233, 46);
            this.headPanel.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.helpPictureBox);
            this.panel2.Controls.Add(this.portraitpictureBox);
            this.panel2.Controls.Add(this.usernamelabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(906, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(327, 46);
            this.panel2.TabIndex = 1;
            // 
            // helpPictureBox
            // 
            this.helpPictureBox.Cursor = System.Windows.Forms.Cursors.Help;
            this.helpPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("helpPictureBox.Image")));
            this.helpPictureBox.Location = new System.Drawing.Point(192, 10);
            this.helpPictureBox.Name = "helpPictureBox";
            this.helpPictureBox.Size = new System.Drawing.Size(24, 24);
            this.helpPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.helpPictureBox.TabIndex = 3;
            this.helpPictureBox.TabStop = false;
            this.helpPictureBox.Click += new System.EventHandler(this.HelpPictureBox_Click);
            // 
            // portraitpictureBox
            // 
            this.portraitpictureBox.Image = ((System.Drawing.Image)(resources.GetObject("portraitpictureBox.Image")));
            this.portraitpictureBox.Location = new System.Drawing.Point(222, 10);
            this.portraitpictureBox.Name = "portraitpictureBox";
            this.portraitpictureBox.Size = new System.Drawing.Size(24, 24);
            this.portraitpictureBox.TabIndex = 3;
            this.portraitpictureBox.TabStop = false;
            // 
            // usernamelabel
            // 
            this.usernamelabel.AutoSize = true;
            this.usernamelabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.usernamelabel.ForeColor = System.Drawing.Color.White;
            this.usernamelabel.Location = new System.Drawing.Point(257, 12);
            this.usernamelabel.Name = "usernamelabel";
            this.usernamelabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.usernamelabel.Size = new System.Drawing.Size(40, 22);
            this.usernamelabel.TabIndex = 3;
            this.usernamelabel.Text = "IAO";
            this.usernamelabel.MouseEnter += new System.EventHandler(this.UsernameLabel_MouseEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(282, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "IAO解决方案建模平台";
            // 
            // leftMainMenuPanel
            // 
            this.leftMainMenuPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(97)))), ((int)(((byte)(125)))));
            this.leftMainMenuPanel.Controls.Add(this.flowChartButton);
            this.leftMainMenuPanel.Controls.Add(this.dataButton);
            this.leftMainMenuPanel.Controls.Add(this.operateButton);
            this.leftMainMenuPanel.Controls.Add(this.myModelButton);
            this.leftMainMenuPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftMainMenuPanel.Location = new System.Drawing.Point(0, 46);
            this.leftMainMenuPanel.Name = "leftMainMenuPanel";
            this.leftMainMenuPanel.Size = new System.Drawing.Size(136, 560);
            this.leftMainMenuPanel.TabIndex = 1;
            // 
            // flowChartButton
            // 
            this.flowChartButton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.flowChartButton.Location = new System.Drawing.Point(4, 150);
            this.flowChartButton.Name = "flowChartButton";
            this.flowChartButton.Size = new System.Drawing.Size(124, 42);
            this.flowChartButton.TabIndex = 3;
            this.flowChartButton.Text = "IAO实验室";
            this.toolTip1.SetToolTip(this.flowChartButton, "数据分析建模需要的复杂模型探索");
            this.flowChartButton.UseVisualStyleBackColor = true;
            this.flowChartButton.Click += new System.EventHandler(this.FlowChartButton_Click);
            // 
            // dataButton
            // 
            this.dataButton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataButton.Location = new System.Drawing.Point(4, 102);
            this.dataButton.Name = "dataButton";
            this.dataButton.Size = new System.Drawing.Size(124, 42);
            this.dataButton.TabIndex = 2;
            this.dataButton.Text = "数据管理";
            this.toolTip1.SetToolTip(this.dataButton, "当前用户已导入的所有数据");
            this.dataButton.UseVisualStyleBackColor = true;
            this.dataButton.Click += new System.EventHandler(this.DataButton_Click);
            // 
            // operateButton
            // 
            this.operateButton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.operateButton.Location = new System.Drawing.Point(4, 6);
            this.operateButton.Name = "operateButton";
            this.operateButton.Size = new System.Drawing.Size(124, 42);
            this.operateButton.TabIndex = 1;
            this.operateButton.Text = "业务视图";
            this.toolTip1.SetToolTip(this.operateButton, "当前用户的所有业务视图");
            this.operateButton.UseVisualStyleBackColor = true;
            this.operateButton.Click += new System.EventHandler(this.OperateButton_Click);
            // 
            // myModelButton
            // 
            this.myModelButton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myModelButton.Location = new System.Drawing.Point(4, 54);
            this.myModelButton.Name = "myModelButton";
            this.myModelButton.Size = new System.Drawing.Size(124, 42);
            this.myModelButton.TabIndex = 0;
            this.myModelButton.Text = "模型市场";
            this.toolTip1.SetToolTip(this.myModelButton, "当前用户发布的所有模型");
            this.myModelButton.UseVisualStyleBackColor = true;
            this.myModelButton.Click += new System.EventHandler(this.MyModelButton_Click);
            // 
            // leftToolBoxPanel
            // 
            this.leftToolBoxPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.leftToolBoxPanel.Controls.Add(this.mindMapModelControl);
            this.leftToolBoxPanel.Controls.Add(this.dataSourceControl);
            this.leftToolBoxPanel.Controls.Add(this.myModelControl);
            this.leftToolBoxPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftToolBoxPanel.Location = new System.Drawing.Point(136, 46);
            this.leftToolBoxPanel.Name = "leftToolBoxPanel";
            this.leftToolBoxPanel.Size = new System.Drawing.Size(187, 560);
            this.leftToolBoxPanel.TabIndex = 2;
            // 
            // mindMapModelControl
            // 
            this.mindMapModelControl.AllowDrop = true;
            this.mindMapModelControl.AutoScroll = true;
            this.mindMapModelControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mindMapModelControl.BackColor = System.Drawing.Color.White;
            this.mindMapModelControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.mindMapModelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mindMapModelControl.Location = new System.Drawing.Point(0, 0);
            this.mindMapModelControl.Margin = new System.Windows.Forms.Padding(4);
            this.mindMapModelControl.Name = "mindMapModelControl";
            this.mindMapModelControl.Size = new System.Drawing.Size(185, 558);
            this.mindMapModelControl.TabIndex = 0;
            this.mindMapModelControl.Visible = false;
            // 
            // dataSourceControl
            // 
            this.dataSourceControl.AllowDrop = true;
            this.dataSourceControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dataSourceControl.BackColor = System.Drawing.Color.White;
            this.dataSourceControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.dataSourceControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataSourceControl.Location = new System.Drawing.Point(0, 0);
            this.dataSourceControl.Margin = new System.Windows.Forms.Padding(4);
            this.dataSourceControl.Name = "dataSourceControl";
            this.dataSourceControl.Size = new System.Drawing.Size(185, 558);
            this.dataSourceControl.TabIndex = 0;
            // 
            // myModelControl
            // 
            this.myModelControl.AutoScroll = true;
            this.myModelControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.myModelControl.BackColor = System.Drawing.Color.White;
            this.myModelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myModelControl.Location = new System.Drawing.Point(0, 0);
            this.myModelControl.Margin = new System.Windows.Forms.Padding(4);
            this.myModelControl.Name = "myModelControl";
            this.myModelControl.Size = new System.Drawing.Size(185, 558);
            this.myModelControl.TabIndex = 0;
            this.myModelControl.Visible = false;
            // 
            // leftFoldPanel
            // 
            this.leftFoldPanel.Location = new System.Drawing.Point(0, 200);
            this.leftFoldPanel.Margin = new System.Windows.Forms.Padding(0);
            this.leftFoldPanel.Name = "leftFoldPanel";
            this.leftFoldPanel.Size = new System.Drawing.Size(7, 100);
            this.leftFoldPanel.TabIndex = 1;
            this.leftFoldPanel.Click += new System.EventHandler(this.LeftFoldButton_Click);
            // 
            // leftFoldButton
            // 
            this.leftFoldButton.BackColor = System.Drawing.SystemColors.Control;
            this.leftFoldButton.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.leftFoldButton.Image = ((System.Drawing.Image)(resources.GetObject("leftFoldButton.Image")));
            this.leftFoldButton.Location = new System.Drawing.Point(0, 0);
            this.leftFoldButton.Margin = new System.Windows.Forms.Padding(0);
            this.leftFoldButton.Name = "leftFoldButton";
            this.leftFoldButton.Size = new System.Drawing.Size(7, 100);
            this.leftFoldButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.leftFoldButton.TabIndex = 0;
            this.leftFoldButton.TabStop = false;
            this.toolTip1.SetToolTip(this.leftFoldButton, "隐藏左侧面板");
            this.leftFoldButton.Click += new System.EventHandler(this.LeftFoldButton_Click);
            // 
            // commonPanel
            // 
            this.commonPanel.Location = new System.Drawing.Point(0, 0);
            this.commonPanel.Name = "commonPanel";
            this.commonPanel.Size = new System.Drawing.Size(200, 100);
            this.commonPanel.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(200, 100);
            this.panel5.TabIndex = 0;
            // 
            // newModelButton
            // 
            this.newModelButton.BackColor = System.Drawing.Color.White;
            this.newModelButton.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.newModelButton.Image = ((System.Drawing.Image)(resources.GetObject("newModelButton.Image")));
            this.newModelButton.Location = new System.Drawing.Point(3, 8);
            this.newModelButton.Name = "newModelButton";
            this.newModelButton.Size = new System.Drawing.Size(79, 32);
            this.newModelButton.TabIndex = 0;
            this.toolTip1.SetToolTip(this.newModelButton, "新建模型");
            this.newModelButton.UseVisualStyleBackColor = false;
            this.newModelButton.Click += new System.EventHandler(this.NewModelButton_Click);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 23);
            this.label7.TabIndex = 0;
            // 
            // moreButton
            // 
            this.moreButton.Location = new System.Drawing.Point(0, 0);
            this.moreButton.Name = "moreButton";
            this.moreButton.Size = new System.Drawing.Size(75, 23);
            this.moreButton.TabIndex = 0;
            // 
            // formatButton
            // 
            this.formatButton.Location = new System.Drawing.Point(0, 0);
            this.formatButton.Name = "formatButton";
            this.formatButton.Size = new System.Drawing.Size(75, 23);
            this.formatButton.TabIndex = 0;
            // 
            // groupButton
            // 
            this.groupButton.Location = new System.Drawing.Point(0, 0);
            this.groupButton.Name = "groupButton";
            this.groupButton.Size = new System.Drawing.Size(75, 23);
            this.groupButton.TabIndex = 0;
            // 
            // interOpButton
            // 
            this.interOpButton.Location = new System.Drawing.Point(0, 0);
            this.interOpButton.Name = "interOpButton";
            this.interOpButton.Size = new System.Drawing.Size(75, 23);
            this.interOpButton.TabIndex = 0;
            // 
            // unionButton
            // 
            this.unionButton.Location = new System.Drawing.Point(0, 0);
            this.unionButton.Name = "unionButton";
            this.unionButton.Size = new System.Drawing.Size(75, 23);
            this.unionButton.TabIndex = 0;
            // 
            // diffButton
            // 
            this.diffButton.Location = new System.Drawing.Point(0, 0);
            this.diffButton.Name = "diffButton";
            this.diffButton.Size = new System.Drawing.Size(75, 23);
            this.diffButton.TabIndex = 0;
            // 
            // filterButton
            // 
            this.filterButton.Location = new System.Drawing.Point(0, 0);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(75, 23);
            this.filterButton.TabIndex = 0;
            // 
            // connectOpButton
            // 
            this.connectOpButton.Location = new System.Drawing.Point(0, 0);
            this.connectOpButton.Name = "connectOpButton";
            this.connectOpButton.Size = new System.Drawing.Size(75, 23);
            this.connectOpButton.TabIndex = 0;
            // 
            // ImportDataSourceButton
            // 
            this.ImportDataSourceButton.BackColor = System.Drawing.Color.GhostWhite;
            this.ImportDataSourceButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ImportDataSourceButton.ForeColor = System.Drawing.Color.GhostWhite;
            this.ImportDataSourceButton.Image = global::C2.Properties.Resources.importDataSource;
            this.ImportDataSourceButton.Location = new System.Drawing.Point(66, 0);
            this.ImportDataSourceButton.Name = "ImportDataSourceButton";
            this.ImportDataSourceButton.Size = new System.Drawing.Size(32, 32);
            this.ImportDataSourceButton.TabIndex = 9;
            this.toolTip1.SetToolTip(this.ImportDataSourceButton, "导入本地数据文件,支持bcp,txt,csv,xls四种格式");
            this.ImportDataSourceButton.UseVisualStyleBackColor = false;
            this.ImportDataSourceButton.Click += new System.EventHandler(this.ImportDataSource_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.GhostWhite;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.GhostWhite;
            this.button1.Image = global::C2.Properties.Resources.importDataSource;
            this.button1.Location = new System.Drawing.Point(94, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 32);
            this.button1.TabIndex = 10;
            this.toolTip1.SetToolTip(this.button1, "临时新建一个DocumentForm窗体");
            this.button1.UseVisualStyleBackColor = false;
            // 
            // minMaxPictureBox
            // 
            this.minMaxPictureBox.Image = global::C2.Properties.Resources.maxunfold;
            this.minMaxPictureBox.Location = new System.Drawing.Point(115, 12);
            this.minMaxPictureBox.Name = "minMaxPictureBox";
            this.minMaxPictureBox.Size = new System.Drawing.Size(25, 24);
            this.minMaxPictureBox.TabIndex = 1;
            this.minMaxPictureBox.TabStop = false;
            this.toolTip1.SetToolTip(this.minMaxPictureBox, "隐藏底层面板");
            this.minMaxPictureBox.Click += new System.EventHandler(this.minMaxPictureBox_Click);
            // 
            // logLabel
            // 
            this.logLabel.AutoSize = true;
            this.logLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.logLabel.Location = new System.Drawing.Point(120, 8);
            this.logLabel.Name = "logLabel";
            this.logLabel.Size = new System.Drawing.Size(74, 22);
            this.logLabel.TabIndex = 3;
            this.logLabel.Text = "运行日志";
            this.toolTip1.SetToolTip(this.logLabel, "当前模型运行情况的日志信息.");
            this.logLabel.Click += new System.EventHandler(this.LogLabel_Click);
            // 
            // pyControlLabel
            // 
            this.pyControlLabel.AutoSize = true;
            this.pyControlLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.pyControlLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pyControlLabel.Location = new System.Drawing.Point(236, 8);
            this.pyControlLabel.Name = "pyControlLabel";
            this.pyControlLabel.Size = new System.Drawing.Size(58, 22);
            this.pyControlLabel.TabIndex = 2;
            this.pyControlLabel.Text = "控制台";
            this.toolTip1.SetToolTip(this.pyControlLabel, "Cmd控制台,用来调试第三方脚本.");
            this.pyControlLabel.Click += new System.EventHandler(this.PyControlLabel_Click);
            // 
            // previewLabel
            // 
            this.previewLabel.AutoSize = true;
            this.previewLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.previewLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.previewLabel.Location = new System.Drawing.Point(14, 8);
            this.previewLabel.Name = "previewLabel";
            this.previewLabel.Size = new System.Drawing.Size(74, 22);
            this.previewLabel.TabIndex = 0;
            this.previewLabel.Text = "数据预览";
            this.toolTip1.SetToolTip(this.previewLabel, "当前模型对应数据源的部分数据预览.");
            this.previewLabel.Click += new System.EventHandler(this.PreviewLabel_Click);
            // 
            // dragLineControl
            // 
            this.dragLineControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.dragLineControl.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.dragLineControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dragLineControl.Location = new System.Drawing.Point(394, 34);
            this.dragLineControl.Margin = new System.Windows.Forms.Padding(4);
            this.dragLineControl.Name = "dragLineControl";
            this.dragLineControl.Size = new System.Drawing.Size(366, 3);
            this.dragLineControl.TabIndex = 3;
            this.toolTip1.SetToolTip(this.dragLineControl, "按住鼠标左键可以上下拖动改变预览面板的大小.");
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.workSpacePanel);
            this.MainPanel.Controls.Add(this.tabPanel);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(323, 46);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(910, 560);
            this.MainPanel.TabIndex = 7;
            // 
            // workSpacePanel
            // 
            this.workSpacePanel.Controls.Add(this.leftFoldPanel);
            this.workSpacePanel.Controls.Add(this.mdiWorkSpace);
            this.workSpacePanel.Controls.Add(this.bottomViewPanel);
            this.workSpacePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workSpacePanel.Location = new System.Drawing.Point(0, 32);
            this.workSpacePanel.Name = "workSpacePanel";
            this.workSpacePanel.Size = new System.Drawing.Size(910, 528);
            this.workSpacePanel.TabIndex = 10;
            // 
            // mdiWorkSpace
            // 
            this.mdiWorkSpace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mdiWorkSpace.Location = new System.Drawing.Point(0, 0);
            this.mdiWorkSpace.Name = "mdiWorkSpace";
            this.mdiWorkSpace.Size = new System.Drawing.Size(910, 328);
            this.mdiWorkSpace.TabIndex = 8;
            // 
            // bottomViewPanel
            // 
            this.bottomViewPanel.Controls.Add(this.bottomPreview);
            this.bottomViewPanel.Controls.Add(this.bottomLogControl);
            this.bottomViewPanel.Controls.Add(this.bottomPyConsole);
            this.bottomViewPanel.Controls.Add(this.panel4);
            this.bottomViewPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomViewPanel.Location = new System.Drawing.Point(0, 328);
            this.bottomViewPanel.Name = "bottomViewPanel";
            this.bottomViewPanel.Size = new System.Drawing.Size(910, 200);
            this.bottomViewPanel.TabIndex = 11;
            // 
            // bottomPreview
            // 
            this.bottomPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomPreview.Location = new System.Drawing.Point(0, 39);
            this.bottomPreview.Name = "bottomPreview";
            this.bottomPreview.Size = new System.Drawing.Size(910, 161);
            this.bottomPreview.TabIndex = 3;
            // 
            // bottomLogControl
            // 
            this.bottomLogControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomLogControl.Location = new System.Drawing.Point(0, 39);
            this.bottomLogControl.Name = "bottomLogControl";
            this.bottomLogControl.Size = new System.Drawing.Size(910, 161);
            this.bottomLogControl.TabIndex = 2;
            // 
            // bottomPyConsole
            // 
            this.bottomPyConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomPyConsole.Location = new System.Drawing.Point(0, 39);
            this.bottomPyConsole.Name = "bottomPyConsole";
            this.bottomPyConsole.Size = new System.Drawing.Size(910, 161);
            this.bottomPyConsole.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.dragLineControl);
            this.panel4.Controls.Add(this.panel9);
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(910, 39);
            this.panel4.TabIndex = 0;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.minMaxPictureBox);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel9.Location = new System.Drawing.Point(760, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(148, 37);
            this.panel9.TabIndex = 2;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.logLabel);
            this.panel8.Controls.Add(this.pyControlLabel);
            this.panel8.Controls.Add(this.previewLabel);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(394, 37);
            this.panel8.TabIndex = 0;
            // 
            // tabPanel
            // 
            this.tabPanel.Controls.Add(this.taskBar);
            this.tabPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabPanel.Location = new System.Drawing.Point(0, 0);
            this.tabPanel.Name = "tabPanel";
            this.tabPanel.Size = new System.Drawing.Size(910, 32);
            this.tabPanel.TabIndex = 9;
            // 
            // taskBar
            // 
            this.taskBar.AeroBackground = false;
            this.taskBar.BaseLineSize = 3;
            this.taskBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskBar.Location = new System.Drawing.Point(0, 0);
            this.taskBar.Name = "taskBar";
            this.taskBar.Size = new System.Drawing.Size(910, 32);
            this.taskBar.TabIndex = 0;
            this.taskBar.Text = "taskBar1";
            // 
            // blankButton
            // 
            this.blankButton.Location = new System.Drawing.Point(616, 303);
            this.blankButton.Margin = new System.Windows.Forms.Padding(1);
            this.blankButton.Name = "blankButton";
            this.blankButton.Size = new System.Drawing.Size(0, 0);
            this.blankButton.TabIndex = 43;
            this.blankButton.Text = "button1";
            this.blankButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1233, 606);
            this.Controls.Add(this.blankButton);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.leftToolBoxPanel);
            this.Controls.Add(this.leftMainMenuPanel);
            this.Controls.Add(this.headPanel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "烽火FiberHome";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.headPanel.ResumeLayout(false);
            this.headPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.helpPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.portraitpictureBox)).EndInit();
            this.leftMainMenuPanel.ResumeLayout(false);
            this.leftToolBoxPanel.ResumeLayout(false);
            this.leftFoldPanel.ResumeLayout(false);
            this.leftFoldPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftFoldButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minMaxPictureBox)).EndInit();
            this.MainPanel.ResumeLayout(false);
            this.workSpacePanel.ResumeLayout(false);
            this.bottomViewPanel.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.tabPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel headPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label usernamelabel;
        private System.Windows.Forms.Panel leftMainMenuPanel;
        private System.Windows.Forms.Button myModelButton;
        private System.Windows.Forms.Button dataButton;
        private System.Windows.Forms.Button operateButton;
        private System.Windows.Forms.Panel leftToolBoxPanel;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button newModelButton;
        private System.Windows.Forms.Panel commonPanel;
        private System.Windows.Forms.Button moreButton;
        private System.Windows.Forms.Button formatButton;
        private System.Windows.Forms.Button groupButton;
        private System.Windows.Forms.Button interOpButton;
        private System.Windows.Forms.Button unionButton;
        private System.Windows.Forms.Button diffButton;
        private System.Windows.Forms.Button filterButton;
        private System.Windows.Forms.Button connectOpButton;
        private Controls.Left.MindMapModelControl mindMapModelControl;
        private Controls.Left.DataSourceControl dataSourceControl;
        private Controls.Left.MyModelControl myModelControl;
        private System.Windows.Forms.PictureBox helpPictureBox;
        private System.Windows.Forms.PictureBox portraitpictureBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button flowChartButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private Panel MainPanel;
        private Panel tabPanel;
        private Button ImportDataSourceButton;
        private Button button1;
        private PictureBox leftFoldButton;
        private Controls.TaskBar taskBar;
        private Controls.C1.Left.LeftFoldButton leftFoldPanel;
        private Panel workSpacePanel;
        private Panel bottomViewPanel;
        private Controls.Bottom.BottomPreviewControl bottomPreview;
        private Controls.Bottom.BottomLogControl bottomLogControl;
        private Controls.Bottom.BottomConsoleControl bottomPyConsole;
        private Panel panel4;
        private Panel panel9;
        private PictureBox minMaxPictureBox;
        private Panel panel8;
        private Label logLabel;
        private Label pyControlLabel;
        private Label previewLabel;
        private WorkSpace.MdiWorkSpace mdiWorkSpace;
        private Controls.Bottom.DragLineControl dragLineControl;
        private Button blankButton;
    }
}