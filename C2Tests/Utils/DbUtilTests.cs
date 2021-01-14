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
            List<Table> tables = new List<Table>
            {
                new Table(conn.User, "TMP"),
                new Table(conn.User, "TMP2")
            };
            if (!DbUtil.TestConn(conn))
            {
                Console.WriteLine("无法连接数据库");
                return;
            }
            Console.WriteLine("成功连接数据库");
            Dictionary<String, List<String>> cols = DbUtil.GetTableCol(conn, tables);
            Assert.IsTrue(cols.Keys.Count == 2);
        }
        [TestMethod()]
        public void ConnectOracleTest()
        {
            OraConnection conn = new OraConnection("test@aliyun", "phx2", "123456", "114.55.248.85", "orcl", String.Empty, "1521");
            Console.WriteLine("=================开始测试连接耗时，单位:ms========================");
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch = System.Diagnostics.Stopwatch.StartNew();
            watch.Start();
            long now = watch.ElapsedMilliseconds;
            long startTime = now;

            // 连接
            if (!DbUtil.TestConn(conn))
            {
                Console.WriteLine("无法连接数据库");
                return;
            }
            Console.WriteLine(String.Format("DbUtil.TestConn: {0}ms", watch.ElapsedMilliseconds - now));
            now = watch.ElapsedMilliseconds;

            //刷新架构
            List<string> users = DbUtil.GetUsers(conn);
            Console.WriteLine(String.Format("DbUtil.GetUsers: {0}ms", watch.ElapsedMilliseconds - now));
            now = watch.ElapsedMilliseconds;

            //刷新数据表
            List<Table> tables = DbUtil.GetTablesByUser(conn, conn.User);
            Console.WriteLine(String.Format("DbUtil.GetTablesByUser: {0}ms", watch.ElapsedMilliseconds - now));
            now = watch.ElapsedMilliseconds;
            watch.Stop();
            long totalTime = now - startTime;
            Console.WriteLine("共耗时：{0}ms", totalTime);
            Console.WriteLine("====================结束测试======================");
            Assert.IsTrue(totalTime < 4000);
        }
    }
}