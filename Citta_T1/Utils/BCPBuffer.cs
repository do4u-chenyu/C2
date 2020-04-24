using System.IO;
using System.Collections.Generic;
using System.Text;
using System;
using System.Windows.Forms;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Citta_T1.Utils
{
    class BCPBuffer
    {
        private Dictionary<string, string> dataPreviewDict = new Dictionary<string, string>();
        private Dictionary<string, string> columnDict = new Dictionary<string, string>();
        private int maxRow = 100;

        private static BCPBuffer BcpBufferSingleInstance;
        private LogUtil log = LogUtil.GetInstance("BCPBuffer");

        public string GetCacheBcpPreVewContent(string bcpFullPath, DSUtil.Encoding encoding)
        {
            string ret = "";

            // 数据不存在时 按照路径重新读取
            if (!dataPreviewDict.ContainsKey(bcpFullPath) || dataPreviewDict[bcpFullPath] == "")           
                PreLoadBcpFile(bcpFullPath, encoding);
            // 防止文件读取时发生错误, 重新判断下是否存在
            if (dataPreviewDict.ContainsKey(bcpFullPath))
                ret = dataPreviewDict[bcpFullPath];
            return ret;

        }

        public string GetCacheExcelPreVewContent(string bcpFullPath)
        {
            string ret = "";

            // 数据不存在时 按照路径重新读取
            if (!dataPreviewDict.ContainsKey(bcpFullPath) || dataPreviewDict[bcpFullPath] == "")
                PreLoadExcelFile(bcpFullPath);
            // 防止文件读取时发生错误, 重新判断下是否存在
            if (dataPreviewDict.ContainsKey(bcpFullPath))
                ret = dataPreviewDict[bcpFullPath];
            return ret;

        }
        public string GetCacheColumnLine(string bcpFullPath, DSUtil.Encoding encoding)
        {
            string ret = "";
            //刷新一下存放表头的字典
            PreLoadBcpFile(bcpFullPath, encoding);
            if (!columnDict.ContainsKey(bcpFullPath) || columnDict[bcpFullPath] == "")
                PreLoadBcpFile(bcpFullPath, encoding);
            // 防止文件读取时发生错误, 重新判断下是否存在
            if (columnDict.ContainsKey(bcpFullPath))
                ret = columnDict[bcpFullPath];
            return ret;
        }


        public void TryLoadFile(string bcpFullPath, DSUtil.ExtType extType, DSUtil.Encoding encoding)
        {
            if (!dataPreviewDict.ContainsKey(bcpFullPath) || dataPreviewDict[bcpFullPath] == "")
            {
                if (extType == DSUtil.ExtType.Text)
                    PreLoadBcpFile(bcpFullPath, encoding);  // 按行读取文件 不分割
                else
                    PreLoadExcelFile(bcpFullPath);
            }
                
        }
        
        // TODO 加载下方预览数据，读取excel数据为string
        public void TryLoadExcel(string excelFullPath)
        {
            if (!dataPreviewDict.ContainsKey(excelFullPath) || dataPreviewDict[excelFullPath] == "")
                PreLoadExcelFile(excelFullPath);
        }

        public void Remove(string bcpFullPath)
        {
            dataPreviewDict.Remove(bcpFullPath);
            columnDict.Remove(bcpFullPath);
        }

        /*
         * 按行读取excel文件
         */
        private void PreLoadExcelFile(string filePath, string sheetName = null, bool isFirstRowColumn = true)
        {
            ISheet sheet = null;
            XSSFWorkbook workbook2007;
            HSSFWorkbook workbook2003;
            FileStream fs;
            string firstLine;
            int startRow;
            StringBuilder sb = new StringBuilder(1024 * 16);
            try
            {
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                if (filePath.IndexOf(".xlsx") > 0) // 2007版本
                {
                    workbook2007 = new XSSFWorkbook(fs);
                    if (sheetName != null)
                    {
                        sheet = workbook2007.GetSheet(sheetName);
                    }
                    else
                    {
                        sheet = workbook2007.GetSheetAt(0);
                    }
                }
                else
                {
                    workbook2003 = new HSSFWorkbook(fs);   // 2003版本
                    if (sheetName != null)
                    {
                        sheet = workbook2003.GetSheet(sheetName);
                    }
                    else
                    {
                        sheet = workbook2003.GetSheetAt(0);
                    }
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum;       // 一行最后一个cell的编号 即总的列数
                    int colNum = firstRow.Cells.Count;
                    string[] headers = new string[colNum];
                    string[] rowContent = new string[colNum];
                    string content;

                    for (int i = 0; i < colNum; i++)
                    {
                        headers[i] = firstRow.Cells[i].ToString();
                    }
                    firstLine = string.Join("\t", headers);     // 大师说默认第一行就是表头    
                    firstLine += "\n";
                    sb.Append(firstLine);
                    startRow = sheet.FirstRowNum + 1;
                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = 0; i <= Math.Min(maxRow, rowCount); ++i)
                    {
                        IRow row = sheet.GetRow(i + startRow);
                        if (row == null) continue;              // 没有数据的行默认是null　　　　　　　

                        for (int j = 0; j <colNum; j++)
                        {
                            if (row.GetCell(j) == null)
                                rowContent[j] = "";
                            else
                                rowContent[j] = row.GetCell(j).ToString();
                        }
                        content = string.Join("\t", rowContent);
                        content += "\n";
                        sb.Append(content);
                    }
                    dataPreviewDict[filePath] = sb.ToString();
                    columnDict[filePath] = firstLine;
                }
            }
            catch (Exception ex)
            {
                log.Error("预读Excel: " + filePath + " 失败, error: " + ex);
            }
        }

        /*
         * 按行读取文件，不分割
         */
        private void PreLoadBcpFile(string filePath, DSUtil.Encoding encoding)
        {
            System.IO.StreamReader sr;
            StringBuilder sb = new StringBuilder(1024 * 16);
            try
            {
                if (encoding == DSUtil.Encoding.UTF8)
                    sr = File.OpenText(filePath);
                else
                {
                    FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    sr = new StreamReader(fs, System.Text.Encoding.Default);
                }
                string firstLine = sr.ReadLine();
                sb.AppendLine(firstLine);

                for (int row = 1; row < maxRow && !sr.EndOfStream; row++)
                    sb.AppendLine(sr.ReadLine());                                   // 分隔符

                sr.Close();
                sr.Dispose();

                dataPreviewDict[filePath] = sb.ToString();
                columnDict[filePath] = firstLine;
            }
            catch(Exception ex) 
            {
                log.Error("BCPBuffer 空路径名是非法的: " + ex.ToString());
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
        public string CreateNewBCPFile(string fileName, List<string> columnName)
        {
            string filePath = "";
            string columns = "";
            filePath = Global.GetCurrentDocument().SavePath;
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
                Utils.FileUtil.AddPathPower(filePath, "FullControl");
            }
            //Directory.CreateDirectory(filePath);

            filePath = Path.Combine(filePath, fileName);
            if (!System.IO.File.Exists(filePath))
            {
                System.IO.File.Create(filePath).Close();
                StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8);
                foreach (string name in columnName)
                    columns += name + "\t";
                sw.WriteLine(columns.Trim('\t'));
                sw.Flush();
                sw.Close();
            }
               
            return filePath;

        }
        public void ReWriteBCPFile(string filePath, List<string> columnName)
        {
            string columns = "";
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);
            fs.Close();
            StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8);
            foreach (string name in columnName)
                columns += name + "\t";
            sw.WriteLine(columns.Trim('\t'));
            sw.Flush();
            sw.Close();
           
        }
    }
}
