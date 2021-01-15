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
    public class OracleDAOImpl: BaseDAO
    {
        private static readonly LogUtil log = LogUtil.GetInstance("OracleImpl");
        public string Name, User, Pass, Host, Sid, Service, Port;
        public string getUserSQL = @"select distinct owner from all_tables";
        public new string getTablesByUserSQL = @"select table_name from all_tables where owner='{0}'order by table_name";
        public new string getTableContentSQL = @"select * from {0}.{1} where rownum <= {2}";
        public new string getSchemaByTablesSQL;
        public OracleDAOImpl() { }
        public OracleDAOImpl(DatabaseItem dbi)
        {
            this.Name = dbi.Server;
            this.User = dbi.User;
            this.Pass = dbi.Password;
            this.Host = dbi.Server;
            this.Sid = dbi.SID;
            this.Service = dbi.Service;
            this.Port = dbi.Port;
        }

        public OracleDAOImpl(DataItem item) : this(item.DBItem)
        {
        }
        public OracleDAOImpl(string name, string user, string pass, string host, string sid, string service, string port)
        {
            this.Name = name;
            this.User = user;
            this.Pass = pass;
            this.Host = host;
            this.Sid = sid;
            this.Service = service;
            this.Port = port;
        }
        public OracleDAOImpl Clone()
        {
            OracleDAOImpl tmp = new OracleDAOImpl
            {
                Name = this.Name,
                User = this.User,
                Pass = this.Pass,
                Host = this.Host,
                Sid = this.Sid,
                Service = this.Service,
                Port = this.Port
            };
            return tmp;
        }
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

        public override string GenGetSchemaByTablesSQL(string getSchemaByTablesSQL, List<Table> tables)
        {
            throw new NotImplementedException();
        }
        public override string GenGetTableContentSQL(Table table, int maxNum)
        {
            return String.Format(this.getTableContentSQL, this.User, table, maxNum);
        }
    }
}
