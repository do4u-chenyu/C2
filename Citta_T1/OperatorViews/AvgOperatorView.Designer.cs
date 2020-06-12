namespace Citta_T1.OperatorViews
{
    partial class AvgOperatorView
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
            this.cancelButton.Location = new System.Drawing.Point(253, 19);
            this.cancelButton.Size = new System.Drawing.Size(63, 27);
            // 
            // confirmButton
            // 
            this.confirmButton.FlatAppearance.BorderSize = 0;
            this.confirmButton.Location = new System.Drawing.Point(170, 19);
            this.confirmButton.Size = new System.Drawing.Size(60, 27);
            // 
            // comboBox0
            // 
            this.comboBox0.Location = new System.Drawing.Point(0, 64);
            this.comboBox0.Size = new System.Drawing.Size(136, 24);
            this.comboBox0.TabIndex = 1;
            // 
            // topPanel
            // 
            this.topPanel.Size = new System.Drawing.Size(334, 37);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Location = new System.Drawing.Point(0, 125);
            this.bottomPanel.Size = new System.Drawing.Size(334, 64);
            // 
            // keyPanel
            // 
            this.keyPanel.Controls.Add(this.label2);
            this.keyPanel.Controls.Add(this.label1);
            this.keyPanel.Location = new System.Drawing.Point(0, 37);
            this.keyPanel.Size = new System.Drawing.Size(116, 88);
            this.keyPanel.Controls.SetChildIndex(this.label1, 0);
            this.keyPanel.Controls.SetChildIndex(this.label2, 0);
            // 
            // valuePanel
            // 
            this.valuePanel.Controls.Add(this.dataSourceTB0);
            this.valuePanel.Controls.Add(this.comboBox0);
            this.valuePanel.Location = new System.Drawing.Point(116, 37);
            this.valuePanel.Size = new System.Drawing.Size(218, 88);
            // 
            // label1
            // 
            this.label1.Size = new System.Drawing.Size(90, 22);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(32, 62);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 22);
            this.label2.TabIndex = 0;
            this.label2.Text = "取平均值：";
            // 
            // AvgOperatorView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(334, 189);
            this.Controls.Add(this.valuePanel);
            this.Controls.Add(this.keyPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "AvgOperatorView";
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