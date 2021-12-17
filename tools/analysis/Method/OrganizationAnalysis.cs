using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace analysis.Method
{
    class OrganizationAnalysis
    {
        private string groupFilePath;
        private Dictionary<string, List<string>> memberGroupDict;
        //认为2个QQ号在2个群同时出现，认为这2个QQ属于统一组织,以QQ为字典key，加入群为value

        public OrganizationAnalysis(string groupFilePath)
        {
            this.groupFilePath = groupFilePath;
            this.memberGroupDict = new Dictionary<string, List<string>>();
        }

        public DataTable GenDataTable()
        {

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
                    string member = rowList[1];
                    if (memberGroupDict.ContainsKey(member) && !memberGroupDict[member].Contains(group))
                        memberGroupDict[member].Add(group);
                    else if(!memberGroupDict.ContainsKey(member))
                        memberGroupDict.Add(member, new List<string>() { group });

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

            return JudgeRelation();
        }

        private DataTable JudgeRelation()
        {
            DataTable dataTable = new DataTable("organization");

            string[] columnArray = new string[] { "QQNUM", "GROUPCODE", "GROUPCOUNT" };
            foreach (string col in columnArray)
                dataTable.Columns.Add(col);

            List<string> done = new List<string>();

            List<string> memberList = new List<string>(memberGroupDict.Keys);
            for (int i = 0; i < memberGroupDict.Count; i++)
            {
                string member = memberList[i];
                List<string> groupList = memberGroupDict[memberList[i]];
                groupList.Sort();

                if(groupList.Count == 1 || done.Contains(string.Join(",", groupList)))
                    continue;

                List<string> orgList = new List<string>() { member };

                for (int j = 0; j < memberGroupDict.Count; j++)
                {
                    if (j == i)
                        continue;

                    string compareMember = memberList[j];
                    List<string> compareGroupList = memberGroupDict[memberList[j]];
                    compareGroupList.Sort();

                    if (groupList.Intersect(compareGroupList).ToList().Count == groupList.Count)
                        orgList.Add(compareMember);
                }
                done.Add(string.Join(",", groupList));

                if(orgList.Count > 1)
                    dataTable.Rows.Add(new string[3] { string.Join(",",orgList), string.Join(",", groupList), groupList.Count.ToString() });

            }


            return dataTable;
        }
    }
}
