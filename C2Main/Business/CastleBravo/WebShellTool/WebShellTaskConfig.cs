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
        public string SGInfoCollectionConfig;  // 后SG素描结果
        public string IP;              // IP地址
        public string Country;         // 归属地
        public string Country2;        // 归属地
        public string DatabaseConfig;  // 数据库配置


        public WebShellTaskConfig(string[] array)
        {
            if (array.Length > 7)
            {
                CreateTime = array[0];
                Remark = array[1];
                Url = array[2];
                Password = array[3];
                TrojanType = array[4];
                Status = array[5];
                ClientVersion = array[6];
                SGInfoCollectionConfig = array[7];
                IP = "0.0.0.0";
                Country = string.Empty;
                Country2 = string.Empty;
            }
            // 字段随版本不断扩充, 保持向上兼容
            if (array.Length > 8)
                IP = array[8];
            // 增加归属地字段
            if (array.Length > 10)
            {
                Country = array[9];
                Country2 = array[10];
            }
            if (array.Length > 11)
                DatabaseConfig = array[11];
        }
        public WebShellTaskConfig() : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty) { }

        public WebShellTaskConfig(string cTime, string remark, string url, string pass, string trojanType, string status, string cVersion, string sg, string ip, string c1, string c2, string dbConfig)
        : this(new string[] { cTime, remark, url, pass, trojanType, status, cVersion, sg, ip, c1, c2, dbConfig })
        { }

        public static string AutoDetectTrojanType(string url)
        {
            url = url.Trim().ToLower();
            if (url.EndsWith(".asp"))
                return "aspEval";
            if (url.EndsWith(".php"))
                return "phpEval";
            return "自动判断";
        }
        public static string AutoDetectClientType(string url, string defaultVersion)
        {
            if (url.ToLower().Trim().EndsWith(".asp"))
                return "ASP通用版";
            return defaultVersion;
        }
    }
}
