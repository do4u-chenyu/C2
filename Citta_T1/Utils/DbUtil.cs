using C2.Database;
using C2.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Hive2;
using System.Linq;

namespace C2.Utils
{
    public static class DbUtil
    {
        private static readonly LogUtil log = LogUtil.GetInstance("DbUtil");

        public static string HiveDeaultSchema = "default";
        public static string PostgreDeaultSchema = "postgres";
        public static StringBuilder TrimEndN(this StringBuilder sb)
        {
            if(sb.Length > 0)
                return sb.Replace(@"\n", @"\0", sb.Length - 1, 1);
            return sb;
        }
        public static StringBuilder TrimEndT(this StringBuilder sb)
        {
            if (sb.Length > 0)
                return sb.Replace(@"\t", @"\0", sb.Length - 1, 1);
            return sb;
        }
        public static Dictionary<string, List<string>> StringToDict(string v)
        {
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
            foreach(string line in v.Split(OpUtil.DefaultLineSeparator))
            {
                var kv = line.Split(OpUtil.TabSeparator);
                if (kv.Length != 2)
                    continue;
                string key = kv[0];
                string val = kv[1];
                if (result.Keys.Contains(key))
                    result[key].Add(val);
                else
                    result.Add(key, new List<string>() { val });
            }
            return result;
        }
        public static List<List<string>> StringTo2DString(string contentString)
        {
            List<List<string>> ret = new List<List<string>>();
            if (!String.IsNullOrEmpty(contentString))
            {
                string[] lines = contentString.Split(OpUtil.DefaultLineSeparator);
                for (int i = 0; i < lines.Length; i++)
                {
                    ret.Add(new List<string>(lines[i].Split(OpUtil.TabSeparator)));
                }
            }
            return ret;
        }

        public static string DefaultSchema(DatabaseType type, string user)
        {
            switch (type)
            {
                case DatabaseType.Oracle:
                    return user;
                case DatabaseType.Hive:
                    return HiveDeaultSchema;
                case DatabaseType.Postgre:
                    return PostgreDeaultSchema;
                default:
                    return String.Empty;
            }
        }
        public static string PurifyOnelineSQL(string sql)
        {
            return sql.Trim().TrimEnd(';');
        }
    }
}