using C2.IAOLab.IDInfoGet;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace C2.Business.GlueWater.Settings
{
    class YellowGlueSetting : BaseGlueSetting
    {
        private string YellowWebPath;
        private string YellowMemberPath;

        private List<string> YellowPhotoMemberExcelColList;
        private List<string> YellowWebExcelColList;//excel里字段
        private List<string> YellowMemberExcelColList;

        private string[] YellowWebColList;//内存datatable里字段
        private string[] YellowMemberColList;

        private DataTable YellowWebTable;
        private DataTable YellowMemberTable;
        private DataTable resTable = new DataTable();


        private static YellowGlueSetting YellowGlueSettingInstance;
        public static YellowGlueSetting GetInstance()
        {
            if (YellowGlueSettingInstance == null)
            {
                YellowGlueSettingInstance = new YellowGlueSetting();
            }
            return YellowGlueSettingInstance;
        }

        public YellowGlueSetting() : base()
        {
            YellowWebPath = Path.Combine(txtDirectory, "Yellow_web.txt");
            YellowMemberPath = Path.Combine(txtDirectory, "Yellow_member.txt");

            YellowPhotoMemberExcelColList = new List<string> { "认证账号", "手机号", "QQ", "微信账号", "微信ID", "涉黄标签", "发现时间", "归属地" };
            YellowWebExcelColList = new List<string> { "网站名称", "网站网址", "网站IP", "备注", "用户数", "创建时间", "后台登陆账号", "安全码", "后台网址" };
            YellowMemberExcelColList = new List<string> { "网站网址", "认证账号", "登录名", "登录密码", "IP地址", "人员角色" };

            YellowWebColList = new string[] { "认证账号", "手机号", "QQ", "微信账号", "微信ID", "涉黄标签", "发现时间", "发现地市", "网站名称", "网站网址", "网站IP", "网站类型", "网站人数", "运营时间" };
            YellowMemberColList = new string[] { "网站", "认证账号", "登录账号", "登录密码", "安全码", "登陆IP", "登录地址", "人员角色" };
            InitDataTable();
        }

        public override void InitDataTable()
        {
            YellowWebTable = GenDataTable(YellowWebPath, YellowWebColList);
            YellowMemberTable = GenDataTable(YellowMemberPath, YellowMemberColList);
            //TempWebTable = GenDataTable(YellowWebPath, YellowWebColList);
            RefreshHtmlTable(resTable,true,true,true);
        }

        public override string RefreshHtmlTable(DataTable resTable,bool freshTitle, bool freshColumn,bool freshSort)
        {
            StringBuilder sb = new StringBuilder();
            if (freshSort == true && freshColumn == true)
                sb.Append("<tr name=\"title\">" +
                      "    <th>认证账号</th>" +
                      "    <th>身份信息</th>" +
                      "    <th>涉黄标签</th>" +
                      "    <th>发现时间<img src=\"..\\img\\arrow.png\" class=\"arrow desc\" onmousedown=\"SortCol(this)\"></img></th>" +
                      "    <th>发现地市</th>" +
                      "    <th>关联涉黄网站</th>" +
                      "    <th style=\"width:200px\"> 网站类型/网站人数/运营时间</th>" +
                      "    <th style=\"width:60px\">操作</th>" +
                      "</tr>"
                  );

            //删除操作，对表进行更新
            if (freshTitle == false)
                YellowWebTable = resTable;

            foreach (DataRow dr in YellowWebTable.Rows)
            {
                sb.Append(string.Format(
                            "<tr name=\"row\">" +
                            "   <td>{0}</td>" +
                            "   <td>{1}<br>{2}<br>{3}<br>{4}</td>" +
                            "   <td>{5}</td>" +
                            "   <td>{6}</td>" +
                            "   <td>{7}</td>" +
                            "   <td id=\"th0\">{8}<br><a onmousedown=\"ShowDetails(this)\" style=\"cursor:pointer\">{9}</a><br>{10}</td>" +
                            "   <td>{11}<br>{12}<br>{13}</td>" +
                            "   <td><a title =\"删除\" name=\"{9}\" onClick = \"data_del(this)\" href = \"javascript:;\" >删除</ a ></ td >" +
                            "</tr>",
                            dr["认证账号"].ToString(),
                            dr["手机号"].ToString(), dr["QQ"].ToString(), dr["微信账号"].ToString(), dr["微信ID"].ToString(),
                            dr["涉黄标签"].ToString(),
                            dr["发现时间"].ToString(),
                            dr["发现地市"].ToString(),
                            dr["网站名称"].ToString(), dr["网站网址"].ToString(), dr["网站IP"].ToString(),
                            dr["网站类型"].ToString(), dr["网站人数"].ToString(), dr["运营时间"].ToString()
                ));
            }
            return sb.ToString();
        }

        public override DataTable SearchInfo(string item)
        {
            DataTable resTable = YellowMemberTable.Clone();
            DataRow[] rows = YellowMemberTable.Select("网站='" + item + "'");

            foreach (DataRow row in rows)
                resTable.Rows.Add(row.ItemArray);

            return resTable;
        }

        public override DataTable DeleteInfo(string memeber)
        {
            DataRow[] rows = YellowWebTable.Select("网站网址='" + memeber + "'");
            foreach (DataRow row in rows)
                YellowWebTable.Rows.Remove(row);
            return YellowWebTable;
        }

        private List<List<String>> listData(List<List<String>> resultList)
        {
            return resultList;
        }

        private List<String> GetEmptylist(List<String> colList)
        {
            List<string> emptyList = new List<string> { };
            for (int i = 0; i < colList.Count; i++)
                emptyList.Add(string.Empty);
            return emptyList;
        }

        public override string UpdateContent(string zipPath)
        {
            string excelPath = FindExcelFromZip(zipPath);
            if (!excelPath.EndsWith(".xlsx") && !excelPath.EndsWith(".xls"))
                return excelPath;
            List<List<string>> WebMember = new List<List<string>>();
            List<List<string>> Web = new List<List<string>>();
            List<List<string>> Member = new List<List<string>>();

            ReadRst rrst0 = FileUtil.ReadExcel(excelPath, maxRow, "涉黄人员信息");
            if (rrst0.ReturnCode != 0)
                return rrst0.Message;

            ReadRst rrst1 = FileUtil.ReadExcel(excelPath, maxRow, "涉黄网站");
            if (rrst1.ReturnCode != 0)
                return rrst1.Message;

            ReadRst rrst2 = FileUtil.ReadExcel(excelPath, maxRow, "涉黄网站人员信息");
            if (rrst2.ReturnCode != 0)
                return rrst2.Message;

            if (DealWebMemberContent(rrst0.Result, WebMember) && DealWebContent(rrst1.Result, Web) && DealMemberContent(rrst2.Result, Member))
            {
                BackupZip(zipPath);
                WriteToTable(WebMember, Web, Member, zipPath);
                return "数据添加成功";
            }
            else
                return "非系统要求格式，请查看模板样例修改";
        }


        private bool DealWebMemberContent(List<List<string>> contents, List<List<string>> WebMember)
        {
            if (contents.Count == 0)
                return false;

            List<int> headIndex = IndexFilter(YellowPhotoMemberExcelColList, contents);

            for (int i = 1; i < contents.Count; i++)
            {
                if (headIndex.Max() > contents[i].Count)
                    return false;
                List<string> resultList = Trans(ContentFilter(headIndex, contents[i]));
                int city = YellowWebColList.ToList().IndexOf("发现地市");
                resultList[city] = IDInfoGet.GetInstance().TransRegionCode(resultList[city]);

                DataRow[] rows = YellowWebTable.Select("认证账号='" + resultList[0] + "'" +
                     "and 手机号='" + resultList[1] + "'" +
                     "and QQ='" + resultList[2] + "'" +
                     "and 微信账号='" + resultList[3] + "'" +
                     "and 微信ID='" + resultList[4] + "'" +
                     "and 发现时间='" + resultList[6] + "'");
                if (rows.Length > 0)
                    YellowWebTable.Rows.Remove(rows[0]);
                WebMember.Add(resultList);
            }
            listData(WebMember);
            return true;
        }

        private bool DealWebContent(List<List<string>> contents, List<List<string>> Web)
        {
            if (contents.Count == 0)
                return false;

            List<int> headIndex = IndexFilter(YellowWebExcelColList, contents);
            for (int i = 1; i < contents.Count; i++)
            {
                if (headIndex.Max() > contents[i].Count)
                    return false;
                List<string> resultList = ContentFilter(headIndex, contents[i]);
                int userNum = YellowWebColList.ToList().IndexOf("网站人数") - YellowPhotoMemberExcelColList.Count;
                string tmpMember = resultList[userNum];
                resultList[userNum] = tmpMember == string.Empty ? "0" : tmpMember;
                Web.Add(resultList);
            }
            listData(Web);
            return true;
        }

        private bool DealMemberContent(List<List<string>> contents, List<List<string>> Member)
        {
            List<int> headIndex2 = IndexFilter(YellowMemberExcelColList, contents);

            for (int i = 1; i < contents.Count; i++)
            {
                if (headIndex2.Max() > contents[i].Count)
                    return false;
                List<string> resultList = ContentFilter(headIndex2, contents[i]);
                Member.Add(resultList);

            }
            listData(Member);
            return true;
        }
        public override void SortDataTableByCol(string col, string sortType)
        {
            YellowWebTable.DefaultView.Sort = col + " " + sortType;
            YellowWebTable = YellowWebTable.DefaultView.ToTable();
        }

        private void WriteToTable(List<List<string>> WebMember, List<List<string>> Web, List<List<string>> Member, string zipPath)
        {
            List<object> returnList = MemberToTable(WebMember, Web, Member, zipPath);
            Dictionary<string, List<string>> webAndAuth = (Dictionary<string, List<string>>)returnList[0];
            List<int> removeWebMemberIndex = (List<int>)returnList[1];
            List<int> saveWebIndex = (List<int>)returnList[2];
            WebToTable(Web, WebMember, Member, saveWebIndex, webAndAuth, zipPath);
            WebMemberToTable(WebMember, removeWebMemberIndex);

        }

        private List<object> MemberToTable(List<List<string>> WebMember, List<List<string>> Web, List<List<string>> Member, string zipPath)
        {
            List<object> returnList = new List<object>();
            Dictionary<string, List<string>> webAndAuth = new Dictionary<string, List<string>>();
            List<int> removeWebMemberIndex = new List<int>();
            List<string> removeList = new List<string>();
            webAndAuth.Add("remove", removeList);
            List<int> saveWebIndex = new List<int>();
            List<List<string>> rowsList = new List<List<string>>();

            foreach (List<string> memberList in Member)
            {
                List<string> authList = new List<string>();
                if (webAndAuth.ContainsKey(memberList[0]) && memberList[1] != string.Empty)
                    webAndAuth[memberList[0]].Add(memberList[1]);
                else if (memberList[1] != string.Empty)
                {
                    authList.Add(memberList[1]);
                    webAndAuth.Add(memberList[0], authList);
                }
                memberList.Insert(4, string.Empty);
                memberList.Insert(6, string.Empty);

                for (int i = 0; i < Web.Count(); i++)
                {
                    if (memberList[0] != Web[i][1])
                        continue;
                    saveWebIndex.Add(i);
                    if (memberList[2] == Web[i][6])
                    {
                        memberList[4] = Web[i][7];
                        memberList[6] = Web[i][8];
                    }
                    for (int j = 0; j < WebMember.Count(); j++)
                    {
                        if (memberList[1] == WebMember[j][0] && memberList[1] != string.Empty)
                        {
                            removeWebMemberIndex.Add(j);
                            List<string> cutWeb = new List<string> { Web[i][0], Web[i][1], Web[i][2], Web[i][3], Web[i][4], Web[i][5] };
                            int len = WebMember[j].Count;
                            WebMember[j].AddRange(cutWeb);

                            WebMember[j][6] = WebMember[j][6] == string.Empty ? TimeTrans(zipPath) : WebMember[j][6].Replace(" 00:00:00", "");
                            WebMember[j][13] = WebMember[j][13].Replace(" 00:00:00", "");
                            WebMember[j][7] = WebMember[j][7] == string.Empty ? CityTrans(zipPath) : WebMember[j][7];

                            DataRow[] webRows = YellowWebTable.Select("网站网址='" + WebMember[j][9] + "'");
                            rowsList = TableFilter(webRows, rowsList, WebMember[j], YellowWebTable);
                            YellowWebTable = SortNewTable(rowsList, YellowWebTable);
                            ReWriteResult(YellowWebPath, YellowWebTable);

                            WebMember[j].RemoveRange(len, cutWeb.Count());
                            webAndAuth[memberList[0]].Remove(memberList[1]);
                            webAndAuth["remove"].Add(memberList[1]);
                            if (saveWebIndex.Contains(i))
                                saveWebIndex.Remove(i);
                        }
                        else if (!saveWebIndex.Contains(i) && webAndAuth[memberList[0]].Contains(memberList[1]))
                            saveWebIndex.Add(i);
                    }
                }
                if (webAndAuth.ContainsKey(memberList[0]) && webAndAuth[memberList[0]].Count == 0)
                    webAndAuth.Remove(memberList[0]);
                DataRow[] rows = YellowMemberTable.Select("网站='" + memberList[0] + "'");
                List<List<string>> YellowMemberList = new List<List<string>>();
                YellowMemberList = TableFilter(rows, YellowMemberList, memberList, YellowMemberTable);
                foreach (List<string> YellowMember in YellowMemberList)
                    YellowMemberTable.Rows.Add(YellowMember.ToArray());
                ReWriteResult(YellowMemberPath, YellowMemberTable);
            }
            returnList.Add(webAndAuth);
            returnList.Add(removeWebMemberIndex);
            returnList.Add(saveWebIndex);
            return returnList;
        }


        private void WebMemberToTable(List<List<string>> WebMember, List<int> removeWebMemberIndex)
        {
            for (int j = 0; j < WebMember.Count(); j++)
            {
                List<String> yellowWebEmpty = new List<string>();
                if (removeWebMemberIndex.Contains(j))
                    continue;
                List<string> WebExcelCol = new List<string> { "网站名称", "网站网址", "网站IP", "备注", "用户数", "创建时间" };
                yellowWebEmpty = GetEmptylist(WebExcelCol);
                WebMember[j].AddRange(yellowWebEmpty);
                YellowWebTable.Rows.Add(WebMember[j].ToArray());
                ReWriteResult(YellowWebPath, YellowWebTable);
            }
        }

        private void WebToTable(List<List<string>> Web, List<List<string>> WebMember, List<List<string>> Member, List<int> saveWebIndex, Dictionary<string, List<string>> webAndAuth, string zipPath)
        {
            List<List<string>> rowsList = new List<List<string>>();
            for (int i = 0; i < Web.Count(); i++)
            {
                DataRow[] rows = YellowMemberTable.Select("网站='" + Web[i][1] + "'");
                if (rows.Length == 0)
                    continue;
                foreach (DataRow row in rows)
                {
                    List<string> authList = new List<string>();
                    if (webAndAuth.ContainsKey(row[0].ToString()) && !webAndAuth["remove"].Contains(row[1].ToString()))
                    {
                        if (webAndAuth[row[0].ToString()].Contains(row[1].ToString()))
                            continue;
                        webAndAuth[row[0].ToString()].Add(row[1].ToString());
                    }
                    else if (!webAndAuth.ContainsKey(row[0].ToString()))
                    {
                        authList.Add(row[1].ToString());
                        webAndAuth[row[0].ToString()] = authList;
                    }
                }
                List<String> yellowPhotoEmpty = new List<string>();
                if (saveWebIndex.Count() == 0 || (saveWebIndex.Count() != 0 && !saveWebIndex.Contains(i)))
                    continue;

                yellowPhotoEmpty = GetEmptylist(YellowPhotoMemberExcelColList);
                yellowPhotoEmpty.AddRange(Web[i]);
                yellowPhotoEmpty.RemoveRange(yellowPhotoEmpty.Count() - 3, 3);
                foreach (string web in webAndAuth.Keys.ToList())
                {
                    if (web == Web[i][1] && webAndAuth[web].Count() > 2)
                        yellowPhotoEmpty[0] = webAndAuth[web][0] + "<br>" + webAndAuth[web][1] + "<br>" + "...";
                    else if (web == Web[i][1] && webAndAuth[web].Count() == 2)
                        yellowPhotoEmpty[0] = webAndAuth[web][0] + "<br>" + webAndAuth[web][1];
                    else if (web == Web[i][1] && webAndAuth[web].Count() < 2)
                        yellowPhotoEmpty[0] = webAndAuth[web][0];
                }
                if (yellowPhotoEmpty[9] != string.Empty && yellowPhotoEmpty[5] == string.Empty)
                    yellowPhotoEmpty[5] = "涉黄网站人员";

                if (yellowPhotoEmpty[9] != string.Empty && yellowPhotoEmpty[6] == string.Empty)
                    yellowPhotoEmpty[6] = TimeTrans(zipPath);

                yellowPhotoEmpty[13] = yellowPhotoEmpty[13].Replace(" 00:00:00", "");

                yellowPhotoEmpty[7] = WebMember.Count > 0 ? WebMember[0][7] : yellowPhotoEmpty[7];
                if (yellowPhotoEmpty[9] != string.Empty && yellowPhotoEmpty[7] == string.Empty)
                    yellowPhotoEmpty[7] = CityTrans(zipPath);

                yellowPhotoEmpty = Trans(yellowPhotoEmpty);
                DataRow[] webRows = YellowWebTable.Select("网站网址='" + yellowPhotoEmpty[9] + "'");
                rowsList = TableFilter(webRows, rowsList, yellowPhotoEmpty, YellowWebTable);
                YellowWebTable = SortNewTable(rowsList, YellowWebTable);
                ReWriteResult(YellowWebPath, YellowWebTable);
            }
        }
        private List<string> Trans(List<string> resultList)
        {
            resultList[1] = "手机号: " + resultList[1];
            resultList[2] = "QQ: " + resultList[2];
            resultList[3] = "微信帐号: " + resultList[3];
            resultList[4] = "微信ID: " + resultList[4];
            return resultList;
        }

        private string TimeTrans(string zipPath)
        {
            string findTime = string.Empty;
            string fileName = Path.GetFileNameWithoutExtension(zipPath);
            Match regResult = Regex.Match(fileName, @"-(?<time>[0-9]{4}[0-9]{2}[0-9]{2})-");

            if (regResult.Success)
            {
                findTime = regResult.Groups["time"].ToString();
                findTime = DateTime.ParseExact(findTime, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture, DateTimeStyles.None).ToString().Replace(" 0:00:00", "");
            }
            else
                findTime = DateTime.Now.ToString().Replace(" 00:00:00", "");
            findTime = findTime.Replace("/", "-");
            return findTime;
        }

        private string CityTrans(string zipPath)
        {
            string findCity = string.Empty;
            string fileName = Path.GetFileNameWithoutExtension(zipPath);
            Match regResult = Regex.Match(fileName, @"(?<city>.*)-yellow");

            if (regResult.Success)
                findCity = regResult.Groups["city"].ToString();
            return findCity;
        }

        private List<List<string>> TableFilter(DataRow[] data, List<List<string>> rowsList, List<string> memberList, DataTable dataTable)
        {
            if (data.Length == 0)
                rowsList.Add(memberList);
            else
            {
                List<string> rowContentList = new List<string>();
                foreach (DataRow row in data)
                {
                    List<string> rowContent = new List<string>();
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                        rowContent.Add(row[j].ToString());
                    rowContentList.Add(string.Join("\t", rowContent));
                }

                if (!rowContentList.Contains(string.Join("\t", memberList)))
                    rowsList.Add(memberList);
            }
            return rowsList;
        }
    }
}
