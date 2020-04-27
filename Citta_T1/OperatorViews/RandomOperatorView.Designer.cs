﻿namespace Citta_T1.OperatorViews
{
    partial class RandomOperatorView
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
            this.topPanel = new System.Windows.Forms.Panel();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.confirmButton = new System.Windows.Forms.Button();
            this.keyPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.valuePanel = new System.Windows.Forms.Panel();
            this.OutList = new UserControlDLL.ComCheckBoxList();
            this.RandomNumBox = new HZH_Controls.Controls.TextBoxEx();
            this.DataInfoBox = new HZH_Controls.Controls.TextBoxEx();
            this.bottomPanel.SuspendLayout();
            this.keyPanel.SuspendLayout();
            this.valuePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Margin = new System.Windows.Forms.Padding(2);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(376, 37);
            this.topPanel.TabIndex = 0;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.cancelButton);
            this.bottomPanel.Controls.Add(this.confirmButton);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 205);
            this.bottomPanel.Margin = new System.Windows.Forms.Padding(2);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(376, 64);
            this.bottomPanel.TabIndex = 1;
            // 
            // cancelButton
            // 
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.cancelButton.Location = new System.Drawing.Point(293, 19);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
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
            this.confirmButton.Location = new System.Drawing.Point(208, 19);
            this.confirmButton.Margin = new System.Windows.Forms.Padding(2);
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
            this.keyPanel.Margin = new System.Windows.Forms.Padding(2);
            this.keyPanel.Name = "keyPanel";
            this.keyPanel.Size = new System.Drawing.Size(116, 168);
            this.keyPanel.TabIndex = 2;
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
            this.label2.Text = "随机条数：";
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
            // valuePanel
            // 
            this.valuePanel.Controls.Add(this.OutList);
            this.valuePanel.Controls.Add(this.RandomNumBox);
            this.valuePanel.Controls.Add(this.DataInfoBox);
            this.valuePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valuePanel.Location = new System.Drawing.Point(116, 37);
            this.valuePanel.Margin = new System.Windows.Forms.Padding(2);
            this.valuePanel.Name = "valuePanel";
            this.valuePanel.Size = new System.Drawing.Size(260, 168);
            this.valuePanel.TabIndex = 3;
            // 
            // OutList
            // 
            this.OutList.DataSource = null;
            this.OutList.Location = new System.Drawing.Point(0, 128);
            this.OutList.Margin = new System.Windows.Forms.Padding(0);
            this.OutList.Name = "OutList";
            this.OutList.Size = new System.Drawing.Size(135, 20);
            this.OutList.TabIndex = 2;
            // 
            // RandomNumBox
            // 
            this.RandomNumBox.DecLength = 2;
            this.RandomNumBox.Font = new System.Drawing.Font("微软雅黑", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RandomNumBox.InputType = HZH_Controls.TextInputType.NotControl;
            this.RandomNumBox.Location = new System.Drawing.Point(0, 64);
            this.RandomNumBox.Margin = new System.Windows.Forms.Padding(2);
            this.RandomNumBox.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.RandomNumBox.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.RandomNumBox.MyRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.RandomNumBox.Name = "RandomNumBox";
            this.RandomNumBox.OldText = null;
            this.RandomNumBox.PromptColor = System.Drawing.Color.Gray;
            this.RandomNumBox.PromptFont = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.RandomNumBox.PromptText = "";
            this.RandomNumBox.RegexPattern = "";
            this.RandomNumBox.Size = new System.Drawing.Size(136, 25);
            this.RandomNumBox.TabIndex = 1;
            // 
            // DataInfoBox
            // 
            this.DataInfoBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.DataInfoBox.DecLength = 2;
            this.DataInfoBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.DataInfoBox.Font = new System.Drawing.Font("微软雅黑", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DataInfoBox.InputType = HZH_Controls.TextInputType.NotControl;
            this.DataInfoBox.Location = new System.Drawing.Point(0, 0);
            this.DataInfoBox.Margin = new System.Windows.Forms.Padding(2);
            this.DataInfoBox.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.DataInfoBox.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.DataInfoBox.MyRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.DataInfoBox.Name = "DataInfoBox";
            this.DataInfoBox.OldText = null;
            this.DataInfoBox.PromptColor = System.Drawing.Color.White;
            this.DataInfoBox.PromptFont = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.DataInfoBox.PromptText = "";
            this.DataInfoBox.ReadOnly = true;
            this.DataInfoBox.RegexPattern = "";
            this.DataInfoBox.Size = new System.Drawing.Size(136, 25);
            this.DataInfoBox.TabIndex = 0;
            this.DataInfoBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // RandomOperatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 269);
            this.ControlBox = false;
            this.Controls.Add(this.valuePanel);
            this.Controls.Add(this.keyPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "RandomOperatorView";
            this.ShowIcon = false;
            this.Text = "随机算子设置";
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
        private HZH_Controls.Controls.TextBoxEx DataInfoBox;
        private HZH_Controls.Controls.TextBoxEx RandomNumBox;
        private UserControlDLL.ComCheckBoxList OutList;
    }
}