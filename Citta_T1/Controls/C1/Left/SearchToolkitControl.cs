using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C2.SearchToolkit;
using C2.Utils;

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

            // TODO run task
            bool succ = taskManager.RunTask(task) && taskManager.SaveTask(task);

            if (succ)
                AddInnerButton(new SearchToolkitButton(task));
            else
                HelpUtil.ShowMessageBox(String.Format("创建全文任务[{0}]失败：{1}", task.TaskName, task.LastErrorMsg));
        }

        private void SearchToolkitControl_Load(object sender, EventArgs e)
        {
            taskManager.Refresh();

            foreach (TaskInfo task in taskManager.Tasks)
                AddInnerButton(new SearchToolkitButton(task));
        }
    }
}
