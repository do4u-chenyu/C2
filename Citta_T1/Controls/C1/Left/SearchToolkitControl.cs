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


            TaskInfo taskInfo = new SearchToolkitForm().ShowTaskConfigDialog();

            if (taskInfo.IsEmpty())
                return;

            // TODO run task

            AddInnerButton(new SearchToolkitButton(taskInfo));
            taskManager.AddTask(taskInfo);
        }
    }
}
