using Microsoft.VisualStudio.TestTools.UnitTesting;
using C2.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C2.Model;

namespace C2.Database.Tests
{
    [TestClass()]
    public class HiveDAOImplTests
    {
        DatabaseItem dbi;
        HiveDAOImpl hiveDao;
        [TestInitialize]
        public void Init()
        {
            dbi = new DatabaseItem(DatabaseType.Hive, "10.1.126.4", String.Empty, String.Empty, "10000", "root", "123456", table: new Table("default", "hive1"));
            hiveDao = new HiveDAOImpl(dbi);

        }
        [TestMethod()]
        public void CutColumnNameTest()
        {
            // 空测试
            Assert.AreEqual("", hiveDao.CutColumnName(""));
            // 无.测试
            Assert.AreEqual("nihao", hiveDao.CutColumnName("nihao"));
            //多.测试
            Assert.AreEqual("hadsfj.dj", hiveDao.CutColumnName("ni.hadsfj.dj"));
            //正常测试
            Assert.AreEqual("你好", hiveDao.CutColumnName("dsaf.你好"));
            //正常测试
            Assert.AreEqual("你好", hiveDao.CutColumnName("春他.你好"));

        }

        [TestMethod()]
        public void ExecuteSQLTest()
        {
            // 有数据的表
            string sqlText = "select * from default.students";
            string outputPath = @"D:\test202103_1.txt";
            int maxReturnNum = 100;
            bool notfail0 = hiveDao.ExecuteSQL(sqlText, outputPath, maxReturnNum);
            Assert.AreEqual(true, notfail0);
            // 无效路径
            string outputPath1 = @"h:\test20210321_2.txt";
            bool notfail1 = hiveDao.ExecuteSQL(sqlText, outputPath1, maxReturnNum);
            Assert.AreEqual(false, notfail1);
            // 错误的表
            sqlText = "select * from default.student";
            string outputPath2 = @"h:\test2021032_3.csv";
            bool notfail2 = hiveDao.ExecuteSQL(sqlText, outputPath2, maxReturnNum);
            Assert.AreEqual(false, notfail2);
            // 限制行数的查询
            string sqlText3 = "select * from default.100w limit 20";
            string outputPath3 = @"D:\test2021032_4.txt";
            int maxReturnNum3 = 100;
            bool notfail3 = hiveDao.ExecuteSQL(sqlText3, outputPath3, maxReturnNum3);
            Assert.AreEqual(true, notfail3);

        }

        [TestMethod()]
        public void QueryTest()
        {
            string getUserSQL = @"show databases";
            string getTablesSQL = @"use default;show tables;";
            string getTableContentSQL = @"use default;select * from students";
            string getColNameByTableSQL = "use default;desc students";
            bool header = true;
            // 
            string databaseNames = hiveDao.Query(getUserSQL, header, 1000);
            Console.WriteLine("数据库名:"+databaseNames);
            //
            string tableNames = hiveDao.Query(getTablesSQL, header, 1000);
            Console.WriteLine("表名:" + tableNames);
            //
            string content = hiveDao.Query(getTableContentSQL, header, 1000);
            Console.WriteLine("内容:" + content);
            //
            string structure = hiveDao.Query(getColNameByTableSQL, header, 1000);
            Console.WriteLine("表结构:" + structure);
        }
    }
}