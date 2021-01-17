using Microsoft.VisualStudio.TestTools.UnitTesting;
using C2.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C2.Model;
using System.IO;

namespace C2.Database.Tests
{
    [TestClass()]
    public class DAOFactoryTests
    {
        private DatabaseItem oralcDBI = new DatabaseItem(DatabaseType.Oracle, "114.55.248.85", "orcl", String.Empty, "1521", "test", "test", table: new Table("test", "TEST_100W"));
        private DatabaseItem hiveDBI = new DatabaseItem(DatabaseType.Hive, "10.1.126.4", String.Empty, String.Empty, "10000", "root", "123456");
        List<DatabaseItem> dbis;
        List<BaseDAOImpl> daos;
        public void InitDao()
        {
            dbis = new List<DatabaseItem>() { oralcDBI };
            daos = new List<BaseDAOImpl>();
            foreach (var dbi in dbis)
                daos.Add(DAOFactory.CreatDAO(dbi));
        }
        public int getFileLines(string filePath)
        {
            if (!File.Exists(filePath))
                return 0;
            int lines = 0;
            using (var sr = new StreamReader(filePath))
            {
                var ls = "";
                while ((ls = sr.ReadLine()) != null)
                {
                    lines++;
                }
            }
            return lines;
        }
        [TestMethod()]
        public void CreatDAOTest()
        {
            BaseDAOImpl oracleDAO = DAOFactory.CreatDAO(oralcDBI);
            BaseDAOImpl hiveDAO = DAOFactory.CreatDAO(hiveDBI);
            Assert.IsNotNull(oracleDAO);
            Assert.IsNotNull(hiveDAO);
        }
        [TestMethod()]
        public void TestConnTest()
        {
            InitDao();
            foreach(var dao in daos)
                Assert.IsTrue(dao.TestConn() == true);

        }

        [TestMethod()]
        public void GetUsersTest()
        {
            InitDao();
            foreach (var dao in daos)
            {
                List<string> users = dao.GetUsers();
                Assert.IsTrue(users.Count > 0);
                Console.WriteLine(users[0]);
            }
        }

        [TestMethod()]
        public void GetTablesByUserOrDbTest()
        {
            InitDao();
            foreach (var dao in daos)
            {
                List<Table> tables = dao.GetTablesByUserOrDb(oralcDBI.User);
                Assert.IsTrue(tables.Count > 0);
                Console.WriteLine(tables[0].Name);
            }
        }

        [TestMethod()]
        public void GetTableContentStringTest()
        {
            InitDao();
            foreach (var dao in daos)
            {
                string result = dao.GetTableContentString(oralcDBI.DataTable, 1000);
                Assert.IsTrue(!String.IsNullOrEmpty(result));
                Console.WriteLine(result.Substring(0, 100));
            }
        }

        [TestMethod()]
        public void GetTableContentTest()
        {
            InitDao();
            foreach (var dao in daos)
            {
                List<List<string>> result = dao.GetTableContent(oralcDBI.DataTable, 1000);
                Assert.IsTrue(result.Count > 0 && result[0].Count > 0);
                Console.WriteLine(result[0][0]);
            }
        }

        [TestMethod()]
        public void GetSchemaByTablesTest()
        {
            InitDao();
            foreach (var dao in daos)
            {
                Dictionary<string, List<string>> result = dao.GetSchemaByTables(new List<Table>() { oralcDBI.DataTable });
                Assert.IsTrue(result.Keys.Count > 0);
            }
        }
        [TestMethod]
        public void ExecuteOracleSQLTest()
        {
            InitDao();
            foreach (var dao in daos)
            {
                string sql = @"select * from TEST_100W";
                string filePath = @"D:/tmp.txt";
                int maxReturnNum = 10000;
                bool result = dao.ExecuteOracleSQL(sql, filePath, maxReturnNum);

                Assert.IsTrue(File.Exists(filePath));
                Assert.IsTrue(getFileLines(filePath) == maxReturnNum + 1);
            }
        }
    }
}