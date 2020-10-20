

namespace C2.Controls
{
    partial class CanvasPanel
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.DelSelectControl = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DelControls = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.DelSelectControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.DeleteLineToolStripMenuItem_Click);
            // 
            // DelSelectControl
            // 
            this.DelSelectControl.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.DelSelectControl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DelControls});
            this.DelSelectControl.Name = "contextMenuStrip2";
            this.DelSelectControl.Size = new System.Drawing.Size(125, 26);
            // 
            // DelControls
            // 
            this.DelControls.Name = "DelControls";
            this.DelControls.Size = new System.Drawing.Size(124, 22);
            this.DelControls.Text = "批量删除";
            this.DelControls.Click += new System.EventHandler(this.DelControls_Click);
            // 
            // CanvasPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "CanvasPanel";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.CanvasPanel_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.CanvasPanel_DragEnter);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CanvasPanel_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CanvasPanel_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CanvasPanel_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CanvasPanel_MouseUp);
            this.contextMenuStrip1.ResumeLayout(false);
            this.DelSelectControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;

        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip DelSelectControl;
        private System.Windows.Forms.ToolStripMenuItem DelControls;
    }
}
