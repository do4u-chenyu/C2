using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Citta_T1.Utils;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using OfficeOpenXml;
namespace relate
{
    class DealRelate
    {
        private string leftFilePath;
        private string leftFileEncoding;
        private char leftFileSeparator;
        private string rightFilePath;
        private string rightFileEncoding;
        private char rightFileSeparator;
        private string leftOutputField;
        private string rightOutputField;
        private string[] option;
        private List<List<string[]>> optionLists;
        private List<string> lout;
        private List<string> rout;

        private DataTable leftTable = new DataTable();//左表
        private DataTable rightTable = new DataTable();//右表
        private DataTable resultTable = new DataTable();//结果表

        public DealRelate(string[] param)
        {
            this.leftFilePath = param[0];
            this.leftFileEncoding = param[1];
            this.leftFileSeparator = param[2][0];
            this.rightFilePath = param[3];
            this.rightFileEncoding = param[4];
            this.rightFileSeparator = param[5][0];

            this.leftOutputField = param[6];
            this.rightOutputField = param[7];
            this.option = param[8].Split('|');
            //this.option = "0\t0\t0".Split('|');

            //this.leftFilePath = @"D:\sqy\datas\100W_test.xls";
            //this.leftFileEncoding = "GBK";
            //this.leftFileSeparator = "\t"[0];
            //this.rightFilePath = @"D:\FiberHomeIAOModelDocument\phx\我的新模型16\L13_20200701_102245.bcp";
            //this.rightFileEncoding = "UTF8";
            //this.rightFileSeparator = "\t"[0];
            //this.leftOutputField = "0\t1\t2\t3\t4";
            //this.rightOutputField = "0";

            InitTable();//初始化左右表
            GenOption();//规整化option
        }

        #region 初始化左右表,结果表
        private void InitTable()
        {
            CreateTable(this.leftTable, this.leftFilePath, this.leftFileEncoding, this.leftFileSeparator);
            CreateTable(this.rightTable, this.rightFilePath, this.rightFileEncoding, this.rightFileSeparator);

            for (int i = 0; i < (leftOutputField.Split('\t').Length + rightOutputField.Split('\t').Length); i++)
            {
                resultTable.Columns.Add(i.ToString(), typeof(string));
            }
        }

        private void CreateTable(DataTable dataTable, string filePath, string fileEncoding, char fileSeparator)
        {
            if (filePath.IndexOf(".xls") > 0)
            {
                CreateExcelDataTable(dataTable, filePath);
            }
            else
            {
                CreateTxtDataTable(dataTable, filePath, fileEncoding, fileSeparator);
            }
        }

        public static void CreateTxtDataTable(DataTable dt, string filePath, string encoding, char separator)
        {
            StreamReader sr;
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            if (encoding == "GBK")
            {
                sr = new StreamReader(fs, Encoding.Default);
            }
            else
            {
                sr = File.OpenText(filePath);
            }

            String header = sr.ReadLine();
            String[] headers = header.Split(separator);

            for (int i = 0; i < headers.Length; i++)
            {
                dt.Columns.Add(i.ToString(), typeof(string));
            }
            while (sr.Peek() != -1)
            {
                string line = sr.ReadLine();
                string[] lines = line.Split(separator);

                //这里要判断列元素不足或者多了的情况
                if (lines.Length >= headers.Length)
                {
                    dt.Rows.Add(lines.Take(headers.Length).ToArray());
                }
                else
                {
                    List<string> tmpLines = new List<string>();
                    foreach (string tmp in lines)
                    {
                        tmpLines.Add(tmp);
                    }
                    for (int i = 0; i < headers.Length - lines.Length; i++)
                    {
                        tmpLines.Add("");
                    }
                    dt.Rows.Add(tmpLines.ToArray());
                }

            }
            if (fs != null)
            {
                fs.Close();
            }
        }

