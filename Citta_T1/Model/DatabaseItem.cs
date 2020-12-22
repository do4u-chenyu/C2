using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Model
{
    public enum DatabaseType
    {
        Oracle,
        Hive
    }
  public  class DatabaseItem
    {

        public DatabaseType Type { set; get; }//数据库类型
        public string Server { set; get; }//服务器
        public string Service { set; get; }//服务sid
        public string Port { set; get; }//端口
        public string User { set; get; }//用户
        public string Password { set; get; }//密码
        public string Group { set; get; }//架构
        public string Table { set; get; }//表名

        public DatabaseItem()
        {

        }

        public DatabaseItem(DatabaseType type, string server, string service, string port, string user, string password, string group, string table)
        {
            Type = type;
            Server = server;
            Service = service;
            Port = port;
            User = user;
            Password = password;
            Group = group;
            Table = table;
        }
    }
}
