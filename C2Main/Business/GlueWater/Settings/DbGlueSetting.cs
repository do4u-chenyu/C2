using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            DbMemberExcelColList = new List<string> { "网站网址", "认证账号", "最后登录IP", "登陆账号(多值##分隔)", "登陆密码(多值##分隔)" };

            DbWebColList = new string[] { "网站名称", "域名", "IP", "Refer对应Title", "Refer", "涉案金额", "涉赌人数", "赌博类型", "运营时间", "发现地市", "发现时间" };
            DbMemberColList = new string[] { "域名", "认证账号", "登陆IP", "登陆账号", "登陆密码", "安全码", "登陆地址" };
        }

        public override void InitDataTable()
        {
            //空文件的话，这些table都只有colume表头信息
            DbWebTable = GenDataTable(DbWebPath, DbWebColList);
            DbMemberTable = GenDataTable(DbMemberPath, DbMemberColList);

            RefreshHtmlTable();
        }

        public override string RefreshHtmlTable()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<tr name=\"row\">" +
                      "    <th>网站名称/域名/IP</th>" +
                      "    <th>Refer对应Title/Refer</th>" +
                      "    <th>涉案金额</th>" +
                      "    <th>涉赌人数</th>" +
                      "    <th>赌博类型/运营时间</th>" +
                      "    <th>发现地市/发现时间</th>" +
                      "</tr>"
                      );
            //先试试初始化
            foreach (DataRow dr in DbWebTable.Rows)
            {
                sb.Append(string.Format(
                            "<tr name=\"row\">" +
                            "   <td id=\"th0\">{0}<br><a onclick=\"Hello(this)\">{1}</a><br>{2}</td>" +
                            "   <td>{3}<br>{4}</td>" +
                            "   <td>{5}</td>" +
                            "   <td>{6}</td>" +
                            "   <td>{7}<br>{8}</td>" +
                            "   <td>{9}<br>{10}</td>" +
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

        public override string SearchInfo(string memeber)
        {
            DataRow[] rows = DbMemberTable.Select("域名='" + memeber + "'");
            if (rows.Length > 0)
                return (rows[0][0].ToString() + rows[0][1].ToString() + rows[0][2].ToString());
            else
                return "";
        }

        public override bool UpdateContent(string excelPath)
        {
            return DealWebContent(excelPath) && DealMemberContent(excelPath);
        }

        private bool DealWebContent(string excelPath)
        {
            ReadRst rrst1 = FileUtil.ReadExcel(excelPath, maxRow, "涉赌网站");
            if (rrst1.ReturnCode != 0 || rrst1.Result.Count == 0)
                return false;

            List<int> headIndex = IndexFilter(DbWebExcelColList, rrst1.Result);
            for (int i = 1; i < rrst1.Result.Count; i++)
            {
                if (headIndex.Max() > rrst1.Result[i].Count)
                    return false;
                List<string> resultList = ContentFilter(headIndex, rrst1.Result[i]);

                //这里要做判断了 对于web，url存在，替换掉
                DataRow[] rows = DbWebTable.Select("域名='" + resultList[1] + "'");
                if (rows.Length > 0)
                    DbWebTable.Rows.Remove(rows[0]);

                DbWebTable.Rows.Add(resultList.ToArray());
            }
            ReWriteResult(DbWebPath, DbWebTable);
            return true;
        }

        private bool DealMemberContent(string excelPath)
        {
            ReadRst rrst2 = FileUtil.ReadExcel(excelPath, maxRow, "涉赌网站人员");
            if (rrst2.ReturnCode != 0 || rrst2.Result.Count == 0)
                return false;

            List<int> headIndex2 = IndexFilter(DbMemberExcelColList, rrst2.Result);
            for (int i = 1; i < rrst2.Result.Count; i++)
            {
                if (headIndex2.Max() > rrst2.Result[i].Count)
                    return false;
                List<string> resultList = ContentFilter(headIndex2, rrst2.Result[i]);

                //这里要做判断了 对于member，url存在，比较是否完全一致
                DataRow[] rows = DbMemberTable.Select("域名='" + resultList[1] + "'");
                if (rows.Length == 0)
                    DbMemberTable.Rows.Add(resultList.ToArray());
                else
                {
                    foreach (DataRow row in rows)
                    {
                        List<string> rowContent = new List<string>();
                        for (int j = 0; j < DbMemberTable.Columns.Count; j++)
                            rowContent.Add(row[j].ToString());

                        if (rowContent.Count != 0 && string.Join("\t", rowContent) != string.Join("\t", resultList))
                            DbMemberTable.Rows.Add(resultList.ToArray());
                    }
                }

            }
            ReWriteResult(DbMemberPath, DbMemberTable);
            return true;
        }
    }
}
