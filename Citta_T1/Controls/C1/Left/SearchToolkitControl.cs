using C2.SearchToolkit;
using C2.Utils;
using System;
using System.Windows.Forms;

namespace C2.Controls.C1.Left
{
    public partial class SearchToolkitControl : BaseLeftInnerPanel
    {
        private TaskManager taskManager;
        public SearchToolkitControl()
        {
            InitializeComponent();
            taskManager = new TaskManager();
        }

        private void AddTaskLabel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            TaskInfo task = new SearchToolkitForm().ShowTaskConfigDialog();

            if (task.IsEmpty())
                return;

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

            foreach (TaskInfo task in taskManager.Tasks)
                AddInnerButton(new SearchToolkitButton(task));
        }

        public void DeleteButton(SearchToolkitButton button, TaskInfo task) 
        {
            RemoveButton(button);
            taskManager.DeleteTask(task);
        }
    }
}
