using System;
using System.Collections.Generic;
using System.Text;
using C2.Model;
using C2.Utils;
using System.IO;
using MySql.Data.MySqlClient;

namespace C2.Database
{
    public class MysqlDAOImpl : BaseDAOImpl
    {

        private static readonly LogUtil log = LogUtil.GetInstance("MysqlDAOImpl");
        private readonly string getUserSQL = @"SELECT SCHEMA_NAME FROM information_schema.SCHEMATA";
        private readonly string getTablesSQL = @"select table_name from information_schema.tables where table_schema='{0}'order by table_name";
        private readonly string getTableContentSQL = @"use {0};select * from {1}";
        private readonly string getColNameBySchemaSQL = @"select TABLE_NAME, COLUMN_NAME from information_schema.COLUMNS where TABLE_NAME in {0}";
        private readonly string getColNameByTablesSQL = @"select TABLE_NAME, COLUMN_NAME from information_schema.COLUMNS where TABLE_NAME in ('{0}')";
        

        public MysqlDAOImpl(DatabaseItem dbi) : base(dbi) { }


        public string ConnectionString()
        {
            return String.Format(
                     "server={0};port={1};user={2};password={3};",
                      Host,
                      Port,
                      User,
                      Pass);
        }

        //数据库建立连接:MySqlConnection
        public override bool TestConn()
        {
            bool isSuccss = true;
            MySqlConnection con = new MySqlConnection(this.ConnectionString());
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
            MySqlConnection con = new MySqlConnection(this.ConnectionString());
            sql = DbUtil.PurifyOnelineSQL(sql);
            try
            {
                con.Open();
                result = this.ExecuteQuery(sql, con, header, returnNum);
            }
            catch (Exception ex)
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

        private string ExecuteQuery(string sql, MySqlConnection conn, bool header, int returnNum)
        {
            StringBuilder sb = new StringBuilder(1024 * 16);
            int totalReturnNum = 0;
            MySqlCommand comm = new MySqlCommand(sql, conn);
            using (MySqlDataReader rdr = comm.ExecuteReader())
            {
                if (rdr.FieldCount == 0)
                    return String.Empty;
                //这里不关闭conn是因为conn在外层关闭
                if (header)
                {
                    for (int i = 0; i < rdr.FieldCount - 1; i++)
                        sb.Append(rdr.GetName(i)).Append(OpUtil.TabSeparator);
                    sb.Append(rdr.GetName(rdr.FieldCount - 1)).Append(OpUtil.LineSeparator);
                }
                while (rdr.Read() && totalReturnNum++ < returnNum)
                {
                    for (int i = 0; i < rdr.FieldCount - 1; i++)
                        sb.Append(GetRdrResult(rdr, i)).Append(OpUtil.TabSeparator);
                    sb.Append(GetRdrResult(rdr, rdr.FieldCount - 1)).Append(OpUtil.LineSeparator);
                }
            }
            return sb.TrimEndN().ToString();
        }


        private object GetRdrResult(MySqlDataReader rdr, int index)
        {
            // 只能解决浮点数的异常，其他异常正常抛出
            try
            {
                return rdr[index];
            }
            catch (System.InvalidCastException)
            {
                return rdr.GetDouble(index);
            }
        }

        
        public override string GetTablesSQL(string schema)
        {
            //this.Schema = schema;
            return String.Format(this.getTablesSQL, schema);
            //return String.Format(this.getTablesSQL, this.Schema);
        }

        public override string GetColNameBySchemaSQL(string schema)
        {
            return String.Format(this.getColNameBySchemaSQL,
                //String.Format(@"(select SCHEMA_NAME from information_schema.SCHEMATA where SCHEMA_NAME='{0}')", schema)
                String.Format(@"(select table_name from information_schema.tables where table_schema='{0}') order by table_name", schema)
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
            return String.Format(this.getTableContentSQL, this.Schema,table.Name);
        }
        
        public override string GetUserSQL()
        {
            return this.getUserSQL;
        }
        
        public override string GetColNameByTableSQL(Table table)
        {
            return this.GetColNameByTablesSQL(new List<Table>() { table });
        }
        // 刷新返回指定数据库：information_schema
        public override string DefaultSchema()
        {
            return "information_schema";
            //return String.IsNullOrEmpty(this.Schema) ? this.User : "information_schema";
        }

        public override bool ExecuteSQL(string sqlText, string outPutPath, int maxReturnNum = int.MaxValue)
        {
            bool returnCode = true;
            int totalReturnNum = 0;
            StreamWriter sw = new StreamWriter(outPutPath, false);
            MySqlConnection con = new MySqlConnection(this.ConnectionString());
            try
            {
                con.Open();
                MySqlCommand comm = new MySqlCommand(sqlText, con);
                using (MySqlDataReader rdr = comm.ExecuteReader())
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
                        sw.WriteLine(sb.ToString().TrimEnd(OpUtil.TabSeparator));
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