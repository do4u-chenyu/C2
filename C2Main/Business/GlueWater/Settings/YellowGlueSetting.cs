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
    class YellowGlueSetting : BaseGlueSetting
    {
        private string YellowWebPath;
        private string YellowMemberPath;

        private List<string> YellowWebExcelColList;//excel里字段
        private List<string> YellowMemberExcelColList;

        private string[] YellowWebColList;//内存datatable里字段
        private string[] YellowMemberColList;

        private DataTable YellowWebTable;
        private DataTable YellowMemberTable;

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

            YellowWebExcelColList = new List<string> { "网站名称", "网站网址", "网站IP", "后台网址", "后台登陆账号", "后台登录密码", "用户数", "创建时间" };
            YellowMemberExcelColList = new List<string> { "网站网址", "认证账号", "IP地址", "登录名", "登录密码", "交易金额", "交易类型" };

            YellowWebColList = new string[] { "网站名称", "域名", "IP", "Refer", "登录账号", "登录密码", "涉黄人数", "运营时间" };
            YellowMemberColList = new string[] { "域名", "认证账号", "登陆IP", "登录账号", "登录密码", "涉案金额", "网站类型", };
            InitDataTable();
        }

        public override void InitDataTable()
        {
            YellowWebTable = GenDataTable(YellowWebPath, YellowWebColList);
            YellowMemberTable = GenDataTable(YellowMemberPath, YellowMemberColList);

            RefreshHtmlTable();
        }

        public override string RefreshHtmlTable(bool freshTitle = true)
        {
            StringBuilder sb = new StringBuilder();
            if (freshTitle)
                sb.Append("<tr name=\"title\">" +
                      "    <th>网站名称/域名/IP</th>" +
                      "    <th>Refer</th>" +
                      "    <th>登录账号/登录密码</th>" +
                      "    <th>涉黄人数<a class=\"arrow desc\" onmousedown=\"SortCol(this)\"></a></th>" +
                      "    <th>运营时间</th>" +
                      "</tr>"
                      );
            foreach (DataRow dr in YellowWebTable.Rows)
            {
                sb.Append(string.Format(
                            "<tr name=\"row\">" +
                            "   <td id=\"th0\">{0}<br><a onmousedown=\"ShowDetails(this)\" style=\"cursor:pointer\">{1}</a><br>{2}</td>" +
                            "   <td>{3}</td>" +
                            "   <td>{4}<br>{5}</td>" +
                            "   <td>{6}</td>" +
                            "   <td>{7}</td>" +
                            "</tr>",
                            dr["网站名称"].ToString(), dr["域名"].ToString(), dr["IP"].ToString(),
                            dr["Refer"].ToString(),
                            dr["登录账号"].ToString(), dr["登录密码"].ToString(),
                            dr["涉黄人数"].ToString(),
                            dr["运营时间"].ToString()
                ));
            }
            return sb.ToString();
        }

        public override DataTable SearchInfo(string item)
        {
            DataTable resTable = YellowMemberTable.Clone();
            DataRow[] rows = YellowMemberTable.Select("域名='" + item + "'");

            foreach (DataRow row in rows)
                resTable.Rows.Add(row.ItemArray);

            return resTable;
        }

        public override string UpdateContent(string excelPath)
        {
            ReadRst rrst1 = FileUtil.ReadExcel(excelPath, maxRow, "涉黄网站");
            if (rrst1.ReturnCode != 0)
                return rrst1.Message;

            ReadRst rrst2 = FileUtil.ReadExcel(excelPath, maxRow, "涉黄网站人员信息");
            if (rrst2.ReturnCode != 0)
                return rrst2.Message;

            if (DealWebContent(rrst1.Result) && DealMemberContent(rrst2.Result))
                return "数据添加成功";
            else
                return "非系统要求格式，请查看模板样例修改";
        }

        private bool DealWebContent(List<List<string>> contents)
        {
            if (contents.Count == 0)
                return false;

            List<int> headIndex = IndexFilter(YellowWebExcelColList, contents);

            for (int i = 1; i < contents.Count; i++)
            {
                if (headIndex.Max() > contents[i].Count)
                    return false;
                List<string> resultList = ContentFilter(headIndex, contents[i]);

                //这里要做判断了 对于web，url存在，替换掉
                DataRow[] rows = YellowWebTable.Select("域名='" + resultList[1] + "'");
                if (rows.Length > 0)
                    YellowWebTable.Rows.Remove(rows[0]);

                string tmpMember = resultList[YellowWebColList.ToList().IndexOf("涉黄人数")];
                resultList[YellowWebColList.ToList().IndexOf("涉黄人数")] = tmpMember == string.Empty ? "0" : tmpMember;
               
                YellowWebTable.Rows.Add(resultList.ToArray());
            }
            ReWriteResult(YellowWebPath, YellowWebTable);
            return true;
        }

        private bool DealMemberContent(List<List<string>> contents)
        {
            
            List<int> headIndex2 = IndexFilter(YellowMemberExcelColList, contents);
            List<List<string>> needAddList = new List<List<string>>();

            for (int i = 1; i < contents.Count; i++)
            {
                if (headIndex2.Max() > contents[i].Count)
                    return false;
                List<string> resultList = ContentFilter(headIndex2, contents[i]);

                //这里要做判断了 对于member，url存在，比较是否完全一致
                DataRow[] rows = YellowMemberTable.Select("域名='" + resultList[0] + "'");
                if (rows.Length == 0)
                    needAddList.Add(resultList);
                else
                {
                    foreach (DataRow row in rows)
                    {
                        List<string> rowContent = new List<string>();
                        for (int j = 0; j < YellowMemberTable.Columns.Count; j++)
                            rowContent.Add(row[j].ToString());

                        if (rowContent.Count != 0 && string.Join("\t", rowContent) != string.Join("\t", resultList))
                            needAddList.Add(resultList);
                    }
                }

            }
            foreach (List<string> li in needAddList)
            {
                string tmpMoney = li[YellowMemberColList.ToList().IndexOf("涉案金额")];
                li[YellowMemberColList.ToList().IndexOf("涉案金额")] = tmpMoney == string.Empty ? "0" : tmpMoney;

                YellowMemberTable.Rows.Add(li.ToArray());
            }
            ReWriteResult(YellowMemberPath, YellowMemberTable);
            return true;
        }
        public override void SortDataTableByCol(string col, string sortType)
        {
            YellowWebTable.DefaultView.Sort = col + " " + sortType;
            YellowWebTable = YellowWebTable.DefaultView.ToTable();
        }
    }
}
