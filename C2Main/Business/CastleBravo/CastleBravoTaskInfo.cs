using C2.Utils;
using System;
using System.Collections.Generic;

namespace C2.Business.CastleBravo
{
    public enum CastleBravoTaskStatus
    {
        Null,       
        Running,
        Half,
        Done,
        Fail        
    }

    class CastleBravoTaskInfo
    {
        private static readonly Dictionary<String, String> MD5ModelTable = new Dictionary<String, String>
        {
            ["btpwd"]     = "宝塔面板",
            ["behinder"]  = "三代冰蝎",
            ["md5"]       = "MD5",
            ["md5md5"]    = "MD5(MD5($pass))",
            ["md5md5md5"] = "MD5(MD5(MD5($pass)))",
        };
        // 版本兼容, 经常会修改描述信息
        private static readonly Dictionary<String, String> MD5SaltTable = new Dictionary<string, string>
        {
            ["base"] = "快简表",
            ["基础库"] = "快简表",
            ["基础表"] = "快简表",
            ["彩虹表"] = "彩虹表",
            ["rainbow"] = "彩虹表",
            ["控制信息"] = "控制信息",
            ["快简表"] = "快简表"
        };

        public static readonly CastleBravoTaskInfo Empty = new CastleBravoTaskInfo();

        public string TaskName;
        public string TaskID;
        public string TaskCount;
        public string TaskCreateTime;
        public string MD5FilePath;
        public string ResultFilePath;
        public List<CastleBravoResultOne> PreviewResults = new List<CastleBravoResultOne>();
        public CastleBravoTaskStatus Status;

        public CastleBravoTaskInfo() : this("0", string.Empty, string.Empty, string.Empty, string.Empty, CastleBravoTaskStatus.Null)
        { }

        public CastleBravoTaskInfo(string taskCount, string taskName, string taskId, string md5FilePath, string resultFilePath, CastleBravoTaskStatus status)
        {
            TaskCount = taskCount;
            TaskName = taskName;
            TaskID = taskId;
            MD5FilePath = md5FilePath;
            ResultFilePath = resultFilePath;
            Status = status;
            TaskCreateTime = ConvertUtil.TransToUniversalTime(DateTime.Now);
        }

        public static string Model(string key)
        {
            return MD5ModelTable.ContainsKey(key) ? MD5ModelTable[key] : key;
        }

        public static string Salt(string key)
        {
            return MD5SaltTable.ContainsKey(key) ? MD5SaltTable[key] : key;
        }

    }
}
