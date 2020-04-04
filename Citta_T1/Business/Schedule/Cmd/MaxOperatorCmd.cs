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

        public string GenCmd()
        {
            string inputFilePath = inputFilePaths.First();

            //以后算子路径功能写完后去掉
            if (inputFilePath == "")
            {
                Thread.Sleep(5000);
                return "echo max";
            }

            string inputfieldLine = (int.Parse(option.GetOption("maxfield")) + 1).ToString();
            string outfieldLine = TransOutputField(option.GetOption("outfield").Split(','));

            string cmd = string.Format("sbin\\sort.exe -k {0} {1} | sbin\\head.exe -n1 | sbin\\awk.exe -F'\\t' '{{ print {2}}}'>> {3}", inputfieldLine, inputFilePath, outfieldLine, this.outputFilePath);
            return cmd;
        }
    }
}
