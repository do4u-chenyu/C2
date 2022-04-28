using C2.Business.WebsiteFeatureDetection;
using C2.Controls;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Dialogs.WebsiteFeatureDetection
{
    partial class AddYQTask : StandardDialog
    {
        public YQTaskInfo TaskInfo { set; get; }
        string TaskName { get => this.taskNameTextBox.Text; set => this.taskNameTextBox.Text = value; }
        string TaskModelName { get => this.taskModelComboBox.Text; set => this.taskModelComboBox.Text = value; }
        string FilePath { get => this.filePathTextBox.Text; set => this.filePathTextBox.Text = value; }
        readonly int maxRow;
        public AddYQTask()
        {
            InitializeComponent();
            FilePath = string.Empty;
            maxRow = 100;
            InitTaskName();
        }

        private void InitTaskName()
        {
            taskModelComboBox.SelectedIndex = 0;
            TaskName = String.Format("{0}任务{1}", TaskModelName, DateTime.Now.ToString("MMdd"));
            this.OKButton.Size = new System.Drawing.Size(75, 27);
            this.CancelBtn.Size = new System.Drawing.Size(75, 27);
        }

        private void TaskModelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.TaskName = String.Format("{0}任务{1}", this.TaskModelName, DateTime.Now.ToString("MMdd"));
        }
        private void PasteModeCB_CheckedChanged(object sender, EventArgs e)
        {
            this.wsTextBox.Clear();
            this.filePathTextBox.Clear();

            this.browserButton.Enabled = !this.pasteModeCB.Checked;

            this.wsTextBox.Enabled = this.pasteModeCB.Checked;
            this.wsTextBox.ReadOnly = !this.pasteModeCB.Checked;
        }

        private void BrowserButton_Click(object sender, EventArgs e)
        {

            OpenFileDialog OpenFileDialog = new OpenFileDialog
            {
                Filter = "文件 | *.txt",
                FileName = FilePath
            };
            if (OpenFileDialog.ShowDialog() != DialogResult.OK)
                return;

            this.FilePath = OpenFileDialog.FileName;
        }

        protected override bool OnOKButtonClick()
        {
            bool genTask = this.pasteModeCB.Checked ? GenTasksFromPaste() : GenTasksFromFile();
            if (genTask == false || base.OnOKButtonClick() == false)
                return false;
            HelpUtil.ShowMessageBox("任务下发成功");
            string destDirectory = Path.Combine(Global.UserWorkspacePath, "侦察兵", "舆情侦察兵");
            string destFilePath = Path.Combine(destDirectory, string.Format("{0}_{1}.txt", TaskName, "123"));
            FileUtil.CreateDirectory(destDirectory);
            using (File.Create(destFilePath)) { }
            TaskInfo = new YQTaskInfo(TaskName,"123",TaskModelName, FilePath, destFilePath, YQTaskStatus.Null);
            return true;
        }

        private bool GenTasksFromPaste()
        {
            if (this.wsTextBox.Text.Trim().IsEmpty())
                return false;
            // 如果粘贴文件不合格,就别清空旧数据了

            string[] lines = this.wsTextBox.Text.SplitLine();
            for (int i = 0; i < Math.Min(lines.Length, maxRow); i++)
                AddTasksByLine(lines[i]);

            return true;
        }

        private bool GenTasksFromFile()
        {

            if (!File.Exists(FilePath))
            {
                HelpUtil.ShowMessageBox("该数据文件不存在");
                return false;
            }
            try
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                    for (int row = 0; row < maxRow && !sr.EndOfStream; row++)
                        AddTasksByLine(sr.ReadLine().Trim());
            }
            catch
            {
                HelpUtil.ShowMessageBox(FilePath + ",文件加载出错，请检查文件内容。");
                return false;
            }
            return true;
        }


        private void AddTasksByLine(string line)
        {
            return;
        }

        
    }
}
