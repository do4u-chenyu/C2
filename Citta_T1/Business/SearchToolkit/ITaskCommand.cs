using C2.SearchToolkit;
using System;

namespace C2.Business.SearchToolkit
{
    internal interface ITaskCommand
    {
        bool RunTask(TaskInfo task);
        bool DeleteTask(TaskInfo task);
        String QueryTaskStatus();

        bool DownloadTaskResult(String d);
    }
}
