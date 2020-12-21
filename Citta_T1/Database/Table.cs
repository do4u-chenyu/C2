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
        public List<string> Columns = new List<string>();
        public Schema ParentSchema;

        public Table(Schema Parent)
        {
            ParentSchema = Parent;
        }
    }
}
