using Citta_T1.Utils;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Citta_T1.Core
{
    class FileCache
    {
        private string previewFileContent;     // 文件内容
        private string headColumnLine;         // 表头
        private bool dirty;

        public FileCache(string content, string headLine)
        {
            previewFileContent = content;
            headColumnLine = headLine;
            dirty = false;
        }

        public bool IsEmpty()
        {
            // 必须有表头,如果连表头都没有,就认定为空
            return String.IsNullOrEmpty(headColumnLine);
        }

        public bool IsNotEmpty()
        {
            // 必须有表头,如果连表头都没有,就认定为空
            return !IsEmpty();
        }

        public string PreviewFileContent { get => previewFileContent; set => previewFileContent = value; }
        public string HeadColumnLine { get => headColumnLine; set => headColumnLine = value; }
        public bool Dirty { get => dirty; set => dirty = value; }
        public bool NotDirty { get => !Dirty; }
    }
    class BCPBuffer
    {
        private readonly Dictionary<string, FileCache> dataPreviewDict = new Dictionary<string, FileCache>(128);

        private static BCPBuffer BcpBufferSingleInstance;
        private static readonly LogUtil log = LogUtil.GetInstance("BCPBuffer");
        private static readonly Regex regexXls = new Regex(@"\.xl(s?[xmb]?|t[xm]|am)$");
        private static readonly int maxRow = 100;
        public string GetCachePreViewBcpContent(string fullFilePath, OpUtil.Encoding encoding, char separator, bool isForceRead = false)
        {
            return GetCachePreViewFileContent(fullFilePath, OpUtil.ExtType.Text, encoding, separator, isForceRead);
        }

        public string GetCachePreViewExcelContent(string fullFilePath, bool isForceRead = false)
        {
            return GetCachePreViewFileContent(fullFilePath, OpUtil.ExtType.Excel, OpUtil.Encoding.NoNeed, '\t', isForceRead);
        }

        private string GetCachePreViewFileContent(string fullFilePath, OpUtil.ExtType type, OpUtil.Encoding encoding, char separator, bool isForceRead = false)
        {
            string ret = String.Empty;
            // 数据不存在 或 需要强制读取时 按照路径重新读取
            if (!HitCache(fullFilePath) || isForceRead)
                switch (type)
                {
                    case OpUtil.ExtType.Excel:
                        PreLoadExcelFileNew(fullFilePath);
                        break;
                    case OpUtil.ExtType.Text:
                        PreLoadBcpFile(fullFilePath, encoding, separator);
                        break;
                    default:
                        break;
                }
            // 防止文件读取时发生错误, 重新判断下是否存在
            if (HitCache(fullFilePath))
                ret = dataPreviewDict[fullFilePath].PreviewFileContent;
            return ret;
        }
        public string GetCacheColumnLine(string fullFilePath, OpUtil.Encoding encoding, char separator, bool isForceRead = false)
        {

            string ret = String.Empty;
            //现在支持excel和bcp，以后增加格式这边可能要改
            if (!HitCache(fullFilePath) || isForceRead)
            {
                if (regexXls.IsMatch(fullFilePath))
                {
                    PreLoadExcelFileNew(fullFilePath);
                }
                else
                    PreLoadBcpFile(fullFilePath, encoding, separator);

            }
            if (HitCache(fullFilePath))
                ret = dataPreviewDict[fullFilePath].HeadColumnLine;
            return ret;
        }


        public void TryLoadFile(string fullFilePath, OpUtil.ExtType extType, OpUtil.Encoding encoding, char separator)
        {
            // 命中缓存,直接返回,不再加载文件
            if (HitCache(fullFilePath))
                return;

            switch (extType)
            {
                case OpUtil.ExtType.Excel:
                    PreLoadExcelFileNew(fullFilePath);
                    break;
                case OpUtil.ExtType.Text:
                    PreLoadBcpFile(fullFilePath, encoding, separator);  // 按行读取文件 不分割
                    break;
                case OpUtil.ExtType.Unknow:
                default:
                    break;
            }
        }

        private bool HitCache(string fullFilePath)
        {
            return dataPreviewDict.ContainsKey(fullFilePath)   // 哈希表里有这个记录
                && dataPreviewDict[fullFilePath] != null       // 对应的Value不为Null,返回null的话特别容易出bug,源头杜绝
                && dataPreviewDict[fullFilePath].IsNotEmpty()  // 没内容认为是没命中, 空文件每次读也无所谓
                && dataPreviewDict[fullFilePath].NotDirty;     // 外部置脏数据了,得重读
        }


        public void Remove(string bcpFullPath)
        {
            dataPreviewDict.Remove(bcpFullPath);
        }

        private void PreLoadExcelFileNew(string fullFilePath, string sheetName = "")
        {
            List<List<String>> rowContentList = new List<List<String>>();
            rowContentList = FileUtil.ReadExcel(fullFilePath, maxRow);
            if (rowContentList.Count == 0)
                return;
            StringBuilder sb = new StringBuilder(1024 * 16);
            string firstLine = String.Join("\t", rowContentList[0]);
            for (int i = 0; i < rowContentList.Count; i++)
                sb.AppendLine(String.Join("\t", rowContentList[i]));
            dataPreviewDict[fullFilePath] = new FileCache(sb.ToString(), firstLine);
        }
        /*
         * 按行读取文件，不分割
         */
        private void PreLoadBcpFile(string fullFilePath, OpUtil.Encoding encoding, char separator)
        {
            StringBuilder sb = new StringBuilder(1024 * 16);
            try
            {
                Tuple<List<string>, List<List<string>>> result = FileUtil.ReadBcpFile(fullFilePath, encoding, separator, 100);
                sb.AppendLine(string.Join(separator.ToString(), result.Item1));
                foreach (List<string> row in result.Item2)
                    sb.AppendLine(string.Join(separator.ToString(), row));
                dataPreviewDict[fullFilePath] = new FileCache(sb.ToString(), result.Item1.ToString());
            }
            catch (Exception e)
            {
                log.Error("BCPBuffer 预加载BCP文件出错: " + e.ToString());
            }
            finally
            {
            }
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

        public void SetDirty(string fullFilePath)
        {
            // 如果未命中缓存,肯定是每次重新加载,不需要设置Dirty
            if (!HitCache(fullFilePath))
                return;
            dataPreviewDict[fullFilePath].Dirty = true;
        }
        public string CreateNewBCPFile(string fileName, List<string> columnsName)
        {
            if (!Directory.Exists(Global.GetCurrentDocument().SavePath))
            {
                Directory.CreateDirectory(Global.GetCurrentDocument().SavePath);
                FileUtil.AddPathPower(Global.GetCurrentDocument().SavePath, "FullControl");
            }

            string fullFilePath = Path.Combine(Global.GetCurrentDocument().SavePath, fileName);
            ReWriteBCPFile(fullFilePath, columnsName);
            return fullFilePath;
        }
        public void ReWriteBCPFile(string fullFilePath, List<string> columnsName)
        {
            using (StreamWriter sw = new StreamWriter(fullFilePath, false, Encoding.UTF8))
            {
                string columns = String.Join("\t", columnsName);
                sw.WriteLine(columns.Trim(OpUtil.DefaultSeparator));
                sw.Flush();
            }
        }
    }
}
