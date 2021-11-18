using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace preprocessing
{
    class DealData
    {
        private string inputFilePath;
        private string outputFilePath;
        private string preType;
        private DataTable dataTable;
        bool deleteAdGroup;
        bool deleteLongText;
        bool deletePic;


        public DealData(string[] param)
        {
            this.inputFilePath = param[0];
            this.outputFilePath = param[1];
            this.preType = param[2];

            //this.inputFilePath = "C:\\Users\\RedHat\\Desktop\\ql\\GROUPCODE_680692357_QQ.tsv";
            //this.outputFilePath = "C:\\Users\\RedHat\\Desktop\\ql\\1.txt";
            //this.preType = "3";


            this.dataTable = new DataTable();

            string pretype = DecimalToBinary(int.Parse(preType));
            this.deleteAdGroup = pretype[0].ToString() == "1";
            this.deleteLongText = pretype[1].ToString() == "1";
            this.deletePic = pretype[2].ToString() == "1";
        }

        private string DecimalToBinary(int decimalNum)
        {
            string binaryNum = Convert.ToString(decimalNum, 2);
            if (binaryNum.Length < 3)
            {
                for (int i = 0; i < 3 - binaryNum.Length; i++)
                {
                    binaryNum = '0' + binaryNum;
                }
            }
            return binaryNum;
        }

        public void Deal()
        {
            SaveResultToLocal(GenDataTable(inputFilePath));
            return;
        }

        private DataTable GenDataTable(string path)
        {
            DataTable dataTable = new DataTable(Path.GetFileNameWithoutExtension(path));

            string[] columnArray = new string[] { "APPID", "QQNUM", "CAPTURE_TIME", "GROUPCODE", "CONTENT", "DATATYPE", "IP", "IPAREAID", "IP_CITY", "IP_PROVINCE", "DotIP", "Position" };

            // 可能有同名列，这里需要重命名一下
            Dictionary<string, int> induplicatedName = new Dictionary<string, int>() { };
            foreach (string col in columnArray)
            {
                if (!induplicatedName.ContainsKey(col))
                {
                    induplicatedName.Add(col, 0);
                    dataTable.Columns.Add(col);
                }
                else
                {
                    induplicatedName[col] += 1;
                    dataTable.Columns.Add(col + "_" + induplicatedName[col]);
                }
            }

            int lineCount = 0;
            FileStream fs_dir = null;
            StreamReader reader = null;
            try
            {
                fs_dir = new FileStream(path, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(fs_dir);
                string lineStr;
                while ((lineStr = reader.ReadLine()) != null)
                {
                    lineCount++;
                    if (lineCount == 1 || lineStr == string.Empty)
                        continue;

                    string[] rowList = lineStr.TrimEnd(new char[] { '\r', '\n' }).Split('\t');
                    if((deleteLongText && rowList[4].Length > 200) || (deletePic && ContainPic(rowList[4])))
                        continue;

                    List<string> tmpRowList = new List<string>();
                    for (int j = 0; j < columnArray.Length; j++)
                    {
                        string cellValue = j < rowList.Length ? rowList[j] : "";
                        tmpRowList.Add(cellValue);
                    }
                    dataTable.Rows.Add(tmpRowList.ToArray());
                }
            }
            catch { }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (fs_dir != null)
                    fs_dir.Close();
            }

            return dataTable;
        }

        private bool ContainPic(string content)
        {
            string[] ends = new string[] { ".pngA", ".gifA", ".jpgA", ".png", ".gif", ".jpg" };
            foreach(string end in ends)
            {
                if (content.EndsWith(end))
                    return true;
            } 
            return false;
        }

        private void SaveResultToLocal(DataTable dataTable)
        {
            StreamWriter sw = null;
            FileStream fs = null;
            try
            {
                fs = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs, Encoding.UTF8);
                List<string> colList = new List<string>();
                for (int i = 0; i < dataTable.Columns.Count; i++)
                    colList.Add(dataTable.Columns[i].ColumnName);

                sw.WriteLine(string.Join("\t", colList));

                foreach (DataRow row in dataTable.Rows)
                {
                    sw.WriteLine(string.Join("\t", row.ItemArray));  
                }
            }
            catch { }
            finally
            {
                if (sw != null)
                    sw.Close();
                if (fs != null)
                    fs.Close();
            }
        }
    }
}
