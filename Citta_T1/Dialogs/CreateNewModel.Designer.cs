namespace Citta_T1.Dialogs
{
    partial class CreateNewModel
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxEx1 = new HZH_Controls.Controls.TextBoxEx();
            this.AddButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14F);
            this.label1.Location = new System.Drawing.Point(92, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "新建模型";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 15F);
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(67, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = "*";
            // 
            // textBoxEx1
            // 
            this.textBoxEx1.DecLength = 2;
            this.textBoxEx1.InputType = HZH_Controls.TextInputType.NotControl;
            this.textBoxEx1.Location = new System.Drawing.Point(196, 48);
            this.textBoxEx1.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.textBoxEx1.MinValue = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.textBoxEx1.MyRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.textBoxEx1.Name = "textBoxEx1";
            this.textBoxEx1.OldText = null;
            this.textBoxEx1.PromptColor = System.Drawing.Color.Gray;
            this.textBoxEx1.PromptFont = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.textBoxEx1.PromptText = "";
            this.textBoxEx1.RegexPattern = "";
            this.textBoxEx1.Size = new System.Drawing.Size(150, 21);
            this.textBoxEx1.TabIndex = 4;
            // 
            // AddButton
            // 
            this.AddButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.AddButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.AddButton.FlatAppearance.BorderSize = 0;
            this.AddButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.AddButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.AddButton.Location = new System.Drawing.Point(218, 106);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(55, 28);
            this.AddButton.TabIndex = 18;
            this.AddButton.Text = "添加";
            this.AddButton.UseVisualStyleBackColor = false;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.cancelButton.Location = new System.Drawing.Point(291, 106);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(55, 28);
            this.cancelButton.TabIndex = 19;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // CreateNewModel
            // 
            this.ClientSize = new System.Drawing.Size(434, 172);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.textBoxEx1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateNewModel";
            this.ShowIcon = false;
            this.Text = "新建模型";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CreateNewModel_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private HZH_Controls.Controls.TextBoxEx textBoxEx1;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button cancelButton;
    }
}