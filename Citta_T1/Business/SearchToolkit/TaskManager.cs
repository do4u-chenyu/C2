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
        private String home;
        private List<TaskInfo> tasks;

        public TaskManager() 
        {
            tasks = new List<TaskInfo>();
            home = Global.SearchToolkitPath;
        }

        public IEnumerable<TaskInfo> Tasks { get => tasks; }

        // 运行Task,成功返回TaskID，失败String.Empty
        public bool RunTask(TaskInfo task) 
        {
            BastionAPI api = new BastionAPI(task);

            task.TaskID = api.Login()
                             .UploadGambleScript()
                             .CreateGambleTaskDirectory()
                             .RunGambleTask();

            return true;
        }
        public bool SaveTask(TaskInfo task)
        {   // TODO 文件名重复问题, 需要加入随机数
            String taskFFP = Path.Combine(home, task.BcpFilename);
            try
            {
                if (!Directory.Exists(home))
                    FileUtil.CreateDirectory(home);

                using (StreamWriter sw = new StreamWriter(taskFFP))
                    sw.WriteLine(task.ToString());
            } catch (Exception ex)
            {
                task.LastErrorMsg = ex.Message;
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
            // TODO 删除远程结果文件
            BastionAPI api = new BastionAPI(task);

            task.TaskID = api.Login()
                             .KillGambleTask()
                             .DeleteGambleTaskResult();

            String taskFFP = Path.Combine(home, task.BcpFilename);   
            return tasks.Remove(task) && FileUtil.DeleteFile(taskFFP); 
        }
    }
}
