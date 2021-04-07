using C2.Business.SSH;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace C2.SearchToolkit
{
    class TaskManager
    {
        private readonly List<TaskInfo> tasks;

        public TaskManager() 
        {
            tasks = new List<TaskInfo>();
        }

        public IEnumerable<TaskInfo> Tasks { get => tasks; }

        public bool RunTask(TaskInfo task) 
        {
            BastionAPI api = new BastionAPI(task);
            task.PID = api.Login()
                            .DeleteGambleTaskWorkspace()
                            .CreateGambleTaskDirectory()
                            .UploadGambleScript()
                            .RunGambleTask();
        
            if (task.PID == String.Empty)
                return false;

            tasks.Add(task);
            return task.Save();
        }


        private String[] ListTaskBcpFiles()
        {
            return FileUtil.TryListFiles(Global.SearchToolkitPath, "*.bcp");
        }

        private TaskInfo LoadTaskBcp(String taskFFP)
        {
            try 
            {
                return TaskInfo.StringToTaskInfo(new StreamReader(taskFFP).ReadToEnd(), true);
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

        public bool DeleteTask(TaskInfo task) 
        {
            BastionAPI api = new BastionAPI(task);

            api.Login()
               .DeleteGambleTaskWorkspace()
               .KillGambleTask();
      
            return tasks.Remove(task) && FileUtil.DeleteFile(task.BcpFFP); 
        }
    }
}
