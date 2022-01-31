using C2.Business.CastleBravo;
using C2.Controls;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace C2.Dialogs.CastleBravo
{
    partial class AddCBTask : StandardDialog
    {
        public CastleBravoTaskInfo TaskInfo { set; get; }
        string TaskName { get => this.taskNameTextBox.Text; set => this.taskNameTextBox.Text = value; }
        string FilePath { get => this.filePathTextBox.Text; set => this.filePathTextBox.Text = value; }

        private static readonly int MaxRowNumber = 2000;   // 单任务最大处理数

        public AddCBTask()
        {
            InitializeComponent();
            InitTaskName();
        }

        private void BrowserButton_Click(object sender, EventArgs e)
        {
            this.FilePath = ofd.ShowDialog() == DialogResult.OK ? ofd.FileName : String.Empty;
        }

        private void InitTaskName()
        {
            TaskName = String.Format("任务_{0}", DateTime.Now.ToString("MMddhhmm"));
        }

        protected override bool OnOKButtonClick()
        {
            TaskName = TaskName.Trim();//去掉首尾空白符

            if (this.pasteModeCB.Checked)
            {
                if (this.md5TextBox.Text.Trim().IsEmpty())
                    return false;
                GenPasteCBFile();
            }

            if (!IsValidityTaskName() || !IsValidityFilePath())
                return false;

            List<string> md5List = GetUrlsFromFile(FilePath);
            if (md5List.Count == 0)
                return false;

            CastleBravoAPIResponse result = new CastleBravoAPIResponse();
            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
            {
                if (!CastleBravoAPI.GetInstance().StartTask(md5List, out result))
                    return false;
            }

            if (result.StatusCode != HttpStatusCode.OK)
            {
                HelpUtil.ShowMessageBox(result.Message);
                return false;
            }

            HelpUtil.ShowMessageBox(result.Message);

            string destDirectory = Path.Combine(Global.UserWorkspacePath, "喝彩城堡");
            string destFilePath = Path.Combine(destDirectory, string.Format("{0}_{1}.txt", TaskName, result.Data));
            FileUtil.CreateDirectory(destDirectory);
            using (File.Create(destFilePath)) { }

            TaskInfo = new CastleBravoTaskInfo(md5List.Count.ToString(), 
                                               TaskName, 
                                               result.Data, 
                                               FilePath,
                                               destFilePath, 
                                               CastleBravoTaskStatus.Null);

            return base.OnOKButtonClick();
        }

        private void GenPasteCBFile()
        {
            FileUtil.FileWriteToEnd(FilePath, this.md5TextBox.Text);
        }
        private List<string> GetUrlsFromFile(string filePath)
        {

            List<string> md5List = new List<string>();
            if (!File.Exists(filePath))
            {
                HelpUtil.ShowMessageBox("该数据文件不存在");
                return md5List;
            }

            StreamReader sr = null;
            FileStream fs = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs, Encoding.Default);
                for (int row = 0; row < MaxRowNumber && !sr.EndOfStream; row++)
                    md5List.Add(sr.ReadLine().Trim());
            }
            catch
            {
                HelpUtil.ShowMessageBox(filePath + "文件加载出错，请检查文件内容。");
            }
            finally
            {
                if (fs != null)
                    fs.Close();
                if (sr != null)
                    sr.Close();
            }
            return md5List;
        }

        private bool IsValidityTaskName()
        {
            if (string.IsNullOrEmpty(TaskName))
            {
                HelpUtil.ShowMessageBox("任务名不能为空");
                return false;
            }

            if (FileUtil.IsContainIllegalCharacters(TaskName, "任务名") || FileUtil.NameTooLong(TaskName, "任务名"))
            {
                return false;
            }

            return true;
        }

        private bool IsValidityFilePath()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                HelpUtil.ShowMessageBox("查询文件路径不能为空，请点击预览选择文件。");
                return false;
            }

            return true;
        }

        private void AddCBTask_Load(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset(int i = 0)
        {
            this.taskComboBox.SelectedIndex = i;
            this.modeComboBox.SelectedIndex = 0;
            this.modeComboBox.Visible = this.taskComboBox.SelectedIndex == 0;
            this.label3.Visible = this.taskComboBox.SelectedIndex == 0;
            this.browserButton.Visible = this.taskComboBox.SelectedIndex == 0;
            this.filePathTextBox.ReadOnly = this.taskComboBox.SelectedIndex != 0;

            this.OKButton.Size = new System.Drawing.Size(75, 27);
            this.CancelBtn.Size = new System.Drawing.Size(75, 27);
        }

        private void TaskComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Reset(this.taskComboBox.SelectedIndex);
        }

        private void PasteModeCB_CheckedChanged(object sender, EventArgs e)
        {
            this.md5TextBox.Clear();
            this.filePathTextBox.Clear();

            this.md5TextBox.ReadOnly      = !this.pasteModeCB.Checked;
            this.browserButton.Enabled    = !this.pasteModeCB.Checked;
            this.filePathTextBox.ReadOnly =  this.pasteModeCB.Checked;

            if (this.pasteModeCB.Checked)
                FilePath = Path.Combine(Global.TempDirectory, Guid.NewGuid().ToString("N") + ".txt");
            else
                FilePath = String.Empty;
        }

        private void HelpLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string helpfile = Path.Combine(Application.StartupPath, "Resources", "Help", "喝彩城堡帮助文档.txt");
                Help.ShowHelp(this, helpfile);
            }
            catch { };
        }
    }
}