        public static void CreateExcelDataTable(DataTable dt, string filePath)
        {
            DataRow dataRow = null;
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

                        for (int i = 0; i < realColCount; i++)
                            dt.Columns.Add(i.ToString(), typeof(string));

                        for (int row = 2; row <= rowCount; row++)
                        {
                            dataRow = dt.NewRow();
                            for (int col = 0; col < realColCount; col++)
                            {
                                ExcelRange cell = worksheet.Cells[row, col + 1];
                                dataRow[col] = cell != null ? ExcelUtil.GetCellValue(cell).Replace('\n', ' ') : string.Empty;
                            }
                            dt.Rows.Add(dataRow);
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
                    for (int i = sheet.GetRow(0).FirstCellNum; i < cellCount; ++i)
                    {
                        dt.Columns.Add(i.ToString(), typeof(string));
                    }
                    for (int rowNum = 1; rowNum < rowCount; rowNum++)
                    {
                        IRow tmpRow = sheet.GetRow(rowNum);//从第2行（索引1）开始
                        if (tmpRow == null) continue;
                        dataRow = dt.NewRow();
                        for (int cellNum = 0; cellNum < cellCount; cellNum++)
                        {
                            cell = tmpRow.GetCell(cellNum);
                            dataRow[cellNum] = cell != null ? ExcelUtil.GetCellValue(workbook, cell).Replace('\n', ' ') : string.Empty;
                        }
                        dt.Rows.Add(dataRow);
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
        #endregion

        #region 规整化option
        private void GenOption()
        {
            this.optionLists = new List<List<string[]>>();//最后存储 [ [[0,0],[1,1]] ,[[2,2],[3,3]] ]
            this.lout = new List<string>();
            this.rout = new List<string>();

            List<string[]> alr = new List<string[]>();
            List<string[]> relateTmpList = new List<string[]>();
            foreach (string tmp in option)
            {
                string[] singleOption = tmp.Split('\t');//[[0,0,0],[0,1,1],[1,2,2],[0,3,3]]
                string andor = singleOption[0];
                string leftField = singleOption[1];
                string rightField = singleOption[2];
                if (andor == "0")
                {
                    //如果是AND，那么添加到当前列表
                    relateTmpList.Add(singleOption.Skip(1).Take(2).ToArray());
                }
                else
                {
                    //如果是or，开启一个新列表
                    optionLists.Add(relateTmpList);
                    relateTmpList = new List<string[]>();
                    relateTmpList.Add(singleOption.Skip(1).Take(2).ToArray());
                }
            }
            optionLists.Add(relateTmpList);

            foreach (string tmp in leftOutputField.Split('\t'))
            {
                lout.Add(tmp);
            }
            foreach (string tmp in rightOutputField.Split('\t'))
            {
                rout.Add(tmp);
            }

        }
        #endregion

        #region 关联左右表（笛卡尔集）
        public void RelateTwoTables()
        {
            foreach (DataRow rowa in leftTable.Rows)
            {
                //拿出左表一行
                bool matchFlag = false;
                for (int i = 0; i < rightTable.Rows.Count; i++)
                {
                    bool matchRow = false;
                    //拿出右表一行
                    DataRow rowb = rightTable.Rows[i];

                    //this.optionLists = new List<List<string[]>>();//最后存储 [ [[0,0],[1,1]] ,[[2,2],[3,3]] ]
                    //判断逻辑是，and的所有项均满足，返回true  ,or（外层循环）只要有一个true就行，所以循环为true时，可以break
                    foreach (List<string[]> lr in optionLists) //lr = [[0,0],[1,1]]
                    {
                        foreach (string[] tmplr in lr)  //tmplr = [0,0]//内层and循环，必须全为true,一旦有false就break
                        {
                            string le = tmplr[0];
                            string ri = tmplr[1];
                            if (rowa[le].ToString() == rowb[ri].ToString())
                            {
                                matchRow = true;
                            }
                            else
                            {
                                matchRow = false;
                                break;
                            }
                        }
                        if (matchRow) break;
                    }


                    if (matchRow)
                    {
                        //lstbRowsIndex.Add(i);
                        List<object> outContent = new List<object>();
                        lout.ForEach(cc => outContent.Add(rowa[cc]));
                        rout.ForEach(cc => outContent.Add(rowb[cc]));
                        resultTable.Rows.Add(outContent.ToArray());
                        matchFlag = true;
                    }
                }

                if (!matchFlag)
                {
                    List<object> outContent = new List<object>();
                    lout.ForEach(cc => outContent.Add(rowa[cc]));
                    rout.ForEach(cc => outContent.Add("null"));
                    resultTable.Rows.Add(outContent.ToArray());
                }

                matchFlag = false;
            }

            foreach (DataRow dr in resultTable.Rows)
            {
                Console.Write(string.Join("\t", dr.ItemArray));
                Console.Write("\n");
            }
        }

        #endregion

    }
}
