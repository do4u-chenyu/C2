using C2.Utils;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace C2.Business.GlueWater.Settings
{
    class SqGlueSetting : BaseGlueSetting
    {
        private string SqWebPath;
        private string SqMemberPath;

        private List<string> SqWebExcelColList;//excel里字段
        private List<string> SqMemberExcelColList;

        private string[] SqWebColList;//内存datatable里字段
        private string[] SqMemberColList;

        private DataTable SqWebTable;
        private DataTable SqMemberTable;

        private static SqGlueSetting SqGlueSettingInstance;
        public static SqGlueSetting GetInstance()
        {
            if (SqGlueSettingInstance == null)
            {
                SqGlueSettingInstance = new SqGlueSetting();
            }
            return SqGlueSettingInstance;
        }

        public SqGlueSetting() : base()
        {
            
            SqWebPath = Path.Combine(txtDirectory, "SQ_web.txt");
            SqMemberPath = Path.Combine(txtDirectory, "SQ_member.txt");

            /*
            SqWebExcelColList = new List<string> { "网站名称", "网站网址", "网站Ip", "REFER标题", "REFER", "金额", "用户数", "赌博类型(多值##分隔)", "开始运营时间", "归属地", "发现时间" };
            SqMemberExcelColList = new List<string> { "网站网址", "认证账号", "最后登录IP", "登陆账号(多值##分隔)", "登陆密码(多值##分隔)" };

            SqWebColList = new string[] { "网站名称", "域名", "IP", "Refer对应Title", "Refer", "涉案金额", "涉赌人数", "赌博类型", "运营时间", "发现地市", "发现时间" };
            SqMemberColList = new string[] { "域名", "认证账号", "登陆IP", "登陆账号", "登陆密码", "安全码", "登陆地址" };
            */

            
            //  涉枪论坛人员用户信息 & 涉枪人员信息
            SqWebExcelColList = new List<string> { "论坛名称", "论坛网址", "源IP", "认证账号", "目的IP", "登录名", "登录密码", "注册时间", "主题数", "回帖数", "归属地","发现时间" };
            SqMemberExcelColList = new List<string> { "网站网址", "认证账号", "最后登录IP", "登陆账号(多值##分隔)", "登陆密码(多值##分隔)", "安全码", "登陆地址(多值##分隔)" };

            SqWebColList = new string[] { "论坛名称", "网址", "IP", "认证账号", "登录IP", "登录账号", "登录密码", "论坛注册时间", "主题数", "回帖数", "发现地市","发现时间" };
            SqMemberColList = new string[] { "域名", "认证账号", "登陆IP", "登陆账号", "登陆密码", "安全码", "登陆地址" };
            
            InitDataTable();
        }

        public override void InitDataTable()
        {
            //空文件的话，这些table都只有colume表头信息
            SqWebTable = GenDataTable(SqWebPath, SqWebColList);
            SqMemberTable = GenDataTable(SqMemberPath, SqMemberColList);

            RefreshHtmlTable();
        }

        public override string RefreshHtmlTable(bool freshTitle = true)
        {
            
            StringBuilder sb = new StringBuilder();
            sb.Append("<tr name=\"row\">" +
                      "    <th>论坛名称/网址/IP</th>" +
                      "    <th>认证账号/登陆IP</th>" +
                      "    <th>登陆账号/登陆密码</th>" +
                      "    <th>论坛注册时间</th>" +
                      "    <th>主题数/回帖数</th>" +
                      "    <th>发现地市/发现时间</th>" +
                      "</tr>"
                      );
            return sb.ToString();
            

            /*
            StringBuilder sb = new StringBuilder();

            if (freshTitle)
                sb.Append("<tr name=\"title\">" +
                      "    <th>网站名称/域名/IP</th>" +
                      "    <th style=\"width:200px\"> Refer对应Title/Refer</th>" +
                      "    <th>涉案金额<a class=\"arrow desc\" onmousedown=\"SortCol(this)\"></a></th>" +
                      "    <th>涉赌人数<a class=\"arrow desc\" onmousedown=\"SortCol(this)\"></a></th>" +
                      "    <th>赌博类型/运营时间</th>" +
                      "    <th>发现地市/发现时间</th>" +
                      "</tr>"
                      );

            //先试试初始化
            foreach (DataRow dr in SqWebTable.Rows)
            {
                sb.Append(string.Format(
                            "<tr name=\"row\">" +
                            "   <td id=\"th0\">{0}<br><a onmousedown=\"ShowDetails(this)\" style=\"cursor:pointer\">{1}</a><br>{2}</td>" +
                            "   <td  style=\"width:150px\">{3}<br>{4}</td>" +
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

            */

        }

        public override DataTable SearchInfo(string memeber)
        {
            DataTable resTable = SqMemberTable.Clone();
            DataRow[] rows = SqMemberTable.Select("域名='" + memeber + "'");

            foreach (DataRow row in rows)
                resTable.Rows.Add(row.ItemArray);

            return resTable;
        }

        public override void SortDataTableByCol(string col, string sortType)
        {
            SqWebTable.DefaultView.Sort = col + " " + sortType;
            SqWebTable = SqWebTable.DefaultView.ToTable();
        }

        public override string UpdateContent(string excelPath)
        {
            //return DealWebContent(excelPath) && DealMemberContent(excelPath);

            ReadRst rrst1 = FileUtil.ReadExcel(excelPath, maxRow, "涉赌网站");
            if (rrst1.ReturnCode != 0 || rrst1.Result.Count == 0)
                return rrst1.Message;

            ReadRst rrst2 = FileUtil.ReadExcel(excelPath, maxRow, "涉赌网站人员");
            if (rrst2.ReturnCode != 0 || rrst2.Result.Count == 0)
                return rrst2.Message;

            if (DealWebContent(rrst1.Result) && DealMemberContent(rrst2.Result))
                return "文件上传成功";
            else
                return "文件格式不正确";
        }

        private bool DealWebContent(List<List<string>> contents)
        {
            List<int> headIndex = IndexFilter(SqWebExcelColList, contents);

            for (int i = 1; i < contents.Count; i++)
            {
                if (headIndex.Max() > contents[i].Count)
                    return false;
                List<string> resultList = ContentFilter(headIndex, contents[i]);

                //这里要做判断了 对于web，url存在，替换掉
                DataRow[] rows = SqWebTable.Select("域名='" + resultList[1] + "'");
                if (rows.Length > 0)
                    SqWebTable.Rows.Remove(rows[0]);

                SqWebTable.Rows.Add(resultList.ToArray());
            }
            ReWriteResult(SqWebPath, SqWebTable);
            return true;
        }

        private bool DealMemberContent(List<List<string>> contents)
        {
            List<int> headIndex = IndexFilter(SqMemberExcelColList, contents);
            List<List<string>> needAddList = new List<List<string>>();

            for (int i = 1; i < contents.Count; i++)
            {
                if (headIndex.Max() > contents[i].Count)
                    return false;
                List<string> resultList = ContentFilter(headIndex, contents[i]);

                //这里要做判断了 对于member，url存在，比较是否完全一致
                DataRow[] rows = SqMemberTable.Select("域名='" + resultList[0] + "'");
                if (rows.Length == 0)
                    needAddList.Add(resultList);
                else
                {
                    foreach (DataRow row in rows)
                    {
                        List<string> rowContent = new List<string>();
                        for (int j = 0; j < SqMemberTable.Columns.Count; j++)
                            rowContent.Add(row[j].ToString());

                        if (rowContent.Count != 0 && string.Join("\t", rowContent) != string.Join("\t", resultList))
                            needAddList.Add(resultList);
                    }
                }

            }
            foreach (List<string> li in needAddList)
                SqMemberTable.Rows.Add(li.ToArray());

            ReWriteResult(SqMemberPath, SqMemberTable);
            return true;
        }


    }
}
