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
        public string IP;               // IP地址
        public string Country;          // 归属地
        public string Country2;          // 归属地

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
                DatabaseConfig = array[7];
                IP = "0.0.0.0";
                Country = string.Empty;
                Country2 = string.Empty;
            }
            // 字段随版本不断扩充, 保持向上兼容
            if (array.Length > 8)
                IP = array[8];

            if (array.Length > 10)
            {
                Country = array[9];
                Country2 = array[10];
            }
        }
        public WebShellTaskConfig() : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty) { }

        public WebShellTaskConfig(string cTime, string remark, string url, string pass, string trojanType, string status, string cVersion, string dbConfig)
        : this(new string[] { cTime, remark, url, pass, trojanType, status, cVersion, dbConfig })
        { }
    }
}
