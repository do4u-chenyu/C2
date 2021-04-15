using C2.SearchToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.SearchToolkit
{
    interface TaskCommand
    {
        bool RunTask(TaskInfo task);
        bool DeleteTask(TaskInfo task);
        String QueryTaskStatus();
    }
}
