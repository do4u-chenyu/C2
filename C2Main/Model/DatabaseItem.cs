﻿using C2.Database;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Model
{
    public enum DatabaseType
    {
        Null,
        Oracle,
        Hive,
        Postgre
    }
    public class DatabaseItem
    {
        public DatabaseType Type { set; get; }//数据库类型
        public string Server { set; get; }//服务器
        public string Service { set; get; }//服务名
        public string SID { set; get; }//服务sid
        public string Port { set; get; }//端口
        public string User { set; get; }//用户
        public string Password { set; get; }//密码
        public string Schema { set; get; }//架构
        public Table DataTable { set; get; }//表名
        //所有信息合并字符串，便于比较是否一致
        public string AllDatabaseInfo 
        {
            get
            { 
                return string.Join(",", Type.ToString(), Server, SID, Service, Port, User, Password, Schema, DataTable == null ? string.Empty:DataTable.Name); 
            }
        }
        public string PrettyDatabaseInfo
        {
            get
            {
                string serviceName = string.IsNullOrEmpty(Service) ? SID : Service; 
                return string.Format("{0}@//{1}:{2}/{3}", User, Server,Port,serviceName);
            }
        }

        public DatabaseItem() { }
        public DatabaseItem(DatabaseType type, string server, string sid, string service, string port, string user, string password, string schema = "", Table table =null)
        {
            Type = type;
            Server = server;
            SID = sid;
            Service = service;
            Port = port;
            User = user;
            Password = password;
            Schema = String.IsNullOrEmpty(schema) ? DbUtil.DefaultSchema(type, this.User): schema;
            DataTable = table;
        }

        public DatabaseItem(string allDatabaseInfo)
        {
            string[] tmpInfo = allDatabaseInfo.Split(',');
            if (tmpInfo.Count() < 9)
                return;
            Type = (DatabaseType)Enum.Parse(typeof(DatabaseType), tmpInfo[0]);
            Server = tmpInfo[1];
            SID = tmpInfo[2];
            Service = tmpInfo[3];
            Port = tmpInfo[4];
            User = tmpInfo[5];
            Password = tmpInfo[6];
            Schema = tmpInfo[7];
            DataTable = new Table(tmpInfo[7], tmpInfo[8]);
        }

        public DatabaseItem Clone()
        {
            DatabaseItem tmpDatabaseItem = new DatabaseItem(
                this.Type,
                this.Server,
                this.SID,
                this.Service,
                this.Port,
                this.User,
                this.Password,
                this.Schema,
                this.DataTable);
            return tmpDatabaseItem;
        }
    }
}
