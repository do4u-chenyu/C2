using C2.Database;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Utils
{
    public static class DbUtil
    {
        //private void executeSQL(Connection connection, string sqlText)
        //{
        //    // Execute the given query for the first 1000 records it spits out
        //    try
        //    {
        //        using (OracleConnection conn = new OracleConnection(connection.ConnectionString))
        //        {
        //            conn.Open();
        //            string sql = sqlText;
        //            using (OracleCommand comm = new OracleCommand(sql, conn))
        //            {
        //                using (OracleDataReader rdr = comm.ExecuteReader())
        //                {
        //                    // Grab all the column names
        //                    gridOutput.Rows.Clear();
        //                    gridOutput.Columns.Clear();
        //                    for (int i = 0; i < rdr.FieldCount; i++)
        //                    {
        //                        gridOutput.Columns.Add(i.ToString(), rdr.GetName(i));
        //                    }

        //                    // Read up to 1000 rows
        //                    int rows = 0;
        //                    while (rdr.Read() && rows < 1000)
        //                    {
        //                        string[] objs = new string[rdr.FieldCount];
        //                        for (int f = 0; f < rdr.FieldCount; f++) objs[f] = rdr[f].ToString();
        //                        gridOutput.Rows.Add(objs);
        //                        rows++;
        //                    }
        //                }
        //            }
        //            conn.Close();
        //        }
        //    }
        //    catch (Exception ex) // Better catch in case they have bad sql
        //    {
        //        HelpUtil.ShowMessageBox(ex.Message);
        //    }
        //}
        //private void connect()
        //{
        //    OracleConnection oconn = new OracleConnection(cs);
        //    OracleDataAdapter oda = new OracleDataAdapter(oconn);
        //    oda.Fill()
        //}

        public static List<string> GetUsers(Connection conn)
        {
            List<string> users = new List<string>();
            // select distinct owner from sys.all_objects
            // http://forums.devshed.com/oracle-development-96/need-help-to-view-all-schemas-using-sql-plus-218002.html
            using (OracleConnection con = new OracleConnection(conn.ConnectionString))
            {
                con.Open();
                string sql = String.Format(@"select distinct owner from sys.all_objects where object_type in ('TABLE','VIEW')");
                using (OracleCommand comm = new OracleCommand(sql, con))
                {
                    using (OracleDataReader rdr = comm.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            users.Add(rdr.GetString(0));
                        }
                    }
                }
                con.Close();
            }
            return users;
        }

        public static bool TestConn(Connection conn)
        {
            using (OracleConnection con = new OracleConnection(conn.ConnectionString))
            {
                try
                {
                    con.Open();
                    con.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    HelpUtil.ShowMessageBox(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                    return false;
                }
            }
        }

        public static List<Table> GetTablesByUser(Connection conn, string user)
        {
            List<Table> tables = new List<Table>();
            Connection tmpConn = conn.Clone();
            tmpConn.User = user;
            List<Schema> schemas = tmpConn.Schemas;
            if (schemas != null && schemas.Count > 0)
                tables = schemas[0].Tables;
            return tables;
        }

        public static void FillDGVWithTbSchema(DataGridView gridOutput, Connection conn, string tableName)
        {
            try
            {
                using (OracleConnection con = new OracleConnection(conn.ConnectionString))
                {
                    con.Open();
                    string sql = String.Format(@"select A.COLUMN_NAME,A.DATA_TYPE  from user_tab_columns A where TABLE_NAME='{0}'", tableName.ToUpper());
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
                }
            }
            catch (Exception ex) // Better catch in case they have bad sql
            {
                HelpUtil.ShowMessageBox(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
            }
        }

        public static void FillDGVWithTbContent(DataGridView gridOutput, Connection conn, string tableName, int maxNum)
        {
            try
            {
                using (OracleConnection con = new OracleConnection(conn.ConnectionString))
                {
                    con.Open();
                    string sql = String.Format(@"select * from {0}", tableName);
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
                            while (rdr.Read() && rows < maxNum)
                            {
                                string[] objs = new string[rdr.FieldCount];
                                for (int f = 0; f < rdr.FieldCount; f++) objs[f] = rdr[f].ToString();
                                gridOutput.Rows.Add(objs);
                                rows++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) // Better catch in case they have bad sql
            {
                HelpUtil.ShowMessageBox(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
            }
        }
    }
}