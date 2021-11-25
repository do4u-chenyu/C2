using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace analysis.Method
{

    class KeyWordAnalysis
    {
        private string groupFilePath;
        private string keyFilePath;

        public KeyWordAnalysis(string groupFilePath, string keyFilePath)
        {
            this.groupFilePath = groupFilePath;
            this.keyFilePath = keyFilePath;
        }

        private List<string> GetKeyList()
        {
            List<string> keyList = new List<string>();

            FileStream fs_dir = null;
            StreamReader reader = null;

            try
            {
                int lineCount = 0;
                fs_dir = new FileStream(keyFilePath, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(fs_dir);
                string lineStr;
                while ((lineStr = reader.ReadLine()) != null)
                {
                    lineCount++;
                    if (lineCount == 1 || lineStr == string.Empty)
                        continue;
                    keyList.Add(lineStr.TrimEnd(new char[] { '\r', '\n' }));
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

            return keyList;
        }


        public DataTable GenDataTable()
        {
            DataTable dataTable = new DataTable("keyword");

            string[] columnArray = new string[] { "APPID", "QQNUM", "CAPTURE_TIME", "GROUPCODE", "CONTENT", "DATATYPE", "IP", "IPAREAID", "IP_CITY", "IP_PROVINCE", "DotIP", "Position", "keyCount" };
            foreach (string col in columnArray)
                dataTable.Columns.Add(col);

            FileStream fs_dir = null;
            StreamReader reader = null;

            List<string> keyList = GetKeyList();

            try
            {
                int lineCount = 0;
                fs_dir = new FileStream(groupFilePath, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(fs_dir);
                string lineStr;
                while ((lineStr = reader.ReadLine()) != null)
                {
                    lineCount++;
                    if (lineCount == 1 || lineStr == string.Empty)
                        continue;

                    List<string> rowList = new List<string>(lineStr.TrimEnd(new char[] { '\r', '\n' }).Split('\t'));
                    if (rowList.Count < 5)
                        continue;
                    string group = rowList[3];
                    string content = rowList[4];

                    StringBuilder sb = new StringBuilder();
                    foreach(string key in keyList)
                    {
                        if (key.Length == 0)
                            continue;

                        string tmpContent = content;
                        int count = (content.Length - tmpContent.Replace(key, "").Length) / key.Length;
                        if(count > 0)
                            sb.Append(key).Append(":").Append(count.ToString()).Append(",");
                    }
                    if (!string.IsNullOrEmpty(sb.ToString()))
                    {
                        rowList.Add(sb.ToString());
                        dataTable.Rows.Add(rowList.ToArray());
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

    }
}
