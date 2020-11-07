using System.Collections.Generic;
using C2.Model.Widgets;

namespace C2.Business.Schedule.Cmd
{
    class PythonOperatorCmd : OperatorCmd
    {

        public PythonOperatorCmd(Triple triple) : base(triple)
        {
        }
        public PythonOperatorCmd(OperatorWidget operatorWidget) : base(operatorWidget)
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
