using C2.Business.SSH;
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
        private readonly SearchTaskManager taskManager;
        private LastOptionInfo lastInfo;
        public SearchToolkitControl()
        {
            taskManager = new SearchTaskManager();
            lastInfo = new LastOptionInfo();
            InitializeComponent();     
            SearchToolkitControlLoad();
            PluginsButtonLoad();
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
                    message += ", 但全文主节点上并没有找到全文环境 /home/search/sbin/queryclient" +
                        ", 很有可能配置了错误的全文机IP";
            }
            else
                message = String.Format("创建全文任务【{0}】 失败：{1}", task.TaskName, task.LastErrorMsg);

            HelpUtil.ShowMessageBox(message);
        }

        private void SearchToolkitControlLoad()
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

        
        private void PluginsButtonLoad()
        {
            LoadCBPlugins();
        }
       

        private void LoadCBPlugins()
        {
            List<string> CBPlugins = new List<string>() { 
                "涉赌专项", 
                "涉枪专项", 
                "涉黄专项",
                "盗洞专项",
                "黑吃黑专项",
                "境外网产专项"
            };
            CBPlugins.ForEach(pname => this.AddCBPlugin(new PluginButton(pname)));
        }
        private void ResizeCBLocation()
        {
            backPanel.Height = this.Height - ComputeSplitLineLocation();
            backPanel.Height -= searchTitleLabel.Height;
            searchTitleLabel.Location = new Point(searchTitleLabel.Location.X, backPanel.Location.Y - searchTitleLabel.Height - 2);
            
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

        private void SearchToolkitControl_Resize(object sender, EventArgs e)
        {
            ResizeCBLocation();
        }
    }
}
