using C2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Database
{
    public class DAOFactory
    {
        public static BaseDAOImpl dao
        {
            get;
            private set;
        }
        public static BaseDAOImpl CreatDAO(DatabaseItem dbi)
        {
            switch (dbi.Type)
            {
                case DatabaseType.Oracle:
                    dao = new OracleDAOImpl(dbi);
                    break;
                case DatabaseType.Hive:
                    dao = new HiveDAOImpl(dbi);
                    break;
                default:
                    break;
            }
            return dao;
        }
    }
}
