namespace Citta_T1.Controls.Bottom
{
    partial class BottomPythonConsoleControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.startProcessButton = new System.Windows.Forms.Button();
            this.resetProcessButton = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.clearScreenButton = new System.Windows.Forms.Button();
            this.copyContentButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.consoleControl1 = new ConsoleControl.ConsoleControl();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.consoleControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1011, 137);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1005, 28);
            this.panel1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Controls.Add(this.comboBox1);
            this.flowLayoutPanel1.Controls.Add(this.splitter2);
            this.flowLayoutPanel1.Controls.Add(this.startProcessButton);
            this.flowLayoutPanel1.Controls.Add(this.resetProcessButton);
            this.flowLayoutPanel1.Controls.Add(this.splitter1);
            this.flowLayoutPanel1.Controls.Add(this.clearScreenButton);
            this.flowLayoutPanel1.Controls.Add(this.copyContentButton);
            this.flowLayoutPanel1.Controls.Add(this.button2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1005, 28);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 25);
            this.button1.TabIndex = 1;
            this.button1.Text = "显示输出来源:";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.White;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.ItemHeight = 16;
            this.comboBox1.Items.AddRange(new object[] {
            "Cmd控制台"});
            this.comboBox1.Location = new System.Drawing.Point(114, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(215, 24);
            this.comboBox1.TabIndex = 0;
            this.toolTip1.SetToolTip(this.comboBox1, "当前配置好的Cmd控制台和Python虚拟机");
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.splitter2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter2.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitter2.Location = new System.Drawing.Point(335, 3);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(1, 25);
            this.splitter2.TabIndex = 7;
            this.splitter2.TabStop = false;
            // 
            // startProcessButton
            // 
            this.startProcessButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.startProcessButton.Location = new System.Drawing.Point(342, 3);
            this.startProcessButton.Name = "startProcessButton";
            this.startProcessButton.Size = new System.Drawing.Size(46, 25);
            this.startProcessButton.TabIndex = 2;
            this.startProcessButton.Text = "启动";
            this.toolTip1.SetToolTip(this.startProcessButton, "启动当前选择的控制台或Python虚拟机");
            this.startProcessButton.UseVisualStyleBackColor = true;
            this.startProcessButton.Click += new System.EventHandler(this.StartProcessButton_Click);
            // 
            // resetProcessButton
            // 
            this.resetProcessButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.resetProcessButton.Location = new System.Drawing.Point(394, 3);
            this.resetProcessButton.Name = "resetProcessButton";
            this.resetProcessButton.Size = new System.Drawing.Size(46, 25);
            this.resetProcessButton.TabIndex = 3;
            this.resetProcessButton.Text = "重置";
            this.toolTip1.SetToolTip(this.resetProcessButton, "重启当前选择的控制台或Python虚拟机");
            this.resetProcessButton.UseVisualStyleBackColor = true;
            this.resetProcessButton.Click += new System.EventHandler(this.ResetProcessButton_Click);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.splitter1.Location = new System.Drawing.Point(443, 0);
            this.splitter1.Margin = new System.Windows.Forms.Padding(0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1, 31);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // clearScreenButton
            // 
            this.clearScreenButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.clearScreenButton.Location = new System.Drawing.Point(447, 3);
            this.clearScreenButton.Name = "clearScreenButton";
            this.clearScreenButton.Size = new System.Drawing.Size(46, 25);
            this.clearScreenButton.TabIndex = 4;
            this.clearScreenButton.Text = "清空";
            this.toolTip1.SetToolTip(this.clearScreenButton, "清空屏幕");
            this.clearScreenButton.UseVisualStyleBackColor = true;
            this.clearScreenButton.Click += new System.EventHandler(this.ClearScreenButton_Click);
            // 
            // copyContentButton
            // 
            this.copyContentButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.copyContentButton.Location = new System.Drawing.Point(499, 3);
            this.copyContentButton.Name = "copyContentButton";
            this.copyContentButton.Size = new System.Drawing.Size(46, 25);
            this.copyContentButton.TabIndex = 5;
            this.copyContentButton.Text = "复制";
            this.toolTip1.SetToolTip(this.copyContentButton, "复制内容到剪切板");
            this.copyContentButton.UseVisualStyleBackColor = true;
            this.copyContentButton.Click += new System.EventHandler(this.CopyContentButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(551, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(362, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "此部分正在施工中,部分功能还不完善,暂时经不起测试";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // consoleControl1
            // 
            this.consoleControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.consoleControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.consoleControl1.IsInputEnabled = true;
            this.consoleControl1.Location = new System.Drawing.Point(3, 37);
            this.consoleControl1.Name = "consoleControl1";
            this.consoleControl1.SendKeyboardCommandsToProcess = false;
            this.consoleControl1.ShowDiagnostics = false;
            this.consoleControl1.Size = new System.Drawing.Size(1005, 97);
            this.consoleControl1.TabIndex = 2;
            // 
            // BottomPythonConsoleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "BottomPythonConsoleControl";
            this.Size = new System.Drawing.Size(1011, 137);
            this.Load += new System.EventHandler(this.BottomPythonConsoleControl_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button startProcessButton;
        private ConsoleControl.ConsoleControl consoleControl1;
        private System.Windows.Forms.Button resetProcessButton;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Button clearScreenButton;
        private System.Windows.Forms.Button copyContentButton;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button2;
    }
}
