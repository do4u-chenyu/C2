using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;


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
            //this.option = "0,0,0|0,1,1|1,2,2|0,3,3".Split('|');

            //this.leftFilePath = "D:\\sqy\\datas\\text_gbk_gang4.txt";
            //this.leftFileEncoding = "GBK";
            //this.leftFileSeparator = '|';
            //this.rightFilePath = "D:\\sqy\\datas\\text_utf8_tab1.txt";
            //this.rightFileEncoding = "UTF8";
            //this.rightFileSeparator = '\t';
            //this.leftOutputField = "0,1,2";
            //this.rightOutputField = "0,2";

            InitTable();//初始化左右表
            GenOption();//规整化option
        }

        #region 初始化左右表,结果表
        private void InitTable()
        {
            CreateTable(this.leftTable, this.leftFilePath, this.leftFileEncoding, this.leftFileSeparator);
            CreateTable(this.rightTable, this.rightFilePath, this.rightFileEncoding, this.rightFileSeparator);

            for (int i = 0; i < (leftOutputField.Split(',').Length + rightOutputField.Split(',').Length); i++)
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
            ISheet sheet = null;
            IWorkbook workbook = null;
            FileStream fs = null;
            ICell cell;
            string sheetName = null;
            DataRow dataRow = null;

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
                    //列名加入datatable
                    IRow firstRow = sheet.GetRow(0);//第一行
                    int cellCount = firstRow.LastCellNum;//列数
                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                    {
                        dt.Columns.Add(i.ToString(), typeof(string));
                    }


                    if (rowCount > 1)
                    {

                        for (int rowNum = 1; rowNum <= rowCount; rowNum++)
                        {
                            IRow tmpRow = sheet.GetRow(rowNum);//从第2行（索引1）开始
                            if (tmpRow == null) continue;
                            dataRow = dt.NewRow();
                            for (int cellNum = 0; cellNum < cellCount; cellNum++)
                            {
                                cell = tmpRow.GetCell(cellNum);
                                dataRow[cellNum] = cell != null ? cell.ToString() : "";
                            }
                            dt.Rows.Add(dataRow);
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
                string[] singleOption = tmp.Split(',');//[[0,0,0],[0,1,1],[1,2,2],[0,3,3]]
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

            foreach (string tmp in leftOutputField.Split(','))
            {
                lout.Add(tmp);
            }
            foreach (string tmp in rightOutputField.Split(','))
            {
                rout.Add(tmp);
            }

        }
        #endregion

        #region 初始化左右表
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
                foreach (object dia in dr.ItemArray)
                {
                    Console.Write(dia.ToString() + "\t");
                }
                Console.Write("\n");
            }
        }

        #endregion
    }
}
