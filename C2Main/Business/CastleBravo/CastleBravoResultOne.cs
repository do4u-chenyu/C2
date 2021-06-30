using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace C2.Business.CastleBravo
{
    class CastleBravoResultOne
    {
        private static readonly List<string> HeaderColumns = new List<string>() { "md5", "model", "result", "salt" };
        public string Content { get => string.Join("\t", ResDict.Values); }
        public string Columns { get => string.Join("\t", HeaderColumns); }
        public Dictionary<string, string> ResDict { set; get; }

        public CastleBravoResultOne() { }

        public CastleBravoResultOne(Dictionary<string, string> resDict)
        {
            ResDict = resDict;
        }
        public static  string ReplaceValueSpecialChars(string value)
        {
            return Regex.Replace(value, @"[\t\r\n]", string.Empty);
        }

        public string Md5 { get => ResDict.TryGetValue("md5", out string md5) ? md5 : string.Empty;}
        public string Model { get => ResDict.TryGetValue("model", out string model) ? model : string.Empty;}
        public string Result { get => ResDict.TryGetValue("result", out string res) ? res : string.Empty;}
        public string Salt { get => ResDict.TryGetValue("salt", out string salt) ? salt  : string.Empty;}
}

    class CastleBravoAPIResponse
    {
        public string Message { set; get; }
        public string Data { set; get; }
        public CastleBravoAPIResponse()
        {
            Message = string.Empty;
            Data = string.Empty;
        }

    }
}
