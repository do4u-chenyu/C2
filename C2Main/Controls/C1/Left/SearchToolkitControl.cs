using C2.SearchToolkit;
using C2.Utils;
using System;
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
            string helpfile = Path.Combine(Application.StartupPath, "Resources", "Help", "全文工具箱帮助文档.txt");
            Help.ShowHelp(this, helpfile);
        }
    }
}
