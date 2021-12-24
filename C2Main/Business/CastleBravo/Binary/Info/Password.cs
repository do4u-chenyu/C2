using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.CastleBravo.Binary.Info
{
    class Password
    {
        private static Password instance = null;

        public static Password GetInstance()
        {
            if (instance == null)
                instance = new Password();
            return instance;
        }

        private Password()
        {

        }
    }
}
