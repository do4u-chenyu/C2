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
            this.noSelectBtn = new System.Windows.Forms.Button();
            this.allSelectBtn = new System.Windows.Forms.Button();
            this.secondChoiceBtn = new System.Windows.Forms.Button();
            this.firstChoiceBtn = new System.Windows.Forms.Button();
            this.dictListView = new System.Windows.Forms.ListView();
            this.IPID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IPName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IPPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dictListView);
            this.groupBox2.Location = new System.Drawing.Point(0, 59);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(346, 285);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "IP（选择0个）";
            // 
            // noSelectBtn
            // 
            this.noSelectBtn.Location = new System.Drawing.Point(253, 30);
            this.noSelectBtn.Name = "noSelectBtn";
            this.noSelectBtn.Size = new System.Drawing.Size(75, 23);
            this.noSelectBtn.TabIndex = 4;
            this.noSelectBtn.Text = "全不选";
            this.noSelectBtn.UseVisualStyleBackColor = true;
            // 
            // allSelectBtn
            // 
            this.allSelectBtn.Location = new System.Drawing.Point(172, 30);
            this.allSelectBtn.Name = "allSelectBtn";
            this.allSelectBtn.Size = new System.Drawing.Size(75, 23);
            this.allSelectBtn.TabIndex = 3;
            this.allSelectBtn.Text = "全选";
            this.allSelectBtn.UseVisualStyleBackColor = true;
            // 
            // secondChoiceBtn
            // 
            this.secondChoiceBtn.Location = new System.Drawing.Point(91, 30);
            this.secondChoiceBtn.Name = "secondChoiceBtn";
            this.secondChoiceBtn.Size = new System.Drawing.Size(75, 23);
            this.secondChoiceBtn.TabIndex = 2;
            this.secondChoiceBtn.Text = "非第一次";
            this.secondChoiceBtn.UseVisualStyleBackColor = true;
            // 
            // firstChoiceBtn
            // 
            this.firstChoiceBtn.Location = new System.Drawing.Point(10, 30);
            this.firstChoiceBtn.Name = "firstChoiceBtn";
            this.firstChoiceBtn.Size = new System.Drawing.Size(75, 23);
            this.firstChoiceBtn.TabIndex = 1;
            this.firstChoiceBtn.Text = "第一次推荐";
            this.firstChoiceBtn.UseVisualStyleBackColor = true;
            // 
            // dictListView
            // 
            this.dictListView.CheckBoxes = true;
            this.dictListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IPID,
            this.IPName,
            this.IPPort});
            this.dictListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dictListView.FullRowSelect = true;
            this.dictListView.HideSelection = false;
            this.dictListView.Location = new System.Drawing.Point(3, 17);
            this.dictListView.Name = "dictListView";
            this.dictListView.Size = new System.Drawing.Size(340, 265);
            this.dictListView.TabIndex = 0;
            this.dictListView.UseCompatibleStateImageBehavior = false;
            this.dictListView.View = System.Windows.Forms.View.Details;
            // 
            // IPID
            // 
            this.IPID.Text = "ID";
            this.IPID.Width = 54;
            // 
            // IPName
            // 
            this.IPName.Text = "IP信息";
            this.IPName.Width = 193;
            // 
            // IPPort
            // 
            this.IPPort.Text = "端口号";
            this.IPPort.Width = 85;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 18);
            this.label1.TabIndex = 10003;
            this.label1.Text = "*提示：第一次选2-3台，观察结果大小后再运行全部机器。";
            // 
            // SelectValidIPsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 389);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.noSelectBtn);
            this.Controls.Add(this.allSelectBtn);
            this.Controls.Add(this.secondChoiceBtn);
            this.Controls.Add(this.firstChoiceBtn);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SelectValidIPsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "全文运行节点选择窗口";
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.firstChoiceBtn, 0);
            this.Controls.SetChildIndex(this.secondChoiceBtn, 0);
            this.Controls.SetChildIndex(this.allSelectBtn, 0);
            this.Controls.SetChildIndex(this.noSelectBtn, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button noSelectBtn;
        private System.Windows.Forms.Button allSelectBtn;
        private System.Windows.Forms.Button secondChoiceBtn;
        private System.Windows.Forms.Button firstChoiceBtn;
        private System.Windows.Forms.ListView dictListView;
        private System.Windows.Forms.ColumnHeader IPID;
        private System.Windows.Forms.ColumnHeader IPName;
        private System.Windows.Forms.ColumnHeader IPPort;
        private System.Windows.Forms.Label label1;
    }
}