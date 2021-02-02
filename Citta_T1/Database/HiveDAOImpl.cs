using C2.Model;
using C2.Utils;
using Hive2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2.Database
{
    public class HiveDAOImpl : BaseDAOImpl
    {
        private static readonly LogUtil log = LogUtil.GetInstance("HiveDAOImpl");
        private readonly string getUserSQL = @"show databases";
        private readonly string getTablesSQL = @"use {0};show tables;";
        private readonly string getTableContentSQL = @"use {0};select * from {1}";
        //private string getColNameByTablesSQL;
        private readonly string getColNameByTableSQL = "desc {0}";
        private readonly string dataBaseName;
        public HiveDAOImpl(DatabaseItem dbi) : base(dbi)
        {
            this.dataBaseName = dbi.Schema;
        }
        public override bool TestConn()
        {
            using (var conn = new Connection(this.Host, ConvertUtil.TryParseInt(this.Port),
                                                   this.User, this.Pass))
            {
                try
                {

                    LimitTimeout(conn);
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
        private void LimitTimeout(Connection conn)
        {
            conn.SetSocketTimeout = 8000;
            conn.SetTcpReceiveTimeout = 8000;
            conn.SetTcpSendTimeout = 8000;
        }
        protected override QueryResult ExecuteSQL_Page(string sqlText, int pageSize, int pageIndex, int maxNum, bool returnHeader)
        {
            StringBuilder sb = new StringBuilder(1024 * 16); // TODO DK 单页够就行，太小了会copy数组浪费性能，需要选择合适的值
            QueryResult result;
            result.content = string.Empty;
            result.returnNum = 0;

            string sqlPage = String.Format(@"select * from (select row_number() over () as rowno,tmp0.* from ({0}) tmp0) t where t.rowno  between {1} and {2}",
                                    sqlText,
                                    pageSize * (pageIndex),
                                    pageSize * (pageIndex) + maxNum);
            try
            {
                using (Connection conn = new Connection(this.Host, ConvertUtil.TryParseInt(this.Port),
                                                   this.User, this.Pass))
                {
                    LimitTimeout(conn);
                    var cursor = conn.GetCursor();
                    cursor.Execute("use " + dataBaseName);
                    foreach (var s in sqlPage.Split(';'))
                    {
                        if (!String.IsNullOrEmpty(s))
                            cursor.Execute(s);
                    }
                    var list = cursor.FetchMany(int.MaxValue);

                    // 分页查询去掉第一列索引
                    if (returnHeader && !list.IsEmpty() && !(list[0] as IDictionary<string, object>).IsEmpty())
                    {
                        IDictionary<string, object> iDict = list[0];
                        for (int i = 1; i < iDict.Count; i++)
                        {
                            string key = GetColumnName(iDict.Keys.ElementAt(i));
                            sb.Append(key).Append(OpUtil.DefaultFieldSeparator);
                        }

                        if (iDict.Count > 1)
                            sb.Remove(sb.Length - 1, 1).Append(OpUtil.DefaultLineSeparator);

                    }

                    foreach (IDictionary<string, object> item in list)
                    {
                        for (int i = 1; i < item.Count; i++)   // 第一列不要
                        {
                            String key = item.Keys.ElementAt(i);
                            sb.Append(item[key]).Append(OpUtil.DefaultFieldSeparator);
                        }
                        if (item.Count > 1)
                            sb.Remove(sb.Length - 1, 1).Append(OpUtil.DefaultLineSeparator); // 最后一列多加了个\t，去掉
                    }
                }
                result.content = sb.ToString();
                result.returnNum = maxNum;
            }
            catch (Exception ex)
            {
                log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());

            }
            return result;
        }

        private string GetColumnName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;
            string[] names = name.Split('.');
            return names.Length == 2 ? names[1] : name;
        }

        public override string Query(string sql, bool header = true, int returnNum = OpUtil.PreviewMaxNum)
        {
            StringBuilder sb = new StringBuilder(1024 * 16);
            try
            {
                using (Connection conn = new Connection(this.Host, ConvertUtil.TryParseInt(this.Port),
                                                   this.User, this.Pass))
                {
                    LimitTimeout(conn);
                    var cursor = conn.GetCursor();
                    cursor.Execute("use " + dataBaseName);
                    foreach (var s in sql.Split(';'))
                    {
                        if (!String.IsNullOrEmpty(s))
                            cursor.Execute(s);
                    }
                    var list = cursor.FetchMany(returnNum);
                    if (header && !list.IsEmpty())
                    {
                        // 添加表头
                        IDictionary<string, object> iDict = list[0];
                        for (int i = 0; i < iDict.Count; i++)
                        {
                            string key = GetColumnName(iDict.Keys.ElementAt(i));
                            sb.Append(key).Append(OpUtil.DefaultFieldSeparator);
                        }
                        if (iDict.Count > 0)
                            sb.Remove(sb.Length - 1, 1).Append(OpUtil.DefaultLineSeparator); // 最后一列多加了个\t，去掉       

                    }
                    foreach (IDictionary<string, object> dict in list)
                    {
                        foreach (var key in dict.Keys)
                        {
                            sb.Append(dict[key].ToString()).Append(OpUtil.DefaultFieldSeparator);
                        }
                        if (!dict.Keys.IsEmpty())
                            sb.Remove(sb.Length - 1, 1).Append(OpUtil.DefaultLineSeparator);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());   // 辅助工具类，showmessage不能放在外面
                throw new DAOException(ex.Message);

            }
            return sb.ToString().Trim(OpUtil.DefaultLineSeparator);
        }

        public override string GetTablesSQL(string schema)
        {
            return String.Format(this.getTablesSQL, schema);
        }
        public override string GetColNameByTablesSQL(List<Table> tables)
        {
            return String.Empty;
        }
        public override string GetTableContentSQL(Table table)
        {
            return String.Format(getTableContentSQL, this.Schema, table.Name);
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
