namespace C2.SearchToolkit
{
    partial class SearchToolkitModelSettingsForm
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
            this.panelBottom = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.confirmButton = new System.Windows.Forms.Button();
            this.startTimeTB = new System.Windows.Forms.TextBox();
            this.endTimeTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panelBottom.Controls.Add(this.cancelButton);
            this.panelBottom.Controls.Add(this.confirmButton);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 136);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(550, 50);
            this.panelBottom.TabIndex = 5;
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cancelButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.cancelButton.Location = new System.Drawing.Point(476, 12);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(60, 27);
            this.cancelButton.TabIndex = 40;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // confirmButton
            // 
            this.confirmButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.confirmButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.confirmButton.Location = new System.Drawing.Point(385, 12);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(60, 27);
            this.confirmButton.TabIndex = 39;
            this.confirmButton.Text = "确认";
            this.confirmButton.UseVisualStyleBackColor = false;
            this.confirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // startTimeTB
            // 
            this.startTimeTB.Location = new System.Drawing.Point(93, 26);
            this.startTimeTB.Name = "startTimeTB";
            this.startTimeTB.Size = new System.Drawing.Size(200, 25);
            this.startTimeTB.TabIndex = 6;
            // 
            // endTimeTB
            // 
            this.endTimeTB.Location = new System.Drawing.Point(339, 26);
            this.endTimeTB.Name = "endTimeTB";
            this.endTimeTB.Size = new System.Drawing.Size(200, 25);
            this.endTimeTB.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 19);
            this.label1.TabIndex = 8;
            this.label1.Text = "查询时间范围";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(300, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 19);
            this.label2.TabIndex = 9;
            this.label2.Text = "----";
            // 
            // SearchToolkitModelSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(550, 186);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.endTimeTB);
            this.Controls.Add(this.startTimeTB);
            this.Controls.Add(this.panelBottom);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::C2.Properties.Resources.logo;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchToolkitModelSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "全文工具箱-自定义模型参数";
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.TextBox startTimeTB;
        private System.Windows.Forms.TextBox endTimeTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}