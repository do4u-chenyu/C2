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
        private SSHClient ssh;

        public TaskManager() 
        {
            ssh = new SSHClient();
            tasks = new List<TaskInfo>();  
        }

        public TaskInfo AddTask(String value)
        {
            TaskInfo task = TaskInfo.GenTaskInfo(value);
            if (task != TaskInfo.EmptyTaskInfo)
                tasks.Add(task);
            return task;
        }

        public bool DeleteTask(TaskInfo taskInfo) { return tasks.Remove(taskInfo); }
    }
}
