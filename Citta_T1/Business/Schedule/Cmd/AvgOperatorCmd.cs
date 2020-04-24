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
            string inputFilePath = inputFilePaths.First();//输入文件
            string avgfieldLine = TransInputLine(option.GetOption("avgfield"));//取平均值字段

            cmds.Add(string.Format("{0} | sbin\\awk.exe '{{a+=${1}}}END{{print a/NR}}' >> {2}", TransInputfileToCmd(inputFilePath), avgfieldLine, this.outputFilePath));
            return cmds;
        }
    }
}
