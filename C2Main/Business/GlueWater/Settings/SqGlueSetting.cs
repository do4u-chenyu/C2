using C2.IAOLab.IDInfoGet;
using C2.Utils;
using System;
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
        private string SqMemberPath2;

        private List<string> SqWebExcelColList;//涉枪论坛人员用户信息
        private List<string> SqWebExcelColList1;//部分涉枪论坛人员用户信息
        private List<string> SqWebExcelColList2;//涉枪人员信息
        private List<string> SqMemberExcelColList;
        private List<string> SqMemberExcelColList2;

        private string[] SqWebColList;//内存datatable里字段
        private string[] SqMemberColList;
        private string[] SqMemberColList2;

        private DataTable SqWebTable;
        private DataTable SqMemberTable;
        private DataTable SqMemberTableReply;
        private DataTable resTable = new DataTable();

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
            SqMemberPath2 = Path.Combine(txtDirectory, "SQ_member2.txt");

            //  涉枪论坛人员用户信息 & 涉枪人员信息 & 论坛发帖情况
            SqWebExcelColList = new List<string> { "论坛名称", "论坛网址", "源IP", "认证账号", "目的IP", "登录名", "登录密码", "注册时间", "主题数", "回帖数" };
            SqWebExcelColList2 = new List<string> { "归属地", "发现时间" };
            SqWebExcelColList1 = new List<string> { "论坛网址","认证账号", "登录名","登录密码","主题数" };
            SqMemberExcelColList = new List<string> {"论坛网址","认证账号","登入名","密码", "主题", "发帖时间","内容关键词", "内容", "评论" };
            SqMemberExcelColList2 = new List<string> { "论坛网址", "认证账号", "登入名", "密码", "主题","回帖内容" };

            SqWebColList = new string[] { "论坛名称", "网址", "IP", "认证账号", "登录IP", "登录账号", "登录密码", "论坛注册时间", "主题数", "回帖数", "发现地市","发现时间" };
            SqMemberColList = new string[] { "论坛网址","用户名称", "主题", "发帖时间", "关键词", "发帖信息", "回帖信息" };
            SqMemberColList2 = new string[] { "论坛网址", "用户名称", "主题", "回帖内容" };

            InitDataTable();
        }

        public override void InitDataTable()
        {
            //空文件的话，这些table都只有colume表头信息
            SqWebTable = GenDataTable(SqWebPath, SqWebColList);
            SqMemberTable = GenDataTable(SqMemberPath, SqMemberColList);
            SqMemberTableReply = GenDataTable(SqMemberPath2, SqMemberColList2);

            RefreshHtmlTable(resTable,true);
        }

        public override string RefreshHtmlTable(DataTable resTable,bool freshTitle)
        {
            
            StringBuilder sb = new StringBuilder();

            
            sb.Append("<tr name=\"title\">" +
                      "    <th>论坛名称/网址/IP</th>" +
                      "    <th>认证账号/登陆IP</th>" +
                      "    <th>登陆账号/登陆密码</th>" +
                      "    <th>论坛注册时间</th>" +
                      "    <th>主题数/回帖数</th>" +
                      "    <th>发现地市/发现时间<img src=\"..\\img\\arrow.png\" class=\"arrow desc\" onmousedown=\"SortCol(this)\"></img></th>" +
                      "    <th style=\"width:60px\">操作</th>" +
                      "</tr>"
                  );

            //删除操作，对表进行更新
            if (freshTitle == false)
                SqWebTable = resTable;

            //先试试初始化
            foreach (DataRow dr in SqWebTable.Rows)
            {
                string discoveryTime;
                string registerTime;
                try { registerTime = DateTime.FromOADate(double.Parse(dr["论坛注册时间"].ToString())).ToString().Replace("/", "-"); } catch { registerTime = dr["论坛注册时间"].ToString(); }
                try {discoveryTime = DateTime.FromOADate(double.Parse(dr["发现时间"].ToString())).ToString().Replace("/","-");} catch { discoveryTime = dr["发现时间"].ToString(); }
                    sb.Append(string.Format(
                           "<tr name=\"row\">" +
                           "   <td>{0}<br>{1}<br>{2}</td>" +
                           "   <td>认证账号：{3}<br>登录IP：{4}</td>" +
                           "   <td>登录账号：{5}<br>登录密码：{6}</td>" +
                           "   <td>{7}</td>" +
                           "   <td id=\"th0\"><a name=\"{1},{5}\" onmousedown=\"ShowDetailsTopic(this)\" style=\"cursor:pointer\">主题数：{8}</a><br><a name=\"{1},{5}\" onmousedown=\"ShowDetailsReply(this)\" style=\"cursor:pointer\">回帖数：{9}</a></td>" +
                           "   <td>{10}<br>{11}</td>" +
                           "   <td><a title =\"删除\" name=\"{1},{3},{4},{5},{6}\" onClick = \"data_del(this)\" href = \"javascript:;\" >删除</ a ></ td >" +
                           "</tr>",
                           dr["论坛名称"].ToString(), dr["网址"].ToString(), dr["IP"].ToString(),
                           dr["认证账号"].ToString(), dr["登录IP"].ToString(),
                           dr["登录账号"].ToString(), dr["登录密码"].ToString(),
                           registerTime,
                           dr["主题数"].ToString(), dr["回帖数"].ToString(),
                           dr["发现地市"].ToString(), discoveryTime
               ));
            }
            return sb.ToString();
        }

        public DataTable SqInformation(string member, DataTable table)
        {
            string url = member.Substring(0, member.IndexOf(","));
            string username = member.Substring(member.IndexOf(",") + 1, member.Length - member.IndexOf(",") - 1);
            DataTable resTable = table.Clone();
            DataRow[] rows = table.Select(
                 //"主题数='" + memeber + "'"
                 "论坛网址='" + url + "' " +
                 "and 用户名称='" + username + "' "
                );

            foreach (DataRow row in rows)
                resTable.Rows.Add(row.ItemArray);

            return resTable;
        }

        public override DataTable SearchInfo(string memeber)
        {
            return SqInformation(memeber, SqMemberTable);
        }

        public override DataTable DeleteInfo(string memeber)
        {
            //网址，认证账号，登录IP，登录账号，登录密码
            string address = memeber.Split(',')[0];
            string certAccount = memeber.Split(',')[1];
            string logIp = memeber.Split(',')[2];
            string logAccount = memeber.Split(',')[3];
            string logPassword = memeber.Split(',')[4];

            DataRow[] rows = SqWebTable.Select(
               "网址='" + address + "'" +
                "and 认证账号='" + certAccount + "' " +
                "and 登录IP='" + logIp + "' " +
                "and 登录账号='" + logAccount + "' " +
                "and 登录密码='" + logPassword + "' "
               );

            foreach (DataRow row in rows)
                SqWebTable.Rows.Remove(row);
            return SqWebTable;
        }

        public override DataTable SearchInfoReply(string memeber)
        {
            return SqInformation(memeber, SqMemberTableReply);
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
                return rrst3.Message;

            ReadRst rrst4 = FileUtil.ReadExcel(excelPath, maxRow, "论坛回帖情况");
            if (rrst4.ReturnCode != 0 || rrst4.Result.Count == 0)
                return rrst4.Message;

            if (DealWebContent(rrst1.Result, rrst2.Result) && DealMemberContent(rrst1.Result, rrst3.Result)
                && DealMemberContentReply(rrst1.Result, rrst4.Result))
            {
                BackupZip(zipPath);
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
                List<List<string>> tempResultList = new List<List<string>>();

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
                
                //SqWebTable.Rows.Add(resultList.ToArray());
                tempResultList.Add(resultList);
                SqWebTable = SortNewTable(tempResultList, SqWebTable);
            }
            ReWriteResult(SqWebPath, SqWebTable);
            return true;
        }

     
        private bool DealMemberContent(List<List<string>> contentsFirst, List<List<string>> contentSecond)
        {
            List<List<string>> tempResultList = new List<List<string>>();
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
                            try { resultListSecond[5] = DateTime.FromOADate(double.Parse(resultListSecond[5].ToString())).ToString().Replace("/","-"); } catch { }
                            //resultListSecond.Add(resultListFirst[4]);
                            resultListSecond.Remove(resultListSecond[1]);
                            resultListSecond.Remove(resultListSecond[2]);

                            //重复数据筛选条件
                            DataRow[] rows = SqMemberTable.Select(
                                "发帖信息='" + resultListSecond[5] + "' " +
                                "and 用户名称='" + resultListSecond[1] + "' " +
                                "and 主题='" + resultListSecond[2] + "'" +
                                 "and 发帖时间='" + resultListSecond[3] + "'"
                                );
                            if (rows.Length > 0)
                                SqMemberTable.Rows.Remove(rows[0]);


                            //SqMemberTable.Rows.Add(resultListSecond.ToArray());
                            tempResultList.Add(resultListSecond);
                            SqMemberTable = SortNewTable(tempResultList, SqMemberTable);
                        }
                    }
                }
                ReWriteResult(SqMemberPath, SqMemberTable);
            }
            return true;
        }

        private bool DealMemberContentReply(List<List<string>> contentsFirst, List<List<string>> contentSecond)
        {
            List<List<string>> tempResultList = new List<List<string>>();
            List<int> headIndex = IndexFilter(SqWebExcelColList1, contentsFirst);
            List<int> tailIndex = IndexFilter(SqMemberExcelColList2, contentSecond);


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
                        //论坛网址、认证账号、登录名
                        if (resultListFirst[0] == resultListSecond[0] && resultListFirst[1] == resultListSecond[1]
                            && resultListFirst[2] == resultListSecond[2])
                        {
                            //resultListSecond.Add(resultListFirst[4]);
                            resultListSecond.Remove(resultListSecond[1]);
                            resultListSecond.Remove(resultListSecond[2]);

                            
                            //重复数据筛选条件
                            DataRow[] rows = SqMemberTableReply.Select(
                                "论坛网址='" + resultListSecond[0] + "' " +
                                "and 用户名称='" + resultListSecond[1] + "' " +
                                "and 主题='" + resultListSecond[2] + "'" +
                                 "and 回帖内容='" + resultListSecond[3] + "'"
                                );
                            if (rows.Length > 0)
                                SqMemberTableReply.Rows.Remove(rows[0]);

                            //SqMemberTableReply.Rows.Add(resultListSecond.ToArray());
                            tempResultList.Add(resultListSecond);
                            SqMemberTableReply = SortNewTable(tempResultList, SqMemberTableReply);
                        }
                    }
                }
                ReWriteResult(SqMemberPath2, SqMemberTableReply);
            }
            return true;
        }
    }
}
