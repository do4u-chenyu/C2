namespace _2048
{
    partial class GameOver
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
            this.lbl = new System.Windows.Forms.Label();
            this.plTitle = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.plSelect = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnAgain = new System.Windows.Forms.Button();
            this.plTitle.SuspendLayout();
            this.plSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl.ForeColor = System.Drawing.Color.White;
            this.lbl.Location = new System.Drawing.Point(11, 22);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(158, 29);
            this.lbl.TabIndex = 0;
            this.lbl.Text = "游戏失败！";
            // 
            // plTitle
            // 
            this.plTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plTitle.Controls.Add(this.lbl);
            this.plTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.plTitle.Location = new System.Drawing.Point(0, 0);
            this.plTitle.Name = "plTitle";
            this.plTitle.Size = new System.Drawing.Size(351, 78);
            this.plTitle.TabIndex = 1;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInfo.ForeColor = System.Drawing.Color.White;
            this.lblInfo.Location = new System.Drawing.Point(13, 109);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(315, 57);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "你并没有拼出2048，别灰心，继续努\r\n力哦，据说全世界只有3%的人能拼\r\n出2048，再来一局吧，看好你哟！";
            // 
            // plSelect
            // 
            this.plSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(154)))), ((int)(((byte)(154)))));
            this.plSelect.Controls.Add(this.btnExit);
            this.plSelect.Controls.Add(this.btnAgain);
            this.plSelect.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plSelect.Location = new System.Drawing.Point(0, 199);
            this.plSelect.Name = "plSelect";
            this.plSelect.Size = new System.Drawing.Size(351, 73);
            this.plSelect.TabIndex = 3;
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.Location = new System.Drawing.Point(193, 16);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(135, 45);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnAgain
            // 
            this.btnAgain.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAgain.Location = new System.Drawing.Point(17, 16);
            this.btnAgain.Name = "btnAgain";
            this.btnAgain.Size = new System.Drawing.Size(135, 45);
            this.btnAgain.TabIndex = 0;
            this.btnAgain.Text = "再来一局";
            this.btnAgain.UseVisualStyleBackColor = true;
            this.btnAgain.Click += new System.EventHandler(this.btnAgain_Click);
            // 
            // frmGameOver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(351, 272);
            this.Controls.Add(this.plSelect);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.plTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmGameOver";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmGameOver";
            this.plTitle.ResumeLayout(false);
            this.plTitle.PerformLayout();
            this.plSelect.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Panel plTitle;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Panel plSelect;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnAgain;

    }
}