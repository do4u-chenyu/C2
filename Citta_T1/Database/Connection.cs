using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using C2.Model;
using Oracle.ManagedDataAccess.Client;

namespace C2.Database
{
    public class Connection
    {
        public string Name, User, Pass, Host, Sid, Service, Port;
        public Connection() { }
        public Connection(DatabaseItem dbi)
        {
            this.Name = dbi.Server;
            this.User = dbi.User;
            this.Pass = dbi.Password;
            this.Host = dbi.Server;
            this.Sid = dbi.SID;
            this.Service = dbi.Service;
            this.Port = dbi.Port;
        }
        public Connection(string name, string user, string pass, string host, string sid, string service, string port)
        {
            this.Name = name;
            this.User = user;
            this.Pass = pass;
            this.Host = host;
            this.Sid = sid;
            this.Service = service;
            this.Port = port;
        }
        public Connection Clone()
        {
            Connection tmp = new Connection();
            tmp.Name = this.Name;
            tmp.User = this.User;
            tmp.Pass = this.Pass;
            tmp.Host = this.Host;
            tmp.Sid = this.Sid;
            tmp.Service = this.Service;
            tmp.Port = this.Port;
            return tmp;
        }
        /// <summary>
        /// Returns the connection string for this connection
        /// </summary>
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

        List<Schema> _Schemas = null;
        /// <summary>
        /// The list of schemas for this connection
        /// </summary>
        public List<Schema> Schemas
        {
            get
            {
                if (_Schemas == null)
                {
                    // select distinct owner from sys.all_objects
                    // http://forums.devshed.com/oracle-development-96/need-help-to-view-all-schemas-using-sql-plus-218002.html
                    OracleConnection conn = null;
                    try
                    {
                        conn = new OracleConnection(ConnectionString);
                        conn.Open();
                        AfterConnDb(conn);
                        conn.Close();
                        return _Schemas;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("连接数据库失败, 详情：" + ex.ToString());
                        return null;
                    }
                }
                return _Schemas;
            }
        }
        private void AfterConnDb(OracleConnection conn)
        {
            string sql = String.Format(@"select distinct owner from sys.all_objects where object_type in ('TABLE','VIEW') and owner='{0}'", User.ToUpper());
            using (OracleCommand comm = new OracleCommand(sql, conn))
            {
                using (OracleDataReader rdr = comm.ExecuteReader())
                {
                    _Schemas = new List<Schema>();
                    while (rdr.Read())
                    {
                        Schema schema = new Schema(this);
                        schema.Name = rdr.GetString(0);
                        _Schemas.Add(schema);
                    }
                }
            }
        }
    }
}
