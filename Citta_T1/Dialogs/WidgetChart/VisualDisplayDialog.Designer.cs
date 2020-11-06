namespace C2.Dialogs
{
    partial class VisualDisplayDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisualDisplayDialog));
            this.chartType = new System.Windows.Forms.Label();
            this.x = new System.Windows.Forms.Label();
            this.y = new System.Windows.Forms.Label();
            this.chartTypesList = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cancelButton.Location = new System.Drawing.Point(373, 257);
            this.cancelButton.Size = new System.Drawing.Size(74, 32);
            this.cancelButton.TabIndex = 7;
            // 
            // confirmButton
            // 
            this.confirmButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.confirmButton.FlatAppearance.BorderSize = 0;
            this.confirmButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.confirmButton.Location = new System.Drawing.Point(287, 257);
            this.confirmButton.Margin = new System.Windows.Forms.Padding(0);
            this.confirmButton.Size = new System.Drawing.Size(77, 32);
            this.confirmButton.TabIndex = 6;
            // 
            // outListCCBL0
            // 
            this.outListCCBL0.Location = new System.Drawing.Point(182, 146);
            this.outListCCBL0.Margin = new System.Windows.Forms.Padding(2);
            this.outListCCBL0.Size = new System.Drawing.Size(150, 23);
            this.outListCCBL0.TabIndex = 5;
            // 
            // comboBox0
            // 
            this.comboBox0.Location = new System.Drawing.Point(182, 110);
            this.comboBox0.Size = new System.Drawing.Size(150, 21);
            this.comboBox0.TabIndex = 4;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Location = new System.Drawing.Point(0, 249);
            this.bottomPanel.Size = new System.Drawing.Size(459, 47);
            // 
            // chartType
            // 
            this.chartType.AutoSize = true;
            this.chartType.BackColor = System.Drawing.Color.White;
            this.chartType.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chartType.Location = new System.Drawing.Point(79, 72);
            this.chartType.Name = "chartType";
            this.chartType.Size = new System.Drawing.Size(84, 20);
            this.chartType.TabIndex = 0;
            this.chartType.Text = "图表类型：";
            // 
            // x
            // 
            this.x.AutoSize = true;
            this.x.BackColor = System.Drawing.Color.White;
            this.x.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.x.Location = new System.Drawing.Point(79, 109);
            this.x.Name = "x";
            this.x.Size = new System.Drawing.Size(84, 20);
            this.x.TabIndex = 1;
            this.x.Text = "输入维度：";
            // 
            // y
            // 
            this.y.AutoSize = true;
            this.y.BackColor = System.Drawing.Color.White;
            this.y.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.y.Location = new System.Drawing.Point(79, 146);
            this.y.Name = "y";
            this.y.Size = new System.Drawing.Size(84, 20);
            this.y.TabIndex = 2;
            this.y.Text = "输出维度：";
            // 
            // chartTypesList
            // 
            this.chartTypesList.AllowDrop = true;
            this.chartTypesList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chartTypesList.FormattingEnabled = true;
            this.chartTypesList.ItemHeight = 14;
            this.chartTypesList.Items.AddRange(new object[] {
            "饼图",
            "折线图",
            "雷达图",
            "圆环图"});
            this.chartTypesList.Location = new System.Drawing.Point(182, 73);
            this.chartTypesList.Name = "chartTypesList";
            this.chartTypesList.Size = new System.Drawing.Size(150, 22);
            this.chartTypesList.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.comboBox0);
            this.panel1.Controls.Add(this.chartTypesList);
            this.panel1.Controls.Add(this.y);
            this.panel1.Controls.Add(this.x);
            this.panel1.Controls.Add(this.outListCCBL0);
            this.panel1.Controls.Add(this.chartType);
            this.panel1.Location = new System.Drawing.Point(0, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(461, 247);
            this.panel1.TabIndex = 8;
            this.panel1.Controls.SetChildIndex(this.chartType, 0);
            this.panel1.Controls.SetChildIndex(this.outListCCBL0, 0);
            this.panel1.Controls.SetChildIndex(this.x, 0);
            this.panel1.Controls.SetChildIndex(this.y, 0);
            this.panel1.Controls.SetChildIndex(this.chartTypesList, 0);
            this.panel1.Controls.SetChildIndex(this.comboBox0, 0);
            // 
            // VisualDisplayDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(459, 296);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bottomPanel);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisualDisplayDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "可视化展示";
            this.Controls.SetChildIndex(this.bottomPanel, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.confirmButton, 0);
            this.Controls.SetChildIndex(this.cancelButton, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label chartType;
        private System.Windows.Forms.Label x;
        private System.Windows.Forms.Label y;
        private System.Windows.Forms.ComboBox chartTypesList;
        private System.Windows.Forms.Panel panel1;
    }
}