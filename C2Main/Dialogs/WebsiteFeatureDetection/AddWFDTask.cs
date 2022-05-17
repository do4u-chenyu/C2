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
            TaskName = String.Format("网站侦察兵{0}", DateTime.Now.ToString("MMdd"));
            this.OKButton.Size = new System.Drawing.Size(75, 27);
            this.CancelBtn.Size = new System.Drawing.Size(75, 27);
        }


        protected override bool OnOKButtonClick()
        {
            TaskName = TaskName.Trim();//去掉首尾空白符

            if (this.pasteModeCB.Checked)
            {
                if (this.md5TextBox.Text.Trim().IsEmpty())
                    return false;
                GenPasteWFDFile();
            }

            if (!IsValidityTaskName() || !IsValidityFilePath())
                return false;

            List<string> urls = GetUrlsFromFile(FilePath);
            if (urls.Count == 0)
                return false;

            WFDAPIResult result = new WFDAPIResult();
            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
            {
                if (!WFDWebAPI.GetInstance().StartTask(urls, out result))
                    return false;
            }

            //任务下发，修改desc字段信息

            /*
            if (result.RespMsg != "success")
            {
                HelpUtil.ShowMessageBox(result.RespMsg);
                return false;
            }*/

            if (result.RespMsg.Contains("Your quota for this type of task is full"))
            {
                /*
                 * {'time': _time, 'version': version, 'attention': 'Your quota for this type of task is full'}
                   {'时间':_time,'版本':version,'注意':'任务的配额已满，请等已经下发任务完成后再下发！'}   
                 */
                HelpUtil.ShowMessageBox(result.RespMsg.Replace("time", "时间").Replace("version", "版本").Replace("attention", "注意").Replace("our quota for this type of task is full", "任务的配额已满，请等已经下发任务完成后再下发"));
                return false;
            }
            else if (result.RespMsg.Contains("please use your own token"))
            {
                /*
                 * {'time': _time, 'version': version,  'attention': 'please use your own token!'}
                 * {'时间':_time,'版本':version,'注意':'请使用账号对应的token!'}
                 */
                HelpUtil.ShowMessageBox(result.RespMsg.Replace("time", "时间").Replace("version", "版本").Replace("attention", "注意").Replace("please use your own token", "请使用账号对应的token"));
                return false;
            }
            else if (result.RespMsg.Contains("Please enter the correct user_id"))
            {
                /*
                 * {'time': _time, 'version': version, 'attention': 'Please enter the correct user_id'}
                 * {'时间':_time,'版本':version,'注意':'请输入正确的工号！'}
                 */
                HelpUtil.ShowMessageBox(result.RespMsg.Replace("time", "时间").Replace("version", "版本").Replace("attention", "注意").Replace("Please enter the correct user_id", "请输入正确的工号"));
                return false;
            }
            else if (result.RespMsg.Contains("Please enter the correct user_id"))
            {
                /*
                 * {'time': _time, 'version': version,'attention': 'Please make sure post urllist is json type.!'}
                 * {'时间':_time,'版本':version,'注意':'请确认输入是否为json格式!'}
                 */
                HelpUtil.ShowMessageBox(result.RespMsg.Replace("time", "时间").Replace("version", "版本").Replace("attention", "注意").Replace("Please make sure post urllist is json type", "请确认输入是否为json格式"));
                return false;
            }
            else if (result.RespMsg.Contains("User should post at least one url"))
            {
                /*
                 * {'time': _time, 'version': version,'attention': 'User should post at least one url.'}
                 * {'时间':_time,'版本':version,'注意':'请输入至少一个url.'}
                 */
                HelpUtil.ShowMessageBox(result.RespMsg.Replace("time", "时间").Replace("version", "版本").Replace("attention", "注意").Replace("User should post at least one url", "请输入至少一个url"));
                return false;
            }
            else if (result.RespMsg.Contains("URLs are illega"))
            {
                /*
                 * {'time': _time, 'version': version,'attention': 'URLs are illega'}
                 * {'时间':_time,'版本':version,'注意':'url全部格式非法'}
                 */
                HelpUtil.ShowMessageBox(result.RespMsg.Replace("time", "时间").Replace("version", "版本").Replace("attention", "注意").Replace("URLs are illega", "url全部格式非法"));
                return false;
            }
            else if (result.RespMsg.Contains("Failed to calculate task quantity"))
            {
                /*
                 * {'time': _time, 'version': version,'attention': 'Failed to calculate task quantity'}
                 * {'时间':_time,'版本':version,'注意':'计算任务数量失败，请重试'}
                 */
                HelpUtil.ShowMessageBox(result.RespMsg.Replace("time", "时间").Replace("version", "版本").Replace("attention", "注意").Replace("Failed to calculate task quantity", "计算任务数量失败，请重试"));
                return false;
            }
            else if (result.RespMsg.Contains("Warehousing error"))
            {
                /*
                 * {'time': _time, 'version': version, 'attention': 'Warehousing error'}
                 * {'时间':_time,'版本':version,'注意':'入库错误，请重试'}
                 */
                HelpUtil.ShowMessageBox(result.RespMsg.Replace("time", "时间").Replace("version", "版本").Replace("attention", "注意").Replace("Warehousing error", "入库错误，请重试"));
                return false;
            }
            else if (result.RespMsg.Contains("tasks_in_pool") && result.RespMsg.Contains("shortuuid"))
            {
                /*
                 * {'time':_time,'version':version,'tasks_in_pool':work_task+1,'shortuuid':short_id,'attention':desc}
                 * {'时间':_time,'版本':version,'任务池任务数量':work_task+1,'任务id':short_id,'注意':desc}
                 */
                HelpUtil.ShowMessageBox(result.RespMsg.Replace("time", "时间").Replace("version", "版本").Replace("attention", "注意").Replace("tasks_in_pool", "任务池任务数量").Replace("shortuuid", "任务id"));
                return false;
            }
            else if (result.RespMsg.Contains("tasks_in_pool") && result.RespMsg.Contains("try_again"))
            {
                /*
                 * {'time': _time, 'version': version,'tasks_in_pool':work_task, 'try_again': again_desc,               'attention':'task pool is full'}
                 * {'时间':_time,'版本':version,     '任务池任务数量':work_task, '预计再次下发时间（分钟）':again_desc,  '注意':'任务池任务已满，请在预计再次下发时间后下发'}
                 */
                HelpUtil.ShowMessageBox(result.RespMsg.Replace("time", "时间").Replace("version", "版本").Replace("attention", "注意").Replace("tasks_in_pool", "任务池任务数量").Replace("try_again", "预计再次下发时间（分钟）").Replace("task pool is full", "任务池任务已满，请在预计再次下发时间后下发"));
                return false;
            }
            new Log.Log().LogManualButton("网站侦察兵", "02");

            HelpUtil.ShowMessageBox("任务下发成功");
            string destDirectory = Path.Combine(Global.UserWorkspacePath, "侦察兵", "网站侦察兵");
            string destFilePath = Path.Combine(destDirectory, string.Format("{0}_{1}.txt", TaskName, result.Datas));
            FileUtil.CreateDirectory(destDirectory);
            using (File.Create(destFilePath)) { }

            TaskInfo = new WFDTaskInfo(TaskName, result.Datas, FilePath, destFilePath, WFDTaskStatus.Null);

            return base.OnOKButtonClick();
        }

        private void GenPasteWFDFile()
        {
            FileUtil.FileWriteToEnd(FilePath, this.md5TextBox.Text);  
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
            FileStream fs = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
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
                HelpUtil.ShowMessageBox(filePath + "文件加载出错，请检查文件内容。");
            }
            finally
            {
                if (fs != null)
                    fs.Close();
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
    }
}
