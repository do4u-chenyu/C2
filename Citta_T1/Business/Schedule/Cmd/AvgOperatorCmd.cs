using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Citta_T1.Business.Schedule.Cmd
{
    class AvgOperatorCmd : OperatorCmd
    {
        public AvgOperatorCmd(Triple triple) : base(triple)
        {
        }

        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            string inputFilePath = inputFilePaths.First();

            //以后算子路径功能写完后去掉
            if (inputFilePath == "")
            {
                Thread.Sleep(5000);
                cmds.Add("echo avg");
            }
            Thread.Sleep(5000);
            string avgfieldLine = TransInputLine(option.GetOption("avgfield"));

            cmds.Add(string.Format("sbin\\tail -n +2 {1} | sbin\\awk.exe '{{a+=${0}}}END{{print a/NR}}' >> {2}", avgfieldLine, inputFilePath, this.outputFilePath));
            return cmds;
        }
    }
}
