
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ResetButton = new System.Windows.Forms.Button();
            this.DigButton = new System.Windows.Forms.Button();
            this.DGV = new System.Windows.Forms.DataGridView();
            this.MD5Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PassColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaltColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.resultDGV = new System.Windows.Forms.DataGridView();
            this.pasteButton = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultDGV)).BeginInit();
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
            this.panel1.Size = new System.Drawing.Size(519, 120);
            this.panel1.TabIndex = 10004;
            // 
            // ResetButton
            // 
            this.ResetButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.ResetButton.Location = new System.Drawing.Point(432, 83);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 27);
            this.ResetButton.TabIndex = 10026;
            this.ResetButton.Text = "重置";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // DigButton
            // 
            this.DigButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.DigButton.Location = new System.Drawing.Point(349, 83);
            this.DigButton.Name = "DigButton";
            this.DigButton.Size = new System.Drawing.Size(75, 27);
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
            this.DGV.Size = new System.Drawing.Size(519, 84);
            this.DGV.TabIndex = 10024;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(395, 18);
            this.label1.TabIndex = 10027;
            this.label1.Text = "表中填入几组已知带盐加密样例,速查对应的加密模式";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.resultDGV);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 120);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(519, 473);
            this.panel2.TabIndex = 10005;
            // 
            // resultDGV
            // 
            this.resultDGV.AllowUserToAddRows = false;
            this.resultDGV.AllowUserToDeleteRows = false;
            this.resultDGV.AllowUserToResizeColumns = false;
            this.resultDGV.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            this.resultDGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.resultDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.resultDGV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.resultDGV.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.resultDGV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.resultDGV.ColumnHeadersHeight = 26;
            this.resultDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.resultDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.resultDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultDGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.resultDGV.Location = new System.Drawing.Point(0, 0);
            this.resultDGV.Margin = new System.Windows.Forms.Padding(4, 4, 0, 4);
            this.resultDGV.MultiSelect = false;
            this.resultDGV.Name = "resultDGV";
            this.resultDGV.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.Format = "N0";
            dataGridViewCellStyle7.NullValue = null;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.resultDGV.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.resultDGV.RowHeadersVisible = false;
            this.resultDGV.RowHeadersWidth = 10;
            this.resultDGV.RowTemplate.Height = 30;
            this.resultDGV.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resultDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.resultDGV.ShowCellErrors = false;
            this.resultDGV.ShowCellToolTips = false;
            this.resultDGV.ShowEditingIcon = false;
            this.resultDGV.ShowRowErrors = false;
            this.resultDGV.Size = new System.Drawing.Size(519, 473);
            this.resultDGV.TabIndex = 10025;
            // 
            // pasteButton
            // 
            this.pasteButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.pasteButton.Location = new System.Drawing.Point(4, 599);
            this.pasteButton.Name = "pasteButton";
            this.pasteButton.Size = new System.Drawing.Size(143, 27);
            this.pasteButton.TabIndex = 10026;
            this.pasteButton.Text = "复制模式列表";
            this.pasteButton.UseVisualStyleBackColor = true;
            this.pasteButton.Click += new System.EventHandler(this.PasteButton_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.FillWeight = 64F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Mode";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.ToolTipText = "模式";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn2.FillWeight = 12F;
            this.dataGridViewTextBoxColumn2.HeaderText = "MD5(1)";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn3.FillWeight = 12F;
            this.dataGridViewTextBoxColumn3.HeaderText = "MD5(2)";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn4.FillWeight = 12F;
            this.dataGridViewTextBoxColumn4.HeaderText = "MD5(3)";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 638);
            this.Controls.Add(this.pasteButton);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MLForm";
            this.Text = "MD5加密模式速查表";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.pasteButton, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.resultDGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView DGV;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button DigButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MD5Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn PassColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaltColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UColumn;
        private System.Windows.Forms.Button pasteButton;
        private System.Windows.Forms.DataGridView resultDGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}