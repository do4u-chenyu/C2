using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Controls.C1.Left
{
    public partial class SearchToolkitButton : BaseLeftInnerButton
    {
        public SearchToolkitButton(string buttonText) : base(buttonText)
        {
            InitializeComponent();
            InitButtonMenu();
            InitButtonType();
        }

        private void InitButtonType()
        {
            this.leftPictureBox.Image = global::C2.Properties.Resources.数据;
            this.rightPictureBox.Image = global::C2.Properties.Resources.提示;
        }

        private void InitButtonMenu()
        {
            //ToolStripMenuItem RemoveToolStripMenuItem = new ToolStripMenuItem();
            //RemoveToolStripMenuItem.Name = "ReviewToolStripMenuItem";
            //RemoveToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            //RemoveToolStripMenuItem.Text = "删除任务";
            //RemoveToolStripMenuItem.ToolTipText = "从面板中移除任务,同时删除本地结果文件";
            //RemoveToolStripMenuItem.Click += new System.EventHandler(RemoveToolStripMenuItem_Click);

            //ToolStripMenuItem OpenDatasourceToolStripMenuItem = new ToolStripMenuItem();
            //OpenDatasourceToolStripMenuItem.Name = "OpenDatasourceToolStripMenuItem";
            //OpenDatasourceToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            //OpenDatasourceToolStripMenuItem.Text = "打开源文件";
            //OpenDatasourceToolStripMenuItem.ToolTipText = "从本地文本编辑器中打开文件";
            //OpenDatasourceToolStripMenuItem.Click += new System.EventHandler(OpenDatasourceToolStripMenuItem_Click);

            //ToolStripMenuItem ResultToolStripMenuItem = new ToolStripMenuItem();
            //ResultToolStripMenuItem.Name = "ResultToolStripMenuItem";
            //ResultToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            //ResultToolStripMenuItem.Text = "查看结果";
            //ResultToolStripMenuItem.ToolTipText = "查看任务返回结果";
            //ResultToolStripMenuItem.Click += new System.EventHandler(ResultToolStripMenuItem_Click);

            //this.contextMenuStrip.Items.AddRange(new ToolStripItem[] {
            //        OpenDatasourceToolStripMenuItem,
            //        ResultToolStripMenuItem,
            //        RemoveToolStripMenuItem
            //     });

        }
    }
}
