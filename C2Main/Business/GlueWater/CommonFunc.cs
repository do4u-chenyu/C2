using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace C2.Business.GlueWater
{
    class CommonFunc
    {
        public int maxRow = 65534;
        public string txtDirectory = Path.Combine(Global.UserWorkspacePath, "胶水系统");

        public CommonFunc()
        {
            if(!Directory.Exists(txtDirectory))
                FileUtil.CreateDirectory(txtDirectory);
        }

        public List<int> IndexFilter(List<string> colList, List<List<string>> rowContentList)
        {
            List<int> headIndex = new List<int> { };
            foreach (string content in colList)
            {
                for (int i = 0; i < rowContentList[0].Count; i++)
                {
                    if (rowContentList[0][i] == content)
                    {
                        headIndex.Add(i);
                        break;
                    }
                }
            }
            if (headIndex.Count != colList.Count)
            {
                HelpUtil.ShowMessageBox("上传的数据格式错误。");
                return new List<int>();
            }
            return headIndex;
        }

        public List<string> ContentFilter(List<int> indexList, List<string> contentList)
        {
            List<string> resultList = new List<string> { };
            {
                foreach (int index in indexList)
                    resultList.Add(contentList[index]);
            }

            return resultList;
        }


        public void ReWriteResult(string txtPath, DataTable dataTable)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(txtPath, false, System.Text.Encoding.UTF8))
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        List<string> rowContent = new List<string>();
                        for (int i = 0; i < dataTable.Columns.Count; i++)
                            rowContent.Add(row[i].ToString());

                        sw.WriteLine(string.Join("\t", rowContent));
                    }
                }
            }
            catch (Exception ex)
            {
                HelpUtil.ShowMessageBox(ex.Message);
            }
        }

        public DataTable GenDataTable(string path, string[] colList)
        {
            DataTable dataTable = new DataTable(Path.GetFileNameWithoutExtension(path));

            foreach (string col in colList)
                dataTable.Columns.Add(col);

            if (!File.Exists(path))
                return dataTable;

            FileStream fs_dir = null;
            StreamReader reader = null;
            try
            {
                fs_dir = new FileStream(path, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(fs_dir);

                string lineStr;
                while ((lineStr = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(lineStr))
                        continue;

                    string[] rowList = lineStr.TrimEnd(new char[] { '\r', '\n' }).Split('\t');
                    List<string> tmpRowList = new List<string>();
                    for (int j = 0; j < colList.Length; j++)
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
    }
}
