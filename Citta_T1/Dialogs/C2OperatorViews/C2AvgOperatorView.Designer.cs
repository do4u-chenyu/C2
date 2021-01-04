namespace C2.Dialogs.C2OperatorViews
{
    partial class C2AvgOperatorView
    {
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
            this.label2 = new System.Windows.Forms.Label();
            this.bottomPanel.SuspendLayout();
            this.keyPanel.SuspendLayout();
            this.valuePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataSourceTB0
            // 
            this.dataSourceTB0.Location = new System.Drawing.Point(0, 0);
            this.dataSourceTB0.Size = new System.Drawing.Size(136, 23);
            this.dataSourceTB0.TabIndex = 2;
            // 
            // cancelButton
            // 
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Location = new System.Drawing.Point(219, 7);
            this.cancelButton.Size = new System.Drawing.Size(63, 27);
            // 
            // confirmButton
            // 
            this.confirmButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.confirmButton.FlatAppearance.BorderSize = 0;
            this.confirmButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.confirmButton.Location = new System.Drawing.Point(130, 7);
            this.confirmButton.Size = new System.Drawing.Size(60, 27);
            this.confirmButton.UseVisualStyleBackColor = false;
            // 
            // comboBox0
            // 
            this.comboBox0.Location = new System.Drawing.Point(1, 56);
            this.comboBox0.Size = new System.Drawing.Size(137, 21);
            this.comboBox0.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.Size = new System.Drawing.Size(86, 21);
            // 
            // topPanel
            // 
            this.topPanel.Size = new System.Drawing.Size(299, 10);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Location = new System.Drawing.Point(0, 118);
            this.bottomPanel.Size = new System.Drawing.Size(299, 43);
            // 
            // keyPanel
            // 
            this.keyPanel.Controls.Add(this.label2);
            this.keyPanel.Controls.Add(this.label1);
            this.keyPanel.Location = new System.Drawing.Point(0, 10);
            this.keyPanel.Size = new System.Drawing.Size(116, 108);
            this.keyPanel.Controls.SetChildIndex(this.label1, 0);
            this.keyPanel.Controls.SetChildIndex(this.label2, 0);
            // 
            // valuePanel
            // 
            this.valuePanel.Controls.Add(this.dataSourceTB0);
            this.valuePanel.Controls.Add(this.comboBox0);
            this.valuePanel.Location = new System.Drawing.Point(116, 10);
            this.valuePanel.Size = new System.Drawing.Size(183, 108);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(32, 4);
            this.label1.Size = new System.Drawing.Size(63, 14);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(32, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "取平均值";
            // 
            // C2AvgOperatorView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(299, 161);
            this.ControlBox = true;
            this.Controls.Add(this.valuePanel);
            this.Controls.Add(this.keyPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = global::C2.Properties.Resources.avg_icon;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(315, 200);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(315, 200);
            this.Name = "C2AvgOperatorView";
            this.ShowIcon = true;
            this.Text = "取平均值算子设置";
            this.bottomPanel.ResumeLayout(false);
            this.keyPanel.ResumeLayout(false);
            this.keyPanel.PerformLayout();
            this.valuePanel.ResumeLayout(false);
            this.valuePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label2;
    }
}