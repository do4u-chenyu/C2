﻿using C2.Business.SSH;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace C2.SearchToolkit
{
    class SearchTaskManager
    {
        private readonly List<SearchTaskInfo> tasks;

        public SearchTaskManager() 
        {
            tasks = new List<SearchTaskInfo>();
        }

        public IEnumerable<SearchTaskInfo> Tasks { get => tasks; }

        public void SearchDaemonIP(SearchTaskInfo task)
        {
            using (GuarderUtil.WaitCursor)
            {
                BastionAPI api = new BastionAPI(task);
                task.DaemonIP = api.Login()
                                   .SearchDaemonIP();

                api.Close();
            }
        }


        public bool RunTask(SearchTaskInfo task) 
        {
            
            using (GuarderUtil.WaitCursor)
            {
                BastionAPI api = new BastionAPI(task);
                task.PID = api.Login()
                              .DeleteTaskDirectory()
                              .CreateTaskDirectory()
                              .EnterTaskDirectory()
                              .UploadTaskScript()
                              .CheckHomeSearch()
                              .RunTask();
                api.Close();
            }

        
            if (task.PID == String.Empty)
                return false;

            tasks.Add(task);
            return task.Save();
        }

        public bool RunDSQTask(SearchTaskInfo task)
        {

            using (GuarderUtil.WaitCursor)
            {
                BastionAPI api = new BastionAPI(task);
                task.PID = api.Login()
                              .DeleteTaskDirectory()
                              .CreateTaskDirectory()
                              .EnterTaskDirectory()
                              .UploadTaskScript()
                              .CheckHomeSearch()
                              .RunDSQTask();
                api.Close();
            }


            if (task.PID == String.Empty)
                return false;

            tasks.Add(task);
            return task.Save();
        }

        private String[] ListTaskBcpFiles()
        {
            return FileUtil.TryListFiles(Global.SearchToolkitPath, "*.bcp");
        }

        private SearchTaskInfo LoadTaskBcp(String taskFFP)
        {
            try 
            {
                using (StreamReader sw = new StreamReader(taskFFP))
                    return SearchTaskInfo.StringToTaskInfo(sw.ReadToEnd(), true);
            } catch 
            {
                return SearchTaskInfo.EmptyTaskInfo;
            }

        }

        public bool Refresh() 
        {
            tasks.Clear();
            foreach (String taskFFP in ListTaskBcpFiles())
            {
                SearchTaskInfo task = LoadTaskBcp(taskFFP);
                if (task != SearchTaskInfo.EmptyTaskInfo)
                    tasks.Add(task);
            }
            return true; 
        }

        public bool DeleteTask(SearchTaskInfo task) 
        {
            BastionAPI api = new BastionAPI(task);

            api.Login()
               .DeleteTaskDirectory()
               .KillTask();
      
            return tasks.Remove(task) && FileUtil.DeleteFile(task.BcpFFP); 
        }
    }
}
