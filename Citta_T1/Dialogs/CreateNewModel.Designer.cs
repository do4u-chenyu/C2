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
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
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
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Highlight;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.button2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button2.Location = new System.Drawing.Point(218, 106);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(55, 28);
            this.button2.TabIndex = 18;
            this.button2.Text = "添加";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.button3.Location = new System.Drawing.Point(291, 106);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(55, 28);
            this.button3.TabIndex = 19;
            this.button3.Text = "取消";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // CreateNewModel
            // 
            this.ClientSize = new System.Drawing.Size(434, 172);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBoxEx1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateNewModel";
            this.ShowIcon = false;
            this.Text = "新建模型";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private HZH_Controls.Controls.TextBoxEx textBoxEx1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}