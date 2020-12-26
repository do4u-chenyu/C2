using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace C2.Database
{
    public class Table
    {
        public string Name;
        public bool View = false;
        public string UserName;

        public Table(string userName)
        {
            UserName = userName;
        }
    }
}
