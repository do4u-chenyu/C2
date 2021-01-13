using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace C2.Database
{
    public class Table
    {
        public string Name { get; }
        public bool View = false;
        public string UserName { get; }
        public List<string> columns;

        public Table(string userName, string name, List<string> cols=null)
        {
            UserName = userName;
            Name = name;
            columns = cols;
        }
    }
}
