﻿using C2.Model;
using ICSharpCode.SharpZipLib.Checksum;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace C2.Utils
{
    class ReadRst
    {
        List<List<string>> result;
        int returnCode;
        string msg;
        public List<List<string>> Result { get { return this.result; } }
        public int ReturnCode { get { return this.returnCode; } }
        public string Msg { get { return this.msg; } }
        public ReadRst()
        {
            result = null;
            returnCode = 0;
            msg = String.Empty;
        }
        public ReadRst(List<List<string>> rst , int rtc, string m)
        {
            result = rst;
            returnCode = rtc;
            msg = m;
        }
    }
    class FileUtil
    {
        public static LogUtil log = LogUtil.GetInstance("FileUtil");
        private static Crc32 crc32 = new Crc32();
        public static void AddPathPower(string pathName, string power)
        {
            string userName = System.Environment.UserName;
            DirectoryInfo dirInfo = new DirectoryInfo(pathName);

            if ((dirInfo.Attributes & FileAttributes.ReadOnly) != 0)
            {
                dirInfo.Attributes = FileAttributes.Normal;
            }

            //取得访问控制列表   
            DirectorySecurity dirsecurity = dirInfo.GetAccessControl();

            switch (power)
            {
                case "FullControl":
                    dirsecurity.AddAccessRule(new FileSystemAccessRule(userName, FileSystemRights.FullControl, InheritanceFlags.ContainerInherit, PropagationFlags.InheritOnly, AccessControlType.Allow));
                    break;
                case "ReadOnly":
                    dirsecurity.AddAccessRule(new FileSystemAccessRule(userName, FileSystemRights.Read, AccessControlType.Allow));
                    break;
                case "Write":
                    dirsecurity.AddAccessRule(new FileSystemAccessRule(userName, FileSystemRights.Write, AccessControlType.Allow));
                    break;
                case "Modify":
                    dirsecurity.AddAccessRule(new FileSystemAccessRule(userName, FileSystemRights.Modify, AccessControlType.Allow));
                    break;
            }
            dirInfo.SetAccessControl(dirsecurity);
        }

        // 实践中发现复制粘贴板有时会出异常
        // 非核心功能,捕捉异常忽略之
        public static bool TryClipboardSetText(string text)
        {
            bool ret = true;
            try { Clipboard.SetText(text); }
            catch { ret = false; }
            return ret;
        }
        public static void ExploreDirectory(string fullFilePath)
        {//判断文件的存在
            if (File.Exists(fullFilePath))
            {
                //存在文件
                try
                {
                    ProcessStartInfo processStartInfo = new ProcessStartInfo
                    {
                        FileName = "explorer.exe",  //资源管理器
                        Arguments = "/e,/select," + fullFilePath
                    };
                    Process.Start(processStartInfo);
                }
                catch (Exception)
                {
                    //某些机器直接打开文档目录会报“拒绝访问”错误，此时换一种打开方式
                    FileUtil.AnotherOpenFilePathMethod(fullFilePath);
                }
            }
            else
            {
                //不存在文件
                HelpUtil.ShowMessageBox("该文件已被删除!");

            }
            
        }
        public static long TryGetFileSize(string fullFilePath, int failCode = -1)
        {
            try
            {
                return new FileInfo(fullFilePath).Length;
            }
            catch
            {
                return failCode;
            }
            
        }
        private static void AnotherOpenFilePathMethod(string fullFilePath)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = "explorer.exe",  //资源管理器
                    Arguments = Path.GetDirectoryName(fullFilePath)
                };
                Process.Start(processStartInfo);
            }
            catch { }; // 非核心功能, Double异常就不用管了
        }


        public static bool FileCopy(String src, String dst)
        {
            try
            {
                new FileInfo(src).CopyTo(dst, true);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static string RenameAndCenterPadding(string name, int rowLength, int textLength)
        {
            string newName = FileUtil.ReName(name, textLength);
            int spaceNum = (rowLength - newName.Length) / 2;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < spaceNum; i++)
                sb.Append(OpUtil.Blank);
            for (int i = 0; i < newName.Length; i++)
                sb.Append(newName[i]);
            for (int i = spaceNum + newName.Length; i < rowLength; i++)
                sb.Append(OpUtil.Blank);
            return sb.ToString();
        }

        public static void DeleteDirectory(string directoryPath)
        {
            try
            {
               Directory.Delete(directoryPath, true);
            }
            catch
            {
                // 如果无法回滚的话,这个地方只能直接忽略了
            }
        }

        public static bool CreateDirectory(string dicectoryPath)
        {
            bool ret = true;
            try
            {
                Directory.CreateDirectory(dicectoryPath);
            }
            catch { ret = false; }
            return ret;
        }

        public static bool FileMove(string oldFFP, string newFFP)
        {
            bool ret = true;
            try
            {
                File.Move(oldFFP, newFFP);
            }
            catch { ret = false; }
            return ret;
        }

        public static bool DirecotryMove(string oldDirectory, string newDirectory)
        {
            bool ret = true;
            try
            {
                Directory.Move(oldDirectory, newDirectory);
            }
            catch { ret = false; }
            return ret;
        }

        public static string TryGetPathRoot(string path)
        {
            string root;
            try
            {
                root = Path.GetPathRoot(path);
            }
            catch (ArgumentException)
            {
                root = String.Empty;
            }
            return root;
        }

        public static string TryGetSysTempDir()
        {
            string tempDir;
            try
            {
                tempDir = Path.GetTempPath();
            }
            catch (System.Security.SecurityException)
            {
                tempDir = string.Empty;
            }
            return tempDir;
        }

        public static string[] TryListDirectory(string path)
        {
            try
            {
                return Directory.GetDirectories(path);
            }
            catch
            {
                return new string[0];
            }
        }

        public static string[] TryListFiles(string path, string searchPattern = "*")
        {
            try
            {
                return Directory.GetFiles(path, searchPattern);
            }
            catch
            {
                return new string[0];
            }
        }
        public static string FileExistOrUse(string fullFilePath)
        {
            if (!File.Exists(fullFilePath))
                return fullFilePath + " 该文件不存在";

            FileStream fs = null;
            try
            {
                fs = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read);
            }
            catch
            {
                return string.Format("文件{0}可能是空文件或者已被其他应用打开。", fullFilePath);
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
            return String.Empty;
        }

        public static ReadRst ReadExcel(string fullFilePath, int maxRow, string sheetName = "")
        {
            FileStream fs = null;
            List<List<string>> rst = new List<List<string>>();
            int returnCode = 1;
            string errMsg = String.Empty;
            if (!File.Exists(fullFilePath))
            {
                returnCode = 0;
                errMsg = fullFilePath + "文件不存在";
                log.Error(errMsg);
                return new ReadRst(rst, returnCode, errMsg);
            }

            try
            {
                fs = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read);
                //03版(xls)用npoi,07版(xlsx)用epplus
                //npoi的索引从0开始，epplus的索引从1开始
                if (fullFilePath.EndsWith(".xlsx"))
                {
                    using (ExcelPackage package = new ExcelPackage(fs))    //  此步骤, 100W_test.xlsx加载需要2.6秒, 后续要优化
                    {
                        ExcelWorksheet worksheet; // 为了测试性能,将三元语句拆开
                        if (string.IsNullOrEmpty(sheetName))  
                            worksheet = package.Workbook.Worksheets[0];    //  此步骤, 100W_test.xlsx加载需要17秒, 后续要优化
                        else
                            worksheet = package.Workbook.Worksheets[sheetName];
                        


                        if (worksheet == null)
                        {
                            fs.Close();
                            return new ReadRst(rst, 0, "");
                        }
                        int rowCount = worksheet.Dimension.End.Row;
                        int colCount = worksheet.Dimension.End.Column;
                        //int realColCount = colCount;
                        //因为是最大列数，可能出现表头列数小于最大列数的情况
                        //for (int i = colCount; i > 0; i--)
                        //{
                        //    if (worksheet.Cells[1, i].Value == null || worksheet.Cells[1, i].Value.ToString() == string.Empty)
                        //        realColCount--;
                        //    else
                        //        break;//从后往前，遇到不为空的代表剩下的表头均有值
                        //}

                        //遍历单元格赋值
                        for (int row = 1; row <= Math.Min(maxRow, rowCount); row++)
                        {
                            List<string> tmpRowValueList = new List<string>();
                            for (int col = 1; col <= colCount; col++)
                            {
                                ExcelRange cell = worksheet.Cells[row, col];
                                string unit = ExcelUtil.GetCellValue(cell).Replace('\n',' ');
                                tmpRowValueList.Add(unit);
                            }
                            rst.Add(tmpRowValueList);
                        }
                    }
                }
                else
                {
                    IWorkbook workbook = new HSSFWorkbook(fs);
                    ISheet sheet = String.IsNullOrEmpty(sheetName) ? workbook.GetSheetAt(0) : workbook.GetSheet(sheetName);
                    if (sheet == null)
                    {
                        fs.Close();
                        return new ReadRst(rst, 0, String.Empty); ;
                    }
                    IRow firstRow = sheet.GetRow(0);
                    if (firstRow == null)
                    {
                        returnCode = -1;
                        errMsg = "不支持预览和导入没有表头的xls文件，请给文件添加合适的表头";
                        log.Error(errMsg);
                        return new ReadRst(rst, returnCode, errMsg);
                    }
                    int rowCount = sheet.LastRowNum + 1;
                    int cellCount = firstRow is null ? 1 : firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    for (int i = 0; i < Math.Min(maxRow + 1, rowCount); i++)
                    {
                        List<string> tmpRowValueList = new List<string>();
                        for (int j = 0; j < cellCount; j++)
                        {
                            if (sheet.GetRow(i) == null || sheet.GetRow(i).GetCell(j) == null) //同理，没有数据的单元格都默认是null
                            {
                                tmpRowValueList.Add(string.Empty);
                            }
                            else
                            {
                                ICell cell = sheet.GetRow(i).GetCell(j);
                                string unit = ExcelUtil.GetCellValue(workbook, cell).Replace('\n',' ');
                                tmpRowValueList.Add(unit);
                            }
                        }
                        rst.Add(tmpRowValueList);
                    }
                }
            }
            catch (System.IO.IOException)
            {
                returnCode = -2;
                errMsg = string.Format("文件{0}可能是空文件或者已被其他应用打开。", fullFilePath);
                log.Error(errMsg);
            }
            catch (Exception ex)
            {
                returnCode = -3;
                errMsg = "预读Excel: " + fullFilePath + " 失败, error: " + ex.Message;
                log.Error(errMsg);
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }

            return new ReadRst(rst, returnCode, errMsg);
        }
        public static List<List<string>> FormatDatas(List<List<string>> datas, int maxNumOfRow)
        {
            /*
             *  返回一个maxNumOfRow长的二维数组
             *  datas.Count = 0
             *  datas.Count = 1
             *  datas.Count > 1
             */
            if (datas.Count == 0)
            {
                List<List<string>> result = new List<List<string>>();
                for (int i = 0; i < maxNumOfRow; i++)
                {
                    result.Add(new List<string> { String.Empty });
                }
                return result;
            }
            int maxNumOfCol = 0;
            List<string> blankRow = new List<string> { };
            for (int i = 0; i < datas.Count; i++)
                maxNumOfCol = Math.Max(datas[i].Count, maxNumOfCol);
            for (int i = 0; i < maxNumOfCol; i++)
                blankRow.Add(String.Empty);
            for (int i = 0; i < Math.Max(maxNumOfRow, datas.Count); i++)
            {
                if (i >= datas.Count)
                    datas.Add(blankRow);
                else
    
                {
                    int numOfCurRow = datas[i].Count;
                    for (int j = 0; j < maxNumOfCol - numOfCurRow; j++)
                        datas[i].Add(String.Empty);
                }
            }
            return datas;
        }

        public static bool IsContainIllegalCharacters(string userName, string target, bool isShowMsg=true)
        {
            string[] illegalCharacters = new string[] { "*", "\\", "/", "$", "[", "]", "+", "-", "&", "%", "#", "!", "~", "`", " ", "\\t", "\\n", "\\r", ":" };
            foreach (string character in illegalCharacters)
            {
                if (userName.Contains(character))
                {
                    if (isShowMsg)
                        MessageBox.Show(target + "包含非法字符，请输入新的" + target + "." + System.Environment.NewLine + "非法字符包含：*, \\, $, [, ], +, -, &, %, #, !, ~, `, \\t, \\n, \\r, :, 空格");
                    return true;
                }
            }
            return false;
        }
        public static string ReName(string name,int maxLen=10)
        {
            string newName;
            int maxLength = maxLen;

            if (name.Length <= maxLength)
            {
                return name;
            }

            int specialChars = Regex.Matches(name.Substring(0, maxLength), "[a-zA-Z0-9* \\ / $  + - & % # ! ~ `   \\t \\n \\r :]").Count;
            if (specialChars < 4)
                newName = name.Substring(0, maxLength);
            else
            {
                newName = name.Substring(0, Math.Min(maxLength, name.Length));
            }
            return newName;
        }
        public static bool NameTooLong(string userName, string target)
        {
            if (userName.Length > 128)
            {
                MessageBox.Show(target + "长度过长,请重新输入" + target);
                return true;
            }
            return false;
        }

        public static Tuple<List<string>, List<List<string>>> ReadBcpFile(string fullFilePath, OpUtil.Encoding encoding, char separator, int maxNumOfRow)
        {
            List<string> headers = new List<string> { };
            int maxColsNum = 0;
            List<List<string>> rows = new List<List<string>> {  };
            Tuple<List<string>, List<List<string>>> result;
            if (fullFilePath == null)
                return new Tuple<List<string>, List<List<string>>>(headers, rows);

            System.IO.StreamReader sr = null;
            FileStream fs = null;
            if (encoding == OpUtil.Encoding.UTF8)
                sr = File.OpenText(fullFilePath);
            else
            {
                fs = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs, System.Text.Encoding.Default);
            }
            try
            {
                headers = new List<string>(sr.ReadLine().Split(separator));
                for (int rowIndex = 1; rowIndex < maxNumOfRow && !sr.EndOfStream; rowIndex++)
                {
                    String line = sr.ReadLine();
                    if (line == null)
                        continue;
                    String[] eles = line.Split(separator);
                    if (eles.Length > maxColsNum)
                        maxColsNum = eles.Length;
                    rows.Add(new List<string>(eles));
                }
                for (int headersColNum = headers.Count; headersColNum < maxColsNum; headersColNum++)
                    headers.Add("");
            }
            catch (Exception e)
            {
                log.Error(string.Format("加载数据 '{0}' 失败, error: {1}", fullFilePath, e.ToString()));
            }
            finally
            {
                result = new Tuple<List<string>, List<List<string>>>(headers, rows);
                if (fs != null)
                    fs.Close();
                if (sr != null)
                    sr.Close();
            }
            return result;
        }
        public static void FillTable(DataGridView dgv, List<List<string>> datas, int maxNumOfRow = 100)
        {
            datas = FileUtil.FormatDatas(datas, maxNumOfRow);
            List<string> headers = datas[0];
            datas.RemoveAt(0);

            DgvUtil.CleanDgv(dgv);
            FileUtil.FillTable(dgv, headers, datas, maxNumOfRow - 1);
        }
        private static void FillTable(DataGridView dgv, List<string> headers, List<List<string>> rows, int maxNumOfRow=100)
        {
            DgvUtil.CleanDgv(dgv);
            try
            {
                DataTable table = new DataTable();
                DataRow newRow;
                DataView view;
                int maxNumOfCol = headers.Count;
                DataColumn[] cols = new DataColumn[maxNumOfCol];
                Dictionary<string, int> induplicatedName = new Dictionary<string, int>() { };
                string headerText;
                char[] seperator = new char[] { '_' };

                // 可能有同名列，这里需要重命名一下
                for (int i = 0; i < maxNumOfCol; i++)
                {
                    cols[i] = new DataColumn();
                    headerText = headers[i];
                    if (!induplicatedName.ContainsKey(headerText))
                        induplicatedName.Add(headerText, 0);
                    else
                    {
                        induplicatedName[headerText] += 1;
                        induplicatedName[headerText] = induplicatedName[headerText];
                    }
                    headerText = induplicatedName[headerText] + "_" + headerText;
                    cols[i].ColumnName = headerText;
                }
                // 表头
                table.Columns.AddRange(cols);
                // DONE 有隐患。目前这个方法不允许外部调用，改为private
                for (int rowIndex = 0; rowIndex < Math.Max(maxNumOfRow, rows.Count); rowIndex++)
                {
                    List<string> row = rows[rowIndex]; // IndexOutOfRange隐患
                    newRow = table.NewRow();
                    for (int colIndex = 0; colIndex < row.Count; colIndex++)
                        newRow[colIndex] = row[colIndex];
                    for (int colIndex = row.Count; colIndex < maxNumOfCol; colIndex++)
                        newRow[colIndex] = "";
                    table.Rows.Add(newRow);
                }

                view = new DataView(table);
                dgv.DataSource = view;
                DgvUtil.ResetColumnsWidth(dgv);


                // 取消重命名
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    try
                    {
                        dgv.Columns[i].HeaderText = dgv.Columns[i].Name.Split(seperator, 2)[1];
                    }
                    catch
                    {
                        dgv.Columns[i].HeaderText = dgv.Columns[i].Name;
                        log.Error("读取文件时，去重命名过程出错");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("FromInputData.FillTable occurs error! " + ex);
            }
            finally
            {

            }
            DgvUtil.DisableOrder(dgv);
        }
        public static  List<List<string>> GetColumns(List<int> indexs, DataItem dataItem, List<string> rows, int upperLimit)
        {
            List<List<string>> columnValues = new List<List<string>>();
            for (int i = 0; i < upperLimit; i++)
            {
                if (i == 0)
                    continue;
                string row = rows[i].TrimEnd('\r');
                if (String.IsNullOrEmpty(row))
                    continue;
                string[] rowElement = row.Split(dataItem.FileSep);
                if (rowElement.Length <= indexs.Max() || indexs.Min() < 0)
                {
                    MessageBox.Show(String.Format("{0}:第{1}行数据可能不完整", dataItem.FilePath, i));
                    return new List<List<string>>();
                }
                for (int j = 0; j < indexs.Count; j++)
                {
                    if (columnValues.Count < j + 1)
                        columnValues.Add(new List<string>());
                    columnValues[j].Add(rowElement[indexs[j]]);
                }
            }
            return columnValues;
        }

        public static long  GetFileCRC32Value(string filePath)
        {
            FileStream fs = null;
            byte[] buffer;
            crc32.Reset();
            try
            {
                fs = new FileStream(filePath, FileMode.Open);
                int size_to_read = Math.Min(100 * 1024 * 1024, (int)fs.Length);
                buffer = new byte[size_to_read];
                fs.Read(buffer, 0, buffer.Length);
            }
            catch
            {
                return 0;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
            crc32.Update(buffer);
            return crc32.Value;
        }
    }
}
