using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using C2.Model;
using Oracle.ManagedDataAccess.Client;
using Hive2;
using C2.Utils;

namespace C2.Database
{
    public class OraConnection
    {
        public string Name, User, Pass, Host, Sid, Service, Port;
        public OraConnection() { }
        public OraConnection(DatabaseItem dbi)
        {
            this.Name = dbi.Server;
            this.User = dbi.User;
            this.Pass = dbi.Password;
            this.Host = dbi.Server;
            this.Sid = dbi.SID;
            this.Service = dbi.Service;
            this.Port = dbi.Port;
        }

        public OraConnection(DataItem item) : this(item.DBItem)
        {
        }
        public OraConnection(string name, string user, string pass, string host, string sid, string service, string port)
        {
            this.Name = name;
            this.User = user;
            this.Pass = pass;
            this.Host = host;
            this.Sid = sid;
            this.Service = service;
            this.Port = port;
        }
        public OraConnection Clone()
        {
            OraConnection tmp = new OraConnection
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


    public class HiveConnection
    {
        private static readonly LogUtil log = LogUtil.GetInstance("HiveConnection");
        public string Server, User, Pass, Host, Port;
        public Connection conn;

        public HiveConnection(DatabaseItem dbi)
        {
            this.Server = dbi.Server;
            this.User = dbi.User;
            this.Pass = dbi.Password;
            this.Host = dbi.Server;
            this.Port = dbi.Port;
        }

        private string GetHeaders(IDictionary<string, object> headersDict)
        {
            List<string> tmpResult = new List<string>();
            foreach (var key in headersDict.Keys)
            {
                int start = key.IndexOf('.');
                if (start + 1 > 0 && start + 1 < key.Length - 1)
                    tmpResult.Add(key.Substring(start + 1));
            }
            return string.Join(OpUtil.DefaultFieldSeparator.ToString(), tmpResult);
        }


        public string GetSQLResult(string database,string sql)
        {

            StringBuilder sb = new StringBuilder(1024 * 16);
            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
            {
                try
                {
                    using (Connection conn = new Connection(this.Server, ConvertUtil.TryParseInt(this.Port),
                                                       this.User, this.Pass))
                    {

                        var cursor = conn.GetCursor();
                        cursor.Execute("use " + database);
                        cursor.Execute(sql.TrimEnd(';'));
                        var list = cursor.FetchMany(int.MaxValue);
                        if (list.Count > 0)
                        {
                            // 添加表头
                            sb.Append(GetHeaders(list[0])).Append(OpUtil.DefaultLineSeparator);
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
                return sb.ToString();
            }

        }

    }
}
