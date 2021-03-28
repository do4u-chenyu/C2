﻿using C2.Business.Model;
using C2.Business.WebsiteFeatureDetection;
using C2.Core;
using C2.Dialogs.WebsiteFeatureDetection;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace C2.Controls.C1.Left
{
    public partial class WebsiteFeatureDetectionControl : BaseLeftInnerPanel
    {
        //Global.WFDUser持久化到文档中UserInformation.xml
        string WFDUser;

        public WebsiteFeatureDetectionControl()
        {
            InitializeComponent();
            this.addTaskLabel.Click += new EventHandler(this.AddLabel_Click);
        }

        private void AddLabel_Click(object sender, EventArgs e)
        {
            //判断用户是否认证？已认证的可以直接新建任务，否则先认证再新建任务
            if (string.IsNullOrEmpty(WFDUser))
            {
                var UAdialog = new UserAuth();
                if (UAdialog.ShowDialog() != DialogResult.OK)
                    return;

                WFDUser = UAdialog.UserName;
            }

            AddTask();
        }

        private void AddTask()
        {
            var dialog = new AddWFDTask();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //TODO phx 需要问一下这个分类接口，返回状态的时间，立即返回？还是查完才返回？

                List<string> urls = new List<string>() { "http://www.1.com", "http://www.1.com" };
                string taskId = WFDWebAPI.GetInstance().ClassifierUrls(urls);

                string destDirectory = Path.Combine(Global.UserWorkspacePath, "侦察兵", "网络侦察兵");
                string destFilePath = Path.Combine(destDirectory, string.Format("{0}_{1}.bcp", dialog.TaskName, taskId));
                FileUtil.CreateDirectory(destDirectory);
                using (File.Create(destFilePath)) { }

                //添加按钮并持久化到本地
                WFDTaskInfo taskInfo = new WFDTaskInfo(dialog.TaskName, taskId, dialog.FilePath, destFilePath, WFDTaskStatus.Null);
                AddInnerButton(new WebsiteFeatureDetectionButton(taskInfo));
                SaveWFDTasksToXml();
            }
        }

        #region 持久化保存/加载
        public void SaveWFDTasksToXml()
        {
            List<WebsiteFeatureDetectionButton> buttons = FindControls<WebsiteFeatureDetectionButton>();

            string xmlDirectory = Path.Combine(Global.UserWorkspacePath, "侦察兵");
            string xmlPath = Path.Combine(xmlDirectory, "WFDTasks.xml");
            Directory.CreateDirectory(xmlDirectory);
            XmlDocument xDoc = new XmlDocument();
            XmlElement rootElement = xDoc.CreateElement("WFDTasks");
            xDoc.AppendChild(rootElement);
            foreach (WebsiteFeatureDetectionButton button in buttons)
                SaveSingleTask(button.TaskInfo, xDoc);
            // 保存时覆盖原文件
            xDoc.Save(xmlPath);
        }

        private void SaveSingleTask(WFDTaskInfo taskInfo, XmlDocument xDoc)
        {
            XmlNode node = xDoc.SelectSingleNode("WFDTasks");
            ModelXmlWriter mxw = new ModelXmlWriter("task", node);
            mxw.Write("taskName", taskInfo.TaskName)
               .Write("taskId", taskInfo.TaskID)
               .Write("datasourceFilePath", taskInfo.DatasourceFilePath)
               .Write("resultFilePath", taskInfo.ResultFilePath)
               .Write("status", taskInfo.Status);
        }

        public void LoadXmlToWFDTasks(string xmlDirectory)
        {
            string xmlPath = Path.Combine(xmlDirectory, "侦察兵", "WFDTasks.xml");
            if (!File.Exists(xmlPath))
                return;

            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(xmlPath);
                XmlNode rootNode = xDoc.SelectSingleNode("WFDTasks");
                XmlNodeList nodeList = rootNode.SelectNodes("task");

                foreach (XmlNode xn in nodeList)
                    LoadSingleTask(xn);
            }
            catch (Exception ex)
            {
                HelpUtil.ShowMessageBox("WFDTasks 加载发生错误，错误 :" + ex.Message);
            }
            return;
        }

        private void LoadSingleTask(XmlNode xn)
        {
            WFDTaskInfo taskInfo = new WFDTaskInfo
            {
                TaskName = xn.SelectSingleNode("taskName").InnerText,
                TaskID = xn.SelectSingleNode("taskId").InnerText,
                DatasourceFilePath = xn.SelectSingleNode("datasourceFilePath").InnerText,
                ResultFilePath = xn.SelectSingleNode("resultFilePath").InnerText,
                Status = WFDTaskStatusEnum(xn.SelectSingleNode("status").InnerText)
            };

            AddInnerButton(new WebsiteFeatureDetectionButton(taskInfo));
        }

        private WFDTaskStatus WFDTaskStatusEnum(string encoding, WFDTaskStatus defaultStatus = WFDTaskStatus.Null)
        {
            if (!Enum.TryParse(encoding, true, out WFDTaskStatus outStatus))
                return defaultStatus;
            return outStatus;
        }
        #endregion

        #region WFD按钮类
        private class WebsiteFeatureDetectionButton : BaseLeftInnerButton
        {
            public WFDTaskInfo TaskInfo { get; set; } = WFDTaskInfo.Empty;

            public WebsiteFeatureDetectionButton()
            {
                InitButtonMenu();
                InitButtonType();
                InitButtonDoubleClick();
            }
            public WebsiteFeatureDetectionButton(WFDTaskInfo taskInfo) : this()
            {
                TaskInfo = taskInfo;
                this.ButtonText = taskInfo.TaskName;
                this.toolTip.SetToolTip(this.rightPictureBox, TaskInfo.ResultFilePath);
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
                    Name = "ReviewToolStripMenuItem",
                    Size = new System.Drawing.Size(196, 22),
                    Text = "删除任务",
                    ToolTipText = "删除任务,同时删除本地文件"
                };
                RemoveToolStripMenuItem.Click += new System.EventHandler(RemoveToolStripMenuItem_Click);

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
                DialogResult rs = MessageBox.Show(
                    String.Format("删除任务 {0}及结果文件, 继续删除请点击 \"确定\"", ButtonText),
                    "删除", MessageBoxButtons.OKCancel,MessageBoxIcon.Information);

                if (rs != DialogResult.OK)
                    return;

                FileUtil.DeleteFile(this.TaskInfo.ResultFilePath);
                Global.GetWebsiteFeatureDetectionControl().RemoveButton(this);
                Global.GetWebsiteFeatureDetectionControl().SaveWFDTasksToXml();
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

            private void NoFocusButton_MouseDown(object sender, MouseEventArgs e)
            {   // 双击打开
                if (e.Button == MouseButtons.Left && e.Clicks == 2)
                    ShowDialogTaskInfo();
            }

            private void ShowDialogTaskInfo()
            {
                //TODO phx 查看结果前向api发起查看任务状态请求,结果在这里做处理并更新button对应信息，把button更新之后的结果展示在新窗口里
                //如果task本身是done状态，不发起查询
                string resp = WFDWebAPI.GetInstance().GetTaskResultsById(TaskInfo.TaskID);
                string urlResults = UpdateTaskInfoByResp(resp);

                var dialog = new WFDTaskResult(TaskInfo, urlResults);
                if (dialog.ShowDialog() == DialogResult.OK)
                    return;
            }

            private string UpdateTaskInfoByResp(string resp)
            {
                TaskInfo.Status = WFDTaskStatus.Running;
                //判断status，如果done，将result写入结果文件，其他情况不写。结果文件除了done状态，其余情况均为空
                if(TaskInfo.Status == WFDTaskStatus.Done)
                {

                    return "111";
                }
                else
                    return string.Empty;
            }


        }
        #endregion
    }
}
