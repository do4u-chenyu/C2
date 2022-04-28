using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C2.Business.WebsiteFeatureDetection;
using C2.Dialogs.WebsiteFeatureDetection;
using C2.Core;
using C2.Utils;

namespace C2.Controls.C1.Left
{
    public partial class YQTaskButton : BaseLeftInnerButton
    {
        public YQTaskInfo TaskInfo { get; set; } = YQTaskInfo.Empty;
        public YQTaskButton(YQTaskInfo task) : base(task.TaskName)
        {
            TaskInfo = task;
            InitializeComponent();
            InitButtonMenu();
            InitButtonType();
            InitButtonDoubleClick();
            InitTaskInfo(TaskInfo);
        }

        private void InitButtonDoubleClick()
        {
            this.noFocusButton.MouseDown += new MouseEventHandler(this.NoFocusButton_MouseDown);
        }
        private void InitButtonType()
        {
            this.leftPictureBox.Image = global::C2.Properties.Resources.侦察兵左侧;
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

        private void InitTaskInfo(YQTaskInfo task)
        {
            this.TaskInfo = task;
            this.ButtonText = TaskInfo.TaskName;
            this.toolTip.SetToolTip(this.rightPictureBox, TaskInfo.ResultFilePath);
            this.toolTip.SetToolTip(this.leftPictureBox, task.TaskModel);
            this.toolTip.SetToolTip(this.noFocusButton, TaskInfo.TaskName);
        }

        private void ResultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTaskInfoDialog();
        }

        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!TaskInfo.IsOverTime() && TaskInfo.Status != YQTaskStatus.Done)
            {
                HelpUtil.ShowMessageBox("当前任务正在执行中，无法删除。");
                return;
            }

            DialogResult rs = MessageBox.Show(
                String.Format("删除任务 {0}及结果文件, 继续删除请点击 \"确定\"", ButtonText),
                "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (rs != DialogResult.OK)
                return;

            Global.GetWebsiteFeatureDetectionControl().RemoveButton(this);
            FileUtil.DeleteFile(this.TaskInfo.ResultFilePath);
            
        }

        private void NoFocusButton_MouseDown(object sender, MouseEventArgs e)
        {   // 双击打开
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
                ShowTaskInfoDialog();
        }

        private void ShowTaskInfoDialog()
        {
            var dialog = new YQTaskResult(TaskInfo);
            if (dialog.ShowDialog() == DialogResult.OK)
                return;
        }
    }
}
