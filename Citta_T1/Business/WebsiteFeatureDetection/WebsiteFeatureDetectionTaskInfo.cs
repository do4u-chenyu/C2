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
    class WebsiteFeatureDetectionTaskInfo
    {
        public string TaskName;
        public string TaskId;
        public string DatasourceFilePath;
        public string ResultFilePath;
        public WFDTaskStatus Status;

        public WebsiteFeatureDetectionTaskInfo()
        {
            TaskName = string.Empty;
            TaskId = string.Empty;
            DatasourceFilePath = string.Empty;
            ResultFilePath = string.Empty;
            Status = WFDTaskStatus.Null;
        }

        public WebsiteFeatureDetectionTaskInfo(string taskName, string taskId, string datasourceFilePath, string resultFilePath, WFDTaskStatus status)
        {
            TaskName = taskName;
            TaskId = taskId;
            DatasourceFilePath = datasourceFilePath;
            ResultFilePath = resultFilePath;
            Status = status;
        }
    }
}
