namespace _2048
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.plBoard = new System.Windows.Forms.Panel();
            this.ssr = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.mnu = new System.Windows.Forms.MenuStrip();
            this.游戏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新游戏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于游戏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrWatch = new System.Windows.Forms.Timer(this.components);
            this.lblTag = new System.Windows.Forms.Label();
            this.ssr.SuspendLayout();
            this.mnu.SuspendLayout();
            this.SuspendLayout();
            // 
            // plBoard
            // 
            this.plBoard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(175)))), ((int)(((byte)(158)))));
            this.plBoard.Location = new System.Drawing.Point(49, 92);
            this.plBoard.Name = "plBoard";
            this.plBoard.Size = new System.Drawing.Size(307, 307);
            this.plBoard.TabIndex = 0;
            // 
            // ssr
            // 
            this.ssr.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.ssr.Location = new System.Drawing.Point(0, 423);
            this.ssr.Name = "ssr";
            this.ssr.Size = new System.Drawing.Size(410, 22);
            this.ssr.TabIndex = 1;
            this.ssr.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(152, 17);
            this.toolStripStatusLabel1.Text = "键盘上方向键可以控制哦！";
            // 
            // mnu
            // 
            this.mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.游戏ToolStripMenuItem,
            this.关于ToolStripMenuItem});
            this.mnu.Location = new System.Drawing.Point(0, 0);
            this.mnu.Name = "mnu";
            this.mnu.Size = new System.Drawing.Size(410, 25);
            this.mnu.TabIndex = 2;
            this.mnu.Text = "menuStrip1";
            // 
            // 游戏ToolStripMenuItem
            // 
            this.游戏ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新游戏ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.游戏ToolStripMenuItem.Name = "游戏ToolStripMenuItem";
            this.游戏ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.游戏ToolStripMenuItem.Text = "游戏";
            // 
            // 新游戏ToolStripMenuItem
            // 
            this.新游戏ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("新游戏ToolStripMenuItem.Image")));
            this.新游戏ToolStripMenuItem.Name = "新游戏ToolStripMenuItem";
            this.新游戏ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.新游戏ToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.新游戏ToolStripMenuItem.Text = "新游戏(&N)";
            this.新游戏ToolStripMenuItem.Click += new System.EventHandler(this.新游戏ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("退出ToolStripMenuItem.Image")));
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.退出ToolStripMenuItem.Text = "退出(&X)";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.结束游戏ToolStripMenuItem_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于游戏ToolStripMenuItem});
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.关于ToolStripMenuItem.Text = "关于";
            // 
            // 关于游戏ToolStripMenuItem
            // 
            this.关于游戏ToolStripMenuItem.Name = "关于游戏ToolStripMenuItem";
            this.关于游戏ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.关于游戏ToolStripMenuItem.Text = "关于游戏";
            this.关于游戏ToolStripMenuItem.Click += new System.EventHandler(this.关于游戏ToolStripMenuItem_Click);
            // 
            // tmrWatch
            // 
            this.tmrWatch.Interval = 20;
            this.tmrWatch.Tick += new System.EventHandler(this.tmrWatch_Tick);
            // 
            // lblTag
            // 
            this.lblTag.AutoSize = true;
            this.lblTag.BackColor = System.Drawing.SystemColors.Control;
            this.lblTag.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTag.ForeColor = System.Drawing.Color.Gray;
            this.lblTag.Location = new System.Drawing.Point(38, 25);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(148, 64);
            this.lblTag.TabIndex = 7;
            this.lblTag.Text = "2048";
            this.lblTag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 445);
            this.Controls.Add(this.lblTag);
            this.Controls.Add(this.ssr);
            this.Controls.Add(this.mnu);
            this.Controls.Add(this.plBoard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnu;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.Text = "2048";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ssr.ResumeLayout(false);
            this.ssr.PerformLayout();
            this.mnu.ResumeLayout(false);
            this.mnu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel plBoard;
        private System.Windows.Forms.StatusStrip ssr;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.MenuStrip mnu;
        private System.Windows.Forms.ToolStripMenuItem 游戏ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于游戏ToolStripMenuItem;
        private System.Windows.Forms.Timer tmrWatch;
        private System.Windows.Forms.ToolStripMenuItem 新游戏ToolStripMenuItem;
        private System.Windows.Forms.Label lblTag;
    }
}

