using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C2.Dialogs.CastleBravo;
using C2.Business.CastleBravo;
using System.IO;
using C2.Core;
using System.Xml;
using C2.Business.Model;
using C2.Utils;

namespace C2.Controls.C1.Left
{
    public partial class CastleBravoControl : BaseLeftInnerPanel
    {
        public CastleBravoControl()
        {
            InitializeComponent();
            this.addTaskLabel.Click += new EventHandler(this.AddLabel_Click);
        }

        private void AddLabel_Click(object sender, EventArgs e)
        {
            var dialog = new AddCBTask();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //添加按钮并持久化到本地
                AddInnerButton(new CastleBravoButton(dialog.TaskInfo));
                SaveWFDTasksToXml();
            }
        }

        public void SaveWFDTasksToXml()
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
               .Write("taskCreateTime", taskInfo.TaskCreateTime)
               .Write("datasourceFilePath", taskInfo.DatasourceFilePath)
               .Write("resultFilePath", taskInfo.ResultFilePath)
               .Write("status", taskInfo.Status);
        }

        public void LoadXmlToWFDTasks(string xmlDirectory)
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
                HelpUtil.ShowMessageBox("侦察兵任务加载时发生错误:" + ex.Message);
            }

            foreach (XmlNode xn in xDoc.SelectNodes(@"CBTasks/task")) //要学着尽量利用XPath自己的能力
                LoadSingleTask(xn);

            return;
        }

        private void LoadSingleTask(XmlNode xn)
        {
            try
            {
                CastleBravoTaskInfo taskInfo = new CastleBravoTaskInfo
                {
                    TaskName = xn.SelectSingleNode("taskName").InnerText,
                    TaskID = xn.SelectSingleNode("taskId").InnerText,
                    TaskCreateTime = xn.SelectSingleNode("taskCreateTime").InnerText,
                    DatasourceFilePath = xn.SelectSingleNode("datasourceFilePath").InnerText,
                    ResultFilePath = xn.SelectSingleNode("resultFilePath").InnerText,
                    Status = CastleBravoTaskStatusEnum(xn.SelectSingleNode("status").InnerText)
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
                    Size = new System.Drawing.Size(196, 22),
                    Text = "删除任务",
                    ToolTipText = "删除任务,同时删除本地文件"
                };
                RemoveToolStripMenuItem.Click += new EventHandler(RemoveToolStripMenuItem_Click);

                ToolStripMenuItem OpenDatasourceToolStripMenuItem = new ToolStripMenuItem
                {
                    Name = "OpenDatasourceToolStripMenuItem",
                    Size = new System.Drawing.Size(196, 22),
                    Text = "打开源文件",
                    ToolTipText = "预览输入的URL列表文件"
                };
                OpenDatasourceToolStripMenuItem.Click += new EventHandler(OpenDatasourceToolStripMenuItem_Click);

                ToolStripMenuItem ResultToolStripMenuItem = new ToolStripMenuItem
                {
                    Name = "ResultToolStripMenuItem",
                    Size = new System.Drawing.Size(196, 22),
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
                //删除前先判断，非done状态时，不支持删除
                if (TaskInfo.Status == CastleBravoTaskStatus.Running)
                {
                    HelpUtil.ShowMessageBox("当前任务正在执行中，无法删除。");
                    return;
                }

                DialogResult rs = MessageBox.Show(
                    String.Format("删除任务 {0}及结果文件, 继续删除请点击 \"确定\"", ButtonText),
                    "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (rs != DialogResult.OK)
                    return;

                Global.GetCastleBravoControl().RemoveButton(this);
                FileUtil.DeleteFile(this.TaskInfo.ResultFilePath);
                Global.GetCastleBravoControl().SaveWFDTasksToXml();//先删除后持久化
            }
            private void OpenDatasourceToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (File.Exists(this.TaskInfo.DatasourceFilePath))
                    ProcessUtil.ProcessOpen(this.TaskInfo.DatasourceFilePath);
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

        private void CastleBravoControl_Load(object sender, EventArgs e)
        {
            //初次加载时加载本地文件内容到button
            LoadXmlToWFDTasks(Path.Combine(Global.WorkspaceDirectory, Global.GetUsername()));
        }
    }
}
