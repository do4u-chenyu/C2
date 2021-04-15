﻿using C2.SearchToolkit;
using C2.Utils;
using System;
using System.IO;
using System.Windows.Forms;

namespace C2.Controls.C1.Left
{
    public partial class SearchToolkitControl : BaseLeftInnerPanel
    {
        private SearchTaskManager taskManager;
        public SearchToolkitControl()
        {
            InitializeComponent();
            taskManager = new SearchTaskManager();
        }

        private void AddTaskLabel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            SearchTaskInfo task = new SearchToolkitForm().ShowTaskConfigDialog();

            if (task.IsEmpty())
                return;

            if (task.TaskModel != "涉赌模型")
            {
                HelpUtil.ShowMessageBox(String.Format("该模型【{0}】还在施工中", task.TaskModel));
                return;
            }

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
            RemoveButton(button);
            taskManager.DeleteTask(task);
        }

        private void HelpInfoLable_Click(object sender, EventArgs e)
        {
            string helpfile = Path.Combine(Application.StartupPath, "Resources", "Help", "全文工具箱帮助文档.txt");
            Help.ShowHelp(this, helpfile);
        }
    }
}
