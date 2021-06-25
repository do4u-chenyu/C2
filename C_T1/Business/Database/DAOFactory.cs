using C2.Model;

namespace C2.Database
{
    public class DAOFactory
    {
        public static IDAO CreateDAO(DatabaseItem dbi)
        {
            switch (dbi.Type)
            {
                case DatabaseType.Oracle:
                    return new OracleDAOImpl(dbi);
                case DatabaseType.Hive:
                    return new HiveDAOImpl(dbi);
                case DatabaseType.Postgre:
                    return new PostgreDAOImpl(dbi);
                default:
                    return new EmptyDAOImpl();
            }
        }
    }
}
