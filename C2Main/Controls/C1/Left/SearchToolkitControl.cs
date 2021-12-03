using C2.Business.GlueWater;
using C2.Business.SSH;
using C2.Globalization;
using C2.SearchToolkit;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace C2.Controls.C1.Left
{
    struct LastOptionInfo
    {
        public string BastionIP;
        public string SearchAgentIP;
        public string Username;
        public string InterfaceIP;
    };
    public partial class SearchToolkitControl : BaseLeftInnerPanel
    {
        private SearchTaskManager taskManager;
        private LastOptionInfo lastInfo;
        public SearchToolkitControl()
        {
            InitializeComponent();
            taskManager = new SearchTaskManager();
            lastInfo = new LastOptionInfo();
            this.addTaskLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AddTaskLabel_MouseClick);
            this.Load += new System.EventHandler(this.SearchToolkitControl_Load);
        }

        private void AddTaskLabel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            SearchTaskInfo task = new SearchToolkitForm().GenLastInfo(lastInfo.BastionIP, lastInfo.SearchAgentIP, lastInfo.InterfaceIP, lastInfo.Username)
                                                         .ShowTaskConfigDialog();

            if (task.IsEmpty())
                return;
            // 保存用户配置信息
            lastInfo.BastionIP = task.BastionIP;
            lastInfo.SearchAgentIP = task.SearchAgentIP;
            lastInfo.Username = task.Username;
            lastInfo.InterfaceIP = task.InterfaceIP;

            string message;
            if (taskManager.RunTask(task))
            {
                AddInnerButton(new SearchToolkitButton(task));
                message = String.Format("创建全文任务【{0}】 成功", task.TaskName);

                if (task.LastErrorCode == BastionCodePage.NoHomeSearch)
                    message += ", 但全文机上并没有找到全文环境 /home/search/sbin/queryclient";
            }
            else
                message = String.Format("创建全文任务【{0}】 失败：{1}", task.TaskName, task.LastErrorMsg);

            HelpUtil.ShowMessageBox(message);
        }

        private void SearchToolkitControl_Load(object sender, EventArgs e)
        {
            taskManager.Refresh();

            foreach (SearchTaskInfo task in taskManager.Tasks)
                AddInnerButton(new SearchToolkitButton(task));
        }

        public void DeleteButton(SearchToolkitButton button, SearchTaskInfo task)
        {
            using (GuarderUtil.WaitCursor)
                taskManager.DeleteTask(task);
            RemoveButton(button);
        }

        private void HelpInfoLable_Click(object sender, EventArgs e)
        {
            try
            {
                string helpfile = Path.Combine(Application.StartupPath, "Resources", "Help", "全文工具箱帮助文档.txt");
                Help.ShowHelp(this, helpfile);
            }
            catch { };

        }



        //  -----------------------------------------------------

      

        
     


        private class CastleBravoPlugin : BaseLeftInnerButton
        {
            private string pluginType;
            public CastleBravoPlugin(string name)
            {
                pluginType = name;
                InitButtonMenu();
                InitButtonType();
                InitButtonDoubleClick();
            }

            private void InitButtonType()
            {
                ButtonText = Lang._(this.pluginType);
                this.rightPictureBox.Image = global::C2.Properties.Resources.提示;
                switch (this.pluginType)
                {
                    case "DB专项":
                        this.leftPictureBox.Image = global::C2.Properties.Resources.db;
                        this.toolTip.SetToolTip(this.rightPictureBox, HelpUtil.DBFormHelpInfo);
                        break;
                    case "SQ专项":
                        this.leftPictureBox.Image = global::C2.Properties.Resources.sq;
                        this.toolTip.SetToolTip(this.rightPictureBox, HelpUtil.SQFormHelpInfo);
                        break;
                    case "SH专项":
                        this.leftPictureBox.Image = global::C2.Properties.Resources.sh;
                        this.toolTip.SetToolTip(this.rightPictureBox, HelpUtil.SHFormHelpInfo);
                        break;
                    case "DD专项":
                        this.leftPictureBox.Image = global::C2.Properties.Resources.dd;
                        this.toolTip.SetToolTip(this.rightPictureBox, HelpUtil.DDFormHelpInfo);
                        break;
          
                }
            }
            private void InitButtonMenu()
            {
                ToolStripMenuItem OpenToolStripMenuItem = new ToolStripMenuItem
                {
                    Name = "OpenToolStripMenuItem",
                    Size = new System.Drawing.Size(196, 22),
                    Text = "打开"
                };
                OpenToolStripMenuItem.Click += new EventHandler(OpenToolStripMenuItem_Click);

                this.contextMenuStrip.Items.AddRange(new ToolStripItem[] {
                    OpenToolStripMenuItem
                 });

            }
            private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
            {
                OpenPluginForm();
            }

            private void InitButtonDoubleClick()
            {
                this.noFocusButton.MouseDown += new MouseEventHandler(this.NoFocusButton_MouseDown);
            }
            private void NoFocusButton_MouseDown(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left && e.Clicks == 2)
                    OpenPluginForm();
            }

            private void OpenPluginForm()
            {
                switch (pluginType)
                {
                    case "DB专项":
                        new DB().ShowDialog();
                        break;
                    case "SQ专项":
                        new SQ().ShowDialog();
                        break;
                    case "SH专项":
                        new SH().ShowDialog();
                        break;
                    case "DD专项":
                        new DD().ShowDialog();
                        break;
                }
            }
        }

        
        
        private void CastleBravoControl_Load(object sender, EventArgs e)
        {
            LoadCBPlugins();
            ResizeCBLocation();
        }
       

        private void LoadCBPlugins()
        {
            List<string> CBPlugins = new List<string>() { "DB专项", "SQ专项", "SH专项" ,"DD专项"};
            CBPlugins.ForEach(pname => this.AddCBPlugin(new CastleBravoPlugin(pname)));
        }
        private void ResizeCBLocation()
        {
            backPanel.Location = new Point(backPanel.Location.X, ComputeSplitLineLocation());
            backPanel.Height = this.Height - ComputeSplitLineLocation() + 95;
        }

        private void AddCBPlugin(CastleBravoPlugin plugin)
        {
            plugin.Location = new Point(20, ComputeSplitLineLocation());
            this.Controls.Add(plugin);
        }

        private int ComputeSplitLineLocation()
        {
            return this.titleLabel.Height + this.Controls.Find("BaseLeftInnerButton", false).Length * 40 + 10;
        }

    }
}
