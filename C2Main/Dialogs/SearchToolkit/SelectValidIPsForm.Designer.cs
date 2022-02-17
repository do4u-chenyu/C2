namespace C2.Dialogs.SearchToolkit
{
    partial class SelectValidIPsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectValidIPsForm));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dictDataGridView = new System.Windows.Forms.DataGridView();
            this.IPSelectStatus = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IPID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IPName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IPPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noSelectBtn = new System.Windows.Forms.Button();
            this.allSelectBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dictDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.dictDataGridView);
            this.groupBox2.Location = new System.Drawing.Point(0, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(346, 266);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "IP（选择";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "2个）";
            // 
            // dictDataGridView
            // 
            this.dictDataGridView.AllowUserToAddRows = false;
            this.dictDataGridView.AllowUserToDeleteRows = false;
            this.dictDataGridView.AllowUserToResizeColumns = false;
            this.dictDataGridView.AllowUserToResizeRows = false;
            this.dictDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dictDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dictDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IPSelectStatus,
            this.IPID,
            this.IPName,
            this.IPPort});
            this.dictDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dictDataGridView.GridColor = System.Drawing.SystemColors.Control;
            this.dictDataGridView.Location = new System.Drawing.Point(3, 17);
            this.dictDataGridView.Name = "dictDataGridView";
            this.dictDataGridView.RowHeadersVisible = false;
            this.dictDataGridView.RowTemplate.Height = 23;
            this.dictDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dictDataGridView.Size = new System.Drawing.Size(340, 246);
            this.dictDataGridView.TabIndex = 1;
            this.dictDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DictDataGridView_CellContentClick);
            // 
            // IPSelectStatus
            // 
            this.IPSelectStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IPSelectStatus.FillWeight = 25F;
            this.IPSelectStatus.HeaderText = "是否选择";
            this.IPSelectStatus.Name = "IPSelectStatus";
            this.IPSelectStatus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IPSelectStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // IPID
            // 
            this.IPID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IPID.FillWeight = 15F;
            this.IPID.HeaderText = "ID";
            this.IPID.Name = "IPID";
            // 
            // IPName
            // 
            this.IPName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IPName.FillWeight = 40F;
            this.IPName.HeaderText = "IP信息";
            this.IPName.Name = "IPName";
            // 
            // IPPort
            // 
            this.IPPort.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IPPort.FillWeight = 25F;
            this.IPPort.HeaderText = "端口号";
            this.IPPort.Name = "IPPort";
            // 
            // noSelectBtn
            // 
            this.noSelectBtn.Location = new System.Drawing.Point(84, 49);
            this.noSelectBtn.Name = "noSelectBtn";
            this.noSelectBtn.Size = new System.Drawing.Size(75, 23);
            this.noSelectBtn.TabIndex = 4;
            this.noSelectBtn.Text = "取消全选";
            this.noSelectBtn.UseVisualStyleBackColor = true;
            this.noSelectBtn.Click += new System.EventHandler(this.NoSelectBtn_Click);
            // 
            // allSelectBtn
            // 
            this.allSelectBtn.Location = new System.Drawing.Point(3, 49);
            this.allSelectBtn.Name = "allSelectBtn";
            this.allSelectBtn.Size = new System.Drawing.Size(75, 23);
            this.allSelectBtn.TabIndex = 3;
            this.allSelectBtn.Text = "全选";
            this.allSelectBtn.UseVisualStyleBackColor = true;
            this.allSelectBtn.Click += new System.EventHandler(this.AllSelectBtn_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 16);
            this.label1.TabIndex = 10003;
            this.label1.Text = "*提示：第一次选2-3台，观察结果大小后再运行全部机器。";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 12);
            this.label2.TabIndex = 10004;
            this.label2.Text = "全文主节点当前可用空间大小为：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(187, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 14);
            this.label3.TabIndex = 10005;
            this.label3.Text = " ";
            // 
            // SelectValidIPsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 389);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.noSelectBtn);
            this.Controls.Add(this.allSelectBtn);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SelectValidIPsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "全文运行节点选择窗口";
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.allSelectBtn, 0);
            this.Controls.SetChildIndex(this.noSelectBtn, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dictDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button noSelectBtn;
        private System.Windows.Forms.Button allSelectBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dictDataGridView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IPSelectStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn IPID;
        private System.Windows.Forms.DataGridViewTextBoxColumn IPName;
        private System.Windows.Forms.DataGridViewTextBoxColumn IPPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}