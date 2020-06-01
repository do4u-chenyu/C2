using System.Collections.Generic;

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
