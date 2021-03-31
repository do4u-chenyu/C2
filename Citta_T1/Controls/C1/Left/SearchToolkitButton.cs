using C2.Core;
using C2.SearchToolkit;
using System;
using System.Drawing;
using System.Windows.Forms;

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
            InitTaskInfo(task);
            
        }

        private void InitButtonType()
        {
            this.leftPictureBox.Image = global::C2.Properties.Resources.全文工具左侧;
            this.rightPictureBox.Image = global::C2.Properties.Resources.提示;
        }

        private void InitButtonMenu()
        {
            ToolStripMenuItem RemoveToolStripMenuItem = new ToolStripMenuItem
            {
                Name = "RemoveToolStripMenuItem",
                Size = new Size(196, 22),
                Text = "删除任务",
                ToolTipText = "删除任务,同时删除本地文件"
            };
            RemoveToolStripMenuItem.Click += new EventHandler(RemoveToolStripMenuItem_Click);


            ToolStripMenuItem ResultToolStripMenuItem = new ToolStripMenuItem
            {
                Name = "ResultToolStripMenuItem",
                Size = new Size(196, 22),
                Text = "任务详情",
                ToolTipText = "查看任务的详细信息"
            };
            ResultToolStripMenuItem.Click += new EventHandler(ResultToolStripMenuItem_Click);

            this.contextMenuStrip.Items.AddRange(new ToolStripItem[] {
                    ResultToolStripMenuItem,
                    RemoveToolStripMenuItem
                 });

        }

        private void InitTaskInfo(TaskInfo task)
        {
            this.task = task;
            this.toolTip.SetToolTip(this.rightPictureBox, task.BastionInfo);
            this.toolTip.SetToolTip(this.leftPictureBox, task.PID);
        }
        private void ResultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTaskInfoDialog();
        }

        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show(
                  String.Format("删除任务【{0}】及结果文件, 继续删除请点击 \"确定\"", ButtonText),
                  "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (rs != DialogResult.OK)
                return;
            // 用全局变量机械降神, 不是好的方式, 只是相对省事儿,不得已为之,尽量少用 
            Global.GetSearchToolkitControl().DeleteButton(this, task);
        }

        private void NoFocusButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || e.Clicks != 2)
                return;
            // 双击打开
            ShowTaskInfoDialog();
        }

        private void ShowTaskInfoDialog()
        {
            new SearchToolkitForm().ShowTaskInfoDialog(this.task);
        }
    }
}
