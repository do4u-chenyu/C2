using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.CastleBravo
{
    class CastleBravoResult
    {
        private List<string> fixColList;
        public string AllContent { set; get; }
        public string AllCol { set; get; }
        public Dictionary<string, string> ResDict { set; get; }

        public CastleBravoResult()
        {
            //由于字典无序导致结果文件内容顺序混乱，因此记录固定列，剩余列往后放
            fixColList = new List<string>() { "md5", "type", "trans", "salt" };
        }

        public CastleBravoResult(Dictionary<string, string> resDict):this()
        {
            ResDict = resDict;
            SetFixColValue();
            JoinAllContentAndCol();
        }

        private void SetFixColValue()
        {
            md5 = ResDict.TryGetValue("md5", out string tmpMd5) ? tmpMd5 : string.Empty;
            type = ResDict.TryGetValue("type", out string tmpType) ? tmpType : string.Empty;
            trans = ResDict.TryGetValue("trans", out string tmpTrans) ? tmpTrans : string.Empty;
            salt = ResDict.TryGetValue("salt", out string tmpSalt) ? tmpSalt : string.Empty;
        }
        public void JoinAllContentAndCol()
        {
            List<string> contentList = new List<string>();
            List<string> colList = new List<string>();
            foreach (string fixCol in fixColList)
            {
                colList.Add(fixCol);
                contentList.Add(ResDict.ContainsKey(fixCol) ? ReplaceValueSpecialChars(ResDict[fixCol]) : string.Empty);
            }

            var key = ResDict.Keys.GetEnumerator();
            while (key.MoveNext())
            {
                if (fixColList.Contains(key.Current))
                    continue;

                colList.Add(key.Current);
                contentList.Add(ReplaceValueSpecialChars(ResDict[key.Current]));
            }

            AllCol = string.Join("\t", colList);
            AllContent = string.Join("\t", contentList);

        }

        private string ReplaceValueSpecialChars(string value)
        {
            //"null"会判定为true,且不支持replace
            return string.IsNullOrEmpty(value) ? value : value.Replace("\t", "").Replace("\n", "").Replace("\r", "");
        }

        public string md5 { get; set; }
        public string type { get; set; }
        public string trans { get; set; }
        public string salt { get; set; }
    }

    class CastleBravoAPIResult
    {
        public string RespMsg { set; get; }
        public string Datas { set; get; }
        public CastleBravoAPIResult()
        {
            RespMsg = string.Empty;
            Datas = string.Empty;
        }

    }
}
