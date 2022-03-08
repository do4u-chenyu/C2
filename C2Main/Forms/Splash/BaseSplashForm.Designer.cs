
using System;
using System.Windows.Forms;

namespace C2.Forms.Splash
{
    partial class BaseSplashForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseSplashForm));
            this.DGV = new System.Windows.Forms.DataGridView();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.closeTimer = new System.Windows.Forms.Timer(this.components);
            this.topPanel = new System.Windows.Forms.Panel();
            this.titlePanel = new System.Windows.Forms.Panel();
            this.closePanel = new System.Windows.Forms.Panel();
            this.closeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            this.topPanel.SuspendLayout();
            this.closePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGV
            // 
            this.DGV.AllowUserToAddRows = false;
            this.DGV.AllowUserToDeleteRows = false;
            this.DGV.AllowUserToResizeColumns = false;
            this.DGV.AllowUserToResizeRows = false;
            this.DGV.BackgroundColor = System.Drawing.Color.White;
            this.DGV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DGV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.DGV.ColumnHeadersHeight = 34;
            this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DGV.ColumnHeadersVisible = false;
            this.DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGV.DefaultCellStyle = dataGridViewCellStyle2;
            this.DGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV.GridColor = System.Drawing.Color.White;
            this.DGV.Location = new System.Drawing.Point(0, 30);
            this.DGV.MultiSelect = false;
            this.DGV.Name = "DGV";
            this.DGV.ReadOnly = true;
            this.DGV.RowHeadersVisible = false;
            this.DGV.RowHeadersWidth = 62;
            this.DGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DGV.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DGV.RowTemplate.DividerHeight = 1;
            this.DGV.RowTemplate.Height = 55;
            this.DGV.RowTemplate.ReadOnly = true;
            this.DGV.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DGV.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGV.Size = new System.Drawing.Size(350, 770);
            this.DGV.TabIndex = 0;
            this.DGV.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DGV_CellMouseDoubleClick);
            this.DGV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BaseSplashForm_KeyPress);
            this.DGV.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BaseSplashForm_MouseClick);
            this.DGV.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BaseSplashForm_MouseMove);
            // 
            // NameColumn
            // 
            this.NameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.SlateGray;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            this.NameColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.NameColumn.HeaderText = "";
            this.NameColumn.MinimumWidth = 8;
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            this.NameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // closeTimer
            // 
            this.closeTimer.Interval = 3000;
            this.closeTimer.Tick += new System.EventHandler(this.CloseTimer_Tick);
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.LightSteelBlue;
            this.topPanel.Controls.Add(this.titlePanel);
            this.topPanel.Controls.Add(this.closePanel);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(350, 30);
            this.topPanel.TabIndex = 1;
            // 
            // titlePanel
            // 
            this.titlePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(227)))), ((int)(((byte)(237)))));
            this.titlePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titlePanel.Location = new System.Drawing.Point(0, 0);
            this.titlePanel.Name = "titlePanel";
            this.titlePanel.Size = new System.Drawing.Size(321, 30);
            this.titlePanel.TabIndex = 2;
            // 
            // closePanel
            // 
            this.closePanel.Controls.Add(this.closeLabel);
            this.closePanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.closePanel.Location = new System.Drawing.Point(321, 0);
            this.closePanel.Name = "closePanel";
            this.closePanel.Size = new System.Drawing.Size(29, 30);
            this.closePanel.TabIndex = 1;
            // 
            // closeLabel
            // 
            this.closeLabel.AutoSize = true;
            this.closeLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(227)))), ((int)(((byte)(237)))));
            this.closeLabel.Location = new System.Drawing.Point(2, 6);
            this.closeLabel.Name = "closeLabel";
            this.closeLabel.Size = new System.Drawing.Size(26, 18);
            this.closeLabel.TabIndex = 0;
            this.closeLabel.Text = "×";
            this.closeLabel.Click += new System.EventHandler(this.CloseLabel_Click);
            // 
            // BaseSplashForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(350, 800);
            this.ControlBox = false;
            this.Controls.Add(this.DGV);
            this.Controls.Add(this.topPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BaseSplashForm";
            this.Opacity = 0.85D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "快捷入口";
            this.Shown += new System.EventHandler(this.BaseSplashForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            this.topPanel.ResumeLayout(false);
            this.closePanel.ResumeLayout(false);
            this.closePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGV;
        private System.Windows.Forms.Timer closeTimer;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label closeLabel;
        private System.Windows.Forms.Panel closePanel;
        private System.Windows.Forms.Panel titlePanel;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
    }
}