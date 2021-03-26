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
    }
}