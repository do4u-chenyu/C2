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
        private DatabaseItem hiveDBI = new DatabaseItem(DatabaseType.Hive, "10.1.126.4", String.Empty, String.Empty, "10000", "root", "123456", table: new Table("root", "hive1"));
        List<DatabaseItem> dbis;
        List<IDAO> daos;
        private string[] userOrDb;
        public void InitDao()
        {
            dbis = new List<DatabaseItem>() {
                oralcDBI,
                hiveDBI 
            };
            userOrDb = new string[]
            {
                oralcDBI.User,
                "default"
            };
            daos = new List<IDAO>();
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
            IDAO oracleDAO = DAOFactory.CreatDAO(oralcDBI);
            IDAO hiveDAO = DAOFactory.CreatDAO(hiveDBI);
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
                Console.WriteLine("Start");
                List<string> users = dao.GetUsers();
                Assert.IsTrue(users.Count > 0);
                Console.WriteLine(users.Count);
            }
        }

        [TestMethod()]
        public void GetTablesByUserOrDbTest()
        {
            InitDao();
            for (int i = 0; i < daos.Count; i++)
            {
                List<Table> tables = daos[i].GetTables(userOrDb[i]);
                Assert.IsTrue(tables.Count > 0);
                Console.WriteLine(tables[0].Name);
            }
        }

        [TestMethod()]
        public void GetTableContentStringTest()
        {
            InitDao();
            for(int i = 0; i < daos.Count; i++)
            {
                string result = daos[i].GetTableContentString(userOrDb[i], dbis[i].DataTable, 1000);
                Assert.IsTrue(!String.IsNullOrEmpty(result));
            }
        }

        [TestMethod()]
        public void GetTableContentTest()
        {
            InitDao();
            for (int i = 0; i < daos.Count; i++)
            {
                List<List<string>> result = daos[i].GetTableContent(userOrDb[i], dbis[i].DataTable, 1000);
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
                Dictionary<string, List<string>> result = dao.GetColNameByTables(new List<Table>() { oralcDBI.DataTable });
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