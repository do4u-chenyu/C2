using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Citta_T1.Business.Schedule.Cmd
{
    class MinOperatorCmd : OperatorCmd
    {
        public MinOperatorCmd(Triple triple) : base(triple)
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
                cmds.Add("echo min");
            }
            Thread.Sleep(5000);
            string inputfieldLine = (int.Parse(option.GetOption("minfield")) + 1).ToString();
            string outfieldLine = TransOutputField(option.GetOption("outfield").Split(','));

            cmds.Add(string.Format("sbin\\sort.exe -nr -k {0} {1} | sbin\\head.exe -n1 | sbin\\awk.exe -F'\\t' '{{ print {2}}}'>> {3}", inputfieldLine, inputFilePath, outfieldLine, this.outputFilePath));
            return cmds;
        }

    }
}