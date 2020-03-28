namespace Citta_T1.OperatorViews
{
    partial class DifferOperatorView
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
            this.DataInfo2 = new HZH_Controls.Controls.TextBoxEx();
            this.DataInfo1 = new HZH_Controls.Controls.TextBoxEx();
            this.OutList = new UserControlDLL.ComCheckBoxList();
            this.DifferFactor2 = new UserControlDLL.ComCheckBoxList();
            this.DifferFactor1 = new UserControlDLL.ComCheckBoxList();
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
            this.label2.Text = "差集条件：";
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
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
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
            this.confirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
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
            this.valuePanel.Controls.Add(this.DataInfo2);
            this.valuePanel.Controls.Add(this.DataInfo1);
            this.valuePanel.Controls.Add(this.OutList);
            this.valuePanel.Controls.Add(this.DifferFactor2);
            this.valuePanel.Controls.Add(this.DifferFactor1);
            this.valuePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valuePanel.Location = new System.Drawing.Point(116, 37);
            this.valuePanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.valuePanel.Name = "valuePanel";
            this.valuePanel.Size = new System.Drawing.Size(314, 185);
            this.valuePanel.TabIndex = 3;
            // 
            // DataInfo2
            // 
            this.DataInfo2.DecLength = 2;
            this.DataInfo2.Font = new System.Drawing.Font("微软雅黑", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DataInfo2.InputType = HZH_Controls.TextInputType.NotControl;
            this.DataInfo2.Location = new System.Drawing.Point(157, 0);
            this.DataInfo2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DataInfo2.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.DataInfo2.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.DataInfo2.MyRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.DataInfo2.Name = "DataInfo2";
            this.DataInfo2.OldText = null;
            this.DataInfo2.PromptColor = System.Drawing.Color.Gray;
            this.DataInfo2.PromptFont = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.DataInfo2.PromptText = "";
            this.DataInfo2.ReadOnly = true;
            this.DataInfo2.RegexPattern = "";
            this.DataInfo2.Size = new System.Drawing.Size(136, 25);
            this.DataInfo2.TabIndex = 6;
            this.DataInfo2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DataInfo1
            // 
            this.DataInfo1.DecLength = 2;
            this.DataInfo1.Font = new System.Drawing.Font("微软雅黑", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DataInfo1.InputType = HZH_Controls.TextInputType.NotControl;
            this.DataInfo1.Location = new System.Drawing.Point(0, 0);
            this.DataInfo1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DataInfo1.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.DataInfo1.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.DataInfo1.MyRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.DataInfo1.Name = "DataInfo1";
            this.DataInfo1.OldText = null;
            this.DataInfo1.PromptColor = System.Drawing.Color.Gray;
            this.DataInfo1.PromptFont = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.DataInfo1.PromptText = "";
            this.DataInfo1.ReadOnly = true;
            this.DataInfo1.RegexPattern = "";
            this.DataInfo1.Size = new System.Drawing.Size(136, 25);
            this.DataInfo1.TabIndex = 5;
            this.DataInfo1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // OutList
            // 
            this.OutList.DataSource = null;
            this.OutList.Location = new System.Drawing.Point(0, 128);
            this.OutList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.OutList.Name = "OutList";
            this.OutList.Size = new System.Drawing.Size(135, 20);
            this.OutList.TabIndex = 4;
            // 
            // DifferFactor2
            // 
            this.DifferFactor2.DataSource = null;
            this.DifferFactor2.Location = new System.Drawing.Point(157, 64);
            this.DifferFactor2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DifferFactor2.Name = "DifferFactor2";
            this.DifferFactor2.Size = new System.Drawing.Size(135, 20);
            this.DifferFactor2.TabIndex = 3;
            // 
            // DifferFactor1
            // 
            this.DifferFactor1.DataSource = null;
            this.DifferFactor1.Location = new System.Drawing.Point(0, 64);
            this.DifferFactor1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DifferFactor1.Name = "DifferFactor1";
            this.DifferFactor1.Size = new System.Drawing.Size(135, 20);
            this.DifferFactor1.TabIndex = 2;
            // 
            // DifferOperatorView
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
            this.Name = "DifferOperatorView";
            this.ShowIcon = false;
            this.Text = "取差集算子设置";
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
        private UserControlDLL.ComCheckBoxList OutList;
        private UserControlDLL.ComCheckBoxList DifferFactor2;
        private UserControlDLL.ComCheckBoxList DifferFactor1;
        private HZH_Controls.Controls.TextBoxEx DataInfo2;
        private HZH_Controls.Controls.TextBoxEx DataInfo1;
    }
}