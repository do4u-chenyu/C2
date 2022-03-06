
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseSplashForm));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.IconColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.closeTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IconColumn,
            this.NameColumn,
            this.DescColumn});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.Color.White;
            this.dataGridView1.Location = new System.Drawing.Point(0, 30);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(468, 994);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BaseSplashForm_KeyPress);
            this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BaseSplashForm_MouseClick);
            this.dataGridView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BaseSplashForm_MouseMove);
            // 
            // IconColumn
            // 
            this.IconColumn.FillWeight = 20F;
            this.IconColumn.HeaderText = "";
            this.IconColumn.MinimumWidth = 8;
            this.IconColumn.Name = "IconColumn";
            this.IconColumn.ReadOnly = true;
            this.IconColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IconColumn.Width = 150;
            // 
            // NameColumn
            // 
            this.NameColumn.FillWeight = 50F;
            this.NameColumn.HeaderText = "";
            this.NameColumn.MinimumWidth = 8;
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            this.NameColumn.Width = 150;
            // 
            // DescColumn
            // 
            this.DescColumn.FillWeight = 30F;
            this.DescColumn.HeaderText = "";
            this.DescColumn.MinimumWidth = 8;
            this.DescColumn.Name = "DescColumn";
            this.DescColumn.ReadOnly = true;
            this.DescColumn.Width = 150;
            // 
            // closeTimer
            // 
            this.closeTimer.Interval = 3500;
            this.closeTimer.Tick += new System.EventHandler(this.CloseTimer_Tick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(468, 30);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(439, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(29, 30);
            this.panel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "×";
            this.label1.Click += new System.EventHandler(this.CloseLabel_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(439, 30);
            this.panel3.TabIndex = 2;
            // 
            // BaseSplashForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 1024);
            this.ControlBox = false;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BaseSplashForm";
            this.Opacity = 0.9D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BaseSplashForm";
            this.Shown += new System.EventHandler(this.BaseSplashForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }




        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewImageColumn IconColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescColumn;
        private System.Windows.Forms.Timer closeTimer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}