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
        public static IDAO CreatDAO(DatabaseItem dbi)
        {
            switch (dbi.Type)
            {
                case DatabaseType.Oracle:
                    return new OracleDAOImpl(dbi);
                case DatabaseType.Hive:
                    return new HiveDAOImpl(dbi);
                default:
                    return null;
            }
        }
    }
}
