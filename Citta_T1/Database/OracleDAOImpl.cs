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
            OracleConnection con = new OracleConnection(this.ConnectionString);
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
        public override string Query(string sql, bool header = true, int returnNum = OpUtil.PreviewMaxNum)
        {
            int totalReturnNum = 0;
            StringBuilder sb = new StringBuilder(1024 * 16); // TODO
            try
            {
                using (OracleConnection con = new OracleConnection(this.ConnectionString))
                {
                    con.Open();
                    OracleCommand comm = new OracleCommand(sql, con);
                    using (OracleDataReader rdr = comm.ExecuteReader())  // rdr.Close()
                    {
                        if (rdr.FieldCount == 0)
                            return String.Empty;
                        if (header)
                        {
                            for (int i = 0; i < rdr.FieldCount - 1; i++)
                                sb.Append(rdr.GetName(i)).Append(OpUtil.DefaultFieldSeparator);
                            sb.Append(rdr.GetName(rdr.FieldCount - 1)).Append(OpUtil.DefaultLineSeparator);
                        }
                        while (rdr.Read() && totalReturnNum < returnNum)
                        {
                            for (int i = 0; i < rdr.FieldCount - 1; i++)
                                sb.Append(rdr[i]).Append(OpUtil.DefaultFieldSeparator);
                            sb.Append(rdr[rdr.FieldCount - 1]).Append(OpUtil.DefaultLineSeparator);
                            totalReturnNum += 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                QueryFailureException(ex.Message);
            }

            return sb.ToString().Trim(OpUtil.DefaultLineSeparator);
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

        public override bool ExecuteSQL(string sqlText, string outPutPath, int maxReturnNum = -1, int pageSize = 100000)
        {
            bool returnCode = true;
            int totalReturnNum = 0;
            StreamWriter sw = new StreamWriter(outPutPath, false);
            try
            {
                using (OracleConnection con = new OracleConnection(this.ConnectionString))
                {
                    con.Open();
                    OracleCommand comm = new OracleCommand(sqlText, con);
                    using (OracleDataReader rdr = comm.ExecuteReader())
                    {
                        if (rdr.FieldCount == 0)
                            return true;
                        StringBuilder sb = new StringBuilder(1024);
                        for (int i = 0; i < rdr.FieldCount; i++)
                            sb.Append(rdr.GetName(i)).Append(OpUtil.DefaultFieldSeparator);
                        sw.WriteLine(sb.ToString().TrimEnd(OpUtil.DefaultFieldSeparator));    // 去掉最后一列的列分隔符
                        while (rdr.Read() && (maxReturnNum == -1 ? true : totalReturnNum < maxReturnNum))
                        {
                            sb = new StringBuilder(1024);
                            for (int i = 0; i < rdr.FieldCount; i++)
                                sb.Append(rdr[i]).Append(OpUtil.DefaultFieldSeparator);
                            sw.WriteLine(sb.ToString().TrimEnd(OpUtil.DefaultFieldSeparator));
                            totalReturnNum += 1;
                        }
                        sw.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                returnCode = false;
            }
            finally
            {
                sw.Close();
            }
            return returnCode;
        }
    }
}
