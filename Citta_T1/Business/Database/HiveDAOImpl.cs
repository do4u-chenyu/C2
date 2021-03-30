using C2.Model;
using C2.Utils;
using Hive2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace C2.Database
{
    public class HiveDAOImpl : BaseDAOImpl
    {
        private static readonly LogUtil log = LogUtil.GetInstance("HiveDAOImpl");
        private readonly string getUserSQL = @"show databases";
        private readonly string getTablesSQL = @"use {0};show tables;";
        private readonly string getTableContentSQL = @"use {0};select * from {1}";
        private readonly string getColNameByTableSQL = "desc {0}";
        private readonly string databaseName;
        public HiveDAOImpl(DatabaseItem dbi) : base(dbi)
        {
            this.databaseName = dbi.Schema;
        }
        public override bool TestConn()
        {
            using (Connection con = new Connection(this.Host, ConvertUtil.TryParseInt(this.Port, 10000),
                                                   this.User, this.Pass))
            {
                try
                {

                    LimitTimeout(con);
                    con.Open();

                    return true;
                }
                catch (Exception ex)
                {
                    log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                    return false;
                }
            }
        }
        private void LimitTimeout(Connection conn)
        {
            conn.SetSocketTimeout = 8000;
            conn.SetTcpReceiveTimeout = 8000;
            conn.SetTcpSendTimeout = 8000;
        }
        public override bool ExecuteSQL(string sqlText, string outputPath, int maxReturnNum = int.MaxValue)
        {
            int totalReturnNum = 0;
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(outputPath, false);
                StringBuilder sb = new StringBuilder(1024);
                using (Connection con = new Connection(this.Host, ConvertUtil.TryParseInt(this.Port, 10000),
                                                   this.User, this.Pass))
                                                  
                {
                    var cursor = con.GetCursor();
                    cursor.Execute(string.Format("use {0}", databaseName));
                    cursor.Execute(DbUtil.PurifyOnelineSQL(sqlText));

                    // 获取表头信息
                    TTableSchema tableSchema = cursor.GetSchema();
                    foreach(TColumnDesc column in tableSchema.Columns)
                        sb.Append(CutColumnName(column.ColumnName)).Append(OpUtil.TabSeparator);
                    sw.WriteLine(sb.ToString().TrimEnd(OpUtil.TabSeparator));
                    // 获取一行行内容
                    IDictionary<string, object> iDict;
                    while ((iDict = cursor.FetchOne()) != null && totalReturnNum++ < maxReturnNum)
                    {
                        sw.WriteLine(String.Join(OpUtil.TabSeparatorString, iDict.Values));
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                return false;
            }
            finally
            {
                if (sw != null)
                    sw.Close();

            }
            return true;
        }


        public string CutColumnName(string name)
        {
            int sep = name.IndexOf('.');
            return sep == -1 ? name : name.Substring(sep + 1);
        }

        public override string Query(string sql, bool header = true, int returnNum = OpUtil.PreviewMaxNum)
        {
            int capacity = Math.Max(0, Math.Min(int.MaxValue, returnNum));
            StringBuilder sb = new StringBuilder(1024 * capacity);
            try
            {
                using (Connection con = new Connection(this.Host, ConvertUtil.TryParseInt(this.Port),
                                                   this.User, this.Pass))                                                
                {
                  
                    var cursor = con.GetCursor();
                    cursor.Execute(string.Format("use {0}", databaseName));
                
                    foreach (var s in sql.Trim().Split(';'))
                    {
                        if (!String.IsNullOrEmpty(s))
                            cursor.Execute(s);
                    }
                    var list = cursor.FetchMany(returnNum);// 查询结果不会为null


                    // 获取表头信息
                    if (header)
                    {
                        TTableSchema tableSchema = cursor.GetSchema();
                        List<string> columnNames = new List<string>();
                        foreach (TColumnDesc column in tableSchema.Columns)
                            columnNames.Add(CutColumnName(column.ColumnName));
                        sb.Append(String.Join(OpUtil.TabSeparatorString, columnNames)).Append(OpUtil.LineSeparator);
                    }
                   
                    // 获取一行行内容
                    foreach (IDictionary<string, object> dict in list)
                    {
                        sb.Append(String.Join(OpUtil.TabSeparatorString, dict.Values)).Append(OpUtil.LineSeparator);
                    }
                }
            }
            catch (Exception ex)
            {
                
                log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());  
                throw ex;

            }
            return sb.ToString().Trim(OpUtil.LineSeparator);
        }

        public override string GetTablesSQL(string schema)
        {
            return String.Format(this.getTablesSQL, schema);
        }
        public override string GetColNameByTablesSQL(List<Table> tables)
        {
            return String.Empty;
        }
        public override string GetTableContentSQL(Table table)
        {
            return String.Format(getTableContentSQL, this.Schema, table.Name);
        }
        public override string GetUserSQL()
        {
            return this.getUserSQL;
        }
        public override string DefaultSchema()
        {
            return String.IsNullOrEmpty(this.Schema) ? "default" : this.Schema;
        }
        public override string GetColNameByTableSQL(Table table)
        {
            return String.Format(this.getColNameByTableSQL, table.Name);
        }
    }
}
