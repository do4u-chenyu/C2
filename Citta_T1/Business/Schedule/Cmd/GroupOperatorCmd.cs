using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Citta_T1.Business.Schedule.Cmd
{
    class GroupOperatorCmd : OperatorCmd
    {
        public GroupOperatorCmd(Triple triple) : base(triple)
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
                cmds.Add("echo group");
            }
            string inputfieldLine = TransInputLine(option.GetOption("maxfield"));
            string outfieldLine = TransOutputField(option.GetOption("outfield").Split(','));

            //cmds.Add(string.Format("sbin\\tail.exe -n +2 {0} | sbin\\sort.exe -S 200M -T {1} -nr -k {2} | sbin\\head.exe -n1 | sbin\\awk.exe -F'\\t' -v OFS='\\t' '{{ print {3}}}'>> {4}", inputFilePath, this.tmpSortPath, inputfieldLine, outfieldLine, this.outputFilePath));

            return cmds;
        }
    }
}