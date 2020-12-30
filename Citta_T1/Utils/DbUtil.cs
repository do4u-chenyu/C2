using C2.Database;
using C2.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
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
        public static bool TestConn(OraConnection conn, bool showDetail=false)
        {
            using (new CursorUtil.UsingCursor(Cursors.WaitCursor))
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
                        if (showDetail)
                            HelpUtil.ShowMessageBox(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                        return false;
                    }
                }
            }
        }

        public static bool TestConn(DataItem item, bool showDetail = false)
        {
            bool ret = false;
            switch (item.DataType)
            {
                case DatabaseType.Oracle:
                    ret = TestConn(new OraConnection(item));
                    break;
                case DatabaseType.Hive:
                    break;
                default:
                    break;
            }
            return ret;
        }
        public static List<string> GetUsers(OraConnection conn)
        {
            List<string> users = new List<string>();
            using (new CursorUtil.UsingCursor(Cursors.WaitCursor))
            {
                try
                {
                    using (OracleConnection con = new OracleConnection(conn.ConnectionString))
                    {
                        con.Open();
                        string sql = String.Format(@"select distinct owner from all_tables");
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
                }
                catch (Exception ex)
                {
                    HelpUtil.ShowMessageBox(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                }
            }
            return users;
        }

        public static List<Table> GetTablesByUser(OraConnection conn, string userName)
        {
            List<Table> tables = new List<Table>();
            using (new CursorUtil.UsingCursor(Cursors.WaitCursor))
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
                        using (OracleCommand comm = new OracleCommand(sql, con))
                        {
                            using (OracleDataReader rdr = comm.ExecuteReader())
                            {
                                while (rdr.Read())
                                {
                                    Table table = new Table(userName);
                                    table.Name = rdr.GetString(0);
                                    tables.Add(table);
                                }
                            }
                        }
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    HelpUtil.ShowMessageBox(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                }
            }
            return tables;
        }

        public static bool FillDGVWithTbSchema(DataGridView gridOutput, OraConnection conn, string tableName)
        {
            using (new CursorUtil.UsingCursor(Cursors.WaitCursor))
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
                    HelpUtil.ShowMessageBox(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                    return false;
                }
            }
        }

        public static bool FillDGVWithTbContent(DataGridView gridOutput, OraConnection conn, Table table, int maxNum)
        {
            using (new CursorUtil.UsingCursor(Cursors.WaitCursor))
            {
                try
                {
                    using (OracleConnection con = new OracleConnection(conn.ConnectionString))
                    {
                        con.Open();
                        string sql = String.Format(@"select * from {0}.{1}", table.UserName.ToUpper(), table.Name);
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
                        DgvUtil.ResetColumnsWidth(gridOutput);
                        con.Close();
                        return true;
                    }
                }
                catch (Exception ex) // Better catch in case they have bad sql
                {
                    HelpUtil.ShowMessageBox(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                    return false;
                }
            }
        }
    }
}