using C2.Model;
using C2.Utils;
using Hive2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Database
{
    public class HiveDAOImpl: BaseDAOImpl
    {
        private static readonly LogUtil log = LogUtil.GetInstance("HiveDAOImpl");
        private string getUserSQL = @"show databases";
        private string getTablesSQL = @"use {0};show tables;";
        private string getTableContentSQL = @"use {0};select * from {1} limit {2}";
        //private string getColNameByTablesSQL;
        private string getColNameByTableSQL = "desc {0}";
        private string dataBaseName;
        public HiveDAOImpl(DatabaseItem dbi) : base(dbi)
        {
            this.dataBaseName = dbi.Schema;
        }
        public HiveDAOImpl(DataItem di) : base(di) { }
        public HiveDAOImpl(string name, string user, string pass, string host, string sid, string service, string port) : base(name, user, pass, host, sid, service, port) { }
        public override bool TestConn()
        {
            using (var conn = new Connection(this.Host, ConvertUtil.TryParseInt(this.Port),
                                                   this.User, this.Pass))
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                    return false;
                }
            }
        }
        public override bool ExecuteSQL(string sqlText, string outPutPath, int maxReturnNum = -1, int pageSize = 100000)
        {
            int pageIndex = 0;
            bool returnHeader = true;
            int totalRetuenNum = 0, subMaxNum;
            using (StreamWriter sw = new StreamWriter(outPutPath, false))
            {
                while (maxReturnNum == -1 ? true : totalRetuenNum < maxReturnNum)
                {
                    if (pageSize * pageIndex < maxReturnNum && pageSize * (pageIndex + 1) > maxReturnNum)
                        subMaxNum = maxReturnNum - pageIndex * pageSize;
                    else
                        subMaxNum = pageSize;
                    QueryResult contentAndNum = ExecuteHiveQL_Page(sqlText, pageSize, pageIndex, subMaxNum, returnHeader);

                    string result = contentAndNum.content;
                    totalRetuenNum += contentAndNum.returnNum;

                    if (returnHeader)
                    {
                        if (String.IsNullOrEmpty(result))
                            return false;
                        returnHeader = false;
                    }
                    if (String.IsNullOrEmpty(result))
                        break;
                    sw.Write(result);
                    pageIndex += 1;
                }
                sw.Flush();
            }
            return true;
        }


        private QueryResult ExecuteHiveQL_Page(string sqlText, int pageSize, int pageIndex, int maxNum, bool returnHeader)
        {
            /*
             * pageIndex start from 0.
             */
            QueryResult result;
            int returnNum = 0;

            string sqlPage = String.Format(@"select * from (select row_number() over () as rowno,tmp0.* from ({0}) tmp0) t where t.rowno  between {1} and {2}",
                                    sqlText,
                                    pageSize * (pageIndex),
                                    pageSize * (pageIndex)+maxNum);          
            try
            {
                // 去掉第一列，分页查询引入的
                result.content = Query(sqlPage).Substring(Query(sqlPage).IndexOf('\t')+1);
                result.returnNum = maxNum;
            }
            catch (Exception ex)
            {
                log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                result.content = string.Empty;
                result.returnNum = returnNum;
            }
            return result;
        }



        public override string Query(string sql, bool header=true)
        {
            StringBuilder sb = new StringBuilder(1024 * 16);
            try
            {
                using (Connection conn = new Connection(this.Host, ConvertUtil.TryParseInt(this.Port),
                                                   this.User, this.Pass))
                {

                    var cursor = conn.GetCursor();
                    cursor.Execute("use " + dataBaseName);
                    foreach (var s in sql.Split(';'))
                    {
                        if (!String.IsNullOrEmpty(s))
                            cursor.Execute(s.TrimEnd(';'));
                    }
                    var list = cursor.FetchMany(int.MaxValue);
                    if (header)
                    {
                        string headers;
                        if (list.Count > 0)
                        {
                            // 添加表头
                            headers = string.Join(OpUtil.DefaultFieldSeparator.ToString(), (list[0] as IDictionary<string, object>).Keys);
                            sb.Append(headers).Append(OpUtil.DefaultLineSeparator);
                        }
                    }
                    foreach (var item in list)
                    {
                        var dict = item as IDictionary<string, object>;
                        string tmp = string.Empty;

                        foreach (var key in dict.Keys)
                        {
                            tmp += dict[key].ToString() + OpUtil.DefaultFieldSeparator;
                        }
                        sb.Append(tmp.TrimEnd(OpUtil.DefaultFieldSeparator)).Append(OpUtil.DefaultLineSeparator);
                    }

                }
            }
            catch (Exception ex)
            {
                log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());   // 辅助工具类，showmessage不能放在外面
            }
            return sb.ToString().Trim(OpUtil.DefaultLineSeparator);
        }
        public override string LimitSQL(string sql)
        {
            return String.Format("select * from ({0})tmp limit {1}", sql, OpUtil.PreviewMaxNum);
        }
        public override string GetTablesSQL(string schema)
        {
            return String.Format(this.getTablesSQL, schema);
        }
        public override string GetColNameByTablesSQL(List<Table> tables)
        {
            return String.Empty;
        }
        public override string GetTableContentSQL(Table table, int maxNum)
        {
            return String.Format(getTableContentSQL, this.Schema, table.Name, maxNum);
        }
        public override string GetUserSQL()
        {
            return this.getUserSQL;
        }
        public override string DefaultSchema()
        {
            return String.IsNullOrEmpty(this.Schema) ? "default" : this.Schema;
        }
        public override string GetColNameByTableSQL(Table table)
        {
            return String.Format(this.getColNameByTableSQL, table.Name);
        }
    }
}
