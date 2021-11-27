using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace analysis.Method
{
    class BankCardExtract
    {
        private string groupFilePath;

        public BankCardExtract(string groupFilePath)
        {
            this.groupFilePath = groupFilePath;
        }

        public DataTable GenDataTable()
        {
            DataTable dataTable = new DataTable("keyword");

            string[] columnArray = new string[] { "APPID", "QQNUM", "CAPTURE_TIME", "GROUPCODE", "CONTENT", "DATATYPE", "IP", "IPAREAID", "IP_CITY", "IP_PROVINCE", "DotIP", "Position", "BankCard" };
            foreach (string col in columnArray)
                dataTable.Columns.Add(col);

            FileStream fs_dir = null;
            StreamReader reader = null;

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

                    Match mc = Regex.Match(content, @"[^\d]([1-9]{1}\d{14,18})[^\d]");

                    if (mc.Success)
                    {
                        rowList.Add(mc.Groups[1].Value);
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
