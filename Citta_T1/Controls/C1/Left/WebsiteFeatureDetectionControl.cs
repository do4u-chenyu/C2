using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C2.Dialogs.WebsiteFeatureDetection;
using C2.Utils;
using System.IO;
using C2.Core;
using System.Xml;
using C2.Business.Model;
using C2.Business.WebsiteFeatureDetection;

namespace C2.Controls.C1.Left
{
    public enum WFDTaskStatus
    {
        Null,       //
        Running,    //任务运行中
        Done,       //任务成功
        Failed      //任务失败
    }

    public partial class WebsiteFeatureDetectionControl : BaseLeftInnerPanel
    {
        //Global.WFDUser持久化到文档中UserInformation.xml
        string WFDUser;
        string Token;

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
                var UAdialog = new UserAuthentication();
                if (UAdialog.ShowDialog() == DialogResult.OK)
                {
                    WFDUser = UAdialog.UserName;
                    Token = UAdialog.Token;
                }
                else
                    return;
            }

            var dialog = new AddWFDTask();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //TODO phx 需要问一下这个分类接口，返回状态的时间，立即返回？还是查完才返回？

                List<string> urls = new List<string>() { "http://www.1.com", "http://www.1.com" };
                string taskId = WFDApi.ClassifierUrls(urls, Token);

                string destDirectory = Path.Combine(Global.UserWorkspacePath, "侦察兵", "网络侦察兵");
                string destFilePath = Path.Combine(destDirectory, string.Format("{0}_{1}.bcp", dialog.TaskName, taskId));
                FileUtil.CreateDirectory(destDirectory);
                using (File.Create(destFilePath)) { }

                //添加按钮并持久化到本地
                WebsiteFeatureDetectionTaskInfo taskInfo = new WebsiteFeatureDetectionTaskInfo(dialog.TaskName, taskId, dialog.FilePath, destFilePath, WFDTaskStatus.Null);
                AddInnerButton(new WebsiteFeatureDetectionButton(taskInfo));
                SaveWFDTasksToXml();
            }   
        }

        public List<string> GetTaskNames()
        {
            List<string> taskNames = new List<string>();
            FindControls<WebsiteFeatureDetectionButton>().ForEach(bt => taskNames.Add(bt.TaskInfo.TaskName));
            return taskNames;
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

        private void SaveSingleTask(WebsiteFeatureDetectionTaskInfo taskInfo, XmlDocument xDoc)
        {
            XmlNode node = xDoc.SelectSingleNode("WFDTasks");
            ModelXmlWriter mxw = new ModelXmlWriter("task", node);
            mxw.Write("taskName", taskInfo.TaskName)
                 .Write("taskId", taskInfo.TaskId)
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
            WebsiteFeatureDetectionTaskInfo taskInfo = new WebsiteFeatureDetectionTaskInfo();

            taskInfo.TaskName = xn.SelectSingleNode("taskName").InnerText;
            taskInfo.TaskId = xn.SelectSingleNode("taskId").InnerText;
            taskInfo.DatasourceFilePath = xn.SelectSingleNode("datasourceFilePath").InnerText;
            taskInfo.ResultFilePath = xn.SelectSingleNode("resultFilePath").InnerText;
            taskInfo.Status = WFDTaskStatusEnum(xn.SelectSingleNode("status").InnerText);

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
            public WebsiteFeatureDetectionTaskInfo TaskInfo;

            public WebsiteFeatureDetectionButton()
            {
                InitButtonMenu();
                InitButtonType();
                TaskInfo = new WebsiteFeatureDetectionTaskInfo();
            }
            public WebsiteFeatureDetectionButton(WebsiteFeatureDetectionTaskInfo taskInfo) : this()
            {
                TaskInfo = taskInfo;
                this.ButtonText = taskInfo.TaskName;
                this.toolTip.SetToolTip(this.rightPictureBox, TaskInfo.ResultFilePath);
            }

            private void InitButtonType()
            {
                this.leftPictureBox.Image = global::C2.Properties.Resources.数据;
                this.rightPictureBox.Image = global::C2.Properties.Resources.提示;
            }

            private void InitButtonMenu()
            {
                ToolStripMenuItem RemoveToolStripMenuItem = new ToolStripMenuItem();
                RemoveToolStripMenuItem.Name = "ReviewToolStripMenuItem";
                RemoveToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
                RemoveToolStripMenuItem.Text = "删除任务";
                RemoveToolStripMenuItem.ToolTipText = "从面板中移除任务,同时删除本地结果文件";
                RemoveToolStripMenuItem.Click += new System.EventHandler(RemoveToolStripMenuItem_Click);

                ToolStripMenuItem OpenDatasourceToolStripMenuItem = new ToolStripMenuItem();
                OpenDatasourceToolStripMenuItem.Name = "OpenDatasourceToolStripMenuItem";
                OpenDatasourceToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
                OpenDatasourceToolStripMenuItem.Text = "打开源文件";
                OpenDatasourceToolStripMenuItem.ToolTipText = "从本地文本编辑器中打开文件";
                OpenDatasourceToolStripMenuItem.Click += new System.EventHandler(OpenDatasourceToolStripMenuItem_Click);

                ToolStripMenuItem ResultToolStripMenuItem = new ToolStripMenuItem();
                ResultToolStripMenuItem.Name = "ResultToolStripMenuItem";
                ResultToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
                ResultToolStripMenuItem.Text = "查看结果";
                ResultToolStripMenuItem.ToolTipText = "查看任务返回结果";
                ResultToolStripMenuItem.Click += new System.EventHandler(ResultToolStripMenuItem_Click);

                this.contextMenuStrip.Items.AddRange(new ToolStripItem[] {
                    OpenDatasourceToolStripMenuItem,
                    ResultToolStripMenuItem,
                    RemoveToolStripMenuItem
                 });

            }

            private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
            {
                DialogResult rs = MessageBox.Show(
                    String.Format("删除任务 {0}及结果文件, 继续删除请点击 \"确定\"", ButtonText),
                    "删除",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);

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
                var dialog = new WFDTaskResult();
                if (dialog.ShowDialog() == DialogResult.OK)
                    return;
            }

        }
        #endregion

        #region WFD任务信息类
        private class WebsiteFeatureDetectionTaskInfo
        {
            public string TaskName;
            public string TaskId;
            public string DatasourceFilePath;
            public string ResultFilePath;
            public WFDTaskStatus Status;

            public WebsiteFeatureDetectionTaskInfo()
            {
                TaskName = string.Empty;
                TaskId = string.Empty;
                DatasourceFilePath = string.Empty;
                ResultFilePath = string.Empty;
                Status = WFDTaskStatus.Null;
            }

            public WebsiteFeatureDetectionTaskInfo(string taskName, string taskId, string datasourceFilePath, string resultFilePath, WFDTaskStatus status)
            {
                TaskName = taskName;
                TaskId = taskId;
                DatasourceFilePath = datasourceFilePath;
                ResultFilePath = resultFilePath;
                Status = status;
            }
        }
        #endregion
    }
}
