namespace Citta_T1
{
    partial class DataGridView1
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItem全选 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem复制 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem全部清除 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("微软雅黑", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(1348, 171);
            this.textBox1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem全选,
            this.MenuItem复制,
            this.MenuItem全部清除});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(139, 76);
            // 
            // MenuItem全选
            // 
            this.MenuItem全选.Name = "MenuItem全选";
            this.MenuItem全选.Size = new System.Drawing.Size(138, 24);
            this.MenuItem全选.Text = "全选";
            this.MenuItem全选.Click += new System.EventHandler(this.MenuItem全选_Click);
            // 
            // MenuItem复制
            // 
            this.MenuItem复制.Name = "MenuItem复制";
            this.MenuItem复制.Size = new System.Drawing.Size(138, 24);
            this.MenuItem复制.Text = "复制";
            this.MenuItem复制.Click += new System.EventHandler(this.MenuItem复制_Click);
            // 
            // MenuItem全部清除
            // 
            this.MenuItem全部清除.Name = "MenuItem全部清除";
            this.MenuItem全部清除.Size = new System.Drawing.Size(138, 24);
            this.MenuItem全部清除.Text = "全部清除";
            this.MenuItem全部清除.Click += new System.EventHandler(this.MenuItem全部清除_Click);
            // 
            // DataGridView1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DataGridView1";
            this.Size = new System.Drawing.Size(1348, 171);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuItem复制;
        private System.Windows.Forms.ToolStripMenuItem MenuItem全部清除;
        private System.Windows.Forms.ToolStripMenuItem MenuItem全选;
    }
}
