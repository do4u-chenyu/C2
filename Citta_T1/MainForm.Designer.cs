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
            this.operatorControl = new C2.Controls.Left.OperatorControl();
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
            this.ImportButton = new System.Windows.Forms.Button();
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
            this.MainPanel = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.BaseWorkSpace = new System.Windows.Forms.Panel();
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
            this.MainPanel.SuspendLayout();
            this.panel7.SuspendLayout();
            this.BaseWorkSpace.SuspendLayout();
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
            this.dataButton.Location = new System.Drawing.Point(4, 54);
            this.dataButton.Name = "dataButton";
            this.dataButton.Size = new System.Drawing.Size(124, 42);
            this.dataButton.TabIndex = 2;
            this.dataButton.Text = "数据";
            this.toolTip1.SetToolTip(this.dataButton, "当前用户已导入的所有数据");
            this.dataButton.UseVisualStyleBackColor = true;
            this.dataButton.Click += new System.EventHandler(this.DataButton_Click);
            // 
            // oprateButton
            // 
            this.oprateButton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.oprateButton.Location = new System.Drawing.Point(4, 102);
            this.oprateButton.Name = "oprateButton";
            this.oprateButton.Size = new System.Drawing.Size(124, 42);
            this.oprateButton.TabIndex = 1;
            this.oprateButton.Text = "算子";
            this.toolTip1.SetToolTip(this.oprateButton, "数据分析建模所需的所有算法");
            this.oprateButton.UseVisualStyleBackColor = true;
            this.oprateButton.Click += new System.EventHandler(this.OprateButton_Click);
            // 
            // myModelButton
            // 
            this.myModelButton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myModelButton.Location = new System.Drawing.Point(4, 6);
            this.myModelButton.Name = "myModelButton";
            this.myModelButton.Size = new System.Drawing.Size(124, 42);
            this.myModelButton.TabIndex = 0;
            this.myModelButton.Text = "我的模型";
            this.toolTip1.SetToolTip(this.myModelButton, "当前用户的所有模型");
            this.myModelButton.UseVisualStyleBackColor = true;
            this.myModelButton.Click += new System.EventHandler(this.MyModelButton_Click);
            // 
            // leftToolBoxPanel
            // 
            this.leftToolBoxPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.leftToolBoxPanel.Controls.Add(this.operatorControl);
            this.leftToolBoxPanel.Controls.Add(this.flowChartControl);
            this.leftToolBoxPanel.Controls.Add(this.dataSourceControl);
            this.leftToolBoxPanel.Controls.Add(this.myModelControl);
            this.leftToolBoxPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftToolBoxPanel.Location = new System.Drawing.Point(136, 46);
            this.leftToolBoxPanel.Name = "leftToolBoxPanel";
            this.leftToolBoxPanel.Size = new System.Drawing.Size(187, 560);
            this.leftToolBoxPanel.TabIndex = 2;
            // 
            // operatorControl
            // 
            this.operatorControl.AllowDrop = true;
            this.operatorControl.BackColor = System.Drawing.Color.White;
            this.operatorControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.operatorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorControl.Location = new System.Drawing.Point(0, 0);
            this.operatorControl.Margin = new System.Windows.Forms.Padding(4);
            this.operatorControl.Name = "operatorControl";
            this.operatorControl.Size = new System.Drawing.Size(185, 558);
            this.operatorControl.TabIndex = 0;
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
            this.saveAllButton.Location = new System.Drawing.Point(1, -1);
            this.saveAllButton.Name = "saveAllButton";
            this.saveAllButton.Size = new System.Drawing.Size(33, 33);
            this.saveAllButton.TabIndex = 3;
            this.toolTip1.SetToolTip(this.saveAllButton, "保存当前打开的所有模型");
            this.saveAllButton.UseVisualStyleBackColor = false;
            this.saveAllButton.Click += new System.EventHandler(this.SaveAllButton_Click);
            // 
            // ImportButton
            // 
            this.ImportButton.BackColor = System.Drawing.Color.White;
            this.ImportButton.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ImportButton.Image = ((System.Drawing.Image)(resources.GetObject("ImportButton.Image")));
            this.ImportButton.Location = new System.Drawing.Point(88, 8);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(79, 32);
            this.ImportButton.TabIndex = 2;
            this.toolTip1.SetToolTip(this.ImportButton, "导入本地数据文件,支持bcp,cvs,txt,xls四种格式");
            this.ImportButton.UseVisualStyleBackColor = false;
            this.ImportButton.Click += new System.EventHandler(this.ImportButton_Click);
            // 
            // saveModelButton
            // 
            this.saveModelButton.BackColor = System.Drawing.Color.GhostWhite;
            this.saveModelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveModelButton.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.saveModelButton.ForeColor = System.Drawing.Color.GhostWhite;
            this.saveModelButton.Image = ((System.Drawing.Image)(resources.GetObject("saveModelButton.Image")));
            this.saveModelButton.Location = new System.Drawing.Point(34, 0);
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
            this.BaseWorkSpace.Controls.Add(this.saveModelButton);
            this.BaseWorkSpace.Controls.Add(this.saveAllButton);
            this.BaseWorkSpace.Controls.Add(this.mdiWorkSpace1);
            this.BaseWorkSpace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BaseWorkSpace.Location = new System.Drawing.Point(0, 0);
            this.BaseWorkSpace.Name = "BaseWorkSpace";
            this.BaseWorkSpace.Size = new System.Drawing.Size(910, 248);
            this.BaseWorkSpace.TabIndex = 0;
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
            this.MainPanel.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.BaseWorkSpace.ResumeLayout(false);
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
        private System.Windows.Forms.Button ImportButton;
        private Controls.Left.OperatorControl operatorControl;
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
        private Panel MainPanel;
        private Panel panel7;
        private Panel panel6;
        private Controls.Title.ModelTitlePanel modelTitlePanel;
        private Panel BaseWorkSpace;
        private C2.Controls.MdiWorkSpace mdiWorkSpace1;
    }
}