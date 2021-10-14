using System;

namespace C2.Business.CastleBravo.WebShellTool
{
    // 用这个持久化,后面新增字段就困难了
    [Serializable]
    public class WebShellTaskConfig
    {
        public static readonly WebShellTaskConfig Empty = new WebShellTaskConfig();

        public string CreateTime;      // 类字段顺序与持久化要求必须保持一致
        public string Remark;             
        public string Url;
        public string Password;
        public string Status;          // 木马是否可连通并可用
        public string TrojanType;      // 木马类型, 如 php, asp, jsp
        public string ClientVersion;   // 客户端版本, 如 中国菜刀, 奥特曼, 哥斯拉 
        public string DatabaseConfig;  


        public WebShellTaskConfig() : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty) { }

        public WebShellTaskConfig(string cTime, string remark, string url, string pass, string trojanType, string status, string cVersion, string dbConfig)
        {
            CreateTime = cTime;
            Remark = remark;
            Url = url;
            Password = pass;
            TrojanType = trojanType;
            Status = status;
            ClientVersion = cVersion;
            DatabaseConfig = dbConfig;
        }
    }
}
