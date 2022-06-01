using C2.Utils;
using System;

namespace C2.Business.WebsiteFeatureDetection
{
    public class YQTaskInfo
    {
        public static readonly YQTaskInfo Empty = new YQTaskInfo();
        public string TaskName;
        public string TaskID;
        public string TaskModel;
        public string TaskCreateTime;
        public string DatasourceFilePath;
        public string ResultFilePath;
        public int PId;

        public bool IsEmpty() { return this == Empty; }

        public YQTaskInfo()
        {
            TaskName = string.Empty;
            TaskID = string.Empty;
            TaskModel = string.Empty;
            TaskCreateTime = ConvertUtil.TransToUniversalTime(DateTime.Now);
            DatasourceFilePath = string.Empty;
            ResultFilePath = string.Empty;
            PId = 0;
        }

        public YQTaskInfo(string taskName, string taskId,  string taskModel, string datasourceFilePath, string resultFilePath, int pid,string taskCreateTime)
        {
            TaskName = taskName;
            TaskID = taskId;
            TaskModel = taskModel;
            DatasourceFilePath = datasourceFilePath;
            ResultFilePath = resultFilePath;
            PId = pid;
            TaskCreateTime = taskCreateTime;
        }
    }
}
