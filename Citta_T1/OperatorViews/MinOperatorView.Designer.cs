namespace Citta_T1.OperatorViews
{
    partial class MinOperatorView
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
            this.CancelButton = new System.Windows.Forms.Button();
            this.ConfirmButton = new System.Windows.Forms.Button();
            this.keyPanel = new System.Windows.Forms.Panel();
            this.valuePanel = new System.Windows.Forms.Panel();
            this.OutList = new UserControlDLL.ComCheckBoxList();
            this.MinValueBox = new System.Windows.Forms.ComboBox();
            this.DataInfoBox = new HZH_Controls.Controls.TextBoxEx();
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
            this.label2.Text = "取最小值：";
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
            this.topPanel.Margin = new System.Windows.Forms.Padding(2);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(422, 37);
            this.topPanel.TabIndex = 0;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.CancelButton);
            this.bottomPanel.Controls.Add(this.ConfirmButton);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 213);
            this.bottomPanel.Margin = new System.Windows.Forms.Padding(2);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(422, 64);
            this.bottomPanel.TabIndex = 1;
            // 
            // CancelButton
            // 
            this.CancelButton.FlatAppearance.BorderSize = 0;
            this.CancelButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.CancelButton.Location = new System.Drawing.Point(326, 19);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(63, 27);
            this.CancelButton.TabIndex = 1;
            this.CancelButton.Text = "取消";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ConfirmButton
            // 
            this.ConfirmButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.ConfirmButton.FlatAppearance.BorderSize = 0;
            this.ConfirmButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ConfirmButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ConfirmButton.Location = new System.Drawing.Point(236, 19);
            this.ConfirmButton.Margin = new System.Windows.Forms.Padding(2);
            this.ConfirmButton.Name = "ConfirmButton";
            this.ConfirmButton.Size = new System.Drawing.Size(60, 27);
            this.ConfirmButton.TabIndex = 0;
            this.ConfirmButton.Text = "确认";
            this.ConfirmButton.UseVisualStyleBackColor = false;
            this.ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
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
            this.keyPanel.Size = new System.Drawing.Size(116, 176);
            this.keyPanel.TabIndex = 2;
            // 
            // valuePanel
            // 
            this.valuePanel.Controls.Add(this.OutList);
            this.valuePanel.Controls.Add(this.MinValueBox);
            this.valuePanel.Controls.Add(this.DataInfoBox);
            this.valuePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valuePanel.Location = new System.Drawing.Point(116, 37);
            this.valuePanel.Margin = new System.Windows.Forms.Padding(2);
            this.valuePanel.Name = "valuePanel";
            this.valuePanel.Size = new System.Drawing.Size(306, 176);
            this.valuePanel.TabIndex = 3;
            // 
            // OutList
            // 
            this.OutList.DataSource = null;
            this.OutList.Location = new System.Drawing.Point(0, 128);
            this.OutList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.OutList.Name = "OutList";
            this.OutList.Size = new System.Drawing.Size(135, 20);
            this.OutList.TabIndex = 2;
            // 
            // MinValueBox
            // 
            this.MinValueBox.FormattingEnabled = true;
            this.MinValueBox.Location = new System.Drawing.Point(0, 64);
            this.MinValueBox.Margin = new System.Windows.Forms.Padding(2);
            this.MinValueBox.Name = "MinValueBox";
            this.MinValueBox.Size = new System.Drawing.Size(136, 20);
            this.MinValueBox.TabIndex = 1;
            // 
            // DataInfoBox
            // 
            this.DataInfoBox.DecLength = 2;
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
            this.DataInfoBox.PromptColor = System.Drawing.Color.Gray;
            this.DataInfoBox.PromptFont = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.DataInfoBox.PromptText = "";
            this.DataInfoBox.ReadOnly = true;
            this.DataInfoBox.RegexPattern = "";
            this.DataInfoBox.Size = new System.Drawing.Size(136, 25);
            this.DataInfoBox.TabIndex = 0;
            this.DataInfoBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MinOperatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 277);
            this.ControlBox = false;
            this.Controls.Add(this.valuePanel);
            this.Controls.Add(this.keyPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MinOperatorView";
            this.ShowIcon = false;
            this.Text = "取最小值算子设置";
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
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button ConfirmButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private UserControlDLL.ComCheckBoxList OutList;
        private System.Windows.Forms.ComboBox MinValueBox;
        private HZH_Controls.Controls.TextBoxEx DataInfoBox;
    }
}