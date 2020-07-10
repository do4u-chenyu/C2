using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Windows.Forms;

namespace Citta_T1.Utils
{
    class FileUtil
    {
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
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = "explorer.exe",  //资源管理器
                    Arguments = "/e,/select," + fullFilePath
                };
                System.Diagnostics.Process.Start(processStartInfo);
            }
            catch (Exception)
            {
                //某些机器直接打开文档目录会报“拒绝访问”错误，此时换一种打开方式
                FileUtil.AnotherOpenFilePathMethod(fullFilePath);
            }
        }


        private static void AnotherOpenFilePathMethod(string fullFilePath)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = "explorer.exe",  //资源管理器
                    Arguments = System.IO.Path.GetDirectoryName(fullFilePath)
                };
                System.Diagnostics.Process.Start(processStartInfo);
            }
            catch { }; // 非核心功能, Double异常就不用管了
        }

        public static void DeleteDirectory(string directoryPath)
        {
            try
            {
                System.IO.Directory.Delete(directoryPath, true);
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

        public static string[] TryListDirectory(string path)
        {
            try
            {
                return System.IO.Directory.GetDirectories(path);
            }
            catch
            {
                return new string[0];
            }
        }

        public static List<List<string>> ReadExcel(string fullFilePath, int maxRow, string sheetName = "") 
        {
            FileStream fs = null;
            List<List<string>> rowContentList = new List<List<string>>(); 
            try
            {
                fs = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read);
                //03版(xls)用npoi,07版(xlsx)用epplus
                //npoi的索引从0开始，epplus的索引从1开始
                if (fullFilePath.EndsWith(".xlsx"))
                {
                    using (ExcelPackage package = new ExcelPackage(fs))
                    {
                        ExcelWorksheet worksheet = string.IsNullOrEmpty(sheetName)? package.Workbook.Worksheets[0] : package.Workbook.Worksheets[sheetName];
                        if (worksheet == null)
                        {
                            fs.Close();
                            return rowContentList;
                        }
                        int rowCount =  worksheet.Dimension.End.Row;
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
                                var cellValue = worksheet.Cells[row, col].Value;
                                tmpRowValueList.Add(cellValue != null ? cellValue.ToString().Replace('\n',' ') : string.Empty);
                            }
                            rowContentList.Add(tmpRowValueList);
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
                        return rowContentList;
                    }
                    IRow firstRow = sheet.GetRow(0);
                    int rowCount = sheet.LastRowNum;
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    for (int i = 0; i < Math.Min(maxRow+1, rowCount); i++)
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
                                string cellValue = sheet.GetRow(i).GetCell(j).ToString().Replace('\n',' ');
                                tmpRowValueList.Add(cellValue);
                            }
                        }
                        rowContentList.Add(tmpRowValueList);
                    }

                }
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show(string.Format("文件{0}已被打开，请先关闭该文件", fullFilePath));
            }
            catch (Exception ex)
            {
                Console.WriteLine("预读Excel: " + fullFilePath + " 失败, error: " + ex.Message);
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }

            return rowContentList;
        }



    }
}
