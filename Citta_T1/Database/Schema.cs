using System;
using System.Collections.Generic;
using System.Text;
//using System.Data.OracleClient;
using Oracle.ManagedDataAccess.Client;

namespace C2.Database
{
    public class Schema
    {
        public string Name;
        public Connection ParentConnection;

        public Schema(Connection Parent)
        {
            ParentConnection = Parent;
        }

        List<Table> _Tables = null;
        /// <summary>
        /// The list of tables for this schema
        /// </summary>
        public List<Table> Tables
        {
            get
            {
                if (_Tables == null)
                {
                    // select distinct owner from sys.all_objects
                    using (OracleConnection conn = new OracleConnection(ParentConnection.ConnectionString))
                    {
                        conn.Open();
                        string sql = String.Format(@"
                            select object_name, object_type
                            from sys.all_objects
                            where owner='{0}' and object_type in ('TABLE','VIEW')
                            order by object_name",
                          DbHelper.Sanitise(Name));
                        using (OracleCommand comm = new OracleCommand(sql, conn))
                        {
                            using (OracleDataReader rdr = comm.ExecuteReader())
                            {
                                _Tables = new List<Table>();
                                while (rdr.Read())
                                {
                                    Table table = new Table(this);
                                    table.Name = rdr.GetString(0);
                                    table.View = rdr.GetString(1) == "VIEW";
                                    _Tables.Add(table);
                                }
                            }
                        }
                        foreach(Table table in _Tables)
                        {
                            sql = "select * from " + table.ParentSchema.Name + "." + table.Name;
                            using (OracleCommand comm = new OracleCommand(sql, conn))
                            {
                                using (OracleDataReader rdr = comm.ExecuteReader())
                                {
                                    for (int i = 0; i < rdr.FieldCount; i++)
                                        table.Columns.Add(rdr.GetName(i));
                                }
                            }
                        }
                        conn.Close();
                    }
                }
                return _Tables;
            }
        }
    }
}
