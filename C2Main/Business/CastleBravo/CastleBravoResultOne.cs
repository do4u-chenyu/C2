using System.Collections.Generic;
using System.Net;

namespace C2.Business.CastleBravo
{
    class CastleBravoResultOne
    {
        private static readonly List<string> HeaderColumns = new List<string>() { "md5", "mode", "result", "salt" };
        public static string Columns { get => string.Join("\t", HeaderColumns); }
        public static int ColumnsCount { get => HeaderColumns.Count; }

        public static CastleBravoResultOne Empty = new CastleBravoResultOne();
        public string Content { get => string.Join("\t", new string[] { MD5, Mode, Result, Salt }); }

        public Dictionary<string, string> ResDict { set; get; } = new Dictionary<string, string>();

        private CastleBravoResultOne() { }

        public CastleBravoResultOne(Dictionary<string, string> resDict)
        {
            ResDict = resDict;
        }

        public string MD5 { get => ResDict.TryGetValue("md5", out string md5) ? md5 : string.Empty;}
        public string Mode { get => ResDict.TryGetValue("mode", out string mode) ? mode : string.Empty;}
        public string Result { get => ResDict.TryGetValue("result", out string res) ? res : string.Empty;}
        // 开始被设计为盐值,但实际中被当作备注字段使用
        public string Salt { get => ResDict.TryGetValue("salt", out string salt) ? salt  : string.Empty;}
}

    class CastleBravoAPIResponse
    {
        public string Message { set; get; }
        public string Data { set; get; }
        public HttpStatusCode StatusCode { set; get; }
        public CastleBravoAPIResponse()
        {
            Message = string.Empty;
            Data = string.Empty;
            StatusCode = HttpStatusCode.OK;
        }

    }
}
