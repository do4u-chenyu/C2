using Microsoft.VisualStudio.TestTools.UnitTesting;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C2.Database;

namespace C2.Utils.Tests
{
    [TestClass()]
    public class DbUtilTests
    {
        [TestMethod()]
        public void GetTableColTest()
        {
            OraConnection conn = new OraConnection("test@aliyun", "phx2", "123456", "114.55.248.85", "orcl", String.Empty, "1521");
            List<Table> tables = new List<Table>();
            tables.Add(new Table(conn.User, "TMP"));
            tables.Add(new Table(conn.User, "TMP2"));
            if (!DbUtil.TestConn(conn))
            {
                Console.WriteLine("无法连接数据库");
                return;
            }
            Console.WriteLine("成功连接数据库");
            Dictionary<String, List<String>> cols = DbUtil.GetTableCol(conn, tables);
            Assert.IsTrue(cols.Keys.Count == 2);
        }
    }
}