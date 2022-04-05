namespace C2.Dialogs.CastleBravo
{
    partial class AddCBTask
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.fileLabel = new System.Windows.Forms.Label();
            this.taskNameTextBox = new System.Windows.Forms.TextBox();
            this.filePathTextBox = new System.Windows.Forms.TextBox();
            this.browserButton = new System.Windows.Forms.Button();
            this.md5Label = new System.Windows.Forms.Label();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.taskComboBox = new System.Windows.Forms.ComboBox();
            this.modeComboBox = new System.Windows.Forms.ComboBox();
            this.md5TextBox = new System.Windows.Forms.TextBox();
            this.pasteModeCB = new System.Windows.Forms.CheckBox();
            this.helpLabel = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.queryButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.saltLabel = new System.Windows.Forms.Label();
            this.DGV = new System.Windows.Forms.DataGridView();
            this.PassColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaltColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modeLabel = new System.Windows.Forms.Label();
            this.saltDescLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(37, 84);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 28);
            this.label1.TabIndex = 10003;
            this.label1.Text = "任务名：";
            // 
            // fileLabel
            // 
            this.fileLabel.AutoSize = true;
            this.fileLabel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fileLabel.Location = new System.Drawing.Point(16, 150);
            this.fileLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.fileLabel.Name = "fileLabel";
            this.fileLabel.Size = new System.Drawing.Size(117, 28);
            this.fileLabel.TabIndex = 10004;
            this.fileLabel.Text = "查询文件：";
            // 
            // taskNameTextBox
            // 
            this.taskNameTextBox.Location = new System.Drawing.Point(144, 85);
            this.taskNameTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.taskNameTextBox.Name = "taskNameTextBox";
            this.taskNameTextBox.Size = new System.Drawing.Size(416, 28);
            this.taskNameTextBox.TabIndex = 10005;
            // 
            // filePathTextBox
            // 
            this.filePathTextBox.Location = new System.Drawing.Point(144, 150);
            this.filePathTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.Size = new System.Drawing.Size(416, 28);
            this.filePathTextBox.TabIndex = 10006;
            // 
            // browserButton
            // 
            this.browserButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.browserButton.Location = new System.Drawing.Point(574, 147);
            this.browserButton.Margin = new System.Windows.Forms.Padding(4);
            this.browserButton.Name = "browserButton";
            this.browserButton.Size = new System.Drawing.Size(110, 36);
            this.browserButton.TabIndex = 10007;
            this.browserButton.Text = "+浏览";
            this.browserButton.UseVisualStyleBackColor = true;
            this.browserButton.Click += new System.EventHandler(this.BrowserButton_Click);
            // 
            // md5Label
            // 
            this.md5Label.AutoSize = true;
            this.md5Label.ForeColor = System.Drawing.Color.Red;
            this.md5Label.Location = new System.Drawing.Point(141, 194);
            this.md5Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.md5Label.Name = "md5Label";
            this.md5Label.Size = new System.Drawing.Size(179, 18);
            this.md5Label.TabIndex = 10008;
            this.md5Label.Text = "*格式: MD5 一行一个";
            // 
            // ofd
            // 
            this.ofd.Filter = "数据文件|*.txt;*.bcp;*.csv;*.tsv|所有文件|*.*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(16, 20);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 28);
            this.label4.TabIndex = 10009;
            this.label4.Text = "任务类型：";
            // 
            // taskComboBox
            // 
            this.taskComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.taskComboBox.FormattingEnabled = true;
            this.taskComboBox.ItemHeight = 18;
            this.taskComboBox.Items.AddRange(new object[] {
            "常规MD5分析",
            "带盐MD5分析"});
            this.taskComboBox.Location = new System.Drawing.Point(144, 20);
            this.taskComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.taskComboBox.Name = "taskComboBox";
            this.taskComboBox.Size = new System.Drawing.Size(416, 26);
            this.taskComboBox.TabIndex = 10010;
            this.taskComboBox.SelectedIndexChanged += new System.EventHandler(this.TaskComboBox_SelectedIndexChanged);
            // 
            // modeComboBox
            // 
            this.modeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modeComboBox.DropDownWidth = 105;
            this.modeComboBox.Font = new System.Drawing.Font("宋体", 9F);
            this.modeComboBox.FormattingEnabled = true;
            this.modeComboBox.ItemHeight = 18;
            this.modeComboBox.Location = new System.Drawing.Point(144, 150);
            this.modeComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.modeComboBox.Name = "modeComboBox";
            this.modeComboBox.Size = new System.Drawing.Size(416, 26);
            this.modeComboBox.TabIndex = 10011;
            // 
            // md5TextBox
            // 
            this.md5TextBox.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.md5TextBox.Location = new System.Drawing.Point(144, 224);
            this.md5TextBox.Margin = new System.Windows.Forms.Padding(4);
            this.md5TextBox.MaxLength = 4194304;
            this.md5TextBox.Multiline = true;
            this.md5TextBox.Name = "md5TextBox";
            this.md5TextBox.ReadOnly = true;
            this.md5TextBox.Size = new System.Drawing.Size(416, 174);
            this.md5TextBox.TabIndex = 10012;
            this.md5TextBox.Text = "粘贴模式可以直接Ctrl+V内容后创建任务";
            this.md5TextBox.WordWrap = false;
            // 
            // pasteModeCB
            // 
            this.pasteModeCB.AutoSize = true;
            this.pasteModeCB.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.pasteModeCB.Location = new System.Drawing.Point(9, 224);
            this.pasteModeCB.Margin = new System.Windows.Forms.Padding(4);
            this.pasteModeCB.Name = "pasteModeCB";
            this.pasteModeCB.Size = new System.Drawing.Size(110, 29);
            this.pasteModeCB.TabIndex = 10013;
            this.pasteModeCB.Text = "粘贴模式";
            this.pasteModeCB.UseVisualStyleBackColor = true;
            this.pasteModeCB.CheckedChanged += new System.EventHandler(this.PasteModeCB_CheckedChanged);
            // 
            // helpLabel
            // 
            this.helpLabel.AutoSize = true;
            this.helpLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.helpLabel.Location = new System.Drawing.Point(471, 194);
            this.helpLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.helpLabel.Name = "helpLabel";
            this.helpLabel.Size = new System.Drawing.Size(84, 18);
            this.helpLabel.TabIndex = 10014;
            this.helpLabel.TabStop = true;
            this.helpLabel.Text = "帮助说明";
            this.helpLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HelpLabel_LinkClicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(6, 267);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 18);
            this.label5.TabIndex = 10016;
            this.label5.Text = "* 最大4M文本";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(6, 294);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 18);
            this.label6.TabIndex = 10017;
            this.label6.Text = "* 约10万行MD5";
            // 
            // queryButton
            // 
            this.queryButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.queryButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.queryButton.Location = new System.Drawing.Point(574, 224);
            this.queryButton.Margin = new System.Windows.Forms.Padding(4);
            this.queryButton.Name = "queryButton";
            this.queryButton.Size = new System.Drawing.Size(110, 36);
            this.queryButton.TabIndex = 10018;
            this.queryButton.Text = "排队咨询";
            this.toolTip1.SetToolTip(this.queryButton, "快剑表-速度快但成功率低;彩虹表-速度慢但成功率高;咨询服务器彩虹表是否有额度");
            this.queryButton.UseVisualStyleBackColor = true;
            this.queryButton.Click += new System.EventHandler(this.QueryButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label7.Location = new System.Drawing.Point(566, 267);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 18);
            this.label7.TabIndex = 10019;
            this.label7.Text = "询问服务器";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label8.Location = new System.Drawing.Point(566, 294);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 18);
            this.label8.TabIndex = 10020;
            this.label8.Text = "彩虹表是否忙";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label9.Location = new System.Drawing.Point(566, 322);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 18);
            this.label9.TabIndex = 10021;
            // 
            // saltLabel
            // 
            this.saltLabel.AutoSize = true;
            this.saltLabel.ForeColor = System.Drawing.Color.Red;
            this.saltLabel.Location = new System.Drawing.Point(141, 194);
            this.saltLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.saltLabel.Name = "saltLabel";
            this.saltLabel.Size = new System.Drawing.Size(260, 18);
            this.saltLabel.TabIndex = 10022;
            this.saltLabel.Text = "*格式: 填表 MD5,Salt 最多5行";
            this.saltLabel.Visible = false;
            // 
            // DGV
            // 
            this.DGV.AllowUserToAddRows = false;
            this.DGV.AllowUserToDeleteRows = false;
            this.DGV.AllowUserToResizeColumns = false;
            this.DGV.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.DGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DGV.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.DGV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DGV.ColumnHeadersHeight = 20;
            this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PassColumn,
            this.SaltColumn,
            this.UColumn});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 8F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGV.DefaultCellStyle = dataGridViewCellStyle3;
            this.DGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DGV.Location = new System.Drawing.Point(144, 224);
            this.DGV.Margin = new System.Windows.Forms.Padding(4, 4, 0, 4);
            this.DGV.MultiSelect = false;
            this.DGV.Name = "DGV";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.NullValue = null;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DGV.RowHeadersVisible = false;
            this.DGV.RowHeadersWidth = 10;
            this.DGV.RowTemplate.Height = 30;
            this.DGV.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DGV.ShowCellErrors = false;
            this.DGV.ShowCellToolTips = false;
            this.DGV.ShowEditingIcon = false;
            this.DGV.ShowRowErrors = false;
            this.DGV.Size = new System.Drawing.Size(416, 174);
            this.DGV.TabIndex = 10023;
            this.DGV.Visible = false;
            // 
            // PassColumn
            // 
            this.PassColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PassColumn.FillWeight = 50F;
            this.PassColumn.HeaderText = "Pass";
            this.PassColumn.MinimumWidth = 8;
            this.PassColumn.Name = "PassColumn";
            this.PassColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PassColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PassColumn.ToolTipText = "待分析的MD5值";
            // 
            // SaltColumn
            // 
            this.SaltColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SaltColumn.FillWeight = 40F;
            this.SaltColumn.HeaderText = "Salt";
            this.SaltColumn.MinimumWidth = 8;
            this.SaltColumn.Name = "SaltColumn";
            this.SaltColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SaltColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SaltColumn.ToolTipText = "Pass(MD5)对应的盐值";
            // 
            // UColumn
            // 
            this.UColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UColumn.FillWeight = 10F;
            this.UColumn.HeaderText = "U";
            this.UColumn.MinimumWidth = 8;
            this.UColumn.Name = "UColumn";
            this.UColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.UColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UColumn.ToolTipText = "Pass(MD5)对应的用户名";
            // 
            // modeLabel
            // 
            this.modeLabel.AutoSize = true;
            this.modeLabel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.modeLabel.Location = new System.Drawing.Point(16, 150);
            this.modeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.modeLabel.Name = "modeLabel";
            this.modeLabel.Size = new System.Drawing.Size(117, 28);
            this.modeLabel.TabIndex = 10024;
            this.modeLabel.Text = "加密模式：";
            this.modeLabel.Visible = false;
            // 
            // saltDescLabel
            // 
            this.saltDescLabel.AutoSize = true;
            this.saltDescLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.saltDescLabel.ForeColor = System.Drawing.Color.Red;
            this.saltDescLabel.Location = new System.Drawing.Point(6, 267);
            this.saltDescLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.saltDescLabel.Name = "saltDescLabel";
            this.saltDescLabel.Size = new System.Drawing.Size(125, 18);
            this.saltDescLabel.TabIndex = 10025;
            this.saltDescLabel.Text = "* 最多一次5个";
            this.saltDescLabel.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(6, 267);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 18);
            this.label2.TabIndex = 10026;
            this.label2.Text = "* 带盐模式";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(6, 294);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 18);
            this.label3.TabIndex = 10027;
            this.label3.Text = "* Salt填盐值";
            this.label3.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(6, 322);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 18);
            this.label10.TabIndex = 10028;
            this.label10.Text = "* U(可选)";
            this.label10.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(571, 156);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 18);
            this.label11.TabIndex = 10029;
            this.label11.Text = "*(必选)";
            this.label11.Visible = false;
            // 
            // AddCBTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(705, 477);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.saltLabel);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.queryButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.helpLabel);
            this.Controls.Add(this.pasteModeCB);
            this.Controls.Add(this.taskComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.md5Label);
            this.Controls.Add(this.browserButton);
            this.Controls.Add(this.filePathTextBox);
            this.Controls.Add(this.taskNameTextBox);
            this.Controls.Add(this.fileLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.md5TextBox);
            this.Controls.Add(this.modeComboBox);
            this.Controls.Add(this.modeLabel);
            this.Controls.Add(this.saltDescLabel);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.DGV);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AddCBTask";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "喝彩城堡-新建任务";
            this.Load += new System.EventHandler(this.AddCBTask_Load);
            this.Controls.SetChildIndex(this.DGV, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.saltDescLabel, 0);
            this.Controls.SetChildIndex(this.modeLabel, 0);
            this.Controls.SetChildIndex(this.modeComboBox, 0);
            this.Controls.SetChildIndex(this.md5TextBox, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.fileLabel, 0);
            this.Controls.SetChildIndex(this.taskNameTextBox, 0);
            this.Controls.SetChildIndex(this.filePathTextBox, 0);
            this.Controls.SetChildIndex(this.browserButton, 0);
            this.Controls.SetChildIndex(this.md5Label, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.taskComboBox, 0);
            this.Controls.SetChildIndex(this.pasteModeCB, 0);
            this.Controls.SetChildIndex(this.helpLabel, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.queryButton, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.saltLabel, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label fileLabel;
        private System.Windows.Forms.TextBox taskNameTextBox;
        private System.Windows.Forms.TextBox filePathTextBox;
        private System.Windows.Forms.Button browserButton;
        private System.Windows.Forms.Label md5Label;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox taskComboBox;
        private System.Windows.Forms.ComboBox modeComboBox;
        private System.Windows.Forms.TextBox md5TextBox;
        private System.Windows.Forms.CheckBox pasteModeCB;
        private System.Windows.Forms.LinkLabel helpLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button queryButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label saltLabel;
        private System.Windows.Forms.DataGridView DGV;
        private System.Windows.Forms.Label modeLabel;
        private System.Windows.Forms.Label saltDescLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridViewTextBoxColumn PassColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaltColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UColumn;
        private System.Windows.Forms.Label label11;
    }
}