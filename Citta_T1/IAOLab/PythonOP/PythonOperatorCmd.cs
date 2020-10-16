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
            string path = System.IO.Path.GetDirectoryName(option.GetOption("pyFullPath"));
            string cmdPython = option.GetOption("cmd");

            cmds.Add("cd /d " + path);
            cmds.Add(cmdPython);
            return cmds;
        }
    }
}
