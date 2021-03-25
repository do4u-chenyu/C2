using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C2.SearchToolkit;

namespace C2.Controls.C1.Left
{
    public partial class SearchToolkitButton : BaseLeftInnerButton
    {
        private TaskInfo task;
        public SearchToolkitButton(TaskInfo task) : base(task.TaskName)
        {
            InitializeComponent();
            InitButtonMenu();
            InitButtonType();
            this.task = task;
        }

        private void InitButtonType()
        {
            this.leftPictureBox.Image = global::C2.Properties.Resources.数据;
            this.rightPictureBox.Image = global::C2.Properties.Resources.提示;
        }

        private void InitButtonMenu()
        {
            ToolStripMenuItem RemoveToolStripMenuItem = new ToolStripMenuItem
            {
                Name = "ReviewToolStripMenuItem",
                Size = new Size(196, 22),
                Text = "删除任务",
                ToolTipText = "从面板中移除任务,同时删除本地结果文件"
            };
            RemoveToolStripMenuItem.Click += new EventHandler(RemoveToolStripMenuItem_Click);


            ToolStripMenuItem ResultToolStripMenuItem = new ToolStripMenuItem
            {
                Name = "ResultToolStripMenuItem",
                Size = new Size(196, 22),
                Text = "查看结果",
                ToolTipText = "查看任务返回结果"
            };
            ResultToolStripMenuItem.Click += new EventHandler(ResultToolStripMenuItem_Click);

            this.contextMenuStrip.Items.AddRange(new ToolStripItem[] {
                    ResultToolStripMenuItem,
                    RemoveToolStripMenuItem
                 });

        }

        private void ResultToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}
