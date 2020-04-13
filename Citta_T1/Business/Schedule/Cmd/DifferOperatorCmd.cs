using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Citta_T1.Business.Schedule.Cmd
{
    class DifferOperatorCmd : OperatorCmd
    {
        public DifferOperatorCmd(Triple triple) : base(triple)
        {
        }

        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            string inputFilePath = inputFilePaths.First();
            if (inputFilePath == "")
            {
                Thread.Sleep(5000);
                cmds.Add("echo differ");
            }
            Thread.Sleep(5000);


            return cmds;
        }

    }
}