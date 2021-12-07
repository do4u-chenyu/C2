using C2.Business.CastleBravo;
using C2.Business.Model;
using C2.Core;
using C2.Dialogs.CastleBravo;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace C2.Controls.C1.Left
{
    public partial class CastleBravoControl : BaseLeftInnerPanel
    {
        public CastleBravoControl()
        {
            InitializeComponent();
            CastleBravoControlLoad(); // 取消延迟加载, 避免visible时的闪烁
            this.addTaskLabel.Click += new EventHandler(this.AddLabel_Click);
        }

        private void AddLabel_Click(object sender, EventArgs e)
        {
            var dialog = new AddCBTask();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //添加按钮并持久化到本地
                AddInnerButton(new CastleBravoButton(dialog.TaskInfo));
                Save();
            }
        }

        public void Save()
        {
            List<CastleBravoButton> buttons = FindControls<CastleBravoButton>();

            string xmlDirectory = Path.Combine(Global.UserWorkspacePath, "喝彩城堡");
            string xmlPath = Path.Combine(xmlDirectory, "CBTasks.xml");
            Directory.CreateDirectory(xmlDirectory);
            XmlDocument xDoc = new XmlDocument();
            XmlElement rootElement = xDoc.CreateElement("CBTasks");
            xDoc.AppendChild(rootElement);

            foreach (CastleBravoButton button in buttons)
                SaveSingleTask(button.TaskInfo, rootElement);

            // 保存时覆盖原文件
            xDoc.Save(xmlPath);
        }


        private void SaveSingleTask(CastleBravoTaskInfo taskInfo, XmlNode node)
        {
            new ModelXmlWriter("task", node)
               .Write("taskName", taskInfo.TaskName)
               .Write("taskId", taskInfo.TaskID)
               .Write("taskCount", taskInfo.TaskCount)
               .Write("taskCreateTime", taskInfo.TaskCreateTime)
               .Write("datasourceFilePath", taskInfo.MD5FilePath)
               .Write("resultFilePath", taskInfo.ResultFilePath)
               .Write("status", taskInfo.Status);
        }

        private void LoadTasks(string xmlDirectory)
        {
            string xmlPath = Path.Combine(xmlDirectory, "喝彩城堡", "CBTasks.xml");
            if (!File.Exists(xmlPath))
                return;

            XmlDocument xDoc = new XmlDocument(); ;
            try
            {
                xDoc.Load(xmlPath);
            }
            catch (Exception ex)
            {
                HelpUtil.ShowMessageBox("任务加载时发生错误:" + ex.Message);
            }

            foreach (XmlNode xn in xDoc.SelectNodes(@"CBTasks/task")) 
                LoadSingleTask(xn);

            return;
        }

        private void LoadSingleTask(XmlNode xn)
        {
            try
            {
                CastleBravoTaskInfo taskInfo = new CastleBravoTaskInfo
                {
                    TaskID = XmlUtil.Read(xn, "taskId"),
                    TaskName = XmlUtil.Read(xn, "taskName"),
                    TaskCount = XmlUtil.ReadDefault(xn, "taskCount", "0"),
                    TaskCreateTime = XmlUtil.Read(xn, "taskCreateTime"),
                    MD5FilePath = XmlUtil.Read(xn, "datasourceFilePath"),
                    ResultFilePath = XmlUtil.Read(xn, "resultFilePath"),
                    Status = CastleBravoTaskStatusEnum(XmlUtil.Read(xn, "status"))
                };

                AddInnerButton(new CastleBravoButton(taskInfo));
            }
            catch { }
        }

        private CastleBravoTaskStatus CastleBravoTaskStatusEnum(string encoding, CastleBravoTaskStatus defaultStatus = CastleBravoTaskStatus.Null)
        {
            if (!Enum.TryParse(encoding, true, out CastleBravoTaskStatus outStatus))
                return defaultStatus;
            return outStatus;
        }

        private class CastleBravoButton : BaseLeftInnerButton
        {
            public CastleBravoTaskInfo TaskInfo { get; set; } = CastleBravoTaskInfo.Empty;

            public CastleBravoButton()
            {
                InitButtonMenu();
                InitButtonType();
                InitButtonDoubleClick();
            }
            public CastleBravoButton(CastleBravoTaskInfo taskInfo) : this()
            {
                TaskInfo = taskInfo;
                this.ButtonText = taskInfo.TaskName;
                this.toolTip.SetToolTip(this.rightPictureBox, TaskInfo.ResultFilePath);
                this.toolTip.SetToolTip(this.noFocusButton, TaskInfo.TaskName);
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

                ToolStripMenuItem OpenDatasourceToolStripMenuItem = new ToolStripMenuItem
                {
                    Name = "OpenDatasourceToolStripMenuItem",
                    Size = new Size(196, 22),
                    Text = "打开源文件",
                    ToolTipText = "预览输入的URL列表文件"
                };
                OpenDatasourceToolStripMenuItem.Click += new EventHandler(OpenDatasourceToolStripMenuItem_Click);

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
                    RemoveToolStripMenuItem,
                    new ToolStripSeparator(),
                    OpenDatasourceToolStripMenuItem
                 });

            }
            private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
            {
                String message = String.Format("删除任务 {0} 及结果文件, 继续删除请点击 \"确定\"", ButtonText);
                if (TaskInfo.Status == CastleBravoTaskStatus.Running)
                    message = "当前任务正在执行中，" + message;

                DialogResult rs = MessageBox.Show(message, "删除", 
                    MessageBoxButtons.OKCancel, 
                    MessageBoxIcon.Information);

                if (rs != DialogResult.OK)
                    return;

                Global.GetCastleBravoControl().RemoveButton(this);
                FileUtil.DeleteFile(this.TaskInfo.ResultFilePath);
                Global.GetCastleBravoControl().Save();//先删除后持久化
            }
            private void OpenDatasourceToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (File.Exists(this.TaskInfo.MD5FilePath))
                    ProcessUtil.ProcessOpen(this.TaskInfo.MD5FilePath);
                else
                    HelpUtil.ShowMessageBox("该文件已不存在.", "提示");
            }
            private void ResultToolStripMenuItem_Click(object sender, EventArgs e)
            {
                ShowDialogTaskInfo();
            }

            private void InitButtonDoubleClick()
            {
                this.noFocusButton.MouseDown += new MouseEventHandler(this.NoFocusButton_MouseDown);
            }
            private void NoFocusButton_MouseDown(object sender, MouseEventArgs e)
            {   // 双击打开
                if (e.Button == MouseButtons.Left && e.Clicks == 2)
                   ShowDialogTaskInfo();
            }

            private void ShowDialogTaskInfo()
            {
                var dialog = new CBTaskResult(TaskInfo);
                if (dialog.ShowDialog() == DialogResult.OK)
                    return;
            }
        }

        private void CastleBravoControlLoad()
        {
            LoadCBPlugins();
            ResizeCBLocation();
            //初次加载时加载本地文件内容到button
            LoadTasks(Path.Combine(Global.WorkspaceDirectory, Global.GetUsername()));
        }


        private void LoadCBPlugins()
        {
            List<string> CBPlugins = new List<string>() { 
                "Cracker", 
                "PwdGenerator", 
                "WebScan",
                /*"RobotsScan",*/ 
                "WebShell", 
                "Binary"};
            CBPlugins.ForEach(pname => this.AddCBPlugin(new PluginButton(pname)));
        }
        private void ResizeCBLocation()
        {
            backPanel.Location = new Point(backPanel.Location.X, ComputeSplitLineLocation());
            backPanel.Height = this.Height - ComputeSplitLineLocation() + 95;
        }

        private void AddCBPlugin(PluginButton plugin)
        {
            plugin.Location = new Point(20, ComputeSplitLineLocation());
            this.Controls.Add(plugin);
        }

        private int ComputeSplitLineLocation()
        {
            return this.titleLabel.Height + this.Controls.Find("BaseLeftInnerButton", false).Length * 40 + 10;
        }

        private void HelpInfoLable_Click(object sender, EventArgs e)
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
