using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace cat_xls
{
    class Program
    {
        public static void CatExcel(string filePath)
        {
            ISheet sheet = null;
            IWorkbook workbook = null;
            FileStream fs = null;
            ICell cell;
            string sheetName = null;

            try
            {
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                if (filePath.IndexOf(".xlsx") > 0)
                {
                    workbook = new XSSFWorkbook(fs);

                }
                else if (filePath.IndexOf(".xls") > 0)
                {
                    workbook = new HSSFWorkbook(fs);
                }

                if (workbook != null)
                {
                    if (sheetName != null)
                    {
                        sheet = workbook.GetSheet(sheetName);
                    }
                    else
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }

                if (sheet != null)
                {
                    int rowCount = sheet.LastRowNum;//总行数  
                    if (rowCount > 1)
                    {
                        int cellCount = sheet.GetRow(0).LastCellNum;//列数
                        for (int rowNum = 1; rowNum <= rowCount; rowNum++)
                        {
                            List<string> tmpRowValueList = new List<string>();
                            IRow tmpRow = sheet.GetRow(rowNum);//从第2行（索引1）开始
                            for (int cellNum = 0; cellNum < cellCount; cellNum++)
                            {
                                cell = tmpRow.GetCell(cellNum);
                                if (cell != null)
                                {
                                    tmpRowValueList.Add(cell.ToString());
                                }
                                else
                                {
                                    tmpRowValueList.Add("");
                                }
                            }
                            Console.WriteLine(string.Join("\t", tmpRowValueList));
                        }
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
