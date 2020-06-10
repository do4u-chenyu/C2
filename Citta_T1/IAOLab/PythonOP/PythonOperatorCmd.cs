using System.Collections.Generic;

namespace Citta_T1.Business.Schedule.Cmd
{
    class PythonOperatorCmd : OperatorCmd
    {

        public PythonOperatorCmd(Triple triple) : base(triple)
        {

        }

        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            string cmdPython = option.GetOption("cmd");

            cmds.Add(cmdPython);
            return cmds;
        }
    }
}
