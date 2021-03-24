using C2.Model;
using C2.Utils;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace C2.Database
{
    public class OracleDAOImpl : BaseDAOImpl
    {
        private static readonly LogUtil log = LogUtil.GetInstance("OracleImpl");
        private readonly string getUserSQL = @"select distinct owner from all_tables";
        private readonly string getTablesSQL = @"select table_name from all_tables where owner='{0}' order by table_name";
        private readonly string getTableContentSQL = @"select * from {0}.{1}";
        private readonly string getColNameBySchemaSQL = @"select a.table_name, a.column_name from all_tab_columns a where table_name in {0}";
        private readonly string getColNameByTablesSQL = @"select a.table_name, a.column_name from all_tab_columns a where table_name in ('{0}')";

        public OracleDAOImpl(DatabaseItem dbi) : base(dbi) { }

        public string ConnectionString
        {
            get
            {
                if (String.IsNullOrEmpty(Service)) // Is it a service name connection?
                    return String.Format(
                      "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1}))(CONNECT_DATA=(SID={2})));User Id={3};Password={4};Connection Lifetime=60;Connection Timeout=8",
                      Host,
                      Port,
                      Sid,
                      User,
                      Pass);

                else // Is it a SID connection?
                    return String.Format(
                      "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1}))(CONNECT_DATA=(SERVICE_NAME={2})));User Id={3};Password={4};Connection Lifetime=60;Connection Timeout=8",
                      Host,
                      Port,
                      Service,
                      User,
                      Pass);
            }
        }
        public override bool TestConn()
        {
            bool isSuccss = true;
            OracleConnection con = new OracleConnection(this.ConnectionString);
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                isSuccss = false;
            }
            finally
            {
                con.Close();
            }
            return isSuccss;
        }
        public override string Query(string sql, bool header = true, int returnNum = OpUtil.PreviewMaxNum)
        {
            string result = String.Empty;
            OracleConnection con = new OracleConnection(this.ConnectionString);
            sql = DbUtil.PurifyOnelineSQL(sql);
            try
            {
                con.Open();
                // 20210204 由于没有SQL语法解析，所以无法处理分隔符带来歧义等各种语法问题，因此目前支持一行SQL
                result = this.ExecuteQuery(sql, con, header, returnNum);
            }
            catch(Exception ex)
            {
                log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        private string ExecuteQuery(string sql, OracleConnection conn, bool header, int returnNum)
        {
            StringBuilder sb = new StringBuilder(1024 * 16);
            int totalReturnNum = 0;
            OracleCommand comm = new OracleCommand(sql, conn);
            using (OracleDataReader rdr = comm.ExecuteReader())
            {
                if (rdr.FieldCount == 0)
                    return String.Empty;
                //这里不关闭conn是因为conn在外层关闭
                if (header)
                {
                    for (int i = 0; i < rdr.FieldCount - 1; i++)
                        sb.Append(rdr.GetName(i)).Append(OpUtil.TabSeparator);
                    sb.Append(rdr.GetName(rdr.FieldCount - 1)).Append(OpUtil.DefaultLineSeparator);
                }
                while (rdr.Read() && totalReturnNum++ < returnNum)
                {
                    for (int i = 0; i < rdr.FieldCount - 1; i++)
                        sb.Append(GetRdrResult(rdr, i)).Append(OpUtil.TabSeparator);
                    sb.Append(GetRdrResult(rdr, rdr.FieldCount - 1)).Append(OpUtil.DefaultLineSeparator);
                }
            }
            return sb.TrimEndN().ToString();
        }
        private object GetRdrResult(OracleDataReader rdr, int index)
        {
            // 只能解决浮点数的异常，其他异常正常抛出
            try
            {
                return rdr[index];
            }
            catch(System.InvalidCastException)
            {
                return rdr.GetDouble(index);
            }
        }
        public override string GetTablesSQL(string schema)
        {
            return String.Format(this.getTablesSQL, schema.ToUpper());
        }
        public override string GetColNameBySchemaSQL(string schema)
        {
            return String.Format(this.getColNameBySchemaSQL,
                        String.Format(@"(select table_name from all_tables where owner='{0}') order by a.table_name", schema)
                   );
        }
        public override string GetColNameByTablesSQL(List<Table> tables)
        {
            List<String> tableNames = new List<String>();
            foreach (Table table in tables)
                tableNames.Add(table.Name);
            return String.Format(this.getColNameByTablesSQL, String.Join("','", tableNames));
        }
        public override string GetTableContentSQL(Table table)
        {
            return String.Format(this.getTableContentSQL, this.Schema, table.Name);
        }
        public override string GetUserSQL()
        {
            return this.getUserSQL;
        }
        public override string GetColNameByTableSQL(Table table)
        {
            return this.GetColNameByTablesSQL(new List<Table>() { table });
        }

        public override string DefaultSchema()
        {
            return String.IsNullOrEmpty(this.Schema) ? this.User.ToUpper() : this.Schema.ToUpper();
        }

        public override bool ExecuteSQL(string sqlText, string outPutPath, int maxReturnNum = int.MaxValue)
        {
            bool returnCode = true;
            int totalReturnNum = 0;
            StreamWriter sw = new StreamWriter(outPutPath, false);
            OracleConnection con = new OracleConnection(this.ConnectionString);
            try
            {
                con.Open();
                OracleCommand comm = new OracleCommand(sqlText, con);
                using (OracleDataReader rdr = comm.ExecuteReader())
                {
                    if (rdr.FieldCount == 0)
                        return true;
                    StringBuilder sb = new StringBuilder(1024);
                    for (int i = 0; i < rdr.FieldCount; i++)
                        sb.Append(rdr.GetName(i)).Append(OpUtil.TabSeparator);
                    sw.WriteLine(sb.ToString().TrimEnd(OpUtil.TabSeparator));    // 去掉最后一列的列分隔符
                    while (rdr.Read() && totalReturnNum++ < maxReturnNum)
                    {
                        sb.Clear();
                        for (int i = 0; i < rdr.FieldCount; i++)
                            sb.Append(rdr[i]).Append(OpUtil.TabSeparator);
                        sw.WriteLine(sb.TrimEndT().ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                // 不抛异常是因为外层不关注他的异常信息，只关注是否执行成功，该方法返回一个bool值
                returnCode = false;
            }
            finally
            {
                if (sw != null)
                    sw.Close();
                con.Close();
            }
            return returnCode;
        }
    }
}
