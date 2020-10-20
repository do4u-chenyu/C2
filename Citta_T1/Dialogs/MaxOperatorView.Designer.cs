namespace C2.Dialogs
{
    partial class MaxOperatorView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaxOperatorView));
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bottomPanel.SuspendLayout();
            this.keyPanel.SuspendLayout();
            this.valuePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataSourceTB0
            // 
            this.dataSourceTB0.Location = new System.Drawing.Point(0, 3);
            this.dataSourceTB0.Size = new System.Drawing.Size(150, 29);
            this.dataSourceTB0.TabIndex = 3;
            this.dataSourceTB0.TabStop = false;
            // 
            // cancelButton
            // 
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Location = new System.Drawing.Point(259, 9);
            this.cancelButton.Size = new System.Drawing.Size(63, 27);
            // 
            // confirmButton
            // 
            this.confirmButton.FlatAppearance.BorderSize = 0;
            this.confirmButton.Location = new System.Drawing.Point(176, 9);
            this.confirmButton.Size = new System.Drawing.Size(60, 27);
            // 
            // outListCCBL0
            // 
            this.outListCCBL0.Location = new System.Drawing.Point(0, 106);
            this.outListCCBL0.Size = new System.Drawing.Size(150, 30);
            this.outListCCBL0.TabIndex = 2;
            // 
            // comboBox0
            // 
            this.comboBox0.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox0.Location = new System.Drawing.Point(0, 49);
            this.comboBox0.Size = new System.Drawing.Size(148, 29);
            this.comboBox0.TabIndex = 1;
            // 
            // topPanel
            // 
            this.topPanel.Size = new System.Drawing.Size(326, 18);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Location = new System.Drawing.Point(0, 142);
            this.bottomPanel.Size = new System.Drawing.Size(326, 43);
            // 
            // keyPanel
            // 
            this.keyPanel.Controls.Add(this.label3);
            this.keyPanel.Controls.Add(this.label2);
            this.keyPanel.Controls.Add(this.label1);
            this.keyPanel.Size = new System.Drawing.Size(116, 124);
            this.keyPanel.Controls.SetChildIndex(this.label1, 0);
            this.keyPanel.Controls.SetChildIndex(this.label2, 0);
            this.keyPanel.Controls.SetChildIndex(this.label3, 0);
            // 
            // valuePanel
            // 
            this.valuePanel.Controls.Add(this.dataSourceTB0);
            this.valuePanel.Controls.Add(this.outListCCBL0);
            this.valuePanel.Controls.Add(this.comboBox0);
            this.valuePanel.Size = new System.Drawing.Size(210, 124);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(32, 4);
            this.label1.Size = new System.Drawing.Size(123, 30);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(32, 105);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 30);
            this.label3.TabIndex = 0;
            this.label3.Text = "输出字段：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(32, 55);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 30);
            this.label2.TabIndex = 0;
            this.label2.Text = "取最大值：";
            // 
            // MaxOperatorView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(326, 185);
            this.ControlBox = true;
            this.Controls.Add(this.valuePanel);
            this.Controls.Add(this.keyPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(344, 234);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(344, 234);
            this.Name = "MaxOperatorView";
            this.ShowIcon = true;
            this.Text = "取最大值算子设置";
            this.bottomPanel.ResumeLayout(false);
            this.keyPanel.ResumeLayout(false);
            this.keyPanel.PerformLayout();
            this.valuePanel.ResumeLayout(false);
            this.valuePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}