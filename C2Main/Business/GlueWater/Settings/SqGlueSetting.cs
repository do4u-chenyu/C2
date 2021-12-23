using C2.IAOLab.IDInfoGet;
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

        private List<string> SqWebExcelColList;//涉枪论坛人员用户信息
        private List<string> SqWebExcelColList1;//部分涉枪论坛人员用户信息
        private List<string> SqWebExcelColList2;//涉枪人员信息
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

            //  涉枪论坛人员用户信息 & 涉枪人员信息
            SqWebExcelColList = new List<string> { "论坛名称", "论坛网址", "源IP", "认证账号", "目的IP", "登录名", "登录密码", "注册时间", "主题数", "回帖数" };
            SqWebExcelColList2 = new List<string> { "归属地", "发现时间" };
            SqWebExcelColList1 = new List<string> { "论坛网址","认证账号", "登录名","登录密码","主题数" };
            SqMemberExcelColList = new List<string> {"论坛网址","认证账号","登入名","密码", "主题", "发帖时间","内容关键词", "内容", "评论" };

            SqWebColList = new string[] { "论坛名称", "网址", "IP", "认证账号", "登录IP", "登录账号", "登录密码", "论坛注册时间", "主题数", "回帖数", "发现地市","发现时间" };
            SqMemberColList = new string[] { "论坛网址","用户名称", "发帖主题", "发帖时间", "涉枪关键词", "发帖信息", "回帖信息" };
            
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
            if (freshTitle)
                sb.Append("<tr name=\"title\">" +
                      "    <th>论坛名称/网址/IP</th>" +
                      "    <th>认证账号/登陆IP</th>" +
                      "    <th>登陆账号/登陆密码</th>" +
                      "    <th>论坛注册时间</th>" +
                      "    <th>主题数/回帖数</th>" +
                      "    <th>发现地市/发现时间<img src=\"..\\img\\arrow.png\" class=\"arrow desc\" onmousedown=\"SortCol(this)\"></img></th>" +
                      "</tr>"
                      );
            
        
            //先试试初始化
            foreach (DataRow dr in SqWebTable.Rows)
            {
                sb.Append(string.Format(
                            "<tr name=\"row\">" +
                            "   <td>{0}<br>{1}<br>{2}</td>" +
                            "   <td>{3}<br>{4}</td>" +
                            "   <td>{5}<br>{6}</td>" +
                            "   <td>{7}</td>" +
                            "   <td id=\"th0\"><a name=\"{1},{5}\" onmousedown=\"ShowDetailsMore(this)\" style=\"cursor:pointer\">主题数：{8}</a><br>回帖数：{9}</td>" +
                            "   <td>{10}<br>{11}</td>" +
                            "</tr>",
                            dr["论坛名称"].ToString(), dr["网址"].ToString(), dr["IP"].ToString(),
                            dr["认证账号"].ToString(), dr["登录IP"].ToString(),
                            dr["登录账号"].ToString(), dr["登录密码"].ToString(),
                            dr["论坛注册时间"].ToString(),
                            dr["主题数"].ToString(), dr["回帖数"].ToString(),
                            dr["发现地市"].ToString(), dr["发现时间"].ToString()
                ));
            }
            return sb.ToString();
        }

        public override DataTable SearchInfo(string memeber)
        {
            string url = memeber.Substring(0, memeber.IndexOf(","));
            string username = memeber.Substring(memeber.IndexOf(",") +1, memeber.Length - memeber.IndexOf(",") -1);
            DataTable resTable = SqMemberTable.Clone();
            DataRow[] rows = SqMemberTable.Select(
                //"主题数='" + memeber + "'"
                 "论坛网址='" + url + "' " +
                 "and 用户名称='" + username + "' " 
                );

            foreach (DataRow row in rows)
                resTable.Rows.Add(row.ItemArray);



            return resTable;
        }

        public override void SortDataTableByCol(string col, string sortType)
        {
            //html的标题为发现地市/发现时间，排序此列需要去掉发现地市
            if (col.Contains("发现时间"))
                col = "发现时间";
            SqWebTable.DefaultView.Sort = col + " " + sortType;
            SqWebTable = SqWebTable.DefaultView.ToTable();
        }

        public override string UpdateContent(string zipPath)
        {
            string excelPath = FindExcelFromZip(zipPath);
            if (!excelPath.EndsWith(".xlsx") && !excelPath.EndsWith(".xls"))
                return excelPath;

            ReadRst rrst1 = FileUtil.ReadExcel(excelPath, maxRow, "涉枪论坛人员用户信息");
            if (rrst1.ReturnCode != 0 || rrst1.Result.Count == 0)
                return rrst1.Message;

            
            ReadRst rrst2 = FileUtil.ReadExcel(excelPath, maxRow, "涉枪人员信息");
            if (rrst2.ReturnCode != 0 || rrst2.Result.Count == 0)
                return rrst2.Message;

            
            ReadRst rrst3 = FileUtil.ReadExcel(excelPath, maxRow, "论坛发帖情况");
            if (rrst3.ReturnCode != 0 || rrst3.Result.Count == 0)
                return rrst2.Message;


            if (DealWebContent(rrst1.Result, rrst2.Result) && DealMemberContent(rrst1.Result, rrst3.Result))
            {
                return "数据添加成功";
            }
                
            else
                return "非系统要求格式，请查看模板样例修改";
        }

        private bool DealWebContent(List<List<string>> contentsFirst,List<List<string>> contentSecond)
        {
            List<int> headIndex = IndexFilter(SqWebExcelColList, contentsFirst);
            List<int> tailIndex = IndexFilter(SqWebExcelColList2, contentSecond);


            int i, j;
            for (i = 1,j = 1; i < contentsFirst.Count && j<contentSecond.Count; i++,j++)
            {
                if (headIndex.Max() > contentsFirst[i].Count || tailIndex.Max() > contentSecond[j].Count)
                    return false;

                List<string> resultListFirst = ContentFilter(headIndex, contentsFirst[i]);
                List<string> resultListSecond = ContentFilter(tailIndex, contentSecond[j]);
                List<string> resultList = resultListFirst.Concat(resultListSecond).ToList();
                //这里要对地市编码做转换 字典映射
                resultList[10] = IDInfoGet.GetInstance().TransRegionCode(resultList[10]);

                DataRow[] rows = SqWebTable.Select(
                     "认证账号='" + resultList[3] + "' " +
                     "and 登录IP='" + resultList[4] + "' " +
                     "and 登录账号='" + resultList[5] + "'" +
                     "and 登录密码='" + resultList[6] + "'"
                    );
                if (rows.Length > 0)
                    SqWebTable.Rows.Remove(rows[0]);

                SqWebTable.Rows.Add(resultList.ToArray());
            }
            ReWriteResult(SqWebPath, SqWebTable);
            return true;
        }

        private bool DealMemberContent(List<List<string>> contentsFirst, List<List<string>> contentSecond)
        {
            List<int> headIndex = IndexFilter(SqWebExcelColList1, contentsFirst);
            List<int> tailIndex = IndexFilter(SqMemberExcelColList, contentSecond);


            int i, j;
            for (i = 1, j = 1; i < contentsFirst.Count && j < contentSecond.Count; i++, j++)
            {
                if (headIndex.Max() > contentsFirst[i].Count || tailIndex.Max() > contentSecond[j].Count)
                    return false;
            }

            if (contentsFirst.Count < contentSecond.Count) 
            {
                for (int k = 1; k < contentsFirst.Count; k++)
                {
                    List<string> resultListFirst = ContentFilter(headIndex, contentsFirst[k]);
                    for (int m = 1; m < contentSecond.Count; m++) 
                    {
                        List<string> resultListSecond = ContentFilter(tailIndex, contentSecond[m]);
                        if (resultListFirst[0] == resultListSecond[0] && resultListFirst[1] == resultListSecond[1] 
                            && resultListFirst[2] == resultListSecond[2])
                        {
                            //resultListSecond.Add(resultListFirst[4]);
                            resultListSecond.Remove(resultListSecond[1]);
                            resultListSecond.Remove(resultListSecond[2]);

                            //重复数据筛选条件
                            DataRow[] rows = SqMemberTable.Select(
                                "发帖信息='" + resultListSecond[5] + "' " +
                                "and 用户名称='" + resultListSecond[1] + "' " +
                                "and 发帖主题='" + resultListSecond[2] + "'" +
                                 "and 发帖时间='" + resultListSecond[3] + "'"
                                );
                            if (rows.Length > 0)
                                SqMemberTable.Rows.Remove(rows[0]);
                            
                            

                            SqMemberTable.Rows.Add(resultListSecond.ToArray());
                        }
                    }
                }
                ReWriteResult(SqMemberPath, SqMemberTable);
            }
            return true;
        }
    }
}
