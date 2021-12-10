using C2.Controls;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool
{
    partial class AddAllWebShellForm : StandardDialog
    {
        readonly int maxRow;
        //string pattern;
        string filePath { get => this.filePathTextBox.Text; set => this.filePathTextBox.Text = value; }
        public List<WebShellTaskConfig> Tasks;

        public AddAllWebShellForm()
        {
            InitializeComponent();
            maxRow = 100000;
            filePath = string.Empty;
            Tasks = new List<WebShellTaskConfig>();
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
                FileName = filePath
            };
            if (OpenFileDialog.ShowDialog() != DialogResult.OK)
                return;

            this.filePath = OpenFileDialog.FileName;
        }
        protected override bool OnOKButtonClick()
        {
            return (this.pasteModeCB.Checked ? GenTasksFromPaste() : GetTasksFromFile()) && base.OnOKButtonClick();
        }

        private bool GenTasksFromPaste()
        {
            if (this.wsTextBox.Text.Trim().IsEmpty())
                return false;
            // 如果粘贴文件不合格,就别清空旧数据了
            Tasks.Clear();

            string[] lines = this.wsTextBox.Text.SplitLine();
            for(int i = 0; i < Math.Min(lines.Length, maxRow); i++)
                AddTasksByLine(lines[i]);
  
            return true;
        }


        private void AddTasksByLine(string line)
        {
            string[] contentArray = Regex.Split(line.Trim(new char[] { '\r', '\n' }), @"\s+");
   
            if (contentArray.Length < 2 || contentArray[0].IsNullOrEmpty() || contentArray[1].IsNullOrEmpty())
                return;

            Tasks.Add(new WebShellTaskConfig(ST.NowString(),
                                             string.Empty,
                                             contentArray[0],
                                             contentArray[1],
                                             WebShellTaskConfig.AutoDetectTrojanType(contentArray[0]),
                                             string.Empty,
                                             WebShellTaskConfig.AutoDetectClientType(contentArray[0], ClientSetting.WSDict.Keys.First()),
                                             string.Empty,
                                             string.Empty,
                                             string.Empty,
                                             string.Empty,
                                             string.Empty));
        }

        private bool GetTasksFromFile()
        {
            Tasks.Clear();

            if (!File.Exists(filePath))
            {
                HelpUtil.ShowMessageBox("该数据文件不存在");
                return false;
            }

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                for (int row = 0; row < maxRow && !sr.EndOfStream; row++)
                    AddTasksByLine(sr.ReadLine());
            }
            catch
            {
                HelpUtil.ShowMessageBox(filePath + ",文件加载出错，请检查文件内容。");
                return false;
            }
            return true;
        }
    }
}
