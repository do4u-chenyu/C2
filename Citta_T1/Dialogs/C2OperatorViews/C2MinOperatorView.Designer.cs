namespace C2.Dialogs.C2OperatorViews
{
    partial class C2MinOperatorView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(C2MinOperatorView));
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bottomPanel.SuspendLayout();
            this.keyPanel.SuspendLayout();
            this.valuePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataSourceTB0
            // 
            this.dataSourceTB0.Location = new System.Drawing.Point(2, 0);
            this.dataSourceTB0.Size = new System.Drawing.Size(148, 23);
            this.dataSourceTB0.TabIndex = 3;
            // 
            // cancelButton
            // 
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Location = new System.Drawing.Point(219, 7);
            this.cancelButton.Size = new System.Drawing.Size(63, 27);
            // 
            // confirmButton
            // 
            this.confirmButton.FlatAppearance.BorderSize = 0;
            this.confirmButton.Location = new System.Drawing.Point(137, 7);
            this.confirmButton.Size = new System.Drawing.Size(60, 27);
            // 
            // outListCCBL0
            // 
            this.outListCCBL0.Location = new System.Drawing.Point(3, 94);
            this.outListCCBL0.Size = new System.Drawing.Size(150, 23);
            this.outListCCBL0.TabIndex = 2;
            // 
            // comboBox0
            // 
            this.comboBox0.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox0.Font = new System.Drawing.Font("宋体", 10F);
            this.comboBox0.Location = new System.Drawing.Point(2, 48);
            this.comboBox0.Size = new System.Drawing.Size(150, 21);
            this.comboBox0.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.Size = new System.Drawing.Size(86, 21);
            // 
            // topPanel
            // 
            this.topPanel.Size = new System.Drawing.Size(299, 14);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Location = new System.Drawing.Point(0, 153);
            this.bottomPanel.Size = new System.Drawing.Size(299, 38);
            // 
            // keyPanel
            // 
            this.keyPanel.Controls.Add(this.label3);
            this.keyPanel.Controls.Add(this.label2);
            this.keyPanel.Controls.Add(this.label1);
            this.keyPanel.Location = new System.Drawing.Point(0, 14);
            this.keyPanel.Size = new System.Drawing.Size(114, 139);
            this.keyPanel.Controls.SetChildIndex(this.label1, 0);
            this.keyPanel.Controls.SetChildIndex(this.label2, 0);
            this.keyPanel.Controls.SetChildIndex(this.label3, 0);
            // 
            // valuePanel
            // 
            this.valuePanel.Controls.Add(this.dataSourceTB0);
            this.valuePanel.Controls.Add(this.outListCCBL0);
            this.valuePanel.Controls.Add(this.comboBox0);
            this.valuePanel.Location = new System.Drawing.Point(114, 14);
            this.valuePanel.Size = new System.Drawing.Size(185, 139);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(31, 5);
            this.label1.Size = new System.Drawing.Size(63, 14);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F);
            this.label3.Location = new System.Drawing.Point(31, 96);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "输出字段";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F);
            this.label2.Location = new System.Drawing.Point(31, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "取最小值";
            // 
            // MinOperatorView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(299, 191);
            this.ControlBox = true;
            this.Controls.Add(this.valuePanel);
            this.Controls.Add(this.keyPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = Properties.Resources.min_icon;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(315, 230);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(315, 230);
            this.Name = "MinOperatorView";
            this.ShowIcon = true;
            this.Text = "取最小值算子设置";
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