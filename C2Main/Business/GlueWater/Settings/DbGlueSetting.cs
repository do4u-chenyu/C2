using C2.IAOLab.IDInfoGet;
using C2.Utils;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace C2.Business.GlueWater.Settings
{
    class DbGlueSetting : BaseGlueSetting
    {
        private string DbWebPath;
        private string DbMemberPath;

        private List<string> DbWebExcelColList;//excel里字段
        private List<string> DbMemberExcelColList;

        private string[] DbWebColList;//内存datatable里字段
        private string[] DbMemberColList;

        private DataTable DbWebTable;
        private DataTable DbMemberTable;
        private DataTable resTable = new DataTable();
        
        private static DbGlueSetting DbSettingInstance;
        public static DbGlueSetting GetInstance()
        {
            if (DbSettingInstance == null)
            {
                DbSettingInstance = new DbGlueSetting();
            }
            return DbSettingInstance;
        }

        public DbGlueSetting() : base()
        {
            DbWebPath = Path.Combine(txtDirectory, "DB_web.txt");
            DbMemberPath = Path.Combine(txtDirectory, "DB_member.txt");
            
            DbWebExcelColList = new List<string> { "网站名称", "网站网址", "网站Ip", "REFER标题", "REFER", "金额", "用户数", "赌博类型(多值##分隔)", "开始运营时间", "归属地", "发现时间" };
            DbMemberExcelColList = new List<string> { "网站网址", "认证账号", "最后登录IP", "登陆账号(多值##分隔)", "登陆密码(多值##分隔)", "安全码", "登陆地址(多值##分隔)" };

            DbWebColList = new string[] { "网站名称", "域名", "IP", "Refer对应Title", "Refer", "涉案金额", "涉赌人数", "赌博类型", "运营时间", "发现地市", "发现时间" };
            DbMemberColList = new string[] { "域名", "认证账号", "登陆IP", "登陆账号", "登陆密码", "安全码", "登陆地址" };
        }

        public override void InitDataTable()
        {
            //空文件的话，这些table都只有colume表头信息
            DbWebTable = GenDataTable(DbWebPath, DbWebColList);
            DbMemberTable = GenDataTable(DbMemberPath, DbMemberColList);

            RefreshHtmlTable(resTable,true,true,true);
        }

        
        public override string RefreshHtmlTable(DataTable resTable,bool freshTitle, bool freshColumn, bool freshSort)
        {
            StringBuilder sb = new StringBuilder();
            if (freshSort == true && freshColumn == true)
                sb.Append("<tr name=\"title\">" +
                          "    <th>网站名称/域名/IP</th>" +
                          "    <th style=\"width:200px\"> Refer对应Title/Refer</th>" +
                          "    <th style=\"width:80px\">涉案金额<img src=\"..\\img\\arrow.png\" class=\"arrow desc\" onmousedown=\"SortCol(this)\"></img></th>" +
                          "    <th style=\"width:80px\">涉赌人数<img src=\"..\\img\\arrow.png\" class=\"arrow desc\" onmousedown=\"SortCol(this)\"></img></th>" +
                          "    <th>赌博类型/运营时间</th>" +
                          "    <th>发现地市/发现时间<img src=\"..\\img\\arrow.png\" class=\"arrow desc\" onmousedown=\"SortCol(this)\"></img></th>" +
                          "    <th style=\"width:60px\">操作</th>" +
                          "</tr>"
                  );
           
            //删除操作，对表进行更新
            if(freshTitle == false)
                DbWebTable = resTable;

            //先试试初始化
            foreach (DataRow dr in DbWebTable.Rows)
            {
                sb.Append(string.Format(
                            "<tr name=\"row\">" +
                            "   <td id=\"th0\">{0}<br><a onmousedown=\"ShowDetails(this)\" style=\"cursor:pointer\">{1}</a><br>{2}</td>" +
                            "   <td>{3}<br>{4}</td>" +
                            "   <td id=\"th2\">{5}</td>" +
                            "   <td id=\"th2\">{6}</td>" +
                            "   <td>{7}<br>{8}</td>" +
                            "   <td>{9}<br>{10}</td>" +
                            "   <td><a title =\"删除\" name=\"{1}\" onClick = \"data_del(this)\" href = \"javascript:;\" >删除</ a ></ td >" +
                            "</tr>",
                            dr["网站名称"].ToString(), dr["域名"].ToString(), dr["IP"].ToString(),
                            dr["Refer对应Title"].ToString(), dr["Refer"].ToString(),
                            dr["涉案金额"].ToString(),
                            dr["涉赌人数"].ToString(),
                            dr["赌博类型"].ToString(), dr["运营时间"].ToString(),
                            dr["发现地市"].ToString(), dr["发现时间"].ToString() 
                ));
            }
            return sb.ToString();
        }

        
        public override DataTable SearchInfo(string memeber)
        {
            DataTable resTable = DbMemberTable.Clone();
            DataRow[] rows = DbMemberTable.Select("域名='" + memeber + "'");

            foreach (DataRow row in rows)
                resTable.Rows.Add(row.ItemArray);

            return resTable;
        }
        
        public override DataTable DeleteInfo(string memeber)
        {
            DataRow[] rows = DbWebTable.Select("域名='" + memeber + "'");
            foreach (DataRow row in rows)
                DbWebTable.Rows.Remove(row);
            return DbWebTable;
        }
        

        public override void SortDataTableByCol(string col, string sortType)
        {
            //html的标题为发现地市/发现时间，排序此列需要去掉发现地市
            if (col.Contains("发现时间"))
                col = "发现时间";
            try { DbWebTable.DefaultView.Sort = col + " " + sortType; } catch { }
            DbWebTable = DbWebTable.DefaultView.ToTable();
        }

        public override string UpdateContent(string zipPath)
        {
            string excelPath = FindExcelFromZip(zipPath);
            if (!excelPath.EndsWith(".xlsx") && !excelPath.EndsWith(".xls"))
                return excelPath;


            ReadRst rrst1 = FileUtil.ReadExcel(excelPath, maxRow, "涉赌网站");
            if (rrst1.ReturnCode != 0)
                return rrst1.Message;

            ReadRst rrst2 = FileUtil.ReadExcel(excelPath, maxRow, "涉赌网站人员");
            if (rrst2.ReturnCode != 0)
                return rrst2.Message;

            if (DealWebContent(rrst1.Result) && DealMemberContent(rrst2.Result))
            {
                BackupZip(zipPath);
                return "数据添加成功";
            }
                
            else
                return "非系统要求格式，请查看模板样例修改";
        }

        private bool DealWebContent(List<List<string>> contents)
        {
            if (contents.Count == 0)
                return false;

            List<int> headIndex = IndexFilter(DbWebExcelColList, contents);

            for (int i = 1; i < contents.Count; i++)
            {
                List<List<string>> tempResultList = new List<List<string>>();
                if (headIndex.Max() > contents[i].Count)
                    return false;
                List<string> resultList = ContentFilter(headIndex, contents[i]);
                //这里要对地市编码做转换
                resultList[9] = IDInfoGet.GetInstance().TransRegionCode(resultList[9]);

                //这里要做判断了 对于web，url存在，替换掉
                DataRow[] rows = DbWebTable.Select("域名='" + resultList[1] + "'");
                if (rows.Length > 0)
                    DbWebTable.Rows.Remove(rows[0]);

                //由于人员和金额可能为空，需要额外判断
                string tmpMember = resultList[DbWebColList.ToList().IndexOf("涉赌人数")];
                resultList[DbWebColList.ToList().IndexOf("涉赌人数")] = tmpMember == string.Empty ? "0" : tmpMember;
                string tmpMoney = resultList[DbWebColList.ToList().IndexOf("涉案金额")];
                resultList[DbWebColList.ToList().IndexOf("涉案金额")] = tmpMoney == string.Empty ? "0" : tmpMoney;

                //DbWebTable.Rows.Add(resultList.ToArray());
                tempResultList.Add(resultList);
                DbWebTable = SortNewTable(tempResultList, DbWebTable);
            }
            ReWriteResult(DbWebPath, DbWebTable);
            return true;
        }

        private bool DealMemberContent(List<List<string>> contents)
        {
            List<List<string>> tempResultList = new List<List<string>>();
            if (contents.Count == 0)
                return false;

            List<int> headIndex = IndexFilter(DbMemberExcelColList, contents);
            List<List<string>> needAddList = new List<List<string>>();

            for (int i = 1; i < contents.Count; i++)
            {
                if (headIndex.Max() > contents[i].Count)
                    return false;
                List<string> resultList = ContentFilter(headIndex, contents[i]);

                //这里要做判断了 对于member，url存在，比较是否完全一致
                DataRow[] rows = DbMemberTable.Select("域名='" + resultList[0] + "'");
                if (rows.Length == 0)
                    needAddList.Add(resultList);
                else
                {
                    List<string> rowContentList = new List<string>();
                    foreach (DataRow row in rows)
                    {
                        List<string> rowContent = new List<string>();
                        for (int j = 0; j < DbMemberTable.Columns.Count; j++)
                            rowContent.Add(row[j].ToString());

                        rowContentList.Add(string.Join("\t", rowContent));

                    }
                    if (!rowContentList.Contains(string.Join("\t", resultList)))
                        needAddList.Add(resultList);
                }

            }
            foreach(List<string> li in needAddList)
                tempResultList.Add(li); 
            DbMemberTable = SortNewTable(tempResultList, DbMemberTable);

            ReWriteResult(DbMemberPath, DbMemberTable);
            return true;
        }
    }
}
