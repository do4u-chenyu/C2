using System;
using System.IO;
using System.Text;
using C2.Model;
using C2.Utils;
using Npgsql;

namespace C2.Database
{
    class PostgreDAOImpl : BaseDAOImpl
    {
        private static readonly LogUtil log = LogUtil.GetInstance("PostgreDAOImpl");
        private readonly string getUserSQL = @"select pg_database.datname from pg_database";
        private readonly string getTablesSQL = @"select table_name from information_schema.tables where table_schema = 'public'";
        private readonly string getTableContentSQL = @"select * from ""{0}""";
        private readonly string getColNameByTableSQL = @"SELECT a.attnum,a.attname AS field FROM pg_class c,pg_attribute a LEFT OUTER JOIN pg_description b ON a.attrelid=b.objoid AND a.attnum = b.objsubid,pg_type t WHERE c.relname = '{0}' and a.attnum > 0 and a.attrelid = c.oid and a.atttypid = t.oid ORDER BY a.attnum";
        public PostgreDAOImpl(DatabaseItem dbi) : base(dbi) { }

        public string ConnectionString(int time = 1024) 
        {
                return String.Format(
                    @"PORT={0};HOST={1};PASSWORD={2};USER ID={3};Timeout={4};DATABASE={5};",
                    Port,
                    Host,
                    Pass,
                    User,
                    time,
                    Schema);
        }
        public override bool TestConn()
        {
            bool connect = true;
            NpgsqlConnection SqlConn = new NpgsqlConnection(ConnectionString(8));
            try
            {
                SqlConn.Open();
            }
            catch(Exception ex) 
            {
                log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                connect = false;
            }
            finally
            {
                SqlConn.Close();
            }
            return connect;
        }

        public override string Query(string sql, bool header = true, int returnNum = OpUtil.PreviewMaxNum)
        {
            string result = String.Empty;
            NpgsqlConnection SqlConn = new NpgsqlConnection(ConnectionString());
            sql = DbUtil.PurifyOnelineSQL(sql);
            try
            {
                SqlConn.Open();
                result = this.ExecuteQuery(sql, SqlConn, header, returnNum);
            }
            catch (Exception ex) 
            {
                log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                //TODO mhd
                throw ex;
            }
            finally
            {
                SqlConn.Close();
            }
            return result;
        }
       
        private string ExecuteQuery(string sql, NpgsqlConnection conn, bool header, int returnNum)
        {
            StringBuilder sb = new StringBuilder(1024 * 16);
            int totalReturnNum = 0;
            NpgsqlCommand SqlCommand = new NpgsqlCommand(sql, conn);
            SqlCommand.CommandTimeout = 0;
            using (NpgsqlDataReader sdr = SqlCommand.ExecuteReader()) 
            {
                if (sdr.FieldCount == 0)
                    return String.Empty;
                //TODO 空表测试 加断点，看逻辑是否能到，测试Cancel逻辑是否需要
                if (header) 
                {
                    for (int i = 0; i < sdr.FieldCount - 1; i++)
                        sb.Append(sdr.GetName(i)).Append(OpUtil.TabSeparator);
                    sb.Append(sdr.GetName(sdr.FieldCount - 1)).Append(OpUtil.DefaultLineSeparator);
                }
                while (sdr.Read() && totalReturnNum++ < returnNum)
                {
                    for (int i = 0; i < sdr.FieldCount - 1; i++)
                        sb.Append(sdr[i]).Append(OpUtil.TabSeparator);
                    sb.Append(sdr[sdr.FieldCount - 1]).Append(OpUtil.DefaultLineSeparator);
                }
                try
                {
                    SqlCommand.Cancel();
                    sdr.Close();
                }
                catch { }
                
            }
            return sb.TrimEndN().ToString();
        }

        public override bool ExecuteSQL(string sqlText, string outputPath, int maxReturnNum = int.MaxValue)
        {
            bool returnCode = true;
            int totalReturnNum = 0;
            StreamWriter sw = new StreamWriter(outputPath, false);
            //TODO
            NpgsqlConnection SqlConn = new NpgsqlConnection(ConnectionString());
            try
            {
                SqlConn.Open();
                NpgsqlCommand SqlCommand = new NpgsqlCommand(sqlText, SqlConn);
                SqlCommand.CommandTimeout = 0;
                using (NpgsqlDataReader sdr = SqlCommand.ExecuteReader())
                {
                    if (sdr.FieldCount == 0)
                        return true;
                    StringBuilder sb = new StringBuilder(1024);
                    for (int i = 0; i < sdr.FieldCount; i++)
                        sb.Append(sdr.GetName(i)).Append(OpUtil.TabSeparator);
                    sw.WriteLine(sb.TrimEndT().ToString());    // 去掉最后一列的列分隔符
                    while (sdr.Read() && totalReturnNum++ < maxReturnNum)
                    {
                        sb.Clear();
                        for (int i = 0; i < sdr.FieldCount; i++)
                            sb.Append(sdr[i]).Append(OpUtil.TabSeparator);
                        sw.WriteLine(sb.TrimEndT().ToString());
                        sw.Flush();
                    }
                    try
                    {
                        SqlCommand.Cancel();
                        sdr.Close();
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                returnCode = false;
            }
            finally
            {
                //TODO 判断是否为空
                sw.Close();
                SqlConn.Close();
            }
            return returnCode;
        }

        public override string GetTablesSQL(string schema)
        {
            this.Schema = schema;
            return String.Format(this.getTablesSQL, schema);   
        }
        public override string GetColNameBySchemaSQL(string schema)
        {
            return String.Empty;
        }
        public override string GetTableContentSQL(Table table)
        {
            return String.Format(this.getTableContentSQL, table.Name);
        }
        public override string GetUserSQL()
        {
            return String.Format(this.getUserSQL);
        }

        public override string GetColNameByTableSQL(Table table)
        {
            return String.Format(this.getColNameByTableSQL, table.Name);
        }
        public override string DefaultSchema()
        {
            return String.IsNullOrEmpty(this.Schema) ? "postgres" : this.Schema;
        }
    }
}
