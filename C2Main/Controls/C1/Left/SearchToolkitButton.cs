using C2.Core;
using C2.SearchToolkit;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace C2.Controls.C1.Left
{
    public partial class SearchToolkitButton : BaseLeftInnerButton
    {
        private SearchTaskInfo task;
        public SearchToolkitButton(SearchTaskInfo task) : base(task.TaskName)
        {
            InitializeComponent();
            InitButtonMenu();
            InitButtonType();
            InitTaskInfo(task);
            
        }

        private void InitButtonType()
        {
            leftPictureBox.Image = Properties.Resources.全文工具左侧;
            rightPictureBox.Image = Properties.Resources.提示;
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

            contextMenuStrip.Items.AddRange(new ToolStripItem[] {
                    ResultToolStripMenuItem,
                    RemoveToolStripMenuItem
                 });

        }

        private void InitTaskInfo(SearchTaskInfo task)
        {
            this.task = task;
            toolTip.SetToolTip(rightPictureBox, task.BastionInfo);
            toolTip.SetToolTip(leftPictureBox, task.TaskModel);
            toolTip.SetToolTip(noFocusButton, task.TaskName);
        }
        private void ResultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTaskInfoDialog();
        }

        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show(
                  string.Format("删除任务【{0}】及结果文件, 继续删除请点击 \"确定\"", task.TaskName),
                  "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (rs != DialogResult.OK)
                return;
            // 用全局变量机械降神, 不是好的方式, 只是相对省事儿,不得已为之,尽量少用 
            Global.GetSearchToolkitControl().DeleteButton(this, task);
        }
        private void NoFocusButton_Paint(object sender, PaintEventArgs e)
        {
            Button B = (Button)sender;
            Size S = TextRenderer.MeasureText(task.TaskName, B.Font);
            TextRenderer.DrawText(e.Graphics, task.TaskName, B.Font, new Rectangle(0, B.ClientRectangle.Top + (B.ClientRectangle.Height - S.Height) / 2,
                B.ClientRectangle.Width - 2, B.ClientRectangle.Height), B.ForeColor, Color.Transparent, TextFormatFlags.EndEllipsis | TextFormatFlags.HorizontalCenter);
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
