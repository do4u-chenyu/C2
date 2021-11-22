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
        bool deleteAd;
        bool deleteLongText;
        bool deletePic;


        public DealData(string[] param)
        {
            this.inputFilePath = param[0];
            this.outputFilePath = param[1];
            this.preType = param[2];

            //this.inputFilePath = "C:\\Users\\RedHat\\Desktop\\ql";
            //this.outputFilePath = "C:\\Users\\RedHat\\Desktop\\ql\\1.txt";
            //this.preType = "7";

            this.dataTable = new DataTable();

            string pretype = DecimalToBinary(int.Parse(preType));
            this.deleteAd = pretype[0].ToString() == "1";
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
            DataTable dataTable = new DataTable("preprocessing");

            string[] columnArray = new string[] { "APPID", "QQNUM", "CAPTURE_TIME", "GROUPCODE", "CONTENT", "DATATYPE", "IP", "IPAREAID", "IP_CITY", "IP_PROVINCE", "DotIP", "Position" };
            Dictionary<string, List<string[]>> groupContentsDict = new Dictionary<string, List<string[]>>();

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

            
            FileStream fs_dir = null;
            StreamReader reader = null;
            List<string> fileList = new List<string>();

            if (Directory.Exists(path))
            {
                DirectoryInfo folder = new DirectoryInfo(path);
                foreach (FileInfo file in folder.GetFiles())
                    fileList.Add(Path.Combine(path,file.Name));
            }
            else
                fileList.Add(path);

            try
            {
                foreach (string file in fileList)
                {
                    int lineCount = 0;
                    fs_dir = new FileStream(file, FileMode.Open, FileAccess.Read);
                    reader = new StreamReader(fs_dir);
                    string lineStr;
                    while ((lineStr = reader.ReadLine()) != null)
                    {
                        lineCount++;
                        if (lineCount == 1 || lineStr == string.Empty)
                            continue;

                        string[] rowList = lineStr.TrimEnd(new char[] { '\r', '\n' }).Split('\t');
                        if (rowList.Length < 5)
                            continue;
                        string group = rowList[3];
                        string content = rowList[4];

                        if (deleteAd && content.Length > 60)
                        {

                            if (groupContentsDict.ContainsKey(group))
                                groupContentsDict[group] = JudgeSimilar(groupContentsDict[group], rowList);
                            else
                                groupContentsDict.Add(group, new List<string[]>() { rowList });
                            continue;
                        }

                        if ((deleteLongText && rowList[4].Length > 100) || (deletePic && ContainPic(rowList[4])))
                            continue;

                        dataTable.Rows.Add(CompleteLine(rowList, columnArray.Length).ToArray());
                    }

                    if (deleteAd)
                    {
                        foreach (string group in groupContentsDict.Keys)
                        {
                            foreach (string[] line in groupContentsDict[group])
                                dataTable.Rows.Add(CompleteLine(line, columnArray.Length).ToArray());
                        }
                    }
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


        private List<string> CompleteLine(string[] rowList, int length)
        {
            List<string> tmpRowList = new List<string>();
            for (int j = 0; j < length; j++)
            {
                string cellValue = j < rowList.Length ? rowList[j] : "";
                tmpRowList.Add(cellValue);
            }
            return tmpRowList;
        }

        private List<string[]> JudgeSimilar(List<string[]> contentList, string[] content)
        {
            //判断文本相似度，如果重复度大于90%，说明文本内容基本一致
            List<string[]> tmpList = new List<string[]>();
            foreach(string[] oriContent in contentList)
            {
                if (Sim(oriContent[4], content[4]) < 0.9)
                    tmpList.Add(oriContent);
            }
            if (tmpList.Count == contentList.Count)//相同，说明没有相似文本
                tmpList.Add(content);

            return tmpList;
        }

        private double Sim(string txt1, string txt2)
        {
            List<char> sl1 = txt1.ToCharArray().ToList();
            List<char> sl2 = txt2.ToCharArray().ToList();
            //去重
            List<char> sl = sl1.Union(sl2).ToList<char>();

            //获取重复次数
            List<int> arrA = new List<int>();
            List<int> arrB = new List<int>();
            foreach (var str in sl)
            {
                arrA.Add(sl1.Where(x => x == str).Count());
                arrB.Add(sl2.Where(x => x == str).Count());
            }
            //计算商
            double num = 0;
            //被除数
            double numA = 0;
            double numB = 0;
            for (int i = 0; i < sl.Count; i++)
            {
                num += arrA[i] * arrB[i];
                numA += Math.Pow(arrA[i], 2);
                numB += Math.Pow(arrB[i], 2);
            }
            double cos = num / (Math.Sqrt(numA) * Math.Sqrt(numB));
            return cos;
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
