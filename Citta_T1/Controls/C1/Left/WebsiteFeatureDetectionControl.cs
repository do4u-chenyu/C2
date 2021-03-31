using C2.Business.Model;
using C2.Business.WebsiteFeatureDetection;
using C2.Core;
using C2.Dialogs.WebsiteFeatureDetection;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml;

namespace C2.Controls.C1.Left
{
    public partial class WebsiteFeatureDetectionControl : BaseLeftInnerPanel
    {
        public WebsiteFeatureDetectionControl()
        {
            InitializeComponent();
            this.addTaskLabel.Click += new EventHandler(this.AddLabel_Click);
        }

        private void AddLabel_Click(object sender, EventArgs e)
        {
            var dialog = new AddWFDTask();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //添加按钮并持久化到本地
                AddInnerButton(new WebsiteFeatureDetectionButton(dialog.TaskInfo));
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

            SaveUserInfo(rootElement);
            foreach (WebsiteFeatureDetectionButton button in buttons)
                SaveSingleTask(button.TaskInfo, rootElement);

            // 保存时覆盖原文件
            xDoc.Save(xmlPath);
        }
        private void SaveUserInfo(XmlNode node)
        {
            new ModelXmlWriter("userInfo", node).Write("userName", WFDWebAPI.GetInstance().UserName);
        }

         private void SaveSingleTask(WFDTaskInfo taskInfo, XmlNode node)
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
            string xmlPath = Path.Combine(xmlDirectory, "侦察兵", "WFDTasks.xml");
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

            LoadUserInfo(xDoc.SelectSingleNode(@"WFDTasks/userInfo"));
            foreach (XmlNode xn in xDoc.SelectNodes(@"WFDTasks/task")) //要学着尽量利用XPath自己的能力
                LoadSingleTask(xn);

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

        private void LoadSingleTask(XmlNode xn)
        {
            try
            {
                WFDTaskInfo taskInfo = new WFDTaskInfo
                {
                    TaskName = xn.SelectSingleNode("taskName").InnerText,
                    TaskID = xn.SelectSingleNode("taskId").InnerText,
                    TaskCreateTime = xn.SelectSingleNode("taskCreateTime").InnerText,
                    DatasourceFilePath = xn.SelectSingleNode("datasourceFilePath").InnerText,
                    ResultFilePath = xn.SelectSingleNode("resultFilePath").InnerText,
                    Status = WFDTaskStatusEnum(xn.SelectSingleNode("status").InnerText)
                };

                AddInnerButton(new WebsiteFeatureDetectionButton(taskInfo));
            }
            catch {}
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
                DialogResult rs = MessageBox.Show(
                    String.Format("删除任务 {0}及结果文件, 继续删除请点击 \"确定\"", ButtonText),
                    "删除", MessageBoxButtons.OKCancel,MessageBoxIcon.Information);

                if (rs != DialogResult.OK)
                    return;

                Global.GetWebsiteFeatureDetectionControl().RemoveButton(this);
                FileUtil.DeleteFile(this.TaskInfo.ResultFilePath);
                Global.GetWebsiteFeatureDetectionControl().SaveWFDTasksToXml();//先删除后持久化
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
                //string datas = "[{'url': 'http://admin.haishen6688.com/', 'cur_url': 'http://admin.haishen6688.com/', 'html_content_id': 'e0ecfa0a908711eb87b20242ac110005', 'title': '登陆', 'html_content': '登陆登陆 欢迎 请登录 请输入用户名密码及验证码 用户名 密码 验证码 验证码 登陆 2017 ISPEC', 'prediction': '10105', 'login': True, 'screen_shot': None}, {'url': 'http://a1.76688.me/', 'cur_url': 'http://a1.76688.me/', 'html_content_id': 'db082744907911eb9f5e0242ac110005', 'title': 'welcome', 'html_content': 'welcomewelcome 外围彩 账 号 密 码', 'prediction': '1010303', 'login': None, 'screen_shot': 'db082744907911eb9f5e0242ac110005'}]";

                if(WFDWebAPI.GetInstance().QueryTaskResultsById(TaskInfo.TaskID, out string respMsg, out string datas))
                {
                    UpdateTaskInfoByResp(respMsg, datas);
                    Global.GetWebsiteFeatureDetectionControl().SaveWFDTasksToXml();//状态刷新，修改本地持久化文件
                    var dialog = new WFDTaskResult(TaskInfo);
                    if (dialog.ShowDialog() == DialogResult.OK)
                        return;
                }

            }

            private void UpdateTaskInfoByResp(string respMsg, string datas)
            {
                if (respMsg == "success")// && TaskInfo.Status != WFDTaskStatus.Done 考虑是否每次都刷新
                {
                    TaskInfo.Status = WFDTaskStatus.Done;
                    //httpresponse结果会返回一些python的参数，无法被c#正确解析，统一转成字符串
                    datas = datas.Replace("None", "'None'").Replace("True", "'True'").Replace("False", "'False'");
                    TaskInfo.PreviewResults = DealDatas(TaskInfo.ResultFilePath, datas);
                }
                else if (respMsg == "wait")
                    TaskInfo.Status = WFDTaskStatus.Running;
                else if (respMsg == "fail")
                    TaskInfo.Status = WFDTaskStatus.Failed;
            }

            private string DealDatas(string resultFilePath, string results)
            {
                StringBuilder sb = new StringBuilder(1024 * 16);
                //TODO 解析正确结果，同时写进本地文件，返回预览字符串
                //List<string> dataList = JsonConvert.DeserializeObject<List<string>>(datas);
                StreamWriter sw = new StreamWriter(resultFilePath);

                List<WFDResult> resultList = new JavaScriptSerializer().Deserialize<List<WFDResult>>(results);
                foreach(WFDResult result in resultList)
                {
                    sb.Append(result.url).Append(OpUtil.TabSeparator)
                      .Append(result.prediction).Append(OpUtil.TabSeparator)
                      .Append(result.title).Append(OpUtil.TabSeparator)
                      .Append(result.screen_shot).Append(OpUtil.TabSeparator)
                      .Append(result.html_content).Append(OpUtil.LineSeparator);

                    sw.WriteLine(result.JoinAllContent());
                }
                
                sw.Close();
                sw.Dispose();
                return sb.ToString().TrimEnd(OpUtil.LineSeparator);
            }

        }
        #endregion
    }
}
