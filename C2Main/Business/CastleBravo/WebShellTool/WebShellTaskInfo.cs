using C2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.CastleBravo.WebShellTool
{
    public enum WebShellTaskType
    {
        Null,
        phpEval
    }

    [Serializable]
    public class WebShellTaskInfo
    {
        public static readonly WebShellTaskInfo Empty = new WebShellTaskInfo();
        
        public string TaskID;
        public string TaskName;
        public string TaskUrl;
        public string TaskPwd;
        public WebShellTaskType TaskType;
        public string TaskRemark;
        public string TaskAddTime;
        public string TaskAdvanced;


        public WebShellTaskInfo()
        {
            TaskID = string.Empty;
            TaskName = string.Empty;
            TaskUrl = string.Empty;
            TaskPwd = string.Empty;
            TaskRemark = string.Empty;
            TaskType = WebShellTaskType.Null;
            TaskAddTime = ConvertUtil.TransToUniversalTime(DateTime.Now);
            TaskAdvanced = string.Empty;
        }

        public WebShellTaskInfo(string taskId, string taskName, string taskUrl, string taskPwd, WebShellTaskType taskType, string taskRemark, string taskAdvancedSetting)
        {
            TaskID = taskId;
            TaskName = taskName;
            TaskUrl = taskUrl;
            TaskPwd = taskPwd;
            TaskRemark = taskRemark;
            TaskType = taskType;
            TaskAddTime = ConvertUtil.TransToUniversalTime(DateTime.Now);
            TaskAdvanced = taskAdvancedSetting;
        }
    }
}
