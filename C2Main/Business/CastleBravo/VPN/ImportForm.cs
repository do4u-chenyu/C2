using C2.Controls;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.VPN
{
    partial class ImportForm : StandardDialog
    {
        string FilePath { get => this.filePathTextBox.Text; set => this.filePathTextBox.Text = value; }
        public List<VPNTaskConfig> Tasks;

        public ImportForm()
        {
            InitializeComponent();
            InitializeOther();
        }

        private void InitializeOther()
        {
            FilePath = string.Empty;
            Tasks = new List<VPNTaskConfig>();

            this.OKButton.Size = new Size(75, 27);
            this.CancelBtn.Size = new Size(75, 27);
        }

        private void BrowserButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog = new OpenFileDialog
            {
                Filter = "C2 VPN 文件 | *.csv",
                FileName = FilePath
            };
            if (OpenFileDialog.ShowDialog() != DialogResult.OK)
                return;

            this.FilePath = OpenFileDialog.FileName;
        }

        protected override bool OnOKButtonClick()
        {
            return GenTasksFromFile() && base.OnOKButtonClick();
        }

        private void AddTasksByLine(string line)
        {
            string[] columns = line.Split('\t');

            if (columns.Length < 13)
                return;

            string createTime = columns[VPNMainForm.CI_创建时间];
            string remarks    = columns[VPNMainForm.CI_备注];
            string host       = columns[VPNMainForm.CI_主机地址];
            string port       = columns[VPNMainForm.CI_端口];
            string password   = columns[VPNMainForm.CI_密码];
            string method     = columns[VPNMainForm.CI_加密算法];
            string status     = columns[VPNMainForm.CI_状态];
            string version    = columns[VPNMainForm.CI_客户端];
            string probeInfo  = columns[VPNMainForm.CI_探测信息];
            string other      = columns[VPNMainForm.CI_其他信息];
            string ip         = columns[VPNMainForm.CI_IP地址];
            string country    = columns[VPNMainForm.CI_归属地];
            string ss         = columns[VPNMainForm.CI_梯子地址];
            string rss        = columns.Length > 13 ? columns[VPNMainForm.CI_订阅地址] : string.Empty;

            Tasks.Add(new VPNTaskConfig(createTime,
                remarks,
                host,
                NetUtil.IsPort(port) ? port : string.Empty,
                password,
                method,
                status,
                version,
                probeInfo,
                other,
                NetUtil.IsIPAddress(ip) ? ip : string.Empty,
                country,
                ss,
                rss));
        }

        private bool GenTasksFromFile()
        {
            Tasks.Clear();

            if (!File.Exists(FilePath))
            {
                HelpUtil.ShowMessageBox("该数据文件不存在");
                return false;
            }
            try
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                {
                    List<string> lines = new List<string>();
                    while(!sr.EndOfStream)
                        lines.Add(sr.ReadLine().Trim());

                    foreach (string line in lines)
                        AddTasksByLine(line);
                }

            }
            catch
            {
                HelpUtil.ShowMessageBox(FilePath + ",文件加载出错，请检查文件内容。");
                return false;
            }
            return true;
        }
    }
}
