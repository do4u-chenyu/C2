using System.IO;
using System.Collections.Generic;
using System.Text;


namespace Citta_T1.Utils
{
    class BCPBuffer
    {
        private Dictionary<string, string> dataPreviewDict = new Dictionary<string, string>();
        private Dictionary<string, string> columnDict = new Dictionary<string, string>();
        private int maxRow = 100;

        private static BCPBuffer BcpBufferSingleInstance;

        public string GetCacheBcpPreVewContent(string bcpFullPath, DSUtil.Encoding encoding)
        {
            string ret = "";

            // 数据不存在时 按照路径重新读取
            if (!dataPreviewDict.ContainsKey(bcpFullPath) || dataPreviewDict[bcpFullPath] == "")           
                PreLoadFile(bcpFullPath, encoding);
            // 防止文件读取时发生错误, 重新判断下是否存在
            if (dataPreviewDict.ContainsKey(bcpFullPath))
                ret = dataPreviewDict[bcpFullPath];
            return ret;

        }

        public string GetCacheColumnLine(string bcpFullPath, DSUtil.Encoding encoding)
        {
            string ret = "";
            if (!columnDict.ContainsKey(bcpFullPath) || columnDict[bcpFullPath] == "")
                PreLoadFile(bcpFullPath, encoding);
            // 防止文件读取时发生错误, 重新判断下是否存在
            if (columnDict.ContainsKey(bcpFullPath))
                ret = columnDict[bcpFullPath];
            return ret;
        }

        public void TryLoadBCP(string bcpFullPath, DSUtil.Encoding encoding)
        {
            if (!dataPreviewDict.ContainsKey(bcpFullPath) || dataPreviewDict[bcpFullPath] == "")
                PreLoadFile(bcpFullPath, encoding);
        }

        public void Remove(string bcpFullPath)
        {
            dataPreviewDict.Remove(bcpFullPath);
            columnDict.Remove(bcpFullPath);
        }

        private void PreLoadFile(string filePath, DSUtil.Encoding encoding)
        {
            System.IO.StreamReader sr;
            StringBuilder sb = new StringBuilder(1024 * 16);
            if (encoding == DSUtil.Encoding.UTF8)
            {
                sr = File.OpenText(filePath);
            }
            else
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs, System.Text.Encoding.Default);
            }

            string firstLine = sr.ReadLine();
            sb.AppendLine(firstLine);

            for (int row = 1; row < maxRow; row++)
                sb.AppendLine(sr.ReadLine());

            dataPreviewDict[filePath] = sb.ToString();
            columnDict[filePath] = firstLine;
        }

        // 数据字典, 全局单例
        public static BCPBuffer GetInstance()
        {
            if (BcpBufferSingleInstance == null)
            {
                BcpBufferSingleInstance = new BCPBuffer();
            }
            return BcpBufferSingleInstance;
        }
    }
}
