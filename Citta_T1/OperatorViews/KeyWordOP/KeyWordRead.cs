using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.OperatorViews.KeyWordOP
{
    class KeyWordRead
    {
        public string KeyWordCombination(string keyWordPath, int index,string sep, string encode)
        {
            string resultKey = "当前选择的关键词列项为空";
            if (sep.Equals(""))
            {
                return "不支持空格分割符的关键词文件，与当前关键词组合逻辑冲突";
            }

            return resultKey;
        }
        //private string BcpFileRead(string keyWordPath, int index, string sep, string encode)
        //{
        //    if (keyWordPath == null)
        //        return "未成功读取到对应的关键词文件";
        //    System.IO.StreamReader sr;
        //    FileStream fs = null;
        //    if (encode == OpUtil.Encoding.UTF8)
        //    {
        //        sr = File.OpenText(keyWordPath);
        //    }
        //    else
        //    {
        //        fs = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read);
        //        sr = new StreamReader(fs, System.Text.Encoding.Default);
        //    }
        //    return "未成功读取到对应的关键词文件";
        //}
    }
}
