using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Business.Schedule.Cmd
{
    class KeyWordOperatorCmd : OperatorCmd
    {
        public KeyWordOperatorCmd(Triple triple) : base(triple)
        {
        }
        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();

            //TODO
            cmds.Add("sbin\\echo.exe keyword");

            return cmds;
        }
    }
}
