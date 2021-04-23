﻿using C2.Core;

namespace C2.Controls.Left
{
    partial class IAOButton
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
            this.leftPictureBox = new System.Windows.Forms.PictureBox();
            this.rightPictureBox = new System.Windows.Forms.PictureBox();
            this.textBox = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtButton = new C2.Controls.Common.AutoEllipsisButton();
            ((System.ComponentModel.ISupportInitialize)(this.leftPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // leftPictureBox
            // 
            this.leftPictureBox.Location = new System.Drawing.Point(1, 1);
            this.leftPictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.leftPictureBox.Name = "leftPictureBox";
            this.leftPictureBox.Size = new System.Drawing.Size(24, 24);
            this.leftPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.leftPictureBox.TabIndex = 10;
            this.leftPictureBox.TabStop = false;
            // 
            // rightPictureBox
            // 
            this.rightPictureBox.Image = global::C2.Properties.Resources.提示;
            this.rightPictureBox.Location = new System.Drawing.Point(122, 6);
            this.rightPictureBox.Name = "rightPictureBox";
            this.rightPictureBox.Size = new System.Drawing.Size(16, 16);
            this.rightPictureBox.TabIndex = 11;
            this.rightPictureBox.TabStop = false;
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.Color.White;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.textBox.Location = new System.Drawing.Point(25, 3);
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.Size = new System.Drawing.Size(94, 16);
            this.textBox.TabIndex = 12;
            this.textBox.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.打开ToolStripMenuItem.Text = "打开";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.ReshowDelay = 500;
            this.toolTip1.ShowAlways = true;
            // 
            // txtButton
            // 
            this.txtButton.AutoEllipsis = true;
            this.txtButton.BackColor = System.Drawing.Color.White;
            this.txtButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtButton.FlatAppearance.BorderSize = 0;
            this.txtButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtButton.Location = new System.Drawing.Point(25, 1);
            this.txtButton.Margin = new System.Windows.Forms.Padding(2);
            this.txtButton.Name = "txtButton";
            this.txtButton.Size = new System.Drawing.Size(95, 25);
            this.txtButton.TabIndex = 9;
            this.txtButton.Text = null;
            this.txtButton.UseVisualStyleBackColor = false;
            this.txtButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TxtButton_MouseDown);
            // 
            // IAOButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.rightPictureBox);
            this.Controls.Add(this.leftPictureBox);
            this.Controls.Add(this.txtButton);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "IAOButton";
            this.Size = new System.Drawing.Size(140, 27);
            ((System.ComponentModel.ISupportInitialize)(this.leftPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C2.Controls.Common.AutoEllipsisButton txtButton;
        private System.Windows.Forms.PictureBox leftPictureBox;
        private System.Windows.Forms.PictureBox rightPictureBox;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
