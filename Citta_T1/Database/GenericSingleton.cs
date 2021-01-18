using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Database
{
    // TODO DK 干掉
    public class GenericSingleton<T> where T : Form, new()
    {
        private static T t = null;
        public static T CreateInstance()
        {
            if (null == t || t.IsDisposed)
            {
                t = new T();
            }
            return t;
        }
    }
}
