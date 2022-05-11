using System;
using C2.Business.Model;
using C2.Business.WebsiteFeatureDetection;
using C2.Core;
using C2.Dialogs.WebsiteFeatureDetection;
using C2.Utils;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace C2.Controls.C1.Left
{
    public partial class YQFeatureDetectionControl : BaseLeftInnerPanel
    {
        private static readonly int ButtonGapY = 50;//上下间隔
        private static readonly int ButtonLeftX = 18;
        private static readonly int ButtonBottomOffsetY = 23;
        private Point startPoint;
        public YQFeatureDetectionControl()
        {
            startPoint = new Point(ButtonLeftX, -ButtonGapY);
            InitializeComponent();
        }
       
        private void AddTaskLabel_Click(object sender, EventArgs e)
        {
            var dialog = new AddYQTask();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //添加按钮并持久化到本地
                AddInnerButton(new YQTaskButton(dialog.TaskInfo));
                YQSave();
            }
        }

        public void YQSave()
        {
            List<YQTaskButton> buttons = FindControls<YQTaskButton>();

            string xmlDirectory = Path.Combine(Global.UserWorkspacePath, "侦察兵");
            string xmlPath = Path.Combine(xmlDirectory, "YQTasks.xml");
            Directory.CreateDirectory(xmlDirectory);
            XmlDocument xDoc = new XmlDocument();
            XmlElement rootElement = xDoc.CreateElement("YQTasks");
            xDoc.AppendChild(rootElement);

            SaveUserInfo(rootElement);
            foreach (YQTaskButton button in buttons)
                SaveYQSingleTask(button.TaskInfo, rootElement);

            // 保存时覆盖原文件
            xDoc.Save(xmlPath);
        }

        private void SaveUserInfo(XmlNode node)
        {
            new ModelXmlWriter("userInfo", node).Write("userName", WFDWebAPI.GetInstance().UserName);
        }

        private void SaveYQSingleTask(YQTaskInfo taskInfo, XmlNode node)
        {
            new ModelXmlWriter("task", node)
               .Write("taskName", taskInfo.TaskName)
               .Write("taskId", taskInfo.TaskID)
               .Write("taskCreateTime", taskInfo.TaskCreateTime)
               .Write("datasourceFilePath", taskInfo.DatasourceFilePath)
               .Write("resultFilePath", taskInfo.ResultFilePath)
               .Write("status", taskInfo.Status);
        }

        private void YQFeatureDetectionControl_Load(object sender, EventArgs e)
        {
            LoadYQTasks(Path.Combine(Global.WorkspaceDirectory, Global.GetUsername()));
        }

        public void LoadYQTasks(string xmlDirectory)
        {
            string xmlPath = Path.Combine(xmlDirectory, "侦察兵", "YQTasks.xml");
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

            LoadUserInfo(xDoc.SelectSingleNode(@"YQTasks/userInfo"));
            foreach (XmlNode xn in xDoc.SelectNodes(@"YQTasks/task")) //要学着尽量利用XPath自己的能力
                LoadSingleYQTask(xn);

            return;
        }

        private void LoadUserInfo(XmlNode xn)
        {
            try
            {
                WFDWebAPI.GetInstance().UserName = xn.SelectSingleNode("userName").InnerText;
            }
            catch { }
        }

        private void LoadSingleYQTask(XmlNode xn)
        {
            try
            {
                YQTaskInfo taskInfo = new YQTaskInfo
                {
                    TaskName = xn.SelectSingleNode("taskName").InnerText,
                    TaskID = xn.SelectSingleNode("taskId").InnerText,
                    TaskCreateTime = xn.SelectSingleNode("taskCreateTime").InnerText,
                    DatasourceFilePath = xn.SelectSingleNode("datasourceFilePath").InnerText,
                    ResultFilePath = xn.SelectSingleNode("resultFilePath").InnerText,
                    Status = YQTaskStatusEnum(xn.SelectSingleNode("status").InnerText)
                };

                AddInnerButton(new YQTaskButton(taskInfo));
            }
            catch { }
        }

        private YQTaskStatus YQTaskStatusEnum(string encoding, YQTaskStatus defaultStatus = YQTaskStatus.Null)
        {
            if (!Enum.TryParse(encoding, true, out YQTaskStatus outStatus))
                return defaultStatus;
            return outStatus;
        }

        public void RemoveYQButton(Control innerButton)
        {

            // panel左上角坐标随着滑动条改变而改变，以下就是将panel左上角坐标校验
            if (this.manageButtonPanel.Controls.Count > 0)
                this.startPoint.Y = this.manageButtonPanel.Controls[0].Location.Y - this.manageButtonPanel.Controls[0].Height - ButtonBottomOffsetY;

            // 先暂停布局,然后调整button位置,最后恢复布局,可以避免闪烁
            using (new GuarderUtil.LayoutGuarder(this.manageButtonPanel))
            {
                ReLayoutButtons(innerButton); // 重新布局
                this.manageButtonPanel.Controls.Remove(innerButton); // 删除控件
            }
        }

        private void ReLayoutButtons(Control innerButton)
        {
            int idx = this.manageButtonPanel.Controls.IndexOf(innerButton);
            for (int i = idx + 1; i < this.manageButtonPanel.Controls.Count; i++)
            {
                Control ct = this.manageButtonPanel.Controls[i];
                ct.Location = new Point(ct.Location.X, ct.Location.Y - ButtonGapY);
            }
        }
        private void HelpInfoLable_Click(object sender, EventArgs e)
        {
            try
            {
                string helpfile = Path.Combine(Global.ResourcesPath, "Help", "舆情侦察兵帮助文档.txt");
                Help.ShowHelp(this, helpfile);
            }
            catch { };
        }
    }
}
