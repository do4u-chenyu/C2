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
            this.oprateButton = new System.Windows.Forms.Button();
            this.myModelButton = new System.Windows.Forms.Button();
            this.leftToolBoxPanel = new System.Windows.Forms.Panel();
            this.mindMapModelControl = new C2.Controls.Left.MindMapModelControl();
            this.flowChartControl = new C2.Controls.Left.FlowChartControl();
            this.dataSourceControl = new C2.Controls.Left.DataSourceControl();
            this.myModelControl = new C2.Controls.Left.MyModelControl();
            this.bottomViewPanel = new System.Windows.Forms.Panel();
            this.bottomPreview = new C2.Controls.Bottom.BottomPreviewControl();
            this.bottomLogControl = new C2.Controls.Bottom.BottomLogControl();
            this.bottomPyConsole = new C2.Controls.Bottom.BottomConsoleControl();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dragLineControl = new C2.Controls.Title.DragLineControl();
            this.panel9 = new System.Windows.Forms.Panel();
            this.minMaxPictureBox = new System.Windows.Forms.PictureBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.logLabel = new System.Windows.Forms.Label();
            this.pyControlLabel = new System.Windows.Forms.Label();
            this.previewLabel = new System.Windows.Forms.Label();
            this.commonPanel = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.saveAllButton = new System.Windows.Forms.Button();
            this.saveModelButton = new System.Windows.Forms.Button();
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
            this.blankButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.leftFoldButton = new System.Windows.Forms.PictureBox();
            this.resetButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.runButton = new System.Windows.Forms.Button();
            this.ImportDataSourceButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.BaseWorkSpace = new System.Windows.Forms.Panel();
            this.remarkControl = new C2.Controls.Flow.RemarkControl();
            this.canvasPanel = new C2.Controls.CanvasPanel();
            this.currentModelFinLab = new System.Windows.Forms.Label();
            this.operatorControl = new C2.Controls.Left.OperatorControl();
            this.progressBarLabel = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.currentModelRunBackLab = new System.Windows.Forms.Label();
            this.currentModelRunLab = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.flowControl = new C2.Controls.Flow.FlowControl();
            this.rightHideButton = new C2.Controls.Flow.RightHideButton();
            this.rightShowButton = new C2.Controls.Flow.RightShowButton();
            this.naviViewControl = new C2.Controls.Flow.NaviViewControl();
            this.topToolBarControl = new C2.Controls.Top.TopToolBarControl();
            this.mdiWorkSpace1 = new C2.Controls.MdiWorkSpace();
            this.panel6 = new System.Windows.Forms.Panel();
            this.modelTitlePanel = new C2.Controls.Title.ModelTitlePanel();
            this.headPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.helpPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.portraitpictureBox)).BeginInit();
            this.leftMainMenuPanel.SuspendLayout();
            this.leftToolBoxPanel.SuspendLayout();
            this.bottomViewPanel.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minMaxPictureBox)).BeginInit();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftFoldButton)).BeginInit();
            this.MainPanel.SuspendLayout();
            this.panel7.SuspendLayout();
            this.BaseWorkSpace.SuspendLayout();
            this.canvasPanel.SuspendLayout();
            this.currentModelRunBackLab.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel6.SuspendLayout();
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
            this.usernamelabel.Size = new System.Drawing.Size(58, 22);
            this.usernamelabel.TabIndex = 3;
            this.usernamelabel.Text = "李警官";
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
            this.leftMainMenuPanel.Controls.Add(this.oprateButton);
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
            // oprateButton
            // 
            this.oprateButton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.oprateButton.Location = new System.Drawing.Point(4, 6);
            this.oprateButton.Name = "oprateButton";
            this.oprateButton.Size = new System.Drawing.Size(124, 42);
            this.oprateButton.TabIndex = 1;
            this.oprateButton.Text = "业务视图";
            this.toolTip1.SetToolTip(this.oprateButton, "当前用户的所有业务视图");
            this.oprateButton.UseVisualStyleBackColor = true;
            this.oprateButton.Click += new System.EventHandler(this.OprateButton_Click);
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
            this.leftToolBoxPanel.Controls.Add(this.flowChartControl);
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
            // flowChartControl
            // 
            this.flowChartControl.AllowDrop = true;
            this.flowChartControl.BackColor = System.Drawing.Color.White;
            this.flowChartControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.flowChartControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowChartControl.Location = new System.Drawing.Point(0, 0);
            this.flowChartControl.Margin = new System.Windows.Forms.Padding(4);
            this.flowChartControl.Name = "flowChartControl";
            this.flowChartControl.Size = new System.Drawing.Size(185, 558);
            this.flowChartControl.TabIndex = 0;
            this.flowChartControl.Visible = false;
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
            // bottomViewPanel
            // 
            this.bottomViewPanel.Controls.Add(this.bottomPreview);
            this.bottomViewPanel.Controls.Add(this.bottomLogControl);
            this.bottomViewPanel.Controls.Add(this.bottomPyConsole);
            this.bottomViewPanel.Controls.Add(this.panel4);
            this.bottomViewPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomViewPanel.Location = new System.Drawing.Point(323, 326);
            this.bottomViewPanel.Name = "bottomViewPanel";
            this.bottomViewPanel.Size = new System.Drawing.Size(910, 280);
            this.bottomViewPanel.TabIndex = 3;
            // 
            // bottomPreview
            // 
            this.bottomPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomPreview.Location = new System.Drawing.Point(0, 39);
            this.bottomPreview.Margin = new System.Windows.Forms.Padding(4);
            this.bottomPreview.Name = "bottomPreview";
            this.bottomPreview.Size = new System.Drawing.Size(910, 241);
            this.bottomPreview.TabIndex = 27;
            // 
            // bottomLogControl
            // 
            this.bottomLogControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomLogControl.Location = new System.Drawing.Point(0, 39);
            this.bottomLogControl.Margin = new System.Windows.Forms.Padding(4);
            this.bottomLogControl.Name = "bottomLogControl";
            this.bottomLogControl.Size = new System.Drawing.Size(910, 241);
            this.bottomLogControl.TabIndex = 1;
            // 
            // bottomPyConsole
            // 
            this.bottomPyConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomPyConsole.Location = new System.Drawing.Point(0, 39);
            this.bottomPyConsole.Margin = new System.Windows.Forms.Padding(4);
            this.bottomPyConsole.Name = "bottomPyConsole";
            this.bottomPyConsole.Size = new System.Drawing.Size(910, 241);
            this.bottomPyConsole.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.panel4.Controls.Add(this.dragLineControl);
            this.panel4.Controls.Add(this.panel9);
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(910, 39);
            this.panel4.TabIndex = 0;
            // 
            // dragLineControl
            // 
            this.dragLineControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.dragLineControl.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.dragLineControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dragLineControl.Location = new System.Drawing.Point(394, 36);
            this.dragLineControl.Margin = new System.Windows.Forms.Padding(4);
            this.dragLineControl.Name = "dragLineControl";
            this.dragLineControl.Size = new System.Drawing.Size(368, 3);
            this.dragLineControl.TabIndex = 3;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.minMaxPictureBox);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel9.Location = new System.Drawing.Point(762, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(148, 39);
            this.panel9.TabIndex = 2;
            // 
            // minMaxPictureBox
            // 
            this.minMaxPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("minMaxPictureBox.Image")));
            this.minMaxPictureBox.Location = new System.Drawing.Point(115, 12);
            this.minMaxPictureBox.Name = "minMaxPictureBox";
            this.minMaxPictureBox.Size = new System.Drawing.Size(25, 24);
            this.minMaxPictureBox.TabIndex = 1;
            this.minMaxPictureBox.TabStop = false;
            this.toolTip1.SetToolTip(this.minMaxPictureBox, "隐藏底层面板");
            this.minMaxPictureBox.Click += new System.EventHandler(this.MinMaxPictureBox_Click);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.logLabel);
            this.panel8.Controls.Add(this.pyControlLabel);
            this.panel8.Controls.Add(this.previewLabel);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(394, 39);
            this.panel8.TabIndex = 0;
            // 
            // logLabel
            // 
            this.logLabel.AutoSize = true;
            this.logLabel.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.logLabel.Location = new System.Drawing.Point(120, 4);
            this.logLabel.Name = "logLabel";
            this.logLabel.Size = new System.Drawing.Size(92, 27);
            this.logLabel.TabIndex = 3;
            this.logLabel.Text = "运行日志";
            this.toolTip1.SetToolTip(this.logLabel, "当前模型运行情况的日志信息.");
            this.logLabel.Click += new System.EventHandler(this.LogLabel_Click);
            // 
            // pyControlLabel
            // 
            this.pyControlLabel.AutoSize = true;
            this.pyControlLabel.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.pyControlLabel.Location = new System.Drawing.Point(226, 4);
            this.pyControlLabel.Name = "pyControlLabel";
            this.pyControlLabel.Size = new System.Drawing.Size(72, 27);
            this.pyControlLabel.TabIndex = 2;
            this.pyControlLabel.Text = "控制台";
            this.toolTip1.SetToolTip(this.pyControlLabel, "Cmd控制台,用来调试第三方脚本.");
            this.pyControlLabel.Click += new System.EventHandler(this.PyControlLabel_Click);
            // 
            // previewLabel
            // 
            this.previewLabel.AutoSize = true;
            this.previewLabel.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.previewLabel.Location = new System.Drawing.Point(14, 4);
            this.previewLabel.Name = "previewLabel";
            this.previewLabel.Size = new System.Drawing.Size(92, 27);
            this.previewLabel.TabIndex = 0;
            this.previewLabel.Text = "数据预览";
            this.toolTip1.SetToolTip(this.previewLabel, "当前模型对应数据源的部分数据预览.");
            this.previewLabel.Click += new System.EventHandler(this.PreviewLabel_Click);
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
            // saveAllButton
            // 
            this.saveAllButton.BackColor = System.Drawing.Color.GhostWhite;
            this.saveAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveAllButton.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.saveAllButton.ForeColor = System.Drawing.Color.GhostWhite;
            this.saveAllButton.Image = ((System.Drawing.Image)(resources.GetObject("saveAllButton.Image")));
            this.saveAllButton.Location = new System.Drawing.Point(6, 0);
            this.saveAllButton.Name = "saveAllButton";
            this.saveAllButton.Size = new System.Drawing.Size(30, 30);
            this.saveAllButton.TabIndex = 3;
            this.toolTip1.SetToolTip(this.saveAllButton, "保存当前打开的所有模型");
            this.saveAllButton.UseVisualStyleBackColor = false;
            this.saveAllButton.Click += new System.EventHandler(this.SaveAllButton_Click);
            // 
            // saveModelButton
            // 
            this.saveModelButton.BackColor = System.Drawing.Color.GhostWhite;
            this.saveModelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveModelButton.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.saveModelButton.ForeColor = System.Drawing.Color.GhostWhite;
            this.saveModelButton.Image = ((System.Drawing.Image)(resources.GetObject("saveModelButton.Image")));
            this.saveModelButton.Location = new System.Drawing.Point(45, 0);
            this.saveModelButton.Name = "saveModelButton";
            this.saveModelButton.Size = new System.Drawing.Size(32, 32);
            this.saveModelButton.TabIndex = 1;
            this.toolTip1.SetToolTip(this.saveModelButton, "保存模型");
            this.saveModelButton.UseVisualStyleBackColor = false;
            this.saveModelButton.Click += new System.EventHandler(this.SaveModelButton_Click);
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
            // blankButton
            // 
            this.blankButton.Location = new System.Drawing.Point(461, 40);
            this.blankButton.Margin = new System.Windows.Forms.Padding(2);
            this.blankButton.Name = "blankButton";
            this.blankButton.Size = new System.Drawing.Size(0, 0);
            this.blankButton.TabIndex = 6;
            this.blankButton.Text = "button1";
            this.blankButton.UseVisualStyleBackColor = true;
            // 
            // leftFoldButton
            // 
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
            // resetButton
            // 
            this.resetButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.resetButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.resetButton.FlatAppearance.BorderSize = 0;
            this.resetButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.resetButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.resetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.resetButton.Image = global::C2.Properties.Resources.reset;
            this.resetButton.Location = new System.Drawing.Point(507, 354);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(52, 53);
            this.resetButton.TabIndex = 21;
            this.toolTip1.SetToolTip(this.resetButton, "清空模型运算结果,让模型可以重新运算");
            this.resetButton.UseVisualStyleBackColor = true;
            // 
            // stopButton
            // 
            this.stopButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.stopButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.stopButton.FlatAppearance.BorderSize = 0;
            this.stopButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.stopButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.stopButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.stopButton.Image = ((System.Drawing.Image)(resources.GetObject("stopButton.Image")));
            this.stopButton.Location = new System.Drawing.Point(450, 354);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(52, 53);
            this.stopButton.TabIndex = 20;
            this.toolTip1.SetToolTip(this.stopButton, "停止调试当前模型");
            this.stopButton.UseVisualStyleBackColor = true;
            // 
            // runButton
            // 
            this.runButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.runButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.runButton.FlatAppearance.BorderSize = 0;
            this.runButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.runButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.runButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.runButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.runButton.Image = ((System.Drawing.Image)(resources.GetObject("runButton.Image")));
            this.runButton.Location = new System.Drawing.Point(398, 354);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(52, 53);
            this.runButton.TabIndex = 20;
            this.toolTip1.SetToolTip(this.runButton, "开始调试当前模型");
            this.runButton.UseVisualStyleBackColor = true;
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
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.panel7);
            this.MainPanel.Controls.Add(this.panel6);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(323, 46);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(910, 280);
            this.MainPanel.TabIndex = 7;
            this.MainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.BaseWorkSpace);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 32);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(910, 248);
            this.panel7.TabIndex = 10;
            // 
            // BaseWorkSpace
            // 
            this.BaseWorkSpace.Controls.Add(this.button1);
            this.BaseWorkSpace.Controls.Add(this.ImportDataSourceButton);
            this.BaseWorkSpace.Controls.Add(this.saveModelButton);
            this.BaseWorkSpace.Controls.Add(this.saveAllButton);
            this.BaseWorkSpace.Controls.Add(this.remarkControl);
            this.BaseWorkSpace.Controls.Add(this.topToolBarControl);
            this.BaseWorkSpace.Controls.Add(this.mdiWorkSpace1);
            this.BaseWorkSpace.Controls.Add(this.canvasPanel);
            this.BaseWorkSpace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BaseWorkSpace.Location = new System.Drawing.Point(0, 0);
            this.BaseWorkSpace.Name = "BaseWorkSpace";
            this.BaseWorkSpace.Size = new System.Drawing.Size(910, 248);
            this.BaseWorkSpace.TabIndex = 0;
            // 
            // remarkControl
            // 
            this.remarkControl.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.remarkControl.BackColor = System.Drawing.Color.Transparent;
            this.remarkControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.remarkControl.Location = new System.Drawing.Point(556, 50);
            this.remarkControl.Margin = new System.Windows.Forms.Padding(4);
            this.remarkControl.Name = "remarkControl";
            this.remarkControl.RemarkDescription = "";
            this.remarkControl.Size = new System.Drawing.Size(120, 120);
            this.remarkControl.TabIndex = 26;
            this.remarkControl.Visible = false;
            // 
            // canvasPanel
            // 
            this.canvasPanel.AllowDrop = true;
            this.canvasPanel.BackColor = System.Drawing.Color.White;
            this.canvasPanel.Controls.Add(this.currentModelFinLab);
            this.canvasPanel.Controls.Add(this.operatorControl);
            this.canvasPanel.Controls.Add(this.progressBarLabel);
            this.canvasPanel.Controls.Add(this.progressBar1);
            this.canvasPanel.Controls.Add(this.currentModelRunBackLab);
            this.canvasPanel.Controls.Add(this.panel11);
            this.canvasPanel.Controls.Add(this.flowControl);
            this.canvasPanel.Controls.Add(this.rightHideButton);
            this.canvasPanel.Controls.Add(this.rightShowButton);
            this.canvasPanel.Controls.Add(this.resetButton);
            this.canvasPanel.Controls.Add(this.stopButton);
            this.canvasPanel.Controls.Add(this.runButton);
            this.canvasPanel.Controls.Add(this.naviViewControl);
            this.canvasPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.canvasPanel.DelEnable = false;
            this.canvasPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvasPanel.EndC = null;
            this.canvasPanel.EndP = ((System.Drawing.PointF)(resources.GetObject("canvasPanel.EndP")));
            this.canvasPanel.LeftButtonDown = false;
            this.canvasPanel.Location = new System.Drawing.Point(0, 0);
            this.canvasPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.canvasPanel.Name = "canvasPanel";
            this.canvasPanel.Size = new System.Drawing.Size(910, 248);
            this.canvasPanel.StartC = null;
            this.canvasPanel.StartP = ((System.Drawing.PointF)(resources.GetObject("canvasPanel.StartP")));
            this.canvasPanel.TabIndex = 7;
            // 
            // currentModelFinLab
            // 
            this.currentModelFinLab.Image = global::C2.Properties.Resources.currentModelFin;
            this.currentModelFinLab.Location = new System.Drawing.Point(498, 174);
            this.currentModelFinLab.Name = "currentModelFinLab";
            this.currentModelFinLab.Size = new System.Drawing.Size(150, 100);
            this.currentModelFinLab.TabIndex = 30;
            this.currentModelFinLab.Visible = false;
            // 
            // operatorControl
            // 
            this.operatorControl.AllowDrop = true;
            this.operatorControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.operatorControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.operatorControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.operatorControl.Location = new System.Drawing.Point(687, 106);
            this.operatorControl.Margin = new System.Windows.Forms.Padding(4);
            this.operatorControl.Name = "operatorControl";
            this.operatorControl.Size = new System.Drawing.Size(210, 310);
            this.operatorControl.TabIndex = 0;
            // 
            // progressBarLabel
            // 
            this.progressBarLabel.AutoSize = true;
            this.progressBarLabel.BackColor = System.Drawing.Color.Transparent;
            this.progressBarLabel.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.progressBarLabel.ForeColor = System.Drawing.Color.Black;
            this.progressBarLabel.Location = new System.Drawing.Point(953, 245);
            this.progressBarLabel.Name = "progressBarLabel";
            this.progressBarLabel.Size = new System.Drawing.Size(24, 16);
            this.progressBarLabel.TabIndex = 32;
            this.progressBarLabel.Text = "0%";
            this.progressBarLabel.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.progressBar1.ForeColor = System.Drawing.Color.Transparent;
            this.progressBar1.Location = new System.Drawing.Point(823, 245);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(125, 10);
            this.progressBar1.Step = 30;
            this.progressBar1.TabIndex = 31;
            this.progressBar1.Visible = false;
            // 
            // currentModelRunBackLab
            // 
            this.currentModelRunBackLab.Controls.Add(this.currentModelRunLab);
            this.currentModelRunBackLab.Image = global::C2.Properties.Resources.currentModelRunningBack;
            this.currentModelRunBackLab.Location = new System.Drawing.Point(498, 174);
            this.currentModelRunBackLab.Name = "currentModelRunBackLab";
            this.currentModelRunBackLab.Size = new System.Drawing.Size(150, 100);
            this.currentModelRunBackLab.TabIndex = 29;
            this.currentModelRunBackLab.Visible = false;
            // 
            // currentModelRunLab
            // 
            this.currentModelRunLab.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.currentModelRunLab.Image = global::C2.Properties.Resources.currentModelRunning;
            this.currentModelRunLab.Location = new System.Drawing.Point(40, 20);
            this.currentModelRunLab.Name = "currentModelRunLab";
            this.currentModelRunLab.Size = new System.Drawing.Size(73, 47);
            this.currentModelRunLab.TabIndex = 28;
            this.currentModelRunLab.Visible = false;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.leftFoldButton);
            this.panel11.Location = new System.Drawing.Point(0, 200);
            this.panel11.Margin = new System.Windows.Forms.Padding(0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(8, 101);
            this.panel11.TabIndex = 1;
            // 
            // flowControl
            // 
            this.flowControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.flowControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.flowControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.flowControl.Location = new System.Drawing.Point(687, 50);
            this.flowControl.Margin = new System.Windows.Forms.Padding(4);
            this.flowControl.Name = "flowControl";
            this.flowControl.SelectDrag = false;
            this.flowControl.SelectFrame = false;
            this.flowControl.SelectRemark = false;
            this.flowControl.Size = new System.Drawing.Size(210, 51);
            this.flowControl.TabIndex = 25;
            this.flowControl.Load += new System.EventHandler(this.flowControl_Load);
            // 
            // rightHideButton
            // 
            this.rightHideButton.BackColor = System.Drawing.Color.Transparent;
            this.rightHideButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rightHideButton.BackgroundImage")));
            this.rightHideButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.rightHideButton.Location = new System.Drawing.Point(909, 111);
            this.rightHideButton.Margin = new System.Windows.Forms.Padding(4);
            this.rightHideButton.Name = "rightHideButton";
            this.rightHideButton.Size = new System.Drawing.Size(55, 55);
            this.rightHideButton.TabIndex = 23;
            // 
            // rightShowButton
            // 
            this.rightShowButton.BackColor = System.Drawing.Color.Transparent;
            this.rightShowButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rightShowButton.BackgroundImage")));
            this.rightShowButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.rightShowButton.Location = new System.Drawing.Point(909, 50);
            this.rightShowButton.Margin = new System.Windows.Forms.Padding(4);
            this.rightShowButton.Name = "rightShowButton";
            this.rightShowButton.Size = new System.Drawing.Size(55, 55);
            this.rightShowButton.TabIndex = 22;
            // 
            // naviViewControl
            // 
            this.naviViewControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.naviViewControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.naviViewControl.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.naviViewControl.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.naviViewControl.Location = new System.Drawing.Point(752, 190);
            this.naviViewControl.Margin = new System.Windows.Forms.Padding(4);
            this.naviViewControl.Name = "naviViewControl";
            this.naviViewControl.Size = new System.Drawing.Size(205, 105);
            this.naviViewControl.TabIndex = 0;
            // 
            // topToolBarControl
            // 
            this.topToolBarControl.BackColor = System.Drawing.Color.GhostWhite;
            this.topToolBarControl.Location = new System.Drawing.Point(0, 2);
            this.topToolBarControl.Name = "topToolBarControl";
            this.topToolBarControl.Size = new System.Drawing.Size(1279, 32);
            this.topToolBarControl.TabIndex = 24;
            // 
            // mdiWorkSpace1
            // 
            this.mdiWorkSpace1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mdiWorkSpace1.Location = new System.Drawing.Point(0, 0);
            this.mdiWorkSpace1.Name = "mdiWorkSpace1";
            this.mdiWorkSpace1.Size = new System.Drawing.Size(910, 248);
            this.mdiWorkSpace1.TabIndex = 8;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.modelTitlePanel);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(910, 32);
            this.panel6.TabIndex = 9;
            // 
            // modelTitlePanel
            // 
            this.modelTitlePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modelTitlePanel.Location = new System.Drawing.Point(0, 0);
            this.modelTitlePanel.Margin = new System.Windows.Forms.Padding(4);
            this.modelTitlePanel.Name = "modelTitlePanel";
            this.modelTitlePanel.Size = new System.Drawing.Size(910, 32);
            this.modelTitlePanel.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1233, 606);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.bottomViewPanel);
            this.Controls.Add(this.leftToolBoxPanel);
            this.Controls.Add(this.leftMainMenuPanel);
            this.Controls.Add(this.headPanel);
            this.Controls.Add(this.blankButton);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "烽火FiberHome";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.headPanel.ResumeLayout(false);
            this.headPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.helpPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.portraitpictureBox)).EndInit();
            this.leftMainMenuPanel.ResumeLayout(false);
            this.leftToolBoxPanel.ResumeLayout(false);
            this.bottomViewPanel.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.minMaxPictureBox)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftFoldButton)).EndInit();
            this.MainPanel.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.BaseWorkSpace.ResumeLayout(false);
            this.canvasPanel.ResumeLayout(false);
            this.canvasPanel.PerformLayout();
            this.currentModelRunBackLab.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel6.ResumeLayout(false);
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
        private System.Windows.Forms.Button oprateButton;
        private System.Windows.Forms.Panel leftToolBoxPanel;
        private System.Windows.Forms.Panel bottomViewPanel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button saveModelButton;
        private System.Windows.Forms.Button newModelButton;
        private System.Windows.Forms.Panel commonPanel;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label previewLabel;
        private System.Windows.Forms.Label pyControlLabel;
        private System.Windows.Forms.Label logLabel;
        private System.Windows.Forms.PictureBox minMaxPictureBox;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button moreButton;
        private System.Windows.Forms.Button formatButton;
        private System.Windows.Forms.Button groupButton;
        private System.Windows.Forms.Button interOpButton;
        private System.Windows.Forms.Button unionButton;
        private System.Windows.Forms.Button diffButton;
        private System.Windows.Forms.Button filterButton;
        private System.Windows.Forms.Button connectOpButton;
        private Controls.Left.MindMapModelControl mindMapModelControl;
        private Controls.Left.FlowChartControl flowChartControl;
        private Controls.Left.DataSourceControl dataSourceControl;
        private Controls.Left.MyModelControl myModelControl;
        private System.Windows.Forms.PictureBox helpPictureBox;
        private System.Windows.Forms.PictureBox portraitpictureBox;
        private C2.Controls.Bottom.BottomLogControl bottomLogControl;
        private C2.Controls.Bottom.BottomConsoleControl bottomPyConsole;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button flowChartButton;
        private Controls.Title.DragLineControl dragLineControl;
        private C2.Controls.Bottom.BottomPreviewControl bottomPreview;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button blankButton;
        private Button saveAllButton;
        private Controls.Top.TopToolBarControl topToolBarControl;
        private Panel MainPanel;
        private Panel panel7;
        private Panel panel6;
        private Controls.Title.ModelTitlePanel modelTitlePanel;
        private Panel BaseWorkSpace;
        private Controls.CanvasPanel canvasPanel;
        private Label progressBarLabel;
        private ProgressBar progressBar1;
        private Label currentModelFinLab;
        private Label currentModelRunBackLab;
        private Label currentModelRunLab;
        private Panel panel11;
        private PictureBox leftFoldButton;
        private Controls.Flow.RemarkControl remarkControl;
        private Controls.Flow.FlowControl flowControl;
        private Controls.Flow.RightHideButton rightHideButton;
        private Controls.Flow.RightShowButton rightShowButton;
        private Button resetButton;
        private Button stopButton;
        private Button runButton;
        private Controls.Flow.NaviViewControl naviViewControl;
        private C2.Controls.MdiWorkSpace mdiWorkSpace1;
        private Button ImportDataSourceButton;
        private Button button1;
        private Controls.Left.OperatorControl operatorControl;
    }
}