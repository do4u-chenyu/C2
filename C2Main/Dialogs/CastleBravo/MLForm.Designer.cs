
namespace C2.Dialogs.CastleBravo
{
    partial class MLForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ResetButton = new System.Windows.Forms.Button();
            this.DigButton = new System.Windows.Forms.Button();
            this.DGV = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.MD5Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PassColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaltColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.ResetButton);
            this.panel1.Controls.Add(this.DigButton);
            this.panel1.Controls.Add(this.DGV);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(519, 140);
            this.panel1.TabIndex = 10004;
            // 
            // ResetButton
            // 
            this.ResetButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.ResetButton.Location = new System.Drawing.Point(432, 104);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 23);
            this.ResetButton.TabIndex = 10026;
            this.ResetButton.Text = "重置";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // DigButton
            // 
            this.DigButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.DigButton.Location = new System.Drawing.Point(349, 104);
            this.DigButton.Name = "DigButton";
            this.DigButton.Size = new System.Drawing.Size(75, 23);
            this.DigButton.TabIndex = 10025;
            this.DigButton.Text = "速查";
            this.DigButton.UseVisualStyleBackColor = true;
            this.DigButton.Click += new System.EventHandler(this.DigButton_Click);
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
            this.DGV.ColumnHeadersHeight = 26;
            this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MD5Column,
            this.PassColumn,
            this.SaltColumn,
            this.UColumn});
            this.DGV.Dock = System.Windows.Forms.DockStyle.Top;
            this.DGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DGV.Location = new System.Drawing.Point(0, 0);
            this.DGV.Margin = new System.Windows.Forms.Padding(4, 4, 0, 4);
            this.DGV.MultiSelect = false;
            this.DGV.Name = "DGV";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DGV.RowHeadersVisible = false;
            this.DGV.RowHeadersWidth = 10;
            this.DGV.RowTemplate.Height = 30;
            this.DGV.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DGV.ShowCellErrors = false;
            this.DGV.ShowCellToolTips = false;
            this.DGV.ShowEditingIcon = false;
            this.DGV.ShowRowErrors = false;
            this.DGV.Size = new System.Drawing.Size(519, 91);
            this.DGV.TabIndex = 10024;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(359, 18);
            this.label1.TabIndex = 10027;
            this.label1.Text = "表中填入几组已知样例,速查可能的加密模式";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 140);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(519, 424);
            this.panel2.TabIndex = 10005;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(519, 424);
            this.textBox1.TabIndex = 10004;
            // 
            // MD5Column
            // 
            this.MD5Column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MD5Column.FillWeight = 45F;
            this.MD5Column.HeaderText = "MD5";
            this.MD5Column.MinimumWidth = 8;
            this.MD5Column.Name = "MD5Column";
            this.MD5Column.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.MD5Column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MD5Column.ToolTipText = "待分析的MD5值";
            // 
            // PassColumn
            // 
            this.PassColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PassColumn.FillWeight = 15F;
            this.PassColumn.HeaderText = "密码($Pass)";
            this.PassColumn.MinimumWidth = 8;
            this.PassColumn.Name = "PassColumn";
            this.PassColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PassColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PassColumn.ToolTipText = "明文密码";
            // 
            // SaltColumn
            // 
            this.SaltColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SaltColumn.FillWeight = 25F;
            this.SaltColumn.HeaderText = "盐($Salt)";
            this.SaltColumn.MinimumWidth = 8;
            this.SaltColumn.Name = "SaltColumn";
            this.SaltColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SaltColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UColumn
            // 
            this.UColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UColumn.FillWeight = 15F;
            this.UColumn.HeaderText = "用户名($U)";
            this.UColumn.MinimumWidth = 8;
            this.UColumn.Name = "UColumn";
            this.UColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.UColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 609);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MLForm";
            this.Text = "MD5加密模式速查表";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView DGV;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button DigButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MD5Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn PassColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaltColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UColumn;
    }
}