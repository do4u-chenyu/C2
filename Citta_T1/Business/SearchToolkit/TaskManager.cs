using C2.Business.SSH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.SearchToolkit
{
    class TaskManager
    {
        private List<TaskInfo> tasks;

        public TaskManager() 
        {
            tasks = new List<TaskInfo>();  
        }

        public TaskInfo AddTask(TaskInfo task)
        {
            tasks.Add(task);
            return task;
        }

        public bool DeleteTask(TaskInfo taskInfo) { return tasks.Remove(taskInfo); }
    }
}
