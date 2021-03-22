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
            this.noFocusButton2 = new C2.Controls.Common.NoFocusButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.DetectionButton = new C2.Controls.Common.NoFocusButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.IAOLabButton = new C2.Controls.Common.NoFocusButton();
            this.IAOLabButtonPictureBox = new System.Windows.Forms.PictureBox();
            this.DataSourceButton = new C2.Controls.Common.NoFocusButton();
            this.DataButtonPictureBox = new System.Windows.Forms.PictureBox();
            this.MindMapButton = new C2.Controls.Common.NoFocusButton();
            this.MindMapButtonPictureBox = new System.Windows.Forms.PictureBox();
            this.ModelMarketButton = new C2.Controls.Common.NoFocusButton();
            this.ModelButtonPictureBox = new System.Windows.Forms.PictureBox();
            this.leftToolBoxPanel = new System.Windows.Forms.Panel();
            this.mindMapModelControl = new C2.Controls.Left.MyMindMapControl();
            this.dataSourceControl = new C2.Controls.Left.DataSourceControl();
            this.iaoModelControl = new C2.Controls.Left.IAOLabControl();
            this.myModelControl = new C2.Controls.Left.MyModelControl();
            this.webDetectionControl = new C2.Controls.C1.Left.WebDetectionControl();
            this.leftFoldButton = new C2.Controls.C1.Left.LeftFoldButton();
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.tabPanel = new System.Windows.Forms.Panel();
            this.taskBar = new C2.Controls.TaskBar();
            this.blankButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.headPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.helpPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.portraitpictureBox)).BeginInit();
            this.leftMainMenuPanel.SuspendLayout();
            this.noFocusButton2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.DetectionButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.IAOLabButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IAOLabButtonPictureBox)).BeginInit();
            this.DataSourceButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataButtonPictureBox)).BeginInit();
            this.MindMapButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MindMapButtonPictureBox)).BeginInit();
            this.ModelMarketButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ModelButtonPictureBox)).BeginInit();
            this.leftToolBoxPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minMaxPictureBox)).BeginInit();
            this.MainPanel.SuspendLayout();
            this.workSpacePanel.SuspendLayout();
            this.bottomViewPanel.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.tabPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headPanel
            // 
            this.headPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(60)))), ((int)(((byte)(85)))));
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
            this.portraitpictureBox.Image = global::C2.Properties.Resources.head;
            this.portraitpictureBox.Location = new System.Drawing.Point(222, 10);
            this.portraitpictureBox.Name = "portraitpictureBox";
            this.portraitpictureBox.Size = new System.Drawing.Size(24, 24);
            this.portraitpictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
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
            this.label1.Location = new System.Drawing.Point(8, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "IAO单兵作战平台";
            // 
            // leftMainMenuPanel
            // 
            this.leftMainMenuPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(60)))), ((int)(((byte)(85)))));
            this.leftMainMenuPanel.Controls.Add(this.noFocusButton2);
            this.leftMainMenuPanel.Controls.Add(this.DetectionButton);
            this.leftMainMenuPanel.Controls.Add(this.IAOLabButton);
            this.leftMainMenuPanel.Controls.Add(this.DataSourceButton);
            this.leftMainMenuPanel.Controls.Add(this.MindMapButton);
            this.leftMainMenuPanel.Controls.Add(this.ModelMarketButton);
            this.leftMainMenuPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftMainMenuPanel.Location = new System.Drawing.Point(0, 46);
            this.leftMainMenuPanel.Name = "leftMainMenuPanel";
            this.leftMainMenuPanel.Size = new System.Drawing.Size(145, 560);
            this.leftMainMenuPanel.TabIndex = 1;
            // 
            // noFocusButton2
            // 
            this.noFocusButton2.Controls.Add(this.pictureBox2);
            this.noFocusButton2.FlatAppearance.BorderSize = 0;
            this.noFocusButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.noFocusButton2.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.noFocusButton2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.noFocusButton2.Location = new System.Drawing.Point(0, 302);
            this.noFocusButton2.Name = "noFocusButton2";
            this.noFocusButton2.Size = new System.Drawing.Size(151, 42);
            this.noFocusButton2.TabIndex = 5;
            this.noFocusButton2.TabStop = false;
            this.noFocusButton2.Text = "   全文工具";
            this.toolTip1.SetToolTip(this.noFocusButton2, "分析师的实验台");
            this.noFocusButton2.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox2.Image = global::C2.Properties.Resources.全文检索;
            this.pictureBox2.Location = new System.Drawing.Point(12, 13);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.TabIndex = 16;
            this.pictureBox2.TabStop = false;
            // 
            // DetectionButton
            // 
            this.DetectionButton.Controls.Add(this.pictureBox1);
            this.DetectionButton.FlatAppearance.BorderSize = 0;
            this.DetectionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DetectionButton.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.DetectionButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.DetectionButton.Location = new System.Drawing.Point(0, 242);
            this.DetectionButton.Name = "DetectionButton";
            this.DetectionButton.Size = new System.Drawing.Size(151, 42);
            this.DetectionButton.TabIndex = 4;
            this.DetectionButton.TabStop = false;
            this.DetectionButton.Text = "   侦察兵";
            this.toolTip1.SetToolTip(this.DetectionButton, "分析师的实验台");
            this.DetectionButton.UseVisualStyleBackColor = true;
            this.DetectionButton.Click += new System.EventHandler(this.DetectionButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = global::C2.Properties.Resources.Artificialintelligence;
            this.pictureBox1.Location = new System.Drawing.Point(12, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // IAOLabButton
            // 
            this.IAOLabButton.Controls.Add(this.IAOLabButtonPictureBox);
            this.IAOLabButton.FlatAppearance.BorderSize = 0;
            this.IAOLabButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IAOLabButton.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.IAOLabButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.IAOLabButton.Location = new System.Drawing.Point(0, 182);
            this.IAOLabButton.Name = "IAOLabButton";
            this.IAOLabButton.Size = new System.Drawing.Size(151, 42);
            this.IAOLabButton.TabIndex = 3;
            this.IAOLabButton.TabStop = false;
            this.IAOLabButton.Text = "   IAO实验室";
            this.toolTip1.SetToolTip(this.IAOLabButton, "分析师的实验台");
            this.IAOLabButton.UseVisualStyleBackColor = true;
            this.IAOLabButton.Click += new System.EventHandler(this.IAOLabButton_Click);
            this.IAOLabButton.Leave += new System.EventHandler(this.IAOLabButton_Leave);
            this.IAOLabButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.IAOLabButton_MouseDown);
            // 
            // IAOLabButtonPictureBox
            // 
            this.IAOLabButtonPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.IAOLabButtonPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.IAOLabButtonPictureBox.Image = global::C2.Properties.Resources.Artificialintelligence;
            this.IAOLabButtonPictureBox.Location = new System.Drawing.Point(12, 13);
            this.IAOLabButtonPictureBox.Name = "IAOLabButtonPictureBox";
            this.IAOLabButtonPictureBox.Size = new System.Drawing.Size(20, 20);
            this.IAOLabButtonPictureBox.TabIndex = 16;
            this.IAOLabButtonPictureBox.TabStop = false;
            // 
            // DataSourceButton
            // 
            this.DataSourceButton.Controls.Add(this.DataButtonPictureBox);
            this.DataSourceButton.FlatAppearance.BorderSize = 0;
            this.DataSourceButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DataSourceButton.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.DataSourceButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.DataSourceButton.Location = new System.Drawing.Point(0, 122);
            this.DataSourceButton.Name = "DataSourceButton";
            this.DataSourceButton.Size = new System.Drawing.Size(151, 42);
            this.DataSourceButton.TabIndex = 2;
            this.DataSourceButton.TabStop = false;
            this.DataSourceButton.Text = "   数据管理";
            this.toolTip1.SetToolTip(this.DataSourceButton, "当前用户已导入的所有数据");
            this.DataSourceButton.UseVisualStyleBackColor = true;
            this.DataSourceButton.Click += new System.EventHandler(this.DataSourceButton_Click);
            this.DataSourceButton.Leave += new System.EventHandler(this.DataSourceButton_Leave);
            this.DataSourceButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataSourceButton_MouseDown);
            // 
            // DataButtonPictureBox
            // 
            this.DataButtonPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.DataButtonPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.DataButtonPictureBox.Image = global::C2.Properties.Resources.Datamanagement;
            this.DataButtonPictureBox.Location = new System.Drawing.Point(12, 13);
            this.DataButtonPictureBox.Name = "DataButtonPictureBox";
            this.DataButtonPictureBox.Size = new System.Drawing.Size(20, 20);
            this.DataButtonPictureBox.TabIndex = 14;
            this.DataButtonPictureBox.TabStop = false;
            // 
            // MindMapButton
            // 
            this.MindMapButton.Controls.Add(this.MindMapButtonPictureBox);
            this.MindMapButton.FlatAppearance.BorderSize = 0;
            this.MindMapButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MindMapButton.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.MindMapButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.MindMapButton.Location = new System.Drawing.Point(0, 2);
            this.MindMapButton.Name = "MindMapButton";
            this.MindMapButton.Size = new System.Drawing.Size(151, 42);
            this.MindMapButton.TabIndex = 1;
            this.MindMapButton.TabStop = false;
            this.MindMapButton.Text = "   业务视图";
            this.toolTip1.SetToolTip(this.MindMapButton, "当前用户的所有业务视图");
            this.MindMapButton.UseVisualStyleBackColor = true;
            this.MindMapButton.Click += new System.EventHandler(this.MindMapButton_Click);
            this.MindMapButton.Leave += new System.EventHandler(this.MindMapButton_Leave);
            this.MindMapButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MindMapButton_MouseDown);
            // 
            // MindMapButtonPictureBox
            // 
            this.MindMapButtonPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.MindMapButtonPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.MindMapButtonPictureBox.Image = global::C2.Properties.Resources.Businessvw;
            this.MindMapButtonPictureBox.Location = new System.Drawing.Point(12, 13);
            this.MindMapButtonPictureBox.Name = "MindMapButtonPictureBox";
            this.MindMapButtonPictureBox.Size = new System.Drawing.Size(20, 20);
            this.MindMapButtonPictureBox.TabIndex = 13;
            this.MindMapButtonPictureBox.TabStop = false;
            // 
            // ModelMarketButton
            // 
            this.ModelMarketButton.Controls.Add(this.ModelButtonPictureBox);
            this.ModelMarketButton.FlatAppearance.BorderSize = 0;
            this.ModelMarketButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ModelMarketButton.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.ModelMarketButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ModelMarketButton.Location = new System.Drawing.Point(0, 62);
            this.ModelMarketButton.Name = "ModelMarketButton";
            this.ModelMarketButton.Size = new System.Drawing.Size(151, 42);
            this.ModelMarketButton.TabIndex = 0;
            this.ModelMarketButton.TabStop = false;
            this.ModelMarketButton.Text = "   模型市场";
            this.toolTip1.SetToolTip(this.ModelMarketButton, "当前用户发布的所有模型");
            this.ModelMarketButton.UseVisualStyleBackColor = true;
            this.ModelMarketButton.Click += new System.EventHandler(this.ModelMarketButton_Click);
            this.ModelMarketButton.Leave += new System.EventHandler(this.ModelMarketButton_Leave);
            this.ModelMarketButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ModelMarketButton_MouseDown);
            // 
            // ModelButtonPictureBox
            // 
            this.ModelButtonPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.ModelButtonPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ModelButtonPictureBox.Image = global::C2.Properties.Resources.modelMarket;
            this.ModelButtonPictureBox.Location = new System.Drawing.Point(12, 13);
            this.ModelButtonPictureBox.Name = "ModelButtonPictureBox";
            this.ModelButtonPictureBox.Size = new System.Drawing.Size(20, 20);
            this.ModelButtonPictureBox.TabIndex = 15;
            this.ModelButtonPictureBox.TabStop = false;
            // 
            // leftToolBoxPanel
            // 
            this.leftToolBoxPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.leftToolBoxPanel.Controls.Add(this.mindMapModelControl);
            this.leftToolBoxPanel.Controls.Add(this.dataSourceControl);
            this.leftToolBoxPanel.Controls.Add(this.iaoModelControl);
            this.leftToolBoxPanel.Controls.Add(this.myModelControl);
            this.leftToolBoxPanel.Controls.Add(this.webDetectionControl);
            this.leftToolBoxPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftToolBoxPanel.Location = new System.Drawing.Point(145, 46);
            this.leftToolBoxPanel.Name = "leftToolBoxPanel";
            this.leftToolBoxPanel.Size = new System.Drawing.Size(187, 560);
            this.leftToolBoxPanel.TabIndex = 2;
            // 
            // mindMapModelControl
            // 
            this.mindMapModelControl.AllowDrop = true;
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
            this.dataSourceControl.Visible = false;
            // 
            // iaoModelControl
            // 
            this.iaoModelControl.AllowDrop = true;
            this.iaoModelControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.iaoModelControl.BackColor = System.Drawing.Color.White;
            this.iaoModelControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.iaoModelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iaoModelControl.Location = new System.Drawing.Point(0, 0);
            this.iaoModelControl.Margin = new System.Windows.Forms.Padding(4);
            this.iaoModelControl.Name = "iaoModelControl";
            this.iaoModelControl.Size = new System.Drawing.Size(185, 558);
            this.iaoModelControl.TabIndex = 0;
            this.iaoModelControl.Visible = false;
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
            // webDetectionControl
            // 
            this.webDetectionControl.AllowDrop = true;
            this.webDetectionControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.webDetectionControl.BackColor = System.Drawing.Color.White;
            this.webDetectionControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.webDetectionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webDetectionControl.Location = new System.Drawing.Point(0, 0);
            this.webDetectionControl.Name = "webDetectionControl";
            this.webDetectionControl.Size = new System.Drawing.Size(185, 558);
            this.webDetectionControl.TabIndex = 1;
            this.webDetectionControl.Visible = false;
            // 
            // leftFoldButton
            // 
            this.leftFoldButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.leftFoldButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.leftFoldButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.leftFoldButton.Location = new System.Drawing.Point(0, 200);
            this.leftFoldButton.Margin = new System.Windows.Forms.Padding(0);
            this.leftFoldButton.Name = "leftFoldButton";
            this.leftFoldButton.Size = new System.Drawing.Size(7, 100);
            this.leftFoldButton.TabIndex = 1;
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
            this.minMaxPictureBox.Location = new System.Drawing.Point(1, 10);
            this.minMaxPictureBox.Name = "minMaxPictureBox";
            this.minMaxPictureBox.Size = new System.Drawing.Size(25, 24);
            this.minMaxPictureBox.TabIndex = 1;
            this.minMaxPictureBox.TabStop = false;
            this.toolTip1.SetToolTip(this.minMaxPictureBox, "隐藏底层面板");
            this.minMaxPictureBox.Click += new System.EventHandler(this.MinMaxPictureBox_Click);
            // 
            // logLabel
            // 
            this.logLabel.AutoSize = true;
            this.logLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.logLabel.Location = new System.Drawing.Point(122, 5);
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
            this.pyControlLabel.Location = new System.Drawing.Point(237, 5);
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
            this.previewLabel.Location = new System.Drawing.Point(14, 5);
            this.previewLabel.Name = "previewLabel";
            this.previewLabel.Size = new System.Drawing.Size(74, 22);
            this.previewLabel.TabIndex = 0;
            this.previewLabel.Text = "数据预览";
            this.toolTip1.SetToolTip(this.previewLabel, "当前模型对应数据源的部分数据预览.");
            this.previewLabel.Click += new System.EventHandler(this.PreviewLabel_Click);
            // 
            // dragLineControl
            // 
            this.dragLineControl.BackColor = System.Drawing.SystemColors.GrayText;
            this.dragLineControl.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.dragLineControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dragLineControl.Location = new System.Drawing.Point(0, 34);
            this.dragLineControl.Margin = new System.Windows.Forms.Padding(4);
            this.dragLineControl.Name = "dragLineControl";
            this.dragLineControl.Size = new System.Drawing.Size(899, 3);
            this.dragLineControl.TabIndex = 3;
            this.toolTip1.SetToolTip(this.dragLineControl, "按住鼠标左键可以上下拖动改变预览面板的大小.");
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.workSpacePanel);
            this.MainPanel.Controls.Add(this.tabPanel);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(332, 46);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(901, 560);
            this.MainPanel.TabIndex = 7;
            // 
            // workSpacePanel
            // 
            this.workSpacePanel.Controls.Add(this.leftFoldButton);
            this.workSpacePanel.Controls.Add(this.mdiWorkSpace);
            this.workSpacePanel.Controls.Add(this.bottomViewPanel);
            this.workSpacePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workSpacePanel.Location = new System.Drawing.Point(0, 32);
            this.workSpacePanel.Name = "workSpacePanel";
            this.workSpacePanel.Size = new System.Drawing.Size(901, 528);
            this.workSpacePanel.TabIndex = 10;
            // 
            // mdiWorkSpace
            // 
            this.mdiWorkSpace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mdiWorkSpace.Location = new System.Drawing.Point(0, 0);
            this.mdiWorkSpace.Name = "mdiWorkSpace";
            this.mdiWorkSpace.Size = new System.Drawing.Size(901, 328);
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
            this.bottomViewPanel.Size = new System.Drawing.Size(901, 200);
            this.bottomViewPanel.TabIndex = 11;
            // 
            // bottomPreview
            // 
            this.bottomPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomPreview.Location = new System.Drawing.Point(0, 39);
            this.bottomPreview.Name = "bottomPreview";
            this.bottomPreview.Size = new System.Drawing.Size(901, 161);
            this.bottomPreview.TabIndex = 3;
            // 
            // bottomLogControl
            // 
            this.bottomLogControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomLogControl.Location = new System.Drawing.Point(0, 39);
            this.bottomLogControl.Name = "bottomLogControl";
            this.bottomLogControl.Size = new System.Drawing.Size(901, 161);
            this.bottomLogControl.TabIndex = 2;
            // 
            // bottomPyConsole
            // 
            this.bottomPyConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomPyConsole.Location = new System.Drawing.Point(0, 39);
            this.bottomPyConsole.Name = "bottomPyConsole";
            this.bottomPyConsole.Size = new System.Drawing.Size(901, 161);
            this.bottomPyConsole.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(this.dragLineControl);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(901, 39);
            this.panel4.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(899, 34);
            this.panel1.TabIndex = 4;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.logLabel);
            this.panel8.Controls.Add(this.pyControlLabel);
            this.panel8.Controls.Add(this.previewLabel);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(393, 34);
            this.panel8.TabIndex = 0;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.minMaxPictureBox);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel9.Location = new System.Drawing.Point(872, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(27, 34);
            this.panel9.TabIndex = 2;
            // 
            // tabPanel
            // 
            this.tabPanel.Controls.Add(this.taskBar);
            this.tabPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabPanel.Location = new System.Drawing.Point(0, 0);
            this.tabPanel.Name = "tabPanel";
            this.tabPanel.Size = new System.Drawing.Size(901, 32);
            this.tabPanel.TabIndex = 9;
            // 
            // taskBar
            // 
            this.taskBar.AeroBackground = false;
            this.taskBar.BaseLineSize = 3;
            this.taskBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskBar.Location = new System.Drawing.Point(0, 0);
            this.taskBar.Name = "taskBar";
            this.taskBar.ShowPreferencesButton = true;
            this.taskBar.Size = new System.Drawing.Size(901, 32);
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
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog2";
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
            this.Icon = global::C2.Properties.Resources.logo;
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
            this.noFocusButton2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.DetectionButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.IAOLabButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.IAOLabButtonPictureBox)).EndInit();
            this.DataSourceButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataButtonPictureBox)).EndInit();
            this.MindMapButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MindMapButtonPictureBox)).EndInit();
            this.ModelMarketButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ModelButtonPictureBox)).EndInit();
            this.leftToolBoxPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.minMaxPictureBox)).EndInit();
            this.MainPanel.ResumeLayout(false);
            this.workSpacePanel.ResumeLayout(false);
            this.bottomViewPanel.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.tabPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel headPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label usernamelabel;
        private System.Windows.Forms.Panel leftMainMenuPanel;
        private Controls.Common.NoFocusButton ModelMarketButton;
        private Controls.Common.NoFocusButton DataSourceButton;
        private Controls.Common.NoFocusButton MindMapButton;
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
        private Controls.Left.MyMindMapControl mindMapModelControl;
        private Controls.Left.DataSourceControl dataSourceControl;
        private Controls.Left.IAOLabControl iaoModelControl;
        private Controls.Left.MyModelControl myModelControl;
        private System.Windows.Forms.PictureBox helpPictureBox;
        private System.Windows.Forms.PictureBox portraitpictureBox;
        private System.Windows.Forms.Label label7;
        private Controls.Common.NoFocusButton IAOLabButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private Panel MainPanel;
        private Panel tabPanel;
        private Button ImportDataSourceButton;
        private Button button1;
        private Controls.TaskBar taskBar;
        private Controls.C1.Left.LeftFoldButton leftFoldButton;
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
        private PictureBox MindMapButtonPictureBox;
        private PictureBox IAOLabButtonPictureBox;
        private PictureBox ModelButtonPictureBox;
        private PictureBox DataButtonPictureBox;
        private OpenFileDialog openFileDialog1;
        private Panel panel1;
        private Controls.Common.NoFocusButton DetectionButton;
        private PictureBox pictureBox1;
        private Controls.Common.NoFocusButton noFocusButton2;
        private PictureBox pictureBox2;
        private Controls.C1.Left.WebDetectionControl webDetectionControl;
    }
}