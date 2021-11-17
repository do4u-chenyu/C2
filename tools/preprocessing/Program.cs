using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace preprocessing
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args != null && args.Length == 3)
                {
                    DealData dd = new DealData(args);
                    dd.Deal();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
