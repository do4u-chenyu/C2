using C2.Model;
using C2.Utils;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
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
    }
}
