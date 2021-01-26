using C2.Business.Model;
using C2.Configuration;
using C2.Database;
using C2.Model;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace C2.Core
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
            // dk 然而存在表头为空的文件
            return String.IsNullOrEmpty(headColumnLine) && String.IsNullOrEmpty(previewFileContent);
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
        public long CrcValue { get; set; } //文件内容crc校验码
    }
    class BCPBuffer
    {
        private readonly Dictionary<string, FileCache> dataPreviewDict = new Dictionary<string, FileCache>(128);

        private static BCPBuffer BcpBufferSingleInstance;
        private static readonly LogUtil log = LogUtil.GetInstance("BCPBuffer");
        private static readonly Regex regexXls = new Regex(@"\.xl(s?[xmb]?|t[xm]|am)$");
        private static readonly int maxRow = 100;
        #region 获取缓存的方法
        public string GetCachePreviewBcpContent(string fullFilePath, OpUtil.Encoding encoding, bool isForceRead = false)
        {
            return GetCachePreviewFileContent(fullFilePath, OpUtil.ExtType.Text, encoding, isForceRead);
        }
        public string GetCachePreviewExcelContent(string fullFilePath, bool isForceRead = false)
        {
            return GetCachePreviewFileContent(fullFilePath, OpUtil.ExtType.Excel, OpUtil.Encoding.NoNeed, isForceRead);
        }
        public string GetCachePreviewTable(DatabaseItem databaseItem, int mNumOfLine = 100, bool isForceRead = false)
        {
            string key = databaseItem.AllDatabaseInfo;
            if (!HitCache(key) || isForceRead)
            {
                IDAO dao = DAOFactory.CreateDAO(databaseItem);
                if (!dao.TestConn())
                {
                    HelpUtil.ShowMessageBox(HelpUtil.DbCannotBeConnectedInfo);
                    return String.Empty;
                }
                Table table = databaseItem.DataTable;
                string tbContent = dao.GetTableContentString(table, mNumOfLine);
                string firstLine = GetFirstLine(tbContent);
                dataPreviewDict[key] = new FileCache(tbContent, firstLine);
            }
            return dataPreviewDict[key].PreviewFileContent;
        }

        private string GetCachePreviewFileContent(string fullFilePath, OpUtil.ExtType type, OpUtil.Encoding encoding, bool isForceRead = false)
        {
            string ret = String.Empty;
            // 超过100M Excel不处理
            if (IsBigFile(fullFilePath))
                return ret;
            // 数据不存在 或 需要强制读取时 按照路径重新读取
            if (!HitCache(fullFilePath) || isForceRead)
                switch (type)
                {
                    case OpUtil.ExtType.Excel:
                        PreLoadExcelFileNew(fullFilePath);
                        break;
                    case OpUtil.ExtType.Text:
                        PreLoadBcpFile(fullFilePath, encoding);
                        break;
                    default:
                        break;
                }
            // 防止文件读取时发生错误, 重新判断下是否存在
            if (HitCache(fullFilePath))
            {
                IsUpdateCache(type, fullFilePath);
                ret = dataPreviewDict[fullFilePath].PreviewFileContent;
            }
            return ret;
        }
        private bool PreLoadExcelFileNew(string fullFilePath)
        {
            // 查看是否命中本地大文件缓存
            if (HasCache(fullFilePath, ProgramEnvironment.DataBufferFilename))
            {
                return true;
            }

            ReadRst rrst = FileUtil.ReadExcel(fullFilePath, maxRow);
            if (rrst.ReturnCode <= 0 || rrst.ReturnCode > 0 && rrst.Result.Count == 0)
            {
                // 不能把显示messagebox的逻辑放到底层，这种方式很容易出问题
                // HelpUtil.ShowMessageBox(rrst.Msg);
                return false;
            }
            List<List<string>> rowContentList = rrst.Result;
            string firstLine = String.Join("\t", rowContentList[0]);
            StringBuilder sb = new StringBuilder(1024 * 16);
            for (int i = 0; i < rowContentList.Count; i++)
                sb.AppendLine(String.Join("\t", rowContentList[i]));
            dataPreviewDict[fullFilePath] = new FileCache(sb.ToString(), firstLine);
            dataPreviewDict[fullFilePath].CrcValue = FileUtil.GetFileCRC32Value(fullFilePath);

            // 大文件(10M以上)内容写入本地缓存
            if (FileUtil.TryGetFileSize(fullFilePath) > 10 * 1024 * 1024)
            {
                WriteBuffer(fullFilePath, sb, firstLine);
            }
            return true;
        }
        private bool PreLoadBcpFile(string fullFilePath, OpUtil.Encoding encoding)
        {
            if (!File.Exists(fullFilePath))
            {
                // 不能把显示messagebox的逻辑放到底层，这种方式很容易出问题
                //HelpUtil.ShowMessageBox(fullFilePath + "该文件不存在");
                return false;
            }

            StreamReader sr = null;
            bool returnVar = true;
            try
            {
                if (encoding == OpUtil.Encoding.UTF8)
                    sr = File.OpenText(fullFilePath);
                else
                {
                    FileStream fs = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read);
                    sr = new StreamReader(fs, Encoding.Default);
                }
                string firstLine = sr.ReadLine();
                StringBuilder sb = new StringBuilder(1024 * 16);
                sb.AppendLine(firstLine);

                for (int row = 1; row < maxRow && !sr.EndOfStream; row++)
                    sb.AppendLine(sr.ReadLine());                                   // 分隔符
                dataPreviewDict[fullFilePath] = new FileCache(sb.ToString(), firstLine);
            }
            catch (Exception e)
            {
                log.Error("BCPBuffer 预加载BCP文件出错: " + e.ToString());
                returnVar = false;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
            return returnVar;
        }
        #endregion
        public  string GetFirstLine(string tbContent)
        {
            if (string.IsNullOrEmpty(tbContent))
                return string.Empty;
            if (!tbContent.Contains(OpUtil.DefaultLineSeparator.ToString()))
                return tbContent;
            int lineSepIndex = tbContent.IndexOf(OpUtil.DefaultLineSeparator);
            return tbContent.Substring(0, lineSepIndex);
        }
        public string GetCacheColumnLine(string fullFilePath, OpUtil.Encoding encoding, bool isForceRead = false)
        {
            string ret = String.Empty;
            OpUtil.ExtType type = OpUtil.ExtType.Unknow;
            // 超过100M Excel不处理
            if (IsBigFile(fullFilePath))
                return ret;
            //现在支持excel和bcp，以后增加格式这边可能要改
            if (!HitCache(fullFilePath) || isForceRead)
            {
                if (regexXls.IsMatch(fullFilePath))
                {
                    PreLoadExcelFileNew(fullFilePath);
                    type = OpUtil.ExtType.Excel;
                }
                else
                    PreLoadBcpFile(fullFilePath, encoding);

            }
            if (HitCache(fullFilePath))
            {
                IsUpdateCache(type,fullFilePath);
                ret = dataPreviewDict[fullFilePath].HeadColumnLine;
            }
            return ret;
        }
        private bool IsBigFile(string fullFilePath)
        {
            // 100M Excel大文件不处理，提示信息
            if (!regexXls.IsMatch(fullFilePath))
                return false;
            try
            {
                FileInfo fi = new FileInfo(fullFilePath);
                long fileSize = fi.Length / 1024 / 1024;
                if (fileSize > 100)
                {
                    // 不能把显示messagebox的逻辑放到底层，这种方式很容易出问题
                    // MessageBox.Show("Excel文件过大，超过100M，无法处理.", "文件过大", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
            }
            catch
            { }
            return false;
        }
        private void IsUpdateCache(OpUtil.ExtType type,string fullFilePath)
        {
            if (type != OpUtil.ExtType.Excel)
                return;
            long crcValue = FileUtil.GetFileCRC32Value(fullFilePath);
            long oldCrc = dataPreviewDict[fullFilePath].CrcValue;
            if (crcValue != oldCrc)
            {
                PreLoadExcelFileNew(fullFilePath);
            }
        }
        public bool TryLoadFile(string fullFilePath, OpUtil.ExtType extType, OpUtil.Encoding encoding, char separator)
        {
            bool returnVar = true;

            // 超过100M Excel不处理
            if (IsBigFile(fullFilePath))
                return !returnVar;
            // 命中缓存,直接返回,不再加载文件
            if (HitCache(fullFilePath))
            {
                IsUpdateCache(extType, fullFilePath);
                return returnVar;
            }             
            switch (extType)
            {
                case OpUtil.ExtType.Excel:
                    returnVar = PreLoadExcelFileNew(fullFilePath);
                    break;
                case OpUtil.ExtType.Text:
                    returnVar = PreLoadBcpFile(fullFilePath, encoding);  // 按行读取文件 不分割
                    break;
                case OpUtil.ExtType.Unknow:
                default:
                    break;
            }
            return returnVar;
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
        // 数据缓存信息写入xml
        private void WriteBuffer(string fullFilePath, StringBuilder sb, string firstLine)
        {
            XmlDocument xDoc = new XmlDocument();
            string bufferPath = ProgramEnvironment.DataBufferFilename;

            if (!File.Exists(bufferPath))
            {
                
                XmlElement root = xDoc.CreateElement("DataCache");
                xDoc.AppendChild(root);
                xDoc.Save(bufferPath);
            }

            xDoc.Load(bufferPath);
            Utils.FileUtil.AddPathPower(bufferPath, "FullControl");
            XmlNode rootNode = xDoc.SelectSingleNode("DataCache");
            XmlNodeList nodeList;
            try 
            {
                nodeList = xDoc.SelectNodes(String.Format("//DataItem[path='{0}']", fullFilePath));
            }
            catch 
            { 
                nodeList = null;
            }

            if (nodeList != null && nodeList.Count > 0)
            {
                foreach (XmlNode node in nodeList)
                {
                    ModelXmlWriter mxw = new ModelXmlWriter(node);
                    mxw.ReWriteInnerText("crc32", dataPreviewDict[fullFilePath].CrcValue.ToString())
                       .ReWriteInnerText("content", sb.ToString())
                       .ReWriteInnerText("head", firstLine);
                }
            }
            else
            {
                
                ModelXmlWriter mxw = new ModelXmlWriter("DataItem", rootNode);
                mxw.Write("path", fullFilePath)
                   .Write("crc32", FileUtil.GetFileCRC32Value(fullFilePath).ToString())
                   .Write("head", firstLine)
                   .Write("content", sb.ToString());
            }
            xDoc.Save(bufferPath);
            
        }
        private bool HasCache(string fullFilePath, string bufferPath)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlNodeList nodeList;
            try
            {
                xDoc.Load(bufferPath);
                XmlNode rootNode = xDoc.SelectSingleNode("DataCache");
                nodeList = xDoc.SelectNodes(String.Format("//DataItem[path='{0}']", fullFilePath));
            }          
            catch
            { 
                return false;
            }
            if (nodeList.Count == 0)
                return false;
            long crcValue = FileUtil.GetFileCRC32Value(fullFilePath);
            foreach (XmlNode node in nodeList)
            {
                string oldCrc = XmlUtil.GetInnerText(node, "crc32");
                if (string.IsNullOrEmpty(oldCrc)|| !string.Equals(oldCrc, crcValue.ToString()))
                {
                    return false;
                }
                dataPreviewDict[fullFilePath] = new FileCache(XmlUtil.GetInnerText(node, "content"),
                                                              XmlUtil.GetInnerText(node, "head"));
                dataPreviewDict[fullFilePath].CrcValue = crcValue;
            }
            return true;
        }
        /*
         * 按行读取文件，不分割
         */



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
            if (!Directory.Exists(Global.GetCurrentModelDocument().SavePath))
            {
                Directory.CreateDirectory(Global.GetCurrentModelDocument().SavePath);
                FileUtil.AddPathPower(Global.GetCurrentModelDocument().SavePath, "FullControl");
            }

            string fullFilePath = Path.Combine(Global.GetCurrentModelDocument().SavePath, fileName);
            ReWriteBCPFile(fullFilePath, columnsName);
            return fullFilePath;
        }
        public void ReWriteBCPFile(string fullFilePath, List<string> columnsName)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(fullFilePath, false, Encoding.UTF8))
                {
                    string columns = String.Join("\t", columnsName);
                    sw.WriteLine(columns.Trim(OpUtil.DefaultFieldSeparator));
                    sw.Flush();
                }
            }
            catch(Exception e)
            {
                log.Error("重写BCP文件失败， error: " + e.ToString());
            }
        }
        public bool IsEmptyHeader(string fullFilePath)
        {
            return !dataPreviewDict.ContainsKey(fullFilePath) || string.IsNullOrEmpty(dataPreviewDict[fullFilePath].HeadColumnLine);
        }
    }
}
