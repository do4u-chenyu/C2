using C2.Utils;
using System;
using System.Collections.Generic;

namespace C2.Business.CastleBravo
{
    public enum CastleBravoTaskStatus
    {
        Null,       
        Running,    
        Done,       
        Fail        
    }

    class CastleBravoTaskInfo
    {
        private static readonly Dictionary<String, String> MD5ModelTable = new Dictionary<String, String>
        {
            ["btpwd"]     = "宝塔面板",
            ["md5md5"]    = "md5(md5($pass))",
            ["md5md5md5"] = "md5(md5(md5($pass)))"
        };

        public static readonly CastleBravoTaskInfo Empty = new CastleBravoTaskInfo();
        public string TaskName;
        public string TaskID;
        public string TaskCreateTime;
        public string MD5FilePath;
        public string ResultFilePath;
        public List<CastleBravoResultOne> PreviewResults;
        public CastleBravoTaskStatus Status;
        public bool IsEmpty() { return this == Empty; }

        public CastleBravoTaskInfo()
        {
            TaskName = string.Empty;
            TaskID = string.Empty;
            TaskCreateTime = ConvertUtil.TransToUniversalTime(DateTime.Now);
            MD5FilePath = string.Empty;
            ResultFilePath = string.Empty;
            Status = CastleBravoTaskStatus.Null;
            PreviewResults = new List<CastleBravoResultOne>();
        }

        public CastleBravoTaskInfo(string taskName, string taskId, string datasourceFilePath, string resultFilePath, CastleBravoTaskStatus status)
        {
            TaskName = taskName;
            TaskID = taskId;
            MD5FilePath = datasourceFilePath;
            ResultFilePath = resultFilePath;
            Status = status;
            TaskCreateTime = ConvertUtil.TransToUniversalTime(DateTime.Now);
            PreviewResults = new List<CastleBravoResultOne>();
        }

        public static string Model(string key)
        {
            return MD5ModelTable.ContainsKey(key) ? MD5ModelTable[key] : key;
        }

    }
}
