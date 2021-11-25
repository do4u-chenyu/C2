
namespace C2.Business.CastleBravo.WebShellTool
{
    partial class FunctionUnlockForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FunctionUnlockForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.ExitButton = new System.Windows.Forms.Button();
            this.IDBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.ExitButton);
            this.panel1.Location = new System.Drawing.Point(0, 165);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(340, 38);
            this.panel1.TabIndex = 0;
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(266, 10);
            this.ExitButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(64, 26);
            this.ExitButton.TabIndex = 0;
            this.ExitButton.Text = "取消";
            this.ExitButton.UseVisualStyleBackColor = true;
            // 
            // IDBox
            // 
            this.IDBox.Location = new System.Drawing.Point(154, 22);
            this.IDBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.IDBox.Name = "IDBox";
            this.IDBox.Size = new System.Drawing.Size(104, 21);
            this.IDBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(57, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "请输入工号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(63, 58);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(218, 57);
            this.label2.TabIndex = 3;
            this.label2.Text = "点击解锁危险功能，慎用\r\n点击解锁危险功能，慎用\r\n点击解锁危险功能，慎用";
            // 
            // OKButton
            // 
            this.OKButton.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OKButton.Location = new System.Drawing.Point(124, 129);
            this.OKButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(76, 31);
            this.OKButton.TabIndex = 4;
            this.OKButton.Text = "解锁";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // FunctionUnlockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 203);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IDBox);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FunctionUnlockForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "危险功能解锁";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox IDBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button OKButton;
    }
}