using C2.Business.WebsiteFeatureDetection;
using C2.Controls;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace C2.Dialogs.WebsiteFeatureDetection
{
    partial class AddWFDTask : StandardDialog
    {
        public WFDTaskInfo TaskInfo { set; get; }
        string TaskName { get => this.taskNameTextBox.Text; set => this.taskNameTextBox.Text = value; }
        string FilePath { get => this.filePathTextBox.Text; set => this.filePathTextBox.Text = value; }
        public AddWFDTask()
        {
            InitializeComponent();
            InitTaskName();
        }

        private void InitTaskName()
        {
            TaskName = String.Format("网络侦察兵{0}", DateTime.Now.ToString("MMdd"));
        }


        protected override bool OnOKButtonClick()
        {
            TaskName = TaskName.Trim();//去掉首尾空白符
            if (!IsValidityTaskName() || !IsValidityFilePath())
                return false;

            WFDAPIResult result = new WFDAPIResult();
            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
            {
                if (!WFDWebAPI.GetInstance().StartTask(GetUrlsFromFile(FilePath), out result))
                    return false;
            }

            if (result.RespMsg != "success")
            {
                HelpUtil.ShowMessageBox(result.RespMsg);
                return false;
            }

            HelpUtil.ShowMessageBox("任务下发成功");
            string destDirectory = Path.Combine(Global.UserWorkspacePath, "侦察兵", "网络侦察兵");
            string destFilePath = Path.Combine(destDirectory, string.Format("{0}_{1}.bcp", TaskName, result.Datas));
            FileUtil.CreateDirectory(destDirectory);
            using (File.Create(destFilePath)) { }

            TaskInfo = new WFDTaskInfo(TaskName, result.Datas, FilePath, destFilePath, WFDTaskStatus.Null);

            return base.OnOKButtonClick();
        }
        private List<string> GetUrlsFromFile(string filePath)
        {
            int maxRow = 10000;

            List<string> urls = new List<string>();
            if (!File.Exists(filePath))
            {
                HelpUtil.ShowMessageBox("该数据文件不存在");
                return urls;
            }

            StreamReader sr = null;
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs, Encoding.Default);

                //判断是否存在表头
                string firstLine = sr.ReadLine().Trim(new char[] { '\r', '\n', '\t' });
                string Pattern = @"^((http|https|ftp)\://)?[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&$%\$#\=~])*$";
                if (new Regex(Pattern).Match(firstLine).Success)
                    urls.Add(firstLine);

                for (int row = 1; row < maxRow && !sr.EndOfStream; row++)
                    urls.Add(sr.ReadLine().Trim(new char[] { '\r', '\n', '\t' }));
            }
            catch
            {
                HelpUtil.ShowMessageBox(filePath + "文件加载出错");
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
            return urls;
        }

        private bool IsValidityTaskName()
        {
            if(string.IsNullOrEmpty(TaskName))
            {
                HelpUtil.ShowMessageBox("任务名不能为空");
                return false;
            }

            if(FileUtil.IsContainIllegalCharacters(TaskName, "任务名") || FileUtil.NameTooLong(TaskName, "任务名"))
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

        private void BrowserButton_Click(object sender, EventArgs e)
        {
            this.FilePath = ofd.ShowDialog() == DialogResult.OK ? ofd.FileName : String.Empty;
        }
    }
}
