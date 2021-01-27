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
        public List<string> Columns { get; set; }

        public Table(string userName, string name)
        {
            UserName = userName;
            Name = name;
            Columns = new List<string>();
        }        
        public Table(string userName, string name, List<string> cols)
        {
            UserName = userName;
            Name = name;
            Columns = cols;
        }
    }
}
