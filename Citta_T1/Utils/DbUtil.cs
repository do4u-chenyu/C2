using C2.Database;
using C2.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Hive2;
using System.Linq;

namespace C2.Utils
{
    public static class DbUtil
    {
        private static readonly LogUtil log = LogUtil.GetInstance("DbUtil");
        public static bool ExecuteOracleSQL(OraConnection conn, string sqlText, string outPutPath, int maxReturnNum=-1, int pageSize=100000)
        {
            int pageIndex = 0;
            bool returnHeader = true;
            int totalRetuenNum = 0, subMaxNum = 0;
            using (StreamWriter sw = new StreamWriter(outPutPath, false))
            {
                while (maxReturnNum == -1 ? true : totalRetuenNum < maxReturnNum)
                {
                    if (pageSize * pageIndex < maxReturnNum && pageSize * (pageIndex + 1) > maxReturnNum)
                        subMaxNum = maxReturnNum - pageIndex * pageSize;
                    else
                        subMaxNum = pageSize;
                    QueryResult contentAndNum = ExecuteOracleQL_Page(conn, sqlText, pageSize, pageIndex, subMaxNum, returnHeader);

                    string result = contentAndNum.content;
                    totalRetuenNum  += contentAndNum.returnNum;

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
        private static QueryResult ExecuteOracleQL_Page(OraConnection conn, string sqlText, int pageSize, int pageIndex, int maxNum, bool returnHeader)
        {
            /*
             * pageIndex start from 0.
             */
            QueryResult result;
            StringBuilder sb = new StringBuilder(1024 * 16);
            int returnNum = 0;
            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
            {
                try
                {
                    using (OracleConnection con = new OracleConnection(conn.ConnectionString))
                    {
                        con.Open();
                        string sqlPage = String.Format(@"select * from 
	                                                        (select rownum as rowno,tmp.* from ({0}) tmp where rownum<={1}) 
                                                        t where t.rowno>{2}",
                                            sqlText,
                                            pageSize * (pageIndex + 1), 
                                            pageSize * (pageIndex));
                        OracleCommand comm = new OracleCommand(sqlPage, con);
                        using (OracleDataReader rdr = comm.ExecuteReader())  // rdr.Close()
                        {
                            if (returnHeader)
                            {
                                for (int i = 0; i < rdr.FieldCount - 1; i++)
                                    sb.Append(rdr.GetName(i)).Append(OpUtil.DefaultFieldSeparator);
                                sb.Append(rdr.GetName(rdr.FieldCount - 1)).Append(OpUtil.DefaultLineSeparator);
                            }

                            while (rdr.Read() && returnNum < maxNum)
                            {
                                for (int i = 0; i < rdr.FieldCount - 1; i++)
                                    sb.Append(rdr[i]).Append(OpUtil.DefaultFieldSeparator);
                                sb.Append(rdr[rdr.FieldCount - 1]).Append(OpUtil.DefaultLineSeparator);
                                returnNum += 1;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());   // 辅助工具类，showmessage不能放在外面
                }
                finally
                {
                    result.content = sb.ToString();
                    result.returnNum = returnNum;
                }
            }
            return result;
        }

        public static Dictionary<string, List<string>> StringToDict(string v)
        {
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
            foreach(string line in v.Split(OpUtil.DefaultLineSeparator))
            {
                var kv = line.Split(OpUtil.DefaultFieldSeparator);
                if (kv.Length != 2)
                    continue;
                string key = kv[0];
                string val = kv[1];
                if (result.Keys.Contains(key))
                    result[key].Add(val);
                else
                    result.Add(key, new List<string>() { val });
            }
            return result;
        }

        public static bool TestConn(OraConnection conn)
        {
            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
            {
                using (OracleConnection con = new OracleConnection(conn.ConnectionString))
                {
                    try
                    {
                        con.Open();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                        return false;
                    }
                }
            }
        }
        public static bool TestConn(DatabaseItem dbi)
        {
            bool ret = false;
            switch (dbi.Type)
            {
                case DatabaseType.Oracle:
                    ret = TestConn(new OraConnection(dbi));
                    break;
                case DatabaseType.Hive:
                    ret = new HiveConnection(dbi).TestConn();
                    break;
                default:
                    break;
            }
            return ret;

        }

        public static bool TestConn(DataItem item)
        {
            return TestConn(item.DBItem);
        }
        public static List<string> GetUsers(OraConnection conn)
        {
            List<string> users = new List<string>();
            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
            {
                try
                {
                    using (OracleConnection con = new OracleConnection(conn.ConnectionString))
                    {
                        con.Open();
                        string sql = String.Format(@"select distinct owner from all_tables");
                        OracleCommand comm = new OracleCommand(sql, con);
                        using (OracleDataReader rdr = comm.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                users.Add(rdr.GetString(0));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                }
            }
            return users;
        }

        public static List<Table> GetTablesByUser(OraConnection conn, string userName)
        {
            List<Table> tables = new List<Table>();
            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
            {
                try
                {
                    using (OracleConnection con = new OracleConnection(conn.ConnectionString))
                    {
                        con.Open();
                        string sql = String.Format(@"
                            select table_name
                            from all_tables
                            where owner='{0}'
                            order by table_name",
                              DbHelper.Sanitise(userName.ToUpper()));
                        OracleCommand comm = new OracleCommand(sql, con);
                        using (OracleDataReader rdr = comm.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                Table table = new Table(userName, rdr.GetString(0));
                                tables.Add(table);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                }
            }
            return tables;
        }
        public static List<List<string>> GetTbContent(OraConnection conn, Table table, int maxNum)
        {
            List<List<string>> ret = new List<List<string>>();
            string contentString = DbUtil.GetOracleTbContentString(conn, table, maxNum);
            ret = DbUtil.StringTo2DString(contentString);
            return ret;
        }

        public static List<List<string>> StringTo2DString(string contentString)
        {
            List<List<string>> ret = new List<List<string>>();
            if (!String.IsNullOrEmpty(contentString))
            {
                string[] lines = contentString.Split(OpUtil.DefaultLineSeparator);
                for (int i = 0; i < lines.Length; i++)
                {
                    ret.Add(new List<string>(lines[i].Split(OpUtil.DefaultFieldSeparator)));
                }
            }
            return ret;
        }
        public static string GetTbContentString(DatabaseItem databaseItem, int maxNum)
        {
            OraConnection conn = new OraConnection(databaseItem);
            string ret = String.Empty;
            switch (databaseItem.Type)
            {
                case DatabaseType.Oracle:
                    ret = DbUtil.GetOracleTbContentString(conn, databaseItem.DataTable, maxNum);
                    break;
                case DatabaseType.Hive:
                    ret = DbUtil.GetHiveTbContentString();
                    break;
                default:
                    break;
            }
            return ret;
        }

        private static string GetHiveTbContentString()
        {
            // TODO DK 实现Hive查表
            throw new NotImplementedException();
        }

        public static string GetOracleTbContentString(OraConnection conn, Table table, int maxNum)
        {
            StringBuilder sb = new StringBuilder(1024 * 16);
            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
            {
                try
                {
                    using (OracleConnection con = new OracleConnection(conn.ConnectionString))
                    {
                        con.Open();
                        string sql = String.Format(@"select * from {0}.{1} where rownum <= {2}", table.UserName.ToUpper(), table.Name, maxNum);
                        OracleCommand comm = new OracleCommand(sql, con);
                        using (OracleDataReader rdr = comm.ExecuteReader())  // rdr.Close()
                        {  
                            for (int i = 0; i < rdr.FieldCount - 1; i++)
                                sb.Append(rdr.GetName(i)).Append(OpUtil.DefaultFieldSeparator);
                            sb.Append(rdr.GetName(rdr.FieldCount - 1)).Append(OpUtil.DefaultLineSeparator);

                            while (rdr.Read())
                            {
                                for (int i = 0; i < rdr.FieldCount - 1; i++)
                                    sb.Append(rdr[i]).Append(OpUtil.DefaultFieldSeparator);
                                sb.Append(rdr[rdr.FieldCount - 1]).Append(OpUtil.DefaultLineSeparator);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());   // 辅助工具类，showmessage不能放在外面
                }
                return sb.ToString();
            }
        }

        public static Dictionary<String, List<String>> GetTableCol(OraConnection conn, List<Table> tables)
        {
            Dictionary<String, List<String>> cols = new Dictionary<String, List<String>>();
            String[] tableNames = new string[tables.Count];
            for (int i = 0; i < tableNames.Length; i++)
                tableNames[i] = tables[i].Name;
            string sql = String.Format(@"select a.table_name, a.column_name from all_tab_columns a where table_name in ('{0}')",
                String.Join("','", tableNames));
            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
            {
                try
                {
                    using (OracleConnection con = new OracleConnection(conn.ConnectionString))
                    {
                        con.Open();
                        using (OracleCommand comm = new OracleCommand(sql, con))
                        {
                            using (OracleDataReader rdr = comm.ExecuteReader())
                            {
                                while (rdr.Read())
                                {
                                    string table_name = rdr[0].ToString().Trim(), column_name = rdr[1].ToString().Trim();
                                    if (!cols.ContainsKey(table_name))
                                        cols.Add(table_name, new List<string>() { column_name });
                                    else
                                        cols[table_name].Add(column_name);
                                }
                            }
                        }
                        con.Close();
                    }
                }
                catch (Exception ex) // Better catch in case they have bad sql
                {
                    log.Error(ex.ToString());
                }
            }
            return cols;
        }
        public static bool FillDGVWithTbSchema(DataGridView gridOutput, OraConnection conn, string tableName)
        {
            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
            {
                try
                {
                    using (OracleConnection con = new OracleConnection(conn.ConnectionString))
                    {
                        con.Open();
                        string sql = String.Format(@"select a.column_name, a.data_type from all_tab_columns a where TABLE_NAME='{0}'", tableName.ToUpper());
                        using (OracleCommand comm = new OracleCommand(sql, con))
                        {
                            using (OracleDataReader rdr = comm.ExecuteReader())
                            {
                                // Grab all the column names
                                gridOutput.Rows.Clear();
                                gridOutput.Columns.Clear();
                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    gridOutput.Columns.Add(i.ToString(), rdr.GetName(i));
                                }
                                int rows = 0;
                                while (rdr.Read())
                                {
                                    string[] objs = new string[rdr.FieldCount];
                                    for (int f = 0; f < rdr.FieldCount; f++) objs[f] = rdr[f].ToString();
                                    gridOutput.Rows.Add(objs);
                                    rows++;
                                }
                            }
                        }
                        DgvUtil.ResetColumnsWidth(gridOutput);
                        con.Close();
                        return true;
                    }
                }
                catch (Exception ex) // Better catch in case they have bad sql
                {
                    log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                    return false;
                }
            }
        }

        public static bool FillDGVWithTbContent(DataGridView gridOutput, OraConnection conn, Table table, int maxNum)
        {
            List<List<string>> ret = DbUtil.GetTbContent(conn, table, maxNum);
            if (ret.Count <= 0)
                return false;
            ret = FileUtil.FormatDatas(ret, maxNum);
            FileUtil.FillTable(gridOutput, ret, maxNum);
            return true;
        }

        struct QueryResult
        {
            public string content;
            public int returnNum;
        }
    }
}