using C2.Business.SSH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C2.Core;
using System.IO;
using C2.Utils;

namespace C2.SearchToolkit
{
    class TaskManager
    {
        private String home;
        private List<TaskInfo> tasks;

        public TaskManager() 
        {
            tasks = new List<TaskInfo>();
            home = Global.SearchToolkitPath;
        }

        public IEnumerable<TaskInfo> Tasks { get => tasks; }

        public bool SaveTask(TaskInfo task)
        {
            String taskFFP = Path.Combine(home, task.BcpFilename);
            try
            {  
                using (StreamWriter sw = new StreamWriter(taskFFP))
                    sw.WriteLine(task.ToString());
            } catch 
            {
                return false;
            }
            
            tasks.Add(task);
            return true;
        }

        private String[] ListTaskBcpFiles()
        {
            return FileUtil.TryListFiles(home, "*.bcp");
        }
        private TaskInfo LoadTaskBcp(String taskFFP)
        {
            try 
            {
                String content = new StreamReader(taskFFP).ReadToEnd();
                return TaskInfo.GenTaskInfo(content, true);
            } catch 
            {
                return TaskInfo.EmptyTaskInfo;
            }

        }

        public bool Refresh() 
        {
            tasks.Clear();
            foreach (String taskFFP in ListTaskBcpFiles())
            {
                TaskInfo task = LoadTaskBcp(taskFFP);
                if (task != TaskInfo.EmptyTaskInfo)
                    tasks.Add(task);
            }
            return true; 
        }

        public bool DeleteTask(TaskInfo taskInfo) { return tasks.Remove(taskInfo); }
    }
}
