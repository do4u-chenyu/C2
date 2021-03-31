using C2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.WebsiteFeatureDetection
{
    public enum WFDTaskStatus
    {
        Null,       //
        Running,    //任务运行中
        Done,       //任务成功
        Failed      //任务失败
    }
    class WFDTaskInfo
    {
        public static readonly WFDTaskInfo Empty = new WFDTaskInfo();
        public string TaskName;
        public string TaskID;
        public string TaskCreateTime;
        public string DatasourceFilePath;
        public string ResultFilePath;
        public string PreviewResults;
        public WFDTaskStatus Status;
        public bool IsEmpty() { return this == Empty; }

        public WFDTaskInfo()
        {
            TaskName = string.Empty;
            TaskID = string.Empty;
            TaskCreateTime = ConvertUtil.TransToUniversalTime(DateTime.Now);
            DatasourceFilePath = string.Empty;
            ResultFilePath = string.Empty;
            Status = WFDTaskStatus.Null;
            PreviewResults = string.Empty;
        }

        public WFDTaskInfo(string taskName, string taskId, string datasourceFilePath, string resultFilePath, WFDTaskStatus status)
        {
            TaskName = taskName;
            TaskID = taskId;
            DatasourceFilePath = datasourceFilePath;
            ResultFilePath = resultFilePath;
            Status = status;
            TaskCreateTime = ConvertUtil.TransToUniversalTime(DateTime.Now);
        }
    }
}
