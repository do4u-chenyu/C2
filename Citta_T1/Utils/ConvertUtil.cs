using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Utils
{
    class ConvertUtil
    {
        public static bool TryParseBool(string value, bool defaultValue = false)
        {
            try
            {
                return bool.Parse(value);
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
