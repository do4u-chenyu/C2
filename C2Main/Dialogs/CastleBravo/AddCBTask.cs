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
        private static readonly int MaxSaltRowNumber = 5;  // Salt模式最大处理数

        public AddCBTask()
        {
            InitializeComponent();
            InitTaskName();
            InitializeDGV();
            InitializeSaltMode();
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
            int mode = this.taskComboBox.SelectedIndex;

            TaskName = TaskName.Trim(); //去掉首尾空白符
            if (!IsValidityTaskName())
                return false;

            if (mode == 0 && this.pasteModeCB.Checked)
            {
                if (this.md5TextBox.Text.Trim().IsEmpty())
                    return false;
                GenPasteCBFile();
            }

            if (mode == 0 && !IsValidityFilePath())
                return false;

            if (mode == 1 && !IsValidityDGV())
                return false;

            if (mode == 1 && IsValidityDGV())
                GenPasteDGVFile();

            List<string> md5List = mode == 0 ? GenMD5ListFromFile(FilePath) : GenMD5ListFromDGV();
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

            TaskInfo = new CastleBravoTaskInfo(md5List.Count - mode,      // 省if
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
        private void GenPasteDGVFile()
        {
            FileUtil.FileWriteToEnd(FilePath, string.Join(Environment.NewLine, GenMD5ListFromDGV()));
        }

        private List<string> GenMD5ListFromDGV()
        {
            List<string> ret = new List<string>
            {
                // 添加Salt模式的第一行控制信息
                // @@@  Mode    ###
                string.Format("@@@\t{0}\t###", GenSelectedModeString().Trim().ToLower())
            };

            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                string pass = ST.GetValue<string>(DGV.Rows[i].Cells[0].Value, string.Empty);
                string salt = ST.GetValue<string>(DGV.Rows[i].Cells[1].Value, string.Empty);
                string user = ST.GetValue<string>(DGV.Rows[i].Cells[2].Value, string.Empty);
                // 不符合条件的跳过
                string su = salt + user;
                if (pass.IsNullOrEmpty() || su.IsNullOrEmpty())
                    continue;
                
                ret.Add(string.Format("{0}\t{1}\t{2}", pass, salt, user));
            }
            return ret;
        }
        private List<string> GenMD5ListFromFile(string filePath)
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

        private bool IsValidityDGV()
        {
            bool empty = true;
            for (int i = 0; i < DGV.Rows.Count; i++)
                for (int j = 0; j < DGV.Rows[i].Cells.Count; j++)
                    empty = empty && DGV.Rows[i].Cells[j].Value == null;

            if (empty)
                HelpUtil.ShowMessageBox("列表为空,没有在列表中填写需要分析的MD5");
      
            return !empty;
        }

        private void AddCBTask_Load(object sender, EventArgs e)
        {
            Reset();
            this.OKButton.Size = new System.Drawing.Size(75, 27);
            this.CancelBtn.Size = new System.Drawing.Size(75, 27);
        }

        private void Reset(int i = 0)
        {
            this.taskComboBox.SelectedIndex = i;
            // Salt模式时, mode必须选一个, 模式04最常用
            if (i == 1 && this.modeComboBox.SelectedIndex < 0)
                this.modeComboBox.SelectedIndex = 3;
            // Salt模式时, 必须有一个临时文件
            if (i == 1 && FilePath.IsNullOrEmpty())
                FilePath = Path.Combine(Global.TempDirectory, Guid.NewGuid().ToString("N") + ".txt");

            // 常规MD5模式
            this.md5Label.Visible = i == 0;
            this.fileLabel.Visible = i == 0;
            this.md5TextBox.Visible = i == 0;
            this.browserButton.Visible = i == 0;
            this.filePathTextBox.Visible = i == 0;
            this.pasteModeCB.Visible = i == 0;
            this.label5.Visible = i == 0;
            this.label6.Visible = i == 0;
            // 加盐MD5模式
            this.label2.Visible = i == 1;
            this.label3.Visible = i == 1;
            this.label10.Visible = i == 1;
            this.label11.Visible = i == 1;
            this.DGV.Visible = i == 1;
            this.modeLabel.Visible = i == 1;
            this.saltLabel.Visible = i == 1;
            this.modeComboBox.Visible = i == 1;
        }

        private void InitializeDGV()
        {
            this.DGV.SetAutoScaleMode(AutoScaleMode.None);
            for (int i = 0; i < MaxSaltRowNumber; i++)
                this.DGV.Rows.Add();
        }

        private void InitializeSaltMode()
        {
            this.modeComboBox.Items.AddRange(new object[] {
                "模式01: MD5($Pass.$Salt)",
                "模式02: MD5($Salt.$Pass)",
                "模式03: MD5($Salt.$Pass.$Salt)",
                "模式04: MD5(MD5($Pass).$Salt)",
                "模式05: MD5($Salt.MD5($Pass))",
                "模式06: MD5(MD5($Pass.$Salt))",
                "模式07: MD5(MD5($Salt.$Pass))",
                "模式08: MD5(MD5($Pass).MD5($Salt))",
                "模式09: MD5(MD5($Salt).MD5($Pass))",
                "模式10: MD5($Salt.MD5($Salt.$Pass))",
                "模式11: MD5($Salt.MD5($Pass.$Salt))",
                "模式12: MD5(MD5($Salt.$Pass).$Salt)",
                "模式13: MD5(MD5($Pass.$Salt).$Salt)",
                "模式14: MD5($U.$Pass.$Salt)",
                "模式15: MD5($U.$Salt.$Pass)",
                "模式16: MD5($Salt.$U.$Pass)",
                "模式17: MD5($Salt.$Pass.$U)",
                "模式18: MD5($Pass.$Salt.$U)",
                "模式19: MD5($Pass.$U.$Salt)",
                "模式20: MD5($U.$Pass.MD5($Salt))",
                "模式21: MD5($U.MD5($Pass).$Salt)",
                "模式22: MD5(MD5($U).$Pass.$Salt)",
                "模式23: MD5(MD5($U.$Pass).$Salt)",
                "模式24: MD5(MD5($U.$Pass.$Salt))",
                "模式25: MD5(MD5($U.MD5($Pass)).$Salt)",
                "模式26: MD5(MD5($U).MD5($Pass).MD5($Salt))",
                "模式27: MD5(MD5($U).$Pass.MD5($Salt))",
                "模式28: MD5($U.MD5($Pass).MD5($Salt))",
                "模式29: MD5($U.MD5($Pass.$Salt))",
                "模式30: MD5(MD5($Salt).$Pass)",
                "模式31: MD5(SHA1($Pass))",
                "模式32: MD5(SHA256($Pass))",
                "模式33: MD5(SHA512($Pass))",
            });
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
                string helpfile = Path.Combine(Global.ResourcesPath, "Help", "喝彩城堡帮助文档.txt");
                Help.ShowHelp(this, helpfile);
            }
            catch { };
        }

        private void QueryButton_Click(object sender, EventArgs e)
        {
            string result = string.Empty;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(Global.CastleIDLEUrl);
            req.Method = "POST";
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            if(result.Contains("True"))
                HelpUtil.ShowMessageBox("远程服务器-彩虹表在忙", "查询结果");
            else
                HelpUtil.ShowMessageBox("远程服务器-彩虹表空闲, 欢迎使用", "查询结果");
        }

        private string GenSelectedModeString()
        {
            string selectString = ST.GetValue<string>(this.modeComboBox.SelectedItem, string.Empty);
            string[] s = selectString.Split(new char[] { OpUtil.Blank }, StringSplitOptions.RemoveEmptyEntries);
            if (s.Length < 2)
                return string.Empty;

            return s[1];
        }
    }
}
