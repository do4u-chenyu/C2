namespace Citta_T1.OperatorViews
{
    partial class UnionOperatorView
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.topPanel = new System.Windows.Forms.Panel();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.confirmButton = new System.Windows.Forms.Button();
            this.keyPanel = new System.Windows.Forms.Panel();
            this.valuePanel = new System.Windows.Forms.Panel();
            this.dataSource1 = new HZH_Controls.Controls.TextBoxEx();
            this.dataSource0 = new HZH_Controls.Controls.TextBoxEx();
            this.comCheckBoxList3 = new UserControlDLL.ComCheckBoxList();
            this.comCheckBoxList2 = new UserControlDLL.ComCheckBoxList();
            this.comCheckBoxList1 = new UserControlDLL.ComCheckBoxList();
            this.bottomPanel.SuspendLayout();
            this.keyPanel.SuspendLayout();
            this.valuePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(32, 128);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 22);
            this.label3.TabIndex = 0;
            this.label3.Text = "输出字段：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(32, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 22);
            this.label2.TabIndex = 0;
            this.label2.Text = "并集条件：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(32, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据信息：";
            // 
            // topPanel
            // 
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(430, 37);
            this.topPanel.TabIndex = 0;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.cancelButton);
            this.bottomPanel.Controls.Add(this.confirmButton);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 222);
            this.bottomPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(430, 64);
            this.bottomPanel.TabIndex = 1;
            // 
            // cancelButton
            // 
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.cancelButton.Location = new System.Drawing.Point(326, 19);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(63, 27);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // confirmButton
            // 
            this.confirmButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.confirmButton.FlatAppearance.BorderSize = 0;
            this.confirmButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.confirmButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.confirmButton.Location = new System.Drawing.Point(236, 19);
            this.confirmButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(60, 27);
            this.confirmButton.TabIndex = 0;
            this.confirmButton.Text = "确认";
            this.confirmButton.UseVisualStyleBackColor = false;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // keyPanel
            // 
            this.keyPanel.Controls.Add(this.label3);
            this.keyPanel.Controls.Add(this.label2);
            this.keyPanel.Controls.Add(this.label1);
            this.keyPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.keyPanel.Location = new System.Drawing.Point(0, 37);
            this.keyPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.keyPanel.Name = "keyPanel";
            this.keyPanel.Size = new System.Drawing.Size(116, 185);
            this.keyPanel.TabIndex = 2;
            // 
            // valuePanel
            // 
            this.valuePanel.Controls.Add(this.dataSource1);
            this.valuePanel.Controls.Add(this.dataSource0);
            this.valuePanel.Controls.Add(this.comCheckBoxList3);
            this.valuePanel.Controls.Add(this.comCheckBoxList2);
            this.valuePanel.Controls.Add(this.comCheckBoxList1);
            this.valuePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valuePanel.Location = new System.Drawing.Point(116, 37);
            this.valuePanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.valuePanel.Name = "valuePanel";
            this.valuePanel.Size = new System.Drawing.Size(314, 185);
            this.valuePanel.TabIndex = 3;
            // 
            // dataSource1
            // 
            this.dataSource1.DecLength = 2;
            this.dataSource1.Font = new System.Drawing.Font("微软雅黑", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataSource1.InputType = HZH_Controls.TextInputType.NotControl;
            this.dataSource1.Location = new System.Drawing.Point(157, 0);
            this.dataSource1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataSource1.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.dataSource1.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.dataSource1.MyRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.dataSource1.Name = "dataSource1";
            this.dataSource1.OldText = null;
            this.dataSource1.PromptColor = System.Drawing.Color.Gray;
            this.dataSource1.PromptFont = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.dataSource1.PromptText = "";
            this.dataSource1.ReadOnly = true;
            this.dataSource1.RegexPattern = "";
            this.dataSource1.Size = new System.Drawing.Size(136, 25);
            this.dataSource1.TabIndex = 6;
            this.dataSource1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dataSource0
            // 
            this.dataSource0.DecLength = 2;
            this.dataSource0.Font = new System.Drawing.Font("微软雅黑", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataSource0.InputType = HZH_Controls.TextInputType.NotControl;
            this.dataSource0.Location = new System.Drawing.Point(0, 0);
            this.dataSource0.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataSource0.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.dataSource0.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.dataSource0.MyRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.dataSource0.Name = "dataSource0";
            this.dataSource0.OldText = null;
            this.dataSource0.PromptColor = System.Drawing.Color.Gray;
            this.dataSource0.PromptFont = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.dataSource0.PromptText = "";
            this.dataSource0.ReadOnly = true;
            this.dataSource0.RegexPattern = "";
            this.dataSource0.Size = new System.Drawing.Size(136, 25);
            this.dataSource0.TabIndex = 5;
            this.dataSource0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comCheckBoxList3
            // 
            this.comCheckBoxList3.DataSource = null;
            this.comCheckBoxList3.Location = new System.Drawing.Point(0, 128);
            this.comCheckBoxList3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comCheckBoxList3.Name = "comCheckBoxList3";
            this.comCheckBoxList3.Size = new System.Drawing.Size(135, 20);
            this.comCheckBoxList3.TabIndex = 4;
            // 
            // comCheckBoxList2
            // 
            this.comCheckBoxList2.DataSource = null;
            this.comCheckBoxList2.Location = new System.Drawing.Point(157, 64);
            this.comCheckBoxList2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comCheckBoxList2.Name = "comCheckBoxList2";
            this.comCheckBoxList2.Size = new System.Drawing.Size(135, 20);
            this.comCheckBoxList2.TabIndex = 3;
            // 
            // comCheckBoxList1
            // 
            this.comCheckBoxList1.DataSource = null;
            this.comCheckBoxList1.Location = new System.Drawing.Point(0, 64);
            this.comCheckBoxList1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comCheckBoxList1.Name = "comCheckBoxList1";
            this.comCheckBoxList1.Size = new System.Drawing.Size(135, 20);
            this.comCheckBoxList1.TabIndex = 2;
            // 
            // UnionOperatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 286);
            this.ControlBox = false;
            this.Controls.Add(this.valuePanel);
            this.Controls.Add(this.keyPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "UnionOperatorView";
            this.ShowIcon = false;
            this.Text = "取并集算子设置";
            this.bottomPanel.ResumeLayout(false);
            this.keyPanel.ResumeLayout(false);
            this.keyPanel.PerformLayout();
            this.valuePanel.ResumeLayout(false);
            this.valuePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Panel keyPanel;
        private System.Windows.Forms.Panel valuePanel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private UserControlDLL.ComCheckBoxList comCheckBoxList3;
        private UserControlDLL.ComCheckBoxList comCheckBoxList2;
        private UserControlDLL.ComCheckBoxList comCheckBoxList1;
        private HZH_Controls.Controls.TextBoxEx dataSource1;
        private HZH_Controls.Controls.TextBoxEx dataSource0;
    }
}