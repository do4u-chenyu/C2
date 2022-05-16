using C2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.WebsiteFeatureDetection
{
    public enum YQTaskStatus
    {
        Null,       //
        Running,    //任务运行中
        Done,       //任务成功
        Failed      //任务失败
    }
    public class YQTaskInfo
    {
        public static readonly YQTaskInfo Empty = new YQTaskInfo();
        public string TaskName;
        public string TaskID;
        public string TaskModel;
        public string TaskCreateTime;
        public string DatasourceFilePath;
        public string ResultFilePath;
        public YQTaskStatus Status;
        public bool IsEmpty() { return this == Empty; }

        public YQTaskInfo()
        {
            TaskName = string.Empty;
            TaskID = string.Empty;
            TaskModel = string.Empty;
            TaskCreateTime = ConvertUtil.TransToUniversalTime(DateTime.Now);
            DatasourceFilePath = string.Empty;
            ResultFilePath = string.Empty;
            Status = YQTaskStatus.Null;
        }

        public YQTaskInfo(string taskName, string taskId,  string taskModel, string datasourceFilePath, string resultFilePath, YQTaskStatus status,string taskCreateTime)
        {
            TaskName = taskName;
            TaskID = taskId;
            TaskModel = taskModel;
            DatasourceFilePath = datasourceFilePath;
            ResultFilePath = resultFilePath;
            Status = status;
            TaskCreateTime = taskCreateTime;
        }

        public bool IsOverTime()
        {
            int validityPeriodTime = 86400;//24小时的换算
            return ConvertUtil.TryParseInt(ConvertUtil.TransToUniversalTime(DateTime.Now)) - ConvertUtil.TryParseInt(TaskCreateTime) > validityPeriodTime;
        }
    }
}
