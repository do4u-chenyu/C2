﻿using C2.Model;
using C2.Utils;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace C2.Database
{
    public class OracleDAOImpl : BaseDAOImpl
    {
        private static readonly LogUtil log = LogUtil.GetInstance("OracleImpl");
        private readonly string getUserSQL = @"select distinct owner from all_tables";
        private readonly string getTablesSQL = @"select table_name from all_tables where owner='{0}' order by table_name";
        private readonly string getTableContentSQL = @"select * from {0}.{1} where rownum <= {2}";
        private readonly string getColNameBySchemaSQL = @"select a.table_name, a.column_name from all_tab_columns a where table_name in {0}";
        private readonly string getColNameByTablesSQL = @"select a.table_name, a.column_name from all_tab_columns a where table_name in ('{0}')";

        public OracleDAOImpl(DatabaseItem dbi) : base(dbi) { }

        public string ConnectionString
        {
            get
            {
                // TODO DK 换位置
                if (!String.IsNullOrEmpty(Service)) // Is it a service name connection?
                    return String.Format(
                      "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1}))(CONNECT_DATA=(SERVICE_NAME={2})));User Id={3};Password={4};Connection Lifetime=60;Connection Timeout=8",
                      Host,
                      Port,
                      Service,
                      User,
                      Pass);
                else // Is it a SID connection?
                    return String.Format(
                      "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1}))(CONNECT_DATA=(SID={2})));User Id={3};Password={4};Connection Lifetime=60;Connection Timeout=8",
                      Host,
                      Port,
                      Sid,
                      User,
                      Pass);
            }
        }
        public override bool TestConn()
        {
            OracleConnection con = new OracleConnection(this.ConnectionString);
            try
            {
                con.Open();
                return true;
            }
            catch (Exception ex)
            {
                log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                return false;
            }
        }
        public override string Query(string sql, bool header=true) // TODO Dk
        {
            StringBuilder sb = new StringBuilder(1024 * 16); // TODO
            try
            {
                using (OracleConnection con = new OracleConnection(this.ConnectionString))
                {
                    con.Open();
                    OracleCommand comm = new OracleCommand(sql, con);
                    using (OracleDataReader rdr = comm.ExecuteReader())  // rdr.Close()
                    {
                        if (header)
                        {
                            for (int i = 0; i < rdr.FieldCount - 1; i++)
                                sb.Append(rdr.GetName(i)).Append(OpUtil.DefaultFieldSeparator);
                            sb.Append(rdr.GetName(rdr.FieldCount - 1)).Append(OpUtil.DefaultLineSeparator);
                        }
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
                QueryFailureException(ex.Message);
            }

            return sb.ToString().Trim(OpUtil.DefaultLineSeparator);
        }
        public override string LimitSQL(string sql)
        {
            // TODO DK 两个where
            return String.Format("{0} where rownum <= {1}", sql, OpUtil.PreviewMaxNum);
        }
        public override string GetTablesSQL(string schema)
        {
            return String.Format(this.getTablesSQL, schema.ToUpper());
        }
        public override string GetColNameBySchemaSQL(string schema)
        {
            return String.Format(this.getColNameBySchemaSQL, 
                        String.Format(@"(select table_name from all_tables where owner='{0}') order by a.table_name", schema)
                   );
        }
        public override string GetColNameByTablesSQL(List<Table> tables)
        { 
            // TODO DK
            String[] tableNames = new String[tables.Count];
            if (tableNames.Length != 0)
            {
                for (int i = 0; i < tableNames.Length; i++)
                    tableNames[i] = tables[i].Name;
                return String.Format(this.getColNameByTablesSQL, String.Join("','", tableNames));
            }
            return String.Empty;
        }
        public override string GetTableContentSQL(Table table, int maxNum)
        {
            return String.Format(this.getTableContentSQL, this.Schema, table.Name, maxNum);
        }
        public override string GetUserSQL()
        {
            return this.getUserSQL;
        }
        public override string GetColNameByTableSQL(Table table)
        {
            return this.GetColNameByTablesSQL(new List<Table>() { table });
        }
       
        protected override QueryResult ExecuteSQL_Page(string sqlText, int pageSize, int pageIndex, int maxNum, bool returnHeader)
        {
            /*
             * pageIndex start from 0.
             */
            QueryResult result;
            StringBuilder sb = new StringBuilder(1024 * 16);
            int returnNum = 0;
            try
            {
                using (OracleConnection con = new OracleConnection(this.ConnectionString))
                {
                    con.Open();
                    string sqlPage = String.Format(@"select * from 
	                                                        (select rownum as rowno,tmp.* from ({0}) tmp where rownum<={1}) 
                                                        t where t.rowno>{2}",
                                        sqlText,
                                        pageSize * (pageIndex + 1),
                                        pageSize * (pageIndex));
                    OracleCommand comm = new OracleCommand(sqlPage, con);
                    using (OracleDataReader rdr = comm.ExecuteReader())  // rdr.Close()
                    {
                        if (returnHeader)
                        {
                            for (int i = 0; i < rdr.FieldCount - 1; i++)
                                sb.Append(rdr.GetName(i)).Append(OpUtil.DefaultFieldSeparator);
                            sb.Append(rdr.GetName(rdr.FieldCount - 1)).Append(OpUtil.DefaultLineSeparator);

                         }

                        while (rdr.Read() && returnNum < maxNum)
                        {
                            // TODO 去掉第一列
                            for (int i = 0; i < rdr.FieldCount - 1; i++)
                                sb.Append(rdr[i]).Append(OpUtil.DefaultFieldSeparator);
                            returnNum += 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());   // 辅助工具类，showmessage不能放在外面
            }
            finally
            {
                result.content = sb.ToString();
                result.returnNum = returnNum;
            }
            return result;
        }
        public override string DefaultSchema()
        {
            return String.IsNullOrEmpty(this.Schema) ? this.User.ToUpper() : this.Schema.ToUpper();
        }
    }
}