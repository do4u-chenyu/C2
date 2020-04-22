using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Citta_T1.Business.Schedule.Cmd
{
    class MaxOperatorCmd : OperatorCmd
    {
        public MaxOperatorCmd(Triple triple) : base(triple)
        {
        }

        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            string inputFilePath = inputFilePaths.First();//输入文件
            string inputfieldLine = TransInputLine(option.GetOption("maxfield"));//最大值字段
            string outfieldLine = TransOutputField(option.GetOption("outfield").Split(','));//输出字段

            cmds.Add(string.Format("{0} {1} | sbin\\sort.exe {2} -nr -k {3} | sbin\\head.exe -n1 | sbin\\awk.exe -F'\\t' -v OFS='\\t' '{{ print {4}}}'>> {5}", TransInputfileToCmd(inputFilePath), inputFilePath, this.sortConfig, inputfieldLine, outfieldLine,this.outputFilePath));

            return cmds;
        }
    }
}
