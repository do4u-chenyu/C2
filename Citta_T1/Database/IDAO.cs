using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Database
{
    public interface IDAO
    {
        bool TestConn();
        string Query(string sql);
    }
}
