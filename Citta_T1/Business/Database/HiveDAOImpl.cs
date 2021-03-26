﻿using C2.Model;
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
            //TODO maxReturnNum
            int totalReturnNum = 0;
            StreamWriter sw = new StreamWriter(outputPath, false);
            //TODO
            StringBuilder sb = new StringBuilder(1024); 
            try
            {
                using (Connection con = new Connection(this.Host, ConvertUtil.TryParseInt(this.Port, 10000),
                                                   this.User, this.Pass))
                                                  
                {
                    var cursor = con.GetCursor();
                    cursor.Execute(string.Format("use {0}", databaseName));          
                    cursor.Execute(DbUtil.PurifyOnelineSQL(sqlText));
                    var oneRow = cursor.FetchOne();
                    //TODO ？null 添加return IDictionary<string, object> oneRow
                    if (!oneRow.IsEmpty())
                    {
                        // 添加表头
                        IDictionary<string, object> iDict = oneRow;
                        for (int i = 0; i < iDict.Count; i++)
                        {
                            //TODO foreach
                            string key = CutColumnName(iDict.Keys.ElementAt(i));
                            sb.Append(key).Append(OpUtil.TabSeparator);
                        }
                        if (iDict.Count > 0)
                        {
                            //TODO 刘 是否多余
                            sw.WriteLine(sb.ToString().TrimEnd(OpUtil.TabSeparator));
                        }
                        
                    }
   
                    while (oneRow != null &&  totalReturnNum++ < maxReturnNum)
                    {
                        sb = new StringBuilder(1024);
                        //TODO clear
                        IDictionary<string, object> dict = oneRow;//TODO 多余
                        foreach (var key in dict.Keys)
                        {
                            sb.Append(dict[key].ToString()).Append(OpUtil.TabSeparator);
                        }
                        if (!dict.Keys.IsEmpty())
                        {
                            sw.WriteLine(sb.ToString().TrimEnd(OpUtil.TabSeparator));
                            sw.Flush();
                        }

                        oneRow = cursor.FetchOne();//TODO 太远了
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
            StringBuilder sb = new StringBuilder(1024 * 16);
            try
            {
                using (Connection con = new Connection(this.Host, ConvertUtil.TryParseInt(this.Port),
                                                   this.User, this.Pass))
                                                   //TODO port using 是否可以调close
                {
                  
                    var cursor = con.GetCursor();
                    cursor.Execute(string.Format("use {0}", databaseName));
                    foreach (var s in sql.Trim().Split(';'))
                    {
                        if (!String.IsNullOrEmpty(s))
                            cursor.Execute(s);
                    }
                    var list = cursor.FetchMany(returnNum);//TODO returnNum可能会很大 stringbuilder可能不够
                    if (header && !list.IsEmpty())
                    {
                        // 添加表头
                        IDictionary<string, object> iDict = list[0];
                        for (int i = 0; i < iDict.Count; i++)//TODO foreach
                        {
                            string key = CutColumnName(iDict.Keys.ElementAt(i));
                            sb.Append(key).Append(OpUtil.TabSeparator);
                        }
                        if (iDict.Count > 0)
                            sb.Remove(sb.Length - 1, 1).Append(OpUtil.DefaultLineSeparator); // 最后一列多加了个\t，去掉       

                    }
                    foreach (IDictionary<string, object> dict in list)
                    {
                        foreach (var key in dict.Keys)
                        {
                            sb.Append(dict[key].ToString()).Append(OpUtil.TabSeparator);
                        }
                        if (!dict.Keys.IsEmpty())
                            sb.Remove(sb.Length - 1, 1).Append(OpUtil.DefaultLineSeparator);
                    }
                }
            }
            catch (Exception ex)
            {
                
                log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());   // 辅助工具类，showmessage不能放在外面
                throw ex;

            }
            return sb.ToString().Trim(OpUtil.DefaultLineSeparator);
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
