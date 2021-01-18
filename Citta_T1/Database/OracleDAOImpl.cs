using C2.Model;
using C2.Utils;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
<<<<<<< HEAD
=======
using System.IO;
>>>>>>> f942c83be0ed3e2f3bfd86aaacfd653988779009
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Database
{
    public class OracleDAOImpl : BaseDAOImpl
    {
        private static readonly LogUtil log = LogUtil.GetInstance("OracleImpl");
        private string getUserSQL = @"select distinct owner from all_tables";
        private string getTablesByUserSQL = @"select table_name from all_tables where owner='{0}'order by table_name";
        private string getTableContentSQL = @"select * from {0}.{1} where rownum <= {2}";
        private string getSchemaByTablesSQL = @"select a.table_name, a.column_name from all_tab_columns a where table_name in ('{0}')";

        public OracleDAOImpl(DatabaseItem dbi) : base(dbi) { }
        public OracleDAOImpl(DataItem di) : base(di) { }
        public OracleDAOImpl(string name, string user, string pass, string host, string sid, string service, string port): base(name, user, pass, host, sid, service, port) { }

        public string ConnectionString
        {
            get
            {
                if (Service.Length > 0) // Is it a service name connection?
                    return String.Format(
                      "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1}))(CONNECT_DATA=(SERVICE_NAME={2})));User Id={3};Password={4};",
                      Host,
                      Port,
                      Service,
                      User,
                      Pass);
                else // Is it a SID connection?
                    return String.Format(
                      "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1}))(CONNECT_DATA=(SID={2})));User Id={3};Password={4};",
                      Host,
                      Port,
                      Sid,
                      User,
                      Pass);
            }
        }
        public override bool TestConn()
        {
            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
            {
                try
                {
                    using (OracleConnection con = new OracleConnection(this.ConnectionString))
                    {
                        con.Open();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                    return false;
                }
            }
        }
        public override string Query(string sql)
        {
            StringBuilder sb = new StringBuilder(1024 * 16);
            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
            {
                try
                {
                    using (OracleConnection con = new OracleConnection(this.ConnectionString))
                    {
                        con.Open();
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
                    log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                }
                return sb.ToString();
            }
        }
        public override string GetTablesByUserSQL()
        {
            return String.Format(this.getTablesByUserSQL, this.User);
        }
        public override string GetSchemaByTablesSQL(List<Table> tables)
        {
            String[] tableNames = new string[tables.Count];
            for (int i = 0; i < tableNames.Length; i++)
                tableNames[i] = tables[i].Name;
            return String.Format(this.getSchemaByTablesSQL, tableNames);
        }
        public override string GetTableContentSQL(Table table, int maxNum)
        {
            return String.Format(this.getTableContentSQL, this.User, table.Name, maxNum);
        }
        public override string GetUserSQL()
        {
            return this.getUserSQL;
        }
        public override bool ExecuteOracleSQL(string sqlText, string outPutPath, int maxReturnNum = -1, int pageSize = 100000)
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
                    QueryResult contentAndNum = ExecuteOracleQL_Page(sqlText, pageSize, pageIndex, subMaxNum, returnHeader);

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
        private QueryResult ExecuteOracleQL_Page(string sqlText, int pageSize, int pageIndex, int maxNum, bool returnHeader)
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
                    using (OracleConnection con = new OracleConnection(this.ConnectionString))
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
    }
}
