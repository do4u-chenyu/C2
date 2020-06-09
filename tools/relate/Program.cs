using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace relate
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args != null && args.Length == 9)
                {
                    DealRelate dr = new DealRelate(args);
                    dr.RelateTwoTables();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
