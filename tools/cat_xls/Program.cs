using System;
using System.Collections.Generic;
using System.IO;
using Citta_T1.Utils;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using OfficeOpenXml;

namespace cat_xls
{
    class Program
    {
        public static void CatExcel(string filePath)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                //03版(xls)用npoi,07版(xlsx)用epplus
                //npoi的索引从0开始，epplus的索引从1开始
                if (filePath.EndsWith(".xlsx"))
                {
                    using (ExcelPackage package = new ExcelPackage(fs))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        if (worksheet == null)
                        {
                            fs.Close();
                            return;
                        }
                        int rowCount = worksheet.Dimension.End.Row;
                        int colCount = worksheet.Dimension.End.Column;
                        int realColCount = colCount;

                        if (rowCount < 2)
                        {
                            fs.Close();
                            return;
                        }
                        //因为是最大列数，可能出现表头列数小于最大列数的情况
                        for (int i = colCount; i > 0; i--)
                        {
                            if (worksheet.Cells[1, i].Value == null || worksheet.Cells[1, i].Value.ToString() == string.Empty)
                                realColCount--;
                            else
                                break;//从后往前，遇到不为空的代表剩下的表头均有值
                        }

                        for (int row = 2; row <= rowCount; row++)
                        {
                            List<string> tmpRowValueList = new List<string>();
                            for (int col = 1; col <= realColCount; col++)
                            {
                                ExcelRange cell = worksheet.Cells[row, col];
                                tmpRowValueList.Add(cell != null ? ExcelUtil.GetCellValue(cell).Replace('\n', ' ') : string.Empty);
                            }
                            Console.WriteLine(string.Join("\t", tmpRowValueList));
                        }
                    }
                }
                else
                {
                    IWorkbook workbook = new HSSFWorkbook(fs);
                    ISheet sheet = null;
                    ICell cell = null;
                    string sheetName = null;
                    if (workbook != null)
                        sheet = sheetName != null ? workbook.GetSheet(sheetName) : workbook.GetSheetAt(0);
                    if (sheet == null)
                    {
                        fs.Close();
                        return;
                    }

                    int rowCount = sheet.LastRowNum + 1;//总行数  
                    if (rowCount < 2)
                    {
                        fs.Close();
                        return;
                    }

                    int cellCount = sheet.GetRow(0).LastCellNum;//列数
                    for (int rowNum = 1; rowNum < rowCount; rowNum++)
                    {
                        List<string> tmpRowValueList = new List<string>();
                        IRow tmpRow = sheet.GetRow(rowNum);//从第2行（索引1）开始
                        for (int cellNum = 0; cellNum < cellCount; cellNum++)
                        {
                            cell = tmpRow.GetCell(cellNum);
                            tmpRowValueList.Add(cell != null ? ExcelUtil.GetCellValue(workbook, cell).Replace('\n', ' ') : string.Empty);
                        }
                        Console.WriteLine(string.Join("\t", tmpRowValueList));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }

        static void Main(string[] args)
        {
            try
            {
                if (args != null && args.Length > 0)
                {
                    CatExcel(args[0]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
    }
}
